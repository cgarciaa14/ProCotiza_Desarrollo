#Region "trackers"
'RQ-PI7-PC2: DJUARES: 11/12/2017: CREEACION DE CLASE DE SERVCIO DE SUBPLANES(COBERTURAS)
'RQ-PI7-PC7: CGARCIA: 27/12/2017: CREACION DE LOS SERVICIOS DE SUBPLANES, INDEMNIZACION Y DEDUCIBLES UNICAMENTE PARA SERVICIOS BANCOMER
#End Region

Public Class clsSubplanes
    Private _strError As String = String.Empty

    Public ReadOnly Property StrError As String
        Get
            Return _strError
        End Get
    End Property

    Public Class JSON
        Public header As header = New header
        Public productCode As String = "2002"
        Public iRequest As iRequest = New iRequest
    End Class
    Public Class header
        Public aapType As String = "45555F6"
        Public dateRequest As String = Date.Now.ToString("yyyy-MM-dd") & " 00:00:00.000"
        Public channel As String = "8" 'String.Empty
        Public subChannel As String = "8" 'String.Empty
        Public branchOffice As String = "CONSUMER FINANCE"
        Public managementUnit As String = "AUNCF001" 'String.Empty
        Public user As String = "CARLOS"
        Public idSession As String = "3232-3232"
        Public idRequest As String = "1212-121212-12121-212"
        Public dateConsumerInvocation As String = Date.Now.ToString("dd-MM-yyyy") & " 00:00:00"
    End Class
    Public Class iRequest
        Public productPlan As New productPlan
    End Class
    Public Class productPlan
        Public planReview As String = "001"
        Public planCode As String = "015"
    End Class
    Public Class JSONRespuesta
        Public iCatalogItem As iCatalogItem = New iCatalogItem
        Public messageInfo As messageInfo = New messageInfo
    End Class
    Public Class iCatalogItem
        Public catalogItemBase As List(Of catalogItemBase)
    End Class
    Public Class catalogItemBase
        Public id As String = String.Empty
        Public name As String = String.Empty
    End Class
    Public Class messageInfo
    End Class

    Public Function obtieneSubplan(ByVal usr As String, ByVal idusr As String) As List(Of catalogItemBase)
        Dim json As New JSON()

        Try
            Dim restGT As RESTful = New RESTful()
            Dim jsonResult As String
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

            Dim jsonBody As String = serializer.Serialize(json)

            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("urlSubplanes")
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