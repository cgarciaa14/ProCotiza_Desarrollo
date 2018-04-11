'BBV-P412 RQCOT-01.2: AMR 23/02/2017: Seguros.
'BUG-PC-59:AMATA:08/05/2017:Seguro de vida Bancomer.

Imports SDManejaBD
Public Class clsSeguroDetalle
    Inherits clsSession

    Private _strError As String = String.Empty
    Private _SEG_FL_CVE As Integer = 0
    Private _ID_RECIBO As Integer = 0
    Private _SEG_NO_PRIMANETA As Double = 0
    Private _SEG_NO_RECARGO As Double = 0
    Private _SEG_NO_DERECHO As Double = 0
    Private _SEG_NO_IVA As Double = 0
    Private _SEG_NO_PRIMATOTAL As Double = 0
    Private _SEG_TIPO_SEGURO As Integer = 0


    Public ReadOnly Property StrError As String
        Get
            Return _strError
        End Get
    End Property

    Public Property SEG_FL_CVE As Integer
        Get
            Return _SEG_FL_CVE
        End Get
        Set(value As Integer)
            _SEG_FL_CVE = value
        End Set
    End Property

    Public Property ID_RECIBO As Integer
        Get
            Return _ID_RECIBO
        End Get
        Set(value As Integer)
            _ID_RECIBO = value
        End Set
    End Property

    Public Property SEG_NO_PRIMANETA As Double
        Get
            Return _SEG_NO_PRIMANETA
        End Get
        Set(value As Double)
            _SEG_NO_PRIMANETA = value
        End Set
    End Property

    Public Property SEG_NO_RECARGO As Double
        Get
            Return _SEG_NO_RECARGO
        End Get
        Set(value As Double)
            _SEG_NO_RECARGO = value
        End Set
    End Property

    Public Property SEG_NO_DERECHO As Double
        Get
            Return _SEG_NO_DERECHO
        End Get
        Set(value As Double)
            _SEG_NO_DERECHO = value
        End Set
    End Property

    Public Property SEG_NO_IVA As Double
        Get
            Return _SEG_NO_IVA
        End Get
        Set(value As Double)
            _SEG_NO_IVA = value
        End Set
    End Property

    Public Property SEG_NO_PRIMATOTAL As Double
        Get
            Return _SEG_NO_PRIMATOTAL
        End Get
        Set(value As Double)
            _SEG_NO_PRIMATOTAL = value
        End Set
    End Property

    Public Property SEG_TIPO_SEGURO As Integer
        Get
            Return _SEG_TIPO_SEGURO
        End Get
        Set(value As Integer)
            _SEG_TIPO_SEGURO = value
        End Set
    End Property

    Sub New()
    End Sub

    Sub New(ByVal intcve As Integer)
        CargaDetalleSeg(intcve)
    End Sub

    Public Function CargaDetalleSeg(clave As Integer)
        Dim dtsRes As New DataSet

        Try
            _SEG_FL_CVE = clave
            dtsRes = ManejaDetalleSeg(1)
            _SEG_FL_CVE = 0

            If _strError = "" Then
                If dtsRes.Tables().Count > 0 Then
                    If dtsRes.Tables(0).Rows().Count > 0 Then
                        _SEG_FL_CVE = dtsRes.Tables(0).Rows(0).Item("SEG_FL_CVE")
                        _ID_RECIBO = dtsRes.Tables(0).Rows(0).Item("ID_RECIBO")
                        _SEG_NO_PRIMANETA = dtsRes.Tables(0).Rows(0).Item("SEG_NO_PRIMANETA")
                        _SEG_NO_RECARGO = dtsRes.Tables(0).Rows(0).Item("SEG_NO_RECARGO")
                        _SEG_NO_DERECHO = dtsRes.Tables(0).Rows(0).Item("SEG_NO_DERECHO")
                        _SEG_NO_IVA = dtsRes.Tables(0).Rows(0).Item("SEG_NO_IVA")
                        _SEG_NO_PRIMATOTAL = dtsRes.Tables(0).Rows(0).Item("SEG_NO_PRIMATOTAL")
                        _SEG_TIPO_SEGURO = dtsRes.Tables(0).Rows(0).Item("SEG_TIPO_SEGURO")
                    End If
                End If
            End If

        Catch ex As Exception

        End Try


        Return dtsRes
    End Function

    Public Function ManejaDetalleSeg(ByVal intOper As Integer) As DataSet

        ManejaDetalleSeg = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 'Select
                    ArmaParametros(strParamStored, TipoDato.Entero, "SEG_FL_CVE", _SEG_FL_CVE.ToString)
                Case 2 'Insert
                    ArmaParametros(strParamStored, TipoDato.Entero, "SEG_FL_CVE", _SEG_FL_CVE.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_RECIBO", _ID_RECIBO.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "SEG_NO_PRIMANETA", _SEG_NO_PRIMANETA.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "SEG_NO_RECARGO", _SEG_NO_RECARGO.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "SEG_NO_DERECHO", _SEG_NO_DERECHO.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "SEG_NO_IVA", _SEG_NO_IVA.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "SEG_NO_PRIMATOTAL", _SEG_NO_PRIMATOTAL.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "SEG_TIPO_SEGURO", _SEG_TIPO_SEGURO.ToString)
                Case 3 'Update
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_RECIBO", _ID_RECIBO.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "SEG_NO_PRIMANETA", _SEG_NO_PRIMANETA.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "SEG_NO_RECARGO", _SEG_NO_RECARGO.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "SEG_NO_DERECHO", _SEG_NO_DERECHO.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "SEG_NO_IVA", _SEG_NO_IVA.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "SEG_NO_PRIMATOTAL", _SEG_NO_PRIMATOTAL.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "SEG_TIPO_SEGURO", _SEG_TIPO_SEGURO.ToString)
                Case 4 'Delete
                    ArmaParametros(strParamStored, TipoDato.Entero, "SEG_FL_CVE", _SEG_FL_CVE.ToString)
            End Select

            ManejaDetalleSeg = objSD.EjecutaStoredProcedure("ManejaCotSseguroDetalle", _strError, strParamStored)
            If _strError = "" Then
                If intOper = 2 Then
                    _SEG_FL_CVE = ManejaDetalleSeg.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("COTSEGURO_DETALLE", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception

        End Try

        Return ManejaDetalleSeg
    End Function

End Class
