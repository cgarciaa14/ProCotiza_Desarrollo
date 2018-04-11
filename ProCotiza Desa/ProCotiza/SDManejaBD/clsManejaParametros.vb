Imports System.Data.Common
Imports System.Data

Public Class clsManejaParametros
    Private strErrParametros As String = ""
    Private blnTieneParam As Boolean = False
    Private arrParametros As New ArregloParametros

    Public ReadOnly Property ErrorParametros() As String
        Get
            Return strErrParametros
        End Get
    End Property

    Public ReadOnly Property ArregloParametros() As ArregloParametros
        Get
            Return arrParametros
        End Get
    End Property

    Public ReadOnly Property ContieneParametros() As Boolean
        Get
            Return blnTieneParam
        End Get
    End Property

    Public Sub LimpiaArregloParametros()
        arrParametros.Clear()
        blnTieneParam = False
    End Sub

    Public Sub ArmaArregloParametros(ByVal intTipo As TipoDato, _
                                     ByVal strNom As String, _
                                     ByVal strValor As String)
        Try

            If Not blnTieneParam Then
                arrParametros.Clear()
            End If

            Dim objParam As New clsParametro

            objParam.Nombre = strNom
            objParam.Valor = strValor

            Select Case intTipo ' tipo
                Case 1 'cadena
                    objParam.TipoDato = DbType.String
                Case 2 'entero
                    objParam.TipoDato = DbType.Int32
                Case 3 'double
                    objParam.TipoDato = DbType.Double
                Case 4 'fecha
                    objParam.TipoDato = DbType.Date
                Case 5 'fecha y hora
                    objParam.TipoDato = DbType.DateTime
                Case 6 'Boolean
                    objParam.TipoDato = DbType.Boolean
            End Select

            arrParametros.Add(objParam)

            blnTieneParam = True
        Catch ex As Exception
            strErrParametros = ex.Message
        End Try
    End Sub

    Public Function AgregaParametrosArreglo(ByRef objCmd As DbCommand) As Boolean

        AgregaParametrosArreglo = False
        Try
            Dim intDat As Integer = 0
            Dim objParam As DbParameter

            For intDat = 0 To arrParametros.Count - 1
                objParam = objCmd.CreateParameter

                objParam.ParameterName = arrParametros.Parametro(intDat).Nombre
                objParam.DbType = arrParametros.Parametro(intDat).TipoDato
                objParam.Value = arrParametros.Parametro(intDat).Valor
                objParam.Direction = arrParametros.Parametro(intDat).Direccion

                objCmd.Parameters.Add(objParam)
            Next

            AgregaParametrosArreglo = True
        Catch ex As Exception
            strErrParametros = ex.Message
        End Try
    End Function
End Class

Public Enum TipoDato
    Cadena = 1
    Entero = 2
    Doble = 3
    Fecha = 4
    FechaHora = 5
    Booleano = 6
End Enum