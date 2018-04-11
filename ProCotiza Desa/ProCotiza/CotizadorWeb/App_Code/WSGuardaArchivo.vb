'BBV-P-412:AUBALDO:29/07/2016 RQ 13 – Reporte de Alta de Perfiles y Agencias. (NUEVA BRECHA)
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.IO
Imports System.Text
Imports System.Data
Imports SNProcotiza
Imports SDManejaBD

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WSGuardaArchivo
     Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function GuardaArchivo(ByVal dsReporte As DataSet, ByVal strSeparador As String, ByVal PathRuta As String, ByVal Nombre_Archivo As String) As Boolean
        Dim intRow As Integer = 0
        Dim sw As System.IO.StreamWriter
        Nombre_Archivo = Nombre_Archivo & ".txt"
        Try
            If Not dsReporte Is Nothing Then
                If dsReporte.Tables.Count > 0 Then
                    If dsReporte.Tables(0).Rows.Count > 0 Then
                        'PathRuta = Server.MapPath("Docs\" & Nombre_Archivo)
                        
                        If Not System.IO.Directory.Exists(PathRuta) Then
                            System.IO.Directory.CreateDirectory(PathRuta)
                        End If

                        PathRuta = PathRuta & "\" & Nombre_Archivo
                        If File.Exists(PathRuta) Then
                            File.Delete(PathRuta)
                        End If
                        sw = New System.IO.StreamWriter(PathRuta)
                        Dim Contenido As New System.Text.StringBuilder
                        Dim objCol As System.Data.DataColumn

                        For iCol As Integer = 0 To dsReporte.Tables(0).Columns.Count - 1
                            Contenido.Append(dsReporte.Tables(0).Columns(iCol).ColumnName & strSeparador)
                        Next
                        sw.WriteLine(Contenido.ToString)
                        With dsReporte.Tables(0)
                            For intRow = 0 To .Rows.Count - 1
                                Contenido.Remove(0, Contenido.Length)
                                For intCol As Integer = 0 To .Columns.Count - 1
                                    objCol = .Columns(intCol)
                                    If .Columns(intCol).DataType.Name.ToString.Trim = "DateTime" Then
                                        If .Rows(intRow)(intCol).ToString.Length > 0 Then
                                            Contenido.Append(Format(CDate(.Rows(intRow)(intCol).ToString.Trim), "dd/MM/yyyy") & strSeparador)
                                        Else
                                            Contenido.Append(strSeparador)
                                        End If
                                    Else
                                        Contenido.Append(.Rows(intRow)(intCol).ToString.Trim & strSeparador)
                                    End If
                                Next
                                sw.WriteLine(Contenido.ToString)
                            Next
                            sw.Close()

                        End With
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class