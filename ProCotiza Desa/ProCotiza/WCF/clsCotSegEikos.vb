'BBV-P412 RQCOT-01.2: AMR 23/02/2017: Seguros.
'BUG-PC-52 AMR 21/04/2017 Cambios Eikos y Marsh.
'BUG-PC-81: RHERNANDEZ: 29/06/17: SE EVALUA EL TIPO DE SEGURO PARA CONTADO Y FINANCIADO
'AUTOMIK-TASK-310:RHERNANDEZ:22/11/2017:Implementacion servicio EIKOS a calculadora
'AUTOMIK-BUG-453: RHERNANDEZ: 17/05/18: SE MODIFICA SERVICIO DE CALCULO DE SEGUROS PARA COTIZAR UN SOLO PLAZO
Imports System.Text
Imports System.Net

Public Class clsCotSegEikos

    Private _strError As String = String.Empty
    Private _IDBroker As Integer = 0
    Private _URL As String = String.Empty
    Private _userID As String = String.Empty
    Private _password As String = String.Empty
    Private _BrokerABA As Integer

#Region "Propiedades"

    Public ReadOnly Property urlEIKOS As String
        Get
            Dim urlService = System.Configuration.ConfigurationManager.AppSettings.Item("ulrEikos")

            Dim oldString = urlService.Substring(urlService.IndexOf("@@"), urlService.IndexOf("*") - urlService.IndexOf("@@"))
            Dim newString = System.Configuration.ConfigurationManager.AppSettings.Item(oldString.Replace("@@", ""))
            urlService = urlService.Replace(oldString & "*", newString)

            oldString = urlService.Substring(urlService.IndexOf("@@"), urlService.IndexOf("*") - urlService.IndexOf("@@"))
            newString = System.Configuration.ConfigurationManager.AppSettings.Item(oldString.Replace("@@", ""))
            urlService = urlService.Replace(oldString & "*", newString)

            Return urlService
        End Get
    End Property

    Public Property BrokerABA As Integer
        Get
            Return _BrokerABA
        End Get
        Set(value As Integer)
            _BrokerABA = value
        End Set
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
#End Region

#Region "payload"
    Public Class JSON
        Public quote As quote = New quote()
    End Class

    Public Class quote
        Public complement As complement = New complement()
        Public iQuote As iQuote = New iQuote()
        Public coverageId As String = String.Empty
        Public idPaymentType As String = String.Empty
        Public termId As String = String.Empty
        Public agencyNumber As String = String.Empty
        Public insurerId As String = String.Empty
        Public prospect As prospect = New prospect()
        Public insuredAmount As insuredAmount = New insuredAmount()
        Public iComplements As iComplements = New iComplements()
        Public credit As credit = New credit()
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
        Public downPayment As downPayment = New downPayment()
        Public specialEquipmentSum As specialEquipmentSum = New specialEquipmentSum()
        Public accessorySum As accessorySum = New accessorySum()
    End Class

    Public Class car
        Public model As String = String.Empty
        Public type As String = String.Empty
        Public useType As String = String.Empty
        Public description As String = String.Empty
        Public unitType As String = String.Empty
        Public stateId As String = String.Empty
    End Class

    Public Class downPayment
        Public amount As Decimal = 0
    End Class

    Public Class specialEquipmentSum
        Public amount As Decimal = 0
    End Class

    Public Class accessorySum
        Public amount As Decimal = 0
    End Class

    Public Class prospect
        Public name As String = String.Empty
        Public middleName As String = String.Empty
        Public lastName As String = String.Empty
        Public mothersLastName As String = String.Empty
        Public legalAddress As legalAddress = New legalAddress()
    End Class

    Public Class legalAddress
        Public zipCode As String = String.Empty
    End Class

    Public Class insuredAmount
        Public amount As Decimal = 0.0
    End Class

    Public Class iComplements
        Public ComplementEikos As ComplementEikos = New ComplementEikos()
    End Class

    Public Class ComplementEikos
        Public employeedId As String = String.Empty
    End Class

    Public Class credit
        Public idFinancingTerm As String = String.Empty
    End Class

    Public Class msjerr
        Public message As String
        Public status As String
    End Class
