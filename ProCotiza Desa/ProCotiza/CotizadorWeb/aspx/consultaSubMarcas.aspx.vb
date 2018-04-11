'BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.
'BUG-PC-09:AMR:16/11/2016 CUTARMA01:No pertime espacios en blanco al ingresar el nombre de la marca

Imports System.Data

Partial Class aspx_consultaSubMarcas
    Inherits System.Web.UI.Page
    Dim strErr As String = ""


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()

        If Not IsPostBack Then
            CargaCombos()
            BuscaDatos()
            LimpiaFiltros()
        End If
    End Sub

    Public Sub LimpiaError()
        lblMensaje.Text = ""
    End Sub

    Private Sub LimpiaFiltros()
        cmbEstatus.SelectedValue = 0
        ddlmarca.SelectedValue = 0
        txtNom.Text = ""
    End Sub

    Public Sub MensajeError(ByVal strMsj As String)
        strMsj = Replace(strMsj, "'", "")
        strMsj = Replace(strMsj, """", "")
        strMsj = Replace(strMsj, "(", "")
        strMsj = Replace(strMsj, ")", "")
        lblMensaje.Text = "<script>alert('" & strMsj & "')</script>"
    End Sub

    Private Function CargaCombos() As Boolean
        Dim objCombo As New clsProcGenerales
        Dim objMarcas As New SNProcotiza.clsMarcas
        Dim dtsRes As New DataSet
        Try
            CargaCombos = False

            'Combo Estatus
            dtsRes = objCombo.ObtenInfoParametros(1, strErr)
            If Trim$(strErr) = "" Then
                objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbEstatus, strErr, True)
                If Trim$(strErr) <> "" Then
                    MensajeError(strErr)
                    Exit Function
                End If
            Else
                MensajeError(strErr)
                Exit Function
            End If

            'Combo Marcas
            objMarcas.IDEstatus = 2
            dtsRes = objMarcas.ManejaMarca(1)

            If objMarcas.ErrorMarcas = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_MARCA", ddlmarca, strErr, True)
                    If strErr <> "" Then
                        MensajeError(strErr)
                        Exit Function
                    End If
                End If
            Else
                MensajeError(objMarcas.ErrorMarcas)
                Exit Function
            End If

            CargaCombos = True
        Catch ex As Exception
            CargaCombos = False
            MensajeError(ex.Message)
        End Try
    End Function

    Private Sub BuscaDatos()
        Try
            Dim objSubMarca As New SNProcotiza.clsSubMarcas
            Dim dtsRes As New DataSet

            Session("dtsConsultasubmarcas") = Nothing
            grvConsulta.PageIndex = 0
            grvConsulta.DataSource = Nothing

            If cmbEstatus.SelectedValue > 0 Then
                objSubMarca.IDEstatus = cmbEstatus.SelectedValue
            End If

            If ddlmarca.SelectedValue > 0 Then
                objSubMarca.IDMarca = ddlmarca.SelectedValue
            End If

            If txtNom.Text.Trim.Length > 0 Then
                objSubMarca.Descripcion = txtNom.Text.Trim
            End If

            dtsRes = objSubMarca.ManejaSubMarca(1)

            If objSubMarca.ErrorSubMarcas = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    Session("dtsConsultasubmarcas") = dtsRes
                    grvConsulta.DataSource = dtsRes
                End If
            Else
                MensajeError(objSubMarca.ErrorSubMarcas)
                Exit Sub
            End If

            grvConsulta.DataBind()

        Catch ex As Exception
            MensajeError(ex.Message)
            Exit Sub
        End Try
    End Sub

    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsultasubmarcas"), DataSet)
        grvConsulta.DataBind()
    End Sub


    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "submarcaId" Then
            Response.Redirect("./manejaSubMarcas.aspx?idSubMarca=" & e.CommandArgument)
        End If
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As System.EventArgs) Handles btnAgregar.Click
        Response.Redirect("./manejaSubMarcas.aspx?idMarca=" & ddlmarca.SelectedValue) 'BUG-PC-09
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        BuscaDatos()
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As System.EventArgs) Handles btnLimpiar.Click
        LimpiaFiltros()
		BuscaDatos()
    End Sub

    Protected Sub cmbEstatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEstatus.SelectedIndexChanged
        BuscaDatos()
    End Sub

    Protected Sub ddlmarca_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlmarca.SelectedIndexChanged
        BuscaDatos()
    End Sub
End Class
