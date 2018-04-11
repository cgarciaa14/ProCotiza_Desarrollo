'BUG-PC-59:AMATA:08/05/2017:Seguro de vida Bancomer.

Imports System.Text

Public Class clsDeserialBBVA

    Private _strError As String = String.Empty

    Public ReadOnly Property StrError As String
        Get
            Return _strError
        End Get
    End Property


    Public Class BBVA
        Public quote As quote = New quote()
    End Class

    Public Class quote
        Public idquote As String = String.Empty
    End Class

    Sub New()
    End Sub

    Public Function DeserealizeDaños(respuesta As String) As DataTable
        Dim dttdaño As DataTable = New DataTable()

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jresult As BBVA = serializer.Deserialize(Of BBVA)(respuesta)

        Dim msjerror As String = jresult.quote.idquote

        Return dttdaño
    End Function

    Public Function DeserealizeVida(respuesta As String) As DataTable
        Dim dttvida As DataTable = New DataTable

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jresult As BBVA = serializer.Deserialize(Of BBVA)(respuesta)

        Dim msjerror As String = jresult.quote.idquote

        Return dttvida
    End Function
End Class
