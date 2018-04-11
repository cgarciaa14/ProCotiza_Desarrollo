'BBV-P-412:AVH:26/08/2016 RQCOT-05 REPORTES
'BBVA-P-412 RQ06: AMR: 20/09/2016 Administración de Planes de Financiamiento: Promociones

Imports SDManejaBD

Public Class clsReportes
    Inherits clsSession

    Private intCotizacion As Integer = 0
    Private strErrReporte As String = ""

    Public Property IDCotizacion() As Integer
        Get
            Return intCotizacion
        End Get
        Set(ByVal value As Integer)
            intCotizacion = value
        End Set
    End Property

    Public Function ManejaReporte(ByVal intOper As Integer) As DataSet
        strErrReporte = ""
        ManejaReporte = New DataSet
        Dim strParamStored As String = ""
        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta Reporte
                    If intCotizacion >= 1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idCotizacion", intCotizacion.ToString) 'RQ06

            End Select

            ManejaReporte = objSD.EjecutaStoredProcedure("spManejaReportes", strErrReporte, strParamStored)
            

        Catch ex As Exception
            strErrReporte = ex.Message
        End Try

    End Function



End Class
