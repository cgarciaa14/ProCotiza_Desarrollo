'BBV-P-412:AUBALDO:29/07/2016 RQ 13 – Reporte de Alta de Perfiles y Agencias. (NUEVA BRECHA)
'BBV-P-412:BUG-PC-35 18/01/2017 JRHM Se agrega objeto para solucion de fallas
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports SNProcotiza
Imports SDManejaBD



' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class GeneraReportes
     Inherits System.Web.Services.WebService
    Public Sub ArmaParametros(ByRef strParam As String, _
                              ByVal intTipo As TipoDato, _
                              ByVal strNom As String, _
                              ByVal strValor As String)

        strValor = Replace(strValor, ",", "\c\")
        strValor = Replace(strValor, "|", "\p\")

        If Trim$(strParam) = "" Then
            strParam = strNom & "," & intTipo & "," & strValor
        Else
            strParam += "|" & strNom & "," & intTipo & "," & strValor
        End If
    End Sub
    <WebMethod()> _
    Public Function ReporteAltaAgencia(ByVal FechaInicio As String, ByVal FechaFin As String, ByVal intGeneraReporte As Integer, ByRef strErrAgencia As String) As Byte() 'Boolean
        Dim strParamStored As String = ""
        Dim intOper As Integer = 0
        Dim intAgencia As Integer = 0
        Dim dsReporteAltaAgencia As New DataSet
        Dim blGuardaArchivo As New WSGuardaArchivo()
        Dim guardo As Boolean = 0
        Dim codificador As New ASCIIEncoding

        Try
            Dim objSD As New clsConexion
            Dim strPath As String = System.Configuration.ConfigurationManager.AppSettings.Item("RepAltaPerfilAgencia")

            ArmaParametros(strParamStored, TipoDato.Cadena, "FECHAINICIO", FechaInicio)
            ArmaParametros(strParamStored, TipoDato.Cadena, "FECHAFIN", FechaFin)

            dsReporteAltaAgencia = objSD.EjecutaStoredProcedure("Rep_Alta_Perfil_Agencia", strErrAgencia, strParamStored)

            If strErrAgencia = "" Then
                If Not dsReporteAltaAgencia Is Nothing Then
                    If dsReporteAltaAgencia.Tables.Count > 0 Then
                        If intGeneraReporte = 1 Then
                            guardo = blGuardaArchivo.GuardaArchivo(dsReporteAltaAgencia, ",", strPath, "Alta_Agencia")
                        Else

                        End If
                    End If
                End If
            Else
                If "ERROR" = dsReporteAltaAgencia.Tables(0).Rows(0).Item(0) Then
                    strErrAgencia = "ERROR EN EL REPORTE Rep_Alta_Perfil_Agencia"
                End If
            End If
            codificador.GetBytes(dsReporteAltaAgencia.Tables(0).Rows(0).Item(0).ToString)
        Catch ex As Exception
            strErrAgencia = ex.Message
        End Try
    End Function

End Class