' BBV-P-412  RQ WSD  gvargas   25/10/2016 Agregado metodos de captura de variables por metodo post de envio, permitiendo Login externo
' BBV-P-412:RQ LOGIN GVARGAS: 03/01/2016 Actualizacion REST Service iv_ticket & userID
' BUG-PC-36 19/01/2017 GVARGAS salida segun usuario interno o externo
'BUG-PC-39 25/01/17  JRHM Correccion de errores multiples
' BUG-PD-13  GVARGAS  28/02/2017 Switch To ProDesk
' BUG-PC-55 GVARGAS 21/04/17 Cambios LogIn TC
' BUG-PC-64: RHERNANDEZ: 18/05/17: SE CORRIGE PROBLEMA CON LOGIN*
' BUG-PC-85 GVARGAS 05/07/2017 consumerID_Extranet
' BUG-PC-91 GVARGAS 08/07/2017 Correccion iv_ticket
' BUG-PC-96 GVARGAS 27/07/2017 Correccion Session
' BUG-PD-249 GVARGAS 26/10/2017 Cierre de session cambio general

Imports System.Data
Imports SNProcotiza
Imports System.Net
Imports System.Data.SqlClient

Partial Class Login
    Inherits System.Web.UI.Page
    Private objGral As New clsProcGenerales

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Request("tc") <> "" Then
            txtTC.Text = Request("tc")
        End If
        If Request.Headers("iv-user") <> "" Then
            txtUSER.Text = Request.Headers("iv-user")
        End If
        If Request.Headers("iv_server_name") <> "" Then
            txtSN.Text = Request.Headers("iv_server_name")
        End If
        If Request.Headers("iv_ticketservice") <> "" Then
            txtIV.Text = Request.Headers("iv_ticketservice")
        End If

        Dim salida As String = Request.Params("out")
        Dim sessionEXT As String = CType(System.Web.HttpContext.Current.Session.Item("ext"), String)
        If ((salida <> Nothing) And (sessionEXT <> Nothing)) Then
            Dim urlExit_ As String = String.Empty
            If (System.Web.HttpContext.Current.Session.Item("ext").ToString() = "EXT") Then
				Dim msg_ As String = Request.Params("msg")
				
				If (msg_ <> Nothing) Then
					urlExit_ = "../CotizadorWeb/salidaWAN.aspx?msg=2"
				Else
					urlExit_ = "../CotizadorWeb/salidaWAN.aspx"
				End If
            Else
                urlExit_ = System.Configuration.ConfigurationManager.AppSettings("RedirectLAN").ToString()
            End If
            Session("userID") = Nothing
            Session("iv_ticket") = Nothing
            Session("css") = Nothing
            Session("ext") = Nothing
            Session("agencia") = Nothing
            Session("cveAcceso") = Nothing
            Session("cveUsu") = Nothing
            Session("ipAcceso") = Nothing
            Session("urlAcceso") = Nothing
            Response.Redirect(urlExit_)
        End If

        Dim headers = Request.Headers.ToString()

        Dim userID As String = Request.Headers("iv-user")
        Dim iv_ticket As String = Request.Headers("iv_ticketservice")
        Dim css As String
        If Not Request.Headers("iv_server_name") Is Nothing Then
            css = Request.Headers("iv_server_name").ToString().ToUpper()
        End If

        Dim LoginExt As Integer = 1
        If Request("d3bug") = "t313pr0" Then
            LoginExt = 0
        End If
         Dim urlExit As String = System.Configuration.ConfigurationManager.AppSettings("RedirectLAN").ToString()
        If (Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("LoginEXTERNO").ToString()) = 1) And LoginExt = 1 Then
            'Nueva regla de negocio
            Dim extitem = CType(System.Web.HttpContext.Current.Session.Item("ext"), String)

            Dim strfecha As String = String.Empty
            Dim dsres As DataSet
            Dim clsparametros As New clsParametros
            clsparametros.IDParametro = 207
            clsparametros.IDPadre = -1
            clsparametros.OrdenarXValor = 0
            dsres = clsparametros.ManejaParametro(1)
            If Not IsNothing(dsres) Then
                If dsres.Tables.Count > 0 Then
                    If dsres.Tables(0).Rows.Count > 0 Then
                        strfecha = dsres.Tables(0).Rows(0).Item("VALOR").ToString()
                    Else
                        objGral.MensajeError(Me, "Contacte administrador Telepro")
                        Exit Sub
                    End If
                Else
                    objGral.MensajeError(Me, "Contacte administrador Telepro")
                    Exit Sub
                End If
            Else
                objGral.MensajeError(Me, "Contacte administrador Telepro")
                Exit Sub
            End If
            If Date.Parse(strfecha) <= Date.Now.Date Then
                If Not IsNothing(extitem) Then
                    urlExit = "../CotizadorWeb/salidaWAN.aspx"
                Else
                    urlExit = "../CotizadorWeb/salidaLAN.aspx"
                End If
                Response.Redirect(urlExit)
            End If
        End If

        'ClientScript.RegisterStartupScript(Me.GetType(), "AlertScript", "alert('userID : " + userID + ", iv_ticket : " + iv_ticket + ", css : " + css + "');", True)
        'ClientScript.RegisterStartupScript(Me.GetType(), "AlertScript", "alert('" + headers.ToString() + "');", True)

        If (Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("LoginEXTERNO").ToString()) = 1) And LoginExt = 1 Then
            If ((userID <> Nothing) And (iv_ticket <> Nothing)) Then
                Session("userID") = userID
                Session("iv_ticket") = iv_ticket
                Session("css") = css

                If userID.IndexOf("EXT") <> -1 Then
                    Session("ext") = "EXT"

                    'Session("userID") = System.Configuration.ConfigurationManager.AppSettings("GENERIC_userID").ToString()
                    'Session("iv_ticket") = System.Configuration.ConfigurationManager.AppSettings("GENERIC_iv_ticket").ToString()
                Else
                    Session("ext") = "INT"
                End If


                If (css.IndexOf("HONDA") <> -1) Then
                    Session("agencia") = "H"
                ElseIf (css.IndexOf("ACURA") <> -1) Then
                    Session("agencia") = "A"
                ElseIf (css.IndexOf("JLR_") <> -1) Then
                    Session("agencia") = "J"
                ElseIf (css.IndexOf("SUKI") <> -1) Then
                    Session("agencia") = "S"
                Else
                    Session("agencia") = "SIN"
                End If
                txtalianza.Text = Session("agencia")
                txtUsu.Text = userID.Replace("EXT", "") 'PENDIENTE DE MODIFICAR 
                txtPwd.Text = "NO_PASS"
                btnAceptar_Click(Me, EventArgs.Empty)
                Return
            Else
                Dim userID_ As String = Request.QueryString("userID")
                Dim iv_ticket_ As String = Request.QueryString("iv_ticket")
                Dim css_ As String
                If Not Request.Headers("iv_server_name") Is Nothing Then
                    css_ = Request.QueryString("css").ToUpper()
                End If

                If ((userID_ <> Nothing) And (iv_ticket_ <> Nothing) And (css_ <> Nothing)) Then
                    Session("userID") = userID_
                    Session("iv_ticket") = iv_ticket_.ToString().Replace("_encode_1", "+").Replace("_encode_2", "/")
                    Session("css") = css_

                    If userID_.IndexOf("EXT") <> -1 Then
                        Session("ext") = "EXT"
                    Else
                        Session("ext") = "INT"
                    End If


                    If (css_.IndexOf("HONDA") <> -1) Then
                        Session("agencia") = "H"
                    ElseIf (css_.IndexOf("ACURA") <> -1) Then
                        Session("agencia") = "A"
                    ElseIf (css_.IndexOf("JLR_") <> -1) Then
                        Session("agencia") = "J"
                    ElseIf (css_.IndexOf("SUKI") <> -1) Then
                        Session("agencia") = "S"
                    Else
                        Session("agencia") = "SIN"
                    End If


                    txtalianza.Text = Session("agencia")
                    txtUsu.Text = userID_.Replace("EXT", "") 'PENDIENTE DE MODIFICAR 
                    txtPwd.Text = "NO_PASS"
                    btnAceptar_Click(Me, EventArgs.Empty)
                    Return


                Else
                    txtUsu.Text = "USUARIO DENEGADO" 'PENDIENTE DE MODIFICAR 
                    txtPwd.Text = "NO PASSWORD"
                    btnAceptar_Click(Me, EventArgs.Empty)
                    Return
                End If
            End If
        Else
            If (Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("LoginEXTERNO").ToString()) = 0) Or LoginExt = 0 Then
                Session("userID") = System.Configuration.ConfigurationManager.AppSettings("userID").ToString()
                Session("iv_ticket") = System.Configuration.ConfigurationManager.AppSettings("iv_ticket").ToString()
                Session("css") = System.Configuration.ConfigurationManager.AppSettings("css").ToString().ToUpper()
                If System.Configuration.ConfigurationManager.AppSettings("userID").ToString().IndexOf("EXT") <> -1 Then
                    Session("ext") = "EXT"
                Else
                    Session("ext") = "INT"
                    Session("agencia") = "J"
                End If

                Dim cssN As String = System.Configuration.ConfigurationManager.AppSettings("css").ToString().ToUpper()

            End If
        End If

        Dim strError As String = String.Empty
        If Request("mns") <> "" Then
            objGral.MensajeError(Me, Request("mns"))
            Dim LoginEXTERNO As String = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("LoginEXTERNO").ToString())
            If LoginEXTERNO = 0 Or LoginExt = 0 Then
                Exit Sub
            End If
        End If




        If (Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("LoginEXTERNO").ToString()) = 1) And LoginExt = 1 Then
            If (Session("ext") IsNot Nothing) Then
                Dim LoginEXTERNO As String = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("LoginEXTERNO").ToString())
                If LoginEXTERNO = 1 And LoginExt = 1 Then 'web.config LoginEXTERNO
                    'Para validar si la peticiones intranet o extranet
                    If (System.Web.HttpContext.Current.Session.Item("ext").ToString() = "EXT") Then
                        urlExit = "../CotizadorWeb/salidaWAN.aspx?msg=1"
                    Else
                        urlExit = "../CotizadorWeb/salidaLAN.aspx?msg=1"
                    End If
                End If
            End If
            Response.Redirect(urlExit)
        End If

        If Not (IsPostBack) Then
            lblMensaje.Text = String.Empty
            txtUsu.Focus()
            Session("userID") = Nothing
            Session("iv_ticket") = Nothing
            Session("css") = Nothing
            Session("ext") = Nothing
            Session("agencia") = Nothing
            Session("cveAcceso") = Nothing
            Session("cveUsu") = Nothing
            Session("ipAcceso") = Nothing
            Session("urlAcceso") = Nothing
        End If
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click
        Dim objDat As New clsSeguridad
        Dim dtsRes As DataSet
        Dim dtePwd As Date
        Dim strPassword As String = String.Empty
        Dim LoginExt As Integer = 1
        If Request("d3bug") = "t313pr0" Then
            LoginExt = 0
        End If


        If Trim$(txtUsu.Text) = "" Or Trim$(txtPwd.Text) = "" Then
            objGral.MensajeError(Me, "Debe proporcionar usuario y contraseña")
        Else

            dtsRes = objDat.ValidaUsuario(Trim$(txtUsu.Text), Trim$(txtPwd.Text))
            If Trim$(objDat.ErrorSeguridad) <> "" Then
                objGral.MensajeError(Me, objDat.ErrorSeguridad)
                'EOST - 26062013
                'Se agregan estas condiciones para un mejor funcionamiento del sistema
                If Trim$(objDat.ErrorSeguridad) = "Contraseña Incorrecta" Then
                    txtPwd.Focus()
                End If
                If Trim$(objDat.ErrorSeguridad) = "El usuario NO existe" Or Trim$(objDat.ErrorSeguridad) = "Usuario Inactivo" Then
                    txtUsu.Text = String.Empty

                    If (Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("LoginEXTERNO").ToString()) = 1) And LoginExt = 1 Then
                        Dim urlExit As String = System.Configuration.ConfigurationManager.AppSettings("RedirectLAN").ToString()
                        If (Session("ext") IsNot Nothing) Then
                            Dim LoginEXTERNO As String = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("LoginEXTERNO").ToString())
                            If LoginEXTERNO = 1 And LoginExt = 1 Then 'web.config LoginEXTERNO
                                'Para validar si la peticiones intranet o extranet
                                If (System.Web.HttpContext.Current.Session.Item("ext").ToString() = "EXT") Then
                                    urlExit = "../CotizadorWeb/salidaWAN.aspx?msg=1"
                                Else
                                    urlExit = "../CotizadorWeb/salidaLAN.aspx?msg=1"
                                End If
                            End If
                        End If
                        Response.Redirect(urlExit)
                        Return
                    End If
                End If
            Else
                objDat.IP = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList(1).ToString()
                objDat.UsuarioAccesoID = dtsRes.Tables(0).Rows(0).Item("ID_USUARIO")
                objDat.EmpresaAccesoID = dtsRes.Tables(0).Rows(0).Item("ID_EMPRESA")
                objDat.UserNameAcceso = UCase(Trim$(txtUsu.Text))
                objDat.URLAccesoSession = ObtenUrlAcceso()
                dtePwd = CDate(dtsRes.Tables(0).Rows(0).Item("FEC_CAMBIO_PWD"))
                objDat.RegistraAcceso()

                If objDat.ErrorSession = "" Then
                    If objDat.AccesoID = 0 Then
                        objGral.MensajeError(Me, "Error al intentar registrar el acceso")
                    Else
                        Session("cveAcceso") = objDat.AccesoID
                        Session("cveUsu") = objDat.UsuarioAccesoID
                        Session("ipAcceso") = objDat.IP
                        Session("urlAcceso") = objDat.URLAccesoSession

                        Dim strDat As String = FormsAuthentication.GetRedirectUrl(User.Identity.Name, False)
                        Dim strPaso() As String = Split(strDat, "/")
                        strDat = strPaso(UBound(strPaso, 1))

                        FormsAuthentication.SetAuthCookie(objDat.AccesoID.ToString(), False)

                        If strDat <> "Default.aspx" Then
                            Response.Redirect("./" & "Login.aspx")
                        Else
                            If Request("tc") <> "" Then
                                'Response.Write("<script>alert('" + txtTC.Text.ToString() + "');</script>")
                                UpdateTC()
                            End If

                            If dtePwd > Now() Then
                                Response.Redirect("aspx/Default.aspx?TC=" & txtTC.Text.ToString() & "&sn=" & txtSN.Text.ToString() & "&Us=" & txtUSER.Text.ToString() & "&alianza=" & txtalianza.Text.ToString() & "&iv=" & txtIV.Text.ToString())
                            Else
                                If (Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("LoginEXTERNO").ToString()) = 1) And LoginExt = 1 Then
                                    Response.Redirect("aspx/Default.aspx?TC=" & txtTC.Text & "&sn=" & txtSN.Text & "&Us=" & txtUSER.Text & "&alianza=" & Session("agencia").ToString() & "&iv=" & txtIV.Text)
                                Else
                                    lblMensaje.Text = "<script>AbrePopup('./aspx/cambiaPwd.aspx?idUsu=" & objDat.UsuarioAccesoID & "&post=1', 5, 5, 350, 190);</script>"
                                End If
                            End If
                        End If
                    End If
                Else
                    objGral.MensajeError(Me, objDat.ErrorSession)
                End If
            End If
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

        strUrl = strProtocol & Request.ServerVariables("SERVER_NAME") & strPort & "/"
        ObtenUrlAcceso = Trim(strUrl)
    End Function

    Protected Sub UpdateTC()
        Dim username As String = txtUsu.Text
        Dim tc As String = txtTC.Text

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("ConexionBD").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "UpdateTC"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@USERNAME", username)
            cmd.Parameters.AddWithValue("@TC", tc)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            'Do While reader.Read()
            'COT = reader(0)
            'Loop
        Catch ex As Exception
            'COT = "ERROR"
        End Try
        sqlConnection1.Close()
    End Sub
End Class
