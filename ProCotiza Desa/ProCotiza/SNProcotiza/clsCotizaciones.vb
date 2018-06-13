'BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.
'BBVA-P-412: 25-08-2016: AMR: RQCOT-01 Ajustes a la pantalla de Cotización.
'BBV-P-412 RQCOT-04:AVH: 20/09/2016 SE AGREGAN FILTROS DE BUSQUEDA, RECOTIZACION SE AGREGA OPCION 31
'BBVA-P-412 RQ06: AMR: 20/09/2016 Administración de Planes de Financiamiento: Promociones
'BBVA-P-412 RQ WSC AVH: 20/10/2016 SE INSERTAN PLAZOS DE LA COTIZACION 
'BUG-PC-45: AVH: 07/02/2017 SE QUITA PARAMETRO EN ObtenFechasPagos
'BBV-P412 RQCOT-01.2: AMR 23/02/2017: Seguros.
'BUG-PC-48 JRHM 28/02/17 SE CAMBIAN OPCIONES PARA CONTROLAR EL ENGANCHE CI
'BUG-PC-49: MAPH: 28/02/2017 Modificación del cálculo del CAT y período irregular sólo de accesorios financiados
'BUG-PC-50 MAPH 28/03/2016 Comisión por apertura en el CAT
'BUG-PC-53 AMR 21/04/2017 Correcciones Multicotiza.
'BUG-PC-69:MPUESTO:30/05/2017:CORRECCION DEL CAT Y COMISIÓN POR APERTURA
'BUG-PC-81: RHERNANDEZ: 29/06/17: SE ENVIO TIPO DE SEGURO DE VIDA A LA CARGA DE DATOS AL MULTICOTIZA PARA EVALUAR SI ES CONTADO
'RQ-SEGIP : RHERNANDEZ: 19/07/17: SE AGREGA A COTIZADOR EL ID PRODUCTO PARA EL CALCULO DE SEGUROS POR FACTOR CON CONSTANTE DINAMICA
'RQ-MN2-4 : CGARCIA: 11/09/2017 :  SE AGREGO LA PARTE DE GUARDAR EL DEDUCIBLE 
'RQ-MN2-4.2 : CGARCIA: 28/09/2017 :  SE ELIMINO EL TIPO DE DEDUCIBLE  
'-- RQ-PI7-PC3: CGARCIA: 09/10/2017: CREACION DE NUEVA ALIANZA KIA PARA AGENCIA KIA, INDIAN, POLARIAS Y DEDUCIBLES
'AUTOMIK-TASK-29:MPUESTO:06/10/2017: Adición de strDescVersion, AutomikUser y intIDTipoUnidad para InstallmentCalculation (Calculadora de cotizaciones)
'RQMN2-2.3: ERODRIGUEZ : 30/10/2017:  Se agrego funcion para calculat cat dos.
'BUG-PC-125  RIGLESIAS: 14/11/2017:      Sa agrego nuevo combolist para indemnización  
'RQ-PI7-PC7: CGARCIA: 09/01/2017: SE MODIFICA CLASE PARA MANDAR EN SERVICIO DE COTIZACION EL ID DE COBERTURAS QUE ARROJA EL WS DE SUBPLANES.
'AUTOMIK-BUG-453: RHERNANDEZ: 17/05/18: SE MODIFICA LA CLASE PARA QUE EN EL MULTICOTIZADOR SOLO COTICE 1 PLAZO
'BUG-PC-197: CGARCIA: 22/05/2018: SE AGREGA CLASIFICACION DEL PRODUCTO EN LA OPCION 28 DE CARGA DE COBERTURAS.
Imports System.Data
Imports SDManejaBD

Public Class clsCotizaciones
    Inherits clsSession

#Region "Variables"

    Private strErrCotiza As String = ""

    Private intCotiza As Integer = 0
    Private intPeriodoPagEsp As Integer = 0
    Private intEmpresa As Integer = 0
    Private intAgencia As Integer = 0
    Private intAsesor As Integer = 0
    Private intPromotor As Integer = 0
    Private intVendedor As Integer = 0
    Private intPerJur As Integer = 0
    Private intTipoProd As Integer = 0
    Private intMarca As Integer = 0
    Private intClasif As Integer = 0
    Private intProducto As Integer = 0
    Private intPlazo As Integer = 0
    Private intEsqFinan As Integer = 0
    Private intCalendario As Integer = 0
    Private intPeriodicidad As Integer = 0
    Private intTipoSeg As Integer = 0
    Private intAseguradora As Integer = 0
    Private intEstado As Integer = 0
    Private intPlazoSeg As Integer = 0
    Private intAccesorio As Integer = 0
    Private intAccesorioCot As Integer = 0
    Private intMoneda As Integer = 0
    Private intPaquete As Integer = 0
    Private intTipoSegVida As Integer = 0
    Private intPersonaCte As Integer = 0
    Private intFolioCot As Integer = 0
    Private intTipoOper As Integer = 0
    Private intPaqueteSeg As Integer = 0
    Private intTipoCot As Integer = 0
    Private intTipoVenc As Integer = 0
    Private intIDTasaIva As Integer = 0
    Private intGiro As Integer = 0
    Private intMonedaFact As Integer = 0
    Private intPagosGraCap As Integer = 0
    Private intPagosGraInt As Integer = 0
    Private intValorPlazo As Integer = 0
    Private intUnidadesProd As Integer = 0
    Private intRentasDep As Integer = 0
    Private intAnioUsados As Integer = 0
    Private intValorPlazoSeguro As Integer = 0
    Private intVigenciaCot As Integer = 0
    Private intTipoCalculoSeguro As Integer = 0
    Private intDiaPago As Integer = 0
    Private intIDTasaIntVar As Integer = 0
    Private intIDTipoUnidad As Integer

    Private sngAccContado As Single = 0
    Private sngPriPagoIrreg As Single = 0
    Private sngCalculaPagoIni As Single = 0
    Private sngUsaTasaPCP As Single = 0
    Private sngConsideraEngAcc As Single = 0
    Private sngCalcularIVASegVida As Single = 0
    Private sngCapturaManualSeg As Single = 0
    Private sngAccAfectaSeg As Single = 0
    Private sngManejaTasaIntVar As Single = 0
    Private sngConsultaCotAbierta As Single = 0
    Private sngIncluyeRC As Single = 0

    'Private strNombre As String = ""
    'Private strPaterno As String = ""
    'Private strMaterno As String = ""
    Private strTel As String = ""
    'Private strMail As String = ""
    Private strDescAcc As String = ""
    Private strFecPagEsp As String = ""
    Private strFecIniCot As String = ""
    Private strFecFinCot As String = ""
    Private strUIDEmp As String = ""
    Private strUIDAseg As String = ""
    Private strUIDProd As String = ""
    Private strUIDMon As String = ""
    Private strUIDTipoPago As String = ""
    Private strUIDPlazo As String = ""
    Private strUIDPaqSeg As String = ""
    Private strUIDEdo As String = ""
    Private strTelMovil As String = ""
    'Private strContacto As String = ""
    Private strMarcaUsado As String = ""
    Private strProdUsado As String = ""
    Private strUIDFam As String = ""
    Private strUIDTipoProd As String = ""

    Private dblPtjServFin As Double = 0
    Private dblServFinan As Double = 0
    'Private dblIngresos As Double = 0
    Private dblPrecioLista As Double = 0
    Private dblPrecioProd As Double = 0
    Private dblMtoAccesorios As Double = 0
    Private dblMtoAccesoriosContado As Double = 0
    Private dblMtoAccesoriosNoSeg As Double = 0
    Private dblPtjEnganche As Double = 0
    Private dblPtjEngancheReal As Double = 0
    Private dblEnganche As Double = 0
    Private dblTasaIVA As Double = 0
    Private dblTasaInt As Double = 0
    Private dblTasaIntSeg As Double = 0
    Private dblMtoSeguro As Double = 0
    Private dblMtoSeguroRegalado As Double = 0
    Private dblPrecioAcc As Double = 0
    Private dblMontoPagEsp As Double = 0
    Private dblFactSegVida As Double = 0
    Private dblMontoSubsidio As Double = 0
    Private dblValorResidual As Double = 0
    Private dblMontoRentasDep As Double = 0
    Private dblPtjOpcionComp As Double = 0
    Private dblMontoOpcionComp As Double = 0
    Private dblTasaPCP As Double = 0
    Private dblPtjBlindDiscount As Double = 0
    Private dblValorTipoCambio As Double = 0
    Private dblIncentivoVtas As Double = 0
    Private dblPtjSubsidio As Double = 0
    Private dblTasaIntVar As Double = 0.0
    Private dblValorAdap As Double = 0

    'Inicia Monto Plazos seguros
    Private dblMontoSeguro1 As Double = 0
    Private dblMontoSeguro2 As Double = 0
    Private dblMontoSeguro3 As Double = 0
    'Finaliza Monto Plazos seguros

    Private dtsAccesorios As New DataSet
    Private dtsPagesp As New DataSet
    Private dblPuntosST As Double = 0


    Private intFamilia As Integer = 0
    Private intOrigen As Integer = 0

    Private intcotizador As Integer = 0
    Private intnvacot As Integer = 0
    Private inttipo As Integer = 0
    Private intuso As Integer = 0
    Private dcPRIMA_TOTAL_SG As Double = 0
    Private intIDCobertura As Integer = 0    'RQ06

    Private intBandera As Integer = 0
    Private strusureg As String = String.Empty
    Private intidversion As Integer = 0
    Private strDescVersion As String = String.Empty
    Private intTipoCalculoSeguroVida As Integer = 0

    'AVH
    Private intAlianza As Integer = -1
    Private intGrupo As Integer = -1
    Private intDivision As Integer = -1
    Private strNombre As String = ""

    Private intCondicionAcc As Integer = 0
    Private dblMonto As Double = 0 'AVH RQ WSC
    Private PlazoMod As String = ""

    Private intBroker As Integer = 0
    Private strparams As String = String.Empty

    'JRHM
    Private initEngancheCI As Double? = Nothing

    Private dbMtoGarantia As Double = 0

    'cgarcia   
    Private DecDeducible As String = String.Empty
    Private DecDeucibleRoboTotal As String = String.Empty
    Private Indemnizacion As String = String.Empty

    'mpuesto
    Private _automikUser As String
    Private _idSubMarca As Integer
    'erodriguez
    Private dblTasaCatDos As Double = 0.0

    'PARAMETRO MULTICOTIZACION (SOLO APLICABLE A AUTOMIK)
    Private init_IsMulticotizacion As Integer = 1

#End Region

    Sub New()
    End Sub

