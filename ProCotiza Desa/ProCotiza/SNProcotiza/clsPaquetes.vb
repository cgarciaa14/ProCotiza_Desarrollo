'BBVA-P-412: AMR: RQ02,RQ03,RQ04,RQ05,RQ06,RQ08
'BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.
''BBVA-P-412: 08/08/2016: AMR: RQ09 – Carga Masiva de Plan de Financiamiento
'BBVA-P-412: 25-08-2016: AMR: RQCOT-01 Ajustes a la pantalla de Cotización.
'BBVA-P-412 RQ WSA. WS GetLoan Products (RQ06, RQ09): AMR: 25/10/2016 WS GetLoan Products
'BUG-PC-34 MAUT 12/01/2017 Se arregla consulta para compra inteligente
'BUG-PC-46 JRHM 13/02/17 SE AGREGA EL CASE 52 PARA LA CONSULTA DE TIPOS DE SEGURO DE VIDA PARA EL COTIZADOR EN BASE AL PAQUETE SELECCIONADO
'BUG-PC-48 JRHM 15/02/17 SE MODIFICA LA OPCION 35 PARA QUE EN RELACIONA AGENCIA-PAQUETE PARA QUE ACEPTE LAS OPCIONES <SELECCIONAR>
'BUG-PC-49 MARREDONDO 06/03/2017 SE CORRIGE INSERT Y DELETE DE ASIGNACION DE AGENCIAS MASIVO
'BBVA RQTARESQ-06 CGARCIA 19/04/2017 SE AGREGA LA VARIABLE ESQUEMA PATRA LA RELACION DEL CATÁLOGO DE ESQUEMAS 
'BUG-PC-65:MPUESTO:19/05/2017:CORRECCION DE ADICION Y BORRADO DE RELACION PAQUETES-PRODUCTOS
'RQ-MN2-6: RHERNANDEZ: 15/09/17: SE AGREGAN NUEVOS FILTROS PARA LA OPCION 11 DE CONSULTA PAQUETES-PRODUCTOS
'RQ-MN2-2: ERODRIGUEZ: 21/08/2017 Requerimiento para agregar Tasa Nominal Dos, Comision Dos, para cuando el tipo de calculo sea igual a Balloon .
'RQ-MN2-2.2 ERODRIGUEZ: 27/09/2017 Se elimina porcentaje de refinanciamiento
'BUG-PC-160: CGARCIA: 26/02/2018: SE AGREGA FILTRADO POR ESQUEMA A LA OPCION 35 
Imports SDManejaBD
Imports System.Text

