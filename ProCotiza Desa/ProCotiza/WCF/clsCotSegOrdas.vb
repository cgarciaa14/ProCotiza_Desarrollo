'BBV-P412 RQCOT-01.2: AMR 23/02/2017: Seguros.
'BUG-PC-58:AMATA:03/05/2017:Seguros Ordas
'BUG-PC-81: RHERNANDEZ: 29/06/17: SE EVALUA EL TIPO DE SEGURO PARA CONTADO Y FINANCIADO
'BUG-PC-84: RHERNANDEZ: 11/07/17: SE AGREGA FUNCIONALIDAD PARA MULTIANUAL FINANCIADO
'AUTOMIK-TASK-309:RHERNANDEZ:22/11/2017:Implementacion servicio ORDAS a calculadora
Imports System.Text
Imports WCF.clsDeserialOrdas
Imports System.Net

Public Class clsCotSegOrdas

    Private _strError As String = String.Empty
    Private _IDBroker As Integer = 0
    Private _URL As String = String.Empty
    Private _userID As String = String.Empty
    Private _password As String = String.Empty

    Public ReadOnly Property urlORDAS As String
        Get
            Dim urlService = System.Configuration.ConfigurationManager.AppSettings.Item("urlOrdas")

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

    Public Property IDBroker As Integer
        Get
            Return _IDBroker
        End Get
        Set(value As Integer)
            _IDBroker = value
        End Set
    End Property

    Public Property URL As String
        Get
            Return _URL
        End Get
        Set(value As String)
            _URL = value
        End Set
    End Property

    Public Property UserID As String
        Get
            Return _userID
        End Get
        Set(value As String)
            _userID = value
        End Set
    End Property

    Public Property Password As String
        Get
            Return _password
        End Get
        Set(value As String)
            _password = value
        End Set
    End Property

    Public Class JSON
        Public quote As quote = New quote()
    End Class

    Public Class quote
        Public complement As complement = New complement()
        Public iQuote As iQuote = New iQuote()
        Public coverageId As String = String.Empty
        Public idProduct As String = String.Empty
        Public termId As String = String.Empty
        Public agencyNumber As String = String.Empty
        Public insurerId As String = String.Empty
        Public insuredAmount As insuredAmount = New insuredAmount()
        Public prospect As prospect = New prospect()
        Public idAdditionalPack As Integer?
        Public idPaymentType As Integer?
        Public idAdditionalPaymentWay As Integer?
        Public idAdditionalTerm As Integer?
        Public generateSeveralTerms As Integer?
        Public currency As currency = New currency()
    End Class

    Public Class complement
        Public user As user = New user()
    End Class

    Public Class user
        Public id As String = String.Empty
        Public credentials As credentials = New credentials
    End Class

    Public Class credentials
        Public accessPassword As String = String.Empty
    End Class

    Public Class iQuote
        Public VehicleQuote As VehicleQuote = New VehicleQuote()
    End Class

    Public Class VehicleQuote
        Public accessoryDescription As String = String.Empty
        Public idVehicle As String = String.Empty
        Public driver As driver = New driver()
        Public accessorySum As accessorySum = New accessorySum()
    End Class

    Public Class driver
        Public extendedData As extendedData = New extendedData()
    End Class

    Public Class extendedData
        Public age As Integer?
        Public gender As Integer?
    End Class

    Public Class accessorySum
        Public amount As Decimal?
    End Class

    Public Class insuredAmount
        Public amount As Integer?
    End Class

    Public Class prospect
        Public extendedData_prspct As extendedData_prspct = New extendedData_prspct()
        Public legalAddress As legalAddress = New legalAddress()
    End Class

    Public Class extendedData_prspct
        Public fiscalSituation As fiscalSituation = New fiscalSituation()
    End Class

    Public Class fiscalSituation
        Public relationName As Integer?
    End Class

    Public Class legalAddress
        Public state As Integer?
        Public zipCode As String = String.Empty
    End Class

    Public Class currency
        Public id As Integer?
    End Class

    Public Class msjerr
        Public message As String
        Public status As String
    End Class

    Sub New()
    End Sub

    Public Function CotizaOrdas(ByVal brokerid As Integer, idclasif As Integer, ByVal Idmarca As Integer, ByVal Idsubmarca As Integer,
                                ByVal Idversion As Integer, ByVal IdAnio As Integer, ByVal edad As Integer, ByVal gender As Integer,
                                ByVal accessorySum As Double, ByVal coverageId As String, ByVal idProduct As String,
                                ByVal insuredAmount As Double, ByVal relationName As Integer, ByVal state As Integer,
                                ByVal zipCode As String, ByVal insurerId As Integer, ByVal currency As Integer,
                                ByVal PaymentType As Integer, ByVal PaqueteId As Integer, Optional ByVal automikRequest As Boolean = 0, Optional headers As WebHeaderCollection = Nothing) As DataSet

        Dim dts As New DataSet()
        Dim json As New JSON()
        Dim IDQuote As New DataTable()
        Dim dttrecibos As New DataTable()
        Dim result As New DataSet()

        Dim relation As Integer = 0

        Select Case relationName
            Case 14 'FÍSICA
                relation = 1
            Case 15 'MORAL
                relation = 2
            Case 16 'FÍSICA CON ACTIVIDAD EMPRESARIAL
                relation = 3
        End Select
        Dim tipopago As String = ""
        If PaymentType = 30 Or PaymentType = 31 Or PaymentType = 75 Or PaymentType = 76 Then 'Cambio urgente rhernande ajustes a seguro financiado rhernandez
            tipopago = "9"
        Else
            tipopago = "1"
        End If
        Dim dtb As New DataTable
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
        dtb.Columns.Add("INSURERID")

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
        dtrec.Columns.Add("insurerId")
        dtrec.Columns.Add("idPack")


        Try


            _userID = System.Configuration.ConfigurationManager.AppSettings.Item("useridOrdas")
            _password = System.Configuration.ConfigurationManager.AppSettings.Item("passwordOrdas")

            Dim idveh As String = ObtenidVehicle(brokerid, idclasif, Idmarca, Idsubmarca, Idversion, IdAnio)

            If idveh = "" Then
                _strError = "El producto seleccionado no tiene idenficador para el broker de seguros."
                Exit Function
            End If

            dts = Obten_Plazos(PaqueteId)
            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To dts.Tables(0).Rows.Count - 1
                        json.quote.complement.user.id = _userID
                        json.quote.complement.user.credentials.accessPassword = _password
                        json.quote.iQuote.VehicleQuote.accessoryDescription = "CON BASE A FACTURA"
                        json.quote.iQuote.VehicleQuote.idVehicle = idveh ''"HO1503A073932"
                        json.quote.iQuote.VehicleQuote.driver.extendedData.age = edad ''35
                        json.quote.iQuote.VehicleQuote.driver.extendedData.gender = gender '1
                        json.quote.iQuote.VehicleQuote.accessorySum.amount = accessorySum ''0
                        json.quote.coverageId = Obtencoverage(brokerid, coverageId)  '"4216"
                        json.quote.idProduct = ObtenProduct(brokerid, idProduct) '"1" '?? nuevo - seminuevo - uber/taxi
                        json.quote.termId = IIf(PaymentType = 30 Or PaymentType = 31, "12", dts.Tables(0).Rows(i).Item("VALOR")) '' "12"
                        json.quote.agencyNumber = "19" ''19 ''3739
                        json.quote.insurerId = Obteninsurer(brokerid, insurerId) ' "0"
                        json.quote.insuredAmount.amount = insuredAmount ''450000
                        json.quote.prospect.extendedData_prspct.fiscalSituation.relationName = relation '1 ''tipo de personalidad
                        json.quote.prospect.legalAddress.state = state ''9
                        json.quote.prospect.legalAddress.zipCode = zipCode '"09430"

                        json.quote.idAdditionalPack = 0
                        json.quote.idPaymentType = tipopago ' multianual fraccionado 1 --> cualquier otro 9
                        json.quote.idAdditionalPaymentWay = -1
                        json.quote.idAdditionalTerm = 0
                        json.quote.generateSeveralTerms = 0
                        json.quote.currency.id = currency '1

                        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                        Dim jsonBODY As String = serializer.Serialize(json)

                        jsonBODY = Replace(jsonBODY, "extendedData_prspct", "extendedData")

                        Dim restGT As RESTful = New RESTful()
                        restGT.Uri = _URL
                        restGT.automikRequest = automikRequest

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
                            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("urlOrdas")
                        Else
                            restGT.Uri = urlORDAS
                        End If
                        Dim objDes As clsDeserialOrdas = New clsDeserialOrdas()

                        restGT.buscarHeader("ResponseWarningDescription")
                        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)

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
                        End If

                        Dim jresult As Ordas = serializer.Deserialize(Of Ordas)(jsonResult)

                        If jresult.quote.complement.errorInfo.description.ToString.Contains("GO:: ") Then
                            _strError = jresult.quote.complement.errorInfo.description.ToString
                        End If

                        IDQuote = objDes.Deserealize(jsonResult, dts.Tables(0).Rows(i).Item("VALOR"), tipopago)
                        dttrecibos = objDes.RecibosOrdas(jsonResult)

                        'If objDes.StrError <> "" Then
                        '    _strError = objDes.StrError
                        '    Return Nothing
                        '    Exit Function
                        'End If

                        For Each row As DataRow In IDQuote.Rows
                            dtb.ImportRow(row)
                        Next

                        For Each row2 As DataRow In dttrecibos.Rows
                            dtrec.ImportRow(row2)
                        Next
                    Next

                    result.Tables.Add(dtb)
                    result.Tables.Add(dtrec)

                End If
            End If

            result.Tables(0).TableName = "Result"
            result.Tables(1).TableName = "Recibos"

            Return result

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

    Private Function ObtenidVehicle(ByVal brokerid As Integer, idclasif As Integer, ByVal Idmarca As Integer, ByVal Idsubmarca As Integer,
                                  ByVal Idversion As Integer, ByVal IdAnio As Integer) As String

        Dim Vehicleid As String = String.Empty
        Dim objconex As New SDManejaBD.clsConexion
        Dim sql As New StringBuilder
        Dim dts As New DataSet()

        sql.AppendLine("SELECT B.NOMBRE AS MARCA, C.DESCRIPCION AS SUBMARCA, ISNULL(A.ID_EXTERNO, '') ID_EXTERNO")
        sql.AppendLine("FROM PRODUCTOS A")
        sql.AppendLine("INNER JOIN MARCAS B ON B.ID_MARCA = A.ID_MARCA")
        sql.AppendLine("INNER JOIN SUBMARCAS C ON C.ID_SUBMARCA = A.ID_SUBMARCA")
        sql.AppendLine("WHERE A.ID_CLASIFICACION = " & idclasif.ToString)
        sql.AppendLine("AND A.ID_MARCA = " & Idmarca.ToString)
        sql.AppendLine("AND A.ID_SUBMARCA = " & Idsubmarca.ToString)
        sql.AppendLine("AND A.ID_VERSION = " & Idversion.ToString)
        sql.AppendLine("AND A.ID_ANIO = " & IdAnio.ToString)

        dts = objconex.EjecutaQueryConsulta(sql.ToString)

        Vehicleid = dts.Tables(0).Rows(0).Item("ID_EXTERNO")

        Return Vehicleid

    End Function

    Private Function Obtencoverage(ByVal brokerid As Integer, ByVal intcoverage As Integer) As String
        Dim cover As String = String.Empty

        Dim objconex As New SDManejaBD.clsConexion
        Dim sql As New StringBuilder
        Dim dts As New DataSet()

        sql.AppendLine("SELECT ID_EXTERNO ")
        sql.AppendLine("FROM COBERTURAS ")
        sql.AppendLine("WHERE ID_BROKER = " & brokerid)
        sql.AppendLine("AND ESTATUS = 2")
        sql.AppendLine("AND ID_COBERTURA = " & intcoverage)

        dts = objconex.EjecutaQueryConsulta(sql.ToString)

        cover = dts.Tables(0).Rows(0).Item("ID_EXTERNO")

        Return cover
    End Function

    Private Function ObtenProduct(ByVal brokerid As Integer, ByVal intuso As Integer) As String
        Dim idProduct As String = String.Empty

        Dim objconex As New SDManejaBD.clsConexion
        Dim sql As New StringBuilder
        Dim dts As New DataSet()

        sql.AppendLine("SELECT ID_EXTERNO ")
        sql.AppendLine("FROM TIPO_USO ")
        sql.AppendLine("WHERE ID_BROKER = " & brokerid)
        sql.AppendLine("AND ESTATUS = 2")
        sql.AppendLine("AND ID_USO = " & intuso)

        dts = objconex.EjecutaQueryConsulta(sql.ToString)

        idProduct = dts.Tables(0).Rows(0).Item("ID_EXTERNO")

        Return idProduct
    End Function

    Private Function Obteninsurer(ByVal brokerid As Integer, ByVal insurerId As Integer) As String
        Dim insurer As String = String.Empty

        Dim objconex As New SDManejaBD.clsConexion
        Dim sql As New StringBuilder
        Dim dts As New DataSet()

        sql.AppendLine("SELECT IDEXTERNO")
        sql.AppendLine("FROM CATALOGO_BANCOMER")
        sql.AppendLine("WHERE RECURSOWEB = '2'")
        sql.AppendLine("AND IDINTERNO = " & insurerId)

        dts = objconex.EjecutaQueryConsulta(sql.ToString)

        insurer = dts.Tables(0).Rows(0).Item("IDEXTERNO")

        Return insurer
    End Function

    Public Function cotizaGarantia(ByVal brokerid As Integer, idclasif As Integer, ByVal Idmarca As Integer, ByVal Idsubmarca As Integer,
                                    ByVal Idversion As Integer, ByVal IdAnio As Integer, ByVal edad As Integer, ByVal gender As Integer,
                                    ByVal accessorySum As Double, ByVal coverageId As String, ByVal idProduct As String,
                                    ByVal insuredAmount As Double, ByVal relationName As Integer, ByVal state As Integer,
                                    ByVal zipCode As String, ByVal insurerId As Integer, ByVal currency As Integer,
                                    ByVal PaymentType As Integer, ByVal PaqueteId As Integer, coverage() As String) As DataSet


        Dim dttrecibosgar As New DataTable()
        Dim result As New DataSet()
        Dim json As New JSON()

        Dim cover As String = String.Empty
        Dim term As String = String.Empty

        Dim s As String = String.Empty
        Dim arr() As String

        Dim relation As Integer = 0

        Select Case relationName
            Case 14 'FÍSICA
                relation = 1
            Case 15 'MORAL
                relation = 2
            Case 16 'FÍSICA CON ACTIVIDAD EMPRESARIAL
                relation = 3
        End Select

        Dim dtgarantia = New DataTable()
        dtgarantia.Columns.Add("idRequest")
        dtgarantia.Columns.Add("startDate")
        dtgarantia.Columns.Add("endDate")
        dtgarantia.Columns.Add("shippingCosts")
        dtgarantia.Columns.Add("tax")
        dtgarantia.Columns.Add("realPremium")
        dtgarantia.Columns.Add("totalPremium")
        dtgarantia.Columns.Add("lateFee")
        dtgarantia.Columns.Add("serialNumber")
        dtgarantia.Columns.Add("insurerId")
        dtgarantia.Columns.Add("idPack")


        _URL = System.Configuration.ConfigurationManager.AppSettings.Item("urlOrdas")
        _userID = System.Configuration.ConfigurationManager.AppSettings.Item("useridOrdas")
        _password = System.Configuration.ConfigurationManager.AppSettings.Item("passwordOrdas")

        Dim idveh As String = ObtenidVehicle(brokerid, idclasif, Idmarca, Idsubmarca, Idversion, IdAnio)

        If idveh = "" Then
            _strError = "El producto seleccionado no tiene idenficador para el broker de seguros."
            Exit Function
        End If

        Try
            For Each value As String In coverage
                s = value
                arr = s.Split(",")

                For i As Integer = 0 To UBound(arr)
                    cover = arr(i)
                    term = arr(i + 1)
                    Exit For
                Next

                json.quote.complement.user.id = _userID
                json.quote.complement.user.credentials.accessPassword = _password
                json.quote.iQuote.VehicleQuote.accessoryDescription = "CON BASE A FACTURA"
                json.quote.iQuote.VehicleQuote.idVehicle = idveh
                json.quote.iQuote.VehicleQuote.driver.extendedData.age = 35
                json.quote.iQuote.VehicleQuote.driver.extendedData.gender = 1
                json.quote.iQuote.VehicleQuote.accessorySum.amount = accessorySum
                json.quote.coverageId = cover
                json.quote.idProduct = 20 ' nuevo = 20 - seminuevo = 23
                json.quote.termId = term
                json.quote.agencyNumber = "19" ''19
                json.quote.insurerId = "32"
                json.quote.insuredAmount.amount = insuredAmount
                json.quote.prospect.extendedData_prspct.fiscalSituation.relationName = relation '1 ''tipo de personalidad
                json.quote.prospect.legalAddress.state = 9 'state
                json.quote.prospect.legalAddress.zipCode = "06900" '"09430" 'zipCode

                json.quote.idAdditionalPack = -1
                json.quote.idPaymentType = 9
                json.quote.idAdditionalPaymentWay = -1
                json.quote.idAdditionalTerm = -1
                json.quote.generateSeveralTerms = 0
                json.quote.currency.id = currency

                Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                Dim jsonBODY As String = serializer.Serialize(json)

                jsonBODY = Replace(jsonBODY, "extendedData_prspct", "extendedData")

                Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
                Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

                Dim objDes As clsDeserialOrdas = New clsDeserialOrdas()
                Dim restGT As RESTful = New RESTful()
                restGT.Uri = _URL
                restGT.buscarHeader("ResponseWarningDescription")
                Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)

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
                End If

                dttrecibosgar = objDes.RecibosOrdas(jsonResult)

                For Each row2 As DataRow In dttrecibosgar.Rows
                    dtgarantia.ImportRow(row2)
                Next
            Next

            result.Tables.Add(dtgarantia)
            result.Tables(0).TableName = "Garantia"

            Return result

        Catch ex As Exception
            _strError = ex.Message
            Return Nothing
        End Try
    End Function

End Class
