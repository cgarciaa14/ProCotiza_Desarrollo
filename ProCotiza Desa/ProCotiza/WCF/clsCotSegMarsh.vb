'BBV-P412 RQCOT-01.2: AMR 23/02/2017: Seguros.
'BUG-PC-51 MAPH 17/04/2017 CAMBIOS SOLICITADOS POR MARREDONDO
'BUG-PC-52 AMR 21/04/2017 Cambios Eikos y Marsh.
'BUG-PC-63:AMATA:13/05/2017:Correccion Eikos-Marsh.
'BUG-PC-72:RHERNANDEZ: 17/06/17: Se agrega validacion en caso de que se relacione una aseguradora equivocada  para cotizar seguros.
'RQ-MN2-3: RHERNANDEZ: 08/09/17: SE MODIFICA COTIZACION DE SEGUROS MARSH PARA QUE CUANDO LA ASEGURADORA SEA MULTICOTIZACION EL TIPO POLIZA SE ENVIE VACIO
'AUTOMIK-TASK-312:RHERNANDEZ:22/11/2017:Implementacion servicio MARSH a calculadora
'BUG-PC-156: RHERNANDEZ: 08/02/2018: Se corrige salida de cotizacion del seguro al fallar el servicio en un plazo
'AUTOMIK-BUG-453: RHERNANDEZ: 17/05/18: SE MODIFICA SERVICIO DE CALCULO DE SEGUROS PARA COTIZAR UN SOLO PLAZO
Imports System.Text
Imports WCF.clsDeserialMarsh
Imports System.Net

