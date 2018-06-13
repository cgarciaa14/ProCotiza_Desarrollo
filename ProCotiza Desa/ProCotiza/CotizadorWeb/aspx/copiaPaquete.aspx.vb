'BBVA-P-412: 06-07-2016: AMR: RQ02,RQ03,RQ04,RQ05,RQ06,RQ08 SE AGREGA FUNCIONALIDAD
'BUG-PC-24 MAUT 14/12/2016 Se agrega el WS a la copia del paquete
'BBV-P-412:RQ LOGIN GVARGAS: 23/12/2016 Actualizacion REST Service iv_ticket & userID
'BUG-PC-39 JRHM 25/01/17 Se corrigen varios errores
'BUG-PC-53 AMR 21/04/2017 Correcciones Multicotiza.
'BUG-PC-56:AMATA:28/04/2017:Correccion Paquete Multicoza
'BUG-PC-64: RHERNANDEZ: 18/05/17: Se cambio packageId de payload para nueve digitos con ceros a la izquierda
'RQ-PC7: CGARCIA: 02/04/2018: SE MODIFICA EL PAYLOAD DEL WS
Imports System.Data
Imports SNProcotiza
Imports WCF

Partial Class aspx_copiaPaquete
    Inherits System.Web.UI.Page
    Dim strErr As String = String.Empty
    Dim objPaq As New clsPaquetes

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()

        If Not IsPostBack Then
            If Val(Request("paqId")) > 0 Then
                CargaInfo()
            End If
        End If
    End Sub

    Public Sub LimpiaError()
        lblMensaje.Text = String.Empty
        lblScript.Text = String.Empty
    End Sub

    Public Sub MensajeError(ByVal strMsj As String)
        strMsj = Replace(strMsj, "'", "")
        strMsj = Replace(strMsj, """", "")
        strMsj = Replace(strMsj, "(", "")
        strMsj = Replace(strMsj, ")", "")
        lblMensaje.Text = "<script>alert('" & strMsj & "')</script>"
    End Sub

    Public Sub CierraPantalla(ByVal strPant As String)
        lblMensaje.Text += "<script>alert('El registro se guardó con éxito'); document.location.href='" & strPant & "';</script>"
    End Sub

    Private Sub CargaInfo()
        Try
            Dim dtsRes As New DataSet
            Dim intPaq As Integer = Val(Request("paqId"))
            Dim objPaq As New SNProcotiza.clsPaquetes

            objPaq.IDPaquete = intPaq
            dtsRes = objPaq.ManejaPaquete(1)
            If objPaq.ErrorPaquete = "" Then
                If dtsRes.Tables.Count > 0 Then
                    If dtsRes.Tables(0).Rows.Count > 0 Then
                        lblId.Text = dtsRes.Tables(0).Rows(0).Item("ID_PAQUETE")
                        lblNom.Text = dtsRes.Tables(0).Rows(0).Item("NOMBRE")
                        'lblEmp.Text = dtsRes.Tables(0).Rows(0).Item("EMPRESA")
                        'lblMoneda.Text = dtsRes.Tables(0).Rows(0).Item("MONEDA")
                    Else
                        MensajeError("No se localizó información para el paquete.")
                    End If
                Else
                    MensajeError("No se localizó información para el paquete.")
                End If
            Else
                MensajeError(objPaq.ErrorPaquete)
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub
    Private Function ValidaCampos() As Boolean
        ValidaCampos = False
        If Trim(txtNom.Text) = "" Then
            strErr = "Todos los campos marcados con * son obligatorios."
            Exit Function
        End If

        If Trim(lblNom.Text) = Trim(txtNom.Text) Then
            strErr = "El nombre del paquete no puede ser igual."
            Exit Function
        End If
        ValidaCampos = True
    End Function

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then
                Dim intPaq As Integer = Val(Request("paqId"))
                'guardamos la info de la copia
                objPaq.IDPaquete = intPaq
                objPaq.NombreCopia = Trim$(txtNom.Text)

                If MandaWS(intPaq) Then
                objPaq.ManejaPaquete(8)
                    Else
                        MensajeError("Error al copiar el paquete WS - " & strErr)
                        Exit Sub
                    End If

                If objPaq.ErrorPaquete = "" Then
                    CierraPantalla("./consultaPaquetes.aspx")
                Else
                    MensajeError(objPaq.ErrorPaquete)
                End If

            Else
                MensajeError(strErr)
                Exit Sub
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
            Exit Sub
        End Try

    End Sub
	'BBVA-P-412
    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaPaquetes.aspx")
    End Sub

    ''BUG-PC-24 14-12-2016 MAUT
    Private Function MandaWS(PaqId As Integer) As Boolean

        MandaWS = False

        Dim ddlprod As Integer
        Dim maxPlazo As Integer
        Dim minPlazo As Integer
        Dim ddlsubprod As Integer
        Dim cmbTipoVenc As Integer
        Dim cmbPeriodicidad As Integer
        Dim maxtasa As Double
        Dim txtIniVig As String
        Dim txtFinVig As String
        Dim txtimpmaxg As String
        Dim txtimpming As String

        Dim dataset As New DataSet

        Try
            'Recupera datos del paquete que se va a copiar
            objPaq.CargaPaquete(PaqId)

            ddlsubprod = objPaq.IDSubProdUG
            cmbTipoVenc = objPaq.IDTipoVencimiento
            txtimpmaxg = objPaq.ImporteMaxG
            txtimpming = objPaq.ImporteMinG
            txtIniVig = objPaq.InicioVigencia
            cmbPeriodicidad = objPaq.IDPeriodicidad
            ddlprod = objPaq.IDProdUG
            txtFinVig = objPaq.FinVigencia

            dataset = objPaq.ManejaPaquete(34)
            maxtasa = dataset.Tables(1).Rows(0).Item("maxtasa")
            maxPlazo = dataset.Tables(1).Rows(0).Item("maxplazo")
            minPlazo = dataset.Tables(1).Rows(0).Item("minplazo")
            'RQ-PC7: CGARCIA: 02/04/2018: SE MODIFICA EL PAYLOAD DEL WS
            Dim strTasaNominal As String
            Dim strPorcentajeNominal As String
            For index As Integer = 0 To dataset.Tables(0).Rows.Count - 1 Step 1
                If maxPlazo = CInt(dataset.Tables(0).Rows(index).Item("PLAZO").ToString) Then
                    strTasaNominal = CStr(dataset.Tables(0).Rows(index).Item("TASA_NOMINAL_DOS").ToString)
                    strPorcentajeNominal = CStr(dataset.Tables(0).Rows(index).Item("PTJ_SERV_FINAN_DOS").ToString)
                End If

            Next

            Dim newId As Integer = 0
            Dim dtsCopy As New DataSet
            'No se agrega la opcion 51 a ManejaPaquetes ya que no necesita parametros
            dtsCopy = objPaq.ManejaPaquete(51)
            If dtsCopy.Tables.Count > 0 AndAlso dtsCopy.Tables(0).Rows.Count > 0 Then
                newId = dtsCopy.Tables(0).Rows(0).Item("ID_PAQUETE")
            End If
            'RQ-PC7: CGARCIA: 02/04/2018: SE MODIFICA EL PAYLOAD DEL WS
            'manda los datos del paquete al WS
            If ConsulWS(newId, ddlsubprod, cmbTipoVenc, txtimpmaxg, txtimpming, txtIniVig, cmbPeriodicidad, ddlprod, txtFinVig, maxPlazo, minPlazo, maxtasa, strTasaNominal, strPorcentajeNominal) Then
                MandaWS = True
            End If

        Catch ex As Exception
            MandaWS = False
            strErr = "Error al copiar el paquete"
        End Try

    End Function

    Private Function ConsulWS(PaqId As Integer, ddlsubprod As Integer, cmbTipoVenc As Integer, txtimpmaxg As String,
                              txtimpming As String, txtIniVig As String, cmbPeriodicidad As Integer, ddlprod As Integer,
                              txtFinVig As String, maxPlazo As Integer, minPlazo As Integer, maxtasa As Double, strTasaNominal As String, strPorcentajeNominal As String) As Boolean
        ConsulWS = False

        Dim dts As DataSet = New DataSet()
        Dim produg As SNProcotiza.clsSubProductosUG = New SNProcotiza.clsSubProductosUG()
        produg.CargaSubProductoUG(ddlsubprod)

        Dim loanBASE As loanBASE = New loanBASE()

        Dim objpaqws As SNProcotiza.clsPaquetes = New SNProcotiza.clsPaquetes()
        dts = objpaqws.consultaWS(1)
        If objpaqws.ErrorPaquete <> "" Then
            strErr = "Eror al cargar información."
            Exit Function
        End If

        If objpaqws.ErrorPaquete = "" Then
            If dts.Tables(0).Rows.Count > 0 Then
                loanBASE.loan.loanProduct.extendedData.planType.id = "00002"
                loanBASE.loan.loanProduct.extendedData.operationType.id = "TR"
                loanBASE.loan.loanProduct.extendedData.money.currency = "0001"
                loanBASE.loan.loanProduct.extendedData.paymentType.id = ObtenValor(dts, cmbTipoVenc, 5, 0)
                loanBASE.loan.loanProduct.extendedData.maximumAmount.amount = formatsendws(txtimpmaxg.Trim, 15, 1)
                loanBASE.loan.loanProduct.extendedData.minimumCapital.amount = formatsendws(txtimpming.Trim, 15, 1)
                loanBASE.loan.loanProduct.extendedData.maximumTerm = formatsendws(maxPlazo, 4, 0) ''"0072"
                loanBASE.loan.loanProduct.extendedData.minimumTerm = formatsendws(minPlazo, 4, 0)
                loanBASE.loan.loanProduct.extendedData.gracePeriodUnit = "000"
                loanBASE.loan.dueDate = txtIniVig
                loanBASE.loan.period.timeUnit.name = IIf(cmbPeriodicidad = 83, "MEN", IIf(cmbPeriodicidad = 95, "QUI", "SEM"))
                loanBASE.productCode = ddlprod
                loanBASE.subProductCode = produg.Nombre
                loanBASE.startLoanDate = txtFinVig & " 00:00:00:000000"

                Dim l1 As listDtoRate = New listDtoRate()
                l1.type.name = "real"
                l1.percentage = formatsendws(maxtasa, 7, 1, 1)
                loanBASE.iLoanDetail.loanCar.listDtoRate.Add(l1)

                Dim l2 As listDtoRate = New listDtoRate()
                l2.type.name = "mora"
                l2.percentage = "0000000"
                loanBASE.iLoanDetail.loanCar.listDtoRate.Add(l2)

                loanBASE.iLoanDetail.loanCar.packageId = New String("0"c, 9 - Len(PaqId.ToString)) & PaqId.ToString 'PaqId
                loanBASE.iLoanDetail.loanCar.packageDescription = txtNom.Text.Trim()
                'RQ-PC7: CGARCIA: 02/04/2018: SE MODIFICA EL PAYLOAD DEL WS
                Dim rate As New rate()

                rate.percentage = strTasaNominal
                loanBASE.refinancing.rate.Add(rate)
                rate = New rate
                rate.percentage = strPorcentajeNominal
                loanBASE.refinancing.rate.Add(rate)

                Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                Dim jsonBODY As String = serializer.Serialize(loanBASE)

                Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
                Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
                Dim restGT As RESTful = New RESTful()
                restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("url") & System.Configuration.ConfigurationManager.AppSettings.Item("metodo")

                restGT.buscarHeader("ResponseWarningDescription")

                Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)

                Dim str As String = restGT.valorHeader

                Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()
                Dim alert = srrSerialer.Deserialize(Of msjerr)(jsonResult)

                If restGT.IsError Then
                    strErr = IIf(restGT.StatusHTTP = 500, "Error al consultar Servicio Web: ", "Mensaje del Servicio Web: ") & restGT.MensajeError & ". Estatus:" & restGT.StatusHTTP ''& "Estatus:" & restGT.StatusHTTP & ". Descripción: "  & "."
                Else
                    ConsulWS = True
                End If
            Else
                strErr = "No se encontro información."
                Exit Function
            End If
        Else
            strErr = "Error al consultar la información."
            Exit Function
        End If

        Return ConsulWS
    End Function

    Private Function ObtenValor(dts As DataSet, valor As Integer, lng As Integer, isDec As Integer) As String
        Dim strdato As String = String.Empty

        Dim renglones() As DataRow

        renglones = dts.Tables(0).Select("IDINTERNO = " & valor.ToString)

        For Each dato As DataRow In renglones
            strdato = dato("IDEXTERNO").ToString
        Next

        strdato = formatsendws(strdato, lng, isDec)

        Return strdato
    End Function

    Private Function formatsendws(valor As String, lng As Integer, isDec As Integer, Optional ispercent As Integer = 0) As String
        Dim strresult As String = String.Empty
        Dim Pos As Integer = 0

        Select Case ispercent
            Case 0
                If isDec = 1 Then
                    Pos = InStr(valor, ".")
                    If Pos > 0 Then
                        strresult = ((valor).Substring(0, Pos) & (valor).Substring(Pos, 2)).Replace(".", "")
                    Else
                        strresult = valor & "00"
                    End If
                    strresult = New String("0"c, lng - Len(strresult)) & strresult
                Else
                    strresult = New String("0"c, lng - Len(valor)) & valor
                End If
            Case 1
                Pos = InStr(valor, ".")
                If Pos > 0 Then
                    Dim strdec As String = valor.Substring(Pos, valor.Length - Pos)
                    If strdec.Length < 4 Then
                        strdec = strdec & New String("0"c, 4 - Len(strdec))
                    End If

                    Dim strent As String = valor.Substring(0, Pos - 1)
                    strresult = New String("0"c, lng - Len(strent & strdec)) & (strent & strdec)
                Else
                    strresult = valor & "0000"
                    strresult = New String("0"c, lng - Len(strresult)) & strresult
                End If
        End Select

        Return strresult
    End Function

End Class

Public Class loanBASE
    Public productCode As String
    Public subProductCode As String
    Public startLoanDate As String
    Public loan As loan = New loan()
    Public iLoanDetail As iLoanDetail = New iLoanDetail()
    Public refinancing As refinancing = New refinancing()
End Class

Public Class loan
    Public loanProduct As loanProduct = New loanProduct()
    Public dueDate As String
    Public period As period = New period
End Class

Public Class loanProduct
    Public extendedData As extendedData = New extendedData()
End Class

Public Class extendedData
    Public planType As planType = New planType()
    Public operationType As operationType = New operationType()
    Public money As money = New money()
    Public paymentType As paymentType = New paymentType()
    Public maximumAmount As maximumAmount = New maximumAmount()
    Public minimumCapital As minimumCapital = New minimumCapital()
    Public maximumTerm As String
    Public minimumTerm As String
    Public gracePeriodUnit As String
End Class

Public Class planType
    Public id As String
End Class

Public Class operationType
    Public id As String
End Class
Public Class money
    Public currency As String
End Class

Public Class paymentType
    Public id As String
End Class

Public Class maximumAmount
    Public amount As String
End Class

Public Class minimumCapital
    Public amount As String
End Class

Public Class period
    Public timeUnit As timeUnit = New timeUnit
End Class

Public Class timeUnit
    Public name As String
End Class

Public Class iLoanDetail
    Public loanCar As loanCar = New loanCar()
End Class

Public Class loanCar
    Public listDtoRate As List(Of listDtoRate) = New List(Of listDtoRate)
    Public packageId As String
    Public packageDescription As String
End Class

Public Class listDtoRate
    Public type As Tipe = New Tipe()
    Public percentage As String
End Class

Public Class Tipe
    Public name As String
End Class

Public Class msjerr
    Public message As String
    Public status As String
End Class

Public Class refinancing
    Public rate As List(Of rate) = New List(Of rate)
End Class

Public Class rate
    Public percentage As String = String.Empty
End Class
