'RQ-PC5: JMENDIETA  27/01/2018: Se crea la clase que genera DataTable
Public Class clsDeserialMarshMulti
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
        Public quotes As List(Of quote) = New List(Of quote)
        Public idRequestQuote As String = String.Empty
        Public requestDateQuote As String = String.Empty
        Public serviceTypeQuote As String = String.Empty
        Public iComplements As iComplements = New iComplements()
        Public errorInfo As errorInfo = New errorInfo()
    End Class


    Public Class quote
        Public complement As complement = New complement()
        Public insurerId As String
        Public validityPeriod As validityPeriod = New validityPeriod()
        Public totalPremium As totalPremium = New totalPremium()
        Public quoteId As String = String.Empty
        Public receipts As List(Of receipts) = New List(Of receipts)
        Public shippingCosts As shippingCosts = New shippingCosts()
        Public tax As tax = New tax()
        Public realPremium As realPremium = New realPremium()
        Public lateFee As lateFee = New lateFee()
        Public policyTypeId As String = String.Empty
        Public totalNumberReceipts As String = String.Empty
    End Class

    Public Class complement
        Public errorInfo As errorInfo = New errorInfo()
    End Class

    Public Class errorInfo
        Public errorId As String
        Public description As String
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
        Public reference1 As String
        Public reference2 As String
    End Class

    Public Class receipts
        Public validityPeriod As validityPeriod = New validityPeriod()
        Public shippingCosts As shippingCosts = New shippingCosts()
        Public tax As tax = New tax()
        Public realPremium As realPremium = New realPremium()
        Public totalPremium As totalPremium = New totalPremium()
        Public lateFee As lateFee = New lateFee()
        Public serialNumber As String = String.Empty
    End Class

    Public Class shippingCosts
        Public amount As Decimal
    End Class

    Public Class tax
        Public value As value = New value()
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

    Public Class AseguradorasDetalle

        Public IDCATALOGO As Integer
        Public RECURSOWEB As String
        Public IDEXTERNO As String
        Public DESCEXTERNO As String
        Public IDINTERNO As Integer
        Public DESCINTERNO As String
        Public REFERENCIA As String
        Public ESTATUS As Integer
    End Class

    Sub New()
    End Sub

    Public Function Deserealize(ByRef lsQuotes As List(Of quote), ByRef lstAseguradoras As List(Of AseguradorasDetalle), ByVal term As Integer) As DataTable
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

        If (Not IsNothing(lsQuotes)) AndAlso lsQuotes.Count > 0 Then

            'Dim lstAseguradora = (From dr In dsAseguradora.Tables(0).AsEnumerable
            '                      Select New With { _
            '                    Key .IDCATALOGO = dr.Field(Of Integer)("IDCATALOGO"), _
            '                        Key .RECURSOWEB = dr.Field(Of String)("RECURSOWEB"), _
            '                        Key .IDEXTERNO = dr.Field(Of String)("IDEXTERNO"), _
            '                        Key .DESCEXTERNO = dr.Field(Of String)("DESCEXTERNO"), _
            '                        Key .IDINTERNO = dr.Field(Of Integer)("IDINTERNO"), _
            '                        Key .DESCINTERNO = dr.Field(Of String)("DESCINTERNO"), _
            '                        Key .REFERENCIA = dr.Field(Of String)("REFERENCIA"), _
            '                        Key .ESTATUS = dr.Field(Of Integer)("ESTATUS")
            '                        }).ToList()

            For Each quote In lsQuotes

                If String.IsNullOrEmpty(quote.complement.errorInfo.description) Then 'AndAlso (Not IsNothing(quote.receipts)) AndAlso quote.receipts.Count > 0 Then

                    Dim R As DataRow = dtt.NewRow

                    If lstAseguradoras.Any(Function(aseg) aseg.IDEXTERNO = quote.insurerId) Then
                        Dim descExternoAseg = lstAseguradoras.FirstOrDefault(Function(aseg) aseg.IDEXTERNO = quote.insurerId).DESCINTERNO.ToString()
                        R("ASEGURADORA") = IIf(String.IsNullOrEmpty(descExternoAseg), String.Empty, descExternoAseg)
                    Else
                        R("ASEGURADORA") = String.Empty
                    End If

                    R("PRIMA NETA") = Math.Truncate(quote.realPremium.amount * 100) / 100  'quote.receipts.Sum(Function(receipt) receipt.realPremium.amount) 'TODO Math.Truncate(quote.realPremium.amount * 100) / 100
                    R("DERECHO") = Math.Truncate(quote.shippingCosts.amount * 100) / 100 'quote.receipts.Sum(Function(receipt) receipt.shippingCosts.amount) 'TODO Math.Truncate(quote.shippingCosts.amount * 100) / 100
                    R("IVA") = Math.Truncate(quote.tax.value.amount * 100) / 100 'quote.receipts.Sum(Function(receipt) receipt.tax.value.amount) 'TODO Math.Truncate(quote.tax.amount * 100) / 100
                    R("PRIMA TOTAL") = Math.Truncate(quote.totalPremium.amount * 100) / 100 'quote.receipts.Sum(Function(receipt) receipt.totalPremium.amount) 'TODO Math.Truncate(quote.totalPremium.amount * 100) / 100

                    R("ID_PAQUETE") = term.ToString
                    R("PAQUETE") = quote.policyTypeId 'TODO validar el nuevo servicio devuel numero no descripcion
                    R("ID_COTIZACION") = quote.quoteId

                    R("USO") = String.Empty
                    R("URL_COTIZACION") = String.Empty
                    R("PRIMA_NETA_GAP") = String.Empty
                    R("IVA_GAP") = String.Empty
                    R("SEGURO_GAP") = String.Empty
                    R("SEGURO_VIDA") = String.Empty
                    R("OBSERVACIONES") = String.Empty
                    R("PRIMA_TOTAL_SG") = 0
                    R("RECARGO") = 0
                    dtt.Rows.Add(R)
                End If
            Next
        Else
            Dim R As DataRow = dtt.NewRow
            R("ASEGURADORA") = String.Empty
            R("PRIMA NETA") = 0.0
            R("DERECHO") = 0.0
            R("IVA") = 0.0
            R("PRIMA TOTAL") = 0.0
            R("ID_PAQUETE") = term.ToString
            R("PAQUETE") = String.Empty
            R("ID_COTIZACION") = 0
            R("USO") = String.Empty
            R("URL_COTIZACION") = String.Empty
            R("PRIMA_NETA_GAP") = String.Empty
            R("IVA_GAP") = String.Empty
            R("SEGURO_GAP") = String.Empty
            R("SEGURO_VIDA") = String.Empty
            R("OBSERVACIONES") = String.Empty
            R("PRIMA_TOTAL_SG") = 0
            R("RECARGO") = 0
            dtt.Rows.Add(R)
        End If
        Return dtt
    End Function

    Public Function RecibosMarsh(ByRef lsQuotes As List(Of quote)) As DataTable
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

        If (Not IsNothing(lsQuotes)) AndAlso lsQuotes.Count > 0 Then

            For Each quote In lsQuotes
                If String.IsNullOrEmpty(quote.complement.errorInfo.description) AndAlso (Not IsNothing(quote.receipts)) AndAlso quote.receipts.Count > 0 Then
                    For Each receipt In quote.receipts

                        Dim R As DataRow = dtt.NewRow
                        R("idRequest") = quote.quoteId 'TODO jresult.quote.idRequest validar si es correcto (quote.quoteId o jresult.idRequestQuote )
                        R("startDate") = receipt.validityPeriod.startDate 'recibos(j).validityPeriod.startDate
                        R("endDate") = receipt.validityPeriod.endDate 'recibos(j).validityPeriod.endDate
                        R("shippingCosts") = Math.Truncate(receipt.shippingCosts.amount * 100) / 100 'Math.Truncate(receipt.shippingCosts.amount * 100) / 100
                        R("tax") = Math.Truncate(receipt.tax.value.amount * 100) / 100 'Math.Truncate(receipt.tax.value.amount * 100) / 100
                        R("realPremium") = Math.Truncate(receipt.realPremium.amount * 100) / 100 'Math.Truncate(receipt.realPremium.amount * 100) / 100
                        R("totalPremium") = Math.Truncate(receipt.totalPremium.amount * 100) / 100 'Math.Truncate(receipt.totalPremium.amount * 100) / 100
                        R("lateFee") = Math.Truncate(receipt.lateFee.amount * 100) / 100 'Math.Truncate(receipt.lateFee.amount * 100) / 100
                        R("serialNumber") = receipt.serialNumber

                        dtt.Rows.Add(R)
                    Next
                End If
            Next
        End If
        Return dtt
    End Function

End Class
