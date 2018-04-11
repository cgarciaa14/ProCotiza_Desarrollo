'BUG-PC-58:AMATA:03/05/2017:Seguros Ordas

Imports Newtonsoft.Json
Imports System.Text

Public Class clsDeserialOrdas
    Private _strError As String = String.Empty

    Public Property StrError As String
        Get
            Return _strError
        End Get
        Set(value As String)
            _strError = value
        End Set
    End Property

    Public Class Ordas
        Public quote As quote = New quote()
    End Class

    Public Class quote
        Public complement As complement = New complement()
        Public insuredAmount As insuredAmount
        Public prospect As prospect = New prospect()
        Public validityPeriod As validityPeriod
        Public totalPremium As totalPremium
        Public receipts As New List(Of receipts)
        Public shippingCosts As shippingCosts
        Public tax As tax
        Public realPremium As realPremium
        Public lateFee As lateFee
        Public currency As currency
    End Class

    Public Class complement
        Public user As user = New user()
        Public errorInfo As errorInfo = New errorInfo()
    End Class

    Public Class user
        Public credentials As credentials
    End Class

    Public Class credentials

    End Class

    Public Class errorInfo
        Public description As String = String.Empty
    End Class

    Public Class insuredAmount

    End Class

    Public Class prospect
        Public legalAddress As legalAddress
        Public extendedData As extendedData = New extendedData()
        Public nationality As nationality
    End Class

    Public Class legalAddress

    End Class

    Public Class extendedData
        Public countryOrigin As countryOrigin
        Public homePhone As homePhone
        Public mobilePhone As mobilePhone
        Public fiscalSituation As fiscalSituation
    End Class

    Public Class countryOrigin

    End Class

    Public Class homePhone

    End Class

    Public Class mobilePhone

    End Class

    Public Class fiscalSituation

    End Class

    Public Class nationality

    End Class

    Public Class validityPeriod

    End Class

    Public Class totalPremium
        Public amount As Double = 0
    End Class

    Public Class receipts
        Public validityPeriod As validityPeriod
        Public shippingCosts As shippingCosts = New shippingCosts()
        Public tax As tax = New tax()
        Public realPremium As realPremium = New realPremium()
        Public totalPremium As totalPremium = New totalPremium()
        Public lateFee As lateFee = New lateFee()
        Public serialNumber As String = String.Empty
        Public idRequest As String = String.Empty
        Public insurerId As String = String.Empty
        Public idPack As String = String.Empty
        Public totalNumberReceipts As String = String.Empty
    End Class

    Public Class shippingCosts
        Public amount As Double = 0
    End Class

    Public Class tax
        Public value As value = New value()
    End Class

    Public Class value
        Public amount As Double = 0
    End Class

    Public Class realPremium
        Public amount As Double = 0
    End Class

    Public Class lateFee
        Public amount As Double = 0
    End Class

    Public Class currency

    End Class

    Sub New()
    End Sub

    Public Function Deserealize(respuesta As String, ByVal term As Integer, ByVal idPaymentType As Integer) As DataTable
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
        dtt.Columns.Add("INSURERID")

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jresult As Ordas = serializer.Deserialize(Of Ordas)(respuesta)

        Dim msjerror As String = jresult.quote.complement.errorInfo.description

        Dim recibos As New List(Of receipts)
        recibos = jresult.quote.receipts

        If msjerror = "OK" Then
            Dim i As Integer = 0
            Dim sumprimaneta As Double = 0
            Dim sumprimatotal As Double = 0
            Dim sumderecho As Double = 0
            Dim sumiva As Double = 0


            For j As Integer = 0 To recibos.Count - 1
                i += 1
                Dim strAseguradora As String = Obteninsurer(recibos(j).insurerId)
                If strAseguradora <> "" Then
                    Dim R As DataRow = dtt.NewRow
                    R("ID_PAQUETE") = term.ToString
                    R("PRIMA NETA") = Math.Truncate(recibos(j).realPremium.amount * 100) / 100
                    R("RECARGO") = 0
                    R("DERECHO") = Math.Truncate(recibos(j).shippingCosts.amount * 100) / 100
                    R("IVA") = Math.Truncate(recibos(j).tax.value.amount * 100) / 100
                    R("PRIMA TOTAL") = Math.Truncate(recibos(j).totalPremium.amount * 100) / 100
                    R("ASEGURADORA") = Obteninsurer(recibos(j).insurerId)
                    R("PAQUETE") = ObtenPack(recibos(j).idPack)
                    R("USO") = ""
                    R("URL_COTIZACION") = ""
                    R("PRIMA_NETA_GAP") = ""
                    R("IVA_GAP") = ""
                    R("SEGURO_GAP") = ""
                    R("SEGURO_VIDA") = ""
                    R("ID_COTIZACION") = recibos(j).idRequest
                    R("OBSERVACIONES") = ""
                    R("PRIMA_TOTAL_SG") = 0
                    R("INSURERID") = recibos(j).insurerId

                    If idPaymentType = 1 Then
                        If i = recibos(j).totalNumberReceipts Then
                            sumprimaneta = sumprimaneta + recibos(j).realPremium.amount
                            sumprimatotal = sumprimatotal + recibos(j).totalPremium.amount
                            sumderecho = sumderecho + recibos(j).shippingCosts.amount
                            sumiva = sumiva + recibos(j).tax.value.amount

                            R("PRIMA NETA") = Math.Truncate(sumprimaneta * 100) / 100
                            R("PRIMA TOTAL") = Math.Truncate(sumprimatotal * 100) / 100
                            R("DERECHO") = Math.Truncate(sumderecho * 100) / 100
                            R("IVA") = Math.Truncate(sumiva * 100) / 100

                            dtt.Rows.Add(R)
                            i = 0
                            sumprimaneta = 0
                            sumprimatotal = 0
                            sumderecho = 0
                            sumiva = 0
                        Else
                            sumprimaneta = sumprimaneta + recibos(j).realPremium.amount
                            sumprimatotal = sumprimatotal + recibos(j).totalPremium.amount
                            sumderecho = sumderecho + recibos(j).shippingCosts.amount
                            sumiva = sumiva + recibos(j).tax.value.amount
                        End If
                    Else
                        dtt.Rows.Add(R)
                    End If
                End If
            Next
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
            R("INSURERID") = 0

            dtt.Rows.Add(R)
        End If

        Return dtt
    End Function

    Private Function Obteninsurer(ByVal insurer As String) As String
        Dim strinsurer As String = String.Empty
        Dim objconex As New SDManejaBD.clsConexion
        Dim sql As New StringBuilder
        Dim dts As New DataSet()

        sql.AppendLine("SELECT DESCINTERNO ")
        sql.AppendLine("FROM CATALOGO_BANCOMER")
        sql.AppendLine("WHERE RECURSOWEB = '2'")
        sql.AppendLine("AND IDEXTERNO = " & insurer)

        dts = objconex.EjecutaQueryConsulta(sql.ToString)

        If dts.Tables.Count > 0 Then
            If dts.Tables(0).Rows.Count > 0 Then
                strinsurer = dts.Tables(0).Rows(0).Item("DESCINTERNO").ToString()
            End If
        End If

        Return strinsurer
    End Function

    Private Function ObtenPack(ByVal PackId As String) As String
        Dim idPack As String = String.Empty

        Dim objconex As New SDManejaBD.clsConexion
        Dim sql As New StringBuilder
        Dim dts As New DataSet()

        sql.AppendLine("SELECT NOMBRE")
        sql.AppendLine("FROM COBERTURAS")
        sql.AppendLine("WHERE ID_BROKER = 2")
        sql.AppendLine("AND ID_EXTERNO = " & PackId)

        dts = objconex.EjecutaQueryConsulta(sql.ToString)

        idPack = dts.Tables(0).Rows(0).Item("NOMBRE").ToString()

        Return idPack
    End Function

    Public Function RecibosOrdas(respuesta As String) As DataTable
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
        dtt.Columns.Add("insurerId")
        dtt.Columns.Add("idPack")

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jresult As Ordas = serializer.Deserialize(Of Ordas)(respuesta)

        Dim msjerror As String = jresult.quote.complement.errorInfo.description

        Dim recibos As New List(Of receipts)
        recibos = jresult.quote.receipts

        If msjerror = "OK" Then
            For j As Integer = 0 To recibos.Count - 1
                Dim R As DataRow = dtt.NewRow
                R("idRequest") = recibos(j).idRequest
                R("startDate") = Date.Now.ToShortDateString
                R("endDate") = Date.Now.ToShortDateString
                R("shippingCosts") = Math.Truncate(recibos(j).shippingCosts.amount * 100) / 100
                R("tax") = Math.Truncate(recibos(j).tax.value.amount * 100) / 100
                R("realPremium") = Math.Truncate(recibos(j).realPremium.amount * 100) / 100
                R("totalPremium") = Math.Truncate(recibos(j).totalPremium.amount * 100) / 100
                R("lateFee") = Math.Truncate(0.00 * 100) / 100
                R("serialNumber") = recibos(j).serialNumber
                R("insurerId") = recibos(j).insurerId
                R("idPack") = recibos(j).idPack

                dtt.Rows.Add(R)
            Next
        End If

        Return dtt
    End Function

    Public Function Obteninsurerid(ByVal idinterno As Integer) As String
        Dim strinsurerid As String = String.Empty
        Dim objconex As New SDManejaBD.clsConexion
        Dim sql As New StringBuilder
        Dim dts As New DataSet()

        sql.AppendLine("SELECT IDEXTERNO ")
        sql.AppendLine("FROM CATALOGO_BANCOMER")
        sql.AppendLine("WHERE RECURSOWEB = '2'")
        sql.AppendLine("AND IDINTERNO = " & idinterno)

        dts = objconex.EjecutaQueryConsulta(sql.ToString)

        If dts.Tables.Count > 0 Then
            If dts.Tables(0).Rows.Count > 0 Then
                strinsurerid = dts.Tables(0).Rows(0).Item("IDEXTERNO").ToString()
            End If
        End If

        Return strinsurerid
    End Function

End Class



