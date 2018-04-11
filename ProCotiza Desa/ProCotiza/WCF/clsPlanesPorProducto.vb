'RQ-PI7-PC7-2: JMENDIETA: 12/01/2018: SE CREA LA CLASE PARA OBTENER LOS PLANER POR PRODUCTO
Public Class clsPlanesPorProducto

#Region "Request"


    Public Class JsonRequest
        Public Property header As Header = New Header()
        Public Property productCode As String = String.Empty
    End Class

    Public Class Header
        Public Property aapType As String = String.Empty
        Public Property dateRequest As String = String.Empty
        Public Property channel As String = String.Empty
        Public Property subChannel As String = String.Empty
        Public Property branchOffice As String = String.Empty
        Public Property managementUnit As String = String.Empty
        Public Property user As String = String.Empty
        Public Property idSession As String = String.Empty
        Public Property idRequest As String = String.Empty
        Public Property dateConsumerInvocation As String = String.Empty
    End Class
#End Region

#Region "Response"
    Public Class CatalogItemBase
        Public Property id As String = String.Empty
    End Class

    Public Class ProductsPlan
        Public Property catalogItemBase As CatalogItemBase = New CatalogItemBase()
        Public Property planReview As String = String.Empty
        Public Property bouquetCode As String = String.Empty
    End Class

    Public Class ICatalogItem
        Public Property productsPlan As List(Of ProductsPlan) = New List(Of ProductsPlan) 'ProductsPlan = New ProductsPlan()
    End Class

    Public Class MessageInfo
    End Class

    Public Class Catalog
    End Class

    Public Class Pagination
    End Class

    Public Class JsonResponse
        Public Property iCatalogItem As ICatalogItem = New ICatalogItem()
        Public Property messageInfo As MessageInfo = New MessageInfo()
        Public Property catalog As Catalog = New Catalog()
        Public Property pagination As Pagination = New Pagination()
    End Class
#End Region

    Public Function PlanesPorProducto(ByVal user As String, ByVal idSession As String) As List(Of ProductsPlan)
        Dim _strError As String = String.Empty
        Dim jsonRequest As JsonRequest
        Dim jsonResponse As String = String.Empty
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim result As List(Of ProductsPlan) = New List(Of ProductsPlan)

        Try
            jsonRequest = New JsonRequest()
            Dim restFul As RESTful = New RESTful()

            jsonRequest.header.aapType = "45555F6"
            jsonRequest.header.dateRequest = DateTime.Now.ToString("dd-MM-yyyy") & " 00:00:00"
            jsonRequest.header.channel = "8"
            jsonRequest.header.subChannel = "8"
            jsonRequest.header.branchOffice = "CONSUMER FINANCE"
            jsonRequest.header.managementUnit = "0001"
            jsonRequest.header.user = user
            jsonRequest.header.idSession = idSession
            jsonRequest.header.idRequest = "1212-121212-12121-212"
            jsonRequest.header.dateConsumerInvocation = DateTime.Now.ToString("dd-MM-yyyy") & " 00:00:00"
            jsonRequest.productCode = "2002"

            Dim jsonBody = serializer.Serialize(jsonRequest)
            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

            restFul.buscarHeader("ResponseWarningDescription")
            restFul.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("urlPlanePorProducto")

            jsonResponse = restFul.ConnectionPost(userID, iv_ticket1, jsonBody)

            Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim alert = srrSerialer.Deserialize(Of JsonResponse)(jsonResponse)

            If Not restFul.IsError Then
                result = alert.iCatalogItem.productsPlan
            End If
        Catch ex As Exception
            Return result
        End Try

        Return result
    End Function


End Class
