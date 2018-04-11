#Region "Tracker"
'RQ-PI7-PC7: CGARCIA: 28/12/2017: CREACION DE LOS SERVICIOS DE SUBPLANES, INDEMNIZACION Y DEDUCIBLES UNICAMENTE PARA SERVICIOS BANCOMER
#End Region

Public Class clsIndemnizacion
    Private _strError As String = String.Empty

    Public ReadOnly Property StrError As String
        Get
            Return _strError
        End Get
    End Property

    Public Class JSON
        Public header As header = New header
        Public iRequest As iRequest = New iRequest
    End Class
    Public Class header
        Public dateRequest As String = Date.Now.ToString("yyyy-MM-dd") & " 00:00:00:000000"
        Public channel As String = "1"
        Public subChannel As String = "2"
        Public branchOffice As String = "CONFIN"
        Public managementUnit As String = "sucursal"
        Public user As String = String.Empty
        Public idSession As String = String.Empty
        Public idRequest As String = "1212-121212-12121-212"
        Public dateConsumerInvocation As String = Date.Now.ToString("yyyy-MM-dd") & " 00:00:00.000"
    End Class
    Public Class iRequest
        Public compensationPlanRequest As compensationPlanRequest = New compensationPlanRequest
    End Class
    Public Class compensationPlanRequest
        Public agreementCode As String = "AUNCF001"
        Public subPlanId As String = String.Empty
    End Class
    Public Class JSONRespuesta
        Public iCatalogItem As iCatalogItem = New iCatalogItem
    End Class
    Public Class iCatalogItem
        Public catalogItemBase As New List(Of catalogItemBase)
    End Class
    Public Class catalogItemBase
        Public id As String = String.Empty
        Public name As String = String.Empty
    End Class

    Public Function ObtenIndemnizacion(ByVal idSubplan As String, ByVal usr As String, ByVal idusr As String) As List(Of catalogItemBase)
        Dim json As New JSON()
        Try
            json.iRequest.compensationPlanRequest.subPlanId = idSubplan
            json.header.idSession = idusr
            json.header.user = usr

            Dim restGT As RESTful = New RESTful()
            Dim jsonResult As String
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

            Dim jsonBody As String = serializer.Serialize(json)

            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("urlIndemnizacion")
            jsonResult = restGT.ConnectionPost(userID, iv_ticket, jsonBody)

            If (restGT.IsError) Then
                _strError = restGT.MensajeError
                Throw New Exception(StrError)
            Else
                Dim res As JSONRespuesta = serializer.Deserialize(Of JSONRespuesta)(jsonResult)

                Return res.iCatalogItem.catalogItemBase

            End If
        Catch ex As Exception
            _strError = ex.Message
            Return Nothing
        End Try
    End Function
End Class
