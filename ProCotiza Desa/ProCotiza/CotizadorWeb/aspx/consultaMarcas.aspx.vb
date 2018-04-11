'BBV-P-412:AVH:09/08/2016 RQ20.2 Se comenta mensaje de error, se mostrara en el GV si no encuentra informacion
'BUG-PC-09:AMR:16/11/2016 CUTARMA01:No pertime espacios en blanco al ingresar el nombre de la marca

Imports System.Data


Partial Class aspx_consultaMarcas
    Inherits System.Web.UI.Page
    Dim strErr As String = ""


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()
        If Not IsPostBack Then
            If Not CargaCombos(1) Then
                MensajeError(strErr)
                Exit Sub
            End If
            BuscaDatos()
            LimpiaFiltros()
        End If
    End Sub

    Public Sub LimpiaError()
        lblMensaje.Text = ""
    End Sub

    Private Sub LimpiaFiltros()
        cmbEstatus.SelectedValue = "0"
        txtNom.Text = ""
        lblScript.Text = Nothing
    End Sub

    Public Sub MensajeError(ByVal strMsj As String)
        strMsj = Replace(strMsj, "'", "")
        strMsj = Replace(strMsj, """", "")
        strMsj = Replace(strMsj, "(", "")
        strMsj = Replace(strMsj, ")", "")
        lblMensaje.Text = "<script>alert('" & strMsj & "')</script>"
    End Sub

    Public WriteOnly Property Script() As String
        Set(ByVal value As String)
            lblScript.Text = "<script>" & value & " </script>"
        End Set
    End Property

    Private Function CargaCombos(ByVal intOpc As Integer) As Boolean
        Dim objCombo As New clsProcGenerales
        Dim dtsRes As New DataSet
        Try
            CargaCombos = False

            Select Case intOpc
                Case 1
                    'combo de estatus
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
            End Select

            CargaCombos = True
        Catch ex As Exception
            MensajeError(ex.Message)
            CargaCombos = False
        End Try
    End Function

    Private Sub BuscaDatos()
        Try
            Dim objMarca As New SNProcotiza.clsMarcas
            Dim dtsRes As New DataSet

            objMarca.IDEstatus = Val(cmbEstatus.SelectedValue)
            objMarca.Nombre = Trim(txtNom.Text)

            dtsRes = objMarca.ManejaMarca(1)

            Session("dtsConsulta") = Nothing
            grvConsulta.PageIndex = 0
            grvConsulta.DataSource = Nothing

            If dtsRes.Tables.Count > 0 Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    Session("dtsConsulta") = dtsRes
                    grvConsulta.DataSource = dtsRes
                Else
                    'MensajeError("No se encontró información con los parámetros proporcionados.") 
                End If
            Else
                'MensajeError("No se encontró información para los parámetros proporcionados.")
            End If

            grvConsulta.DataBind()
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        BuscaDatos()
        lblScript.Text = Nothing
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As System.EventArgs) Handles btnLimpiar.Click
        LimpiaFiltros()
        If Not CargaCombos(1) Then
            MensajeError(strErr)
            Exit Sub
        End If
        BuscaDatos()
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As System.EventArgs) Handles btnAgregar.Click
        Response.Redirect("./manejaMarcas.aspx?idMarca=0")
    End Sub

    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsulta"), DataSet)
        grvConsulta.DataBind()
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "marcaId" Then
            Response.Redirect("./manejaMarcas.aspx?idMarca=" & e.CommandArgument)
        End If
        If e.CommandName = "MarAge" Then
            Script = "AbrePopup('relacionaMarcasAgencias.aspx?idMarca=" & e.CommandArgument & "&tipoRel=1',300,100,800,550)"
        End If
    End Sub

    Protected Sub cmbEstatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEstatus.SelectedIndexChanged
        lblScript.Text = Nothing
        BuscaDatos()
    End Sub
End Class
