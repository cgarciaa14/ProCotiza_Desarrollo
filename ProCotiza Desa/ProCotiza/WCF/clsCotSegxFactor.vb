'BUG-PC-49 MAPH 22/03/2017 CAMBIO DE MARREDONDO	
'BUG-PC-72: RHERNANDEZ: 08/06/17: SE ARREGLA PROBLEMA DE PERDIDA DE RECIBOS AL GUARDAR COTIZACIONES DE SEGUROS POR FACTOR
'BUG-PC-81: RHERNANDEZ: 29/06/17: SE AGREGA VARIA TIPO SEG PARA EVALUAR SI ES CONTADO O NO
'RQ-SEGIP : RHERNANDEZ: 19/07/17: SE AGREGA A LA CLASE EL ID PRODUCTO PARA EL CALCULO DE SEGUROS POR FACTOR CON CONSTANTE DINAMICA
Imports System.Text
Imports SDManejaBD
Public Class clsCotSegxFactor

    Private _strErr As String = String.Empty

    Public ReadOnly Property StrErr As String
        Get
            Return _strErr
        End Get
    End Property

    Sub New()
    End Sub


    Public Function CotizaxFactor(ByVal intaccion As Integer, ByVal intaseguradoraid As Integer, ByVal dbprecio As Double, ByVal dbiva As Double,
                                  ByVal dbaccesorios As Double, ByVal intclasif As Integer, ByVal intpaqueteid As Integer, ByVal intedo As Integer,
                                  ByVal intcobertura As Integer, ByVal intplazo As Integer, ByVal inttipo As Integer, ByVal intcotizacion As Integer, ByVal intedad As Integer,
                                  ByVal intsexo As Integer, ByVal strnombre As String, ByVal intaniogratis As Integer, ByVal decSegVida As Double, Optional ByVal tiposeg As Integer = 0, Optional ByVal id_prod As Integer = 0) As DataSet
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

        Try
            dts = Obten_Plazos(intpaqueteid)

            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To dts.Tables(0).Rows.Count - 1
                        ArmaParametros(strParamStored, TipoDato.Entero, "accion", intaccion.ToString)
                        ArmaParametros(strParamStored, TipoDato.Entero, "idaseguradora", intaseguradoraid.ToString)
                        ArmaParametros(strParamStored, TipoDato.Doble, "precio", dbprecio.ToString)
                        ArmaParametros(strParamStored, TipoDato.Doble, "iva", dbiva.ToString)
                        ArmaParametros(strParamStored, TipoDato.Doble, "accesorios", dbaccesorios.ToString)
                        ArmaParametros(strParamStored, TipoDato.Entero, "idclasif", intclasif.ToString)
                        ArmaParametros(strParamStored, TipoDato.Entero, "idpaq", intpaqueteid.ToString)
                        ArmaParametros(strParamStored, TipoDato.Entero, "idedo", intedo.ToString)
                        ArmaParametros(strParamStored, TipoDato.Entero, "idcobertura", intcobertura.ToString)
                        ArmaParametros(strParamStored, TipoDato.Entero, "idplazo", dts.Tables(0).Rows(i).Item("ID_PLAZO").ToString)
                        ArmaParametros(strParamStored, TipoDato.Entero, "idtipo", inttipo.ToString)
                        ArmaParametros(strParamStored, TipoDato.Entero, "idcotiza", intcotizacion.ToString)
                        ArmaParametros(strParamStored, TipoDato.Entero, "edad", intedad.ToString)
                        ArmaParametros(strParamStored, TipoDato.Entero, "sexo", intsexo.ToString)
                        ArmaParametros(strParamStored, TipoDato.Cadena, "nombre", strnombre)
                        ArmaParametros(strParamStored, TipoDato.Entero, "AnioGratis", intaniogratis.ToString)
                        ArmaParametros(strParamStored, TipoDato.Doble, "decSegVida", decSegVida)
                        ArmaParametros(strParamStored, TipoDato.Entero, "tiposeg", tiposeg)
                        ArmaParametros(strParamStored, TipoDato.Doble, "id_prod", id_prod)
                        dttfactor = objSD.EjecutaStoredProcedure("procCalculoSegXFactor", _strErr, strParamStored)
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

Public Enum TipoDato
    Cadena = 1
    Entero = 2
    Doble = 3
    Fecha = 4
    FechaHora = 5
    Booleano = 6
End Enum