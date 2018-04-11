'BUG-PC-49 MAPH 22/03/2017 CAMBIO DE MARREDONDO	 
'RQ-SEGIP : RHERNANDEZ: 19/07/17: SE MODIFICA CLASE PARA RECIBIR EL PARAMETRO ID_PRODUCTO
'BUG-PC-98 RHERNANDEZ  10/08/17: CORRECCIONES PARA INDIAN POLARIS MOTOS Y SEGURO CONTADO
Imports System.Text
Imports SDManejaBD
Public Class clsCotSegFordQXFactor

    Private _strErr As String = String.Empty

    Public ReadOnly Property StrErr As String
        Get
            Return _strErr
        End Get
    End Property

    Sub New()
    End Sub

    Public Function CotizaFordQxFactor(ByVal dbPrecio As Double, ByVal dbIva As Double, ByVal intEdoID As Integer, ByVal intAseguradoraID As Integer,
                                       ByVal intClasifID As Integer, ByVal intTipoUnidad As Integer, ByVal intPaqueteID As Integer, ByVal id_prod As Integer,ByVal tipo_Seg As integer) As DataSet
        Dim dts As New DataSet()
        Dim dttrecibos As New DataSet()
        Dim dttfactor As New DataSet()
        Dim resultfactor As New DataSet()
        Dim objSD As New SDManejaBD.clsConexion
        Dim strParamStored As String = String.Empty

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

        ''(@Precio DECIMAL(18,2), @Edo INT, @IDAseguradora INT, @IDPlazo INT, @Clasif INT, @TipoUnidad INT)
        Try

            dts = Obten_Plazos(intPaqueteID)

            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then

                    For i As Integer = 0 To dts.Tables(0).Rows.Count - 1
                        ArmaParametros(strParamStored, TipoDato.Doble, "Precio", dbPrecio.ToString)
                        ArmaParametros(strParamStored, TipoDato.Doble, "IVA", dbIva.ToString)
                        ArmaParametros(strParamStored, TipoDato.Entero, "Edo", intEdoID.ToString)
                        ArmaParametros(strParamStored, TipoDato.Entero, "IDAseguradora", intAseguradoraID.ToString)
                        ArmaParametros(strParamStored, TipoDato.Entero, "IDPlazo", dts.Tables(0).Rows(i).Item("ID_PLAZO").ToString)
                        ArmaParametros(strParamStored, TipoDato.Entero, "Clasif", intClasifID.ToString)
                        ArmaParametros(strParamStored, TipoDato.Entero, "TipoUnidad", intTipoUnidad.ToString)
                        ArmaParametros(strParamStored, TipoDato.Doble, "ID_Prod", id_prod)
                        ArmaParametros(strParamStored, TipoDato.Entero, "Tipo_seg", tipo_Seg) 'cambio rhernandez
                        dttfactor = objSD.EjecutaStoredProcedure("procFordQXFactor", _strErr, strParamStored)
                        strParamStored = String.Empty


                        For Each row As DataRow In dttfactor.Tables(0).Rows
                            dtb.ImportRow(row)
                        Next

                        For Each row2 As DataRow In dttfactor.Tables(1).Rows
                            dtrec.ImportRow(row2)
                        Next
                    Next

                    resultfactor.Tables.Add(dtb)
                    resultfactor.Tables.Add(dtrec)

                End If
            End If

            resultfactor.Tables(0).TableName = "Result"
            resultfactor.Tables(1).TableName = "Recibos"

            Return resultfactor

        Catch ex As Exception
            _strErr = ex.Message
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

    Public Sub ArmaParametros(ByRef strParam As String,
                               ByVal intTipo As TipoDato,
                               ByVal strNom As String,
                               ByVal strValor As String)

        strValor = Replace(strValor, ",", "\c\")
        strValor = Replace(strValor, "|", "\p\")

        If Trim$(strParam) = "" Then
            strParam = strNom & "," & intTipo & "," & strValor
        Else
            strParam += "|" & strNom & "," & intTipo & "," & strValor
        End If
    End Sub

End Class