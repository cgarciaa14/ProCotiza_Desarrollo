'BBVA-P-412 RQ06: AMR: 20/09/2016 Administración de Planes de Financiamiento: Promociones
'BBVA-P-412 RQ WSA. WS GetLoan Products (RQ06, RQ09): AMR: 25/10/2016 WS GetLoan Products

Imports System.Web.Services
Imports System.Web.ApplicationServices

Public Class ConsultaWSRest
    Inherits RESTful

    Private strErrorWS As String = String.Empty
    Private strURL As String = String.Empty
    Private strMetodo As String = String.Empty
    Private strParametros As Array

    Sub New()
    End Sub

    Public ReadOnly Property ErrorWS() As String
        Get
            Return strErrorWS
        End Get
    End Property

    Public Property URL() As String
        Get
            Return strURL
        End Get
        Set(value As String)
            strURL = value
        End Set
    End Property

    Public Property Metodo() As String
        Get
            Return strMetodo
        End Get
        Set(value As String)
            strMetodo = value
        End Set
    End Property

    Public Property Parametros() As Array()
        Get
            Return strParametros
        End Get
        Set(value As Array())
            strParametros = value
        End Set
    End Property

    Public Function ConsultaWS(url As String, metodo As String, par As Array, verbo As String) As String
        Dim result As String

        'Dim rest As RESTful = New RESTful()

        ' ''armar json
        ''rest.uri
        ''Dim str As strign rest.ConnectionPost(json)
        ''If rest.StatusHTTP <> 200 Then
        ''    rest.messageError()
        ''Else
        ''    do something with 
        ''        Str()
        ''End If



        'Dim json As String = String.Empty
        'Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        'Dim message As Message = New Message()
        'Dim messageERROR As MessageErrores = New MessageErrores()
        'Dim cad As String()

        'rest.InitParams()

        'For x = LBound(par) To UBound(par)
        '    cad = Split(par(x), ",")
        '    rest.AddParams(cad(0), cad(1), Convert.ToInt16(cad(2)))
        'Next x

        'rest.EndParams()

        'rest.uri = url & metodo

        'Select Case verbo
        '    Case "POST"
        '        json = rest.ConnectionPost
        '    Case "GET"
        '        json = rest.ConnectionGet
        '    Case "PUT"
        '        json = rest.ConnectionPut
        '    Case "PATCH"
        '        json = rest.ConnectionPatch
        '    Case "DELETE"
        '        json = rest.ConnectionDelete
        'End Select

        'If (rest.IsError = True) Then
        '    messageERROR = serializer.Deserialize(Of MessageErrores)(json)
        '    result = messageERROR.Message & ". Estatus: " & rest.StatusHTTP & "."
        'Else
        '    message = serializer.Deserialize(Of Message)(json)

        '    If rest.StatusHTTP = "200" Then
        '        result = rest.StatusHTTP
        '    Else
        '        result = message.Message & ". Estatus" & rest.StatusHTTP & "."
        '    End If

        'End If

        Return result

    End Function
End Class
