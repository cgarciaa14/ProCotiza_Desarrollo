Imports SDManejaBD

Public Class clsSolicitud
    Inherits clsSession

#Region "Variables"

    'SOL_FL_CVE 			INT NOT NULL PRIMARY KEY,
    Private intClaveSolicitud As Integer = 0
    'SOL_DS_NOMBRE1 		VARCHAR(100) NOT NULL,
    Private strNombre1 As String = String.Empty
    'SOL_DS_NOMBRE2 		VARCHAR(100) NULL,
    Private strNombre2 As String = String.Empty
    'SOL_DS_APEPATERNO 	VARCHAR(50) NOT NULL,
    Private strApePaterno As String = String.Empty
    'SOL_DS_APEMATERNO 	VARCHAR(50) NOT NULL,
    Private strApeMaterno As String = String.Empty
    'SOL_DS_RFC 			VARCHAR(13) NOT NULL,
    Private strRFC As String = String.Empty
    'SOL_DS_LADA     VARCHAR(3) NOT NULL,
    Private stLada As String = String.Empty
    'SOL_DS_TELEFONO     VARCHAR(30) NOT NULL,
    Private stTelefono As String = String.Empty
    'SOL_DS_TELMOVIL		VARCHAR(30) NULL,
    Private strTelMovil As String = String.Empty
    'SOL_DS_CALLE		VARCHAR(100) NOT NULL,
    Private strCalle As String = String.Empty
    'SOL_DS_NUMEXT		VARCHAR(30) NOT NULL,
    Private strNumExt As String = String.Empty
    'SOL_DS_NUMINT		VARCHAR(30) NULL,
    Private strNumInt As String = String.Empty
    'SOL_DS_EMAIL		VARCHAR(100) NULL,
    Private strEmail As String = String.Empty
    'SOL_DS_COLONIA		VARCHAR(50) NOT NULL,
    Private strColonia As String = String.Empty
    'SOL_DS_CIUDAD		VARCHAR(100) NOT NULL,
    Private strCiudad As String = String.Empty
    'SOL_DS_ESTADO		VARCHAR(100) NOT NULL,
    Private strEstado As String = String.Empty
    'SOL_DS_CODPOST		VARCHAR(100) NOT NULL,
    Private strCodPost As String = String.Empty
    'SOL_DS_CURP			VARCHAR(50) NOT NULL,
    Private strCURP As String = String.Empty
    'SOL_DS_FIRMAELEC			VARCHAR(100) NOT NULL,
    Private strFirmElec As String = String.Empty
    'SOL_DS_SEXO 		VARCHAR(20) NOT NULL,
    Private strSexo As String = String.Empty
    'SOL_FE_NACIMIENTO	DATETIME NOT NULL,
    Private fecNacimiento As DateTime = "1900-01-01"
    'SOL_DS_PAISNAC	VARCHAR(100) NOT NULL,
    Private strPaisNac As String = String.Empty
    'SOL_DS_ESTADONAC	VARCHAR(100) NOT NULL,
    Private strEstadoNac As String = String.Empty
    'SOL_DS_NACIONALIDAD	VARCHAR(30) NOT NULL,
    Private intNacionalidad As Integer = 0
    'SOL_DS_DEPENDENCIAS	VARCHAR(10) NOT NULL,
    Private strDependencias As String = String.Empty
    'SOL_DS_VIVEN		VARCHAR(20) NOT NULL,
    Private strViven As String = String.Empty
    'SOL_FG_PROPIEDAD	INT,
    Private intPropiedad As Integer = 0
    'SOL_NO_ANIOSRECDOM	INT NOT NULL,
    Private intAnioRecDomicilio As Integer = 0
    'SOL_NO_ANIOSRECCIUDAD	INT NOT NULL,
    Private intAnioRecCiudad As Integer = 0
    'SOL_DS_BENEFICIARIO1	VARCHAR(50) NOT NULL,
    Private strBeneficiario1 As String = String.Empty
    'SOL_DS_BENEFICIARIO2	VARCHAR(50) NOT NULL,
    Private strBeneficiario2 As String = String.Empty
    'SOL_FG_EDOCIVIL		INT NOT NULL,--1=CASADO,2=SOLTERO
    Private intEdoCivil As Integer = 0
    'SOL_FG_BIENES		INT NOT NULL, 1.-MANCOMUNADOS , 2=SEPARADOS
    Private intBienes As Integer = 0
    'SOL_FG_AUTOPROPIO	INT NOT NULL, 1=SI, 2=NO
    Private intAutoPropio As Integer = 0
    'SOL_DS_MARMODTIPO	VARCHAR(50) NULL, MARCA MODELO TIPO
    Private strMarModTipo As String = String.Empty
    'SOL_DS_EMPPUESTO	VARCHAR(50) NULL,
    Private strEmpPuesto As String = String.Empty
    'SOL_DS_EMPDEPARAREA	VARCHAR(50) NULL, EMPLEO DEPARTAMENTO AREA
    Private strEmpDepArea As String = String.Empty
    'SOL_FE_EMPDESDEANIO	DATETIME NOT NULL,
    Private fecEmpDesdeAnio As Integer = 0
    'SOL_DS_COMPAÑIA		VARCHAR(50) NOT NULL,
    Private strCompania As String = String.Empty
    'SOL_DS_EMPLADA	VARCHAR(3) NOT NULL,
    Private strEmpLada As String = String.Empty
    'SOL_DS_EMPTELEFONO	VARCHAR(50) NOT NULL,
    Private strEmpTelefono As String = String.Empty
    'SOL_DS_EXT			VARCHAR(10) NULL, EXTENCION DEL TELEFONO DEL EMPLEO
    Private strEmpExt As String = String.Empty
    'SOL_NO_SUELDO		NUMERIC(13,2),
    Private strEmpSueldo As Decimal = 0.0
    'SOL_DS_EMPCALLE		VARCHAR(100) NULL,
    Private strEmpCalle As String = String.Empty
    'SOL_DS_EMPNUMEXT	VARCHAR(30) NULL,
    Private strEmpNumExt As String = String.Empty
    'SOL_DS_EMPNUMINT	VARCHAR(30) NULL,
    Private strEmpInt As String = String.Empty
    'SOL_DS_RPNOMBRE1	VARCHAR(100) NULL,
    Private strRPNombre1 As String = String.Empty
    'SOL_DS_RPPARENTESCO1	VARCHAR(100) NULL,
    Private strRPParentesco1 As String = String.Empty
    'SOL_DS_RPTEL1		VARCHAR(100) NULL,
    Private strRPTelfono1 As String = String.Empty
    'SOL_DS_RPEMAIL1		VARCHAR(100) NULL,
    Private strRPEmail1 As String = String.Empty
    'SOL_DS_RPNOMBRE2	VARCHAR(100) NULL,
    Private strRPNombre2 As String = String.Empty
    'SOL_DS_RPPARENTESCO2	VARCHAR(100) NULL,
    Private strRPParentesco2 As String = String.Empty
    'SOL_DS_RPTEL2		VARCHAR(100) NULL,
    Private strRPTelfono2 As String = String.Empty
    'SOL_DS_RPEMAIL2		VARCHAR(100) NULL,
    Private strRPEmail2 As String = String.Empty
    'SOL_DS_RPNOMBRE3	VARCHAR(100) NULL,
    Private strRPNombre3 As String = String.Empty
    'SOL_DS_RPPARENTESCO3	VARCHAR(100) NULL,
    Private strRPParentesco3 As String = String.Empty
    'SOL_DS_RPTEL3		VARCHAR(100) NULL,
    Private strRPTelfono3 As String = String.Empty
    'SOL_DS_RPEMAIL3		VARCHAR(100) NULL,
    Private strRPEmail3 As String = String.Empty
    'SOL_DS_RBBANCO1		VARCHAR(50) NULL, REFERENCIA BANCARIA
    Private strRBBanco1 As String = String.Empty
    'SOL_DS_RBNUMTARJETA1	VARCHAR(50) NULL, REFERENCIA BANCARIA
    Private strRBTarjeta1 As String = String.Empty
    'SOL_DS_RBBANCO2		VARCHAR(50) NULL, REFERENCIA BANCARIA
    Private strRBBanco2 As String = String.Empty
    'SOL_DS_RBNUMTARJETA2	VARCHAR(50) NULL, REFERENCIA BANCARIA
    Private strRBTarjeta2 As String = String.Empty
    'SOL_DS_CDBANCO		VARCHAR(50) NULL, CARGO DIRECTO
    Private strCDBanco As String = String.Empty
    'SOL_DS_CDCLABE		VARCHAR(50) NULL,
    Private strCDCLABE As String = String.Empty
    'SOL_DS_COANOMBRE1	VARCHAR(100) NULL,  COACREDITADO
    Private strCoaNombre1 As String = String.Empty
    'SOL_DS_COANOMBRE2	VARCHAR(100) NULL,COACREDITADO
    Private strCoaNombre2 As String = String.Empty
    'SOL_DS_COAAPEPATERNO	VARCHAR(100) NULL,COACREDITADO
    Private strCoaApePaterno As String = String.Empty
    'SOL_DS_COAAPEMATERNO	VARCHAR(100) NULL,COACREDITADO
    Private strCoaApeMaterno As String = String.Empty
    'SOL_DS_COARFC		VARCHAR(13) NULL,COACREDITADO
    Private strCoaRFC As String = String.Empty
    'SOL_DS_COALADA	VARCHAR(3) NULL,COACREDITADO
    Private strCoaLada As String = String.Empty
    'SOL_DS_COATELEFONO	VARCHAR(100) NULL,COACREDITADO
    Private strCoaTelefono As String = String.Empty
    'SOL_DS_COATELMOVIL	VARCHAR(100) NULL,COACREDITADO
    Private strCoaTelMovil As String = String.Empty
    'SOL_FE_COANACIMIENTO	DATETIME NULL,COACREDITADO
    Private fecCoaNacimiento As DateTime = "1900-01-01"
    'SOL_DS_COACALLE		VARCHAR(100) NULL,COACREDITADO
    Private strCoaCalle As String = String.Empty
    'SOL_DS_COANUMEXT	VARCHAR(30) NULL,COACREDITADO
    Private strCoaNumExt As String = String.Empty
    'SOL_DS_COANUMINT	VARCHAR(30) NULL,COACREDITADO
    Private strCoaNumInt As String = String.Empty
    'SOL_DS_COAEMAIL		VARCHAR(30) NULL,COACREDITADO
    Private strCoaEmail As String = String.Empty
    'SOL_DS_COACOLONIA	VARCHAR(100) NULL,COACREDITADO
    Private strCoaColonia As String = String.Empty
    'SOL_DS_COACIUDAD	VARCHAR(100) NULL,COACREDITADO
    Private strCoaCiudad As String = String.Empty
    'SOL_DS_COAESTADO	VARCHAR(100) NULL,COACREDITADO
    Private strCoaEstado As String = String.Empty
    'SOL_DS_COACP		VARCHAR(100) NULL,COACREDITADO
    Private strCoaCodPost As String = String.Empty
    'SOL_DS_COACURP		VARCHAR(100) NULL,COACREDITADO
    Private strCoaCURP As String = String.Empty
    'SOL_DS_COAFIRMAELEC	VARCHAR(100) NULL,COACREDITADO
    Private strCoaFirmaElec As String = String.Empty
    'SOL_DS_COAEMPLEOPUESTO	VARCHAR(100) NULL,COACREDITADO
    Private strCoaEmpPuesto As String = String.Empty
    'SOL_DS_COAEMPDEPAREA	VARCHAR(100) NULL,COACREDITADO
    Private strCoaEmpDepArea As String = String.Empty
    'SOL_DS_COAEMPDESEANIO	VARCHAR(100) NULL,COACREDITADO
    Private strCoaEmpDesAnio As String = String.Empty
    'SOL_DS_COAEMPCOMPANIA	VARCHAR(100) NULL,COACREDITADO
    Private strCoaEmpCompania As String = String.Empty
    'SOL_DS_COAEMPLADA	VARCHAR(3) NULL,COACREDITADO
    Private strCoaEmpLada As String = String.Empty
    'SOL_DS_COAEMPEXT	VARCHAR(10) NULL,COACREDITADO
    Private strCoaEmpTelefono As String = String.Empty
    'SOL_DS_COAEMPEXT	VARCHAR(10) NULL,COACREDITADO
    Private strCoaEmpExt As String = String.Empty
    'SOL_NO_COAEMPSUELDO	NUMERIC(13,2) NULL,COACREDITADO
    Private decCoaEmpSueldo As Decimal = 0.0
    'SOL_DS_COAEMPCALLE		VARCHAR(100) NULL,COACREDITADO
    Private strCoaEmpCalle As String = String.Empty
    'SOL_DS_COAEMPNUMEXT	VARCHAR(100) NULL,COACREDITADO
    Private strCoaEmpNumExt As String = String.Empty
    'SOL_DS_COAEMPNUMINT	VARCHAR(100) NULLCOACREDITADO
    Private strCoaEmpNumInt As String = String.Empty
    'CLAVE DE LA PROPUESTA
    Private intCvePropuesta As Integer = 0
    'CLAVE DE COTIZACION
    Private intCveCotizacion As Integer = 0
    'CLAVE DE LA COTIZACION DEL SEGURO
    Private intCveCotSeguro As Integer = 0
    'CLAVE DEl MUNICIPIO O DELEGACION
    Private intDelMun As Integer = 0
    'CLAVE DEl MUNICIPIO O DELEGACION DEL COACREDITADO
    Private intCoaDelMun As Integer = 0

