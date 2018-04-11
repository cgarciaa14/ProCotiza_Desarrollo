'BBV-P412 RQCOT-01.2: AMR 23/02/2017: Seguros.
'BUG-PC-63:AMATA:13/05/2017:Correccion Eikos-Marsh.

Imports System.Data
Imports System.Text

Public Class clsDeserialEikos

    Private _strError As String = String.Empty

    Public Property StrError As String
        Get
            Return _strError
        End Get
        Set(value As String)
            _strError = value
        End Set
    End Property

    Public Class Eikos
        Public quote As quote = New quote()
        Public policy As policy = New policy()
    End Class

    Public Class quote
        Public complement As complement = New complement()
        Public coverageId As String = String.Empty
        Public insurerId As String = String.Empty
        Public insuredAmount As insuredAmount = New insuredAmount()
        Public prospect As prospect = New prospect()
        Public validityPeriod As validityPeriod = New validityPeriod()
        Public totalPremium As totalPremium = New totalPremium()
        Public quoteId As String = String.Empty
        Public quoteUrl As String = String.Empty
        Public iComplements As iComplements = New iComplements()
        Public receipts As New List(Of receipts)
        Public shippingCosts As shippingCosts = New shippingCosts()
        Public tax As tax = New tax()
        Public realPremium As realPremium = New realPremium()
        Public lateFee As lateFee = New lateFee()
        Public currency As Currency = New Currency()
    End Class

    Public Class insuredAmount

    End Class

    Public Class prospect
        Public legalAddress As legalAddress = New legalAddress()
        Public extendedData As extendedData = New extendedData()
    End Class

    Public Class legalAddress

    End Class

    Public Class extendedData
        Public countryOrigin As countryOrigin = New countryOrigin()
        Public homePhone As homePhone = New homePhone()
        Public mobilePhone As mobilePhone = New mobilePhone()
        Public fiscalSituation As fiscalSituation = New fiscalSituation()
        Public bussinessData As bussinessData = New bussinessData()
    End Class

    Public Class countryOrigin

    End Class

    Public Class homePhone

    End Class

    Public Class mobilePhone

    End Class

    Public Class fiscalSituation

    End Class

    Public Class bussinessData

    End Class

    Public Class totalPremium
        Public amount As Decimal = 0.00
    End Class

    Public Class iComplements
        Public ComplementEikos As ComplementEikos = New ComplementEikos()
    End Class

    Public Class ComplementEikos
        Public gapInsurance As String = String.Empty
        Public lifeInsurance As String = String.Empty
    End Class

    Public Class receipts
        Public validityPeriod As validityPeriod = New validityPeriod()
        Public shippingCosts As shippingCosts = New shippingCosts()
        Public tax As tax = New tax()
        Public realPremium As realPremium = New realPremium()
        Public totalPremium As totalPremium = New totalPremium()
        Public lateFee As lateFee = New lateFee()
        Public serialNumber As Integer
    End Class

    Public Class validityPeriod
        Public startDate As String = String.Empty
        Public endDate As String = String.Empty
    End Class

    Public Class shippingCosts
        Public shvalor As VariantType
    End Class

    Public Class tax
        Public value As value = New value()
    End Class

    Public Class value
        Public valor As VariantType
    End Class

    Public Class realPremium
        Public amount As Decimal = 0.00
    End Class

    Public Class complement
        Public user As user = New user()
        Public errorInfo As New errorInfo()
    End Class

    Public Class lateFee
        Public lavalor As VariantType
    End Class

    Public Class user
        Public credentials As credentials = New credentials()
    End Class

    Public Class credentials

    End Class
    Public Class errorInfo
        Public errorId As String
        Public description As String
    End Class

    Public Class Currency

    End Class

    Public Class policy
        Public complement As complement = New complement()
    End Class

    Sub New()
    End Sub

    Public Function Deserealize(respuesta As String, ByVal plazo As Integer, ByVal Aseguradora As String, ByVal intbroker As Integer, ByVal intRegitros As Integer) As DataTable
        Dim dtt As DataTable = New DataTable()
        Dim msjerror As String = String.Empty
        Dim strcoverage As String
        Dim noquote As Integer = 0

        dtt = New DataTable()
        dtt.Columns.Add("ID_PAQUETE")
        dtt.Columns.Add("PRIMA NETA")
        dtt.Columns.Add("RECARGO")
        dtt.Columns.Add("DERECHO")
        dtt.Columns.Add("IVA")
        dtt.Columns.Add("PRIMA TOTAL")
        dtt.Columns.Add("ASEGURADORA")
        dtt.Columns.Add("PAQUETE")
        dtt.Columns.Add("USO")
        dtt.Columns.Add("URL_COTIZACION")
        dtt.Columns.Add("PRIMA_NETA_GAP")
        dtt.Columns.Add("IVA_GAP")
        dtt.Columns.Add("SEGURO_GAP")
        dtt.Columns.Add("SEGURO_VIDA")
        dtt.Columns.Add("ID_COTIZACION")
        dtt.Columns.Add("OBSERVACIONES")
        dtt.Columns.Add("PRIMA_TOTAL_SG")

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jresult As Eikos = serializer.Deserialize(Of Eikos)(respuesta)

        If jresult.policy.complement.errorInfo.errorId <> "" Then
            If intRegitros = 1 Then
                StrError = jresult.policy.complement.errorInfo.description
                Exit Function
            End If
            noquote = 1
        End If

        msjerror = jresult.quote.complement.errorInfo.description

        If msjerror Is Nothing Then
            Dim quoteId As String = IIf(noquote = 1, 0, jresult.quote.quoteId)
            Dim quoteUrl As String = IIf(noquote = 1, "", jresult.quote.quoteUrl)
            Dim totalPremium As Decimal = IIf(noquote = 1, 0, jresult.quote.totalPremium.amount)
            Dim gapInsurance As String = IIf(noquote = 1, 0, jresult.quote.iComplements.ComplementEikos.gapInsurance)
            Dim lifeInsurance As String = IIf(noquote = 1, 0, jresult.quote.iComplements.ComplementEikos.lifeInsurance)

            If noquote = 1 Then
                strcoverage = ""
            Else
                strcoverage = Obten_Coverage(jresult.quote.coverageId, intbroker)
            End If

            Dim R As DataRow = dtt.NewRow
            R("ID_PAQUETE") = plazo
            R("PRIMA NETA") = IIf(noquote = 1, 0, Math.Truncate((CDbl(totalPremium) / 1.16) * 100) / 100)
            R("RECARGO") = 0
            R("DERECHO") = 0
            R("IVA") = IIf(noquote = 1, 0, Math.Truncate(((CDbl(totalPremium) / 1.16) * 0.16) * 100) / 100)
            R("PRIMA TOTAL") = IIf(noquote = 1, 0, totalPremium)
            R("ASEGURADORA") = Aseguradora
            R("PAQUETE") = strcoverage
            R("USO") = ""
            R("URL_COTIZACION") = quoteUrl
            R("PRIMA_NETA_GAP") = gapInsurance
            R("IVA_GAP") = gapInsurance
            R("SEGURO_GAP") = gapInsurance
            R("SEGURO_VIDA") = lifeInsurance
            R("ID_COTIZACION") = quoteId
            R("OBSERVACIONES") = ""
            R("PRIMA_TOTAL_SG") = 0

            dtt.Rows.Add(R)
            Return dtt

        Else
            _strError = msjerror
            Return Nothing
        End If
    End Function

    Public Function RecibosEikos(respuesta As String) As DataTable
        Dim dtt As New DataTable

        dtt = New DataTable()
        dtt.Columns.Add("idRequest")
        dtt.Columns.Add("startDate")
        dtt.Columns.Add("endDate")
        dtt.Columns.Add("shippingCosts")
        dtt.Columns.Add("tax")
        dtt.Columns.Add("realPremium")
        dtt.Columns.Add("totalPremium")
        dtt.Columns.Add("lateFee")
        dtt.Columns.Add("serialNumber")

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jresult As Eikos = serializer.Deserialize(Of Eikos)(respuesta)

        Dim recibos As New List(Of receipts)
        recibos = jresult.quote.receipts

        Dim msjerror As String = jresult.quote.complement.errorInfo.description

        If msjerror = "" Then

            For j As Integer = 0 To recibos.Count - 1
                Dim R As DataRow = dtt.NewRow
                R("idRequest") = jresult.quote.quoteId
                R("startDate") = recibos(j).validityPeriod.startDate
                R("endDate") = recibos(j).validityPeriod.endDate
                R("shippingCosts") = IIf(recibos(j).shippingCosts.shvalor = VariantType.Empty, 0, recibos(j).shippingCosts.shvalor)
                R("tax") = ((Math.Truncate(recibos(j).realPremium.amount * 100) / 100) / 1.16) * 0.16 'IIf(recibos(j).tax.value.valor = VariantType.Empty, 0, recibos(j).tax.value.valor)
                R("realPremium") = (Math.Truncate(recibos(j).realPremium.amount * 100) / 100) / 1.16
                R("totalPremium") = Math.Truncate(recibos(j).realPremium.amount * 100) / 100  'Math.Truncate(recibos(j).realPremium.amount * 100) / 100
                R("lateFee") = IIf(recibos(j).lateFee.lavalor = VariantType.Empty, 0, recibos(j).lateFee.lavalor)
                R("serialNumber") = recibos(j).serialNumber

                dtt.Rows.Add(R)
            Next
        End If

        Return dtt
    End Function

    Private Function Obten_Coverage(ByVal intcoverage As Integer, ByVal intbroker As Integer) As String
        Dim strpaquete As String = String.Empty
        Dim objcobertura As SDManejaBD.clsConexion = New SDManejaBD.clsConexion()
        Dim dts As New DataSet()
        Dim strsql As New StringBuilder

        strsql.AppendLine("SELECT ID_COBERTURA, NOMBRE, ID_BROKER, ID_EXTERNO, ESTATUS")
        strsql.AppendLine("FROM COBERTURAS")
        strsql.AppendLine("WHERE ID_BROKER = " & intbroker)
        strsql.AppendLine("AND ESTATUS = 2")
        strsql.AppendLine("AND ID_EXTERNO = " & intcoverage)


        dts = objcobertura.EjecutaQueryConsulta(strsql.ToString)

        strpaquete = dts.Tables(0).Rows(0).Item("NOMBRE")

        Return strpaquete

    End Function
End Class