Public Class clsCotSegMarsh

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

    Sub New()

    End Sub

    Public Class JSON
        Public policy As policy = New policy()
        Public quote As quote = New quote()
    End Class

    Public Class policy

    End Class

    Public Class quote
        Public complement As complement = New complement()
        Public serviceTypeId As String = String.Empty
        Public iQuote As iQuote = New iQuote()
        Public iComplements As iComplements = New iComplements()
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

    Public Class prospect
        Public birthDate As String = String.Empty
        Public extendedData As extendedData = New extendedData()
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


    Public Function CotizaMarsh(ByVal brokerid As Integer, ByVal service As Integer, ByVal model As Integer, ByVal cartype As Integer,
                                ByVal carunitType As Integer, ByVal unitPriceamount As Decimal, ByVal stateId As Integer,
                                ByVal accessorySum As Decimal, ByVal policyType As Integer, ByVal insuranceType As String, ByVal idclasif As Integer,
                                ByVal Idmarca As Integer, ByVal Idsubmarca As Integer, ByVal Idversion As Integer, ByVal IdAnio As Integer,
                                ByVal insurerId As Integer, ByVal idagencia As Integer, ByVal PaqueteId As Integer, Optional ByVal automikRequest As Boolean = 0, Optional headers As WebHeaderCollection = Nothing, Optional IsMulticotizacion As Integer = 1, Optional idplazo As Integer = 0) As DataSet


        Dim dts As New DataSet()
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

            Dim objDes As clsDeserialMarsh = New clsDeserialMarsh()
            Dim serviceType As String = Obten_serviceTypeId(brokerid, service)
            Dim typecar As String = Obten_cartype(brokerid, cartype)
            Dim Typepolicy As String = Obten_policyType(brokerid, policyType)
            Dim dtsplates As DataSet = Obten_plates(brokerid, idclasif, Idmarca, Idsubmarca, Idversion, IdAnio)
            Dim dtsAseg As New DataSet
            dtsAseg = Obten_Aseguradora(brokerid, insurerId)
            If dtsAseg.Tables.Count > 0 Then
                If dtsAseg.Tables(0).Rows.Count > 0 Then
                Else
                    _strError = "Aseguradora seleccionada no valida para cotizacion"
                    Exit Function
                End If
            Else
                _strError = "Aseguradora seleccionada no valida para cotizacion"
                Exit Function
            End If


            If dtsplates.Tables(0).Rows(0).Item("ID_EXTERNO").ToString.Length = 0 Then
                _strError = "El producto seleccionado no tiene idenficador para el broker de seguros."
                Exit Function
            End If

            dts = Obten_Plazos(PaqueteId)
            If IsMulticotizacion = 0 Then
                Dim rows As DataRow() = (From x In dts.Tables(0).AsEnumerable().Cast(Of DataRow)() Where x.Field(Of Integer)("ID_PLAZO") <> idplazo).ToArray()
                For Each row As DataRow In rows
                    dts.Tables(0).Rows.Remove(row)
                Next
                dts.AcceptChanges()
            End If
            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To dts.Tables(0).Rows.Count - 1

                        json.quote.complement.user.id = System.Configuration.ConfigurationManager.AppSettings.Item("useridMarsh")
                        json.quote.complement.user.credentials.accessPassword = System.Configuration.ConfigurationManager.AppSettings.Item("passwordMarsh")
                        json.quote.serviceTypeId = serviceType ''"01"
                        json.quote.iQuote.VehicleQuote.car.brand.brand = dtsplates.Tables(0).Rows(0).Item("MARCA") '"VOLKSWAGEN"
                        json.quote.iQuote.VehicleQuote.car.brand.subBrand = dtsplates.Tables(0).Rows(0).Item("SUBMARCA") '"GOL"
                        json.quote.iQuote.VehicleQuote.car.model = model.ToString ' "2016"
                        json.quote.iQuote.VehicleQuote.car.type = typecar '"A"
                        json.quote.iQuote.VehicleQuote.car.plates = dtsplates.Tables(0).Rows(0).Item("ID_EXTERNO") ' "713GBE"
                        json.quote.iQuote.VehicleQuote.car.unitType = IIf(carunitType = 63, "01", "02") '"01" ''NUEVO/SEMINUEVO
                        json.quote.iQuote.VehicleQuote.car.unitPrice.amount = unitPriceamount.ToString '' "138279.81"
                        json.quote.iQuote.VehicleQuote.car.stateId = IIf(stateId.ToString.Length < 2, "0" & stateId.ToString, stateId.ToString) '"30"
                        json.quote.iQuote.VehicleQuote.accessorySum.amount = accessorySum '"0.00"
                        json.quote.iComplements.EmissionComplementMarsh.bussinesId = "1"
                        json.quote.iComplements.EmissionComplementMarsh.reference1 = dtsplates.Tables(0).Rows(0).Item("DESCRIPCION")  '"CL AIRE 1.6L L4 101HP MT"
                        json.quote.policyType = IIf(dtsAseg.Tables(0).Rows(0).Item("IDEXTERNO").ToString = "0", "", Typepolicy) ' "AMPLIA"
                        json.quote.insuranceType = insuranceType ' "MULTIANUAL FRACCIONADO"
                        json.quote.term = dts.Tables(0).Rows(i).Item("VALOR") '"18"
                        json.quote.agencyNumber = idagencia.ToString 'idagencia  ' "3458" 'BUG-PC-155
                        json.quote.insurerId = dtsAseg.Tables(0).Rows(0).Item("IDEXTERNO").ToString ' "125"
                        json.quote.prospect.birthDate = "1980-05-08" '"1973-05-18"
                        json.quote.prospect.extendedData.gender = "02" '02 ''Correspondiente a "M" de acuerdo al correo enviado por Luis Maya
                        json.quote.validityPeriod.startDate = DateTime.Now.ToString("yyyy-MM-dd")  ''"2016-10-12"
                        json.quote.validityPeriod.endDate = DateTime.Now.ToString("yyyy-MM-dd") ''"2016-10-12"


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
                            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("urlMarsh")
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


                        IDQuote = objDes.Deserealize(jsonResult, dtsAseg.Tables(0).Rows(0).Item("DESCEXTERNO"), dts.Tables(0).Rows(i).Item("VALOR"))
                        dttrecibos = objDes.RecibosMarsh(jsonResult)

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

    Private Function Obten_serviceTypeId(ByVal brokerid As Integer, ByVal useType As Integer) As String
        Dim type As String = String.Empty
        Dim dataset As New DataSet()
        Dim objconex As New SDManejaBD.clsConexion
        Dim sql As New StringBuilder
        Dim dts As New DataSet()

        sql.AppendLine("SELECT * FROM TIPO_USO")
        sql.AppendLine("WHERE ID_BROKER = " & brokerid.ToString)
        sql.AppendLine("AND ID_USO = " & useType.ToString)
        sql.AppendLine("AND ESTATUS = 2")

        dts = objconex.EjecutaQueryConsulta(sql.ToString)

        type = dts.Tables(0).Rows(0).Item("ID_EXTERNO").ToString

        Return type
    End Function

    Private Function Obten_cartype(ByVal brokerid As Integer, ByVal cartype As Integer) As String
        Dim type As String = String.Empty
        Dim dataset As New DataSet()
        Dim objconex As New SDManejaBD.clsConexion
        Dim sql As New StringBuilder
        Dim dts As New DataSet()

        sql.AppendLine("SELECT * FROM TIPO_UNIDAD")
        sql.AppendLine("WHERE ID_BROKER =" & brokerid.ToString)
        sql.AppendLine("AND ID_TIPO = " & cartype.ToString)
        sql.AppendLine("AND ESTATUS = 2")

        dts = objconex.EjecutaQueryConsulta(sql.ToString)

        type = dts.Tables(0).Rows(0).Item("ID_EXTERNO").ToString

        Return type
    End Function

    Private Function Obten_policyType(ByVal brokerid As Integer, ByVal policyType As Integer) As String
        Dim coverageid As String = String.Empty
        Dim objconex As New SDManejaBD.clsConexion
        Dim sql As New StringBuilder
        Dim dts As New DataSet()

        sql.AppendLine("SELECT * FROM COBERTURAS")
        sql.AppendLine("WHERE ID_BROKER = " & brokerid.ToString)
        sql.AppendLine("AND ID_COBERTURA = " & policyType.ToString)
        sql.AppendLine("AND ESTATUS = 2")

        dts = objconex.EjecutaQueryConsulta(sql.ToString)

        coverageid = dts.Tables(0).Rows(0).Item("ID_EXTERNO").ToString

        Return coverageid
    End Function

    Private Function Obten_plates(ByVal brokerid As Integer, idclasif As Integer, ByVal Idmarca As Integer, ByVal Idsubmarca As Integer,
                                  ByVal Idversion As Integer, ByVal IdAnio As Integer) As DataSet

        'Dim plates As String = String.Empty
        Dim objconex As New SDManejaBD.clsConexion
        Dim sql As New StringBuilder
        Dim dts As New DataSet()

        sql.AppendLine("SELECT B.NOMBRE AS MARCA, C.DESCRIPCION AS SUBMARCA, A.ID_EXTERNO, D.DESCRIPCION")
        sql.AppendLine("FROM PRODUCTOS A")
        sql.AppendLine("INNER JOIN MARCAS B ON B.ID_MARCA = A.ID_MARCA")
        sql.AppendLine("INNER JOIN SUBMARCAS C ON C.ID_SUBMARCA = A.ID_SUBMARCA")
        sql.AppendLine("INNER JOIN VERSIONES D ON D.ID_VERSION = A.ID_VERSION")
        sql.AppendLine("WHERE A.ID_CLASIFICACION = " & idclasif.ToString)
        sql.AppendLine("AND A.ID_MARCA = " & Idmarca.ToString)
        sql.AppendLine("AND A.ID_SUBMARCA = " & Idsubmarca.ToString)
        sql.AppendLine("AND A.ID_VERSION = " & Idversion.ToString)
        sql.AppendLine("AND A.ID_ANIO = " & IdAnio.ToString)

        dts = objconex.EjecutaQueryConsulta(sql.ToString)

        'plates = dts.Tables(0).Rows(0).Item("ID_EXTERNO")

        Return dts

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

End Class
