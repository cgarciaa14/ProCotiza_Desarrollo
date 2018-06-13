'BBVA-P-412 RQ06: AMR: 20/09/2016 Administración de Planes de Financiamiento: Promociones
'BBVA-P-412 RQ WSA. WS GetLoan Products (RQ06, RQ09): AMR: 25/10/2016 WS GetLoan Products
'BBVA-P-412 RQWSE: GVARGAS: 27/10/2016 Cambios en la clase
'BUG-PC-22 2016-12-02 MAUT Correcciones 
'BBV-P-412:RQ LOGIN GVARGAS: 23/12/2016 Actualizacion REST Service iv_ticket & userID
'BBV-P412 RQCOT-01.2: AMR 23/02/2017: Seguros.
'BUG-PC-85 GVARGAS 05/07/2017 consumerID_Extranet
'BUG-PC-84 RHERNANDEZ: 11/07/17: SE GUARDA CONSUMER ID EN VARIABLE PARA FUTURAS PRUEBAS A SERVIDORES
'RQ-MN2-6: RHERNANDEZ: 15/09/17: SE REPLICAN CLASES A PRODESKNET PARA EL CONTROL DE SERVICIOS HTTPS DE DESARROLLO
'AUTOMIK-TASK-379: RHERNANDEZ: 06/02/2018 : Configuración para usuario Automik en la obtención del iv_ticket, contraseña y consumerID

'V 1.0.1 Agregados metodos GET, POST, PUT, PATCH Y DELETE
'V 2.0.1 Validacion de errores segun HTTP Status
'V 2.0.2 Agregado StatusHTTP propiedad
'V 3.0.1 Agregados parametros para intentos de conexion para obtener un TSEC, Actualizados los metodos de conexion permitiendo 5 intentos,
'        agregado metodo Reset() que permite reutilizar la clase para mas de 1 conexion a un servicio REST, agregado un metodo buscarHeader() que permite leer valores
'        contenidos en los headers de respuesta, modificados los metodos GetTsec() y Connection() permitiendo leer el bosy de respuesta en caso de error
'V 3.0.2 Mejorado el Catch de errores de Red y logicos para los metodos GetTsec() y Connection(), agregada la opcion de no usar TSEC para el consumo de Servicios REST

Imports System
Imports System.Net
Imports System.IO
Imports System.Threading.Thread
Imports System.Text

