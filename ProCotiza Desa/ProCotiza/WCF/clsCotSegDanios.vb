'BUG-PC-148: JMENDIETA  29/01/2018: Se crea la clase que cotizara para seguros bancomer para el producto 15
'BUG-PC-154: CGARCIA: 19/02/2018: SE ACTALIZA CLASE
'BUG-PC-168: CGARCIA: 15/03/2018: SE MODIFICA SERVICO DE DAÑOS PARA MULTIMARCA
'BUG-PC-171: CGARCIA: 27/03/2018: SE AGREGA EL CP
Imports System.Net
Imports WCF.clsCotSegBBVA
Imports System.Text
Imports SNProcotiza

Public Class clsCotSegDanios

    Private Const ErrorDB As String = "Error al obtener datos de base de datos"

#Region "Variables"

    Private _strError As String = String.Empty
    Private _user As String
    Private _idSesion As String
    Private _idIndemnizacion As String
    Private _montoCredito As String
    Private _subPlan As String
    Private idDEDUCDAM As String
    Private strDEDUCDAM As String
    Private idDEDUCROT As String
    Private strDEDUCROT As String
    Private idSACRCB As String
    Private strSACRCB As String
    Private idSACRCP As String
    Private strSACRCP As String
    Private idSACERC As String
    Private strSACERC As String
    Private idSACGMO As String
    Private strSACGMO As String
    Private idedad As String
    Private stredad As String
    Private idGenero As String
    Private strGenero As String
    Private _cp As String
#End Region

#Region "Propiedades"

    Public ReadOnly Property urlBBVAD As String
        Get
            Dim urlService = System.Configuration.ConfigurationManager.AppSettings.Item("urlBBVAD")

            Dim oldString = urlService.Substring(urlService.IndexOf("@@"), urlService.IndexOf("*") - urlService.IndexOf("@@"))
            Dim newString = System.Configuration.ConfigurationManager.AppSettings.Item(oldString.Replace("@@", ""))
            urlService = urlService.Replace(oldString & "*", newString)

            oldString = urlService.Substring(urlService.IndexOf("@@"), urlService.IndexOf("*") - urlService.IndexOf("@@"))
            newString = System.Configuration.ConfigurationManager.AppSettings.Item(oldString.Replace("@@", ""))
            urlService = urlService.Replace(oldString & "*", newString)

            Return urlService
        End Get
    End Property

    Public ReadOnly Property StrError As String
        Get
            Return _strError
        End Get
    End Property

    Public Property User() As String
        Get
            Return _user
        End Get
        Set(ByVal value As String)
            _user = value
        End Set
    End Property
    Public Property CP As String
        Get
            Return _cp
        End Get
        Set(value As String)
            _cp = value
        End Set
    End Property

    Public Property IdSesion() As String
        Get
            Return _idSesion
        End Get
        Set(ByVal value As String)
            _idSesion = value
        End Set
    End Property

    Public Property IdIndeImnizacion() As String
        Get
            Return _idIndemnizacion
        End Get
        Set(ByVal value As String)
            _idIndemnizacion = value
        End Set
    End Property

    Public Property SubPlan() As String
        Get
            Return _subPlan
        End Get
        Set(ByVal value As String)
            _subPlan = value
        End Set
    End Property

    Public Property MontoCredito() As String
        Get
            Return _montoCredito
        End Get
        Set(ByVal value As String)
            _montoCredito = value
        End Set
    End Property

    Public Property ID_DEDUCDAM() As String
        Get
            Return idDEDUCDAM
        End Get
        Set(ByVal value As String)
            idDEDUCDAM = value
        End Set
    End Property

    Public Property STR_DEDUCDAM() As String
        Get
            Return strDEDUCDAM
        End Get
        Set(ByVal value As String)
            strDEDUCDAM = value
        End Set
    End Property

    Public Property ID_DEDUCROT() As String
        Get
            Return idDEDUCROT
        End Get
        Set(ByVal value As String)
            idDEDUCROT = value
        End Set
    End Property

    Public Property STR_DEDUCROT() As String
        Get
            Return strDEDUCROT
        End Get
        Set(ByVal value As String)
            strDEDUCROT = value
        End Set
    End Property

    Public Property ID_SACRCB() As String
        Get
            Return idSACRCB
        End Get
        Set(ByVal value As String)
            idSACRCB = value
        End Set
    End Property

    Public Property STR_SACRCB() As String
        Get
            Return strSACRCB
        End Get
        Set(ByVal value As String)
            strSACRCB = value
        End Set
    End Property

    Public Property ID_SACRCP() As String
        Get
            Return idSACRCP
        End Get
        Set(ByVal value As String)
            idSACRCP = value
        End Set
    End Property

    Public Property STR_SACRCP() As String
        Get
            Return strSACRCP
        End Get
        Set(ByVal value As String)
            strSACRCP = value
        End Set
    End Property

    Public Property ID_SACERC() As String
        Get
            Return idSACERC
        End Get
        Set(ByVal value As String)
            idSACERC = value
        End Set
    End Property

    Public Property STR_SACERC() As String
        Get
            Return strSACERC
        End Get
        Set(ByVal value As String)
            strSACERC = value
        End Set
    End Property

    Public Property ID_SACGMO() As String
        Get
            Return idSACGMO
        End Get
        Set(ByVal value As String)
            idSACGMO = value
        End Set
    End Property

    Public Property STR_SACGMO() As String
        Get
            Return strSACGMO
        End Get
        Set(ByVal value As String)
            strSACGMO = value
        End Set
    End Property

    Public Property ID_EDAD() As String
        Get
            Return idedad
        End Get
        Set(ByVal value As String)
            idedad = value
        End Set
    End Property

    Public Property STR_EDAD() As String
        Get
            Return stredad
        End Get
        Set(value As String)
            stredad = value
        End Set
    End Property

    Public Property ID_GENERO() As String
        Get
            Return idGenero
        End Get
        Set(ByVal value As String)
            idGenero = value
        End Set
    End Property

    Public Property STR_GENERO() As String
        Get
            Return strGenero
        End Get
        Set(ByVal value As String)
            strGenero = value
        End Set
    End Property