#End Region

    Sub New()

    End Sub

    Public Function CotizaEikos(ByVal brokerid As Integer, ByVal carmodel As Integer, ByVal cartype As Integer, ByVal caruseType As Integer, ByVal description As String,
                                ByVal unitType As Integer, ByVal carstateId As Integer, ByVal downPaymentamount As Decimal, ByVal accessorySum As Decimal, ByVal insuredAmountamount As Decimal,
                                ByVal coverageId As Integer, ByVal insurerId As Integer, ByVal agencyNumber As Integer, ByVal zipCode As String, ByVal PaqueteId As Integer, ByVal iduser As Integer,
                                ByVal name As String, ByVal middleName As String, ByVal lastName As String, ByVal mothersLastName As String, ByVal SegType As Integer,
                                Optional ByVal automikRequest As Boolean = 0, Optional headers As WebHeaderCollection = Nothing, Optional IsMulticotizacion As Integer = 1, Optional idplazo As Integer = 0) As DataSet

        Dim dts As New DataSet()
        Dim json As New JSON()
        Dim IDQuote As New DataTable()
        Dim dttrecibos As New DataTable()
        Dim resultEikos As New DataSet()
        Dim dtsaseg As New DataSet()

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
            Dim typecar As String = Obten_cartype(brokerid, cartype)
            Dim useType As String = Obten_caruseType(brokerid, caruseType)
            dtsaseg = Obten_Aseguradora(brokerid, insurerId)

            Dim restGT As RESTful = New RESTful()
            restGT.automikRequest = automikRequest
            Dim jsonResult As String
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim objDes As clsDeserialEikos = New clsDeserialEikos()
            Dim totreg As Integer = 0

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
                    totreg = dts.Tables(0).Rows.Count
                    For i As Integer = 0 To dts.Tables(0).Rows.Count - 1
                        json.quote.complement.user.id = System.Configuration.ConfigurationManager.AppSettings.Item("useridEikos")
                        json.quote.complement.user.credentials.accessPassword = System.Configuration.ConfigurationManager.AppSettings.Item("passwordEikos")
                        json.quote.iQuote.VehicleQuote.car.model = carmodel.ToString '"2017"
                        json.quote.iQuote.VehicleQuote.car.type = typecar '"1"
                        json.quote.iQuote.VehicleQuote.car.useType = useType '"1"
                        json.quote.iQuote.VehicleQuote.car.description = description ' "5364" ''ID_EXTERNO
                        json.quote.iQuote.VehicleQuote.car.unitType = IIf(unitType = 63, "1", "0") '"0" ''NUEVO/SEMINUEVO
                        json.quote.iQuote.VehicleQuote.car.stateId = carstateId.ToString '' "8" 
                        json.quote.iQuote.VehicleQuote.downPayment.amount = downPaymentamount.ToString ''Enganche
                        json.quote.iQuote.VehicleQuote.specialEquipmentSum.amount = 0
                        json.quote.iQuote.VehicleQuote.accessorySum.amount = accessorySum '0 
                        json.quote.coverageId = Obten_coverageId(brokerid, coverageId) '"2" ''Cobertura
                        json.quote.idPaymentType = "1" ''Contado / Financiado
                        json.quote.termId = IIf(SegType = 30 Or SegType = 31, "12", dts.Tables(0).Rows(i).Item("VALOR").ToString) ' "36" ''termId ''Plazo
                        json.quote.agencyNumber = agencyNumber.ToString '"3793" ''ID_Agencia
                        json.quote.insurerId = dtsaseg.Tables(0).Rows(0).Item("IDEXTERNO").ToString '"101" ''ID_Aseguradora
                        json.quote.prospect.name = name ''"JOSE"
                        json.quote.prospect.middleName = middleName
                        json.quote.prospect.lastName = lastName ''"GUZMAN"
                        json.quote.prospect.mothersLastName = mothersLastName ''"ROBLES"
                        json.quote.prospect.legalAddress.zipCode = zipCode ''zipCode ''"52765"
                        json.quote.insuredAmount.amount = insuredAmountamount.ToString ''184450.0
                        json.quote.iComplements.ComplementEikos.employeedId = iduser.ToString ''"38287021"
                        json.quote.credit.idFinancingTerm = IIf(SegType = 30 Or SegType = 31, "12", dts.Tables(0).Rows(i).Item("VALOR").ToString) '"36"

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

                        restGT.Uri = _URL
                        If Not automikRequest Then
                            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("ulrEikos")
                        Else
                            restGT.Uri = urlEIKOS
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


                        IDQuote = objDes.Deserealize(jsonResult, dts.Tables(0).Rows(i).Item("VALOR").ToString, dtsaseg.Tables(0).Rows(0).Item("DESCEXTERNO"), brokerid, totreg)

                        'If objDes.StrError <> "" Then
                        '    _strError = objDes.StrError
                        '    Return Nothing
                        '    Exit Function
                        'End If

                        dttrecibos = objDes.RecibosEikos(jsonResult)

                        For Each row As DataRow In IDQuote.Rows
                            dtb.ImportRow(row)
                        Next

                        For Each row2 As DataRow In dttrecibos.Rows
                            dtrec.ImportRow(row2)
                        Next
                    Next

                    resultEikos.Tables.Add(dtb)
                    resultEikos.Tables.Add(dtrec)

                End If
            End If

            resultEikos.Tables(0).TableName = "Result"
            resultEikos.Tables(1).TableName = "Recibos"

            Return resultEikos

        Catch ex As Exception
            _strError = ex.Message
            Return Nothing
        End Try
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

    Private Function Obten_caruseType(ByVal brokerid As Integer, ByVal useType As Integer) As String
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

        coverageid = dts.Tables(0).Rows(0).Item("ID_EXTERNO").ToString

        Return coverageid
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

End Class
