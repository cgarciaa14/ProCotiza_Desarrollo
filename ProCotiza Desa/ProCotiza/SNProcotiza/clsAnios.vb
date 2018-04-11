'BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.
Imports SDManejaBD

Public Class clsAnios

    Private strErrAnio As String = ""

    Private intidanio As Integer = 0
    Private valoranio As Integer = 0

    Sub New()
    End Sub

    Public ReadOnly Property ErrAnio As String
        Get
            Return strErrAnio
        End Get
    End Property

    Public Function CargaAnio(cveianio As Integer) As DataSet
        CargaAnio = New DataSet
        strErrAnio = ""
        Dim qry As String = String.Empty

        Try
            Dim objSD As New clsConexion

            qry = "SELECT ID_ANIO, ANIO FROM ANIOS WHERE ESTATUS = 2 AND ID_ANIO = " & cveianio
            CargaAnio = objSD.EjecutaQueryConsulta(qry)

            If ConsultaAnio.Tables(0).Rows.Count = 0 Then
                strErrAnio = "No se pudo recuperar información del Anio."
            End If

        Catch ex As Exception
            strErrAnio = ex.Message
        End Try
    End Function

    Public Function ConsultaAnio() As DataSet
        ConsultaAnio = New DataSet
        strErrAnio = ""
        Dim qry As String = String.Empty

        Try
            Dim objSD As New clsConexion

            qry = "SELECT ID_ANIO, ANIO FROM ANIOS WHERE ESTATUS = 2 ORDER BY ANIO"
            ConsultaAnio = objSD.EjecutaQueryConsulta(qry)

            If ConsultaAnio.Tables(0).Rows.Count = 0 Then
                strErrAnio = "No se pudo recuperar información de Anios."
            End If

        Catch ex As Exception
            strErrAnio = ex.Message
        End Try
    End Function
End Class
