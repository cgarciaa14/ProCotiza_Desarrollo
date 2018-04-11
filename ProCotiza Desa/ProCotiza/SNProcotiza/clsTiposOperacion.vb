Imports SDManejaBD
'BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.

Public Class clsTiposOperacion
    Inherits clsSession

    Private strErrTipoOper As String = ""

    Private intTipoOper As Integer = 0
    Private intTipoEsq As Integer = 0
    Private intEstatus As Integer = 0
    Private intGiro As Integer = 0
    Private intEstatusOtro As Integer = 0
    Private intTipoGasto As Integer = 0
    Private intTipoFormulaPagoIni As Integer = 0
    Private intPerJuridica As Integer = 0

    Private sngCobraGtosExt As Single = 0
    Private sngPideValorResid As Single = 0
    Private sngPideOpcionComp As Single = 0
    Private sngPideDepGarantia As Single = 0
    Private sngPermitePagoIni As Single = 0
    Private sngIvaSobreCapital As Single = 0
    Private sngIvaSobreInteres As Single = 0
    Private sngCapitalIncluyeIVA As Single = 0

    Private strNombre As String = ""
    Private strClave As String = ""
    Private strUsuReg As String = ""
    Private strLeyendaValResid As String = ""
    Private strLeyendaDepGarantia As String = ""
    Private intidpaquete As Integer = 0

    Sub New()
    End Sub
    Sub New(ByVal intCveTipoOper As Integer)
        CargaTipoOperacion(intCveTipoOper)
    End Sub

    Public ReadOnly Property ErrorTipoOperacion() As String
        Get
            Return strErrTipoOper
        End Get
    End Property

    Public Property UsuarioRegistro() As String
        Get
            Return strUsuReg
        End Get
        Set(ByVal value As String)
            strUsuReg = value
        End Set
    End Property

    Public Property IDTipoOperacion() As Integer
        Get
            Return intTipoOper
        End Get
        Set(ByVal value As Integer)
            intTipoOper = value
        End Set
    End Property

    Public Property IDTipoEsquemaFinanciamiento() As Integer
        Get
            Return intTipoEsq
        End Get
        Set(ByVal value As Integer)
            intTipoEsq = value
        End Set
    End Property

    Public Property IDEstatus() As Integer
        Get
            Return intEstatus
        End Get
        Set(ByVal value As Integer)
            intEstatus = value
        End Set
    End Property

    Public Property IDTipoGastoExtra() As Integer
        Get
            Return intTipoGasto
        End Get
        Set(ByVal value As Integer)
            intTipoGasto = value
        End Set
    End Property

    Public Property IDTipoFormulaPagoInicial() As Integer
        Get
            Return intTipoFormulaPagoIni
        End Get
        Set(ByVal value As Integer)
            intTipoFormulaPagoIni = value
        End Set
    End Property

    Public Property CobraGastosExtra() As Single
        Get
            Return sngCobraGtosExt
        End Get
        Set(ByVal value As Single)
            sngCobraGtosExt = value
        End Set
    End Property

    Public Property PideValorResidual() As Single
        Get
            Return sngPideValorResid
        End Get
        Set(ByVal value As Single)
            sngPideValorResid = value
        End Set
    End Property

    Public Property PideDepositoGarantia() As Single
        Get
            Return sngPideDepGarantia
        End Get
        Set(ByVal value As Single)
            sngPideDepGarantia = value
        End Set
    End Property

    Public Property PideOpcionCompra() As Single
        Get
            Return sngPideOpcionComp
        End Get
        Set(ByVal value As Single)
            sngPideOpcionComp = value
        End Set
    End Property

    Public Property PermitecalculoPagoInicial() As Single
        Get
            Return sngPermitePagoIni
        End Get
        Set(ByVal value As Single)
            sngPermitePagoIni = value
        End Set
    End Property

    Public Property CobraIVASobreCapital() As Single
        Get
            Return sngIvaSobreCapital
        End Get
        Set(ByVal value As Single)
            sngIvaSobreCapital = value
        End Set
    End Property

    Public Property CobraIVASobreInteres() As Single
        Get
            Return sngIvaSobreInteres
        End Get
        Set(ByVal value As Single)
            sngIvaSobreInteres = value
        End Set
    End Property

    Public Property CapitalFinaciarIncluyeIVA() As Single
        Get
            Return sngCapitalIncluyeIVA
        End Get
        Set(ByVal value As Single)
            sngCapitalIncluyeIVA = value
        End Set
    End Property

    Public Property IDEstatusOtro() As Integer
        Get
            Return intEstatusOtro
        End Get
        Set(ByVal value As Integer)
            intEstatusOtro = value
        End Set
    End Property

    Public Property IDGiro() As Integer
        Get
            Return intGiro
        End Get
        Set(ByVal value As Integer)
            intGiro = value
        End Set
    End Property

    Public Property IDPersonalidadJuridica() As Integer
        Get
            Return intPerJuridica
        End Get
        Set(ByVal value As Integer)
            intPerJuridica = value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return strNombre
        End Get
        Set(ByVal value As String)
            strNombre = value
        End Set
    End Property

    Public Property Clave() As String
        Get
            Return strClave
        End Get
        Set(ByVal value As String)
            strClave = value
        End Set
    End Property

    Public Property LeyendaValorResidual() As String
        Get
            Return strLeyendaValResid
        End Get
        Set(ByVal value As String)
            strLeyendaValResid = value
        End Set
    End Property

    Public Property LeyendaDepositoGarantia() As String
        Get
            Return strLeyendaDepGarantia
        End Get
        Set(ByVal value As String)
            strLeyendaDepGarantia = value
        End Set
    End Property

    Public Property IDPaquete As Integer
        Get
            Return intidpaquete
        End Get
        Set(value As Integer)
            intidpaquete = value
        End Set
    End Property

    Public Sub CargaTipoOperacion(Optional ByVal intTipOp As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intTipoOper = intTipOp
            dtsRes = ManejaTipoOperacion(1)
            intTipoOper = 0
            If Trim$(strErrTipoOper) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intTipoOper = dtsRes.Tables(0).Rows(0).Item("ID_TIPO_OPERACION")
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    intTipoEsq = dtsRes.Tables(0).Rows(0).Item("ID_TIPO_ESQUEMA")
                    intTipoGasto = dtsRes.Tables(0).Rows(0).Item("ID_TIPO_GASTO")
                    intTipoFormulaPagoIni = dtsRes.Tables(0).Rows(0).Item("ID_FORMULA_PAGO_INICIAL")
                    strNombre = dtsRes.Tables(0).Rows(0).Item("NOMBRE")
                    strClave = dtsRes.Tables(0).Rows(0).Item("CLAVE")
                    strLeyendaValResid = dtsRes.Tables(0).Rows(0).Item("LEYENDA_VALOR_RESIDUAL")
                    strLeyendaDepGarantia = dtsRes.Tables(0).Rows(0).Item("LEYENDA_DEP_GARANTIA")
                    sngCobraGtosExt = dtsRes.Tables(0).Rows(0).Item("COBRA_GASTOS_EXTRA")
                    sngPideValorResid = dtsRes.Tables(0).Rows(0).Item("PIDE_VALOR_RESIDUAL")
                    sngPideOpcionComp = dtsRes.Tables(0).Rows(0).Item("PIDE_OPCION_COMPRA")
                    sngPideDepGarantia = dtsRes.Tables(0).Rows(0).Item("PIDE_DEP_GARANTIA")
                    sngPermitePagoIni = dtsRes.Tables(0).Rows(0).Item("PERMITE_PAGO_INICIAL")
                    sngCapitalIncluyeIVA = dtsRes.Tables(0).Rows(0).Item("CAPITAL_INCLUYE_IVA")
                    strUsuReg = dtsRes.Tables(0).Rows(0).Item("USU_REG")
                Else
                    strErrTipoOper = "No se encontró información para poder cargar el tipo de operación"
                End If
            End If
        Catch ex As Exception
            strErrTipoOper = ex.Message
        End Try
    End Sub

    Public Function ManejaTipoOperacion(ByVal intOper As Integer) As DataSet
        strErrTipoOper = ""
        ManejaTipoOperacion = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta 
                    If intTipoOper > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoOper", intTipoOper.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If Trim(strNombre) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                Case 2 ' inserta 
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoEsq", intTipoEsq.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "clave", strClave)
                    ArmaParametros(strParamStored, TipoDato.Entero, "cobraGtosExt", sngCobraGtosExt.ToString)
                    If intTipoGasto > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoGasto", intTipoGasto.ToString)
                    If intTipoFormulaPagoIni > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idForPagoIni", intTipoFormulaPagoIni.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "pideValResid", sngPideValorResid.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "pideDepGar", sngPideDepGarantia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "pideOpcComp", sngPideOpcionComp.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permitePagoIni", sngPermitePagoIni.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "capitalIncIVA", sngCapitalIncluyeIVA.ToString)
                    If Trim(strLeyendaDepGarantia) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "leyendaDepGar", strLeyendaDepGarantia)
                    If Trim(strLeyendaValResid) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "leyendaValResid", strLeyendaValResid)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 3 ' actualiza 
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoOper", intTipoOper.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoEsq", intTipoEsq.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "clave", strClave)
                    ArmaParametros(strParamStored, TipoDato.Entero, "cobraGtosExt", sngCobraGtosExt.ToString)
                    If intTipoGasto > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoGasto", intTipoGasto.ToString)
                    If intTipoFormulaPagoIni > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idForPagoIni", intTipoFormulaPagoIni.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "pideValResid", sngPideValorResid.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "pideDepGar", sngPideDepGarantia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "pideOpcComp", sngPideOpcionComp.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permitePagoIni", sngPermitePagoIni.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "capitalIncIVA", sngCapitalIncluyeIVA.ToString)
                    If Trim(strLeyendaDepGarantia) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "leyendaDepGar", strLeyendaDepGarantia)
                    If Trim(strLeyendaValResid) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "leyendaValResid", strLeyendaValResid)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 4 ' borra 
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoOper", intTipoOper.ToString)
                Case 5 ' borra tipo operacion/giros 
                    If intTipoOper > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoOper", intTipoOper.ToString)
                    If intGiro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idGiro", intGiro.ToString)
                Case 6 ' inserta tipo operacion/giros 
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoOper", intTipoOper.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idGiro", intGiro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 7 ' consulta tipo operacion/giros 
                    If intTipoOper > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoOper", intTipoOper.ToString)
                    If intGiro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idGiro", intGiro.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If intEstatusOtro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                Case 8 ' borra tipo operacion/cobro iva
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoOper", intTipoOper.ToString)
                Case 9 ' inserta tipo operacion/cobro iva 
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoOper", intTipoOper.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPerJurid", intPerJuridica.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "IVACapital", sngIvaSobreCapital.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "IVAInteres", sngIvaSobreInteres.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 10 ' consulta tipo operacion/cobro iva 
                    If intTipoOper > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoOper", intTipoOper.ToString)
                    If intPerJuridica > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPerJurid", intPerJuridica.ToString)
                Case 11 ' consulta para maneja giros
                    If intTipoOper > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoOper", intTipoOper.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                Case 12 ' consulta para maneja giros
                    If intPerJuridica > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPerJurid", intPerJuridica.ToString)
                Case 13 ' consulta por agencia intidagecnia
                    If intidpaquete > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idpaquete", intidpaquete.ToString)
            End Select

            ManejaTipoOperacion = objSD.EjecutaStoredProcedure("spManejaTiposOperacion", strErrTipoOper, strParamStored)
            If strErrTipoOper = "" Then
                If intOper = 2 Then
                    intTipoOper = ManejaTipoOperacion.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    'GuardaLog("TIPOS_OPERACION", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrTipoOper = ex.Message
        End Try
    End Function
End Class