Public Class clsPaquetes
    Inherits clsSession

    Private strErrPaquete As String = ""

    Private intPaquete As Integer = 0
    Private intMoneda As Integer = 0
    Private intTipoOper As Integer = 0
    Private intEstatus As Integer = 0
    Private intCalendario As Integer = 0
    Private intPeriodicidad As Integer = 0
    Private intTipoCalc As Integer = 0
    Private intPlazo As Integer = 0
    Private intEstatusPlazo As Integer = 0
    Private intTipoSegVida As Integer = 0
    Private intPlazoSegRegalado As Integer = 0
    Private intClasifProd As Integer = 0
    Private intProducto As Integer = 0
    Private intEstatusOtro As Integer = 0
    Private intTipoProducto As Integer = 0
    Private intPerJurid As Integer = 0
    Private intTipoCalcSeg As Integer = 0
    Private intAseguradora As Integer = 0
    Private intPaqueteSeguro As Integer = 0
    Private intIDTipoVenc As Integer = 0
    Private intDepGarMax As Integer = 0
    Private intDepGarMin As Integer = 0
    Private intIDTasaIntVar As Integer = 0

    Private sngRegDef As Single = 0
    Private sngPrimerPagoIrreg As Single = 0
    Private sngSeguroFinanciado As Single = 0
    Private sngSeguroContado As Single = 0
    Private sngSeguroCuentaCte As Single = 0
    Private sngSeguroRegalado As Single = 0
    Private sngSeguroMultiFinan As Single = 0
    Private sngSeguroMultiCont As Single = 0
    Private sngSeguroMultiFinanAnioGratis As Single = 0
    Private sngSeguroMultiContAnioGratis As Single = 0
    Private sngPagosEspeciales As Single = 0
    Private sngConsideraEngancheAcc As Single = 0
    Private sngPermiteMtoSubsidio As Single = 0
    Private sngPermiteCapturaSegManual As Single = 0
    Private sngPermiteGraciaCap As Single = 0
    Private sngPermiteGraciaInt As Single = 0
    Private sngCotizadorAbierto As Single = 0
    Private sngCalculaIVASegVida As Single = 0
    Private sngSelecDiaPago As Single = 0
    Private sngPermitePtjSubsidio As Single = 0
    Private sngManejaTasaIntVar As Single = 0

    Private dblTasaIva As Double = 0
    Private dblEnganche As Double = 0
    Private dblTasaNom As Double = 0
    Private dblTasaNomDos As Double = 0
    Private dblServFinanDos As Double = 0
    Private dblTasaNomSeg As Double = 0
    Private dblServFinan As Double = 0
    Private dblFactSegVida As Double = 0
    Private dblPtosSegCte As Double = 0
    Private dblPtjMaxAcc As Double = 0
    Private dblPtjOpcionCompra As Double = 0
    Private dblPtjBlindDiscount As Double = 0
    Private dblTasaPCP As Double = 0
    Private dblIncentivoVtas As Double = 0

    Private strNombre As String = ""
    Private strUsuReg As String = ""
    Private strIniVig As String = ""
    Private strFinVig As String = ""
    Private strFecVigAct As String = ""
    Private strNomCopia As String = ""
    Private intnoperiodos As Integer = 0
    Private intIDPROMOCION As Integer = 0
    Private inttiposeg As Integer = 0
    Private intIdAgencia As Integer = 0
    Private strAgencia As String = String.Empty
    Private intIdMarca As Integer = 0

    'BBVA-P-412
    Private strProductoDesc As String = String.Empty
    Private dblptjpagesp As Double = 0
    Private dblptjsubsidioArmadora As Double = 0
    Private dblptjsubsidioAgencia As Double = 0
    Private dblptjcomisionvtaseg As Double = 0
    Private dblptjmincomisionvendedor As Double = 0
    Private dblptjmaxcomisionvendedor As Double = 0
    Private dblptjmincomisionagencia As Double = 0
    Private dblptjmaxcomisionagencia As Double = 0
    Private intidcanal As Integer = 0
    Private intidviacalsegvida As Integer = 0
    Private intidtiposegvida As Integer = 0
    Private intCEROCOMISION As Integer = 0
    Private intAnioModelo As Integer = 0
    Private intIdBroker As Integer = 0
    Private strNomAseguradora As String = String.Empty
    Private intidprodug As Integer = 0
    Private stridsubprodug As String = String.Empty
    Private dblimporteming As Double
    Private dblimportemaxg As Double
    Private intidAlianza As Integer = -1
    Private intidDivision As Integer = -1
    Private intidGrupo As Integer = -1
    Private intidversion As Integer = 0
    Private intidEsquema As Integer = -1
    Private intidsubmarca As Integer = 0

    Sub New()
    End Sub
    Sub New(ByVal intCvePaquete As Integer)
        CargaPaquete(intCvePaquete)
    End Sub

    Public ReadOnly Property ErrorPaquete() As String
        Get
            Return strErrPaquete
        End Get
    End Property

    'BBVA-P-412
    Public Property UsuarioRegistro() As String
        Get
            Return strUsuReg
        End Get
        Set(ByVal value As String)
            strUsuReg = value
        End Set
    End Property

    Public Property IDPaquete() As Integer
        Get
            Return intPaquete
        End Get
        Set(ByVal value As Integer)
            intPaquete = value
        End Set
    End Property

    Public Property IDMoneda() As Integer
        Get
            Return intMoneda
        End Get
        Set(ByVal value As Integer)
            intMoneda = value
        End Set
    End Property

    Public Property IDProducto() As Integer
        Get
            Return intProducto
        End Get
        Set(ByVal value As Integer)
            intProducto = value
        End Set
    End Property

    Public Property IDTipoProducto() As Integer
        Get
            Return intTipoProducto
        End Get
        Set(ByVal value As Integer)
            intTipoProducto = value
        End Set
    End Property

    Public Property IDPersonalidadJuridica() As Integer
        Get
            Return intPerJurid
        End Get
        Set(ByVal value As Integer)
            intPerJurid = value
        End Set
    End Property

    Public Property IDTipoCalculoSeguro() As Integer
        Get
            Return intTipoCalcSeg
        End Get
        Set(ByVal value As Integer)
            intTipoCalcSeg = value
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

    Public Property IDClasificacionProd() As Integer
        Get
            Return intClasifProd
        End Get
        Set(ByVal value As Integer)
            intClasifProd = value
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

    Public Property IDPlazo() As Integer
        Get
            Return intPlazo
        End Get
        Set(ByVal value As Integer)
            intPlazo = value
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

    Public Property IDEstatusPlazo() As Integer
        Get
            Return intEstatusPlazo
        End Get
        Set(ByVal value As Integer)
            intEstatusPlazo = value
        End Set
    End Property

    Public Property IDCalendario() As Integer
        Get
            Return intCalendario
        End Get
        Set(ByVal value As Integer)
            intCalendario = value
        End Set
    End Property

    Public Property IDPeriodicidad() As Integer
        Get
            Return intPeriodicidad
        End Get
        Set(ByVal value As Integer)
            intPeriodicidad = value
        End Set
    End Property

    Public Property IDTipoCalculo() As Integer
        Get
            Return intTipoCalc
        End Get
        Set(ByVal value As Integer)
            intTipoCalc = value
        End Set
    End Property

    Public Property IDTipoVencimiento() As Integer
        Get
            Return intIDTipoVenc
        End Get
        Set(ByVal value As Integer)
            intIDTipoVenc = value
        End Set
    End Property

    Public Property IDAplicacionSeguroVida() As Integer
        Get
            Return intTipoSegVida
        End Get
        Set(ByVal value As Integer)
            intTipoSegVida = value
        End Set
    End Property

    Public Property IDPlazoSeguroRegalado() As Integer
        Get
            Return intPlazoSegRegalado
        End Get
        Set(ByVal value As Integer)
            intPlazoSegRegalado = value
        End Set
    End Property

    Public Property RentasDepositoMaximas() As Integer
        Get
            Return intDepGarMax
        End Get
        Set(ByVal value As Integer)
            intDepGarMax = value
        End Set
    End Property

    Public Property RentasDepositoMinimas() As Integer
        Get
            Return intDepGarMin
        End Get
        Set(ByVal value As Integer)
            intDepGarMin = value
        End Set
    End Property

    Public Property IDTasaInteresVariable() As Integer
        Get
            Return intIDTasaIntVar
        End Get
        Set(ByVal value As Integer)
            intIDTasaIntVar = value
        End Set
    End Property

    Public Property RegDefault() As Single
        Get
            Return sngRegDef
        End Get
        Set(ByVal value As Single)
            sngRegDef = value
        End Set
    End Property

    Public Property ConsideraEngancheAccesorios() As Single
        Get
            Return sngConsideraEngancheAcc
        End Get
        Set(ByVal value As Single)
            sngConsideraEngancheAcc = value
        End Set
    End Property

    Public Property PermitePagosEspeciales() As Single
        Get
            Return sngPagosEspeciales
        End Get
        Set(ByVal value As Single)
            sngPagosEspeciales = value
        End Set
    End Property

    Public Property PermiteMontoSubsidio() As Single
        Get
            Return sngPermiteMtoSubsidio
        End Get
        Set(ByVal value As Single)
            sngPermiteMtoSubsidio = value
        End Set
    End Property

    Public Property PermitePtjSubsidio() As Single
        Get
            Return sngPermitePtjSubsidio
        End Get
        Set(ByVal value As Single)
            sngPermitePtjSubsidio = value
        End Set
    End Property

    Public Property PermiteDiaPago() As Single
        Get
            Return sngSelecDiaPago
        End Get
        Set(ByVal value As Single)
            sngSelecDiaPago = value
        End Set
    End Property

    Public Property PermiteCapturaPrimaSeguroManual() As Single
        Get
            Return sngPermiteCapturaSegManual
        End Get
        Set(ByVal value As Single)
            sngPermiteCapturaSegManual = value
        End Set
    End Property

    Public Property PermiteSeguroFinanciado() As Single
        Get
            Return sngSeguroFinanciado
        End Get
        Set(ByVal value As Single)
            sngSeguroFinanciado = value
        End Set
    End Property

    Public Property PermiteSeguroContado() As Single
        Get
            Return sngSeguroContado
        End Get
        Set(ByVal value As Single)
            sngSeguroContado = value
        End Set
    End Property

    Public Property PermiteSeguroCuentaCliente() As Single
        Get
            Return sngSeguroCuentaCte
        End Get
        Set(ByVal value As Single)
            sngSeguroCuentaCte = value
        End Set
    End Property

    Public Property PermiteSeguroMultianualFinanciado() As Single
        Get
            Return sngSeguroMultiFinan
        End Get
        Set(ByVal value As Single)
            sngSeguroMultiFinan = value
        End Set
    End Property

    Public Property PermiteSeguroMultianualContado() As Single
        Get
            Return sngSeguroMultiCont
        End Get
        Set(ByVal value As Single)
            sngSeguroMultiCont = value
        End Set
    End Property

    Public Property PermiteSeguroMultianualFinanciadoAñoGratis() As Single
        Get
            Return sngSeguroMultiFinanAnioGratis
        End Get
        Set(ByVal value As Single)
            sngSeguroMultiFinanAnioGratis = value
        End Set
    End Property

    Public Property PermiteSeguroMultianualContadoAñoGratis() As Single
        Get
            Return sngSeguroMultiContAnioGratis
        End Get
        Set(ByVal value As Single)
            sngSeguroMultiContAnioGratis = value
        End Set
    End Property

    Public Property PermiteSeguroRegalado() As Single
        Get
            Return sngSeguroRegalado
        End Get
        Set(ByVal value As Single)
            sngSeguroRegalado = value
        End Set
    End Property

    Public Property PermiteGraciaCapital() As Single
        Get
            Return sngPermiteGraciaCap
        End Get
        Set(ByVal value As Single)
            sngPermiteGraciaCap = value
        End Set
    End Property

    Public Property PermiteGraciaInteres() As Single
        Get
            Return sngPermiteGraciaInt
        End Get
        Set(ByVal value As Single)
            sngPermiteGraciaInt = value
        End Set
    End Property

    Public Property PrimerPagoIrregular() As Single
        Get
            Return sngPrimerPagoIrreg
        End Get
        Set(ByVal value As Single)
            sngPrimerPagoIrreg = value
        End Set
    End Property

    Public Property PlantillaCotizadorAbierto() As Single
        Get
            Return sngCotizadorAbierto
        End Get
        Set(ByVal value As Single)
            sngCotizadorAbierto = value
        End Set
    End Property

    Public Property CalculaIVASeguroVida() As Single
        Get
            Return sngCalculaIVASegVida
        End Get
        Set(ByVal value As Single)
            sngCalculaIVASegVida = value
        End Set
    End Property

    Public Property ManejaTasaInteresVariable() As Single
        Get
            Return sngManejaTasaIntVar
        End Get
        Set(ByVal value As Single)
            sngManejaTasaIntVar = value
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

    Public Property NombreCopia() As String
        Get
            Return strNomCopia
        End Get
        Set(ByVal value As String)
            strNomCopia = value
        End Set
    End Property

    Public Property InicioVigencia() As String
        Get
            Return strIniVig
        End Get
        Set(ByVal value As String)
            strIniVig = value
        End Set
    End Property

    Public Property FinVigencia() As String
        Get
            Return strFinVig
        End Get
        Set(ByVal value As String)
            strFinVig = value
        End Set
    End Property

    Public Property FechaVigenciaActual() As String
        Get
            Return strFecVigAct
        End Get
        Set(ByVal value As String)
            strFecVigAct = value
        End Set
    End Property

    Public Property PtjMaximoAccesorios() As Double
        Get
            Return dblPtjMaxAcc
        End Get
        Set(ByVal value As Double)
            dblPtjMaxAcc = value
        End Set
    End Property

    Public Property PtjEngancheMinimo() As Double
        Get
            Return dblEnganche
        End Get
        Set(ByVal value As Double)
            dblEnganche = value
        End Set
    End Property

    Public Property PtjServiciosFinancieros() As Double
        Get
            Return dblServFinan
        End Get
        Set(ByVal value As Double)
            dblServFinan = value
        End Set
    End Property

    Public Property TasaNominal() As Double
        Get
            Return dblTasaNom
        End Get
        Set(ByVal value As Double)
            dblTasaNom = value
        End Set
    End Property
    Public Property TasaNominalDos() As Double
        Get
            Return dblTasaNomDos
        End Get
        Set(ByVal value As Double)
            dblTasaNomDos = value
        End Set
    End Property
    Public Property PtjServiciosFinancierosDos() As Double
        Get
            Return dblServFinanDos
        End Get
        Set(ByVal value As Double)
            dblServFinanDos = value
        End Set
    End Property

    Public Property TasaNominalSeguro() As Double
        Get
            Return dblTasaNomSeg
        End Get
        Set(ByVal value As Double)
            dblTasaNomSeg = value
        End Set
    End Property

    Public Property PuntosSeguroCliente() As Double
        Get
            Return dblPtosSegCte
        End Get
        Set(ByVal value As Double)
            dblPtosSegCte = value
        End Set
    End Property

    Public Property TasaIVA() As Double
        Get
            Return dblTasaIva
        End Get
        Set(ByVal value As Double)
            dblTasaIva = value
        End Set
    End Property

    Public Property FactorSeguroVida() As Double
        Get
            Return dblFactSegVida
        End Get
        Set(ByVal value As Double)
            dblFactSegVida = value
        End Set
    End Property

    Public Property TasaPCP() As Double
        Get
            Return dblTasaPCP
        End Get
        Set(ByVal value As Double)
            dblTasaPCP = value
        End Set
    End Property

    Public Property PtjBlindDiscount() As Double
        Get
            Return dblPtjBlindDiscount
        End Get
        Set(ByVal value As Double)
            dblPtjBlindDiscount = value
        End Set
    End Property

    Public Property PtjOpcionCompra() As Double
        Get
            Return dblPtjOpcionCompra
        End Get
        Set(ByVal value As Double)
            dblPtjOpcionCompra = value
        End Set
    End Property

    Public Property IncentivoVentas() As Double
        Get
            Return dblIncentivoVtas
        End Get
        Set(ByVal value As Double)
            dblIncentivoVtas = value
        End Set
    End Property

    Public Property IDAseguradora() As Integer
        Get
            Return intAseguradora
        End Get
        Set(ByVal value As Integer)
            intAseguradora = value
        End Set
    End Property

    Public Property IDPaqueteSeguro() As Integer
        Get
            Return intPaqueteSeguro
        End Get
        Set(ByVal value As Integer)
            intPaqueteSeguro = value
        End Set
    End Property

    Public Property noperiodos() As Integer
        Get
            Return intnoperiodos
        End Get
        Set(ByVal value As Integer)
            intnoperiodos = value
        End Set
    End Property

    Public Property IDPROMOCION() As Integer
        Get
            Return intIDPROMOCION
        End Get
        Set(value As Integer)
            intIDPROMOCION = value
        End Set
    End Property

    Public Property IDTipoSeg() As Integer
        Get
            Return inttiposeg
        End Get
        Set(ByVal value As Integer)
            inttiposeg = value
        End Set
    End Property

    Public Property IDAgencia() As Integer
        Get
            Return intIdAgencia
        End Get
        Set(value As Integer)
            intIdAgencia = value
        End Set
    End Property

    Public Property Agencia() As String
        Get
            Return strAgencia
        End Get
        Set(value As String)
            strAgencia = value
        End Set
    End Property

    Public Property IDMarca() As Integer
        Get
            Return intIdMarca
        End Get
        Set(value As Integer)
            intIdMarca = value
        End Set
    End Property

    Public Property ProductoDesc() As String 'BBVA-P-412
        Get
            Return strProductoDesc 'BBVA-P-412
        End Get
        Set(value As String)
            strProductoDesc = value 'BBVA-P-412
        End Set
    End Property

    'BBVA-P-412
    Public Property PorPagEspeciales As Double
        Get
            Return dblptjpagesp
        End Get
        Set(value As Double)
            dblptjpagesp = value
        End Set
    End Property

    Public Property PorSubsidioArmadora As Double
        Get
            Return dblptjsubsidioArmadora
        End Get
        Set(value As Double)
            dblptjsubsidioArmadora = value
        End Set
    End Property
  

    Public Property PorSubsidioAgencia As Double
        Get
            Return dblptjsubsidioAgencia
        End Get
        Set(value As Double)
            dblptjsubsidioAgencia = value
        End Set
    End Property

    Public Property ComisionVtaSeg As Double
        Get
            Return dblptjcomisionvtaseg
        End Get
        Set(value As Double)
            dblptjcomisionvtaseg = value
        End Set
    End Property

    Public Property PorMinComisionVendedor As Double
        Get
            Return dblptjmincomisionvendedor
        End Get
        Set(value As Double)
            dblptjmincomisionvendedor = value
        End Set
    End Property

    Public Property PorMaxComisionVendedor As Double
        Get
            Return dblptjmaxcomisionvendedor
        End Get
        Set(value As Double)
            dblptjmaxcomisionvendedor = value
        End Set
    End Property

    Public Property PorMinComisionAgencia As Double
        Get
            Return dblptjmincomisionagencia
        End Get
        Set(value As Double)
            dblptjmincomisionagencia = value
        End Set
    End Property

    Public Property PorMaxComisionAgencia As Double
        Get
            Return dblptjmaxcomisionagencia
        End Get
        Set(value As Double)
            dblptjmaxcomisionagencia = value
        End Set
    End Property

    Public Property IDCanal As Integer
        Get
            Return intidcanal
        End Get
        Set(value As Integer)
            intidcanal = value
        End Set
    End Property

    Public Property IDViaCalcSegVida As Integer
        Get
            Return intidviacalsegvida
        End Get
        Set(value As Integer)
            intidviacalsegvida = value
        End Set
    End Property

    Public Property IDTipoSegVida As Integer
        Get
            Return intidtiposegvida
        End Get
        Set(value As Integer)
            intidtiposegvida = value
        End Set
    End Property

    Public Property CEROCOMISION() As Integer
        Get
            Return intCEROCOMISION
        End Get
        Set(value As Integer)
            intCEROCOMISION = value
        End Set
    End Property

    Public Property AnioModelo As Integer
        Get
            Return intAnioModelo
        End Get
        Set(value As Integer)
            intAnioModelo = value
        End Set
    End Property

    Public Property IDBroker As Integer
        Get
            Return intIdBroker
        End Get
        Set(value As Integer)
            intIdBroker = value
        End Set
    End Property

    Public Property NomAseguradora As String
        Get
            Return strNomAseguradora
        End Get
        Set(value As String)
            strNomAseguradora = value
        End Set
    End Property

    Public Property IDProdUG As Integer
        Get
            Return intidprodug
        End Get
        Set(value As Integer)
            intidprodug = value
        End Set
    End Property

    Public Property IDEsquema As Integer
        Get
            Return intidEsquema
        End Get
        Set(value As Integer)
            intidEsquema = value
        End Set
    End Property

    Public Property IDSubProdUG As String
        Get
            Return stridsubprodug
        End Get
        Set(value As String)
            stridsubprodug = value
        End Set
    End Property

    Public Property ImporteMinG As Double
        Get
            Return dblimporteming
        End Get
        Set(value As Double)
            dblimporteming = value
        End Set
    End Property

    Public Property ImporteMaxG As Double
        Get
            Return dblimportemaxg
        End Get
        Set(value As Double)
            dblimportemaxg = value
        End Set
    End Property

    Public Property IDAlianza As Integer
        Get
            Return intidAlianza
        End Get
        Set(value As Integer)
            intidAlianza = value
        End Set
    End Property

    Public Property IDDivision As Integer
        Get
            Return intidDivision
        End Get
        Set(value As Integer)
            intidDivision = value
        End Set
    End Property

    Public Property IDGrupo As Integer
        Get
            Return intidGrupo
        End Get
        Set(value As Integer)
            intidGrupo = value
        End Set
    End Property

    Public Property IDVersion As Integer
        Get
            Return intidversion
        End Get
        Set(value As Integer)
            intidversion = value
        End Set
    End Property
    Public Property IDSubmarca As Integer
        Get
            Return intidsubmarca
        End Get
        Set(value As Integer)
            intidsubmarca = value
        End Set
    End Property


    Public Sub CargaPaquete(Optional ByVal intPaq As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            If intPaq > 0 Then
                intPaquete = intPaq
                dtsRes = ManejaPaquete(1)
                intPaquete = 0
                If Trim$(strErrPaquete) = "" Then
                    If dtsRes.Tables(0).Rows.Count > 0 Then
                        intPaquete = dtsRes.Tables(0).Rows(0).Item("ID_PAQUETE")
                        intMoneda = dtsRes.Tables(0).Rows(0).Item("ID_MONEDA")
                        intTipoOper = dtsRes.Tables(0).Rows(0).Item("ID_TIPO_OPERACION")
                        intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                        intCalendario = dtsRes.Tables(0).Rows(0).Item("ID_CALENDARIO")
                        intPeriodicidad = dtsRes.Tables(0).Rows(0).Item("ID_PERIODICIDAD")
                        intTipoCalc = dtsRes.Tables(0).Rows(0).Item("ID_TIPO_CALCULO")
                        intTipoSegVida = dtsRes.Tables(0).Rows(0).Item("ID_TIPO_SEGURO_VIDA")
                        intPlazoSegRegalado = dtsRes.Tables(0).Rows(0).Item("ID_PLAZO_SEG_REGALADO")
                        intClasifProd = dtsRes.Tables(0).Rows(0).Item("ID_CLASIFICACION_PROD")
                        intTipoProducto = dtsRes.Tables(0).Rows(0).Item("ID_TIPO_PRODUCTO")
                        intPerJurid = dtsRes.Tables(0).Rows(0).Item("ID_PER_JURIDICA")
                        intTipoCalcSeg = dtsRes.Tables(0).Rows(0).Item("ID_TIPO_CALCULO_SEGURO")
                        intIDTipoVenc = dtsRes.Tables(0).Rows(0).Item("ID_TIPO_VENCIMIENTO")
                        intDepGarMax = dtsRes.Tables(0).Rows(0).Item("RENTAS_DEPOSITO_MAX")
                        intDepGarMin = dtsRes.Tables(0).Rows(0).Item("RENTAS_DEPOSITO_MIN")
                        intIDTasaIntVar = dtsRes.Tables(0).Rows(0).Item("ID_TASA_INTERES_VARIABLE")
                        sngManejaTasaIntVar = dtsRes.Tables(0).Rows(0).Item("MANEJA_TASA_INTERES_VARIABLE")
                        sngRegDef = dtsRes.Tables(0).Rows(0).Item("REG_DEFAULT")
                        sngPagosEspeciales = dtsRes.Tables(0).Rows(0).Item("PERMITE_PAGOS_ESPECIALES")
                        sngSeguroFinanciado = dtsRes.Tables(0).Rows(0).Item("PERMITE_SEGURO_FINANCIADO")
                        sngSeguroContado = dtsRes.Tables(0).Rows(0).Item("PERMITE_SEGURO_CONTADO")
                        sngSeguroCuentaCte = dtsRes.Tables(0).Rows(0).Item("PERMITE_SEGURO_CLIENTE")
                        sngSeguroMultiFinan = dtsRes.Tables(0).Rows(0).Item("PERMITE_SEGURO_MULTIANUAL_FINANCIADO")
                        sngSeguroMultiCont = dtsRes.Tables(0).Rows(0).Item("PERMITE_SEGURO_MULTIANUAL_CONTADO")
                        sngSeguroMultiFinanAnioGratis = dtsRes.Tables(0).Rows(0).Item("PERMITE_SEGURO_MULTIANUAL_FINANCIADO_ANIO_GRATIS")
                        sngSeguroMultiContAnioGratis = dtsRes.Tables(0).Rows(0).Item("PERMITE_SEGURO_MULTIANUAL_CONTADO_ANIO_GRATIS")
                        sngSeguroRegalado = dtsRes.Tables(0).Rows(0).Item("PERMITE_SEGURO_REGALADO")
                        sngPrimerPagoIrreg = dtsRes.Tables(0).Rows(0).Item("PRIMER_PAGO_IRREG")
                        sngConsideraEngancheAcc = dtsRes.Tables(0).Rows(0).Item("CONSIDERA_ENGANCHE_ACCESORIOS")
                        sngPermiteMtoSubsidio = dtsRes.Tables(0).Rows(0).Item("PERMITE_MONTO_SUBSIDIO")
                        sngPermiteCapturaSegManual = dtsRes.Tables(0).Rows(0).Item("PERMITE_CAPTURA_SEGURO_MANUAL")
                        sngPermiteGraciaCap = dtsRes.Tables(0).Rows(0).Item("PERMITE_GRACIA_CAPITAL")
                        sngPermiteGraciaInt = dtsRes.Tables(0).Rows(0).Item("PERMITE_GRACIA_INTERES") 'Error tenia PERMITE_GRACIA_CAPITAL en lugar de PERMITE_GRACIA_INTERES
                        sngCotizadorAbierto = dtsRes.Tables(0).Rows(0).Item("PLANTILLA_COTIZADOR_ABIERTO")
                        sngCalculaIVASegVida = dtsRes.Tables(0).Rows(0).Item("CALCULAR_IVA_SEGURO_VIDA")
                        sngSelecDiaPago = dtsRes.Tables(0).Rows(0).Item("PERMITE_DIA_PAGO")
                        sngPermitePtjSubsidio = dtsRes.Tables(0).Rows(0).Item("PERMITE_PTJ_SUBSIDIO")
                        strNombre = dtsRes.Tables(0).Rows(0).Item("NOMBRE")
                        strIniVig = dtsRes.Tables(0).Rows(0).Item("INI_VIG")
                        strFinVig = dtsRes.Tables(0).Rows(0).Item("FIN_VIG")
                        dblTasaIva = dtsRes.Tables(0).Rows(0).Item("TASA_IVA")
                        dblServFinan = dtsRes.Tables(0).Rows(0).Item("PTJ_SERV_FINAN")
                        dblFactSegVida = dtsRes.Tables(0).Rows(0).Item("FACTOR_SEGURO_VIDA")
                        dblPtjMaxAcc = dtsRes.Tables(0).Rows(0).Item("PTJ_MAX_ACCESORIOS")
                        dblIncentivoVtas = dtsRes.Tables(0).Rows(0).Item("INCENTIVO_VENTAS")
                        intnoperiodos = dtsRes.Tables(0).Rows(0).Item("NOPERIODOS")
                        intIDPROMOCION = dtsRes.Tables(0).Rows(0).Item("ID_PROMOCION")
                        'BBVA-P-412
                        dblptjpagesp = dtsRes.Tables(0).Rows(0).Item("PTJ_PAGOS_ESPECIALES")
                        dblptjsubsidioArmadora = dtsRes.Tables(0).Rows(0).Item("PTJ_SUBSIDIO_ARMADORA")
                        dblptjsubsidioAgencia = dtsRes.Tables(0).Rows(0).Item("PTJ_SUBSIDIO_AGENCIA")
                        dblptjcomisionvtaseg = dtsRes.Tables(0).Rows(0).Item("PTJ_COMISION_VTASEG")
                        dblptjmincomisionvendedor = dtsRes.Tables(0).Rows(0).Item("PTJ_MIN_COMISION_VENDEDOR")
                        dblptjmaxcomisionvendedor = dtsRes.Tables(0).Rows(0).Item("PTJ_MAX_COMISION_VENDEDOR")
                        dblptjmincomisionagencia = dtsRes.Tables(0).Rows(0).Item("PTJ_MIN_COMISION_AGENCIA")
                        dblptjmaxcomisionagencia = dtsRes.Tables(0).Rows(0).Item("PTJ_MAX_COMISION_AGENCIA")
                        intidviacalsegvida = dtsRes.Tables(0).Rows(0).Item("VIA_CALC_SEG_VIDA")
                        intCEROCOMISION = dtsRes.Tables(0).Rows(0).Item("CERO_COMISION")
                        intidprodug = dtsRes.Tables(0).Rows(0).Item("ID_PRODUCTO_UG")
                        stridsubprodug = dtsRes.Tables(0).Rows(0).Item("ID_SUBPRODUCTO_UG")
                        dblimporteming = dtsRes.Tables(0).Rows(0).Item("IMPORTEMING")
                        dblimportemaxg = dtsRes.Tables(0).Rows(0).Item("IMPORTEMAXG")
                        intidEsquema = dtsRes.Tables(0).Rows(0).Item("ID_Esquema")
                    Else
                        strErrPaquete = "No se encontró información para poder cargar el paquete"
                    End If
                End If
            End If
        Catch ex As Exception
            strErrPaquete = ex.Message
        End Try
    End Sub

    Public Function ManejaPaquete(ByVal intOper As Integer, Optional ByVal conexion As clsConexion = Nothing) As DataSet
        strErrPaquete = ""
        ManejaPaquete = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As clsConexion ' New clsConexion

            If conexion Is Nothing Then
                objSD = New clsConexion
            Else
                objSD = conexion
            End If

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' Consulta 
                    If intPaquete > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    If intMoneda > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMoneda", intMoneda.ToString)
                    If intTipoOper > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipOper", intTipoOper.ToString)
                    If intTipoProducto > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipProd", intTipoProducto.ToString)
                    If intPerJurid > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPerJurid", intPerJurid.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If intClasifProd > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", intClasifProd.ToString)
                    If Trim(strNombre) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    If Trim(strFecVigAct) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "fecVigActual", strFecVigAct)
                    If sngCotizadorAbierto > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "plantillaCotAbierto", sngCotizadorAbierto.ToString)
                    'BBVA-P-412
                Case 2 ' Inserta
                    If intPlazoSegRegalado > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPlazoSegRegalado", intPlazoSegRegalado.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMoneda", intMoneda.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", intClasifProd.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipProd", intTipoProducto.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPerJurid", intPerJurid.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTasaIntVar", intIDTasaIntVar.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "iniVig", strIniVig)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "finVig", strFinVig)
                    ArmaParametros(strParamStored, TipoDato.Doble, "tasaIva", dblTasaIva.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjServFinan", dblServFinan.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjMaxAccesorios", dblPtjMaxAcc.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCalendario", intCalendario.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPeriodicidad", intPeriodicidad.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipCalculo", intTipoCalc.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipCalcSeg", intTipoCalcSeg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoVencimiento", intIDTipoVenc.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "rentasDepMax", intDepGarMax.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "rentasDepMin", intDepGarMin.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "primerPagoIrreg", sngPrimerPagoIrreg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permitePagEsp", sngPagosEspeciales.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteMtoSubsidio", sngPermiteMtoSubsidio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permitePtjSubsidio", sngPermitePtjSubsidio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteSegFinanciado", sngSeguroFinanciado.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteSegContado", sngSeguroContado.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteSegCte", sngSeguroCuentaCte.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteSegMultiFinan", sngSeguroMultiFinan.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteSegMultiContado", sngSeguroMultiCont.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteSegMultiFinanAnioGratis", sngSeguroMultiFinanAnioGratis.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteSegMultiContadoAnioGratis", sngSeguroMultiContAnioGratis.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteSegRegalado", sngSeguroRegalado.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "consideraAccesoriosEnEnganche", sngConsideraEngancheAcc.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteCapturaSeguro", sngPermiteCapturaSegManual.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteGraciaCap", sngPermiteGraciaCap.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteGraciaInt", sngPermiteGraciaInt.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "plantillaCotAbierto", sngCotizadorAbierto.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "calculaIVASegVida", sngCalculaIVASegVida.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteDiaPago", sngSelecDiaPago.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "manejaTasaIntVar", sngManejaTasaIntVar.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "factSegVid", dblFactSegVida.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "incentivoVentas", dblIncentivoVtas.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAplicaSegVida", intTipoSegVida.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipOper", intTipoOper.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    ArmaParametros(strParamStored, TipoDato.Entero, "noperiodos", intnoperiodos.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_PROMOCION", intIDPROMOCION.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjpagosesp", dblptjpagesp.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjsubsidioarmadora", dblptjsubsidioArmadora.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjsubsidioagencia", dblptjsubsidioAgencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjcomisionvtaseg", dblptjcomisionvtaseg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjmincomvendedor", dblptjmincomisionvendedor.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjmaxcomvendedor", dblptjmaxcomisionvendedor.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjmincomagencia", dblptjmincomisionagencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjmaxcomagencia", dblptjmaxcomisionagencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idviacalcsegvida", intidviacalsegvida.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "CERO_COMISION", intCEROCOMISION.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idprodug", intidprodug.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idEsquema", intidEsquema.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "idsuborodug", stridsubprodug)
                    ArmaParametros(strParamStored, TipoDato.Doble, "importeming", dblimporteming.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "importemaxg", dblimportemaxg.ToString)
                Case 3 ' actualiza
                    If intPlazoSegRegalado > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPlazoSegRegalado", intPlazoSegRegalado.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMoneda", intMoneda.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", intClasifProd.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipProd", intTipoProducto.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPerJurid", intPerJurid.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTasaIntVar", intIDTasaIntVar.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "iniVig", strIniVig)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "finVig", strFinVig)
                    ArmaParametros(strParamStored, TipoDato.Doble, "tasaIva", dblTasaIva.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjServFinan", dblServFinan.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjMaxAccesorios", dblPtjMaxAcc.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCalendario", intCalendario.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPeriodicidad", intPeriodicidad.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipCalculo", intTipoCalc.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipCalcSeg", intTipoCalcSeg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoVencimiento", intIDTipoVenc.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "rentasDepMax", intDepGarMax.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "rentasDepMin", intDepGarMin.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "primerPagoIrreg", sngPrimerPagoIrreg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permitePagEsp", sngPagosEspeciales.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteMtoSubsidio", sngPermiteMtoSubsidio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permitePtjSubsidio", sngPermitePtjSubsidio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteSegFinanciado", sngSeguroFinanciado.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteSegContado", sngSeguroContado.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteSegCte", sngSeguroCuentaCte.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteSegMultiFinan", sngSeguroMultiFinan.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteSegMultiContado", sngSeguroMultiCont.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteSegMultiFinanAnioGratis", sngSeguroMultiFinanAnioGratis.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteSegMultiContadoAnioGratis", sngSeguroMultiContAnioGratis.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteSegRegalado", sngSeguroRegalado.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "consideraAccesoriosEnEnganche", sngConsideraEngancheAcc.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteCapturaSeguro", sngPermiteCapturaSegManual.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteGraciaCap", sngPermiteGraciaCap.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteGraciaInt", sngPermiteGraciaInt.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "plantillaCotAbierto", sngCotizadorAbierto.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "calculaIVASegVida", sngCalculaIVASegVida.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "permiteDiaPago", sngSelecDiaPago.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "manejaTasaIntVar", sngManejaTasaIntVar.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "factSegVid", dblFactSegVida.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "incentivoVentas", dblIncentivoVtas.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAplicaSegVida", intTipoSegVida.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipOper", intTipoOper.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "noperiodos", intnoperiodos.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_PROMOCION", intIDPROMOCION.ToString)
                    'BBVA-P-412
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjpagosesp", dblptjpagesp.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjsubsidioarmadora", dblptjsubsidioArmadora.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjsubsidioagencia", dblptjsubsidioAgencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjcomisionvtaseg", dblptjcomisionvtaseg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjmincomvendedor", dblptjmincomisionvendedor.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjmaxcomvendedor", dblptjmaxcomisionvendedor.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjmincomagencia", dblptjmincomisionagencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjmaxcomagencia", dblptjmaxcomisionagencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idviacalcsegvida", intidviacalsegvida.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "CERO_COMISION", intCEROCOMISION.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idprodug", intidprodug.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idEsquema", intidEsquema.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "idsuborodug", stridsubprodug)
                    ArmaParametros(strParamStored, TipoDato.Doble, "importeming", dblimporteming.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "importemaxg", dblimportemaxg.ToString)
                Case 4 ' Borra
                    'BBVA-P-412
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                Case 5 ' borra plazos
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                Case 6 ' inserta plazo
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPlazo", intPlazo.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "tasaNominal", dblTasaNom.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "tasaNominalDos", dblTasaNomDos.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjServFinanDos", dblServFinanDos.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "tasaNominalSeg", dblTasaNomSeg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjEnganche", dblEnganche.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "puntosSegCte", dblPtosSegCte.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjServFinan", dblServFinan.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "tasaPCP", dblTasaPCP.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjOpcCompra", dblPtjOpcionCompra.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjBlindDisc", dblPtjBlindDiscount.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatPlazo", intEstatusPlazo.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 7 ' consulta plazos
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPeriodicidad", intPeriodicidad.ToString)
                    If intPlazo > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPlazo", intPlazo.ToString)
                    If intEstatusPlazo > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatPlazo", intEstatusPlazo.ToString)
                Case 8 ' copia paquete
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nomCopia", strNomCopia)
                Case 9 ' borra relacion paquete - producto
                    If intPaquete > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    If intProducto > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idProducto", intProducto.ToString)
                    If intIdMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intIdMarca.ToString)
                    If intAnioModelo > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "anio", intAnioModelo.ToString)
                    If intidsubmarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idsubmarca", intidsubmarca.ToString)
                    If intClasifProd > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", intClasifProd.ToString)
                    If intIdBroker > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idbroker", intIdBroker.ToString)
                    'BUG-PC-65:MPUESTO:19/05/2017:CORRECCION DE ADICION Y BORRADO DE RELACION PAQUETES-PRODUCTOS
                    If strProductoDesc.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "prodDescripcion", strProductoDesc)
                Case 10 ' inserta relacion paquete - producto
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idProducto", intProducto.ToString)
                    If intIdMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intIdMarca.ToString)
                    If intAnioModelo > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "anio", intAnioModelo.ToString)
                    If intidsubmarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idsubmarca", intidsubmarca.ToString)
                    If intClasifProd > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", intClasifProd.ToString)
                    If intIdBroker > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idbroker", intIdBroker.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    'BUG-PC-65:MPUESTO:19/05/2017:CORRECCION DE ADICION Y BORRADO DE RELACION PAQUETES-PRODUCTOS
                    If strProductoDesc.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "prodDescripcion", strProductoDesc)
                Case 11 ' consulta relacion paquete - producto
                    If intPaquete > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    If intIdMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intIdMarca.ToString)
                    If strProductoDesc.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "prodDescripcion", strProductoDesc) 'BBVA-P-412
                    If intProducto > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idProducto", intProducto.ToString)
                    If intEstatusOtro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                    If intAnioModelo > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "anio", intAnioModelo.ToString)
                    If intidsubmarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idsubmarca", intidsubmarca.ToString)
                    If intClasifProd > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", intClasifProd.ToString)
                    If intIdBroker > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idbroker", intIdBroker.ToString) 'BBVA-P-412
                Case 12 ' borra relacion paquete - paquete seguro
                    If intPaquete > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                Case 13 ' inserta relacion paquete - paquete seguro
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idtiposeg", inttiposeg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 14 ' Consulta Paquete Seguro
                    If intPaquete > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If intEstatusOtro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                    If inttiposeg > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idtiposeg", inttiposeg.ToString)
                Case 16, 19, 22, 25, 28 ' borra tipos persona, clasificacion, monedas, tipos producto, tipos operacion
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                Case 17 ' inserta tipos persona
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPerJurid", intPerJurid.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 18 ' Consulta Tipos Persona
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    If intPerJurid > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPerJurid", intPerJurid.ToString)
                    If intEstatusOtro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                Case 20 ' inserta clasificacion producto
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", intClasifProd.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 21 ' Consulta clasificacion producto
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    If intClasifProd > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", intClasifProd.ToString)
                    If intEstatusOtro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                Case 26 ' inserta tipos producto
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipProd", intTipoProducto.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 27 ' Consulta Tipos Producto
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    If intTipoProducto > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipProd", intTipoProducto.ToString)
                    If intEstatusOtro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                Case 29 ' inserta tipos operacion
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipOper", intTipoOper.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 30 ' Consulta Tipos Operacion
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    If intTipoOper > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipOper", intTipoOper.ToString)
                    If intEstatusOtro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                Case 33 ' consulta Promociones
                    If intPaquete > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    If intIDPROMOCION > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "ID_PROMOCION", intIDPROMOCION.ToString)
                    If intPeriodicidad > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPeriodicidad", intPeriodicidad.ToString)
                Case 34 'Consulta plazos configuración
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPeriodicidad", intPeriodicidad.ToString)
                    If intPlazo > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPlazo", intPlazo.ToString)
                    If intEstatusPlazo > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatPlazo", intEstatusPlazo.ToString)
                Case 35 'Consulta relación Paquetes - Agencias
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    If intIdAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intIdAgencia.ToString)
                    If strAgencia.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "agencia", strAgencia)
                    If intidAlianza > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", intidAlianza.ToString)
                    If intidDivision > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idDivision", intidDivision.ToString)
                    If intidGrupo > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idGrupo", intidGrupo.ToString)
                    If intidEsquema > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idEsquema", intidEsquema.ToString)
                Case 36 'Inserta relación Paquetes - Agencias
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    If intIdAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intIdAgencia.ToString)
                    If strUsuReg.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    If strAgencia.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "agencia", strAgencia)
                    If intidAlianza > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", intidAlianza.ToString)
                    If intidDivision > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idDivision", intidDivision.ToString)
                    If intidGrupo > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idGrupo", intidGrupo.ToString)
                Case 37 'Borra relación Paquetes - Agencias
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    If intIdAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intIdAgencia.ToString)
                    If strAgencia.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "agencia", strAgencia)
                    If intidAlianza > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", intidAlianza.ToString)
                    If intidDivision > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idDivision", intidDivision.ToString)
                    If intidGrupo > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idGrupo", intidGrupo.ToString)
                Case 38 'Consulta Cotizador
                    If intMoneda > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMoneda", intMoneda.ToString)
                    If intTipoOper > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipOper", intTipoOper.ToString)
                    If intPerJurid > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPerJurid", intPerJurid.ToString)
                    If intProducto > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idProducto", intProducto.ToString)
                    If intIdAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intIdAgencia.ToString)
                    If intClasifProd > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", intClasifProd.ToString)
                Case 39 'Guarda Cotiza
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPlazo", intPlazo.ToString)
                Case 40 'Consulta paquetes - seguros cotizador
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    'BBVA-P-412
                Case 41 'Borra Paquetes - Canales
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                Case 42 'Inserta Paquetes - Canales
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCanal", intidcanal.ToString)
                    If intEstatusOtro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 43 'Consulta Paquetes - Canales
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                Case 44 ' Consulta Paquetes - Seguro de Vida
                    If intPaquete > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If intEstatusOtro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                    If inttiposeg > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idtiposegvida", inttiposeg.ToString)
                Case 45 'Inserta Paquetes - Seguro de Vida
                    If intPaquete > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    If intidtiposegvida > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idtiposegvida", intidtiposegvida.ToString)
                    If intEstatusOtro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 46 'Borra Paquetes - Seguro de Vida
                    If intPaquete > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                Case 47 'Consulta Relación Paquetes - Aseguradoras
                    If intPaquete > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    If intIdBroker > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idbroker", intIdBroker.ToString)
                    If intAseguradora > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAseg", intAseguradora.ToString)
                    If strNomAseguradora.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nomaseg", strNomAseguradora)
                Case 48 'Inserta Relación Paquetes - Aseguradoras
                    If intPaquete > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    If intIdBroker > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idbroker", intIdBroker.ToString)
                    If intAseguradora > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAseg", intAseguradora.ToString)
                    If strUsuReg.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 49 'Borra Relación Paquetes - Aseguradoras
                    If intPaquete > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    If intIdBroker > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idbroker", intIdBroker.ToString)
                    If intAseguradora > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAseg", intAseguradora.ToString)
                Case 50 'Consulta Compra Inteligente
                    'BUG-PC-34 MAUT 12/01/2017 Se cambia idAgencia por IdPaquete
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idversion", intidversion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPlazo", intPlazo.ToString)
                Case 52 'Consulta paquetes - seguros de vida cotizador
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
            End Select

            ManejaPaquete = objSD.EjecutaStoredProcedure("spManejaPaquetes", strErrPaquete, strParamStored)
            If strErrPaquete = "" Then
                If intOper = 2 Then
                    intPaquete = ManejaPaquete.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("PAQUETES", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrPaquete = ex.Message
        End Try
    End Function

    Public Function consultaWS(opc As Integer) As DataSet

        Try
            consultaWS = New DataSet

            Dim strsql As StringBuilder = New StringBuilder()
            Dim objSD As clsConexion = New clsConexion()

            Select Case opc
                Case 1 ''getloanproducts
                    strsql.Append("SELECT * FROM CATALOGO_BANCOMER WHERE RECURSOWEB = ")
                    strsql.Append("'getLoanProducts'")
                    strsql.Append("AND ESTATUS = ")
                    strsql.Append("2")
                    consultaWS = objSD.EjecutaQueryConsulta(strsql.ToString())
            End Select

        Catch ex As Exception
            strErrPaquete = "Error al obtener información."
            Return Nothing
        End Try
    End Function
End Class
