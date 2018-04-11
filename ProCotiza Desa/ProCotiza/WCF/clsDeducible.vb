#Region "Tracker"
'RQ-PI7-PC7: CGARCIA: 28/12/2017: CREACION DE LOS SERVICIOS DE SUBPLANES, INDEMNIZACION Y DEDUCIBLES UNICAMENTE PARA SERVICIOS BANCOMER
'BUG-PD-171: CGARCIA: 27/03/2018: MODIFICACION DE JSON DE SALIDA 
#End Region

Public Class clsDeducible
    Private _strError As String = String.Empty
    Private _subPlan As String

    Public ReadOnly Property StrError As String
        Get
            Return _strError
        End Get
    End Property

    Public Property SubPlan() As String
        Get
            Return _subPlan
        End Get
        Set(ByVal value As String)
            _subPlan = value
        End Set
    End Property

    Public Class JSON
        Public header As header = New header
        Public productCode As String = "2002"
        Public iRequest As New iRequest        
    End Class
    Public Class header
        Public aapType As String = "45555F6"
        Public dateRequest As String = Date.Now.ToString("yyyy-MM-dd") & " 00:00:00.000"
        Public Channel As String = "8"
        Public SubChannel As String = "8"
        Public branchOffice As String = "CONSUMER FINANCE"
        Public managementUnit As String = "AUNCF001"
        Public user As String = "CARLOS"
        Public idSession As String = "3232-3232"
        Public idRequest As String = "1212-121212-12121-212"
        Public dateConsumerInvocation As String = Date.Now.ToString("dd-MM-yyyy") & " 00:00:00"
    End Class
    Public Class iRequest
        Public productPlan As New productPlan1
    End Class
    Public Class productPlan1
        Public subPlanId As String
    End Class
    Public Class JSONRespuesta
        Public iCatalogItem As iCatalogItem = New iCatalogItem
    End Class
    Public Class iCatalogItem
        Public planParticularData As New List(Of planParticularData)
    End Class
    Public Class planParticularData
        Public productPlan As productPlan = New productPlan
        Public particularData As New List(Of particularData)
    End Class
    Public Class productPlan
        Public catalogItemBase As catalogItemBase = New catalogItemBase
        Public planReview As String = String.Empty
        Public bouquetCode As String = String.Empty
    End Class
    'Public Class catalogItemBase
    '    Public id As String = String.Empty
    'End Class
    Public Class particularData
        Public transformer As New List(Of transformer)
        Public aliasCriterion As String = String.Empty
        Public type As String = String.Empty
        Public description As String = String.Empty
    End Class
    Public Class transformer
        Public catalogItemBase As New List(Of catalogItemBase)
    End Class
    Public Class catalogItemBase
        Public id As String = String.Empty
        Public name As String = String.Empty
        Public description As String = String.Empty
    End Class

    Public Function ObtieneDeducible(ByVal usr As String, ByVal idusr As String) As List(Of planParticularData)
        Dim json As New JSON()
        Try
            json.iRequest.productPlan.subPlanId = _subPlan

            Dim restGT As RESTful = New RESTful()
            Dim jsonResult As String
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

            Dim jsonBody As String = serializer.Serialize(json)

            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("urlDeducibleDaños")
            jsonResult = restGT.ConnectionPost(userID, iv_ticket, jsonBody)

            If (restGT.IsError) Then
                _strError = restGT.MensajeError
                Throw New Exception(StrError)
            Else
                Dim res As JSONRespuesta = serializer.Deserialize(Of JSONRespuesta)(jsonResult)

                Return res.iCatalogItem.planParticularData

            End If
        Catch ex As Exception
            _strError = ex.Message
            Return Nothing
        End Try
    End Function
End Class