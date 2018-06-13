'BUG-PC-59:AMATA:08/05/2017:Seguro de vida Bancomer.
'RQ-PI7-PC4: RHERNANDEZ: 11/01/17: Se modifica la clase para el llamado del servicio de calculo de seguro de vida variable
'BUG-PC-147: RHERNANDEZ: 17/01/17: Se modifica la clase con la configuracion de seguro variable especificado por el equipo BBVA
'RQ-PD30: JMENDIETA: 05/03/2018: Para el tipo de producto 1(AUTO) la alianza sera VINCF003 y el plan 046, para producto 2(Moto) alianza sera VINCF004 y el plan 047
Public Class clsCotSegVidaBBVA
    Private _strError As String = String.Empty

    Public ReadOnly Property StrError As String
        Get
            Return _strError
        End Get
    End Property

    Sub New()

    End Sub

    Public Class JSON
        Public technicalInformation As technicalInformation = New technicalInformation
        Public id As String = String.Empty
        Public productPlan As productPlan = New productPlan
        Public validityPeriod As validityPeriod = New validityPeriod
        Public rateQuote As New List(Of rateQuote)
        Public particularData As New List(Of particularData)
        Public insuredList As New List(Of insuredList)
        Public region As String = String.Empty
    End Class
    Public Class technicalInformation
        Public dateRequest As String = String.Empty
        Public technicalChannel As String = String.Empty
        Public technicalSubChannel As String = String.Empty
        Public branchOffice As String = String.Empty
        Public managementUnit As String = String.Empty
        Public user As String = String.Empty
        Public technicalIdSession As String = String.Empty
        Public idRequest As String = String.Empty
        Public dateConsumerInvocation As String = String.Empty
    End Class
    Public Class productPlan
        Public productCode As String = String.Empty
        Public planReview As String = String.Empty
        Public planCode As planCode = New planCode
        Public bouquetCode As String = String.Empty
    End Class
    Public Class planCode
        Public id As String = String.Empty
    End Class
    Public Class validityPeriod
        Public startDate As String = String.Empty
        Public endDate As String = String.Empty
        Public type As type = New type
    End Class
    Public Class type
        Public id As String = String.Empty
        Public name As String = String.Empty
    End Class
    Public Class rateQuote
        Public paymentWay As paymentWay = New paymentWay
    End Class
    Public Class paymentWay
        Public id As String = String.Empty
        Public name As String = String.Empty
    End Class
    Public Class particularData
        Public aliasCriterion As String = String.Empty
        Public transformer As transformer = New transformer
        Public peopleNumber As String = String.Empty
    End Class
    Public Class transformer
        Public id As String = String.Empty
        Public name As String = String.Empty
    End Class
    Public Class insuredList
        Public coverages As New List(Of coverages)
    End Class
    Public Class coverages
        Public catalogItemBase As catalogItemBases = New catalogItemBases
        Public peopleNumber As String = String.Empty
    End Class
    Public Class catalogItemBases
        Public id As String = String.Empty
    End Class
    Public Class clscreateQuoteRespuesta
        Public id As String = String.Empty
        Public insuredList As New List(Of insuredListRespuesta)
        Public rateQuote As New List(Of rateQuoteRespuesta)
    End Class
    Public Class insuredListRespuesta
        Public id As String = String.Empty
        Public holderIndicator As String = String.Empty
        Public relationshipCode As String = String.Empty
        Public coverages As New List(Of coveragesRespuesta)
    End Class
    Public Class coveragesRespuesta
        Public catalogItemBase As catalogItemBaseRespuesta = New catalogItemBaseRespuesta
        Public premium As premium = New premium
        Public premiumLocalCurrency As premiumLocalCurrency = New premiumLocalCurrency
        Public premiumWithoutTax As premiumWithoutTax = New premiumWithoutTax
    End Class
    Public Class catalogItemBaseRespuesta
        Public id As String = String.Empty
        Public name As String = String.Empty
    End Class
    Public Class premium
        Public amount As String = String.Empty
        Public currency As currency = New currency
    End Class
    Public Class currency
        Public code As String = String.Empty
    End Class
    Public Class premiumLocalCurrency
        Public amount As String = String.Empty
        Public currency As currency = New currency
    End Class
    Public Class premiumWithoutTax
        Public amount As String = String.Empty
        Public currency As currency = New currency
    End Class
    Public Class rateQuoteRespuesta
        Public paymentWay As paymentWayRespuesta = New paymentWayRespuesta
        Public subsequentPaymentsNumber As String = String.Empty
        Public totalPaymentWithoutTax As totalPaymentWithoutTax = New totalPaymentWithoutTax
        Public totalPaymentWithoutTaxLocalCurrency As totalPaymentWithoutTaxLocalCurrency = New totalPaymentWithoutTaxLocalCurrency
        Public firstPayment As firstPayment = New firstPayment
        Public firstPaymentLocalCurrency As firstPaymentLocalCurrency = New firstPaymentLocalCurrency
        Public fractionalPaymentFee As fractionalPaymentFee = New fractionalPaymentFee
        Public fractionalPaymentFeeLocal As fractionalPaymentFeeLocal = New fractionalPaymentFeeLocal
        Public taxLocalCurrency As taxLocalCurrency = New taxLocalCurrency
        Public rightPolicy As rightPolicy = New rightPolicy
        Public rightPolicyLocalCurrency As rightPolicyLocalCurrency = New rightPolicyLocalCurrency
        Public subsequentPayment As subsequentPayment = New subsequentPayment
        Public subsequentPaymentLocal As subsequentPaymentLocal = New subsequentPaymentLocal
        Public paymentWithoutTax As paymentWithoutTax = New paymentWithoutTax
        Public paymentWithoutTaxLocal As paymentWithoutTaxLocal = New paymentWithoutTaxLocal
        Public relatedPolicyFee As relatedPolicyFee = New relatedPolicyFee
        Public relatedPolicyFeeLocal As relatedPolicyFeeLocal = New relatedPolicyFeeLocal
        Public discount As discount = New discount
    End Class
    Public Class paymentWayRespuesta
        Public id As String = String.Empty
        Public name As String = String.Empty
    End Class
    Public Class totalPaymentWithoutTax
        Public amount As String = String.Empty
        Public currency As currency = New currency
    End Class
    Public Class totalPaymentWithoutTaxLocalCurrency
        Public amount As String = String.Empty
        Public currency As currency = New currency
    End Class
    Public Class firstPayment
        Public amount As String = String.Empty
        Public currency As currency = New currency
    End Class
    Public Class firstPaymentLocalCurrency
        Public amount As String = String.Empty
        Public currency As currency = New currency
    End Class
    Public Class fractionalPaymentFee
        Public amount As String = String.Empty
        Public currency As currency = New currency
    End Class
    Public Class fractionalPaymentFeeLocal
        Public amount As String = String.Empty
        Public currency As currency = New currency
    End Class
    Public Class taxLocalCurrency
        Public amount As String = String.Empty
        Public currency As currency = New currency
    End Class
    Public Class rightPolicy
        Public amount As String = String.Empty
        Public currency As currency = New currency
    End Class
    Public Class rightPolicyLocalCurrency
        Public amount As String = String.Empty
        Public currency As currency = New currency
    End Class
    Public Class subsequentPayment
        Public amount As String = String.Empty
        Public currency As currency = New currency
    End Class
    Public Class subsequentPaymentLocal
        Public amount As String = String.Empty
        Public currency As currency = New currency
    End Class
    Public Class paymentWithoutTax
        Public amount As String = String.Empty
        Public currency As currency = New currency
    End Class
    Public Class paymentWithoutTaxLocal
        Public amount As String = String.Empty
        Public currency As currency = New currency
    End Class
    Public Class relatedPolicyFee
        Public amount As String = String.Empty
        Public currency As currency = New currency
    End Class
    Public Class relatedPolicyFeeLocal
        Public amount As String = String.Empty
        Public currency As currency = New currency
    End Class
    Public Class discount
        Public maximum As String = String.Empty
    End Class

    Public Class msjerr
        Public message As String
        Public status As String
    End Class


    Public Function CotizaBBVAVida(ByVal plazo As Integer, ByVal saldo_aseg As Double, ByRef idquote As String, ByRef tipoProducto As Integer) As String
        Dim quotevalue As String = String.Empty
        Dim json As New JSON()

        Try
            Dim restGT As RESTful = New RESTful()
            Dim jsonResult As String
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim fecha_inicio As String = DateTime.Now.ToString("yyyy-MM-dd")
            json.technicalInformation.dateRequest = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")
            json.technicalInformation.technicalChannel = "8"
            json.technicalInformation.technicalSubChannel = "8"
            json.technicalInformation.branchOffice = "CONSUMER FINANCE"
            json.technicalInformation.managementUnit = IIf(tipoProducto = 2, "VINCF004", "VINCF003") 'RQ-PD30: 2->MOTOCICLETAS:VINCF004 autos-> VINCF003 "VINCF002"  ---"VINCF006", "VINCF005"
            json.technicalInformation.user = "CARLOS"
            json.technicalInformation.technicalIdSession = "3232-3232"
            json.technicalInformation.idRequest = "1212-121212-12121-212"
            json.technicalInformation.dateConsumerInvocation = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")

            json.id = ""
            json.productPlan.productCode = "4044"
            json.productPlan.planReview = "001"
            json.productPlan.planCode.id = IIf(tipoProducto = 2, "047", "046") 'RQ-PD30: 2->MOTOCICLETAS:047 autos-> 046  "041" ---"044", "041"

            json.productPlan.bouquetCode = "VGPU"

            json.validityPeriod.startDate = DateTime.Now.ToString("yyyy-MM-dd")
            json.validityPeriod.endDate = DateTime.Now.AddMonths(plazo).ToString("yyyy-MM-dd")
            json.validityPeriod.type.id = "L"
            json.validityPeriod.type.name = "LIBRE"

            Dim ratequote As New rateQuote
            ratequote.paymentWay.id = "U"
            ratequote.paymentWay.name = "PAGO UNICO"
            json.rateQuote.Add(ratequote)

            Dim particulardata As New particularData
            particulardata.aliasCriterion = "VFPL"
            particulardata.transformer.id = plazo.ToString("D3")
            particulardata.transformer.name = plazo.ToString()
            particulardata.peopleNumber = "1"

            json.particularData.Add(particulardata)

            particulardata = New particularData
            particulardata.aliasCriterion = "VFSA"
            particulardata.transformer.id = "001"
            particulardata.transformer.name = saldo_aseg.ToString()
            particulardata.peopleNumber = "1"

            json.particularData.Add(particulardata)

            Dim insuredlist As New insuredList
            Dim coverages As New coverages
            coverages.catalogItemBase.id = "0024"
            coverages.peopleNumber = "1"
            insuredlist.coverages.Add(coverages)
            json.insuredList.Add(insuredlist)
            json.region = "CF AUTO"





            Dim jsonBODY As String = serializer.Serialize(json)

            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("urlBBVAV")
            restGT.buscarHeader("ResponseWarningDescription")
            jsonResult = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)

            Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim alert = srrSerialer.Deserialize(Of msjerr)(jsonResult)

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then
                    _strError = restGT.MensajeError
                    Return Nothing
                    Exit Function
                Else
                    If Not alert.message = Nothing Then
                        _strError = alert.message
                        Return Nothing
                        Exit Function
                    End If
                End If
            Else
                Dim res As clscreateQuoteRespuesta = serializer.Deserialize(Of clscreateQuoteRespuesta)(jsonResult)
                For ds As Integer = 0 To res.rateQuote.Count - 1
                    If res.rateQuote(ds).paymentWay.id.ToString = "A" Then
                        idquote = res.id.ToString()
                        Return res.rateQuote(ds).totalPaymentWithoutTax.amount.ToString
                    End If
                Next
            End If



            Return quotevalue
        Catch ex As Exception
            Return "Error WS SegVida: " + ex.Message.ToString
        End Try
    End Function

End Class