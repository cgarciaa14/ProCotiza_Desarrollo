Public Class clsParametro
    Private intTipo As DbType = DbType.String
    Private intDireccion As ParameterDirection = ParameterDirection.Input

    Private strNombre As String = ""
    Private strValor As String = ""

    Public Property Nombre() As String
        Get
            Return strNombre
        End Get

        Set(ByVal value As String)
            strNombre = value
        End Set
    End Property

    Public Property Valor() As String
        Get
            Return strValor
        End Get

        Set(ByVal value As String)
            strValor = value
        End Set
    End Property

    Public Property TipoDato() As DbType
        Get
            Return intTipo
        End Get

        Set(ByVal value As DbType)
            intTipo = value
        End Set
    End Property

    Public Property Direccion() As ParameterDirection
        Get
            Return intDireccion
        End Get

        Set(ByVal value As ParameterDirection)
            intDireccion = value
        End Set
    End Property
End Class


Public Class ArregloParametros
    Inherits System.Collections.CollectionBase

    Public Sub Add(ByVal Bloque As clsParametro)
        Me.List.Add(Bloque)
    End Sub

    Public Sub Remove(ByVal Index As Integer)
        If Index >= 0 Then
            Me.List.RemoveAt(Index)
        End If
    End Sub

    Public Property Parametro(ByVal Index As Integer) As clsParametro
        Get
            Return Me.List(Index)
        End Get
        Set(ByVal Value As clsParametro)
            Me.List(Index) = Value
        End Set
    End Property
End Class