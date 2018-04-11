'BBV-P-412: AVH: RQ 15: 27/07/2016 SE AGREGA VENTANA DE SUB PRODUCTOS UG
Imports System.Data
Partial Class aspx_consultaSubProducto
    Inherits System.Web.UI.Page
    Dim strErr As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            MensajeError(objUsuFirma.ErrorUsuario)
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()
        If Not IsPostBack Then
            CargaCombos(1)
            BuscaDatos()
            LimpiaFiltros()
        End If
    End Sub
    Public Sub LimpiaError()
        lblMensaje.Text = String.Empty
        lblScript.Text = String.Empty
    End Sub

    Private Sub LimpiaFiltros()
        cmbEstatus.SelectedValue = 0
        txtProducto.Text = ""
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
            CargaCombos = False
            MensajeError(ex.Message)
        End Try
    End Function
    Private Sub BuscaDatos()
        Try
            Dim objProducto As New SNProcotiza.clsSubProductosUG
            Dim dtsRes As New DataSet


            objProducto.IDEstatus = Val(cmbEstatus.SelectedValue)
            objProducto.Nombre = Trim(txtProducto.Text)

            dtsRes = objProducto.ManejaSubProductoUG(1)

            Session("dtsConsulta") = Nothing
            grvConsulta.PageIndex = 0
            grvConsulta.DataSource = Nothing

            If dtsRes.Tables.Count > 0 Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    Session("dtsConsulta") = dtsRes
                    grvConsulta.DataSource = dtsRes
                End If
            End If

            grvConsulta.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging

        If CType(Session("dtsConsulta"), DataSet) Is Nothing Then
            BuscaDatos()
        End If

        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsulta"), DataSet)
        grvConsulta.DataBind()
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "ID_SUBPRODUCTO_UG" Then
            Response.Redirect("./manejaSubProductoUG.aspx?idSubProductoUG=" & e.CommandArgument)
        End If


    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As System.EventArgs) Handles btnAgregar.Click
        Response.Redirect("./manejaSubProductoUG.aspx?idSubProductoUG=0")
    End Sub

    Protected Sub cmbEstatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEstatus.SelectedIndexChanged
        BuscaDatos()
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        BuscaDatos()
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As System.EventArgs) Handles btnLimpiar.Click
        LimpiaFiltros()
        BuscaDatos()
    End Sub

End Class
