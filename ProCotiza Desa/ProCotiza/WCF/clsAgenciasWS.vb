Imports SNProcotiza

#Region "Trackers"
'BUG-PC-74:MPUESTO:06/06/2017:ATENCION DE LOS SIGUIENTES PUNTOS:
'                               + SEGMENTACION DE CLASES PARA INVOCAR WEB SERVICE DE INSERCION Y ACTUALIZACION DE AGENCIAS
'                               + ACTUALIZACION DE RELACION AGENCIA <--> DIVISION POR SERVICIO WEB 
#End Region

Public Class clsAgenciasWS
    Private _objAgency As Agency
    Public Property _Agency() As Agency
        Get
            Return _objAgency
        End Get
        Set(ByVal value As Agency)
            _objAgency = value
        End Set
    End Property

    Public Class Agency
        Public name As String
        Public fiscalId As String
        Public divisionId As String
        Public phoneNumber As String
        Public email As String
        Public agencyId As String

        Public address As address = New address()
        Public iCarDetail As iCarDetail = New iCarDetail()
        Public extendedData As extendedData = New extendedData()
        Public account As account = New account()
    End Class
    Public Class Agency2
        Public agencyId As String
        Public extendedData As extendedData2 = New extendedData2()
    End Class
    Public Class address
        Public city As String
        Public neightborthood As String
        Public streetNumber As String
        Public zipCode As String
        Public state As String
    End Class
    Public Class iCarDetail
        Public car As car = New car()
    End Class
    Public Class extendedData
        Public agreementStartDate As String
        Public agreementEndDate As String
        Public paymentPercentageSellerId As Integer
        Public paymentPercentageAgencyId As Integer
        Public managerId As Integer
        Public adviserId As String
        Public floorAdvisorId As Integer
        Public assistantId As Integer
        Public chiefPromoterId As Integer
        Public floorManagerId As Integer

    End Class
    Public Class account
        Public accountNumber As String
    End Class
    Public Class car
        Public usedCar As String
        Public newCar As String
        Public tax As String
        Public makerId As String
        Public deliveryManagerName As String
    End Class
    Public Class causeId
        Public id As String
    End Class
    Public Class msjerr
        Public message As String
        Public status As String
    End Class
    Public Class extendedData2
        Public statusId As String
        Public causeId As causeId = New causeId()
        Public comment As String
    End Class
    Public Class Customer
        Public Person As Person
    End Class
    Public Class Person
        Public id As String
        Public name As String
        Public lastName As String
        Public mothersLastName As String
    End Class


    Public Function InvokeNewAgencyWS(Agency As Agency, ByRef ErrorMessage As String) As Boolean
        Dim result As Boolean = False
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonBODY2 As String = serializer.Serialize(Agency)

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
        Dim restGT As WCF.RESTful = New WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("Agency")

        restGT.buscarHeader("ResponseWarningDescription")
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY2)

        Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim alert = srrSerialer.Deserialize(Of msjerr)(jsonResult)
        Dim STR2 As String = restGT.MensajeError

        'BUG-PC-14 2016-11-25 MAUT Se agrega la leyenda Error WS a los errores por parte del Web Services
        If restGT.IsError Then
            If restGT.MensajeError <> "" Then
                ErrorMessage = "Error WS - " & restGT.MensajeError
            Else
                ErrorMessage = "Error WS - " & IIf(STR2 = "", alert.message & " Estatus: " & alert.status & ".", STR2)
            End If
            result = False
        Else
            result = True
        End If
        Dim str As String = restGT.valorHeader
        Return result
    End Function

    Public Function InvokeModifyAgencyWS(Agency As Agency, ByRef ErrorMessage As String) As Boolean
        Dim result As Boolean = False
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonBODY As String = serializer.Serialize(Agency)

        Dim restGT As WCF.RESTful = New WCF.RESTful()

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("Agency") + Agency.agencyId.ToString()

        restGT.buscarHeader("ResponseWarningDescription")
        Dim jsonResult As String = restGT.ConnectionPut(userID, iv_ticket1, jsonBODY)



        Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim alert = srrSerialer.Deserialize(Of msjerr)(jsonResult)
        'Dim STR2 As String = restGT.MensajeError
        'strError = STR2

        'BUG-PC-14 2016-11-25 MAUT Se agrega la leyenda Error WS a los errores por parte del Web Services
        If restGT.IsError Then
            'MensajeError("Error WS - " & IIf(STR2 = "", alert.message & " Estatus: " & alert.status & ".", STR2))
            If restGT.MensajeError.Length > 0 Then
                ErrorMessage = "Error WS: " & restGT.MensajeError
            Else
                ErrorMessage = "Error WS: " & alert.message
            End If
            result = False
        Else
            result = True
        End If
        Return result
    End Function

    Public Sub LoadAgencyFromDB(ByVal idAgencia As Integer, Optional idDivision As Integer = 0)
        _Agency = New Agency()
        Dim _clsAgencias As New clsAgencias()
        Dim _messageError As String = String.Empty
        Dim _result As Boolean = False
        _clsAgencias.CargaAgencia()
        _Agency.agencyId = idAgencia
        _Agency.name = _clsAgencias.Nombre
        _Agency.fiscalId = _clsAgencias.RFC
        _Agency.divisionId = idDivision
        _Agency.phoneNumber = _clsAgencias.Telefono
        _Agency.email = _clsAgencias.Email
        _Agency.address.city = _clsAgencias.Ciudad
        _Agency.address.neightborthood = _clsAgencias.Colonia
        _Agency.address.streetNumber = _clsAgencias.NoExt
        _Agency.address.zipCode = _clsAgencias.CodPos
        _Agency.address.state = _clsAgencias.IDEstado
        _Agency.iCarDetail.car.usedCar = "true"
        _Agency.iCarDetail.car.newCar = "true"
        _Agency.iCarDetail.car.tax = "16"
        _Agency.iCarDetail.car.makerId = "6"
        _Agency.extendedData.paymentPercentageSellerId = _clsAgencias.ComisionVendedor
        _Agency.extendedData.paymentPercentageAgencyId = _clsAgencias.ComisionAgencia
        _Agency.account.accountNumber = _clsAgencias.CuentaAgencia
    End Sub
End Class