Public Class RESTful
    Private _Uri As String = String.Empty
    Private _IsError As Boolean = False
    Private _MensajeError As String = String.Empty
    Private _StatusHTTP As String = String.Empty
    Private _bodyHTML As String = String.Empty
    Private _userID As String = String.Empty
    Private _iv_ticket As String = String.Empty
    Private _password As String = System.Configuration.ConfigurationManager.AppSettings("automikPassword")
    Private _tsec As String = String.Empty
    Private _counterConnection As Integer = 0
    Private _Interval As Integer = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("Interval").ToString())
    Private _Intents As Integer = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("Intents").ToString())
    Private _uriGRANTINGTICKET As String = System.Configuration.ConfigurationManager.AppSettings("uriGRANTINGTICKET").ToString()
    Private _consumerID As String = System.Configuration.ConfigurationManager.AppSettings("consumerID").ToString()
    Private _buscarHeader As Boolean = False
    Private _Header As String = String.Empty
    Private _valorHeader As String = String.Empty
    Private _SinTSEC As Boolean = False
    Private _automikRequest As Boolean

    Public Property Uri() As String
        Get
            Return Me._Uri
        End Get
        Set(ByVal value As String)
            Me._Uri = value
        End Set
    End Property

    Public ReadOnly Property IsError() As Boolean
        Get
            Return Me._IsError
        End Get
    End Property

    Public ReadOnly Property MensajeError() As String
        Get
            Return Me._MensajeError
        End Get
    End Property

    Public ReadOnly Property StatusHTTP() As String
        Get
            Return Me._StatusHTTP
        End Get
    End Property

    Public ReadOnly Property valorHeader() As String
        Get
            Return Me._valorHeader
        End Get
    End Property
    Public Property consumerID() As String
        Get
            Return Me._consumerID
        End Get
        Set(value As String)
            Me._consumerID = value
        End Set
    End Property

    Public Property automikRequest() As Boolean
        Get
            Return _automikRequest
        End Get
        Set(ByVal value As Boolean)
            _automikRequest = value
        End Set
    End Property

    'Public Sub RESTful()
    '    _automikRequest = False
    'End Sub

    Private Function Connection(ByVal verbo As String) As String
        Dim json As String
        Try
            Try
                If (Me._SinTSEC = False) Then
                    Me.GetTsec()
                End If

                If (Me._tsec = String.Empty) Then
                    Return "{ ""message"" : ""Se ha intentado conectar al servicio pero no esta disponible GT"", ""status"" : ""0""  }"
                End If
                ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications
                Dim request As HttpWebRequest = CType(WebRequest.Create(Me._Uri), HttpWebRequest)

                Dim encoding As New ASCIIEncoding()
                Dim bite As Byte() = encoding.GetBytes(Me._bodyHTML)

                request.Method = verbo
                request.ContentType = "application/json;charset=UTF-8"
                If (Me._SinTSEC = False) Then
                    request.Headers.Add("tsec", Me._tsec)
                End If
                If (verbo <> "GET") Then
                    request.ContentLength = bite.Length
                    Dim newStream As Stream = request.GetRequestStream()
                    newStream.Write(bite, 0, bite.Length)
                    newStream.Close()
                End If

                Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Dim reader As StreamReader = New StreamReader(response.GetResponseStream())
                Me._StatusHTTP = response.StatusCode
                json = reader.ReadToEnd()
                If (Me._buscarHeader) Then
                    Me._valorHeader = response.Headers(Me._Header)
                End If
            Catch ex As WebException
                Dim status As String = CType(ex.Response, HttpWebResponse).StatusCode

                json = "{""message"" : """ + ex.Message + """, ""status"" : """ + status + """}"


                Dim reader As StreamReader = New StreamReader(CType(ex.Response, HttpWebResponse).GetResponseStream())
                Dim jsonError As String = reader.ReadToEnd()
                Dim bodyError As bodyError = New bodyError()
                bodyError = bodyError.Deserializar(jsonError)

                If bodyError Is Nothing Then
                    Me._MensajeError = json
                Else
                    Me._MensajeError = bodyError.error_message
                End If

                Me._IsError = True
                Me._StatusHTTP = status
                If (Me._buscarHeader) Then
                    Me._valorHeader = CType(ex.Response, HttpWebResponse).Headers(Me._Header)
                End If
            End Try
        Catch ex As Exception
            json = "{ ""message"" : """ + ex.Message + """, ""status"" : ""0""  }"
            Me._MensajeError = ex.Message
            Me._IsError = True
            Me._StatusHTTP = "0"
        End Try
        Return json
    End Function

    Public Function ConnectionGet(ByVal userID As String, ByVal iv_ticket As String, ByVal json As String) As String
        Me._userID = userID
        Me._iv_ticket = iv_ticket
        Me._bodyHTML = json
        Dim jsonRespond As String = Connection("GET")
        Return jsonRespond
    End Function

    Public Function ConnectionPost(ByVal userID As String, ByVal iv_ticket As String, ByVal json As String) As String
        Me._userID = userID
        Me._iv_ticket = iv_ticket
        Me._bodyHTML = json
        Dim jsonRespond As String = Connection("POST")
        Return jsonRespond
    End Function

    Public Function ConnectionPut(ByVal userID As String, ByVal iv_ticket As String, ByVal json As String) As String
        Me._userID = userID
        Me._iv_ticket = iv_ticket
        Me._bodyHTML = json
        Dim jsonRespond As String = Connection("PUT")
        Return jsonRespond
    End Function

    Public Function ConnectionDelete(ByVal userID As String, ByVal iv_ticket As String, ByVal json As String) As String
        Me._userID = userID
        Me._iv_ticket = iv_ticket
        Me._bodyHTML = json
        Dim jsonRespond As String = Connection("DELETE")
        Return jsonRespond
    End Function

    Public Function ConnectionPatch(ByVal userID As String, ByVal iv_ticket As String, ByVal json As String) As String
        Me._userID = userID
        Me._iv_ticket = iv_ticket
        Me._bodyHTML = json
        Dim jsonRespond As String = Connection("PATCH")
        Return jsonRespond
    End Function

    Public Sub GetTsec()

        Dim header As headerTsec = New headerTsec()

        Dim authenticationDataBody As authenticationDataBody = New authenticationDataBody()
        'If (Me._userID = System.Configuration.ConfigurationManager.AppSettings("GENERIC_userID").ToString()) Then
        If Not Me._automikRequest Then
            header.authentication.authenticationType = "00"
            header.authentication.userID = Me._userID
            authenticationDataBody.idAuthenticationData = "iv_ticketService"
            authenticationDataBody.authenticationData.Add(Me._iv_ticket)
            If Me._userID.IndexOf("EXT") <> -1 Then
                header.authentication.consumerID = System.Configuration.ConfigurationManager.AppSettings("GENERIC_consumerID").ToString()
            Else
                header.authentication.consumerID = Me._consumerID
            End If
            Me._consumerID = header.authentication.consumerID
        Else
            header.authentication.authenticationType = "04"
            authenticationDataBody.idAuthenticationData = "password"
            authenticationDataBody.authenticationData.Add(Me._password)
            header.authentication.userID = System.Configuration.ConfigurationManager.AppSettings("automikUserID").ToString()
            header.authentication.consumerID = System.Configuration.ConfigurationManager.AppSettings("automikConsumerID").ToString()
        End If



        header.authentication.authenticationData.Add(authenticationDataBody)
        header.backendUserRequest.userId = ""
        header.backendUserRequest.dialogId = ""
        header.backendUserRequest.accessCode = ""

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonBODY As String = serializer.Serialize(header)

        Try
            Try
                ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications
                Dim request As HttpWebRequest = CType(WebRequest.Create(Me._uriGRANTINGTICKET), HttpWebRequest)

                Dim encoding As New ASCIIEncoding()
                Dim bite As Byte() = encoding.GetBytes(jsonBODY)

                request.Method = "POST"
                request.ContentType = "application/json"
                request.ContentLength = bite.Length
                Dim newStream As Stream = request.GetRequestStream()
                newStream.Write(bite, 0, bite.Length)
                newStream.Close()

                Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Dim reader As StreamReader = New StreamReader(response.GetResponseStream())
                Me._tsec = response.Headers("tsec")
                Me._counterConnection = 0

            Catch ex As WebException

                Dim status As String
                Dim httpResponse As HttpWebResponse = CType(ex.Response, HttpWebResponse)
                If httpResponse IsNot Nothing Then
                    status = CType(ex.Response, HttpWebResponse).StatusCode
                    Dim reader As StreamReader = New StreamReader(CType(ex.Response, HttpWebResponse).GetResponseStream())
                    Dim jsonError As String = reader.ReadToEnd()
                    Dim bodyError As bodyError = New bodyError()
                    bodyError = bodyError.Deserializar(jsonError)
                    Me._MensajeError = bodyError.error_message

                    Me._IsError = True
                    Me._StatusHTTP = status
                Else
                    Me._IsError = True
                    Me._StatusHTTP = "500"
                End If
                'Dim status As String = CType(ex.Response, HttpWebResponse).StatusCode
            End Try
        Catch ex As Exception
            Me._MensajeError = ex.Message
            Me._IsError = True
            Me._StatusHTTP = "0"
        End Try
        Return
    End Sub
    Public Function AcceptAllCertifications() As Boolean
        Return True
    End Function
    Public Sub Reset()
        Me._Uri = String.Empty
        Me._IsError = False
        Me._MensajeError = String.Empty
        Me._StatusHTTP = String.Empty
        Me._bodyHTML = String.Empty
        Me._userID = String.Empty
        Me._iv_ticket = String.Empty
        Me._tsec = String.Empty
        Me._counterConnection = 0
        Me._Interval = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("Interval").ToString())
        Me._Intents = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("Intents").ToString())
        Me._uriGRANTINGTICKET = System.Configuration.ConfigurationManager.AppSettings("uriGRANTINGTICKET").ToString()
        Me._consumerID = System.Configuration.ConfigurationManager.AppSettings("consumerID").ToString()
        Me._buscarHeader = False
        Me._Header = String.Empty
        Me._valorHeader = String.Empty
        Me._SinTSEC = False
    End Sub

    Public Sub buscarHeader(ByVal Header As String)
        Me._buscarHeader = True
        Me._Header = Header
    End Sub

    Public Sub SinTSEC()
        Me._SinTSEC = True
    End Sub
End Class


Public Class headerTsec
    Public authentication As authenticationBody = New authenticationBody()
    Public backendUserRequest As backendUserRequestBody = New backendUserRequestBody()
End Class

Public Class authenticationBody
    Public userID As String
    Public consumerID As String
    Public authenticationType As String
    Public authenticationData As New List(Of authenticationDataBody)
End Class

Public Class backendUserRequestBody
    Public userId As String
    Public accessCode As String
    Public dialogId As String
End Class

Public Class authenticationDataBody
    Public idAuthenticationData As String
    Public authenticationData As New List(Of String)
End Class

Public Class bodyError
    Public version As String
    Public severity As String
    Public http_status As String
    Public error_code As String
    Public error_message As String
    Public system_error_code As String
    Public system_error_description As String
    Public system_error_cause As String

    Public Function replaceVars(ByVal json As String) As String
        json = json.Replace("http-status", "http_status")
        json = json.Replace("error-code", "error_code")
        json = json.Replace("error-message", "error_message")
        json = json.Replace("system-error-code", "system_error_code")
        json = json.Replace("system-error-description", "system_error_description")
        json = json.Replace("system-error-cause", "system_error_cause")
        Return json
    End Function

    Public Function Deserializar(ByVal json As String) As bodyError
        Dim bodyError As bodyError = New bodyError()
        json = bodyError.replaceVars(json)
        Dim serializerError As New System.Web.Script.Serialization.JavaScriptSerializer()
        Return serializerError.Deserialize(Of bodyError)(json)
    End Function
    
End Class