#End Region

#Region "Request"
    Public Class JsonRequest
        Public header As header = New header()
        Public quote As quote = New quote()
        Public policy As policy = New policy()
        Public credit As credit = New credit()
        Public insuranceType As insuranceType = New insuranceType()
        Public usageCar As usageCar = New usageCar()
        Public serviceType As serviceType = New serviceType()
        Public paymentWay As paymentWay = New paymentWay()
        Public productPlan As productPlan = New productPlan()
        Public circulationArea As circulationArea = New circulationArea()
        Public vehicleFeatures As vehicleFeatures = New vehicleFeatures()
        Public particularData As List(Of particularData) = New List(Of particularData)
        Public coverageKeys As List(Of coverageKeys) = New List(Of coverageKeys)
        Public zipCode As String = String.Empty
        Public internalTelemarketingCellId As String = String.Empty
    End Class

    Public Class header
        Public aapType As String = String.Empty
        Public dateRequest As String = String.Empty
        Public channel As String = String.Empty
        Public subChannel As String = String.Empty
        Public branchOffice As String = String.Empty
        Public managementUnit As String = String.Empty
        Public user As String = String.Empty
        Public idSession As String = String.Empty
        Public idRequest As String = String.Empty
        Public dateConsumerInvocation As String = String.Empty
    End Class

    Public Class quote
        Public idQuote As String = String.Empty
        Public compensationPlan As compensationPlan = New compensationPlan()
    End Class

    Public Class compensationPlan
        Public id As String = String.Empty
    End Class

    Public Class policy
        Public validityPeriod As validityPeriod = New validityPeriod()
        Public preferredBeneficiary As String = String.Empty
        Public rcUSAIndicator As String = String.Empty
        Public invoiceValue As String = String.Empty
        Public effectiveAdditionaldays As String = String.Empty
        Public agency As agency = New agency()
    End Class

    Public Class validityPeriod
        Public startDate As String = String.Empty
        Public endDate As String = String.Empty
    End Class

    Public Class agency
        Public id As String = String.Empty
        Public description As String = String.Empty
    End Class

    Public Class credit
        Public creditPeriod As String = String.Empty
        Public validityPeriod As validityPeriod = New validityPeriod()
        Public agreedAmount As agreedAmount = New agreedAmount()
    End Class

    Public Class agreedAmount
        Public amount As String = String.Empty
    End Class

    Public Class insuranceType
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class

    Public Class catalogItemBase
        Public id As String
        Public name As String
    End Class

    Public Class usageCar
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class

    Public Class serviceType
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class

    Public Class paymentWay
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class

    Public Class productPlan
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
        Public productCode As String = String.Empty
        Public planReview As String = String.Empty
        Public bouquetCode As String = String.Empty
        Public subplan As String = String.Empty
    End Class

    Public Class circulationArea
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class

    Public Class vehicleFeatures
        Public originType As originType = New originType()
        Public carModel As carModel = New carModel()
        Public identifierVehicleFeatures As identifierVehicleFeatures = New identifierVehicleFeatures()
        Public civilLiabilityId As String = String.Empty
        Public salvageId As String = String.Empty
        Public carBlueBookId As String = String.Empty
    End Class

    Public Class originType
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class

    Public Class carModel
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class

    Public Class identifierVehicleFeatures
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class

    Public Class particularData
        Public aliasCriterion As String = String.Empty
        Public transformer As transformer = New transformer()
    End Class

    Public Class transformer
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class

    Public Class coverageKeys

    End Class