#End Region

#Region "Propiedades"

    ''' <summary>
    ''' CLAVE DE LA TABLA  SOL_FL_CVE
    ''' </summary>
    ''' <value></value>
    Public Property _intClaveSolicitud() As Integer
        Get
            Return intClaveSolicitud
        End Get
        Set(ByVal value As Integer)
            intClaveSolicitud = value
        End Set
    End Property

    ''' <summary>
    '''  NOMBRE DEL CLIENTE SOL_DS_NOMBRE1
    ''' </summary>
    ''' <value></value>
    Public Property _strNombre1() As String
        Get
            Return strNombre1
        End Get
        Set(ByVal value As String)
            strNombre1 = value
        End Set
    End Property

    ''' <summary>
    ''' 'SEGUNDO NOMBRE DEL CLIENTE SOL_DS_NOMBRE2
    ''' </summary>
    ''' <value></value>
    Public Property _strNombre2() As String
        Get
            Return strNombre2
        End Get
        Set(ByVal value As String)
            strNombre2 = value
        End Set
    End Property

    ''' <summary>
    ''' 'APEIDO PATERNO DEL CLIENTE SOL_DS_APEPATERNO 	 
    ''' </summary>
    ''' <value></value>
    Public Property _strApePaterno() As String
        Get
            Return strApePaterno
        End Get
        Set(ByVal value As String)
            strApePaterno = value
        End Set
    End Property

    ''' <summary>
    ''' APEIDO MATERNO DEL CLIENTE SOL_DS_APEMATERNO 	 
    ''' </summary>
    ''' <value></value>
    Public Property _strApeMaterno() As String
        Get
            Return strApeMaterno
        End Get
        Set(ByVal value As String)
            strApeMaterno = value
        End Set
    End Property

    ''' <summary>
    ''' RFC DEL CLIENTE SOL_DS_RFC
    ''' </summary>
    ''' <value></value>
    Public Property _strRFC() As String
        Get
            Return strRFC
        End Get
        Set(ByVal value As String)
            strRFC = value
        End Set
    End Property

    ''' <summary>
    ''' TELEFONO DEL CLIENTE SOL_DS_LADA
    ''' </summary>
    ''' <value></value>
    Public Property _stLada() As String
        Get
            Return stLada
        End Get
        Set(ByVal value As String)
            stLada = value
        End Set
    End Property

    ''' <summary>
    ''' TELEFONO DEL CLIENTE SOL_DS_TELEFONO
    ''' </summary>
    ''' <value></value>
    Public Property _stTelefono() As String
        Get
            Return stTelefono
        End Get
        Set(ByVal value As String)
            stTelefono = value
        End Set
    End Property

    ''' <summary>
    ''' TELEFONO DEL CLIENTE SOL_DS_TELMOVIL	
    ''' </summary>
    ''' <value></value>
    Public Property _strTelMovil() As String
        Get
            Return strTelMovil
        End Get
        Set(ByVal value As String)
            strTelMovil = value
        End Set
    End Property

    ''' <summary>
    ''' CALLLE DEL CLIENT SOL_DS_CALLE		 
    ''' </summary>
    ''' <value></value>
    Public Property _strCalle() As String
        Get
            Return strCalle
        End Get
        Set(ByVal value As String)
            strCalle = value
        End Set
    End Property

    ''' <summary>
    ''' NUMERO EXTERIOR DEL CLIENTE SOL_DS_NUMEXT
    ''' </summary>
    ''' <value></value>
    Public Property _strNumExt() As String
        Get
            Return strNumExt
        End Get
        Set(ByVal value As String)
            strNumExt = value
        End Set
    End Property

    ''' <summary>
    ''' NUMERO INTERIOR DEL CLIENTE SOL_DS_NUMINT
    ''' </summary>
    ''' <value></value>
    Public Property _strNumInt() As String
        Get
            Return strNumInt
        End Get
        Set(ByVal value As String)
            strNumInt = value
        End Set
    End Property

    ''' <summary>
    ''' EMAIL DEL CLIENTE SOL_DS_EMAIL
    ''' </summary>
    ''' <value></value>
    Public Property _strEmail() As String
        Get
            Return strEmail
        End Get
        Set(ByVal value As String)
            strEmail = value
        End Set
    End Property

    ''' <summary>
    ''' COLONIA DEL CLIENTE SOL_DS_COLONIA
    ''' </summary>
    ''' <value></value>
    Public Property _strColonia() As String
        Get
            Return strColonia
        End Get
        Set(ByVal value As String)
            strColonia = value
        End Set
    End Property

    ''' <summary>
    ''' CIUDAD DEL CLIENTE SOL_DS_CIUDAD
    ''' </summary>
    ''' <value></value>
    Public Property _strCiudad() As String
        Get
            Return strCiudad
        End Get
        Set(ByVal value As String)
            strCiudad = value
        End Set
    End Property

    ''' <summary>
    ''' ESTADO DEL CLIENTE SOL_DS_ESTADO
    ''' </summary>
    ''' <value></value>
    Public Property _strEstado() As String
        Get
            Return strEstado
        End Get
        Set(ByVal value As String)
            strEstado = value

        End Set
    End Property

    ''' <summary>
    ''' CODIGO POSTAL DEL CLIENTE SOL_DS_CODPOST
    ''' </summary>
    ''' <value></value>
    Public Property _strCodPost() As String
        Get
            Return strCodPost
        End Get
        Set(ByVal value As String)
            strCodPost = value
        End Set
    End Property

    ''' <summary>
    ''' CUPR DEL CLIENTE SOL_DS_CURP
    ''' </summary>
    ''' <value></value>
    Public Property _strFirmaElec() As String
        Get
            Return strFirmElec
        End Get
        Set(ByVal value As String)
            strFirmElec = value
        End Set
    End Property

    ''' <summary>
    ''' FIRMA ELECTRONICA DEL CLIENTE SOL_DS_FIRMAELEC
    ''' </summary>
    ''' <value></value>
    Public Property _strCURP() As String
        Get
            Return strCURP
        End Get
        Set(ByVal value As String)
            strCURP = value
        End Set
    End Property

    ''' <summary>
    ''' SEXO DEL CLIENTE SOL_DS_SEXO 
    ''' </summary>
    ''' <value></value>
    Public Property _strSexo() As String
        Get
            Return strSexo
        End Get
        Set(ByVal value As String)
            strSexo = value
        End Set
    End Property

    ''' <summary>
    ''' FECHA DE NACIMEINTO DEL CLIENTE SOL_FE_NACIMIENTO
    ''' </summary>
    ''' <value></value>
    Public Property _fecNacimiento() As DateTime
        Get
            Return fecNacimiento
        End Get
        Set(ByVal value As DateTime)
            fecNacimiento = value
        End Set
    End Property

    ''' <summary>
    ''' SEXO DEL CLIENTE SOL_DS_PAISNAC
    ''' </summary>
    ''' <value></value>
    Public Property _strPaisNac() As String
        Get
            Return strPaisNac
        End Get
        Set(ByVal value As String)
            strPaisNac = value
        End Set
    End Property

    ''' <summary>
    ''' SEXO DEL CLIENTE SOL_DS_ESTADONAC
    ''' </summary>
    ''' <value></value>
    Public Property _strEstadoNac() As String
        Get
            Return strEstadoNac
        End Get
        Set(ByVal value As String)
            strEstadoNac = value
        End Set
    End Property

    ''' <summary>
    ''' NACIONALIDAD DEL CLIENTE SOL_DS_NACIONALIDAD
    ''' </summary>
    ''' <value></value>
    Public Property _intNacionalidad() As Integer
        Get
            Return intNacionalidad
        End Get
        Set(ByVal value As Integer)
            intNacionalidad = value
        End Set
    End Property

    ''' <summary>
    ''' DEPENCIAS DEL CLIENTE SOL_DS_DEPENDENCIAS
    ''' </summary>
    ''' <value></value>
    Public Property _strDependencias() As String
        Get
            Return strDependencias
        End Get
        Set(ByVal value As String)
            strDependencias = value
        End Set
    End Property

    ''' <summary>
    ''' VIVE PROPIO O RENTA DEL CLIENTE SOL_DS_VIVEN
    ''' </summary>
    ''' <value></value>
    Public Property _strViven() As String
        Get
            Return strViven
        End Get
        Set(ByVal value As String)
            strViven = value
        End Set
    End Property

    ''' <summary>
    ''' SI TIENE PROPIEDAD EL CLIENTE  SOL_FG_PROPIEDAD
    ''' </summary>
    ''' <value></value>
    Public Property _intPropiedad() As Integer
        Get
            Return intPropiedad
        End Get
        Set(ByVal value As Integer)
            intPropiedad = value
        End Set
    End Property

    ''' <summary>
    ''' AÑOS DE RESIENCIA EN EL DOMICILIO DEL CLIENTE SOL_NO_ANIOSRECDOM
    ''' </summary>
    ''' <value></value>
    Public Property _intAnioRecDomicilio() As Integer
        Get
            Return intAnioRecDomicilio
        End Get
        Set(ByVal value As Integer)
            intAnioRecDomicilio = value
        End Set
    End Property

    ''' <summary>
    ''' AÑOS DE RESIDENCIA EN LA CIUDAD CLIENTE SOL_NO_ANIOSRESCIUDAD
    ''' </summary>
    ''' <value></value>
    Public Property _intAnioRecCiudad() As Integer
        Get
            Return intAnioRecCiudad
        End Get
        Set(ByVal value As Integer)
            intAnioRecCiudad = value
        End Set
    End Property

    ''' <summary>
    ''' NOMBRE DEL PRIMER BENEFICIARIO DEL CLIENTE SOL_DS_BENEFICIARIO1
    ''' </summary>
    ''' <value></value>
    Public Property _strBeneficiario1() As String
        Get
            Return strBeneficiario1
        End Get
        Set(ByVal value As String)
            strBeneficiario1 = value
        End Set
    End Property

    ''' <summary>
    ''' NOMBRE DEL SEGUNDO BENEFICIARIO DEL CLIENTE SOL_DS_BENEFICIARIO2
    ''' </summary>
    ''' <value></value>
    Public Property _strBeneficiario2() As String
        Get
            Return strBeneficiario2
        End Get
        Set(ByVal value As String)
            strBeneficiario2 = value
        End Set
    End Property

    ''' <summary>
    ''' ESTADO CIVIL DEL CLIENTE SOL_FG_EDOCIVIL 1=CASADO 2=SOLTERO
    ''' </summary>
    ''' <value></value>
    Public Property _intEdoCivil() As Integer
        Get
            Return intEdoCivil
        End Get
        Set(ByVal value As Integer)
            intEdoCivil = value
        End Set
    End Property

    ''' <summary>
    ''' TIPO DE FORMA DE BIENES SOL_FG_BIENES 1.-MANCOMUNADOS , 2=SEPARADOS
    ''' </summary>
    ''' <value></value>
    Public Property _intBienes() As Integer
        Get
            Return intBienes
        End Get
        Set(ByVal value As Integer)
            intBienes = value
        End Set
    End Property

    ''' <summary>
    ''' TIENE AUTOMIVIL PROPIO EL CLIENTE SOL_FG_AUTOPROPIO	  1=SI, 2=NO
    ''' </summary>
    ''' <value></value>
    Public Property _intAutoPropio() As Integer
        Get
            Return intAutoPropio
        End Get
        Set(ByVal value As Integer)
            intAutoPropio = value
        End Set
    End Property

    ''' <summary>
    ''' MARCA MODELO Y TIPO DEL VEHICULO SOL_DS_MARMODTIPO	MARCA MODELO TIPO
    ''' </summary>
    ''' <value></value>
    Public Property _strMarModTipo() As String
        Get
            Return strMarModTipo
        End Get
        Set(ByVal value As String)
            strMarModTipo = value
        End Set
    End Property

    ''' <summary>
    ''' PUESTO QUE OCUPA EL CLIENTE EN SU EMPRESA
    ''' </summary>
    ''' <value></value>
    Public Property _strEmpPuesto() As String
        Get
            Return strEmpPuesto
        End Get
        Set(ByVal value As String)
            strEmpPuesto = value
        End Set
    End Property

    ''' <summary>
    ''' Departamento o area que ocupa el cliente dentro de su empresa SOL_DS_EMPDEPARAREA EMPLEO DEPARTAMENTO AREA
    ''' </summary>
    ''' <value></value>
    Public Property _strEmpDepArea() As String
        Get
            Return strEmpDepArea
        End Get
        Set(ByVal value As String)
            strEmpDepArea = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha en que ingreso al empleo el cliente SOL_FE_EMPDESDEANIO	DATETIME 
    ''' </summary>
    ''' <value></value>
    Public Property _fecEmpDesdeAnio() As Integer
        Get
            Return fecEmpDesdeAnio
        End Get
        Set(ByVal value As Integer)
            fecEmpDesdeAnio = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre de la compañia donde trabaja el cliente SOL_DS_COMPAÑIA
    ''' </summary>
    ''' <value></value>
    Public Property _strCompania() As String
        Get
            Return strCompania
        End Get
        Set(ByVal value As String)
            strCompania = value
        End Set
    End Property

    ''' <summary>
    ''' Lada de la empresa donde labora el cliente SOL_DS_EMPLADA
    ''' </summary>
    ''' <value></value>
    Public Property _strEmpLada() As String
        Get
            Return strEmpLada
        End Get
        Set(ByVal value As String)
            strEmpLada = value
        End Set
    End Property

    ''' <summary>
    ''' Numero de telefono de la empresa donde labora el cliente SOL_DS_EMPTELEFONO
    ''' </summary>
    ''' <value></value>
    Public Property _strEmpTelefono() As String
        Get
            Return strEmpTelefono
        End Get
        Set(ByVal value As String)
            strEmpTelefono = value
        End Set
    End Property

    ''' <summary>
    ''' Extencion del trabajo del cliente SOL_DS_EXT EXTENCION DEL TELEFONO DEL EMPLEO
    ''' </summary>
    ''' <value></value>
    Public Property _strEmpExt() As String
        Get
            Return strEmpExt
        End Get
        Set(ByVal value As String)
            strEmpExt = value
        End Set
    End Property

    ''' <summary>
    ''' Sueldo que percibe el cliente SOL_NO_SUELDO
    ''' </summary>
    ''' <value></value>
    Public Property _strEmpSueldo() As Decimal
        Get
            Return strEmpSueldo
        End Get
        Set(ByVal value As Decimal)
            strEmpSueldo = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre de la calle donde se encuentra ubicado el trabajo del cliente SOL_DS_EMPCALLE
    ''' </summary>
    ''' <value></value>
    Public Property _strEmpCalle() As String
        Get
            Return strEmpCalle
        End Get
        Set(ByVal value As String)
            strEmpCalle = value
        End Set
    End Property

    ''' <summary>
    ''' Numero exterior del trabajo del cliente SOL_DS_EMPNUMEXT
    ''' </summary>
    ''' <value></value>
    Public Property _strEmpNumExt() As String
        Get
            Return strEmpNumExt
        End Get
        Set(ByVal value As String)
            strEmpNumExt = value
        End Set
    End Property

    ''' <summary>
    ''' Numero interior del trabajo del cliente SOL_DS_EMPNUMINT
    ''' </summary>
    ''' <value></value>
    Public Property _strEmpInt() As String
        Get
            Return strEmpInt
        End Get
        Set(ByVal value As String)
            strEmpInt = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre de la primera referencia personal del cliente SOL_DS_RPNOMBRE1
    ''' </summary>
    ''' <value></value>
    Public Property _strRPNombre1() As String
        Get
            Return strRPNombre1
        End Get
        Set(ByVal value As String)
            strRPNombre1 = value
        End Set
    End Property

    ''' <summary>
    ''' parentesco de la referencia personal del cliente SOL_DS_RPPARENTESCO1
    ''' </summary>
    ''' <value></value>
    Public Property _strRPParentesco1() As String
        Get
            Return strRPParentesco1
        End Get
        Set(ByVal value As String)
            strRPParentesco1 = value
        End Set
    End Property

    ''' <summary>
    ''' Telefono de la referencia personal del cliente SOL_DS_RPTEL1
    ''' </summary>
    ''' <value></value>
    Public Property _strRPTelfono1() As String
        Get
            Return strRPTelfono1
        End Get
        Set(ByVal value As String)
            strRPTelfono1 = value
        End Set
    End Property

    ''' <summary>
    ''' Email de la referencia personal del cliente SOL_DS_RPEMAIL1
    ''' </summary>
    ''' <value></value>
    Public Property _strRPEmail1() As String
        Get
            Return strRPEmail1
        End Get
        Set(ByVal value As String)
            strRPEmail1 = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre de la segunda referencia personal del cliente SOL_DS_RPNOMBRE2
    ''' </summary>
    ''' <value></value>
    Public Property _strRPNombre2() As String
        Get
            Return strRPNombre2
        End Get
        Set(ByVal value As String)
            strRPNombre2 = value
        End Set
    End Property

    ''' <summary>
    ''' Parentesco de la segunda referencia personal del cliente SOL_DS_RPPARENTESCO2
    ''' </summary>
    ''' <value></value>
    Public Property _strRPParentesco2() As String
        Get
            Return strRPParentesco2
        End Get
        Set(ByVal value As String)
            strRPParentesco2 = value
        End Set
    End Property

    ''' <summary>
    ''' Telefono de la segunda referencia personal del cliente SOL_DS_RPTEL2
    ''' </summary>
    ''' <value></value>
    Public Property _strRPTelfono2() As String
        Get
            Return strRPTelfono2
        End Get
        Set(ByVal value As String)
            strRPTelfono2 = value
        End Set
    End Property

    ''' <summary>
    ''' Email de la segunda referencia personal del cliente SOL_DS_RPEMAIL2
    ''' </summary>
    ''' <value></value>
    Public Property _strRPEmail2() As String
        Get
            Return strRPEmail2
        End Get
        Set(ByVal value As String)
            strRPEmail2 = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre de la tercera referencia personal del cliente SOL_DS_RPNOMBRE3
    ''' </summary>
    ''' <value></value>
    Public Property _strRPNombre3() As String
        Get
            Return strRPNombre3
        End Get
        Set(ByVal value As String)
            strRPNombre3 = value
        End Set
    End Property

    ''' <summary>
    ''' Parentesco de la tercera referencia personal del cliente SOL_DS_RPPARENTESCO3
    ''' </summary>
    ''' <value></value>
    Public Property _strRPParentesco3() As String
        Get
            Return strRPParentesco3
        End Get
        Set(ByVal value As String)
            strRPParentesco3 = value
        End Set
    End Property

    ''' <summary>
    ''' Telefono de la tercera referencia personal del cliente SOL_DS_RPTEL3
    ''' </summary>
    ''' <value></value>
    Public Property _strRPTelfono3() As String
        Get
            Return strRPTelfono3
        End Get
        Set(ByVal value As String)
            strRPTelfono3 = value
        End Set
    End Property

    ''' <summary>
    ''' Email de la tercera referencia personal del cliente SOL_DS_RPEMAIL3
    ''' </summary>
    ''' <value></value>
    Public Property _strRPEmail3() As String
        Get
            Return strRPEmail3
        End Get
        Set(ByVal value As String)
            strRPEmail3 = value
        End Set
    End Property

    ''' <summary>
    ''' Primera referencia bancaria del cliente SOL_DS_RBBANCO1	REFERENCIA BANCARIA
    ''' </summary>
    ''' <value></value>
    Public Property _strRBBanco1() As String
        Get
            Return strRBBanco1
        End Get
        Set(ByVal value As String)
            strRBBanco1 = value
        End Set
    End Property

    ''' <summary>
    ''' Primer numero de tarjeta de la referencia bancaria 1 del cliente SOL_DS_RBNUMTARJETA1 REFERENCIA BANCARIA
    ''' </summary>
    ''' <value></value>
    Public Property _strRBTarjeta1() As String
        Get
            Return strRBTarjeta1
        End Get
        Set(ByVal value As String)
            strRBTarjeta1 = value
        End Set
    End Property

    ''' <summary>
    ''' Segunda referencia bancaria del cliente SOL_DS_RBBANCO2 REFERENCIA BANCARIA
    ''' </summary>
    ''' <value></value>
    Public Property _strRBBanco2() As String
        Get
            Return strRBBanco2
        End Get
        Set(ByVal value As String)
            strRBBanco2 = value
        End Set
    End Property

    ''' <summary>
    ''' Segunda tarjeta de referencia bancaria2 del cliente SOL_DS_RBNUMTARJETA REFERENCIA BANCARIA
    ''' </summary>
    ''' <value></value>
    Public Property _strRBTarjeta2() As String
        Get
            Return strRBTarjeta2
        End Get
        Set(ByVal value As String)
            strRBTarjeta2 = value
        End Set
    End Property

    ''' <summary>
    ''' Banco para el cargo directo del cliente SOL_DS_CDBANCO CARGO DIRECTO
    ''' </summary>
    ''' <value></value>
    Public Property _strCDBanco() As String
        Get
            Return strCDBanco
        End Get
        Set(ByVal value As String)
            strCDBanco = value
        End Set
    End Property

    ''' <summary>
    ''' Cuenta CLABE del cargo directo del cliente SOL_DS_CDCLABE
    ''' </summary>
    ''' <value></value>
    Public Property _strCDCLABE() As String
        Get
            Return strCDCLABE
        End Get
        Set(ByVal value As String)
            strCDCLABE = value
        End Set
    End Property

    ''' <summary>
    ''' 1 Nombre del coacreditado del cliente SOL_DS_COANOMBRE1 COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaNombre1() As String
        Get
            Return strCoaNombre1
        End Get
        Set(ByVal value As String)
            strCoaNombre1 = value
        End Set
    End Property

    ''' <summary>
    ''' Segundo nombre del coacreditado del cliente SOL_DS_COANOMBRE2	 COACREDITADO 
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaNombre2() As String
        Get
            Return strCoaNombre2
        End Get
        Set(ByVal value As String)
            strCoaNombre2 = value
        End Set
    End Property

    ''' <summary>
    ''' Apeido paterno del coacreditado SOL_DS_COAAPEPATERNO	 COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaApePaterno() As String
        Get
            Return strCoaApePaterno
        End Get
        Set(ByVal value As String)
            strCoaApePaterno = value
        End Set
    End Property

    ''' <summary>
    ''' Apeido materno del coacreditado SOL_DS_COAAPEMATERNO COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaApeMaterno() As String
        Get
            Return strCoaApeMaterno
        End Get
        Set(ByVal value As String)
            strCoaApeMaterno = value
        End Set
    End Property

    ''' <summary>
    ''' RFC del coacreditado SOL_DS_COARFC COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaRFC() As String
        Get
            Return strCoaRFC
        End Get
        Set(ByVal value As String)
            strCoaRFC = value
        End Set
    End Property

    ''' <summary>
    ''' Telefono del coacreditado SOL_DS_COALADA	 COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaLada() As String
        Get
            Return strCoaLada
        End Get
        Set(ByVal value As String)
            strCoaLada = value
        End Set
    End Property

    ''' <summary>
    ''' Telefono del coacreditado SOL_DS_COATELFONO	 COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaTelefono() As String
        Get
            Return strCoaTelefono
        End Get
        Set(ByVal value As String)
            strCoaTelefono = value
        End Set
    End Property

    ''' <summary>
    ''' Telefono movil del coacreditado SOL_DS_COATELMOVIL	 COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaTelMovil() As String
        Get
            Return strCoaTelMovil
        End Get
        Set(ByVal value As String)
            strCoaTelMovil = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha de nacimiento del coacreditado SOL_FE_COANACIMIENTO	DATETIME COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _fecCoaNacimiento() As DateTime
        Get
            Return fecCoaNacimiento
        End Get
        Set(ByVal value As DateTime)
            fecCoaNacimiento = value
        End Set
    End Property

    ''' <summary>
    ''' Calle donde vive el coacreditado SOL_DS_COACALLE		 COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaCalle() As String
        Get
            Return strCoaCalle
        End Get
        Set(ByVal value As String)
            strCoaCalle = value
        End Set
    End Property

    ''' <summary>
    ''' Numero exterior del domicilio del coacreditado
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaNumExt() As String
        Get
            Return strCoaNumExt
        End Get
        Set(ByVal value As String)
            strCoaNumExt = value
        End Set
    End Property

    ''' <summary>
    ''' Numero interior del coacreditado SOL_DS_COANUMINT	 COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaNumInt() As String
        Get
            Return strCoaNumInt
        End Get
        Set(ByVal value As String)
            strCoaNumInt = value
        End Set
    End Property

    ''' <summary>
    ''' Email del coacreditado SOL_DS_COAEMAIL	COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaEmail() As String
        Get
            Return strCoaEmail
        End Get
        Set(ByVal value As String)
            strCoaEmail = value
        End Set
    End Property

    ''' <summary>
    ''' Colonia del coacreditado SOL_DS_COACOLONIA	 COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaColonia() As String
        Get
            Return strCoaColonia
        End Get
        Set(ByVal value As String)
            strCoaColonia = value
        End Set
    End Property

    ''' <summary>
    ''' Ciudad del coacreditado SOL_DS_COACIUDAD	 COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaCiudad() As String
        Get
            Return strCoaCiudad
        End Get
        Set(ByVal value As String)
            strCoaCiudad = value
        End Set
    End Property

    ''' <summary>
    ''' Estado del coacreditado SOL_DS_COAESTADO	 COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaEstado() As String
        Get
            Return strCoaEstado
        End Get
        Set(ByVal value As String)
            strCoaEstado = value
        End Set
    End Property

    ''' <summary>
    ''' Codigo postal del coacreditado SOL_DS_COACP		 COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaCodPost() As String
        Get
            Return strCoaCodPost
        End Get
        Set(ByVal value As String)
            strCoaCodPost = value
        End Set
    End Property

    ''' <summary>
    ''' CURP del coacreditado SOL_DS_COACURP		 COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaCURP() As String
        Get
            Return strCoaCURP
        End Get
        Set(ByVal value As String)
            strCoaCURP = value
        End Set
    End Property

    ''' <summary>
    ''' Firma Electronica del coacreditado SOL_DS_COAFIRMAELEC		 COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaFirmaElec() As String
        Get
            Return strCoaFirmaElec
        End Get
        Set(ByVal value As String)
            strCoaFirmaElec = value
        End Set
    End Property

    ''' <summary>
    ''' Empresa del coacreditado SOL_DS_COAEMPLEOPUESTO	 COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaEmpPuesto() As String
        Get
            Return strCoaEmpPuesto
        End Get
        Set(ByVal value As String)
            strCoaEmpPuesto = value
        End Set
    End Property

    ''' <summary>
    ''' Departamento o area del coacreditado SOL_DS_COAEMPDEPAREA	 COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaEmpDepArea() As String
        Get
            Return strCoaEmpDepArea
        End Get
        Set(ByVal value As String)
            strCoaEmpDepArea = value
        End Set
    End Property

    ''' <summary>
    ''' Año que ingreso a la empresa del coacreditado SOL_DS_COAEMPDESEANIO	 COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaEmpDesAnio() As String
        Get
            Return strCoaEmpDesAnio
        End Get
        Set(ByVal value As String)
            strCoaEmpDesAnio = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre de la compañia del coacreditado SOL_DS_COAEMPCOMPANIA	 COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaEmpCompania() As String
        Get
            Return strCoaEmpCompania
        End Get
        Set(ByVal value As String)
            strCoaEmpCompania = value
        End Set
    End Property

    ''' <summary>
    ''' Lada de la empresa del coacreditado SOL_DS_COAEMPLADA COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaEmpLada() As String
        Get
            Return strCoaEmpLada
        End Get
        Set(ByVal value As String)
            strCoaEmpLada = value
        End Set
    End Property

    ''' <summary>
    ''' Telefono de la empresa del coacreditado SOL_DS_COAEMPTEL COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaEmpTelefono() As String
        Get
            Return strCoaEmpTelefono
        End Get
        Set(ByVal value As String)
            strCoaEmpTelefono = value
        End Set
    End Property

    ''' <summary>
    ''' Extencion de la empresa del coacreditado SOL_DS_COAEMPEXT	VARCHAR(10) COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaEmpExt() As String
        Get
            Return strCoaEmpExt
        End Get
        Set(ByVal value As String)
            strCoaEmpExt = value
        End Set
    End Property

    ''' <summary>
    ''' Sueldo del coacreditado SOL_NO_COAEMPSUELDO	NUMERIC(13,2) COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _decCoaEmpSueldo() As Decimal
        Get
            Return decCoaEmpSueldo
        End Get
        Set(ByVal value As Decimal)
            decCoaEmpSueldo = value
        End Set
    End Property

    ''' <summary>
    ''' Calle de la empresa del coacreditado SOL_DS_COAEMPCALLE		 COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaEmpCalle() As String
        Get
            Return strCoaEmpCalle
        End Get
        Set(ByVal value As String)
            strCoaEmpCalle = value
        End Set
    End Property

    ''' <summary>
    ''' Numero exteriror de la empresa del coacreditado
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaEmpNumExt() As String
        Get
            Return strCoaEmpNumExt
        End Get
        Set(ByVal value As String)
            strCoaEmpNumExt = value
        End Set
    End Property

    ''' <summary>
    ''' Numero interior de la empresa del coacreditado SOL_DS_COAEMPNUMINT	 COACREDITADO
    ''' </summary>
    ''' <value></value>
    Public Property _strCoaEmpNumInt() As String
        Get
            Return strCoaEmpNumInt
        End Get
        Set(ByVal value As String)
            strCoaEmpNumInt = value
        End Set
    End Property

    'Clave de la propuesta para guardar la relacion de la cotizacion con la solicitud
    Public Property _intCvePropuesta() As Integer
        Get
            Return intCvePropuesta
        End Get
        Set(ByVal value As Integer)
            intCvePropuesta = value
        End Set
    End Property

    'Clave de la cotizacion para guardar la relacion de la cotizacion con la solicitud
    Public Property _intCveCotizacion() As Integer
        Get
            Return intCveCotizacion
        End Get
        Set(ByVal value As Integer)
            intCveCotizacion = value
        End Set
    End Property

    'Clave de la cotizacion del seguro para guardar la relacion de la cotizacion con la solicitud
    Public Property _intCveCotSeguro() As Integer
        Get
            Return intCveCotSeguro
        End Get
        Set(ByVal value As Integer)
            intCveCotSeguro = value
        End Set
    End Property
    'Clave de la delegacion o municipio para guardar la relacion de la cotizacion con la solicitud
    Public Property _intDelMun() As Integer
        Get
            Return intDelMun
        End Get
        Set(ByVal value As Integer)
            intDelMun = value
        End Set
    End Property
    'Clave de la delegacion o municipio coacreditado para guardar la relacion de la cotizacion con la solicitud
    Public Property _intCoaDelMun() As Integer
        Get
            Return intCoaDelMun
        End Get
        Set(ByVal value As Integer)
            intCoaDelMun = value
        End Set
    End Property


