'BBVA-P-412: 08/08/2016: AMR: RQ09 – Carga Masiva de Plan de Financiamiento

Imports System.Data
Imports SNProcotiza

Partial Class wsCargaPaqPrueba
    Inherits System.Web.UI.Page

    Protected Sub btncargar_Click(sender As Object, e As System.EventArgs) Handles btncargar.Click

        Dim objws As New wsCargaPaquetes
        objws.wscargapaquete()
    End Sub
End Class