#End Region

#Region "Response"

    Public Class JsonResponse
        Public quote As quoteResponse = New quoteResponse()
        Public rate As List(Of rate) = New List(Of rate)()
        Public coverages As List(Of coverages) = New List(Of coverages)()
        Public rightPolicy As New rightPolicy
        Public rcUSARightPolicy As New rcUSARightPolicy
        Public rightPolicyLocalCurrency As New rightPolicyLocalCurrency
        Public rcUSARightPolicyLocalCurrency As New rcUSARightPolicyLocalCurrency
    End Class

    Public Class quoteResponse
        Public idQuote As String = String.Empty
    End Class

    Public Class rate
        Public paymentWay As New paymentWayResponse
        Public subsequentPaymentsNumber As String = String.Empty
        Public totalPaymentWithTax As New totalPaymentWithTax
        Public totalPaymentWithTaxLocalCurrency As New totalPaymentWithTaxLocalCurrency
        Public totalPaymentWithoutTax As New totalPaymentWithoutTax
        Public totalPaymentWithoutTaxLocalCurrency As New totalPaymentWithoutTaxLocalCurrency
        Public firstPayment As New firstPayment
        Public firstPaymentLocalCurrency As New firstPaymentLocalCurrency
    End Class

    Public Class paymentWayResponse
        Public id As String
        Public name As String
    End Class

    Public Class totalPaymentWithTax
        Public currency As New currency
        Public amount As String
    End Class

    Public Class currency
        Public code As String
    End Class

    Public Class totalPaymentWithTaxLocalCurrency
        Public currency As New currency
        Public amount As String
    End Class

    Public Class totalPaymentWithoutTax
        Public currency As New currency
        Public amount As String
    End Class

    Public Class totalPaymentWithoutTaxLocalCurrency
        Public currency As New currency
        Public amount As String
    End Class

    Public Class firstPayment
        Public currency As New currency
        Public amount As String
    End Class

    Public Class firstPaymentLocalCurrency
        Public currency As New currency
        Public amount As String
    End Class

    Public Class coverages
        Public catalogItemBase As New catalogItemBase
        Public insuredValue As String = String.Empty
        Public premium As New premium
        Public premiumLocalCurrency As New premiumLocalCurrency
        Public premiumWithoutTax As New premiumWithoutTax
        Public premiumWithoutTaxLocalCurrency As New premiumWithoutTaxLocalCurrency
    End Class

    Public Class premium
        Public currency As New currency
        Public amount As String
    End Class

    Public Class premiumLocalCurrency
        Public currency As New currency
        Public amount As String
    End Class

    Public Class premiumWithoutTax
        Public currency As New currency
    End Class

    Public Class premiumWithoutTaxLocalCurrency
        Public currency As New currency
    End Class

    Public Class rightPolicy
        Public currency As New currency
        Public amount As String
    End Class

    Public Class rcUSARightPolicy
        Public currency As New currency
    End Class

    Public Class rightPolicyLocalCurrency
        Public currency As New currency
        Public amount As String
    End Class

    Public Class rcUSARightPolicyLocalCurrency
        Public currency As New currency
    End Class

