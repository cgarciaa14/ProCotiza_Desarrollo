' BUG-PC-36 19/01/2017 GVARGAS Salida usuarios externos

' BUG-PD-249 GVARGAS 26/10/2017 Cierre de session cambio general

Partial Class salidaWAN
    Inherits System.Web.UI.Page

    <System.Web.Services.WebMethod()> _
    Public Shared Function url() As String
        Dim COT As String = System.Configuration.ConfigurationManager.AppSettings("RedirectWAN").ToString()
        Return COT
    End Function
End Class
