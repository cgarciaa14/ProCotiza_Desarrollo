'BBV-P412 RQCOT-01.2: AMR 23/02/2017: Seguros.
'BUG-PC-63:AMATA:13/05/2017:Correccion Eikos-Marsh.

Imports Newtonsoft.Json

Public Class clsDeserialMarsh

    Private _strError As String = String.Empty

    Public Property StrError As String
        Get
            Return _strError
        End Get
        Set(value As String)
            _strError = value
        End Set
    End Property

    Public Class Marsh
        Public quote As quote = New quote()
    End Class


    Public Class quote
        Public complement As complement = New complement()
        Public insurerId As String
        Public insuredAmount As insuredAmount = New insuredAmount()
        Public prospect As prospect = New prospect()
        Public validityPeriod As validityPeriod = New validityPeriod()
        Public totalPremium As totalPremium = New totalPremium()
        Public iComplements As iComplements = New iComplements()
        Public receipts As New List(Of receipts)
        Public serviceTypeId As String
        Public dateRequest As String
        Public shippingCosts As shippingCosts = New shippingCosts()
        Public tax As tax = New tax()
        Public realPremium As realPremium = New realPremium()
        Public lateFee As lateFee = New lateFee()
        Public idRequest As String
        Public policyTypeId As String
        Public totalNumberReceipts As String
        Public currency As currency = New currency()
    End Class


    Public Class complement
        Public user As user = New user()
        Public errorInfo As errorInfo = New errorInfo()
    End Class

    Public Class user
        Public credentials As credentials = New credentials()
    End Class

    Public Class credentials

    End Class

    Public Class errorInfo

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

    Public Class validityPeriod
        Public startDate As String
        Public endDate As String
    End Class

    Public Class totalPremium
        Public amount As Decimal
    End Class

    Public Class iComplements
        Public EmissionComplementMarsh As EmissionComplementMarsh = New EmissionComplementMarsh()
    End Class

    Public Class EmissionComplementMarsh
        Public eserror As eserror = New eserror()
        Public reference1 As String
        Public reference2 As String
    End Class

    Public Class eserror
        Public errorId As String
        Public description As String
    End Class

    Public Class receipts
        Public validityPeriod As validityPeriod = New validityPeriod()
        Public shippingCosts As shippingCosts = New shippingCosts()
        Public tax As tax = New tax()
        Public realPremium As realPremium = New realPremium()
        Public totalPremium As totalPremium = New totalPremium()
        Public lateFee As lateFee = New lateFee()
        Public serialNumber As String
    End Class

    Public Class shippingCosts
        Public amount As Decimal
    End Class

    Public Class tax
        Public value As value = New value()
        Public amount As Decimal
    End Class

    Public Class value
        Public amount As Decimal
    End Class

    Public Class realPremium
        Public amount As Decimal
    End Class

    Public Class lateFee
        Public amount As Decimal
    End Class

    Public Class currency

    End Class

    Sub New()
    End Sub

    Public Function Deserealize(respuesta As String, ByVal Aseguradora As String, ByVal term As Integer) As DataTable
        Dim dtt As DataTable = New DataTable()

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

        respuesta = Replace(respuesta, "error", "eserror")
        respuesta = Replace(respuesta, "eserrorInfo", "errorInfo")
        respuesta = Replace(respuesta, "eserrorId", "errorId")

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jresult As Marsh = serializer.Deserialize(Of Marsh)(respuesta)

        Dim msjerror As String = jresult.quote.iComplements.EmissionComplementMarsh.eserror.description

        If msjerror = "" Then

            Dim R As DataRow = dtt.NewRow
            R("ID_PAQUETE") = term.ToString
            R("PRIMA NETA") = Math.Truncate(jresult.quote.realPremium.amount * 100) / 100
            R("RECARGO") = 0
            R("DERECHO") = Math.Truncate(jresult.quote.shippingCosts.amount * 100) / 100
            R("IVA") = Math.Truncate(jresult.quote.tax.amount * 100) / 100
            R("PRIMA TOTAL") = Math.Truncate(jresult.quote.totalPremium.amount * 100) / 100
            R("ASEGURADORA") = Aseguradora
            R("PAQUETE") = jresult.quote.policyTypeId
            R("USO") = ""
            R("URL_COTIZACION") = ""
            R("PRIMA_NETA_GAP") = ""
            R("IVA_GAP") = ""
            R("SEGURO_GAP") = ""
            R("SEGURO_VIDA") = ""
            R("ID_COTIZACION") = jresult.quote.idRequest
            R("OBSERVACIONES") = ""
            R("PRIMA_TOTAL_SG") = 0

            dtt.Rows.Add(R)
            Return dtt

        Else
            Dim R As DataRow = dtt.NewRow
            R("ID_PAQUETE") = term.ToString
            R("PRIMA NETA") = 0.00
            R("RECARGO") = 0
            R("DERECHO") = 0.00
            R("IVA") = 0.00
            R("PRIMA TOTAL") = 0.00
            R("ASEGURADORA") = ""
            R("PAQUETE") = ""
            R("USO") = ""
            R("URL_COTIZACION") = ""
            R("PRIMA_NETA_GAP") = ""
            R("IVA_GAP") = ""
            R("SEGURO_GAP") = ""
            R("SEGURO_VIDA") = ""
            R("ID_COTIZACION") = 0
            R("OBSERVACIONES") = ""
            R("PRIMA_TOTAL_SG") = 0

            dtt.Rows.Add(R)
        End If

        Return dtt
    End Function

    Public Function RecibosMarsh(respuesta As String) As DataTable
        Dim dtt As New DataTable
        respuesta = Replace(respuesta, "error", "eserror")
        respuesta = Replace(respuesta, "eserrorInfo", "errorInfo")
        respuesta = Replace(respuesta, "eserrorId", "errorId")

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
        Dim jresult As Marsh = serializer.Deserialize(Of Marsh)(respuesta)

        Dim recibos As New List(Of receipts)
        recibos = jresult.quote.receipts

        Dim msjerror As String = jresult.quote.iComplements.EmissionComplementMarsh.eserror.description

        If msjerror = "" Then

            For j As Integer = 0 To recibos.Count - 1
                Dim R As DataRow = dtt.NewRow
                R("idRequest") = jresult.quote.idRequest
                R("startDate") = recibos(j).validityPeriod.startDate
                R("endDate") = recibos(j).validityPeriod.endDate
                R("shippingCosts") = Math.Truncate(recibos(j).shippingCosts.amount * 100) / 100
                R("tax") = Math.Truncate(recibos(j).tax.value.amount * 100) / 100
                R("realPremium") = Math.Truncate(recibos(j).realPremium.amount * 100) / 100
                R("totalPremium") = Math.Truncate(recibos(j).totalPremium.amount * 100) / 100
                R("lateFee") = Math.Truncate(recibos(j).lateFee.amount * 100) / 100
                R("serialNumber") = recibos(j).serialNumber

                dtt.Rows.Add(R)
            Next
        End If

        Return dtt
    End Function
End Class