#End Region


    Public Function CotizaDanios(ByVal brokerid As Integer, ByVal model As String, ByVal Id_Externo As String, ByVal version As String, ByVal precio As String, ByVal paqueteid As Integer,
                                 ByVal aseg As String, ByVal coverageId As String, ByVal idedo As Integer, ByVal edo As String, ByVal tipo_seg As String, ByVal cobertura As String,
                                 ByVal mtoacc As String, ByVal tipoprod As Integer, ByVal intTipoCotizacion As Boolean, Optional ByVal automikRequest As Boolean = 0, Optional headers As WebHeaderCollection = Nothing) As DataSet


        Dim dsDatosDanios As New DataSet
        Dim dsCobertura As New DataSet
        Dim dsAseguradora As New DataSet
        Dim dsPlazos As New DataSet
        Dim dsTipoProd As New DataSet
        Dim clsDatosDanios As New SNProcotiza.clsSeguroDanios
        Dim idseg As String = String.Empty
        Dim tiposegbbva As String = String.Empty
        Dim tipopago As String = String.Empty
        Dim idtipopago As String = String.Empty
        Dim cob As String = String.Empty
        Dim descAseguradora As String = String.Empty
        Dim nombreCobertura As String = String.Empty

        Dim dtb As New DataTable
        Dim resulbbva As New DataSet
        dtb.Columns.Add("ID_PAQUETE")
        dtb.Columns.Add("PRIMA NETA")
        dtb.Columns.Add("RECARGO")
        dtb.Columns.Add("DERECHO")
        dtb.Columns.Add("IVA")
        dtb.Columns.Add("PRIMA TOTAL")
        dtb.Columns.Add("ASEGURADORA")
        dtb.Columns.Add("PAQUETE")
        dtb.Columns.Add("USO")
        dtb.Columns.Add("URL_COTIZACION")
        dtb.Columns.Add("PRIMA_NETA_GAP")
        dtb.Columns.Add("IVA_GAP")
        dtb.Columns.Add("SEGURO_GAP")
        dtb.Columns.Add("SEGURO_VIDA")
        dtb.Columns.Add("ID_COTIZACION")
        dtb.Columns.Add("OBSERVACIONES")
        dtb.Columns.Add("PRIMA_TOTAL_SG")

        Dim dtrec = New DataTable()
        dtrec.Columns.Add("idRequest")
        dtrec.Columns.Add("startDate")
        dtrec.Columns.Add("endDate")
        dtrec.Columns.Add("shippingCosts")
        dtrec.Columns.Add("tax")
        dtrec.Columns.Add("realPremium")
        dtrec.Columns.Add("totalPremium")
        dtrec.Columns.Add("lateFee")
        dtrec.Columns.Add("serialNumber")

        If brokerid = 5 Then
            If tipo_seg = 32 Then
                _strError = "Tipo de seguro no valido para cotizar."
                Return Nothing
            Else
                clsDatosDanios.TipoSeg = tipo_seg
                dsDatosDanios = clsDatosDanios.ObtenDatosDanios(5)
                idseg = dsDatosDanios.Tables(0).Rows(0).Item("idseg").ToString()
                tiposegbbva = dsDatosDanios.Tables(0).Rows(0).Item("tiposegbbva").ToString()
                tipopago = dsDatosDanios.Tables(0).Rows(0).Item("tipopago").ToString()
                idtipopago = dsDatosDanios.Tables(0).Rows(0).Item("idtipopago").ToString()
            End If

        Else
            If tipo_seg = 188 Then
                idseg = "E"
                tiposegbbva = "MULTIANUAL FRACCIONADO"
                tipopago = "ANUAL"
                idtipopago = "A"
            ElseIf tipo_seg = 76 Then
                idseg = "L"
                tiposegbbva = "LIBRE"
                tipopago = "PAGO UNICO"
                idtipopago = "U"
            ElseIf tipo_seg = 75 Then 'financiado
                idseg = "A"
                tiposegbbva = "ANUAL"
                tipopago = "PAGO UNICO"
                idtipopago = "U"
            ElseIf tipo_seg = 30 Or tipo_seg = 31 Then
                idseg = "A"
                tiposegbbva = "ANUAL"
                tipopago = "PAGO UNICO"
                idtipopago = "U"
            Else
                _strError = "Tipo de seguro no valido para cotizar."
                Return Nothing
            End If
        End If


        Try

            nombreCobertura = cobertura
            cob = coverageId
            descAseguradora = aseg
            clsDatosDanios.IdPaquete = paqueteid
            dsPlazos = clsDatosDanios.ObtenDatosDanios(4)
            If (Not String.IsNullOrEmpty(clsDatosDanios.ErrorSeguroDanios)) OrElse (IsNothing(dsPlazos)) OrElse dsPlazos.Tables.Count = 0 OrElse dsPlazos.Tables(0).Rows.Count = 0 Then
                _strError = ErrorDB
                Return Nothing
            End If
            clsDatosDanios.TipoProd = tipoprod
            dsDatosDanios = clsDatosDanios.ObtenDatosDanios(1)

            If String.IsNullOrEmpty(clsDatosDanios.ErrorSeguroDanios) AndAlso (Not IsNothing(dsDatosDanios)) AndAlso dsDatosDanios.Tables.Count > 0 Then

                For i As Integer = 0 To dsPlazos.Tables(0).Rows.Count - 1
                    Dim json As New JsonRequest
                    Threading.Thread.Sleep(1000 * Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Item("wsDelay")))
                    Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim jsonBODY As String = String.Empty
                    Dim jsonResult As String = String.Empty
                    Dim restGT As RESTful = New RESTful()
                    restGT.automikRequest = automikRequest

                    json.header.aapType = IIf(dsDatosDanios.Tables(0).Rows(0).Item("header_aapType").ToString = String.Empty, Nothing, dsDatosDanios.Tables(0).Rows(0).Item("header_aapType").ToString)
                    json.header.dateRequest = DateTime.Now.ToString("dd-MM-yyyy") & " 00:00:00"
                    json.header.channel = IIf(dsDatosDanios.Tables(0).Rows(0).Item("header_channel").ToString = String.Empty, Nothing, dsDatosDanios.Tables(0).Rows(0).Item("header_channel").ToString)
                    json.header.subChannel = IIf(dsDatosDanios.Tables(0).Rows(0).Item("header_subChannel").ToString = String.Empty, Nothing, dsDatosDanios.Tables(0).Rows(0).Item("header_subChannel").ToString)
                    json.header.branchOffice = IIf(dsDatosDanios.Tables(0).Rows(0).Item("header_branchOffice").ToString = String.Empty, Nothing, dsDatosDanios.Tables(0).Rows(0).Item("header_branchOffice").ToString)
                    json.header.managementUnit = IIf(dsDatosDanios.Tables(0).Rows(0).Item("header_managementUnit").ToString = String.Empty, Nothing, dsDatosDanios.Tables(0).Rows(0).Item("header_managementUnit").ToString)
                    json.header.user = _user
                    json.header.idSession = _idSesion
                    json.header.idRequest = IIf(dsDatosDanios.Tables(0).Rows(0).Item("header_idRequest").ToString = String.Empty, Nothing, dsDatosDanios.Tables(0).Rows(0).Item("header_idRequest").ToString)
                    json.header.dateConsumerInvocation = DateTime.Now.ToString("dd-MM-yyyy") & " 00:00:00"

                    json.quote.idQuote = IIf(dsDatosDanios.Tables(0).Rows(0).Item("quote_idQuote").ToString = String.Empty, Nothing, dsDatosDanios.Tables(0).Rows(0).Item("quote_idQuote").ToString)
                    json.quote.compensationPlan.id = _idIndemnizacion

                    json.policy.validityPeriod.startDate = DateTime.Now.ToString("yyyy-MM-dd")
                    json.policy.validityPeriod.endDate = DateTime.Now.AddMonths(CInt(IIf(tipo_seg = 30 Or tipo_seg = 31, "12", dsPlazos.Tables(0).Rows(i).Item("VALOR").ToString))).ToString("yyyy-MM-dd")
                    json.policy.preferredBeneficiary = dsDatosDanios.Tables(0).Rows(0).Item("policy_preferredBeneficiary").ToString
                    json.policy.rcUSAIndicator = IIf(dsDatosDanios.Tables(0).Rows(0).Item("policy_rcUSAIndicator").ToString = String.Empty, Nothing, dsDatosDanios.Tables(0).Rows(0).Item("policy_rcUSAIndicator").ToString)
                    json.policy.invoiceValue = precio.ToString().Replace(",", String.Empty)
                    json.policy.effectiveAdditionaldays = IIf(dsDatosDanios.Tables(0).Rows(0).Item("policy_effectiveAdditionaldays").ToString = String.Empty, Nothing, dsDatosDanios.Tables(0).Rows(0).Item("policy_effectiveAdditionaldays").ToString)
                    json.policy.agency.id = IIf(dsDatosDanios.Tables(0).Rows(0).Item("policy_agency_id").ToString = String.Empty, Nothing, dsDatosDanios.Tables(0).Rows(0).Item("policy_agency_id").ToString)
                    json.policy.agency.description = dsDatosDanios.Tables(0).Rows(0).Item("policy_agency_description").ToString

                    json.credit.validityPeriod.startDate = DateTime.Now.ToString("yyyy-MM-dd")
                    json.credit.validityPeriod.endDate = DateTime.Now.AddMonths(CInt(IIf(tipo_seg = 30 Or tipo_seg = 31, "12", dsPlazos.Tables(0).Rows(i).Item("VALOR").ToString))).ToString("yyyy-MM-dd")
                    json.credit.creditPeriod = CInt(dsPlazos.Tables(0).Rows(i).Item("VALOR").ToString)
                    json.credit.agreedAmount.amount = CInt(_montoCredito.ToString().Replace(",", String.Empty))

                    json.insuranceType.catalogItemBase.id = idseg
                    json.insuranceType.catalogItemBase.name = tiposegbbva

                    json.usageCar.catalogItemBase.id = IIf(dsDatosDanios.Tables(0).Rows(0).Item("usageCar_catalogItemBase_id").ToString = String.Empty, Nothing, dsDatosDanios.Tables(0).Rows(0).Item("usageCar_catalogItemBase_id").ToString)
                    json.usageCar.catalogItemBase.name = IIf(dsDatosDanios.Tables(0).Rows(0).Item("usageCar_catalogItemBase_name").ToString = String.Empty, Nothing, dsDatosDanios.Tables(0).Rows(0).Item("usageCar_catalogItemBase_name").ToString)

                    json.serviceType.catalogItemBase.id = IIf(dsDatosDanios.Tables(0).Rows(0).Item("serviceType_catalogItemBase_id").ToString = String.Empty, Nothing, dsDatosDanios.Tables(0).Rows(0).Item("serviceType_catalogItemBase_id").ToString)
                    json.serviceType.catalogItemBase.name = IIf(dsDatosDanios.Tables(0).Rows(0).Item("serviceType_catalogItemBase_name").ToString = String.Empty, Nothing, dsDatosDanios.Tables(0).Rows(0).Item("serviceType_catalogItemBase_name").ToString)

                    json.paymentWay.catalogItemBase.id = idtipopago
                    json.paymentWay.catalogItemBase.name = tipopago
                    If intTipoCotizacion = True Then
                        json.productPlan.catalogItemBase.id = "015"
                    Else
                        json.productPlan.catalogItemBase.id = "003"
                    End If

                    json.productPlan.productCode = IIf(dsDatosDanios.Tables(0).Rows(0).Item("productPlan_productCode").ToString = String.Empty, Nothing, dsDatosDanios.Tables(0).Rows(0).Item("productPlan_productCode").ToString)
                    If intTipoCotizacion = True Then
                        json.productPlan.planReview = IIf(dsDatosDanios.Tables(0).Rows(0).Item("productPlan_planReview").ToString = String.Empty, Nothing, dsDatosDanios.Tables(0).Rows(0).Item("productPlan_planReview").ToString)
                    Else
                        json.productPlan.planReview = IIf(tipoprod = 14, "008", "007")
                    End If

                    json.productPlan.bouquetCode = IIf(dsDatosDanios.Tables(0).Rows(0).Item("productPlan_bouquetCode").ToString = String.Empty, Nothing, dsDatosDanios.Tables(0).Rows(0).Item("productPlan_bouquetCode").ToString)
                    json.productPlan.subplan = _subPlan

                    json.circulationArea.catalogItemBase.id = idedo.ToString("D3")
                    json.circulationArea.catalogItemBase.name = edo

                    json.vehicleFeatures.originType.catalogItemBase.id = IIf(dsDatosDanios.Tables(0).Rows(0).Item("vehicleFeatures_originType_id").ToString = String.Empty, Nothing, dsDatosDanios.Tables(0).Rows(0).Item("vehicleFeatures_originType_id").ToString)
                    json.vehicleFeatures.originType.catalogItemBase.name = IIf(dsDatosDanios.Tables(0).Rows(0).Item("vehicleFeatures_originType_name").ToString = String.Empty, Nothing, dsDatosDanios.Tables(0).Rows(0).Item("vehicleFeatures_originType_name").ToString)
                    json.vehicleFeatures.carModel.catalogItemBase.name = model
                    json.vehicleFeatures.identifierVehicleFeatures.catalogItemBase.id = Id_Externo
                    json.vehicleFeatures.identifierVehicleFeatures.catalogItemBase.name = version
                    json.vehicleFeatures.civilLiabilityId = dsDatosDanios.Tables(0).Rows(0).Item("vehicleFeatures_civilLiabilityId").ToString
                    json.vehicleFeatures.salvageId = dsDatosDanios.Tables(0).Rows(0).Item("vehicleFeatures_salvageId").ToString
                    json.vehicleFeatures.carBlueBookId = dsDatosDanios.Tables(0).Rows(0).Item("vehicleFeatures_carBlueBookId").ToString

                    Dim particualData As New particularData
                    If intTipoCotizacion = True Then

                        particualData.aliasCriterion = "DEDUCDAM"
                        particualData.transformer.catalogItemBase.id = idDEDUCDAM
                        particualData.transformer.catalogItemBase.name = strDEDUCDAM
                        json.particularData.Add(particualData)

                        particualData = New particularData
                        particualData.aliasCriterion = "DEDUCROT"
                        particualData.transformer.catalogItemBase.id = idDEDUCROT
                        particualData.transformer.catalogItemBase.name = strDEDUCROT
                        json.particularData.Add(particualData)

                        particualData = New particularData
                        particualData.aliasCriterion = "SACRCB"
                        particualData.transformer.catalogItemBase.id = idSACRCB
                        particualData.transformer.catalogItemBase.name = strSACRCB
                        json.particularData.Add(particualData)

                        particualData = New particularData
                        particualData.aliasCriterion = "SACRCP"
                        particualData.transformer.catalogItemBase.id = idSACRCP
                        particualData.transformer.catalogItemBase.name = strSACRCP
                        json.particularData.Add(particualData)

                        particualData = New particularData
                        particualData.aliasCriterion = "SACERC"
                        particualData.transformer.catalogItemBase.id = idSACERC
                        particualData.transformer.catalogItemBase.name = strSACERC
                        json.particularData.Add(particualData)

                        particualData = New particularData
                        particualData.aliasCriterion = "SACGMO"
                        particualData.transformer.catalogItemBase.id = idSACGMO
                        particualData.transformer.catalogItemBase.name = strSACGMO
                        json.particularData.Add(particualData)

                        particualData = New particularData
                        particualData.aliasCriterion = "SACEQE"
                        particualData.transformer.catalogItemBase.id = "001"
                        particualData.transformer.catalogItemBase.name = mtoacc.ToString
                        json.particularData.Add(particualData)

                    Else

                        particualData.aliasCriterion = "AFSAE"
                        particualData.transformer.catalogItemBase.id = "001"
                        particualData.transformer.catalogItemBase.name = mtoacc.ToString
                        json.particularData.Add(particualData)

                    End If

                    json.zipCode = _cp 'dsDatosDanios.Tables(0).Rows(0).Item("zipCode").ToString
                    json.internalTelemarketingCellId = IIf(dsDatosDanios.Tables(0).Rows(0).Item("internalTelemarketingCellId").ToString = String.Empty, Nothing, dsDatosDanios.Tables(0).Rows(0).Item("internalTelemarketingCellId").ToString)

                    Dim userID As String
                    If Not automikRequest Then
                        userID = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
                    Else
                        userID = headers("userID")
                        restGT.consumerID = headers("consumerID")
                    End If

                    Dim iv_ticket1 As String
                    If Not automikRequest Then
                        iv_ticket1 = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
                    Else
                        iv_ticket1 = headers("iv_ticket")
                    End If

                    If Not automikRequest Then
                        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("urlBBVAD")
                    Else
                        restGT.Uri = urlBBVAD
                    End If


                    jsonBODY = serializer.Serialize(json)
                    jsonResult = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)

                    Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()
                    If (restGT.IsError) Then
                        If restGT.MensajeError = "" Then
                            Dim alert = srrSerialer.Deserialize(Of msjerr)(jsonResult)
                            _strError = alert.message.Replace(vbLf, ", ")
                        Else
                            Dim alert = srrSerialer.Deserialize(Of msjerr)(restGT.MensajeError)
                            _strError = alert.message.Replace(vbLf, ", ")
                        End If

                        Throw New Exception(_strError)
                    Else
                        Dim row1 As DataRow = dtb.NewRow
                        Dim row2 As DataRow = dtrec.NewRow
                        Dim res As JsonResponse = serializer.Deserialize(Of JsonResponse)(jsonResult)
                        For ds As Integer = 0 To res.rate.Count - 1
                            If res.rate(ds).paymentWay.id.ToString = "A" Then
                                row1("ID_PAQUETE") = dsPlazos.Tables(0).Rows(i).Item("VALOR").ToString
                                row1("PRIMA NETA") = Math.Truncate((CDbl(res.rate(ds).totalPaymentWithTax.amount.ToString()) / 1.16) * 100) / 100
                                row1("RECARGO") = 0
                                row1("DERECHO") = 0 'Math.Truncate((CDbl(res.rightPolicy.amount.ToString)) * 100) / 100 ESTE NO
                                row1("IVA") = (Math.Truncate(CDbl(res.rate(ds).totalPaymentWithTax.amount.ToString()) * 100) / 100) - (Math.Truncate((CDbl(res.rate(ds).totalPaymentWithTax.amount.ToString()) / 1.16) * 100) / 100)
                                row1("PRIMA TOTAL") = Math.Truncate(CDbl(res.rate(ds).totalPaymentWithTax.amount.ToString()) * 100) / 100
                                row1("ASEGURADORA") = descAseguradora
                                row1("PAQUETE") = nombreCobertura
                                row1("USO") = ""
                                row1("URL_COTIZACION") = ""
                                row1("PRIMA_NETA_GAP") = ""
                                row1("IVA_GAP") = ""
                                row1("SEGURO_GAP") = ""
                                row1("SEGURO_VIDA") = "NO APLICA"
                                row1("ID_COTIZACION") = res.quote.idQuote
                                row1("OBSERVACIONES") = ""
                                row1("PRIMA_TOTAL_SG") = 0
                                dtb.Rows.Add(row1)


                                row2("idRequest") = res.quote.idQuote
                                row2("startDate") = DateTime.Now.ToString("dd-MM-yyyy") '"2016-02-01"
                                row2("endDate") = DateTime.Now.AddMonths(CInt(dsPlazos.Tables(0).Rows(i).Item("VALOR"))).ToString("dd-MM-yyyy") '"2017-02-01"
                                row2("shippingCosts") = Math.Truncate((CDbl(res.rightPolicy.amount.ToString)) * 100) / 100
                                row2("tax") = (Math.Truncate(CDbl(res.rate(ds).totalPaymentWithTax.amount.ToString()) * 100) / 100) - (Math.Truncate((CDbl(res.rate(ds).totalPaymentWithTax.amount.ToString()) / 1.16) * 100) / 100)
                                row2("realPremium") = Math.Truncate((CDbl(res.rate(ds).totalPaymentWithTax.amount.ToString()) / 1.16) * 100) / 100
                                row2("totalPremium") = Math.Truncate(CDbl(res.rate(ds).totalPaymentWithTax.amount.ToString()) * 100) / 100
                                row2("lateFee") = 0
                                row2("serialNumber") = "1/1"
                                dtrec.Rows.Add(row2)
                            End If
                        Next
                    End If
                Next
                resulbbva.Tables.Add(dtb)
                resulbbva.Tables.Add(dtrec)

            Else
                _strError = ErrorDB
                Return Nothing
            End If

            resulbbva.Tables(0).TableName = "Result"
            resulbbva.Tables(1).TableName = "Recibos"
            Return resulbbva

        Catch ex As Exception
            _strError = ex.Message
            Return Nothing

        End Try
    End Function

End Class