#End Region

#Region "Metodos"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Sub New(Optional ByVal CveSolicitud As Integer = 0, Optional ByVal intOper As Integer = 0)
        If CveSolicitud > 0 Then
            Me.intClaveSolicitud = CveSolicitud
            Me.ManejaSolicitud(intOper)
        End If

    End Sub

    ''' <summary>
    ''' Guarda y Obtiene la solicitud
    ''' </summary>
    ''' <param name="intOper"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ManejaSolicitud(ByVal intOper As Integer) As DataSet
        Dim strErrCotiza As String = ""
        ManejaSolicitud = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion
            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "intClaveSolicitud", intClaveSolicitud)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strNombre1", strNombre1)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strNombre2", strNombre2)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strApePaterno", strApePaterno)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strApeMaterno", strApeMaterno)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strRFC", strRFC)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strLada", stLada)
            ArmaParametros(strParamStored, TipoDato.Cadena, "stTelefono", stTelefono)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strTelMovil", strTelMovil)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCalle", strCalle)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strNumExt", strNumExt)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strNumInt", strNumInt)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strEmail", strEmail)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strColonia", strColonia)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCiudad", strCiudad)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strEstado", strEstado)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCodPost", strCodPost)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCURP", strCURP)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strSexo", strSexo)
            ArmaParametros(strParamStored, TipoDato.Cadena, "fecNacimiento", fecNacimiento.ToString("yyyy-MM-dd"))
            ArmaParametros(strParamStored, TipoDato.Cadena, "intNacionalidad", intNacionalidad)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strDependencia", strDependencias)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strViven", strViven)
            ArmaParametros(strParamStored, TipoDato.Entero, "intPropiedad", intPropiedad.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "intAnioRecDomicilio", intAnioRecDomicilio.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "intAnioRecCiudad", intAnioRecCiudad.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strBeneficiario1", strBeneficiario1)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strBeneficiario2", strBeneficiario2)
            ArmaParametros(strParamStored, TipoDato.Entero, "intEdoCivil", intEdoCivil.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "intBienes", intBienes.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "intAutoPropio", intAutoPropio.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strMarModTipo", strMarModTipo)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strEmpPuesto", strEmpPuesto)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strEmpDepArea", strEmpDepArea)
            ArmaParametros(strParamStored, TipoDato.Entero, "fecEmpDesdeAnio", fecEmpDesdeAnio)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCompania", strCompania)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strEmpLada", strEmpLada)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strEmpTelefono", strEmpTelefono)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strEmpExt", strEmpExt)
            ArmaParametros(strParamStored, TipoDato.Doble, "strEmpSueldo", strEmpSueldo.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strEmpCalle", strEmpCalle)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strEmpNumExt", strEmpNumExt)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strEmpInt", strEmpInt)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strRPNombre1", strRPNombre1)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strRPParentesco1", strRPParentesco1)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strRPTelfono1", strRPTelfono1)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strRPEmail1", strRPEmail1)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strRPNombre2", strRPNombre2)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strRPParentesco2", strRPParentesco2.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strRPTelfono2", strRPTelfono2.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strRPEmail2", strRPEmail2.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strRPNombre3", strRPNombre3.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strRPParentesco3", strRPParentesco3.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strRPTelfono3", strRPTelfono3.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strRPEmail3", strRPEmail3.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strRBBanco1", strRBBanco1.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strRBTarjeta1", strRBTarjeta1.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strRBBanco2", strRBBanco2.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strRBTarjeta2", strRBTarjeta2.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCDBanco", strCDBanco.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCDCLABE", strCDCLABE.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaNombre1", strCoaNombre1.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaNombre2", strCoaNombre2.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaApePaterno", strCoaApePaterno.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaApeMaterno", strCoaApeMaterno.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaRFC", strCoaRFC.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaLada", strCoaLada.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaTelefono", strCoaTelefono.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaTelMovil", strCoaTelMovil.ToString)
            ArmaParametros(strParamStored, TipoDato.Fecha, "fecCoaNacimiento", fecCoaNacimiento.ToString("yyyy-MM-dd"))
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaCalle", strCoaCalle.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaNumExt", strCoaNumExt.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaNumInt", strCoaNumInt.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaEmail", strCoaEmail.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaColonia", strCoaColonia.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaCiudad", strCoaCiudad.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaEstado", strCoaEstado.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaCodPost", strCoaCodPost)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaCURP", strCoaCURP)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaEmpPuesto", strCoaEmpPuesto)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaEmpDepArea", strCoaEmpDepArea)
            ArmaParametros(strParamStored, TipoDato.Entero, "strCoaEmpDesAnio", strCoaEmpDesAnio)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaEmpCompania", strCoaEmpCompania)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaEmpLada", strCoaEmpLada)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaEmpTelefono", strCoaEmpTelefono)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaEmpExt", strCoaEmpExt)
            ArmaParametros(strParamStored, TipoDato.Doble, "strCoaEmpSueldo", decCoaEmpSueldo)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaEmpCalle", strCoaEmpCalle)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaEmpNumExt", strCoaEmpNumExt.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "strCoaEmpNumInt", strCoaEmpNumInt.ToString)
            'PARAMETROS DE RELACION DE LA COTIZACION Y LA SOLICITUD
            ArmaParametros(strParamStored, TipoDato.Entero, "intCvePropuesta", intCvePropuesta.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "intCveCotizacion", intCveCotizacion.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "intCveCotSeguro", intCveCotSeguro.ToString)
            'PARAMETRO DEL / MUN DEL SOLICITANTE Y COACREDITADO
            ArmaParametros(strParamStored, TipoDato.Entero, "intDelMun", intDelMun.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "intCoaDelMun", intCoaDelMun.ToString)

            ManejaSolicitud = objSD.EjecutaStoredProcedure("spManejaSolicitud", strErrCotiza, strParamStored)

            If strErrCotiza.Trim.Length > 0 Then
                Throw New Exception(strErrCotiza)
            End If

            'If ManejaSolicitud.Tables(0).Rows(0).Item("@ERROR").ToString.Length > 0 Then
            '    Throw New Exception(ManejaSolicitud.Tables(0).Rows(1).Item("@ERROR").ToString)
            'End If

            If intOper = 1 Then
                If ManejaSolicitud Is Nothing OrElse ManejaSolicitud.Tables.Count = 0 OrElse ManejaSolicitud.Tables(0).Rows.Count = 0 Then
                    Return Nothing
                Else
                    With ManejaSolicitud.Tables(0).Rows(0)
                        intClaveSolicitud = .Item("SOL_FL_CVE").ToString
                        strNombre1 = .Item("SOL_DS_NOMBRE1").ToString
                        strNombre2 = .Item("SOL_DS_NOMBRE2").ToString
                        strApePaterno = .Item("SOL_DS_APEPATERNO").ToString
                        strApeMaterno = .Item("SOL_DS_APEMATERNO").ToString
                        strRFC = .Item("SOL_DS_RFC").ToString
                        stLada = .Item("SOL_DS_LADA").ToString
                        stTelefono = .Item("SOL_DS_TELEFONO").ToString
                        strTelMovil = .Item("SOL_DS_TELMOVIL").ToString
                        strCalle = .Item("SOL_DS_CALLE").ToString
                        strNumExt = .Item("SOL_DS_NUMEXT").ToString
                        strNumInt = .Item("SOL_DS_NUMINT").ToString
                        strEmail = .Item("SOL_DS_EMAIL").ToString
                        strColonia = .Item("SOL_DS_COLONIA").ToString
                        strCiudad = .Item("SOL_DS_CIUDAD").ToString
                        strEstado = .Item("SOL_DS_ESTADO").ToString
                        strCodPost = .Item("SOL_DS_CODPOST").ToString
                        strCURP = .Item("SOL_DS_CURP").ToString
                        strFirmElec = .Item("SOL_DS_FIRMAELEC").ToString
                        strSexo = .Item("SOL_DS_SEXO").ToString
                        fecNacimiento = .Item("SOL_FE_NACIMIENTO").ToString
                        strPaisNac = .Item("SOL_DS_PAISNAC").ToString
                        strEstadoNac = .Item("SOL_DS_ESTADONAC").ToString
                        intNacionalidad = .Item("SOL_DS_NACIONALIDAD").ToString
                        strDependencias = .Item("SOL_DS_DEPENDENCIAS").ToString
                        strViven = .Item("SOL_DS_VIVEN").ToString
                        intPropiedad = .Item("SOL_FG_PROPIEDAD").ToString
                        intAnioRecDomicilio = .Item("SOL_NO_ANIOSRECDOM").ToString
                        intAnioRecCiudad = .Item("SOL_NO_ANIOSRECCIUDAD").ToString
                        strBeneficiario1 = .Item("SOL_DS_BENEFICIARIO1").ToString
                        strBeneficiario2 = .Item("SOL_DS_BENEFICIARIO2").ToString
                        intEdoCivil = .Item("SOL_FG_EDOCIVIL").ToString
                        intBienes = .Item("SOL_FG_BIENES").ToString
                        intAutoPropio = .Item("SOL_FG_AUTOPROPIO").ToString
                        strMarModTipo = .Item("SOL_DS_MARMODTIPO").ToString
                        strEmpPuesto = .Item("SOL_DS_EMPPUESTO").ToString
                        strEmpDepArea = .Item("SOL_DS_EMPDEPARAREA").ToString
                        fecEmpDesdeAnio = .Item("SOL_FE_EMPDESDEANIO").ToString
                        strCompania = .Item("SOL_DS_COMPAÑIA").ToString
                        strEmpLada = .Item("SOL_DS_EMPLADA").ToString
                        strEmpTelefono = .Item("SOL_DS_EMPTELEFONO").ToString
                        strEmpExt = .Item("SOL_DS_EXT").ToString
                        strEmpSueldo = .Item("SOL_NO_SUELDO").ToString
                        strEmpCalle = .Item("SOL_DS_EMPCALLE").ToString
                        strEmpNumExt = .Item("SOL_DS_EMPNUMEXT").ToString
                        strEmpInt = .Item("SOL_DS_EMPNUMINT").ToString
                        strRPNombre1 = .Item("SOL_DS_RPNOMBRE1").ToString
                        strRPParentesco1 = .Item("SOL_DS_RPPARENTESCO1").ToString
                        strRPTelfono1 = .Item("SOL_DS_RPTEL1").ToString
                        strRPEmail1 = .Item("SOL_DS_RPEMAIL1").ToString
                        strRPNombre2 = .Item("SOL_DS_RPNOMBRE2").ToString
                        strRPParentesco2 = .Item("SOL_DS_RPPARENTESCO2").ToString
                        strRPTelfono2 = .Item("SOL_DS_RPTEL2").ToString
                        strRPEmail2 = .Item("SOL_DS_RPEMAIL2").ToString
                        strRPNombre3 = .Item("SOL_DS_RPNOMBRE3").ToString
                        strRPParentesco3 = .Item("SOL_DS_RPPARENTESCO3").ToString
                        strRPTelfono3 = .Item("SOL_DS_RPTEL3").ToString
                        strRPEmail3 = .Item("SOL_DS_RPEMAIL3").ToString
                        strRBBanco1 = .Item("SOL_DS_RBBANCO1").ToString
                        strRBTarjeta1 = .Item("SOL_DS_RBNUMTARJETA1").ToString
                        strRBBanco2 = .Item("SOL_DS_RBBANCO2").ToString
                        strRBTarjeta2 = .Item("SOL_DS_RBNUMTARJETA2").ToString
                        strCDBanco = .Item("SOL_DS_CDBANCO").ToString
                        strCDCLABE = .Item("SOL_DS_CDCLABE").ToString
                        strCoaNombre1 = .Item("SOL_DS_COANOMBRE1").ToString
                        strCoaNombre2 = .Item("SOL_DS_COANOMBRE2").ToString
                        strCoaApePaterno = .Item("SOL_DS_COAAPEPATERNO").ToString
                        strCoaApeMaterno = .Item("SOL_DS_COAAPEMATERNO").ToString
                        strCoaRFC = .Item("SOL_DS_COARFC").ToString
                        strCoaLada = .Item("SOL_DS_COALADA").ToString
                        strCoaTelefono = .Item("SOL_DS_COATELFONO").ToString
                        strCoaTelMovil = .Item("SOL_DS_COATELMOVIL").ToString
                        fecCoaNacimiento = .Item("SOL_FE_COANACIMIENTO").ToString
                        strCoaCalle = .Item("SOL_DS_COACALLE").ToString
                        strCoaNumExt = .Item("SOL_DS_COANUMEXT").ToString
                        strCoaNumInt = .Item("SOL_DS_COANUMINT").ToString
                        strCoaEmail = .Item("SOL_DS_COAEMAIL").ToString
                        strCoaColonia = .Item("SOL_DS_COACOLONIA").ToString
                        strCoaCiudad = .Item("SOL_DS_COACIUDAD").ToString
                        strCoaEstado = .Item("SOL_DS_COAESTADO").ToString
                        strCoaCodPost = .Item("SOL_DS_COACP").ToString
                        strCoaCURP = .Item("SOL_DS_COACURP").ToString
                        strCoaFirmaElec = .Item("SOL_DS_COAFIRMAELEC").ToString
                        strCoaEmpPuesto = .Item("SOL_DS_COAEMPLEOPUESTO").ToString
                        strCoaEmpDepArea = .Item("SOL_DS_COAEMPDEPAREA").ToString
                        strCoaEmpDesAnio = .Item("SOL_DS_COAEMPDESEANIO").ToString
                        strCoaEmpCompania = .Item("SOL_DS_COAEMPCOMPANIA").ToString
                        strCoaEmpLada = .Item("SOL_DS_COAEMPLADA").ToString
                        strCoaEmpTelefono = .Item("SOL_DS_COAEMPTEL").ToString
                        strCoaEmpExt = .Item("SOL_DS_COAEMPEXT").ToString
                        decCoaEmpSueldo = CDec(.Item("SOL_NO_COAEMPSUELDO"))
                        strCoaEmpCalle = .Item("SOL_DS_COAEMPCALLE").ToString
                        strCoaEmpNumExt = .Item("SOL_DS_COAEMPNUMEXT").ToString
                        strCoaEmpNumInt = .Item("SOL_DS_COAEMPNUMINT").ToString
                        ' datos delegacion municipio
                        intDelMun = .Item("SOL_DS_DELMUN").ToString
                        intCoaDelMun = .Item("SOL_DS_COADELMUN").ToString

                    End With
                End If
            End If


            If strErrCotiza.Trim.Length > 0 Then
                Throw New Exception(strErrCotiza)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    'Obten relacion de cotizacion con la solicitud
    Public Function ObtenRelacionCotSol() As DataSet
        Try
            Dim _ObjDs As New DataSet
            Dim _strSql As String = String.Empty
            Dim objSD As New clsConexion
            Dim strParamStored As String = ""
            Dim _strError As String = String.Empty

            _strSql = "SELECT SOL_FL_CVE AS 'SOLICITUD', CVEPROPUESTA AS 'PROPUESTA', ID_COTIZACION AS 'COTIZACION' FROM REL_COT_PRO WHERE ID_COTIZACION = " & intCveCotizacion
            ArmaParametros(strParamStored, TipoDato.Cadena, "strQuery", _strSql)
            _ObjDs = objSD.EjecutaStoredProcedure("spEjecutaQuery", _strError, strParamStored)

            If _strError.Length > 0 Then
                Throw New Exception(_strError)
            End If

            Return _ObjDs
        Catch ex As Exception
            Throw New Exception("Error al obtener la relacion de la cotización con la solicitud.")
        End Try
    End Function
    'Obten datos de la solicitud para su generacion
    Public Function ImprimeSolicitud(ByVal intEmp As Integer, ByVal intCot As Integer, ByRef strMensaje As String) As DataSet
        Dim strErrCotiza As String = ""
        ImprimeSolicitud = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion
            ArmaParametros(strParamStored, TipoDato.Entero, "intCot", intCot.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "intEmp", intEmp.ToString)
            ImprimeSolicitud = objSD.EjecutaStoredProcedure("sp_ImprimeSolicitud", strErrCotiza, strParamStored)

            If ImprimeSolicitud Is Nothing OrElse ImprimeSolicitud.Tables.Count = 0 OrElse ImprimeSolicitud.Tables(0).Rows.Count = 0 Then
                Return Nothing
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
