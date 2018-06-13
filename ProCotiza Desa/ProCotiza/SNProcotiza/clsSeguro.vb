'BBVA-P-412: 25-08-2016: AMR: RQCOT-01 Ajustes a la pantalla de Cotización.
'BBVA-P-412 RQ06: AMR: 20/09/2016 Administración de Planes de Financiamiento: Promociones
'BUG-PC-58:AMATA:03/05/2017:Seguros Ordas
'BUG-PC-70: RHERNANDEZ: 30/05/17 SE MODIFICA GUARDADO DE SEGUROS PARA GUARDAR LOS NUMEROS DE POLIZA DE SEGUROS DE VIDA Y DE DAÑOS
'BUG-PD-68: RHERNANDEZ: 02/06/17: SE MODIFICA LA CLASE PARA PERMITIR GUARDAR EL DATO DE TIPO DE USO DEL SEGURO y PROBLEMA DE ENVIO DE SEGURO DE VIDA A SEGXFACT.
'BUG-PC-72: RHERNANDEZ: 08/06/17: SE ARREGLA PROBLEMA DE PERDIDA DE RECIBOS AL GUARDAR COTIZACIONES DE SEGUROS POR FACTOR Y SE GUARDA EDO DEL SEGURO PARA EMISION DE SEGUROS BANCOMER
'BUG-PC-147: RHERNANDEZ: 19/02/18: SE AGREGA VARIABLE QUE CON TENDRA EL IDQUOTE DEL SEGURO DE VIDA BBVA
'BUG-PC-177: JMENDIETA: 16/04/2017 Se agrega opcion 7.
'BUG-PC-195: RHERNANDEZ: 18/05/2018: Se corrigue problema al guardar cotizacion por factor
Imports SDManejaBD

Public Class clsSeguro
    Inherits clsSession

#Region "Variables"

    'SEG_FL_CVE 			INT NOT NULL PRIMARY KEY,
    Private intClaveSeguro As Integer = 0
    'ID_COTIZACION INT,
    Private intClaveCotizacion As Integer = 0
    'SEG_NO_PRIMANETA		NUMERIC(13,2),
    Private decSegPrimaNeta As Decimal = 0.0
    'SEG_NO_RECARGO		 numeric(13,2)
    Private decSegRecargo As Decimal = 0.0
    'SEG_NO_DERECHO, numeric(13,2)
    Private decSegDerecho As Decimal = 0.0
    'SEG_NO_IVA, numeric(13,2)
    Private decSegIva As Decimal = 0.0
    'SEG_NO_PRIMATOTAL, numeric(13,2)
    Private decSegPrimaTotal As Decimal = 0.0
    'SEG_DS_ASEGURADORA, varchar(100),
    Private strSegAseguradora As String = String.Empty
    'SEG_NO_PNGAP, numeric(13,2)
    Private decSegPrimaNGap As Decimal = 0.0
    'SEG_NO_IVAGAP, numeric(13,2)
    Private decSegIvaGap As Decimal = 0.0
    'SEG_NO_PTGAP NUMERIC(13,2)
    Private decSegPrimaTotalGap As Decimal = 0.0
    'SEG_NO_VIDA, numeric(13,2)
    Private decSegVida As Decimal = 0.0
    'SEG_DS_NUMCOTIZACION VARCHAR(50)
    Private strNumCotizacion As String = String.Empty
    'SEG_DS_NOM_COT_VIDA VARCHAR(50)
    Private strNumCotizacionvida As String = String.Empty
    'TIPO_SEGURO
    Private IntSegDanios As Integer = 0



    'Inicia Monto Plazos seguros
    'SEG_NO_PRIMANETA1		NUMERIC(13,2),
    Private decSegPrimaNeta1 As Decimal = 0.0
    'SEG_NO_RECARGO1		 numeric(13,2)
    Private decSegRecargo1 As Decimal = 0.0
    'SEG_NO_DERECHO1, numeric(13,2)
    Private decSegDerecho1 As Decimal = 0.0
    'SEG_NO_IVA1, numeric(13,2)
    Private decSegIva1 As Decimal = 0.0
    'SEG_NO_PRIMATOTAL1, numeric(13,2)
    Private decSegPrimaTotal1 As Decimal = 0.0
    'SEG_NO_PRIMANETA2		NUMERIC(13,2),
    Private decSegPrimaNeta2 As Decimal = 0.0
    'SEG_NO_RECARGO2		 numeric(13,2)
    Private decSegRecargo2 As Decimal = 0.0
    'SEG_NO_DERECHO2, numeric(13,2)
    Private decSegDerecho2 As Decimal = 0.0
    'SEG_NO_IVA2, numeric(13,2)
    Private decSegIva2 As Decimal = 0.0
    'SEG_NO_PRIMATOTAL2, numeric(13,2)
    Private decSegPrimaTotal2 As Decimal = 0.0
    'SEG_NO_PRIMANETA3		NUMERIC(13,2),
    Private decSegPrimaNeta3 As Decimal = 0.0
    'SEG_NO_RECARGO3		 numeric(13,2)
    Private decSegRecargo3 As Decimal = 0.0
    'SEG_NO_DERECHO2, numeric(13,2)
    Private decSegDerecho3 As Decimal = 0.0
    'SEG_NO_IVA2, numeric(13,2)
    Private decSegIva3 As Decimal = 0.0
    'SEG_NO_PRIMATOTAL2, numeric(13,2)
    Private decSegPrimaTotal3 As Decimal = 0.0
    'SEG_FG_ANIOGRATIS, INT
    Private IntAnioGratis As Integer = 0
    'Finaliza Monto Plazos seguros
    'SEG REG
    Private decPrimaTotalSG As Decimal = 0.0
    'YAM-P-207
    Private IntTCB_FL_CVE As Integer = 0

    'Seguro Interno Yamaha
    Private dsSeguroInterno As New DataSet

    Public intProducto As Integer = 0
    Public intPeriodicidad As Integer = 0

    Private intedadrec As Integer = 0
    Private intsexorec As Integer = 0
    Private strnombrerec As String = String.Empty
    Private intidcobertura As Integer = 0
    Private strCodigoPostal As String = String.Empty

    'RQ06
    Private intaccion As Integer = 0
    Private intidaseguradora As Integer = 0
    Private decprecio As Decimal = 0
    Private deciva As Decimal = 0
    Private decmtoacc As Decimal = 0
    Private intidclasif As Integer = 0
    Private intidpaq As Integer = 0
    Private intidedo As Integer = 0
    Private intidplazo As Integer = 0
    Private intidcotia As Integer = 0
    Private strErrorSeguro As String = String.Empty

    Private SEG_DS_NOM_COT_AUTO_BBVA As String = String.Empty
    Private ID_USO As String = String.Empty

