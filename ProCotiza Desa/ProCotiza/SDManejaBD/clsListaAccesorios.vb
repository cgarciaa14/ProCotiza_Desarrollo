Imports System.Data

<Serializable()> _
Public Class clsListaAccesorios

    Private intIDAccesorio As Integer = 0
    Private _ListaAccesorios As List(Of objAccesorios)

    Public Property ListaAccesorios As List(Of objAccesorios)
        Get
            Return _ListaAccesorios
        End Get

        Set(ByVal value As List(Of objAccesorios))
            _ListaAccesorios = value
        End Set
    End Property

    Public Property IDAccesorio() As Integer
        Get
            Return intIDAccesorio
        End Get
        Set(value As Integer)
            intIDAccesorio = value
        End Set
    End Property

    Public Sub New(ByVal cveacc As Integer)
        intIDAccesorio = cveacc
    End Sub

    <Serializable()> _
    Public Class objAccesorios
        Private ID_ACCESORIO As Integer
        Private ID_TIPO_PRODUCTO As Integer
        Private ID_MARCA As Integer
        Private DESCRIPCION As String
        Private PRECIO As Double
        Private AFECTA_CALCULO_SEGURO As Integer

        Public ReadOnly Property _ID_ACCESORIO() As Integer
            Get
                Return ID_ACCESORIO
            End Get
        End Property

        Public Property _ID_TIPO_PRODUCTO() As Integer
            Get
                Return ID_TIPO_PRODUCTO
            End Get
            Set(value As Integer)
                ID_TIPO_PRODUCTO = value
            End Set
        End Property

        Public Property _DESCRIPCION() As String
            Get
                Return DESCRIPCION
            End Get
            Set(value As String)
                DESCRIPCION = value
            End Set
        End Property

        Public Property _ID_MARCA() As Integer
            Get
                Return ID_MARCA
            End Get
            Set(value As Integer)
                ID_MARCA = value
            End Set
        End Property

        Public Property _PRECIO() As Double
            Get
                Return PRECIO
            End Get
            Set(value As Double)
                PRECIO = value
            End Set
        End Property

        Public Property _AFECTA_CALCULO_SEGURO() As Integer
            Get
                Return AFECTA_CALCULO_SEGURO
            End Get
            Set(value As Integer)
                AFECTA_CALCULO_SEGURO = value
            End Set
        End Property
    End Class
End Class
