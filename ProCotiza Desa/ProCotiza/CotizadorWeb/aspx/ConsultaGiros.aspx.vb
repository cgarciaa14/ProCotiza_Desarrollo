Imports System.Data
'BUG-PC-39 27/01/17 JRHM Se agrego valor seleccionar en combo giros y funcion de boton limpiar
'BUG-PC-40:PVARGAS:27/01/2017:SE MUESTRA LA OPCION SELECCIONAR
Partial Class aspx_ConsultaGiros
    Inherits System.Web.UI.Page
    Dim strErr As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
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
        lblMensaje.Text = ""
    End Sub

    Private Sub LimpiaFiltros()
        cmbEstatus.SelectedValue = 0
        txtNom.Text = ""
    End Sub

    Public Sub MensajeError(ByVal strMsj As String)
        strMsj = Replace(strMsj, "'", "")
        strMsj = Replace(strMsj, """", "")
        strMsj = Replace(strMsj, "(", "")
        strMsj = Replace(strMsj, ")", "")
        lblMensaje.Text = "<script>alert('" & strMsj & "')</script>"
    End Sub

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
                        'BUG-PC-40:PVARGAS:27/01/2017:SE MUESTRA LA OPCION SELECCIONAR
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
        End Try
    End Function

    Private Sub BuscaDatos()
        Try
            Dim objGiro As New SNProcotiza.clsGiros
            Dim dtsRes As New DataSet

            objGiro.IDEstatus = Val(cmbEstatus.SelectedValue)
            objGiro.Nombre = Trim(txtNom.Text)

            dtsRes = objGiro.ManejaGiro(1)

            Session("dtsConsulta") = Nothing
            grvConsulta.PageIndex = 0
            grvConsulta.DataSource = Nothing
            'BUG-PC-40:PVARGAS:27/01/2017:SE DEJA DE MOSTRAR EL MENSAJE EMERGENTE CUANDO NO ENCUETRA REGISTROS Y SE MUESTRA SOLO EN LA TABLA.
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
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As System.EventArgs) Handles btnLimpiar.Click
        LimpiaFiltros()
        'BUG-PC-40:PVARGAS:SE CARGA NUEVAMENTE EL GRIDVIEW
        BuscaDatos()
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As System.EventArgs) Handles btnAgregar.Click
        Response.Redirect("./manejaGiros.aspx?idGiro=0")
    End Sub

    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsulta"), DataSet)
        grvConsulta.DataBind()
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "giroId" Then
            Response.Redirect("./manejaGiros.aspx?idGiro=" & e.CommandArgument)
        End If
    End Sub

    Protected Sub cmbEstatus_SelectedIndexChanged(sender As Object, e As EventArgs)
        BuscaDatos()
    End Sub
End Class
