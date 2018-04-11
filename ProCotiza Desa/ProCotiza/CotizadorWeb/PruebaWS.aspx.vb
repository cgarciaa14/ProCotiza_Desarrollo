'BBV-P-412:AUBALDO:29/07/2016 RQ 13 – Reporte de Alta de Perfiles y Agencias. (NUEVA BRECHA)
Imports System.Data
Imports SNProcotiza

Partial Class PruebaWS
    Inherits System.Web.UI.Page
    Private objGral As New clsProcGenerales

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim strError As String = String.Empty
        If Request("mns") <> "" Then
            ''strError =
            objGral.MensajeError(Me, Request("mns"))
            Exit Sub
        End If

        If Not (IsPostBack) Then
            lblMensaje.Text = String.Empty
            txtFecInicio.Focus()
        End If

    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click
        Dim objDat As New GeneraReportes()
        Dim dtsRes As New DataSet

        Dim strPassword As String = String.Empty
        Dim strerror As String = ""

        If Trim$(txtFecInicio.Text) = "" Or Trim$(txtFecFin.Text) = "" Or Trim$(txtTipoReporte.Text) = "" Then
            objGral.MensajeError(Me, "Debe proporcionar todos los campos")
        Else

            'strPassword = objDat.DesEncriptarCadena("MM8csUsDu")

            objDat.ReporteAltaAgencia(txtFecInicio.Text, txtFecFin.Text, txtTipoReporte.Text, strerror)
            'If Trim$(objDat.ErrorSeguridad) <> "" Then
            '    objGral.MensajeError(Me, objDat.ErrorSeguridad)
            '    'EOST - 26062013
            '    'Se agregan estas condiciones para un mejor funcionamiento del sistema
            '    If Trim$(objDat.ErrorSeguridad) = "Contraseña Incorrecta" Then
            '        txtPwd.Focus()
            '    End If
            '    If Trim$(objDat.ErrorSeguridad) = "El usuario NO existe" Or Trim$(objDat.ErrorSeguridad) = "Usuario Inactivo" Then
            '        txtUsu.Text = String.Empty
            '    End If
            'Else
            '    objDat.IP = Request.ServerVariables("REMOTE_ADDR")
            '    ''objDat.PersonaAccesoID = dtsRes.Tables(0).Rows(0).Item("ID_PERSONA")
            '    objDat.UsuarioAccesoID = dtsRes.Tables(0).Rows(0).Item("ID_USUARIO")
            '    objDat.EmpresaAccesoID = dtsRes.Tables(0).Rows(0).Item("ID_EMPRESA")
            '    objDat.UserNameAcceso = UCase(Trim$(txtUsu.Text))
            '    objDat.URLAccesoSession = ObtenUrlAcceso()
            '    dtePwd = CDate(dtsRes.Tables(0).Rows(0).Item("FEC_CAMBIO_PWD"))
            '    objDat.RegistraAcceso()

            '    If objDat.ErrorSession = "" Then
            '        If objDat.AccesoID = 0 Then
            '            objGral.MensajeError(Me, "Error al intentar registrar el acceso")
            '        Else
            '            Session("cveAcceso") = objDat.AccesoID
            '            Session("cveUsu") = objDat.UsuarioAccesoID
            '            Session("ipAcceso") = objDat.IP
            '            Session("urlAcceso") = objDat.URLAccesoSession

            '            Dim strDat As String = FormsAuthentication.GetRedirectUrl(User.Identity.Name, False)
            '            Dim strPaso() As String = Split(strDat, "/")
            '            strDat = strPaso(UBound(strPaso, 1))

            '            FormsAuthentication.SetAuthCookie(objDat.AccesoID.ToString(), False)

            '            If strDat <> "Default.aspx" Then
            '                Response.Redirect("./" & "Login.aspx")
            '            Else
            '                If dtePwd > Now() Then
            '                    Response.Redirect("aspx/Default.aspx")
            '                Else
            '                    lblMensaje.Text = "<script>AbrePopup('./aspx/cambiaPwd.aspx?idUsu=" & objDat.UsuarioAccesoID & "&post=1', 5, 5, 350, 190);</script>"
            '                End If
            '            End If
            '        End If
            '    Else
            '        objGral.MensajeError(Me, objDat.ErrorSession)
            '    End If
            'End If
        End If
    End Sub

    Private Function ObtenUrlAcceso() As String
        ObtenUrlAcceso = ""

        Dim strPort As String = ""
        Dim strProtocol As String = ""
        Dim strUrl As String = ""

        strPort = Request.ServerVariables("SERVER_PORT")
        If Trim(strPort) = "80" Or Trim(strPort) = "443" Then
            strPort = ""
        Else
            strPort = ":" & strPort
        End If

        strProtocol = Request.ServerVariables("SERVER_PORT_SECURE")
        If Trim(strProtocol) = "0" Then
            strProtocol = "http://"
        Else
            strProtocol = "https://"
        End If

        'strUrl = strProtocol & Request.ServerVariables("SERVER_NAME") & strPort & Request.ApplicationPath
        strUrl = strProtocol & Request.ServerVariables("SERVER_NAME") & strPort & "/"
        ObtenUrlAcceso = Trim(strUrl)
    End Function

End Class
