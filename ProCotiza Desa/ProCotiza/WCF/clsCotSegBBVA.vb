Imports System.Text
Imports System.Net
Imports SNProcotiza

'BUG-PC-59:AMATA:08/05/2017:Seguro de vida Bancomer.
'BUG-PC-70: RHERNANDEZ: 30/05/17 SE MODIFICAN PAYLOADS PARA LA COTIZACION DE SEGURO DE DAÑOS BANCOMER
'BUG-PC-72: RHERNANDEZ: 08/06/17: SE CORRIGE ENVIO DE MENSAJES DE ERROR QUE PROVENGAN DE LOS WS y se cambia payload para cuadrar montos
'BUG-PC-81: RHERNANDEZ: 29/06/17: SE ENVIA CAMBIA EVALUACION DE TIPO DE SEGURO DE DE DAÑOS Y SE ENVIAN ACCESORIOS
'BUG-PC-84: RHERNANDEZ: 11/07/17: SE CORRIGE PROBLEMA CON CANTIDADES DE RECIBOS Y COTIZACION
'BUG-PC-98: RHERNANDEZ: 31/07/17: SE CAMBIAN PARAMETROS PARA COTIZAR SEGUROS DEL AUTO BBVA PARA TIPO DE PRODUCTO MOTO
'RQ-PI7-PC7: CGARCIA: 09/01/2017: SE MODIFICA CLASE PARA MANDAR EN SERVICIO DE COTIZACION EL ID DE COBERTURAS QUE ARROJA EL WS DE SUBPLANES.
'BUG-PC-149: CGARCIA: 22/01/2018: SE BANDEREA SEGURO DAÑOS
Public Class clsCotSegBBVA
    Private _strError As String = String.Empty

    Public ReadOnly Property StrError As String
        Get
            Return _strError
        End Get
    End Property

    'urlBBVAD

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

    Sub New()

    End Sub

    Public Class JSON
        Public header As header = New header()
        Public quote As quote = New quote()
        Public policy As policy = New policy()
        Public credit As credit = New credit()
        Public insuranceType As insuranceType = New insuranceType()
        Public paymentWay As paymentWay = New paymentWay()
        Public usageCar As usageCar = New usageCar()
        Public serviceType As serviceType = New serviceType()
        Public productPlan As productPlan = New productPlan()
        Public circulationArea As circulationArea = New circulationArea()
        Public vehicleFeatures As vehicleFeatures = New vehicleFeatures()
        Public particularData As List(Of particularData) = New List(Of particularData)
        Public coverageKeys As List(Of coverageKeys) = New List(Of coverageKeys)
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
        Public idQuote As String
    End Class

    Public Class policy
        Public validityPeriod As validityPeriod = New validityPeriod()
        Public preferredBeneficiary As String = String.Empty
        Public rcUSAIndicator As String = String.Empty
        Public effectiveAdditionaldays As String = String.Empty
        Public invoiceValue As String = String.Empty
    End Class

    Public Class validityPeriod
        Public startDate As String = String.Empty
        Public endDate As String = String.Empty
    End Class

    Public Class credit
        Public creditPeriod As String = String.Empty
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

    Public Class paymentWay
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class

    Public Class productPlan
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
        Public productCode As String = String.Empty
        Public planReview As String = String.Empty
        Public bouquetCode As String = String.Empty
        Public subPlan As String = String.Empty
    End Class

    Public Class circulationArea
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class

    Public Class vehicleFeatures
        Public carModel As carModel = New carModel()
        Public identifierVehicleFeatures As identifierVehicleFeatures = New identifierVehicleFeatures()
        Public originType As originType = New originType()
    End Class

    Public Class carModel
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class

    Public Class identifierVehicleFeatures
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class

    Public Class originType
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class

    Public Class particularData
        Public aliasCriterion As String = String.Empty
        Public transformer As transformer = New transformer()
    End Class

    Public Class transformer
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class

    Public Class serviceType
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class

    Public Class coverageKeys
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class

    Public Class msjerr
        Public message As String
        Public status As String
    End Class
    Public Class jsonresponse
        Public quote As New quote
        Public rate As List(Of rate)
        Public coverages As List(Of coverages)
        Public rightPolicy As New rightPolicy
        Public rcUSARightPolicy As New rcUSARightPolicy
        Public rightPolicyLocalCurrency As New rightPolicyLocalCurrency
        Public rcUSARightPolicyLocalCurrency As New rcUSARightPolicyLocalCurrency
    End Class
    Public Class rate
        Public paymentWay As New RespaymentWay
        Public subsequentPaymentsNumber As String
        Public subplan As String
        Public totalPaymentWithTax As New totalPaymentWithTax
        Public totalPaymentWithTaxLocalCurrency As New totalPaymentWithTaxLocalCurrency
        Public totalPaymentWithoutTax As New totalPaymentWithoutTax
        Public totalPaymentWithoutTaxLocalCurrency As New totalPaymentWithoutTaxLocalCurrency
        Public firstPayment As New firstPayment
        Public firstPaymentLocalCurrency As New firstPaymentLocalCurrency
    End Class
    Public Class RespaymentWay
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
        Public insuredValue As String
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



    Public Function CotizaBBVADaños(ByVal model As String, ByVal Id_Externo As String, ByVal version As String, ByVal precio As String, ByVal plazo As Integer) As String

        Dim json As New JSON()

        Try
            Dim restGT As RESTful = New RESTful()
            Dim jsonResult As String
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

            json.header.aapType = "45555F6"
            json.header.dateRequest = DateTime.Now.ToString("dd-MM-yyyy") & " 00:00:00" ' "06-08-2016 00:00:00"
            json.header.channel = "8"
            json.header.subChannel = "8"
            json.header.branchOffice = "CONSUMER FINANCE"
            json.header.managementUnit = "0001"
            json.header.user = "CARLOS"
            json.header.idSession = "3232-3232"
            json.header.idRequest = "1212-121212-12121-212"
            json.header.dateConsumerInvocation = DateTime.Now.ToString("dd-MM-yyyy") & " 00:00:00" ' "06-08-016 00:00:00"

            json.quote.idQuote = ""

            json.policy.validityPeriod.startDate = DateTime.Now.ToString("yyyy-MM-dd") '"2016-02-01"
            json.policy.validityPeriod.endDate = DateTime.Now.AddMonths(plazo).ToString("yyyy-MM-dd") '"2017-02-01"

            json.policy.preferredBeneficiary = ""
            json.policy.rcUSAIndicator = "N"
            json.policy.effectiveAdditionaldays = ""
            json.policy.invoiceValue = precio.ToString().Replace(",", "")

            json.credit.creditPeriod = IIf(plazo = 12, "10", plazo)

            json.insuranceType.catalogItemBase.id = "A"
            json.insuranceType.catalogItemBase.name = "ANUAL"

            json.usageCar.catalogItemBase.id = "6"
            json.usageCar.catalogItemBase.name = "PARTICULAR"

            json.paymentWay.catalogItemBase.id = "A"
            json.paymentWay.catalogItemBase.name = "ANUAL"

            json.productPlan.catalogItemBase.id = "005"
            json.productPlan.productCode = "2002"
            json.productPlan.planReview = "008"
            json.productPlan.bouquetCode = "AUAR"
            json.productPlan.subPlan = ""

            json.circulationArea.catalogItemBase.id = "001"
            json.circulationArea.catalogItemBase.name = "AGUASCALIENTES"

            json.vehicleFeatures.carModel.catalogItemBase.name = model
            json.vehicleFeatures.identifierVehicleFeatures.catalogItemBase.id = Id_Externo
            json.vehicleFeatures.identifierVehicleFeatures.catalogItemBase.name = version
            json.vehicleFeatures.originType.catalogItemBase.id = "N"
            json.vehicleFeatures.originType.catalogItemBase.name = "NACIONAL"

            Dim data3 As particularData = New particularData()
            data3.aliasCriterion = "AFSAE"
            data3.transformer.catalogItemBase.id = "001"
            data3.transformer.catalogItemBase.name = "0"
            json.particularData.Add(data3)


            json.serviceType.catalogItemBase.id = "PAR"
            json.serviceType.catalogItemBase.name = "PARTICULAR"

            ''Termina armado de json
            ''***********************************************


            Dim jsonBODY As String = serializer.Serialize(json)

            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("urlBBVAD")
            jsonResult = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)




            If (restGT.IsError) Then
                _strError = restGT.MensajeError
                Throw New Exception(StrError)
            Else
                Dim res As jsonresponse = serializer.Deserialize(Of jsonresponse)(jsonResult)
                Return res.quote.idQuote.ToString
            End If


        Catch ex As Exception
            _strError = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function CotizaBBVADaños2(ByVal brokerid As Integer, ByVal model As String, ByVal Id_Externo As String, ByVal version As String, ByVal precio As String, ByVal paqueteid As Integer, ByVal aseg As Integer, ByVal coverageId As String, ByVal idedo As Integer, ByVal edo As String, ByVal tipo_seg As String, ByVal cobertura As Integer, ByVal mtoacc As String, ByVal tipoprod As Integer, Optional ByVal automikRequest As Boolean = 0, Optional headers As WebHeaderCollection = Nothing) As DataSet

        Dim dts As New DataSet()
        Dim dtb As New DataTable
        Dim dtDeduc As New DataSet
        Dim clsParam As New clsParametros
        Dim muestraDeduc As Integer
        dtDeduc = clsParam.ManejaParametro(7)
        If dtDeduc.Tables.Count > 0 AndAlso dtDeduc.Tables(0).Rows.Count > 0 AndAlso dtDeduc.Tables(0).Rows(0).Item("VALOR") <> String.Empty Then
            muestraDeduc = CInt(dtDeduc.Tables(0).Rows(0).Item("VALOR").ToString)
        Else
            muestraDeduc = 0            
        End If
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
        Dim idseg As String = ""
        Dim tiposegbbva As String = ""
        Dim tipopago As String = ""
        Dim idtipopago As String = ""
        Dim cob As String = ""
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
        'RQ-PI7-PC7: CGARCIA: 09/01/2017: SE MODIFICA CLASE PARA MANDAR EN SERVICIO DE COTIZACION EL ID DE COBERTURAS QUE ARROJA EL WS DE SUBPLANES.
        If muestraDeduc = 1 Then
            If brokerid = 5 Or brokerid = 9 Then
                cob = coverageId
            Else
                cob = Obten_coverageIdEXT(brokerid, CInt(coverageId))
            End If
        Else
            cob = Obten_coverageIdEXT(brokerid, CInt(coverageId))
        End If



        Try
            Dim restGT As RESTful = New RESTful()
            restGT.automikRequest = automikRequest
            Dim jsonResult As String
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim totreg As Integer = 0
            Dim dtsaseg As New DataSet()
            dtsaseg = Obten_Aseguradora(brokerid, aseg)
            dts = Obten_Plazos(paqueteid)
            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    totreg = dts.Tables(0).Rows.Count
                    For i As Integer = 0 To dts.Tables(0).Rows.Count - 1

                        Dim json As New JSON()
                        Threading.Thread.Sleep(1000 * Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Item("wsDelay")))
                        json.header.aapType = "45555F6"
                        json.header.dateRequest = DateTime.Now.ToString("dd-MM-yyyy") & " 00:00:00" ' "06-08-2016 00:00:00"
                        json.header.channel = "8"
                        json.header.subChannel = "8"
                        json.header.branchOffice = "CONSUMER FINANCE"
                        json.header.managementUnit = IIf(tipoprod = 14, "0001", "0011")
                        json.header.user = "CARLOS"
                        json.header.idSession = "3232-3232"
                        json.header.idRequest = "1212-121212-12121-212"
                        json.header.dateConsumerInvocation = DateTime.Now.ToString("dd-MM-yyyy") & " 00:00:00" ' "06-08-016 00:00:00"

                        json.quote.idQuote = ""

                        json.policy.validityPeriod.startDate = DateTime.Now.ToString("yyyy-MM-dd") '"2016-02-01"
                        json.policy.validityPeriod.endDate = DateTime.Now.AddMonths(CInt(IIf(tipo_seg = 30 Or tipo_seg = 31, "12", dts.Tables(0).Rows(i).Item("VALOR").ToString))).ToString("yyyy-MM-dd") '"2017-02-01"

                        json.policy.preferredBeneficiary = ""
                        json.policy.rcUSAIndicator = "N"
                        json.policy.effectiveAdditionaldays = ""
                        json.policy.invoiceValue = precio.ToString().Replace(",", "")

                        json.credit.creditPeriod = dts.Tables(0).Rows(i).Item("VALOR").ToString

                        json.insuranceType.catalogItemBase.id = idseg
                        json.insuranceType.catalogItemBase.name = tiposegbbva

                        json.usageCar.catalogItemBase.id = "6"
                        json.usageCar.catalogItemBase.name = "PARTICULAR"

                        json.paymentWay.catalogItemBase.id = idtipopago
                        json.paymentWay.catalogItemBase.name = tipopago

                        json.productPlan.catalogItemBase.id = cob 'Obten_coverageIdEXT(brokerid, coverageId)
                        json.productPlan.productCode = "2002"
                        json.productPlan.planReview = IIf(tipoprod = 14, "008", "007")
                        json.productPlan.bouquetCode = "AUAR"
                        json.productPlan.subPlan = ""

                        json.circulationArea.catalogItemBase.id = idedo.ToString("D3")
                        json.circulationArea.catalogItemBase.name = edo

                        json.vehicleFeatures.carModel.catalogItemBase.name = model
                        json.vehicleFeatures.identifierVehicleFeatures.catalogItemBase.id = Id_Externo
                        json.vehicleFeatures.identifierVehicleFeatures.catalogItemBase.name = version
                        json.vehicleFeatures.originType.catalogItemBase.id = "N"
                        json.vehicleFeatures.originType.catalogItemBase.name = "NACIONAL"

                        Dim data3 As particularData = New particularData()
                        data3.aliasCriterion = "AFSAE"
                        data3.transformer.catalogItemBase.id = "001"
                        data3.transformer.catalogItemBase.name = mtoacc.ToString
                        json.particularData.Add(data3)


                        json.serviceType.catalogItemBase.id = "PAR"
                        json.serviceType.catalogItemBase.name = "PARTICULAR"


                        ''Termina armado de json
                        ''***********************************************


                        Dim jsonBODY As String = serializer.Serialize(json)

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
                            Dim res As jsonresponse = serializer.Deserialize(Of jsonresponse)(jsonResult)
                            For ds As Integer = 0 To res.rate.Count - 1
                                If res.rate(ds).paymentWay.id.ToString = "A" Then
                                    row1("ID_PAQUETE") = dts.Tables(0).Rows(i).Item("VALOR").ToString
                                    row1("PRIMA NETA") = Math.Truncate((CDbl(res.rate(ds).totalPaymentWithTax.amount.ToString()) / 1.16) * 100) / 100
                                    row1("RECARGO") = 0
                                    row1("DERECHO") = 0 'Math.Truncate((CDbl(res.rightPolicy.amount.ToString)) * 100) / 100
                                    row1("IVA") = (Math.Truncate(CDbl(res.rate(ds).totalPaymentWithTax.amount.ToString()) * 100) / 100) - (Math.Truncate((CDbl(res.rate(ds).totalPaymentWithTax.amount.ToString()) / 1.16) * 100) / 100)
                                    row1("PRIMA TOTAL") = Math.Truncate(CDbl(res.rate(ds).totalPaymentWithTax.amount.ToString()) * 100) / 100
                                    row1("ASEGURADORA") = dtsaseg.Tables(0).Rows(0).Item("DESCEXTERNO")
                                    row1("PAQUETE") = Obten_coverageId(brokerid, coverageId)
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
                                    row2("endDate") = DateTime.Now.AddMonths(CInt(dts.Tables(0).Rows(i).Item("VALOR"))).ToString("dd-MM-yyyy") '"2017-02-01"
                                    row2("shippingCosts") = 0 'Math.Truncate((CDbl(res.rightPolicy.amount.ToString)) * 100) / 100
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
                End If
            End If


            resulbbva.Tables(0).TableName = "Result"
            resulbbva.Tables(1).TableName = "Recibos"
            Return resulbbva

        Catch ex As Exception
            _strError = ex.Message
            Return Nothing
        End Try
    End Function

    Private Function Obten_Plazos(ByVal IDPaquete As Integer) As DataSet
        Dim term As New DataSet
        Dim objPlazo As New SNProcotiza.clsPaquetes()
        Dim objconex As New SDManejaBD.clsConexion()
        Dim sql As New StringBuilder

        sql.AppendLine("SELECT A.ID_PLAZO, A.DESCRIPCION, A.VALOR ")
        sql.AppendLine("FROM PLAZOS A ")
        sql.AppendLine("INNER JOIN PAQUETES_PLAZOS B ON B.ID_PLAZO = A.ID_PLAZO")
        sql.AppendLine("WHERE ID_PAQUETE = " & IDPaquete.ToString)
        sql.AppendLine("And A.ESTATUS = 2")
        sql.AppendLine("And B.ESTATUS = 2")
        sql.AppendLine("ORDER BY A.VALOR")

        term = objconex.EjecutaQueryConsulta(sql.ToString)

        Return term
    End Function
    Private Function Obten_Aseguradora(ByVal brokerid As Integer, ByVal AsegID As Integer) As DataSet
        Dim aseguradora As New DataSet()

        Dim objconex As New SDManejaBD.clsConexion()
        Dim sql As New StringBuilder

        sql.AppendLine("SELECT * ")
        sql.AppendLine("FROM CATALOGO_BANCOMER ")
        sql.AppendLine("WHERE RECURSOWEB = '" & brokerid.ToString & "'")
        sql.AppendLine("AND ESTATUS = 2 ")
        sql.AppendLine("AND IDINTERNO = " & AsegID.ToString)

        aseguradora = objconex.EjecutaQueryConsulta(sql.ToString)

        Return aseguradora
    End Function
    Private Function Obten_coverageId(ByVal brokerid As Integer, ByVal coverage As Integer) As String
        Dim coverageid As String = String.Empty
        Dim dataset As New DataSet()
        Dim objconex As New SDManejaBD.clsConexion
        Dim sql As New StringBuilder
        Dim dts As New DataSet()

        sql.AppendLine("SELECT * FROM COBERTURAS")
        sql.AppendLine("WHERE ID_BROKER = " & brokerid.ToString)
        sql.AppendLine("AND ID_COBERTURA = " & coverage.ToString)
        sql.AppendLine("AND ESTATUS = 2")

        dts = objconex.EjecutaQueryConsulta(sql.ToString)

        coverageid = dts.Tables(0).Rows(0).Item("NOMBRE").ToString

        Return coverageid
    End Function
    Private Function Obten_coverageIdEXT(ByVal brokerid As Integer, ByVal coverage As Integer) As String
        Dim coverageid As String = String.Empty
        Dim dataset As New DataSet()
        Dim objconex As New SDManejaBD.clsConexion
        Dim sql As New StringBuilder
        Dim dts As New DataSet()

        sql.AppendLine("SELECT * FROM COBERTURAS")
        sql.AppendLine("WHERE ID_BROKER = " & brokerid.ToString)
        sql.AppendLine("AND ID_COBERTURA = " & coverage.ToString)
        sql.AppendLine("AND ESTATUS = 2")

        dts = objconex.EjecutaQueryConsulta(sql.ToString)

        coverageid = dts.Tables(0).Rows(0).Item("ID_EXTERNO").ToString

        Return coverageid
    End Function
End Class