#Region "Propiedades"


    Public Property _DecDeducible() As String
        Get
            Return DecDeducible
        End Get
        Set(value As String)
            DecDeducible = value
        End Set
    End Property
    Public Property _DecDeducibleRoboTotal As String
        Get
            Return DecDeucibleRoboTotal
        End Get
        Set(value As String)
            DecDeucibleRoboTotal = value
        End Set
    End Property
    Public Property _Indemnizacion As String
        Get
            Return Indemnizacion
        End Get
        Set(value As String)
            Indemnizacion = value
        End Set
    End Property

    Public ReadOnly Property ErrorCotizacion() As String
        Get
            Return strErrCotiza
        End Get
    End Property

    Public ReadOnly Property AccesoriosCotizacion() As DataSet
        Get
            Return dtsAccesorios
        End Get
    End Property

    Public ReadOnly Property PagosEspecialesCotizacion() As DataSet
        Get
            Return dtsPagesp
        End Get
    End Property

    Public Property IDCotizacion() As Integer

        Get
            Return intCotiza
        End Get
        Set(ByVal value As Integer)
            intCotiza = value
        End Set

    End Property

    Public Property FolioCotizacion() As Integer
        Get
            Return intFolioCot
        End Get
        Set(ByVal value As Integer)
            intFolioCot = value
        End Set
    End Property

    Public Property PeriodoPagoEspecial() As Integer
        Get
            Return intPeriodoPagEsp
        End Get
        Set(ByVal value As Integer)
            intPeriodoPagEsp = value
        End Set
    End Property

    Public Property IDEmpresa() As Integer
        Get
            Return intEmpresa
        End Get
        Set(ByVal value As Integer)
            intEmpresa = value
        End Set
    End Property

    Public Property IDTipoCotizacion() As Integer
        Get
            Return intTipoCot
        End Get
        Set(ByVal value As Integer)
            intTipoCot = value
        End Set
    End Property

    Public Property IDMonedaFactura() As Integer
        Get
            Return intMonedaFact
        End Get
        Set(ByVal value As Integer)
            intMonedaFact = value
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

    Public Property IDTipoVencimiento() As Integer
        Get
            Return intTipoVenc
        End Get
        Set(ByVal value As Integer)
            intTipoVenc = value
        End Set
    End Property

    Public Property IDTasaIVA() As Integer
        Get
            Return intIDTasaIva
        End Get
        Set(ByVal value As Integer)
            intIDTasaIva = value
        End Set
    End Property

    Public Property IDAgencia() As Integer
        Get
            Return intAgencia
        End Get
        Set(ByVal value As Integer)
            intAgencia = value
        End Set
    End Property

    Public Property IDAsesor() As Integer
        Get
            Return intAsesor
        End Get
        Set(ByVal value As Integer)
            intAsesor = value
        End Set
    End Property

    Public Property IDPromotor() As Integer
        Get
            Return intPromotor
        End Get
        Set(ByVal value As Integer)
            intPromotor = value
        End Set
    End Property

    Public Property IDVendedor() As Integer
        Get
            Return intVendedor
        End Get
        Set(ByVal value As Integer)
            intVendedor = value
        End Set
    End Property

    Public Property IDPersonaCliente() As Integer
        Get
            Return intPersonaCte
        End Get
        Set(ByVal value As Integer)
            intPersonaCte = value
        End Set
    End Property

    Public Property IDPersonalidadJuridica() As Integer
        Get
            Return intPerJur
        End Get
        Set(ByVal value As Integer)
            intPerJur = value
        End Set
    End Property

    Public Property IDTipoProducto() As Integer
        Get
            Return intTipoProd
        End Get
        Set(ByVal value As Integer)
            intTipoProd = value
        End Set
    End Property

    Public Property IDMarca() As Integer
        Get
            Return intMarca
        End Get
        Set(ByVal value As Integer)
            intMarca = value
        End Set
    End Property

    Public Property IDClasificacionProd() As Integer
        Get
            Return intClasif
        End Get
        Set(ByVal value As Integer)
            intClasif = value
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

    Public Property IDAccesorio() As Integer
        Get
            Return intAccesorio
        End Get
        Set(ByVal value As Integer)
            intAccesorio = value
        End Set
    End Property

    Public Property IDAccesorioCotizacion() As Integer
        Get
            Return intAccesorioCot
        End Get
        Set(ByVal value As Integer)
            intAccesorioCot = value
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

    Public Property IDPaquete() As Integer
        Get
            Return intPaquete
        End Get
        Set(ByVal value As Integer)
            intPaquete = value
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

    Public Property IDMoneda() As Integer
        Get
            Return intMoneda
        End Get
        Set(ByVal value As Integer)
            intMoneda = value
        End Set
    End Property

    Public Property IDEsquemaFinanciamiento() As Integer
        Get
            Return intEsqFinan
        End Get
        Set(ByVal value As Integer)
            intEsqFinan = value
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

    Public Property IDTipoSeguro() As Integer
        Get
            Return intTipoSeg
        End Get
        Set(ByVal value As Integer)
            intTipoSeg = value
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

    Public Property IDEstado() As Integer
        Get
            Return intEstado
        End Get
        Set(ByVal value As Integer)
            intEstado = value
        End Set
    End Property

    Public Property IDPaqueteSeguro() As Integer
        Get
            Return intPaqueteSeg
        End Get
        Set(ByVal value As Integer)
            intPaqueteSeg = value
        End Set
    End Property

    Public Property IDPlazoSeguro() As Integer
        Get
            Return intPlazoSeg
        End Get
        Set(ByVal value As Integer)
            intPlazoSeg = value
        End Set
    End Property

    Public Property IDTipoCalculoSeguro() As Integer
        Get
            Return intTipoCalculoSeguro
        End Get
        Set(ByVal value As Integer)
            intTipoCalculoSeguro = value
        End Set
    End Property

    Public Property PagosGraciaCapital() As Integer
        Get
            Return intPagosGraCap
        End Get
        Set(ByVal value As Integer)
            intPagosGraCap = value
        End Set
    End Property

    Public Property PagosGraciaInteres() As Integer
        Get
            Return intPagosGraInt
        End Get
        Set(ByVal value As Integer)
            intPagosGraInt = value
        End Set
    End Property

    Public Property RentasDeposito() As Integer
        Get
            Return intRentasDep
        End Get
        Set(ByVal value As Integer)
            intRentasDep = value
        End Set
    End Property

    Public Property UnidadesProducto() As Integer
        Get
            Return intUnidadesProd
        End Get
        Set(ByVal value As Integer)
            intUnidadesProd = value
        End Set
    End Property

    Public Property AñoModeloUsado() As Integer
        Get
            Return intAnioUsados
        End Get
        Set(ByVal value As Integer)
            intAnioUsados = value
        End Set
    End Property

    Public Property ValorPlazo() As Integer
        Get
            Return intValorPlazo
        End Get
        Set(ByVal value As Integer)
            intValorPlazo = value
        End Set
    End Property

    Public Property ValorPlazoSeguro() As Integer
        Get
            Return intValorPlazoSeguro
        End Get
        Set(ByVal value As Integer)
            intValorPlazoSeguro = value
        End Set
    End Property

    Public Property IDVigenciaCotizacion() As Integer
        Get
            Return intVigenciaCot
        End Get
        Set(ByVal value As Integer)
            intVigenciaCot = value
        End Set
    End Property

    Public Property DiaPago() As Integer
        Get
            Return intDiaPago
        End Get
        Set(ByVal value As Integer)
            intDiaPago = value
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

    Public Property IDTipoUnidad() As Integer
        Get
            Return intIDTipoUnidad
        End Get
        Set(ByVal value As Integer)
            intIDTipoUnidad = value
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

    Public Property PrimerPagoIrregular() As Single
        Get
            Return sngPriPagoIrreg
        End Get
        Set(ByVal value As Single)
            sngPriPagoIrreg = value
        End Set
    End Property

    Public Property UsaTasaPCP() As Single
        Get
            Return sngUsaTasaPCP
        End Get
        Set(ByVal value As Single)
            sngUsaTasaPCP = value
        End Set
    End Property

    Public Property CalculaPagoInicial() As Single
        Get
            Return sngCalculaPagoIni
        End Get
        Set(ByVal value As Single)
            sngCalculaPagoIni = value
        End Set
    End Property

    Public Property ConsideraEngancheAccesorios() As Single
        Get
            Return sngConsideraEngAcc
        End Get
        Set(ByVal value As Single)
            sngConsideraEngAcc = value
        End Set
    End Property

    Public Property CalcularIVASeguroVida() As Single
        Get
            Return sngCalcularIVASegVida
        End Get
        Set(ByVal value As Single)
            sngCalcularIVASegVida = value
        End Set
    End Property

    Public Property CapturaManualSeguro() As Single
        Get
            Return sngCapturaManualSeg
        End Get
        Set(ByVal value As Single)
            sngCapturaManualSeg = value
        End Set
    End Property

    Public Property AccesorioDeContado() As Single
        Get
            Return sngAccContado
        End Get
        Set(ByVal value As Single)
            sngAccContado = value
        End Set
    End Property

    Public Property AccesorioAfectaCalculoSeguro() As Single
        Get
            Return sngAccAfectaSeg
        End Get
        Set(ByVal value As Single)
            sngAccAfectaSeg = value
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

    Public Property PermiteConsultaCotizacionAbierta() As Single
        Get
            Return sngConsultaCotAbierta
        End Get
        Set(ByVal value As Single)
            sngConsultaCotAbierta = value
        End Set
    End Property

    Public Property IncluyeRC() As Single
        Get
            Return sngIncluyeRC
        End Get
        Set(ByVal value As Single)
            sngIncluyeRC = value
        End Set
    End Property

    'Public Property Nombre() As String
    '    Get
    '        Return strNombre
    '    End Get
    '    Set(ByVal value As String)
    '        strNombre = value
    '    End Set
    'End Property

    'Public Property Paterno() As String
    '    Get
    '        Return strPaterno
    '    End Get
    '    Set(ByVal value As String)
    '        strPaterno = value
    '    End Set
    'End Property

    'Public Property Materno() As String
    '    Get
    '        Return strMaterno
    '    End Get
    '    Set(ByVal value As String)
    '        strMaterno = value
    '    End Set
    'End Property

    Public Property Telefonos() As String
        Get
            Return strTel
        End Get
        Set(ByVal value As String)
            strTel = value
        End Set
    End Property

    Public Property TelefonoMovil() As String
        Get
            Return strTelMovil
        End Get
        Set(ByVal value As String)
            strTelMovil = value
        End Set
    End Property

    'Public Property Mail() As String
    '    Get
    '        Return strMail
    '    End Get
    '    Set(ByVal value As String)
    '        strMail = value
    '    End Set
    'End Property

    'Public Property Contacto() As String
    '    Get
    '        Return strContacto
    '    End Get
    '    Set(ByVal value As String)
    '        strContacto = value
    '    End Set
    'End Property

    Public Property DescripcionAccesorio() As String
        Get
            Return strDescAcc
        End Get
        Set(ByVal value As String)
            strDescAcc = value
        End Set
    End Property

    Public Property MarcaUsados() As String
        Get
            Return strMarcaUsado
        End Get
        Set(ByVal value As String)
            strMarcaUsado = value
        End Set
    End Property

    Public Property ProductoUsados() As String
        Get
            Return strProdUsado
        End Get
        Set(ByVal value As String)
            strProdUsado = value
        End Set
    End Property

    Public Property FechaPagoEspecial() As String
        Get
            Return strFecPagEsp
        End Get
        Set(ByVal value As String)
            strFecPagEsp = value
        End Set
    End Property

    Public Property FechaInicioConsultaCotizacion() As String
        Get
            Return strFecIniCot
        End Get
        Set(ByVal value As String)
            strFecIniCot = value
        End Set
    End Property

    Public Property FechaFinalConsultaCotizacion() As String
        Get
            Return strFecFinCot
        End Get
        Set(ByVal value As String)
            strFecFinCot = value
        End Set
    End Property

    'Public Property Ingresos() As Double
    '    Get
    '        Return dblIngresos
    '    End Get
    '    Set(ByVal value As Double)
    '        dblIngresos = value
    '    End Set
    'End Property

    Public Property PrecioLista() As Double
        Get
            Return dblPrecioLista
        End Get
        Set(ByVal value As Double)
            dblPrecioLista = value
        End Set
    End Property

    Public Property PrecioProducto() As Double
        Get
            Return dblPrecioProd
        End Get
        Set(ByVal value As Double)
            dblPrecioProd = value
        End Set
    End Property

    Public Property MontoSubsidio() As Double
        Get
            Return dblMontoSubsidio
        End Get
        Set(ByVal value As Double)
            dblMontoSubsidio = value
        End Set
    End Property

    Public Property MontoAccesorios() As Double
        Get
            Return dblMtoAccesorios
        End Get
        Set(ByVal value As Double)
            dblMtoAccesorios = value
        End Set
    End Property

    Public Property MontoAccesoriosContado() As Double
        Get
            Return dblMtoAccesoriosContado
        End Get
        Set(ByVal value As Double)
            dblMtoAccesoriosContado = value
        End Set
    End Property

    Public Property MontoAccesoriosNoCalculoSeguro() As Double
        Get
            Return dblMtoAccesoriosNoSeg
        End Get
        Set(ByVal value As Double)
            dblMtoAccesoriosNoSeg = value
        End Set
    End Property

    Public Property TasaIVA() As Double
        Get
            Return dblTasaIVA
        End Get
        Set(ByVal value As Double)
            dblTasaIVA = value
        End Set
    End Property

    Public Property TasaInteres() As Double
        Get
            Return dblTasaInt
        End Get
        Set(ByVal value As Double)
            dblTasaInt = value
        End Set
    End Property

    Public Property TasaInteresSeguro() As Double
        Get
            Return dblTasaIntSeg
        End Get
        Set(ByVal value As Double)
            dblTasaIntSeg = value
        End Set
    End Property

    Public Property MontoSeguro() As Double
        Get
            Return dblMtoSeguro
        End Get
        Set(ByVal value As Double)
            dblMtoSeguro = value
        End Set
    End Property

    Public Property MontoSeguroRegalado() As Double
        Get
            Return dblMtoSeguroRegalado
        End Get
        Set(ByVal value As Double)
            dblMtoSeguroRegalado = value
        End Set
    End Property

    Public Property PtjEnganche() As Double
        Get
            Return dblPtjEnganche
        End Get
        Set(ByVal value As Double)
            dblPtjEnganche = value
        End Set
    End Property

    Public Property PtjEngancheReal() As Double
        Get
            Return dblPtjEngancheReal
        End Get
        Set(ByVal value As Double)
            dblPtjEngancheReal = value
        End Set
    End Property

    Public Property PtjSubsidio() As Double
        Get
            Return dblPtjSubsidio
        End Get
        Set(ByVal value As Double)
            dblPtjSubsidio = value
        End Set
    End Property

    Public Property MontoEnganche() As Double
        Get
            Return dblEnganche
        End Get
        Set(ByVal value As Double)
            dblEnganche = value
        End Set
    End Property

    Public Property PtjServiciosFinancieros() As Double
        Get
            Return dblPtjServFin
        End Get
        Set(ByVal value As Double)
            dblPtjServFin = value
        End Set
    End Property

    Public Property MontoServiciosFinancieros() As Double
        Get
            Return dblServFinan
        End Get
        Set(ByVal value As Double)
            dblServFinan = value
        End Set
    End Property

    Public Property PtjOpcionCompra() As Double
        Get
            Return dblPtjOpcionComp
        End Get
        Set(ByVal value As Double)
            dblPtjOpcionComp = value
        End Set
    End Property

    Public Property MontoOpcionCompra() As Double
        Get
            Return dblMontoOpcionComp
        End Get
        Set(ByVal value As Double)
            dblMontoOpcionComp = value
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

    Public Property TasaPCP() As Double
        Get
            Return dblTasaPCP
        End Get
        Set(ByVal value As Double)
            dblTasaPCP = value
        End Set
    End Property

    Public Property MontoRentasDeposito() As Double
        Get
            Return dblMontoRentasDep
        End Get
        Set(ByVal value As Double)
            dblMontoRentasDep = value
        End Set
    End Property

    Public Property ValorTipoCambio() As Double
        Get
            Return dblValorTipoCambio
        End Get
        Set(ByVal value As Double)
            dblValorTipoCambio = value
        End Set
    End Property

    Public Property MontoValorResidual() As Double
        Get
            Return dblValorResidual
        End Get
        Set(ByVal value As Double)
            dblValorResidual = value
        End Set
    End Property

    Public Property PrecioAccesorio() As Double
        Get
            Return dblPrecioAcc
        End Get
        Set(ByVal value As Double)
            dblPrecioAcc = value
        End Set
    End Property

    Public Property MontoPagoEspecial() As Double
        Get
            Return dblMontoPagEsp
        End Get
        Set(ByVal value As Double)
            dblMontoPagEsp = value
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

    Public Property IncentivoVentas() As Double
        Get
            Return dblIncentivoVtas
        End Get
        Set(ByVal value As Double)
            dblIncentivoVtas = value
        End Set
    End Property
    Public Property TasaCatDos() As Double
        Get
            Return dblTasaCatDos
        End Get
        Set(ByVal value As Double)
            dblTasaCatDos = value
        End Set
    End Property


    Public Property TasaInteresVariable() As Double
        Get
            Return dblTasaIntVar
        End Get
        Set(ByVal value As Double)
            dblTasaIntVar = value
        End Set
    End Property

    Public Property ValorAdaptacion() As Double
        Get
            Return dblValorAdap
        End Get
        Set(ByVal value As Double)
            dblValorAdap = value
        End Set
    End Property

    Public Property UIDEmpresa() As String
        Get
            Return strUIDEmp
        End Get
        Set(ByVal value As String)
            strUIDEmp = value
        End Set
    End Property

    Public Property UIDAseguradora() As String
        Get
            Return strUIDAseg
        End Get
        Set(ByVal value As String)
            strUIDAseg = value
        End Set
    End Property

    Public Property UIDProducto() As String
        Get
            Return strUIDProd
        End Get
        Set(ByVal value As String)
            strUIDProd = value
        End Set
    End Property

    Public Property UIDEstado() As String
        Get
            Return strUIDEdo
        End Get
        Set(ByVal value As String)
            strUIDEdo = value
        End Set
    End Property

    Public Property UIDPaqueteSeguro() As String
        Get
            Return strUIDPaqSeg
        End Get
        Set(ByVal value As String)
            strUIDPaqSeg = value
        End Set
    End Property

    Public Property UIDMoneda() As String
        Get
            Return strUIDMon
        End Get
        Set(ByVal value As String)
            strUIDMon = value
        End Set
    End Property

    Public Property UIDTipoPago() As String
        Get
            Return strUIDTipoPago
        End Get
        Set(ByVal value As String)
            strUIDTipoPago = value
        End Set
    End Property

    Public Property UIDPlazo() As String
        Get
            Return strUIDPlazo
        End Get
        Set(ByVal value As String)
            strUIDPlazo = value
        End Set
    End Property

    Public Property UIDFamilia() As String
        Get
            Return strUIDFam
        End Get
        Set(ByVal value As String)
            strUIDFam = value
        End Set
    End Property

    Public Property UIDTipoProducto() As String
        Get
            Return strUIDTipoProd
        End Get
        Set(ByVal value As String)
            strUIDTipoProd = value
        End Set
    End Property

    'Inicia Monto Plazos seguros
    Public Property MontoSeguro1() As Double
        Get
            Return dblMontoSeguro1
        End Get
        Set(ByVal value As Double)
            dblMontoSeguro1 = value
        End Set
    End Property

    Public Property MontoSeguro2() As Double
        Get
            Return dblMontoSeguro2
        End Get
        Set(ByVal value As Double)
            dblMontoSeguro2 = value
        End Set
    End Property
    Public Property MontoSeguro3() As Double
        Get
            Return dblMontoSeguro3
        End Get
        Set(ByVal value As Double)
            dblMontoSeguro3 = value
        End Set
    End Property


    'Finaliza Monto Plazos seguros

    'Tracker INC-P-  OECA 25-06-2012
    Public Property PuntosSobreTasa() As Double
        Get
            Return dblPuntosST
        End Get
        Set(ByVal value As Double)
            dblPuntosST = value
        End Set
    End Property
    'Tracker INC-P-  OECA 25-06-2012

    'EOST - 28082013
    'RAY-P-123 - Se agregan los campos de id_familia y id_origen
    Public Property IDFamilia() As Integer
        Get
            Return intFamilia
        End Get
        Set(ByVal value As Integer)
            intFamilia = value
        End Set
    End Property

    Public Property IDOrigen() As Integer
        Get
            Return intOrigen
        End Get
        Set(ByVal value As Integer)
            intOrigen = value
        End Set
    End Property
    Public Property IDnvacot() As Integer
        Get
            Return intnvacot
        End Get
        Set(ByVal value As Integer)
            intnvacot = value
        End Set
    End Property
    'INC-B-1987
    Public Property IDtipo() As Integer
        Get
            Return inttipo
        End Get
        Set(ByVal value As Integer)
            inttipo = value
        End Set
    End Property

    'INC-B-1987
    Public Property IDuso() As Integer
        Get
            Return intuso
        End Get
        Set(ByVal value As Integer)
            intuso = value
        End Set
    End Property
    'INC-B-2648: MAUT
    Public Property IDcotizador As Integer
        Get
            Return intcotizador
        End Get
        Set(ByVal value As Integer)
            intcotizador = value
        End Set
    End Property

    Public Property DECPRIMA_TOTAL_SG As Double
        Get
            Return dcPRIMA_TOTAL_SG
        End Get
        Set(ByVal value As Double)
            dcPRIMA_TOTAL_SG = value
        End Set
    End Property

    'RQ06
    Public Property Cobertura() As Integer
        Get
            Return intIDCobertura
        End Get
        Set(ByVal value As Integer)
            intIDCobertura = value
        End Set
    End Property

    Public Property IDBandera
        Get
            Return intBandera
        End Get
        Set(value)
            intBandera = value
        End Set
    End Property

    Public Property UsuReg As String
        Get
            Return strusureg
        End Get
        Set(value As String)
            strusureg = value
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

    Public Property DescVersion As String
        Get
            Return strDescVersion
        End Get
        Set(value As String)
            strDescVersion = value
        End Set
    End Property

    Public Property IDTipoCalculoSeguroVida As Integer
        Get
            Return intTipoCalculoSeguroVida
        End Get
        Set(value As Integer)
            intTipoCalculoSeguroVida = value
        End Set
    End Property

    'AVH
    Public Property IDAlianza As Integer
        Get
            Return intAlianza
        End Get
        Set(value As Integer)
            intAlianza = value
        End Set
    End Property

    Public Property IDGrupo As Integer
        Get
            Return intGrupo
        End Get
        Set(value As Integer)
            intGrupo = value
        End Set
    End Property

    Public Property IDDivision As Integer
        Get
            Return intDivision
        End Get
        Set(value As Integer)
            intDivision = value
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

    Public Property CondicionAcc As Integer
        Get
            Return intCondicionAcc
        End Get
        Set(value As Integer)
            intCondicionAcc = value
        End Set
    End Property

    Public Property Monto() As Double 'AVH RQ WSC
        Get
            Return dblMonto
        End Get
        Set(ByVal value As Double)
            dblMonto = value
        End Set
    End Property
    Public Property PlazoModifica() As String 'AVH RQ WSC
        Get
            Return PlazoMod
        End Get
        Set(ByVal value As String)
            PlazoMod = value
        End Set
    End Property

    Public Property Broker As Integer
        Get
            Return intBroker
        End Get
        Set(value As Integer)
            intBroker = value
        End Set
    End Property

    Public Property Params As String
        Get
            Return strparams
        End Get
        Set(value As String)
            strparams = value
        End Set
    End Property
    Public Property Enganche_CI As Double?
        Get
            Return initEngancheCI
        End Get
        Set(value As Double?)
            initEngancheCI = value
        End Set
    End Property

    Public Property MtoGarantia As Double
        Get
            Return dbMtoGarantia
        End Get
        Set(value As Double)
            dbMtoGarantia = value
        End Set
    End Property

    Public Property automikUser() As String
        Get
            Return _automikUser
        End Get
        Set(ByVal value As String)
            _automikUser = value
        End Set
    End Property

    Public Property IDSubMarca() As Integer
        Get
            Return _idSubMarca
        End Get
        Set(ByVal value As Integer)
            _idSubMarca = value
        End Set
    End Property

    Public Property IsMulticotizacion() As Integer
        Get
            Return init_IsMulticotizacion
        End Get
        Set(value As Integer)
            init_IsMulticotizacion = value
        End Set
    End Property



