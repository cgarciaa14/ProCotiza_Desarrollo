Imports System.Net
Imports System.IO

'BBVA-P-412: 25-08-2016: AMR: RQCOT-01 Ajustes a la pantalla de Cotización.
'BBV-P-412:RQ LOGIN GVARGAS: 23/12/2016 Out Info To ProDesk
'BUG-PD-13  GVARGAS  28/02/2017  Switch To ProDesk
'BUG-PC-88 GVARGAS 06/07/2017  Switch To ProDesk final
'BUG-PC-91 GVARGAS 10/07/2017  change To ProDesk final
'BUG-PC-169 GVARGAS 14/03/2018 Correcion Redirect

Partial Class aspx_Default
    Inherits System.Web.UI.Page
    Private objGral As New clsProcGenerales


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Request("mns") <> "" Then
            objGral.MensajeError(Me, Request("mns"))
            Exit Sub
        End If

        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))
        Session("idPerfil") = objUsuFirma.IDPerfil

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx?mns=" & Trim$(objUsuFirma.ErrorUsuario), True)
        End If
    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function cambioApp() As String
        Dim redirect As Redireccionar = New Redireccionar()
        redirect.ok = "NO"
        redirect.urlDestino = ""
        Try
            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            Dim css As String = CType(System.Web.HttpContext.Current.Session.Item("css"), String)

            Dim URL As String = System.Configuration.ConfigurationManager.AppSettings("urlProDesk").ToString()
            Dim request As HttpWebRequest = CType(WebRequest.Create(URL), HttpWebRequest)

            request.Method = "POST"
            request.ContentType = "application/json"
            request.Headers.Add("HTTP_IV_USER", userID)
            request.Headers.Add("HTTP_IV_TICKETSERVICE", iv_ticket1)
            request.Headers.Add("HTTP_SERVER_NAME", css)
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Dim reader As StreamReader = New StreamReader(response.GetResponseStream())

            redirect.ok = "SI"
            redirect.urlDestino = URL
        Catch e As ArgumentException
            redirect.ok = "Error al cambiar de aplicacion, " + e.Message
        Catch e As WebException
            redirect.ok = "Error al cambiar de aplicacion, " + e.Message
        Catch e As Exception
            redirect.ok = "Error al cambiar de aplicacion, " + e.Message
        End Try

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim redirectOut As String = serializer.Serialize(redirect)
        Return redirectOut
    End Function

    Public Class Redireccionar
        Public ok As String
        Public urlDestino As String
    End Class

    <System.Web.Services.WebMethod()> _
    Public Shared Function OutToProDesk(ByVal url As String) As String
        Dim Link As String = System.Configuration.ConfigurationManager.AppSettings("urlProDesk").ToString()
        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
        Dim css As String = CType(System.Web.HttpContext.Current.Session.Item("css"), String)

        url = String.Empty
        Dim vars As String = "?userID=" + userID + "&iv_ticket=" + iv_ticket1.Replace("+", "_encode_1").Replace("/", "_encode_2") + "&css=" + css

        Return url + Link + vars
    End Function
End Class
