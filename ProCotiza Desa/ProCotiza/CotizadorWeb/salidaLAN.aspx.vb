' BUG-PC-36 19/01/2017 GVARGAS Salida usuarios internos

Partial Class salida
    Inherits System.Web.UI.Page
    Protected Sub btnSalida_Click(sender As Object, e As System.EventArgs) Handles btnSalida.Click
        Dim COT As String = System.Configuration.ConfigurationManager.AppSettings("RedirectLAN").ToString()
        Response.Redirect(COT)
    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function url() As String
        Dim COT As String = System.Configuration.ConfigurationManager.AppSettings("RedirectLAN").ToString()
        Return COT
    End Function
End Class