#End Region

    Public Sub CargaCotizacion(ByVal intCot As Integer)
        Dim dtsCot As New DataSet
        Try

            intCotiza = intCot
            dtsCot = ManejaCotizacion(1)
            intCotiza = 0
            If Trim$(strErrCotiza) = "" Then
                strErrCotiza = "No se encontró información para poder cargar la cotización"
                If dtsCot.Tables.Count > 0 Then
                    If dtsCot.Tables(0).Rows.Count > 0 Then
                        intCotiza = dtsCot.Tables(0).Rows(0).Item("ID_COTIZACION")
                        intEmpresa = dtsCot.Tables(0).Rows(0).Item("ID_EMPRESA")
                        intAgencia = dtsCot.Tables(0).Rows(0).Item("ID_AGENCIA")
                        intAsesor = dtsCot.Tables(0).Rows(0).Item("ID_ASESOR")
                        intPromotor = dtsCot.Tables(0).Rows(0).Item("ID_PROMOTOR")
                        intVendedor = dtsCot.Tables(0).Rows(0).Item("ID_VENDEDOR")
                        'intPersonaCte = dtsCot.Tables(0).Rows(0).Item("ID_PERSONA_CLIENTE")
                        intPerJur = dtsCot.Tables(0).Rows(0).Item("ID_PER_JURIDICA")
                        intTipoProd = dtsCot.Tables(0).Rows(0).Item("ID_TIPO_PRODUCTO")
                        intMarca = dtsCot.Tables(0).Rows(0).Item("ID_MARCA")
                        intClasif = dtsCot.Tables(0).Rows(0).Item("ID_CLASIFICACION")
                        intProducto = dtsCot.Tables(0).Rows(0).Item("ID_PRODUCTO")
                        intPlazo = dtsCot.Tables(0).Rows(0).Item("ID_PLAZO")
                        intEsqFinan = dtsCot.Tables(0).Rows(0).Item("ID_ESQUEMA_FINAN")
                        intCalendario = dtsCot.Tables(0).Rows(0).Item("ID_CALENDARIO")
                        intPeriodicidad = dtsCot.Tables(0).Rows(0).Item("ID_PERIODICIDAD")
                        intTipoSeg = dtsCot.Tables(0).Rows(0).Item("ID_TIPO_SEGURO")
                        intAseguradora = dtsCot.Tables(0).Rows(0).Item("ID_ASEGURADORA")
                        intEstado = dtsCot.Tables(0).Rows(0).Item("ID_ESTADO")
                        intPlazoSeg = dtsCot.Tables(0).Rows(0).Item("ID_PLAZO_SEGURO")
                        intPaquete = dtsCot.Tables(0).Rows(0).Item("ID_PAQUETE")
                        intTipoOper = dtsCot.Tables(0).Rows(0).Item("ID_TIPO_OPER")
                        intMoneda = dtsCot.Tables(0).Rows(0).Item("ID_MONEDA")
                        intTipoSegVida = dtsCot.Tables(0).Rows(0).Item("ID_TIPO_SEGURO_VIDA")
                        intPaqueteSeg = dtsCot.Tables(0).Rows(0).Item("ID_PAQUETE_SEGURO")
                        intGiro = dtsCot.Tables(0).Rows(0).Item("ID_GIRO")
                        intMonedaFact = dtsCot.Tables(0).Rows(0).Item("ID_MONEDA_FACTURA")
                        intIDTasaIva = dtsCot.Tables(0).Rows(0).Item("ID_TASA_IVA")
                        intTipoVenc = dtsCot.Tables(0).Rows(0).Item("ID_TIPO_VENCIMIENTO")
                        intTipoCot = dtsCot.Tables(0).Rows(0).Item("ID_TIPO_COT")
                        intPagosGraCap = dtsCot.Tables(0).Rows(0).Item("PAGOS_GRACIA_CAPITAL")
                        intPagosGraInt = dtsCot.Tables(0).Rows(0).Item("PAGOS_GRACIA_INTERES")
                        intRentasDep = dtsCot.Tables(0).Rows(0).Item("RENTAS_DEPOSITO")
                        intUnidadesProd = dtsCot.Tables(0).Rows(0).Item("UNIDADES_PRODUCTO")
                        intValorPlazo = dtsCot.Tables(0).Rows(0).Item("VALOR_PLAZO")
                        intValorPlazoSeguro = dtsCot.Tables(0).Rows(0).Item("VALOR_PLAZO_SEGURO")
                        intAnioUsados = dtsCot.Tables(0).Rows(0).Item("ANIO_MODELO_USADOS")
                        intTipoCalculoSeguro = dtsCot.Tables(0).Rows(0).Item("ID_TIPO_CALCULO_SEGURO")
                        intDiaPago = dtsCot.Tables(0).Rows(0).Item("DIA_PAGO")
                        intIDTasaIntVar = dtsCot.Tables(0).Rows(0).Item("ID_TASA_VARIABLE")

                        sngPriPagoIrreg = dtsCot.Tables(0).Rows(0).Item("PRIMER_PAGO_IRREG")
                        sngCalculaPagoIni = dtsCot.Tables(0).Rows(0).Item("CALCULO_PAGO_INICIAL")
                        sngUsaTasaPCP = dtsCot.Tables(0).Rows(0).Item("USA_TASA_PCP")
                        sngConsideraEngAcc = dtsCot.Tables(0).Rows(0).Item("CONSIDERA_ENGANCHE_ACCESORIOS")
                        sngCalcularIVASegVida = dtsCot.Tables(0).Rows(0).Item("CALCULAR_IVA_SEGURO_VIDA")
                        sngCapturaManualSeg = dtsCot.Tables(0).Rows(0).Item("CAPTURA_MANUAL_SEGURO")
                        sngManejaTasaIntVar = dtsCot.Tables(0).Rows(0).Item("MANEJA_TASA_VARIABLE")
                        sngIncluyeRC = dtsCot.Tables(0).Rows(0).Item("INCLUYE_RC")

                        'strNombre = dtsCot.Tables(0).Rows(0).Item("NOMBRE")
                        'strPaterno = dtsCot.Tables(0).Rows(0).Item("PATERNO")
                        'strMaterno = dtsCot.Tables(0).Rows(0).Item("MATERNO")
                        'strTel = dtsCot.Tables(0).Rows(0).Item("TELEFONO")
                        'strMail = dtsCot.Tables(0).Rows(0).Item("MAIL")
                        'strTelMovil = dtsCot.Tables(0).Rows(0).Item("TELEFONO_MOVIL")
                        'strContacto = dtsCot.Tables(0).Rows(0).Item("CONTACTO")
                        strMarcaUsado = dtsCot.Tables(0).Rows(0).Item("MARCA_USADOS")
                        strProdUsado = dtsCot.Tables(0).Rows(0).Item("PRODUCTO_USADOS")

                        strUIDEmp = dtsCot.Tables(0).Rows(0).Item("UID_EMPRESA")
                        strUIDAseg = dtsCot.Tables(0).Rows(0).Item("UID_ASEGURADORA")
                        strUIDProd = dtsCot.Tables(0).Rows(0).Item("UID_PRODUCTO")
                        strUIDEdo = dtsCot.Tables(0).Rows(0).Item("UID_ESTADO")
                        strUIDPaqSeg = dtsCot.Tables(0).Rows(0).Item("UID_PAQ_SEG")
                        strUIDMon = dtsCot.Tables(0).Rows(0).Item("UID_MONEDA")
                        strUIDTipoPago = dtsCot.Tables(0).Rows(0).Item("UID_TIPO_PAGO")
                        strUIDPlazo = dtsCot.Tables(0).Rows(0).Item("UID_PLAZO")
                        strUIDFam = dtsCot.Tables(0).Rows(0).Item("UID_FAMILIA")
                        strUIDTipoProd = dtsCot.Tables(0).Rows(0).Item("UID_TIPO_PROD")

                        'dblIngresos = dtsCot.Tables(0).Rows(0).Item("INGRESOS")
                        dblPrecioLista = dtsCot.Tables(0).Rows(0).Item("PRECIO_LISTA")
                        dblPrecioProd = dtsCot.Tables(0).Rows(0).Item("PRECIO_PRODUCTO")
                        dblMtoAccesorios = dtsCot.Tables(0).Rows(0).Item("MONTO_ACCESORIOS")
                        dblMtoAccesoriosContado = dtsCot.Tables(0).Rows(0).Item("MONTO_ACCESORIOS_CONTADO")
                        dblMtoAccesoriosNoSeg = dtsCot.Tables(0).Rows(0).Item("MONTO_ACCESORIOS_NO_SEGURO")
                        dblPtjEnganche = dtsCot.Tables(0).Rows(0).Item("PTJ_ENGANCHE")
                        dblPtjEngancheReal = dtsCot.Tables(0).Rows(0).Item("PTJ_ENGANCHE_REAL")
                        dblEnganche = dtsCot.Tables(0).Rows(0).Item("ENGANCHE")
                        dblPtjServFin = dtsCot.Tables(0).Rows(0).Item("PTJ_SERV_FINAN")
                        dblServFinan = dtsCot.Tables(0).Rows(0).Item("SERVICIOS_FINANCIEROS")
                        dblTasaIVA = dtsCot.Tables(0).Rows(0).Item("TASA_IVA")
                        dblTasaInt = dtsCot.Tables(0).Rows(0).Item("TASA_INTERES")
                        dblTasaIntSeg = dtsCot.Tables(0).Rows(0).Item("TASA_INTERES_SEGURO")
                        dblMtoSeguro = dtsCot.Tables(0).Rows(0).Item("MONTO_SEGURO")
                        dblMtoSeguroRegalado = dtsCot.Tables(0).Rows(0).Item("MONTO_SEGURO_REGALADO")
                        dblFactSegVida = dtsCot.Tables(0).Rows(0).Item("FACTOR_SEG_VIDA")
                        dblMontoSubsidio = dtsCot.Tables(0).Rows(0).Item("MONTO_SUBSIDIO")
                        dblValorResidual = dtsCot.Tables(0).Rows(0).Item("VALOR_RESIDUAL")
                        dblValorTipoCambio = dtsCot.Tables(0).Rows(0).Item("VALOR_TIPO_CAMBIO")
                        dblMontoRentasDep = dtsCot.Tables(0).Rows(0).Item("MONTO_RENTAS_DEPOSITO")
                        dblMontoOpcionComp = dtsCot.Tables(0).Rows(0).Item("MONTO_OPCION_COMPRA")
                        dblTasaPCP = dtsCot.Tables(0).Rows(0).Item("TASA_PCP")
                        dblPtjBlindDiscount = dtsCot.Tables(0).Rows(0).Item("PTJ_BLIND_DISCOUNT")
                        dblPtjOpcionComp = dtsCot.Tables(0).Rows(0).Item("PTJ_OPCION_COMPRA")
                        dblIncentivoVtas = dtsCot.Tables(0).Rows(0).Item("INCENTIVO_VENTAS")
                        dblPtjSubsidio = dtsCot.Tables(0).Rows(0).Item("PTJ_SUBSIDIO")
                        dblTasaIntVar = dtsCot.Tables(0).Rows(0).Item("TASA_VARIABLE")
                        dblValorAdap = dtsCot.Tables(0).Rows(0).Item("VALOR_ADAPTACION")
                        intFolioCot = dtsCot.Tables(0).Rows(0).Item("CVECOTIZACION")

                        'Inicia Monto Plazos Seguro
                        dblMontoSeguro1 = dtsCot.Tables(0).Rows(0).Item("MONTO_SEGURO1")
                        dblMontoSeguro2 = dtsCot.Tables(0).Rows(0).Item("MONTO_SEGURO2")
                        dblMontoSeguro3 = dtsCot.Tables(0).Rows(0).Item("MONTO_SEGURO3")
                        dblPuntosST = dtsCot.Tables(0).Rows(0).Item("PUNTOS_ADIC_TASA")
                        'Finaliza Monto Plazos Seguro

                        'EOST - 28082013
                        'RAY-P-123 - Se agregan los campos de id_familia y id_origen
                        intFamilia = dtsCot.Tables(0).Rows(0).Item("ID_FAMILIA")
                        intOrigen = dtsCot.Tables(0).Rows(0).Item("ID_ORIGEN")
                        intcotizador = dtsCot.Tables(0).Rows(0).Item("ID_COTIZADOR")

                        intTipoCalculoSeguroVida = dtsCot.Tables(0).Rows(0).Item("ID_TIPO_CALCULO_SEGURO_VIDA")
                        initEngancheCI = dtsCot.Tables(0).Rows(0).Item("ENGANCHE_CI")

                        dtsAccesorios = ManejaCotizacion(9)
                        dtsPagesp = ManejaCotizacion(10)
                    End If
                End If
            End If
        Catch ex As Exception
            strErrCotiza = ex.Message
        End Try
    End Sub

    Public Function ManejaCotizacion(ByVal intOper As Integer) As DataSet
        strErrCotiza = ""
        ManejaCotizacion = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta cotizacion paso
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                Case 2 ' inserta cotizacion paso
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoCot", intTipoCot.ToString)
                    'cliente
                    ArmaParametros(strParamStored, TipoDato.Entero, "idEmp", intEmpresa.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAsesor", intAsesor.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPromotor", intPromotor.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idVendedor", intVendedor.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPerJur", intPerJur.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idGiro", intGiro.ToString)
                    'ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    'ArmaParametros(strParamStored, TipoDato.Cadena, "paterno", strPaterno)
                    'ArmaParametros(strParamStored, TipoDato.Cadena, "materno", strMaterno)
                    'ArmaParametros(strParamStored, TipoDato.Cadena, "tel", strTel)
                    'ArmaParametros(strParamStored, TipoDato.Cadena, "telMovil", strTelMovil)
                    'ArmaParametros(strParamStored, TipoDato.Cadena, "contacto", strContacto)
                    'ArmaParametros(strParamStored, TipoDato.Cadena, "mail", strMail)
                    'ArmaParametros(strParamStored, TipoDato.Doble, "ingresos", dblIngresos.ToString)
                    'ArmaParametros(strParamStored, TipoDato.Entero, "idPersonaCte", intPersonaCte.ToString)

                    'producto
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoProd", intTipoProd.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", intClasif.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idProducto", intProducto.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "anioUsados", intAnioUsados.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "unidadesProd", intUnidadesProd.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMonedaFact", intMonedaFact.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "marcaUsados", strMarcaUsado)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "prodUsados", strProdUsado)
                    ArmaParametros(strParamStored, TipoDato.Doble, "precioLista", dblPrecioLista.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "precioProd", dblPrecioProd.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "montoAcc", dblMtoAccesorios.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "montoAccContado", dblMtoAccesoriosContado.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "montoAccNoSeg", dblMtoAccesoriosNoSeg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "valorAdap", dblValorAdap.ToString)

                    'financiamiento
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMoneda", intMoneda.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoOper", intTipoOper.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "plazo", intPlazo.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idEsqFin", intEsqFinan.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCalendario", intCalendario.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPeriodicidad", intPeriodicidad.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTasaIVA", intIDTasaIva.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoVenc", intTipoVenc.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "valorPlazo", intValorPlazo.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "rentasDep", intRentasDep.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "pagosGraCap", intPagosGraCap.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "pagosGraInt", intPagosGraInt.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "primerPagoIrreg", sngPriPagoIrreg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "calcPagoIni", sngCalculaPagoIni.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "consideraEngAcc", sngConsideraEngAcc.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "calculaIVASegVida", sngCalcularIVASegVida.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "usaTasaPCP", sngUsaTasaPCP.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "diaPago", intDiaPago.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "manejaTasaVar", sngManejaTasaIntVar.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTasaVar", intIDTasaIntVar.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjEnganche", dblPtjEnganche.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjEngancheReal", dblPtjEngancheReal.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "enganche", dblEnganche.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjServFin", dblPtjServFin.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "serviciosFin", dblServFinan.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjOpcComp", dblPtjOpcionComp.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "mtoOpcComp", dblMontoOpcionComp.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjBlindDiscount", dblPtjBlindDiscount.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "tasaPCP", dblTasaPCP.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "tasaIva", dblTasaIVA.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "tasaInt", dblTasaInt.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "tasaIntSeg", dblTasaIntSeg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "mtoRentasDep", dblMontoRentasDep.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "montoSubsidio", dblMontoSubsidio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "valorResid", dblValorResidual.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "valorTCambio", dblValorTipoCambio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "incentivoVentas", dblIncentivoVtas.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjSubsidio", dblPtjSubsidio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "tasaVariable", dblTasaIntVar.ToString)

                    'seguro
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoSeg", intTipoSeg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAseg", intAseguradora.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idEstado", intEstado.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "montoSeg", dblMtoSeguro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "montoSegRegalado", dblMtoSeguroRegalado.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "plazoSeg", intPlazoSeg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "valorPlazoSeguro", intValorPlazoSeguro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "factSegVid", dblFactSegVida.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoSegVida", intTipoSegVida.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaqueteSeg", intPaqueteSeg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoCalcSeg", intTipoCalculoSeguro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "capturaManualSeg", sngCapturaManualSeg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "incluyeRC", sngIncluyeRC.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoCalcSegVid", intTipoCalculoSeguroVida.ToString)
                    'ArmaParametros(strParamStored, TipoDato.Entero, "INT_TIPO_DEDUCIBLE", _intTipoDeducible.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "DEC_DEDUCIBLE", _DecDeducible.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "DEC_DEDUCIBLE_ROBO_TOTAL", _DecDeducibleRoboTotal.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Indemnizacion", _Indemnizacion.ToString)

                    'Inicia Monto Plazos seguros
                    ArmaParametros(strParamStored, TipoDato.Doble, "MontoSeguro1", dblMontoSeguro1.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "MontoSeguro2", dblMontoSeguro2.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "MontoSeguro3", dblMontoSeguro3.ToString)
                    'Finaliza Monto Plazos seguros

                    'EOST - 28082013
                    'RAY-P-123 - Se agregan los campos de id_familia y id_origen
                    ArmaParametros(strParamStored, TipoDato.Entero, "idFamilia", intFamilia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idOrigen", intOrigen.ToString)

                    'web service
                    ArmaParametros(strParamStored, TipoDato.Cadena, "uidEmpresa", strUIDEmp)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "uidAseg", strUIDAseg)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "uidProd", strUIDProd)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "uidPaqSeg", strUIDPaqSeg)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "uidEdo", strUIDEdo)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "uidMoneda", strUIDMon)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "uidTipoPago", strUIDTipoPago)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "uidPlazo", strUIDPlazo)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "uidFamilia", strUIDFam)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "uidTipoProd", strUIDTipoProd)

                    ' Tracker INC-P-  OECA 25-06-2012---Se comenta, solo aplica para reymond
                    ArmaParametros(strParamStored, TipoDato.Doble, "PuntosST", dblPuntosST)
                    ' Tracker INC-P-  OECA 25-06-2012

                    ArmaParametros(strParamStored, TipoDato.Entero, "IDCOTIZADOR", intcotizador.ToString)
                    If Not IsNothing(initEngancheCI) Then
                        ArmaParametros(strParamStored, TipoDato.Doble, "engancheCI", initEngancheCI.ToString)
                    End If


                    'If Not IsNothing()
                Case 3 ' actualiza cotizacion paso
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoCot", intTipoCot.ToString)
                    'cliente
                    ArmaParametros(strParamStored, TipoDato.Entero, "idEmp", intEmpresa.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAsesor", intAsesor.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPromotor", intPromotor.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idVendedor", intVendedor.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPerJur", intPerJur.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idGiro", intGiro.ToString)
                    'ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    'ArmaParametros(strParamStored, TipoDato.Cadena, "paterno", strPaterno)
                    'ArmaParametros(strParamStored, TipoDato.Cadena, "materno", strMaterno)
                    'ArmaParametros(strParamStored, TipoDato.Cadena, "tel", strTel)
                    'ArmaParametros(strParamStored, TipoDato.Cadena, "telMovil", strTelMovil)
                    'ArmaParametros(strParamStored, TipoDato.Cadena, "contacto", strContacto)
                    'ArmaParametros(strParamStored, TipoDato.Cadena, "mail", strMail)
                    'ArmaParametros(strParamStored, TipoDato.Doble, "ingresos", dblIngresos.ToString)
                    'ArmaParametros(strParamStored, TipoDato.Entero, "idPersonaCte", intPersonaCte.ToString)

                    'producto
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoProd", intTipoProd.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", intClasif.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idProducto", intProducto.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "anioUsados", intAnioUsados.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "unidadesProd", intUnidadesProd.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMonedaFact", intMonedaFact.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "marcaUsados", strMarcaUsado)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "prodUsados", strProdUsado)
                    ArmaParametros(strParamStored, TipoDato.Doble, "precioLista", dblPrecioLista.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "precioProd", dblPrecioProd.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "montoAcc", dblMtoAccesorios.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "montoAccContado", dblMtoAccesoriosContado.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "montoAccNoSeg", dblMtoAccesoriosNoSeg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "valorAdap", dblValorAdap.ToString)

                    'financiamiento
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMoneda", intMoneda.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoOper", intTipoOper.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "plazo", intPlazo.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idEsqFin", intEsqFinan.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCalendario", intCalendario.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPeriodicidad", intPeriodicidad.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTasaIVA", intIDTasaIva.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoVenc", intTipoVenc.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "valorPlazo", intValorPlazo.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "rentasDep", intRentasDep.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "pagosGraCap", intPagosGraCap.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "pagosGraInt", intPagosGraInt.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "primerPagoIrreg", sngPriPagoIrreg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "calcPagoIni", sngCalculaPagoIni.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "consideraEngAcc", sngConsideraEngAcc.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "calculaIVASegVida", sngCalcularIVASegVida.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "usaTasaPCP", sngUsaTasaPCP.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "diaPago", intDiaPago.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "manejaTasaVar", sngManejaTasaIntVar.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTasaVar", intIDTasaIntVar.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjEnganche", dblPtjEnganche.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjEngancheReal", dblPtjEngancheReal.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "enganche", dblEnganche.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjServFin", dblPtjServFin.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "serviciosFin", dblServFinan.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjOpcComp", dblPtjOpcionComp.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "mtoOpcComp", dblMontoOpcionComp.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjBlindDiscount", dblPtjBlindDiscount.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "tasaPCP", dblTasaPCP.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "tasaIva", dblTasaIVA.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "tasaInt", dblTasaInt.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "tasaIntSeg", dblTasaIntSeg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "mtoRentasDep", dblMontoRentasDep.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "montoSubsidio", dblMontoSubsidio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "valorResid", dblValorResidual.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "valorTCambio", dblValorTipoCambio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "incentivoVentas", dblIncentivoVtas.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ptjSubsidio", dblPtjSubsidio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "tasaVariable", dblTasaIntVar.ToString)

                    'seguro
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoSeg", intTipoSeg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAseg", intAseguradora.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idEstado", intEstado.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "montoSeg", dblMtoSeguro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "montoSegRegalado", dblMtoSeguroRegalado.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "plazoSeg", intPlazoSeg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "valorPlazoSeguro", intValorPlazoSeguro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "factSegVid", dblFactSegVida.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoSegVida", intTipoSegVida.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaqueteSeg", intPaqueteSeg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoCalcSeg", intTipoCalculoSeguro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "capturaManualSeg", sngCapturaManualSeg.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "incluyeRC", sngIncluyeRC.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "DEC_DEDUCIBLE", _DecDeducible.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "DEC_DEDUCIBLE_ROBO_TOTAL", _DecDeducibleRoboTotal.ToString)

                    'Inicia Monto Plazos seguros
                    ArmaParametros(strParamStored, TipoDato.Doble, "MontoSeguro1", dblMontoSeguro1.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "MontoSeguro2", dblMontoSeguro2.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "MontoSeguro3", dblMontoSeguro3.ToString)
                    'Finaliza Monto Plazos seguros

                    'EOST - 28082013
                    'RAY-P-123 - Se agregan los campos de id_familia y id_origen
                    ArmaParametros(strParamStored, TipoDato.Entero, "idFamilia", intFamilia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idOrigen", intOrigen.ToString)

                    'web service
                    ArmaParametros(strParamStored, TipoDato.Cadena, "uidEmpresa", strUIDEmp)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "uidAseg", strUIDAseg)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "uidProd", strUIDProd)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "uidPaqSeg", strUIDPaqSeg)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "uidEdo", strUIDEdo)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "uidMoneda", strUIDMon)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "uidTipoPago", strUIDTipoPago)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "uidPlazo", strUIDPlazo)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "uidFamilia", strUIDFam)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "uidTipoProd", strUIDTipoProd)

                    ' Tracker INC-P-  OECA 25-06-2012---Se comenta, solo aplica para reymond
                    ArmaParametros(strParamStored, TipoDato.Doble, "PuntosST", dblPuntosST)
                    ' Tracker INC-P-  OECA 25-06-2012
                Case 4 ' borra cotizacion paso
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                Case 5 ' borra pagos especiales
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                Case 6 ' inserta pagos especiales
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "periodoPagEsp", intPeriodoPagEsp.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "fechaPagEsp", strFecPagEsp.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "montoPagEsp", dblMontoPagEsp.ToString)
                Case 7 ' agrega accesorio
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAccesorio", intAccesorio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "descAccesorio", strDescAcc)
                    ArmaParametros(strParamStored, TipoDato.Doble, "PrecioAccesorio", dblPrecioAcc.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "accContado", sngAccContado.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "accAfectaCalcSeg", sngAccAfectaSeg.ToString)

                Case 8 ' elimina accesorio
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                    If intAccesorioCot > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPasoAcc", intAccesorioCot.ToString)
                Case 9 ' consulta accesorios cotizacion
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                    If intAccesorioCot > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPasoAcc", intAccesorioCot.ToString)
                Case 10 ' consulta pagos especiales
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                Case 11 'guarda cotizacion
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                Case 12 'carga folio cotizacion
                    ArmaParametros(strParamStored, TipoDato.Entero, "idFolioCot", intFolioCot.ToString)
                Case 13 'consulta folio cotizacion
                    Dim strNomConsul As String = ""
                    'If Trim(strNombre) <> "" Then strNomConsul = Trim(strNombre)
                    'If Trim(strPaterno) <> "" Then strNomConsul += IIf(Trim(strNomConsul) <> "", ".", "") & Trim(strPaterno)
                    'If Trim(strMaterno) <> "" Then strNomConsul += IIf(Trim(strNomConsul) <> "", ".", "") & Trim(strMaterno)

                    If intFolioCot > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idFolioCot", intFolioCot.ToString)
                    If Trim(strFecIniCot) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "fecIniCot", strFecIniCot)
                    If Trim(strFecFinCot) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "fecFinCot", strFecFinCot)
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If intTipoCot > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoCot", intTipoCot.ToString)
                    If Trim(strNomConsul) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNomConsul)
                    If intVigenciaCot > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idVigenciaCot", intVigenciaCot.ToString)

                    ArmaParametros(strParamStored, TipoDato.Entero, "idEmp", intEmpresa.ToString)
                    If intAsesor > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAsesor", intAsesor.ToString)
                    If intPromotor > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPromotor", intPromotor.ToString)
                    If intVendedor > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idVendedor", intVendedor.ToString)
                    'BBV-P-412 RQCOT-04:AVH
                    If intEstado > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idEstado", intEstado.ToString)
                    If intAlianza > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", intAlianza.ToString)
                    If intGrupo > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idGrupo", intGrupo.ToString)
                    If intDivision > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idDivision", intDivision.ToString)
                    If Trim(strNombre) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Nombre", strNombre.ToString)

                    ArmaParametros(strParamStored, TipoDato.Entero, "consultaCotAbierta", sngConsultaCotAbierta.ToString)
                Case 14 'Inserta en la tabla de relacion
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idFolioCot", intFolioCot.ToString)

                Case 15 'Actualiza despues de cambiar el plazo para los multiplazos
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "valorPlazo", intValorPlazo.ToString)

                Case 16 ' CONSULTA PARA INCLUIR ÚNICAMENTE ACCESORIOS QUE AFECTAN A LA COTIZACIÓN DEL SEGURO
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)

                Case 17 ' CONSULTA PARA EL CALCULO DE LA PRIMA TOTAL QUE SE ENVIA A Lorantmms 
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)

                Case 18 ' Actualiza cotseguro con el numero de cotización de lorant en la segunda consulta
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "nvacot", intnvacot.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "PRIMA_TOTAL_SG", dcPRIMA_TOTAL_SG.ToString)

                Case 19 ' Actualiza cotseguro con el numero de cotización de lorant en la segunda consulta
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "tasaVariable", dblTasaIntVar.ToString)
                Case 20 'Obtiene los pagos para el calculo del CAT en AP
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                Case 21 'Obtiene tipo de unidad 'INC-B-1987
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", IDAlianza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                Case 22 'Obtiene tipo de uso 'INC-B-1987
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", IDAlianza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                Case 23 'Inserta el uso y tipo de unidad 'INC-B-1987
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "iduso", intuso.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idtipo", inttipo.ToString)
                Case 24 'Recupera datos de la tabla cotizaciones para mensaje al cambiar plazo en la pantalla multiplazos 'INC-B-1987
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                Case 25 'Recupera tipo y tipo unidad cuando carga una cotiza 'INC-B-1987
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                Case 26 'INC-B-2648: MAUT: Recupera ID_PRODUCTO, ID_PER_JURIDICA, ID_CLASIFICACION 
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                Case 27 'INC-B-2648: MAUT: Se envían parametros para actualizar estado del seguro
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idEstado", intEstado.ToString)
                Case 28 'Obtiene tipo de Cobertura
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", IDAlianza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", IDClasificacionProd.ToString) 'BUG-PC-197
                Case 29 'Accesorios
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAccesorio", IDAccesorio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "descAccesorio", DescripcionAccesorio)
                    ArmaParametros(strParamStored, TipoDato.Doble, "PrecioAccesorio", PrecioAccesorio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "CondicionAcc", intCondicionAcc.ToString)
                Case 30 'Inserta Relaciones cotizaciones(persona, telefonos)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPersonaCte", intPersonaCte.ToString)
                    If strTel.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "tel", strTel)
                    If strTelMovil.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "telMovil", strTelMovil)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usureg", strusureg)
                Case 31 'CONSULTA ACCESORIOS
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                Case 32 'AVH RQ WSC Inserta Plazos Calculados
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "plazo", intValorPlazo.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "monto", dblMonto.ToString)
                Case 35 '--RQ-PI7-PC3: CGARCIA: 10/10/2017: CONSULTA DEDUCIBLE DAÑOS Y ROBO TOTAL
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                Case 36
                    '--RQ-PI7-PC3: CGARCIA: 10/10/2017: CONSULTA DEDUCIBLE DAÑOS Y ROBO TOTAL
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)

                Case 39 'Cat_DOS
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCotPaso", intCotiza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "tasaCatCI", dblTasaCatDos.ToString)
            End Select

            ManejaCotizacion = objSD.EjecutaStoredProcedure("spManejaCotizaciones", strErrCotiza, strParamStored)


            If strErrCotiza = "" Then
                If intOper = 2 Or intOper = 12 Then
                    intCotiza = ManejaCotizacion.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 7 Then
                    intAccesorioCot = ManejaCotizacion.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 11 Then
                    intFolioCot = ManejaCotizacion.Tables(0).Rows(0).Item(0)
                End If

            End If
        Catch ex As Exception
            strErrCotiza = ex.Message
        End Try
    End Function

    Public Function GeneraTablaAmort() As DataSet
        strErrCotiza = ""
        GeneraTablaAmort = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion
            ArmaParametros(strParamStored, TipoDato.Entero, "idCot", intCotiza.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "id_empresa", intEmpresa.ToString)
            GeneraTablaAmort = objSD.EjecutaStoredProcedure("spCalculaPagos", strErrCotiza, strParamStored)
        Catch ex As Exception
            strErrCotiza = ex.Message
        End Try
    End Function

    Public Function ObtenFiltros() As DataSet
        strErrCotiza = ""
        ObtenFiltros = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion
            ArmaParametros(strParamStored, TipoDato.Entero, "idCot", intCotiza.ToString)
            ObtenFiltros = objSD.EjecutaStoredProcedure("spObtenFiltrosCot", strErrCotiza, strParamStored)
        Catch ex As Exception
            strErrCotiza = ex.Message
        End Try
    End Function
    'EOST - 03072013
    Public Function ObtenCargosInicialesRaymond(ByVal intOper As Integer) As DataSet
        strErrCotiza = ""
        ObtenCargosInicialesRaymond = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion
            ArmaParametros(strParamStored, TipoDato.Entero, "idCot", intCotiza.ToString)

            If intOper = 2 Or intOper = 3 Then
                ObtenCargosInicialesRaymond = objSD.EjecutaStoredProcedure("spCargosInicialesSinEnganche", strErrCotiza, strParamStored)
            ElseIf intOper = 1 Or intOper = 4 Then
                ObtenCargosInicialesRaymond = objSD.EjecutaStoredProcedure("spCargosIniciales", strErrCotiza, strParamStored)
            End If

        Catch ex As Exception
            strErrCotiza = ex.Message
        End Try
    End Function

    Public Function ObtenCargosIniciales(ByVal intOper As Integer) As DataSet
        strErrCotiza = ""
        ObtenCargosIniciales = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion
            ArmaParametros(strParamStored, TipoDato.Entero, "idCot", intCotiza.ToString)
            ObtenCargosIniciales = objSD.EjecutaStoredProcedure("spCargosIniciales", strErrCotiza, strParamStored)
        Catch ex As Exception
            strErrCotiza = ex.Message
        End Try
    End Function

    Public Function ObtenMontoSeguro() As DataSet
        strErrCotiza = ""
        ObtenMontoSeguro = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion
            ArmaParametros(strParamStored, TipoDato.Entero, "idCot", intCotiza.ToString)
            ArmaParametros(strParamStored, TipoDato.Doble, "montoFinan", dblPrecioProd.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "idProd", intProducto.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "idAseg", intAseguradora.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "idEstado", intEstado.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "idPaqueteSeg", intPaqueteSeg.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "idPlazo", intPlazoSeg.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "idTipoSeg", intTipoSeg.ToString)
            ObtenMontoSeguro = objSD.EjecutaStoredProcedure("spObtenMontoSeguro", strErrCotiza, strParamStored)
        Catch ex As Exception
            strErrCotiza = ex.Message
        End Try
    End Function

    Public Function CalculaCat_Dos(ByVal dtsPagos As DataSet, _
                              ByVal dtsCargosIni As DataSet, _
                              ByVal dtsFiltros As DataSet) As Double
        Try

            Dim dblMtoCapital As Double = 0
            Dim dblMtoGastos As Double = 0
            Dim dblMtoInicial As Double = 0
            Dim dblTIR As Double = 0
            Dim arrValores() As Double
            Dim intPaso As Integer = 0
            Dim intTiempoCAT As Integer = 0
            Dim objRow As DataRow
            Dim intPlazo As Integer = 0
            Dim objParam As clsParametros = New clsParametros(127)
            Dim dtsPagosCAT As DataSet 'BUG-PC-46 Nuevo dataset de pagos para recálculo del CAT

            'Dim cat As Double
            Dim cotiza = intCotiza

            Dim _ObjCot As New SNProcotiza.clsCotizaciones
            Dim tipo_oper As Integer
            'BUG-PC-49: MAPH: 28/02/2017 Modificación del cálculo del CAT y período irregular
            Dim dsPagosAP As New DataSet

            'Dim dsDatosCAT As New DataSet

            'INC-B-1555
            _ObjCot.CargaCotizacion(cotiza)
            tipo_oper = _ObjCot.IDTipoOperacion
            dsPagosAP = _ObjCot.ManejaCotizacion(20)

            CalculaCat_Dos = 0

            intTiempoCAT = 12 'PERIODICIDAD MENSUAL
            If intPeriodicidad = 84 Then
                intTiempoCAT = 52 'PERIODICIDAD SEMANAL
            End If
            If intPeriodicidad = 95 Then
                intTiempoCAT = 24 'PERIODICIDAD QUINCENAL
            End If

            'INC-B-1555 SE AGREGA VALIDACIÓN PARA EL TIPO DE OPERACIÓN
            If tipo_oper = 1 Then
                'Obtenemos el monto del capital (NO SE INCLUYE SEGURO DE EQUIPO)
                'dblMtoCapital = dtsFiltros.Tables(0).Rows(0).Item("MTO_CAPITAL_CAT")
                'dblMtoCapital = dtsFiltros.Tables(0).Rows(0).Item("MTO_FACTURA_CON_IVA") + dtsFiltros.Tables(0).Rows(0).Item("MTOSEG")

                'BUG-PC-49 13/03/2017 MAPH Reglas de Negocio del Excel:
                dblMtoCapital = dtsFiltros.Tables(0).Rows(0).Item("MTOPRODUCTO")

                'Sólo si es de contado el monto de accesorios entonces se descuenta del total para el CAT
                If dtsFiltros.Tables(0).Rows(0).Item("TIPO_ACC").ToString() = "CONTADO" Then
                    dblMtoCapital = dblMtoCapital - dtsFiltros.Tables(0).Rows(0).Item("MTOACC")
                End If

                'BUG-PC-46 14/03/2017 MAPH Cambio de lugar de la siguiente línea
                'dblMtoCapital = dblMtoCapital * dtsFiltros.Tables(0).Rows(0).Item("VALOR_TIPO_CAMBIO")
                ''dblMtoCapital = iff(dtsFiltros.Tables(0).Rows(0).Item("MTO_FACTURA_CON_IVA") + dtsFiltros.Tables(0).Rows(0).Item("MTOSEG")

                'Obtenemos el monto de los gastos
                For Each objRow In dtsCargosIni.Tables(0).Rows
                    intPaso = Val(objRow.Item("ORDEN"))
                    If intPaso <= 1 Then
                        ' AOC CAT
                        dblMtoGastos += IIf(objRow.Item("ORDEN") = 1, (objRow.Item("MONTOSIVA")), objRow.Item("MONTO"))
                        ' AOC CAT
                    End If
                Next

                'BUG-PC-50 MAPH 28/03/2016 Comisión por apertura en el CAT
                If Not dtsFiltros.Tables(0).Rows(0).Item("COMAPERT") Is Nothing Then
                    dblMtoGastos += (Convert.ToDouble(dtsFiltros.Tables(0).Rows(0).Item("COMAPERT").ToString()) / (1 + (Convert.ToDouble(dtsFiltros.Tables(0).Rows(0).Item("TASA_IVA").ToString())) / 100))
                End If

                'Al capital le quitamos el monto de los gastos que afectan al CAT
                dblMtoInicial = dblMtoCapital * -1
                dblMtoInicial += dblMtoGastos

                'BUG-PC-46 14/03/2017 MAPH Recálculo del CAT
                dblMtoInicial = dblMtoInicial * dtsFiltros.Tables(0).Rows(0).Item("VALOR_TIPO_CAMBIO")
                dtsPagosCAT = _ObjCot.ConsultaTablaAmortCAT()

                'llenamos el arreglo de valores (sin considerar el pago cero
                'intPlazo = dtsPagos.Tables(0).Rows.Count
                'For Each objRow In dtsPagos.Tables(0).Rows

                'BUG-PC-46 14/03/2017 MAPH Recálculo del CAT 
                intPlazo = dtsPagosCAT.Tables(0).Rows.Count
                For Each objRow In dtsPagosCAT.Tables(0).Rows
                    If Val(objRow.Item("NO_PAGO").ToString) = 0 Then
                        intPlazo -= 1
                        Exit For
                    End If
                Next

                ReDim arrValores(intPlazo + 1)

                'el primer valor es monto inicial
                arrValores(0) = dblMtoInicial

                'los valores siguientes son los pagos
                intPaso = 1
                'For Each objRow In dtsPagos.Tables(0).Rows

                'BUG-PC-46 14/03/2017 MAPH Recálculo del CAT
                For Each objRow In dtsPagosCAT.Tables(0).Rows

                    'BUG-PC-49 El segundo valor es el del período irregular, por ello se incluye disyuntiva
                    If Val(objRow.Item("NO_PAGO").ToString) > 0 Then
                        'arrValores(intPaso) = objRow.Item("PAGO") '+ objRow.Item("IVA")
                        arrValores(intPaso) = IIf(IsDBNull(objRow.Item("PAGO")), 0, objRow.Item("PAGO")) + IIf(IsDBNull(objRow.Item("PAGO_ESP")), 0, objRow.Item("PAGO_ESP")) + IIf(IsDBNull(objRow.Item("PAGO_SEG")), 0, objRow.Item("PAGO_SEG")) - IIf(IsDBNull(objRow.Item("SEG_CONT")), 0, objRow.Item("SEG_CONT"))
                        'si es el ultimo pago se le suma la opcion de compra y se le resta el depósito en garantía
                        intPaso += 1
                        'BUG-PC-69:MPUESTO:30/05/2017:CORRECCION DEL CAT Y COMISIÓN POR APERTURA
                    ElseIf Val(objRow.Item("NO_PAGO").ToString) = 0 And Val(objRow.Item("TIPO_TABLA").ToString) = 4 Then
                        arrValores(intPaso) = IIf(IsDBNull(objRow.Item("INTERES")), 0, objRow.Item("INTERES"))
                        intPaso += 1
                    End If

                Next
            Else
                intPlazo = _ObjCot.ValorPlazo 'INC-B-1555 SE QUITA - 1 PARA QUE LA MATRIZ EMPIEZE EN 0
                ReDim arrValores(intPlazo)
                'los valores siguientes son los pagos
                intPaso = 0
                For Each objRow In dsPagosAP.Tables(0).Rows
                    arrValores(intPaso) = objRow.Item("PAGO")
                    intPaso += 1
                Next
            End If

            'obtenemos la TIR
            dblTIR = Microsoft.VisualBasic.IRR(arrValores, 0)
            If dblTIR < 0 Then
                strErrCotiza = "No se puede calcular el CAT, la TIR da una tasa negativa"
                'CalculaCat = dtsFiltros.Tables(0).Rows(0)(8) + 1.56
            Else
                'Obtenemos el CAT
                If Val(objParam.Valor) = 1 Then
                    'se debe validar si se calcula el cat solo multiplicando el valor de la TIR por el plazo anual
                    'CalculaCat = (dblTIR * intTiempoCAT * 100) ' se comenta para aplicar correctamente la formula
                    CalculaCat_Dos = (((1 + dblTIR) ^ 12) - 1) * 100 ' AOC CAT se aplica la formula de banxico para obtencion de CAT a partir de la TIR

                Else
                    CalculaCat_Dos = ((((1 + dblTIR) ^ intTiempoCAT) - 1) * 100) / 1.16
                End If

            End If

            'INC-B-1987
            CalculaCat_Dos = System.Math.Round(CalculaCat_Dos, 1)

            _ObjCot.dblTasaCatDos = Convert.ToDecimal(CalculaCat_Dos) 'INC-B-1555 Se cambia la variable cat por CalculaCat
            _ObjCot.ManejaCotizacion(39)

        Catch ex As Exception
            strErrCotiza = ex.Message
        End Try
    End Function

    Public Function CalculaCat(ByVal dtsPagos As DataSet, _
                               ByVal dtsCargosIni As DataSet, _
                               ByVal dtsFiltros As DataSet) As Double
        Try

            Dim dblMtoCapital As Double = 0
            Dim dblMtoGastos As Double = 0
            Dim dblMtoInicial As Double = 0
            Dim dblTIR As Double = 0
            Dim arrValores() As Double
            Dim intPaso As Integer = 0
            Dim intTiempoCAT As Integer = 0
            Dim objRow As DataRow
            Dim intPlazo As Integer = 0
            Dim objParam As clsParametros = New clsParametros(127)
            Dim dtsPagosCAT As DataSet 'BUG-PC-46 Nuevo dataset de pagos para recálculo del CAT

            'Dim cat As Double
            Dim cotiza = intCotiza

            Dim _ObjCot As New SNProcotiza.clsCotizaciones
            Dim tipo_oper As Integer
            'BUG-PC-49: MAPH: 28/02/2017 Modificación del cálculo del CAT y período irregular
            Dim dsPagosAP As New DataSet

            'Dim dsDatosCAT As New DataSet

            'INC-B-1555
            _ObjCot.CargaCotizacion(cotiza)
            tipo_oper = _ObjCot.IDTipoOperacion
            dsPagosAP = _ObjCot.ManejaCotizacion(20)

            CalculaCat = 0

            intTiempoCAT = 12 'PERIODICIDAD MENSUAL
            If intPeriodicidad = 84 Then
                intTiempoCAT = 52 'PERIODICIDAD SEMANAL
            End If
            If intPeriodicidad = 95 Then
                intTiempoCAT = 24 'PERIODICIDAD QUINCENAL
            End If

            'INC-B-1555 SE AGREGA VALIDACIÓN PARA EL TIPO DE OPERACIÓN
            If tipo_oper = 1 Then
                'Obtenemos el monto del capital (NO SE INCLUYE SEGURO DE EQUIPO)
                'dblMtoCapital = dtsFiltros.Tables(0).Rows(0).Item("MTO_CAPITAL_CAT")
                'dblMtoCapital = dtsFiltros.Tables(0).Rows(0).Item("MTO_FACTURA_CON_IVA") + dtsFiltros.Tables(0).Rows(0).Item("MTOSEG")

                'BUG-PC-49 13/03/2017 MAPH Reglas de Negocio del Excel:
                dblMtoCapital = dtsFiltros.Tables(0).Rows(0).Item("MTOPRODUCTO")

                'Sólo si es de contado el monto de accesorios entonces se descuenta del total para el CAT
                If dtsFiltros.Tables(0).Rows(0).Item("TIPO_ACC").ToString() = "CONTADO" Then
                    dblMtoCapital = dblMtoCapital - dtsFiltros.Tables(0).Rows(0).Item("MTOACC")
                End If

                'BUG-PC-46 14/03/2017 MAPH Cambio de lugar de la siguiente línea
                'dblMtoCapital = dblMtoCapital * dtsFiltros.Tables(0).Rows(0).Item("VALOR_TIPO_CAMBIO")
                ''dblMtoCapital = iff(dtsFiltros.Tables(0).Rows(0).Item("MTO_FACTURA_CON_IVA") + dtsFiltros.Tables(0).Rows(0).Item("MTOSEG")

                'Obtenemos el monto de los gastos
                For Each objRow In dtsCargosIni.Tables(0).Rows
                    intPaso = Val(objRow.Item("ORDEN"))
                    If intPaso <= 1 Then
                        ' AOC CAT
                        dblMtoGastos += IIf(objRow.Item("ORDEN") = 1, (objRow.Item("MONTOSIVA")), objRow.Item("MONTO"))
                        ' AOC CAT
                    End If
                Next

                'BUG-PC-50 MAPH 28/03/2016 Comisión por apertura en el CAT
                If Not dtsFiltros.Tables(0).Rows(0).Item("COMAPERT") Is Nothing Then
                    dblMtoGastos += Convert.ToDouble(dtsFiltros.Tables(0).Rows(0).Item("COMAPERT").ToString())
                End If

                'Al capital le quitamos el monto de los gastos que afectan al CAT
                dblMtoInicial = dblMtoCapital * -1
                dblMtoInicial += dblMtoGastos

                'BUG-PC-46 14/03/2017 MAPH Recálculo del CAT
                dblMtoInicial = dblMtoInicial * dtsFiltros.Tables(0).Rows(0).Item("VALOR_TIPO_CAMBIO")
                dtsPagosCAT = _ObjCot.ConsultaTablaAmortCAT()

                'llenamos el arreglo de valores (sin considerar el pago cero
                'intPlazo = dtsPagos.Tables(0).Rows.Count
                'For Each objRow In dtsPagos.Tables(0).Rows

                'BUG-PC-46 14/03/2017 MAPH Recálculo del CAT 
                intPlazo = dtsPagosCAT.Tables(0).Rows.Count
                For Each objRow In dtsPagosCAT.Tables(0).Rows
                    If Val(objRow.Item("NO_PAGO").ToString) = 0 Then
                        intPlazo -= 1
                        Exit For
                    End If
                Next

                ReDim arrValores(intPlazo + 1)

                'el primer valor es monto inicial
                arrValores(0) = dblMtoInicial

                'los valores siguientes son los pagos
                intPaso = 1
                'For Each objRow In dtsPagos.Tables(0).Rows

                'BUG-PC-46 14/03/2017 MAPH Recálculo del CAT
                For Each objRow In dtsPagosCAT.Tables(0).Rows

                    'BUG-PC-49 El segundo valor es el del período irregular, por ello se incluye disyuntiva
                    If Val(objRow.Item("NO_PAGO").ToString) > 0 Then
                        'arrValores(intPaso) = objRow.Item("PAGO") '+ objRow.Item("IVA")
                        arrValores(intPaso) = IIf(IsDBNull(objRow.Item("PAGO")), 0, objRow.Item("PAGO")) + IIf(IsDBNull(objRow.Item("PAGO_ESP")), 0, objRow.Item("PAGO_ESP")) + IIf(IsDBNull(objRow.Item("PAGO_SEG")), 0, objRow.Item("PAGO_SEG")) - IIf(IsDBNull(objRow.Item("SEG_CONT")), 0, objRow.Item("SEG_CONT"))
                        'si es el ultimo pago se le suma la opcion de compra y se le resta el depósito en garantía
                        intPaso += 1
                        'BUG-PC-69:MPUESTO:30/05/2017:CORRECCION DEL CAT Y COMISIÓN POR APERTURA
                    ElseIf Val(objRow.Item("NO_PAGO").ToString) = 0 And Val(objRow.Item("TIPO_TABLA").ToString) = 4 Then
                        arrValores(intPaso) = IIf(IsDBNull(objRow.Item("INTERES")), 0, objRow.Item("INTERES"))
                        intPaso += 1
                    End If

                Next
            Else
                intPlazo = _ObjCot.ValorPlazo 'INC-B-1555 SE QUITA - 1 PARA QUE LA MATRIZ EMPIEZE EN 0
                ReDim arrValores(intPlazo)
                'los valores siguientes son los pagos
                intPaso = 0
                For Each objRow In dsPagosAP.Tables(0).Rows
                    arrValores(intPaso) = objRow.Item("PAGO")
                    intPaso += 1
                Next
            End If

            'obtenemos la TIR
            dblTIR = Microsoft.VisualBasic.IRR(arrValores, 0)
            If dblTIR < 0 Then
                strErrCotiza = "No se puede calcular el CAT, la TIR da una tasa negativa"
                'CalculaCat = dtsFiltros.Tables(0).Rows(0)(8) + 1.56
            Else
                'Obtenemos el CAT
                If Val(objParam.Valor) = 1 Then
                    'se debe validar si se calcula el cat solo multiplicando el valor de la TIR por el plazo anual
                    'CalculaCat = (dblTIR * intTiempoCAT * 100) ' se comenta para aplicar correctamente la formula
                    CalculaCat = (((1 + dblTIR) ^ 12) - 1) * 100 ' AOC CAT se aplica la formula de banxico para obtencion de CAT a partir de la TIR

                Else
                    CalculaCat = ((((1 + dblTIR) ^ intTiempoCAT) - 1) * 100) / 1.16
                End If

            End If

            'INC-B-1987
            CalculaCat = System.Math.Round(CalculaCat, 1)

            _ObjCot.dblTasaIntVar = Convert.ToDecimal(CalculaCat) 'INC-B-1555 Se cambia la variable cat por CalculaCat
            _ObjCot.ManejaCotizacion(19)




        Catch ex As Exception
            strErrCotiza = ex.Message
        End Try
    End Function

    Public Function ObtenEnganchePagoInicial() As DataSet
        strErrCotiza = ""
        ObtenEnganchePagoInicial = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion
            ArmaParametros(strParamStored, TipoDato.Entero, "idCot", intCotiza.ToString)

            ObtenEnganchePagoInicial = objSD.EjecutaStoredProcedure("spObtenEnganchePagoInicial", strErrCotiza, strParamStored)

        Catch ex As Exception
            strErrCotiza = ex.Message
        End Try
    End Function

    Public Function ObtenLeyendasReporte() As DataSet
        strErrCotiza = ""
        ObtenLeyendasReporte = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", "1")
            ArmaParametros(strParamStored, TipoDato.Entero, "idPerJuridica", intPerJur.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "idClasifProd", intClasif.ToString)

            ObtenLeyendasReporte = objSD.EjecutaStoredProcedure("spManejaLeyendasReporte", strErrCotiza, strParamStored)

        Catch ex As Exception
            strErrCotiza = ex.Message
        End Try
    End Function

    Public Function ObtenFechasPagos() As DataSet
        strErrCotiza = ""
        ObtenFechasPagos = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion
            'If intCotiza > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idCot", intCotiza.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "plazo", ValorPlazo.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "periodicidad", IDPeriodicidad.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "tipoVenc", IDTipoVencimiento.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "idMoneda", IDMoneda.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "idMonedaFact", IDMonedaFactura.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "tipoCambio", dblValorTipoCambio.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "tipo_tabla", 1)
            ArmaParametros(strParamStored, TipoDato.Entero, "diaPago", intDiaPago.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "regresaTabla", 1)
            ObtenFechasPagos = objSD.EjecutaStoredProcedure("spCreaFechasPagosEsp", strErrCotiza, strParamStored)
        Catch ex As Exception
            strErrCotiza = ex.Message
        End Try
    End Function

    Public Function ObtenPagoPeriodo(Optional ByVal blnSeguro As Boolean = False) As Double
        strErrCotiza = ""
        ObtenPagoPeriodo = 0
        Dim strParamStored As String = ""
        Dim dtsRes As DataSet

        Try
            Dim objSD As New clsConexion
            ArmaParametros(strParamStored, TipoDato.Entero, "idCot", intCotiza.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "tipo_tabla", IIf(blnSeguro, 2, 1))
            ArmaParametros(strParamStored, TipoDato.Doble, "tasaInteresPeriodo", 0)
            ArmaParametros(strParamStored, TipoDato.Doble, "tasaInteresDiaria", 0)
            ArmaParametros(strParamStored, TipoDato.Doble, "mtoPagoPeriodo", 0)
            ArmaParametros(strParamStored, TipoDato.Doble, "tasaInteresPeriodoPCP", 0)
            ArmaParametros(strParamStored, TipoDato.Doble, "tasaInteresDiariaPCP", 0)
            ArmaParametros(strParamStored, TipoDato.Entero, "regresaInfo", 1)

            dtsRes = objSD.EjecutaStoredProcedure("spCalculaMontoPago", strErrCotiza, strParamStored)
            If strErrCotiza = "" Then
                ObtenPagoPeriodo = dtsRes.Tables(0).Rows(0).Item(0)
            End If
        Catch ex As Exception
            strErrCotiza = ex.Message
        End Try
    End Function

    Public Function LlenaGrid() As DataSet
        strErrCotiza = ""
        LlenaGrid = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "ID_PAQUETE", intPaquete.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "PRECIO", dblPrecioProd.ToString)
            ArmaParametros(strParamStored, TipoDato.Doble, "ENGANCHE", dblEnganche.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "ID_PER_JURIDICA", intPerJur.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "ID_TIPO_SEGURO", intTipoSeg.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "ID_TIPO_OPER", intTipoOper.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "UNIDADES_PRODUCTO", intUnidadesProd.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "ID_TASA_IVA", IDTasaIVA.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "idcobertura", intIDCobertura.ToString) 'RQ06
            ArmaParametros(strParamStored, TipoDato.Entero, "intidtipo", inttipo.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "USO", intuso.ToString)
            ArmaParametros(strParamStored, TipoDato.Doble, "MONTOACC", dblMtoAccesorios.ToString)
            ArmaParametros(strParamStored, TipoDato.Doble, "SEGPROD", dblMtoSeguro.ToString)
            ArmaParametros(strParamStored, TipoDato.Doble, "SEGGAP ", dblMontoSeguro1.ToString)
            ArmaParametros(strParamStored, TipoDato.Doble, "SEGVIDA", dblMontoSeguro2.ToString)
            ArmaParametros(strParamStored, TipoDato.Doble, "DERECHO", dblMontoSeguro3.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "PLAZO", ValorPlazo.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "IDCLASIF", intClasif.ToString)
            ArmaParametros(strParamStored, TipoDato.Doble, "PTJENGANCHE", dblPtjEnganche.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "BANDERA", intBandera.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "VERSION", intidversion.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "idaseguradora", IDAseguradora.ToString)  'RQ06
            ArmaParametros(strParamStored, TipoDato.Entero, "idedo", IDEstado.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "PlazoMod", PlazoMod.ToString)
            If intBroker > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idbroker", intBroker.ToString)
            If strparams <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "params", strparams)
            ArmaParametros(strParamStored, TipoDato.Doble, "MtoGarantia", dbMtoGarantia.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "TipoSegVida", IDAplicacionSeguroVida.ToString)
            ArmaParametros(strParamStored, TipoDato.Doble, "ID_prod", IDProducto)
            If IsMulticotizacion = 0 Then
                ArmaParametros(strParamStored, TipoDato.Entero, "IsMulticotiza", IsMulticotizacion)
                ArmaParametros(strParamStored, TipoDato.Doble, "ValorPlazo", ValorPlazo)
            End If
            LlenaGrid = objSD.EjecutaStoredProcedure("spMulticotiza", strErrCotiza, strParamStored)

        Catch ex As Exception
            strErrCotiza = ex.Message
        End Try
    End Function

    Public Function ConsultaTablaAmort() As DataSet
        strErrCotiza = ""
        ConsultaTablaAmort = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion
            ArmaParametros(strParamStored, TipoDato.Entero, "idCot", intCotiza.ToString)
            ConsultaTablaAmort = objSD.EjecutaStoredProcedure("SP_CONSULTATBAMORTIZA", strErrCotiza, strParamStored)
        Catch ex As Exception
            strErrCotiza = ex.Message
        End Try
    End Function
    Public Function ConsultaTablaAmort_6() As DataSet
        strErrCotiza = ""
        ConsultaTablaAmort_6 = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion
            ArmaParametros(strParamStored, TipoDato.Entero, "idCot", intCotiza.ToString)
            ConsultaTablaAmort_6 = objSD.EjecutaStoredProcedure("SP_CONSULTATBAMORTIZA_6", strErrCotiza, strParamStored)
        Catch ex As Exception
            strErrCotiza = ex.Message
        End Try
    End Function

    Public Function CargaCombos() As DataSet
        strErrCotiza = ""
        CargaCombos = New DataSet

        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion
            ArmaParametros(strParamStored, TipoDato.Entero, "idusuario", intVendedor.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "idagencia", intAgencia.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "idpaquete", intPaquete.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "idmarca", intMarca.ToString)
            CargaCombos = objSD.EjecutaStoredProcedure("spCargaCombos", strErrCotiza, strParamStored)
        Catch ex As Exception
            strErrCotiza = ex.Message
        End Try

    End Function

    Public Function ConsultaTablaAmortCAT() As DataSet
        strErrCotiza = ""
        ConsultaTablaAmortCAT = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion
            ArmaParametros(strParamStored, TipoDato.Entero, "idCot", intCotiza.ToString)
            ConsultaTablaAmortCAT = objSD.EjecutaStoredProcedure("SP_ConsultaTablaAmortizaCAT", strErrCotiza, strParamStored)
        Catch ex As Exception
            strErrCotiza = ex.Message
        End Try
    End Function


End Class
