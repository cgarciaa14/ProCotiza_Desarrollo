'BBVA-P-412: 08/08/2016: AMR: RQ09 – Carga Masiva de Plan de Financiamiento

Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports CargaExcel
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class wsCargaPaquetes
     Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function wscargapaquete() As Boolean
        Dim dts As New DataSet
        Dim fileNameExtension As String = ""

        wscargapaquete = False

        fileNameExtension = System.Configuration.ConfigurationManager.AppSettings.Item("Repositorio") & _
            System.Configuration.ConfigurationManager.AppSettings.Item("FileName")


        Dim objcarga As New clsCargaPaquetes
        objcarga.CveUsu = Val(Session("cveUsu"))
        objcarga.CveAcceso = Val(Session("cveAcceso"))

        dts = objcarga.Read(fileNameExtension)

    End Function

End Class