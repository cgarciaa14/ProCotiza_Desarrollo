'RQ-PC8: CGARCIA: 09/05/2018: SE CREA CLASE 

Imports System.Data
Imports SDManejaBD

Public Class clsCoberturas

#Region "Variables"
    Private intOpcion As Integer
    Private strErr As String = String.Empty
    Private intAlianza As Integer = 0
    Private intClasificacion As Integer = 0
    Private intCobertura As Integer = 0
    Private intEstatus As Integer
    Private strCobertura As String = String.Empty
    Private intIdExterno As String = String.Empty
#End Region

#Region "Propiedades"
    Property _intOpcion() As Integer
        Get
            Return intOpcion
        End Get
        Set(value As Integer)
            intOpcion = value
        End Set
    End Property

    Property _intAlianza() As Integer
        Get
            Return intAlianza
        End Get
        Set(value As Integer)
            intAlianza = value
        End Set
    End Property


    Property _intClasificacion() As Integer
        Get
            Return intClasificacion
        End Get
        Set(ByVal value As Integer)
            intClasificacion = value
        End Set
    End Property

    Property _intCobertura() As Integer
        Get
            Return intCobertura
        End Get
        Set(value As Integer)
            intCobertura = value
        End Set
    End Property

    Property _intEstatus() As Integer
        Get
            Return intEstatus
        End Get
        Set(value As Integer)
            intEstatus = value
        End Set
    End Property

    Property _strCobertura() As String
        Get
            Return strCobertura
        End Get
        Set(value As String)
            strCobertura = value
        End Set
    End Property

    Property _intIdExterno() As String
        Get
            Return intIdExterno
        End Get
        Set(value As String)
            intIdExterno = value
        End Set
    End Property

#End Region

#Region "Metodos"

    Public Function ManejaCoberturas() As DataSet
        strErr = String.Empty
        ManejaCoberturas = New DataSet

        Dim strParamStored As String = String.Empty

        Try
            Dim objDS As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", _intOpcion)
            Select Case _intOpcion
                Case 1
                    If _intClasificacion <> 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "ID_CLASIFICACION", _intClasificacion)
                    If _intAlianza <> 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "ID_ALIANZA", _intAlianza)
                    If _intCobertura <> 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "ID_COBERTURA", _intCobertura)
                Case 2
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_COBERTURA", _intCobertura)
                Case 3
                    ArmaParametros(strParamStored, TipoDato.Cadena, "NOMBRE", _strCobertura)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ESTATUS", _intEstatus)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "ID_EXTERNO", _intIdExterno)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_CLASIFICACION", _intClasificacion)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_ALIANZA", _intAlianza)
                Case 4
                    ArmaParametros(strParamStored, TipoDato.Cadena, "NOMBRE", _strCobertura)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ESTATUS", _intEstatus)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "ID_EXTERNO", _intIdExterno)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_CLASIFICACION", _intClasificacion)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_ALIANZA", _intAlianza)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_COBERTURA", _intCobertura)
            End Select

            ManejaCoberturas = objDS.EjecutaStoredProcedure("SpManejaCoberturas", strErr, strParamStored)
            ArmaParametros(strParamStored, TipoDato.Entero, "ESTATUS", _intEstatus)

            If strErr = String.Empty Then

            End If
        Catch ex As Exception
            strErr = ex.Message
        End Try
    End Function
#End Region
End Class