#End Region

#Region "Propiedades"
    Public Property DtSSeguroInterno As DataSet
        Get
            Return dsSeguroInterno
        End Get
        Set(value As DataSet)
            value = dsSeguroInterno
        End Set
    End Property

    ''' <summary>
    ''' Clave de la tabla de seguros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _intClaveSeguro() As Integer
        Get
            Return intClaveSeguro
        End Get
        Set(ByVal value As Integer)
            intClaveSeguro = value
        End Set
    End Property

    ''' <summary>
    ''' Clave de la cotizacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _intClaveCotizacion() As Integer
        Get
            Return intClaveCotizacion
        End Get
        Set(ByVal value As Integer)
            intClaveCotizacion = value
        End Set
    End Property

    ''' <summary>
    ''' Prima neta del seguro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegPrimaNeta() As Decimal
        Get
            Return decSegPrimaNeta
        End Get
        Set(ByVal value As Decimal)
            decSegPrimaNeta = value
        End Set
    End Property

    ''' <summary>
    ''' Monto del recargo del seguro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegRecargo() As Decimal
        Get
            Return decSegRecargo
        End Get
        Set(ByVal value As Decimal)
            decSegRecargo = value
        End Set
    End Property

    ''' <summary>
    ''' Monto del derecho del seguro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegDerecho() As Decimal
        Get
            Return decSegDerecho
        End Get
        Set(ByVal value As Decimal)
            decSegDerecho = value
        End Set
    End Property

    ''' <summary>
    ''' Monto del iva del seguro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegIva() As Decimal
        Get
            Return decSegIva
        End Get
        Set(ByVal value As Decimal)
            decSegIva = value
        End Set
    End Property

    ''' <summary>
    ''' Monto de la prima total del seguro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegPrimaTotal() As Decimal
        Get
            Return decSegPrimaTotal
        End Get
        Set(ByVal value As Decimal)
            decSegPrimaTotal = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre de la aseguradora
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _strSegAseguradora() As String
        Get
            Return strSegAseguradora
        End Get
        Set(ByVal value As String)
            strSegAseguradora = value
        End Set
    End Property

    ''' <summary>
    ''' Prima neta del seguro GAP
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegPrimaNGap() As Decimal
        Get
            Return decSegPrimaNGap
        End Get
        Set(ByVal value As Decimal)
            decSegPrimaNGap = value
        End Set
    End Property

    ''' <summary>
    ''' Monto del iva del seguro GAP
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegIvaGap() As Decimal
        Get
            Return decSegIvaGap
        End Get
        Set(ByVal value As Decimal)
            decSegIvaGap = value
        End Set
    End Property

    ''' <summary>
    ''' Monto de la prima total del seguro gap
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegPrimaTotalGap() As Decimal
        Get
            Return decSegPrimaTotalGap
        End Get
        Set(ByVal value As Decimal)
            decSegPrimaTotalGap = value
        End Set
    End Property

    ''' <summary>
    ''' Monto del seguro de vida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegVida() As Decimal
        Get
            Return decSegVida
        End Get
        Set(ByVal value As Decimal)
            decSegVida = value
        End Set
    End Property

    ''' <summary>
    ''' Numero de la cotizacion que viene del seguro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _strNumCotizacion() As String
        Get
            Return strNumCotizacion
        End Get
        Set(ByVal value As String)
            strNumCotizacion = value
        End Set
    End Property

    ''' <summary>
    ''' Id quote del seguro de Vida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _strNumCotizacionvida() As String
        Get
            Return strNumCotizacionvida
        End Get
        Set(ByVal value As String)
            strNumCotizacionvida = value
        End Set
    End Property



    ''' <summary>
    ''' Id Tipo Seguro de Danios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _IntSegDanios() As String
        Get
            Return IntSegDanios
        End Get
        Set(ByVal value As String)
            IntSegDanios = value
        End Set
    End Property

    'Inicia Monto Plazos seguros
    ''' <summary>
    ''' Prima neta del seguro Plazo 1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegPrimaNeta1() As Decimal
        Get
            Return decSegPrimaNeta1
        End Get
        Set(ByVal value As Decimal)
            decSegPrimaNeta1 = value
        End Set
    End Property

    ''' <summary>
    ''' Monto del recargo del seguro Plazo 1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegRecargo1() As Decimal
        Get
            Return decSegRecargo1
        End Get
        Set(ByVal value As Decimal)
            decSegRecargo1 = value
        End Set
    End Property

    ''' <summary>
    ''' Monto del derecho del seguro Palzo 1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegDerecho1() As Decimal
        Get
            Return decSegDerecho1
        End Get
        Set(ByVal value As Decimal)
            decSegDerecho1 = value
        End Set
    End Property

    ''' <summary>
    ''' Monto del iva del seguro Plazo 1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegIva1() As Decimal
        Get
            Return decSegIva1
        End Get
        Set(ByVal value As Decimal)
            decSegIva1 = value
        End Set
    End Property

    ''' <summary>
    ''' Monto de la prima total del seguro Plazo 1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegPrimaTotal1() As Decimal
        Get
            Return decSegPrimaTotal1
        End Get
        Set(ByVal value As Decimal)
            decSegPrimaTotal1 = value
        End Set
    End Property


    ''' <summary>
    ''' Prima neta del seguro Plazo 2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegPrimaNeta2() As Decimal
        Get
            Return decSegPrimaNeta2
        End Get
        Set(ByVal value As Decimal)
            decSegPrimaNeta2 = value
        End Set
    End Property

    ''' <summary>
    ''' Monto del recargo del seguro Plazo 2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegRecargo2() As Decimal
        Get
            Return decSegRecargo2
        End Get
        Set(ByVal value As Decimal)
            decSegRecargo2 = value
        End Set
    End Property

    ''' <summary>
    ''' Monto del derecho del seguro Plazo 2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegDerecho2() As Decimal
        Get
            Return decSegDerecho2
        End Get
        Set(ByVal value As Decimal)
            decSegDerecho2 = value
        End Set
    End Property

    ''' <summary>
    ''' Monto del iva del seguro Plazo 2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegIva2() As Decimal
        Get
            Return decSegIva2
        End Get
        Set(ByVal value As Decimal)
            decSegIva2 = value
        End Set
    End Property

    ''' <summary>
    ''' Monto de la prima total del seguro Plazo 2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegPrimaTotal2() As Decimal
        Get
            Return decSegPrimaTotal2
        End Get
        Set(ByVal value As Decimal)
            decSegPrimaTotal2 = value
        End Set
    End Property


    ''' <summary>
    ''' Prima neta del seguro Plazo 3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegPrimaNeta3() As Decimal
        Get
            Return decSegPrimaNeta3
        End Get
        Set(ByVal value As Decimal)
            decSegPrimaNeta3 = value
        End Set
    End Property

    ''' <summary>
    ''' Monto del recargo del seguro Plazo 3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegRecargo3() As Decimal
        Get
            Return decSegRecargo3
        End Get
        Set(ByVal value As Decimal)
            decSegRecargo3 = value
        End Set
    End Property

    ''' <summary>
    ''' Monto del derecho del seguro Plazo 3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegDerecho3() As Decimal
        Get
            Return decSegDerecho3
        End Get
        Set(ByVal value As Decimal)
            decSegDerecho3 = value
        End Set
    End Property

    ''' <summary>
    ''' Monto del iva del seguro Plazo 3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegIva3() As Decimal
        Get
            Return decSegIva3
        End Get
        Set(ByVal value As Decimal)
            decSegIva3 = value
        End Set
    End Property

    ''' <summary>
    ''' Monto de la prima total del seguro Plazo 3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decSegPrimaTotal3() As Decimal
        Get
            Return decSegPrimaTotal3
        End Get
        Set(ByVal value As Decimal)
            decSegPrimaTotal3 = value
        End Set
    End Property

    Public Property _IntAnioGratis() As Integer
        Get
            Return IntAnioGratis
        End Get
        Set(ByVal value As Integer)
            IntAnioGratis = value
        End Set
    End Property
    'SEG REG
    Public Property _decPrimaTotalSG() As Decimal
        Get
            Return decPrimaTotalSG
        End Get
        Set(ByVal value As Decimal)
            decPrimaTotalSG = value
        End Set
    End Property

    'YAM-P-207
    Public Property _IntTCB_FL_CVE() As Integer
        Get
            Return IntTCB_FL_CVE
        End Get
        Set(ByVal value As Integer)
            IntTCB_FL_CVE = value
        End Set
    End Property
    'Finaliza Monto Plazos seguros

    Public Property EdadRec() As Integer
        Get
            Return intedadrec
        End Get
        Set(value As Integer)
            intedadrec = value
        End Set
    End Property

    Public Property SexoRec() As Integer
        Get
            Return intsexorec
        End Get
        Set(value As Integer)
            intsexorec = value
        End Set
    End Property


    Public Property NombreRec() As String
        Get
            Return strnombrerec
        End Get
        Set(value As String)
            strnombrerec = value
        End Set
    End Property

    Public Property IDCobertura As Integer
        Get
            Return intidcobertura
        End Get
        Set(value As Integer)
            intidcobertura = value
        End Set
    End Property

    'RQ06
    Public Property IDAccion As Integer
        Get
            Return intaccion
        End Get
        Set(value As Integer)
            intaccion = value
        End Set
    End Property

    Public Property IDAseguradora As Integer
        Get
            Return intidaseguradora
        End Get
        Set(value As Integer)
            intidaseguradora = value
        End Set
    End Property

    Public Property Precio As Decimal
        Get
            Return decprecio
        End Get
        Set(value As Decimal)
            decprecio = value
        End Set
    End Property

    Public Property IVA As Decimal
        Get
            Return deciva
        End Get
        Set(value As Decimal)
            deciva = value
        End Set
    End Property

    Public Property MontoAcc As Decimal
        Get
            Return decmtoacc
        End Get
        Set(value As Decimal)
            decmtoacc = value
        End Set
    End Property

    Public Property IDClasif As Integer
        Get
            Return intidclasif
        End Get
        Set(value As Integer)
            intidclasif = value
        End Set
    End Property

    Public Property IDPaquete As Integer
        Get
            Return intidpaq
        End Get
        Set(value As Integer)
            intidpaq = value
        End Set
    End Property

    Public Property IDEstado As Integer
        Get
            Return intidedo
        End Get
        Set(value As Integer)
            intidedo = value
        End Set
    End Property

    Public Property IDPlazo As Integer
        Get
            Return intidplazo
        End Get
        Set(value As Integer)
            intidplazo = value
        End Set
    End Property

    Public ReadOnly Property ErrorSeguro
        Get
            Return strErrorSeguro
        End Get
    End Property

    Public Property CodigoPostal As String
        Get
            Return strCodigoPostal
        End Get
        Set(value As String)
            strCodigoPostal = value
        End Set
    End Property

    Public Property _SEG_DS_NOM_COT_AUTO_BBVA As String
        Get
            Return SEG_DS_NOM_COT_AUTO_BBVA
        End Get
        Set(value As String)
            SEG_DS_NOM_COT_AUTO_BBVA = value
        End Set
    End Property
    Public Property _ID_USO As String
        Get
            Return ID_USO
        End Get
        Set(value As String)
            ID_USO = value
        End Set
    End Property

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Sub New()
        Return
    End Sub

    ''' <summary>
    ''' Guarda y Obtiene los seguros basado en el numero de cotizacion
    ''' </summary>
    ''' <param name="intOper"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ManejaSeguro(ByVal intOper As Integer) As DataSet
        Dim strErrCotiza As String = ""
        ManejaSeguro = New DataSet
        Dim strParamStored As String = ""

        Try

            Dim objSD As New clsConexion
            'RQ06
            If intOper <> 6 Then ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1
                    ArmaParametros(strParamStored, TipoDato.Entero, "intClaveCotizacion", intClaveCotizacion)
                Case 2
                    'ArmaParametros(strParamStored, TipoDato.Entero, "intClaveSeguro", intClaveSeguro)
                    ArmaParametros(strParamStored, TipoDato.Entero, "intClaveCotizacion", intClaveCotizacion)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegPrimaNeta", decSegPrimaNeta)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegRecargo", decSegRecargo)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegDerecho", decSegDerecho)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegIva", decSegIva.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegPrimaTotal", decSegPrimaTotal)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "strSegAseguradora", strSegAseguradora.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegPrimaNGap", decSegPrimaNGap)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegIvaGap", decSegIvaGap)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegPrimaTotalGap", decSegPrimaTotalGap)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegVida", decSegVida)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "strNumCotizacion", strNumCotizacion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "strNumCotizacionvida", strNumCotizacionvida.ToString)
                    'Inicia Monto Plazos seguros
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegPrimaNeta1", decSegPrimaNeta1)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegRecargo1", decSegRecargo1)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegDerecho1", decSegDerecho1)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegIva1", decSegIva1.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegPrimaTotal1", decSegPrimaTotal1)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegPrimaNeta2", decSegPrimaNeta2)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegRecargo2", decSegRecargo2)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegDerecho2", decSegDerecho2)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegIva2", decSegIva2.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegPrimaTotal2", decSegPrimaTotal2)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegPrimaNeta3", decSegPrimaNeta3)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegRecargo3", decSegRecargo3)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegDerecho3", decSegDerecho3)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegIva3", decSegIva3.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegPrimaTotal3", decSegPrimaTotal3)
                    ArmaParametros(strParamStored, TipoDato.Entero, "IntAnioGratis", IntAnioGratis)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decPrimaTotalSG", decPrimaTotalSG)
                    ArmaParametros(strParamStored, TipoDato.Entero, "IntTCB_FL_CVE", IntTCB_FL_CVE)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "SEG_DS_NOM_COT_AUTO_BBVA", SEG_DS_NOM_COT_AUTO_BBVA)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Tipo_uso", ID_USO)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "edo_circulacion", IDEstado)
                    'Finaliza Monto Plazos seguros
                    If intedadrec > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "edad", intedadrec.ToString)
                    If intsexorec > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "sexo", intsexorec.ToString)
                    If strnombrerec.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nombre", strnombrerec)
                    If intidcobertura > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idcobertura", intidcobertura.ToString)
                    If strCodigoPostal.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "codigopostal", strCodigoPostal)
                Case 3
                    ArmaParametros(strParamStored, TipoDato.Entero, "intClaveCotizacion", intClaveCotizacion)
                Case 4
                    ''Consultaba REL_PASCOT_COTIZA
                Case 5
                    ArmaParametros(strParamStored, TipoDato.Entero, "id_producto", intProducto)
                    ArmaParametros(strParamStored, TipoDato.Entero, "intPeriodicidad", intPeriodicidad)
                Case 6 ''Seguro X Factor intaccion	 'RQ06
                    If intaccion > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "intaccion", intaccion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idaseguradora", intidaseguradora.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "precio", decprecio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "iva", deciva.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "accesorios", decmtoacc.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idclasif", intidclasif.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idpaq", intidpaq.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idedo", intidedo.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idcobertura", intidcobertura.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idplazo", intidplazo.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idcotiza", intClaveCotizacion.ToString)
                    If intedadrec > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "edad", intedadrec.ToString)
                    If intsexorec > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "sexo", intsexorec.ToString)
                    If strnombrerec.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nombre", strnombrerec)
                    ArmaParametros(strParamStored, TipoDato.Entero, "AnioGratis", IntAnioGratis.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "decSegVida", decSegVida)
                    ArmaParametros(strParamStored, TipoDato.Entero, "tiposeg", IntSegDanios)
                    ArmaParametros(strParamStored, TipoDato.Doble, "id_prod", intProducto)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "strNumCotizacionvida", strNumCotizacionvida.ToString)
                Case 7 'BUG-PC-177
                    ArmaParametros(strParamStored, TipoDato.Entero, "intClaveCotizacion", intClaveCotizacion)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "codigopostal", CodigoPostal)
            End Select
            'RQ06
            ManejaSeguro = objSD.EjecutaStoredProcedure(IIf(intOper = 6, "procCalculoSegXFactor", "spManejaSeguro"), strErrCotiza, strParamStored)
            If strErrCotiza.Trim.Length > 0 Then
                strErrorSeguro = strErrCotiza
                Throw New Exception(strErrorSeguro)
            End If

            If intOper = 2 Then
                intClaveSeguro = ManejaSeguro.Tables(0).Rows(0).Item("CLAVESEGURO")
            End If

            If intOper = 1 Then
                If ManejaSeguro Is Nothing OrElse ManejaSeguro.Tables.Count = 0 OrElse ManejaSeguro.Tables(0).Rows.Count = 0 Then
                    Return Nothing
                Else
                    With ManejaSeguro.Tables(0).Rows(0)
                        intClaveSeguro = .Item("SEG_FL_CVE")
                        intClaveCotizacion = .Item("ID_COTIZACION")
                        decSegPrimaNeta = .Item("SEG_NO_PRIMANETA")
                        decSegRecargo = .Item("SEG_NO_RECARGO")
                        decSegDerecho = .Item("SEG_NO_DERECHO")
                        decSegIva = .Item("SEG_NO_IVA")
                        decSegPrimaTotal = .Item("SEG_NO_PRIMATOTAL")
                        strSegAseguradora = .Item("SEG_DS_ASEGURADORA").ToString
                        decSegPrimaNGap = .Item("SEG_NO_PNGAP")
                        decSegIvaGap = .Item("SEG_NO_IVAGAP")
                        decSegPrimaTotalGap = .Item("SEG_NO_PTGAP")
                        decSegVida = .Item("SEG_NO_VIDA")
                        strNumCotizacion = .Item("SEG_DS_NUMCOTIZACION").ToString
                        'Inicia Monto Plazos seguros
                        decSegPrimaNeta1 = .Item("SEG_NO_PRIMANETA1")
                        decSegRecargo1 = .Item("SEG_NO_RECARGO1")
                        decSegDerecho1 = .Item("SEG_NO_DERECHO1")
                        decSegIva1 = .Item("SEG_NO_IVA1")
                        decSegPrimaTotal1 = .Item("SEG_NO_PRIMATOTAL1")
                        decSegPrimaNeta2 = .Item("SEG_NO_PRIMANETA2")
                        decSegRecargo2 = .Item("SEG_NO_RECARGO2")
                        decSegDerecho2 = .Item("SEG_NO_DERECHO2")
                        decSegIva2 = .Item("SEG_NO_IVA2")
                        decSegPrimaTotal2 = .Item("SEG_NO_PRIMATOTAL2")
                        decSegPrimaNeta3 = .Item("SEG_NO_PRIMANETA3")
                        decSegRecargo3 = .Item("SEG_NO_RECARGO3")
                        decSegDerecho3 = .Item("SEG_NO_DERECHO3")
                        decSegIva3 = .Item("SEG_NO_IVA3")
                        decSegPrimaTotal3 = .Item("SEG_NO_PRIMATOTAL3")
                        decPrimaTotalSG = .Item("PRIMA_TOTAL_SG")
                        'IntTCB_FL_CVE = .Item("IntTCB_FL_CVE")
                        intedadrec = .Item("SEG_EDAD_REC")
                        intsexorec = .Item("SEG_SEXO_REC")
                        strnombrerec = .Item("SEG_NOMBRE_REC")
                        intidcobertura = .Item("ID_COBERTURA")
                        CodigoPostal = .Item("CODIGO_POSTAL")
                        'Finaliza Monto Plazos seguros
                    End With
                End If
            End If

            If strErrCotiza.Trim.Length > 0 Then
                Throw New Exception(strErrCotiza)
            End If

        Catch ex As Exception
            strErrCotiza = ex.Message
        End Try
    End Function
#End Region
End Class
