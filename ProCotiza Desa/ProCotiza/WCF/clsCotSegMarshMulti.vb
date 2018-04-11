'RQ-PC5: JMENDIETA  27/01/2018: Se crea la clase que consume el servicio quote
Imports WCF.clsDeserialMarshMulti
Imports System.Text
Imports System.Net
Imports SNProcotiza

Public Class clsCotSegMarshMulti
    Private _strError As String = String.Empty
    Private _IDBroker As Integer = 0
    Private _URL As String = String.Empty
    Private _userID As String = String.Empty
    Private _password As String = String.Empty

    Public ReadOnly Property urlMARSH As String
        Get
            Dim urlService = System.Configuration.ConfigurationManager.AppSettings.Item("urlMarsh")

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

    Private wsDelay As Integer = 0

    Sub New()

    End Sub

    Public Class JSON
        Public quotes As List(Of quote) = New List(Of quote)
        Public iComplements As iComplements = New iComplements()
    End Class

    Public Class quote
        Public complement As complement = New complement()
        Public serviceTypeId As String = String.Empty
        Public iQuote As iQuote = New iQuote()
        Public policyType As String = String.Empty
        Public insuranceType As String = String.Empty
        Public term As String = String.Empty
        Public agencyNumber As String = String.Empty
        Public insurerId As String = String.Empty
        Public prospect As prospect = New prospect()
        Public validityPeriod As validityPeriod = New validityPeriod()
    End Class

    Public Class complement
        Public user As user = New user()
    End Class

    Public Class user
        Public id As String = String.Empty
        Public credentials As credentials = New credentials()
    End Class

    Public Class credentials
        Public accessPassword As String = String.Empty
    End Class

    Public Class iQuote
        Public VehicleQuote As VehicleQuote = New VehicleQuote()
    End Class

    Public Class VehicleQuote
        Public car As car = New car()
        Public accessorySum As accessorySum = New accessorySum()
    End Class

    Public Class car
        Public brand As brand = New brand()
        Public model As String = String.Empty
        Public type As String = String.Empty
        Public plates As String = String.Empty
        Public unitType As String = String.Empty
        Public unitPrice As unitPrice = New unitPrice()
        Public stateId As String = String.Empty
    End Class

    Public Class brand
        Public brand As String = String.Empty
        Public subBrand As String = String.Empty
    End Class

    Public Class unitPrice
        Public amount As String = String.Empty
    End Class

    Public Class accessorySum
        Public amount As String = String.Empty
    End Class

    Public Class iComplements
        Public EmissionComplementMarsh As EmissionComplementMarsh = New EmissionComplementMarsh()
    End Class

    Public Class EmissionComplementMarsh
        Public bussinesId As String = String.Empty
        Public reference1 As String = String.Empty
    End Class

    Public Class extendedData
        Public gender As String = String.Empty
    End Class

    Public Class validityPeriod
        Public startDate As String = String.Empty
        Public endDate As String = String.Empty
    End Class

    Public Class msjerr
        Public message As String
        Public status As String
    End Class

    Public Class prospect
        Public birthDate As String = String.Empty
        Public extendedData As extendedData = New extendedData()
        Public legalAddress As legalAddress = New legalAddress()
    End Class

    Public Class legalAddress
        Public zipCode As String = String.Empty
    End Class

    Public Function CotizaMultiMarsh(ByVal brokerid As Integer, ByVal service As Integer, ByVal model As Integer, ByVal cartype As Integer,
                                ByVal carunitType As Integer, ByVal unitPriceamount As Decimal, ByVal stateId As Integer,
                                ByVal accessorySum As Decimal, ByVal policyType As Integer, ByVal insuranceType As String, ByVal idclasif As Integer,
                                ByVal Idmarca As Integer, ByVal Idsubmarca As Integer, ByVal Idversion As Integer, ByVal IdAnio As Integer,
                                ByVal insurerId As Integer, ByVal idagencia As Integer, ByVal PaqueteId As Integer, Optional ByVal automikRequest As Boolean = 0, Optional headers As WebHeaderCollection = Nothing,
                                Optional ByVal zipCode As String = Nothing) As DataSet

        Dim json As New JSON()
        Dim IDQuote As New DataTable()
        Dim dttrecibos As New DataTable()
        Dim result As New DataSet()

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

        Try

            Dim jsonResult As String
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim objDes As New clsDeserialMarshMulti()
            Dim clsDatosMarsh As New clsDatosMarshMulti()
            clsDatosMarsh.IdBroker = brokerid

            'Obten_serviceTypeId
            Dim serviceType As String = String.Empty
            Dim dsServiceType As New DataSet()
            clsDatosMarsh.UseTypeId = service
            dsServiceType = clsDatosMarsh.ObtenDatosMarsh(1)
            If String.IsNullOrEmpty(clsDatosMarsh.ErrorSeguroDanios) AndAlso (Not IsNothing(dsServiceType)) AndAlso dsServiceType.Tables.Count > 0 AndAlso dsServiceType.Tables(0).Rows.Count > 0 Then
                serviceType = Integer.Parse(dsServiceType.Tables(0).Rows(0).Item("ID_EXTERNO")).ToString()
            Else
                _strError = "No se encontro el tipo de uso."
                Return Nothing
            End If

            'Obten_cartype
            Dim typecar As String = String.Empty
            Dim dsCarType As New DataSet()
            clsDatosMarsh.CarTypeId = cartype
            dsCarType = clsDatosMarsh.ObtenDatosMarsh(2)
            If String.IsNullOrEmpty(clsDatosMarsh.ErrorSeguroDanios) AndAlso (Not IsNothing(dsCarType)) AndAlso dsCarType.Tables.Count > 0 AndAlso dsCarType.Tables(0).Rows.Count > 0 Then
                typecar = dsCarType.Tables(0).Rows(0).Item("ID_EXTERNO").ToString
            Else
                _strError = "No se encontro el tipo de transporte."
                Return Nothing
            End If

            'Obten_policyType
            Dim Typepolicy As String = String.Empty
            Dim dsPolicyType As New DataSet()
            clsDatosMarsh.PolicyTypeId = policyType
            dsPolicyType = clsDatosMarsh.ObtenDatosMarsh(3)
            If String.IsNullOrEmpty(clsDatosMarsh.ErrorSeguroDanios) AndAlso (Not IsNothing(dsPolicyType)) AndAlso dsPolicyType.Tables.Count > 0 AndAlso dsPolicyType.Tables(0).Rows.Count > 0 Then
                Typepolicy = dsPolicyType.Tables(0).Rows(0).Item("ID_EXTERNO").ToString
            Else
                _strError = "No se encontro el tipo de cobertura."
                Return Nothing
            End If

            'Obten_plates
            Dim dsPlates As New DataSet()
            clsDatosMarsh.ClasificacionId = idclasif
            clsDatosMarsh.MarcaId = Idmarca
            clsDatosMarsh.SubmarcaId = Idsubmarca
            clsDatosMarsh.VersionId = Idversion
            clsDatosMarsh.AnioId = IdAnio
            dsPlates = clsDatosMarsh.ObtenDatosMarsh(4)
            If (Not String.IsNullOrEmpty(clsDatosMarsh.ErrorSeguroDanios)) OrElse (IsNothing(dsPlates)) OrElse dsPlates.Tables.Count = 0 OrElse dsPlates.Tables(0).Rows.Count = 0 Then
                _strError = "El producto seleccionado no tiene idenficador para el broker de seguros."
                Return Nothing
            End If

            'Obten_Aseguradora Se obtiene todas las aseguradoras
            Dim dsAseguradora As New DataSet
            Dim idExternoAseguradora As String = String.Empty
            Dim lstAseguradora As List(Of AseguradorasDetalle)
            clsDatosMarsh.AseguradoraId = insurerId
            dsAseguradora = clsDatosMarsh.ObtenDatosMarsh(5)
            If (Not String.IsNullOrEmpty(clsDatosMarsh.ErrorSeguroDanios)) OrElse (IsNothing(dsAseguradora)) OrElse dsAseguradora.Tables.Count = 0 OrElse dsAseguradora.Tables(0).Rows.Count = 0 Then
                _strError = "Aseguradora seleccionada no valida para cotizacion"
                Return Nothing
            Else
                'Dim lstAseguradora = (From dr In dsAseguradora.Tables(0).AsEnumerable Select New With { _
                '              Key .RECURSOWEB = dr.Field(Of String)("RECURSOWEB"), _
                '              Key .IDEXTERNO = dr.Field(Of String)("IDEXTERNO"), _
                '              Key .DESCEXTERNO = dr.Field(Of String)("DESCEXTERNO"), _
                '              Key .IDINTERNO = dr.Field(Of Integer)("IDINTERNO"), _
                '              Key .DESCINTERNO = dr.Field(Of String)("DESCINTERNO")
                '              }).ToList()

                lstAseguradora = (From dr In dsAseguradora.Tables(0).AsEnumerable
                                      Select New AseguradorasDetalle With { _
                                          .IDCATALOGO = dr.Field(Of Integer)("IDCATALOGO"), _
                                          .RECURSOWEB = dr.Field(Of String)("RECURSOWEB"), _
                                          .IDEXTERNO = dr.Field(Of String)("IDEXTERNO"), _
                                          .DESCEXTERNO = dr.Field(Of String)("DESCEXTERNO"), _
                                          .IDINTERNO = dr.Field(Of Integer)("IDINTERNO"), _
                                          .DESCINTERNO = dr.Field(Of String)("DESCINTERNO"), _
                                          .REFERENCIA = dr.Field(Of String)("REFERENCIA"), _
                                          .ESTATUS = dr.Field(Of Integer)("ESTATUS")
                                      }).ToList()

                If lstAseguradora.Exists(Function(x) x.IDINTERNO = insurerId) Then

                    idExternoAseguradora = lstAseguradora.FirstOrDefault(Function(x) x.IDINTERNO = insurerId).IDEXTERNO

                    If IsNothing(idExternoAseguradora) Then
                        _strError = "Aseguradora seleccionada no valida para cotizacion"
                        Return Nothing
                    End If
                Else
                    _strError = "Aseguradora seleccionada no valida para cotizacion"
                    Return Nothing
                End If
            End If

            'Obtener el tiempo de espera para volver a consumir el servicio
            Dim dsDelay As New DataSet()
            clsDatosMarsh.ParametroId = 210
            dsDelay = clsDatosMarsh.ObtenDatosMarsh(7)
            If String.IsNullOrEmpty(clsDatosMarsh.ErrorSeguroDanios) AndAlso (Not IsNothing(dsDelay)) AndAlso dsDelay.Tables.Count > 0 AndAlso dsDelay.Tables(0).Rows.Count > 0 Then
                wsDelay = Convert.ToInt32(dsDelay.Tables(0).Rows(0).Item("VALOR"))
            End If

            'Obten_Plazos
            Dim dsPlazos As New DataSet()
            clsDatosMarsh.PaqueteId = PaqueteId
            dsPlazos = clsDatosMarsh.ObtenDatosMarsh(6)
            If (String.IsNullOrEmpty(clsDatosMarsh.ErrorSeguroDanios)) AndAlso (Not IsNothing(dsPlazos)) AndAlso dsPlazos.Tables.Count > 0 AndAlso dsPlazos.Tables(0).Rows.Count > 0 Then

                For Each plazo As DataRow In dsPlazos.Tables(0).Rows

                    Dim quote As New quote()

                    quote.complement.user.id = System.Configuration.ConfigurationManager.AppSettings.Item("useridMarsh")
                    quote.complement.user.credentials.accessPassword = System.Configuration.ConfigurationManager.AppSettings.Item("passwordMarsh")
                    quote.serviceTypeId = serviceType ''"12"
                    quote.iQuote.VehicleQuote.car.brand.brand = dsPlates.Tables(0).Rows(0).Item("MARCA") '"VOLKSWAGEN"
                    quote.iQuote.VehicleQuote.car.brand.subBrand = dsPlates.Tables(0).Rows(0).Item("SUBMARCA") '"GOL"
                    quote.iQuote.VehicleQuote.car.model = model.ToString ' "2017"
                    quote.iQuote.VehicleQuote.car.type = typecar '"A"
                    quote.iQuote.VehicleQuote.car.plates = dsPlates.Tables(0).Rows(0).Item("ID_EXTERNO") ' "MB115135"
                    quote.iQuote.VehicleQuote.car.unitType = IIf(carunitType = 63, "01", "02") '"01" ''NUEVO/SEMINUEVO
                    quote.iQuote.VehicleQuote.car.unitPrice.amount = unitPriceamount.ToString '' "138279.81"
                    quote.iQuote.VehicleQuote.car.stateId = IIf(stateId.ToString.Length < 2, "0" & stateId.ToString, stateId.ToString) '"30"
                    quote.iQuote.VehicleQuote.accessorySum.amount = accessorySum '"0.00"
                    quote.policyType = "" 'IIf(idExternoAseguradora = "0", "", Typepolicy) ' "AMPLIA"
                    quote.insuranceType = insuranceType ' "MULTIANUAL FRACCIONADO"
                    quote.term = plazo.Item("VALOR") '"18"
                    quote.agencyNumber = idagencia.ToString  'idagencia  ' "3458"
                    quote.insurerId = idExternoAseguradora ' "2841"
                    quote.prospect.birthDate = "1980-05-08" '"1973-05-18"
                    quote.prospect.extendedData.gender = "02" '02
                    quote.prospect.legalAddress.zipCode = zipCode
                    quote.validityPeriod.startDate = DateTime.Now.ToString("yyyy-MM-dd")  ''"2016-10-12"
                    quote.validityPeriod.endDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") ''"2016-10-12
                    json.quotes = New List(Of quote)
                    json.quotes.Add(quote)


                    json.iComplements.EmissionComplementMarsh.bussinesId = "1"
                    json.iComplements.EmissionComplementMarsh.reference1 = dsPlates.Tables(0).Rows(0).Item("DESCRIPCION")  '"CL AIRE 1.6L L4 101HP MT"

                    Dim jsonBODY As String = serializer.Serialize(json)

                    Dim restGT As RESTful = New RESTful()
                    restGT.automikRequest = automikRequest
                    restGT.Uri = _URL

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
                        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("urlMarshMulti")
                    Else
                        restGT.Uri = urlMARSH
                    End If

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
                    End If

                    Dim res2 As String = jsonResult
                    res2 = Replace(res2, "error", "errorInfo")
                    res2 = Replace(res2, "errorInfoId", "errorId")
                    Dim jresult As Marsh = serializer.Deserialize(Of Marsh)(res2)


                    'If jresult.errorInfo.errorId.ToString().Trim() = "0" Then

                    Dim _newResult = jresult.quotes.Where(Function(x) x.policyTypeId.Trim.ToUpper = Typepolicy.Trim.ToUpper).ToList()

                    IDQuote = objDes.Deserealize(_newResult, lstAseguradora, plazo.Item("VALOR"))
                    dttrecibos = objDes.RecibosMarsh(_newResult)

                    'End If 

                    'If Not String.IsNullOrEmpty(jresult.errorInfo.description) Then
                    '    _strError = jresult.errorInfo.description
                    'End If


                    For Each row As DataRow In IDQuote.Rows
                        dtb.ImportRow(row)
                    Next

                    For Each row2 As DataRow In dttrecibos.Rows
                        dtrec.ImportRow(row2)
                    Next

                    Threading.Thread.Sleep(1000 * wsDelay)
                Next

                result.Tables.Add(dtb)
                result.Tables.Add(dtrec)
            Else
                _strError = "No se encontraron los plazos."
                Return Nothing
            End If

            result.Tables(0).TableName = "Result"
            result.Tables(1).TableName = "Recibos"

            Return result

        Catch ex As Exception
            _strError = ex.Message
            Return Nothing
        End Try
    End Function

End Class
