'--BBV-P-412:AUBALDO:06/07/2016 RQ10 – Agregar funcionalidad en la pantalla Alta de Agencias (Brecha 63,64, Ampliación de Brecha)
'BBV-P-412:AVH:21/07/2016 RQ20: OPCION-25 MODIFICACION MASIVA DE REL_COM_AGENCIA
'BBV-P-412:AUBALDO:26/07/2016 RQ A – Copia de Alta de Agencias de ProCotiza a Prodesk
'BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.
'BBV-P-412:AVH:02/08/2016 RQ20.2 OPCION 27,28 y 29 BonoCC
'BBVA-P-412: 08/08/2016: AMR: RQ09 – Carga Masiva de Plan de Financiamiento
'BBVA-P-412: 25-08-2016: AMR: RQCOT-01 Ajustes a la pantalla de Cotización.
'BUG-PC-30 AMR 03/01/2017: Al ingresar al portal procotiza e ingresar a la opcion de cotizacion manda un mensaje de warning.
'BBV-P-423: 04/01/17: JRHM RQCONYFOR-06 Se Agrego parametro valida bloqueo agencias a opciones crear, consultar y editar de agencias. 
'BBV-P412 RQCOT-01.2: AMR 23/02/2017: Seguros.
'BBVA-p-412: 18/04/2017: cgarcia RQTARESQ-06  se agrego la opcion de los esquemas para inserta, consulta y actualiza 
'BUG-PC-75:ERODRIGUEZ:15/06/2017:Se agrego checkbox para validar cuando la agencia cuenta con biometrico.
Imports SDManejaBD

Public Class clsAgencias
    Inherits clsSession

    Private strErrAgencia As String = ""
    'DATOS GENERALES AGENCIA    
    Private intMarca As Integer = 0
    Private intAgencia As Integer = 0
    Private intEstatus As Integer = 0    
    Private intTipoBaja As Integer = 0
    Private intAlianza As Integer = 0
    Private intGrupo As Integer = 0
    Private intDivision As Integer = 0
    Private intEsquemas As Integer = 0
    Private sngRegDef As Single = 0

    Private strNombre As String = ""
    Private strRFC As String = ""
    Private strCuentaAgencia As String = ""
    Private strTitularCuenta As String = ""
    Private strResponsableFact As String = ""    
    Private strUsuReg As String = ""
    Private strFecVigPaq As String = ""
    Private strFecReg As String = ""
    Private strFecVig As String = ""

    Private intEmpresa As Integer = 0
    Private intProducto As Integer = 0
    Private intAsesor As Integer = 0
    Private intPromotor As Integer = 0
    Private intVendedor As Integer = 0
    Private intPaquete As Integer = 0
    Private intMarcaProd As Integer = 0
    Private intMoneda As Integer = 0
    Private intEstatusOtro As Integer = 0
    Private intTipoProd As Integer = 0
    Private intClasif As Integer = 0
    Private intPerJurid As Integer = 0
    Private intTipoOperacion As Integer = 0
    Private intValidaBloqueo As Integer = 0
    Private intTieneBiometrico As Int16 = 0
    'COMISIONES AGENCIA
    Private intComisionAgencia As Integer = 0
    Private intComisionVendedor As Integer = 0

    Private dblPrcUdis As Double = 0
    Private dblDividendos As Double = 0
    Private dblBonoVendedor As Double = 0
    Private dblPagoFYI As Double = 0
    Private dblRegalias As Double = 0
    Private dblComiSeguro As Double = 0
    'DOMICILIO AGENCIA                
    Private intEstado As Integer = 0
    Private strDomicilio As String = ""
    Private strNoExt As String = ""
    Private strNoInt As String = ""
    Private intColonia As Integer = 0
    Private intMunicipio As Integer = 0
    Private intCiudad As Integer = 0
    Private strCodPos As String = ""
    Private intTipoDomi As Integer = 0
    'TELEFONO AGENCIA                
    Private intTelefono As Integer = 0
    Private strContacto As String = ""
    Private strLargaDist As String = ""
    Private strLada As String = ""
    Private strExtension As String = ""
    Private strTelefono As String = ""
    'EMAIL AGENCIA                
    Private strEmailContacto As String = ""
    Private strEmail As String = ""
    'AVH:BonoCC
    Private intNoCreditos As Integer = 0
    Private dblImporte As Double = 0

    Private intidusuario = 0

    Sub New()
    End Sub
    Sub New(ByVal intCveAgencia As Integer)
        CargaAgencia(intCveAgencia)
    End Sub

    Public ReadOnly Property ErrorAgencia() As String
        Get
            Return strErrAgencia
        End Get
    End Property

    Public WriteOnly Property UsuarioRegistro() As String
        Set(ByVal value As String)
            strUsuReg = value
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

    Public Property IDMarca() As Integer
        Get
            Return intMarca
        End Get
        Set(ByVal value As Integer)
            intMarca = value
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
    Public Property IDTipoBaja() As Integer
        Get
            Return intTipoBaja
        End Get
        Set(ByVal value As Integer)
            intTipoBaja = value
        End Set
    End Property
    Public Property IDAlianza() As Integer
        Get
            Return intAlianza
        End Get
        Set(ByVal value As Integer)
            intAlianza = value
        End Set
    End Property
    Public Property IDGrupo() As Integer
        Get
            Return intGrupo
        End Get
        Set(ByVal value As Integer)
            intGrupo = value
        End Set
    End Property
    Public Property IDDivision() As Integer
        Get
            Return intDivision
        End Get
        Set(ByVal value As Integer)
            intDivision = value
        End Set
    End Property
    Public Property IDDEsquema() As Integer
        Get
            Return intEsquemas
        End Get
        Set(ByVal value As Integer)
            intEsquemas = value
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

    Public Property IDTipoProducto() As Integer
        Get
            Return intTipoProd
        End Get
        Set(ByVal value As Integer)
            intTipoProd = value
        End Set
    End Property

    Public Property IDTipoOperacion() As Integer
        Get
            Return intTipoOperacion
        End Get
        Set(ByVal value As Integer)
            intTipoOperacion = value
        End Set
    End Property
    Public Property IDValidaBloqueo() As Integer
        Get
            Return intValidaBloqueo
        End Get
        Set(ByVal value As Integer)
            intValidaBloqueo = value
        End Set
    End Property
    Public Property IDTieneBiometrico() As Integer
        Get
            Return intTieneBiometrico
        End Get
        Set(ByVal value As Integer)
            intTieneBiometrico = value
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

    Public Property IDClasificacionProducto() As Integer
        Get
            Return intClasif
        End Get
        Set(ByVal value As Integer)
            intClasif = value
        End Set
    End Property

    Public Property IDMarcaProducto() As Integer
        Get
            Return intMarcaProd
        End Get
        Set(ByVal value As Integer)
            intMarcaProd = value
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

    Public Property IDEstatusOtro() As Integer
        Get
            Return intEstatusOtro
        End Get
        Set(ByVal value As Integer)
            intEstatusOtro = value
        End Set
    End Property

    Public Property RegistroDefault() As Single
        Get
            Return sngRegDef
        End Get
        Set(ByVal value As Single)
            sngRegDef = value
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
    Public Property RFC() As String
        Get
            Return strRFC
        End Get
        Set(ByVal value As String)
            strRFC = value
        End Set
    End Property
    Public Property CuentaAgencia() As String
        Get
            Return strCuentaAgencia
        End Get
        Set(ByVal value As String)
            strCuentaAgencia = value
        End Set
    End Property
    Public Property TitularCuenta() As String
        Get
            Return strTitularCuenta
        End Get
        Set(ByVal value As String)
            strTitularCuenta = value
        End Set
    End Property
    Public Property ResponsableFact() As String
        Get
            Return strResponsableFact
        End Get
        Set(ByVal value As String)
            strResponsableFact = value
        End Set
    End Property
    Public Property Domicilio() As String
        Get
            Return strDomicilio
        End Get
        Set(ByVal value As String)
            strDomicilio = value
        End Set
    End Property

    Public Property Telefono() As String
        Get
            Return strTelefono
        End Get
        Set(ByVal value As String)
            strTelefono = value
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

    Public Property IDAsesor() As Integer
        Get
            Return intAsesor
        End Get
        Set(ByVal value As Integer)
            intAsesor = value
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

    Public Property IDPaquete() As Integer
        Get
            Return intPaquete
        End Get
        Set(ByVal value As Integer)
            intPaquete = value
        End Set
    End Property

    Public Property FechaVigenciaPaquete() As String
        Get
            Return strFecVigPaq
        End Get
        Set(ByVal value As String)
            strFecVigPaq = value
        End Set
    End Property
    Public Property FechaRegistro() As String
        Get
            Return strFecReg
        End Get
        Set(ByVal value As String)
            strFecReg = value
        End Set
    End Property
    Public Property FechaVigencia() As String
        Get
            Return strFecVig
        End Get
        Set(ByVal value As String)
            strFecVig = value
        End Set
    End Property
    Public Property ComisionAgencia() As Integer
        Get
            Return intComisionAgencia
        End Get
        Set(ByVal value As Integer)
            intComisionAgencia = value
        End Set
    End Property
    Public Property ComisionVendedor() As Integer
        Get
            Return intComisionVendedor
        End Get
        Set(ByVal value As Integer)
            intComisionVendedor = value
        End Set
    End Property
    Public Property PrcUdis() As Double
        Get
            Return dblPrcUdis
        End Get
        Set(ByVal value As Double)
            dblPrcUdis = value
        End Set
    End Property
    Public Property Dividendos() As Double
        Get
            Return dblDividendos
        End Get
        Set(ByVal value As Double)
            dblDividendos = value
        End Set
    End Property
    Public Property BonoVendedor() As Double
        Get
            Return dblBonoVendedor
        End Get
        Set(ByVal value As Double)
            dblBonoVendedor = value
        End Set
    End Property
    Public Property PagoFYI() As Double
        Get
            Return dblPagoFYI
        End Get
        Set(ByVal value As Double)
            dblPagoFYI = value
        End Set
    End Property
    Public Property Regalias() As Double
        Get
            Return dblRegalias
        End Get
        Set(ByVal value As Double)
            dblRegalias = value
        End Set
    End Property
    Public Property ComiSeguro() As Double
        Get
            Return dblComiSeguro
        End Get
        Set(ByVal value As Double)
            dblComiSeguro = value
        End Set
    End Property                
    Public Property NoExt() As String
        Get
            Return strNoExt
        End Get
        Set(ByVal value As String)
            strNoExt = value
        End Set
    End Property
    Public Property NoInt() As String
        Get
            Return strNoInt
        End Get
        Set(ByVal value As String)
            strNoInt = value
        End Set
    End Property
    Public Property CodPos() As String
        Get
            Return strCodPos
        End Get
        Set(ByVal value As String)
            strCodPos = value
        End Set
    End Property        
    Public Property Colonia() As Integer
        Get
            Return intColonia
        End Get
        Set(ByVal value As Integer)
            intColonia = value
        End Set
    End Property
    Public Property Municipio() As Integer
        Get
            Return intMunicipio
        End Get
        Set(ByVal value As Integer)
            intMunicipio = value
        End Set
    End Property
    Public Property Ciudad() As Integer
        Get
            Return intCiudad
        End Get
        Set(ByVal value As Integer)
            intCiudad = value
        End Set
    End Property
    Public Property TTipoDomicilio() As Integer
        Get
            Return intTipoDomi
        End Get
        Set(ByVal value As Integer)
            intTipoDomi = value
        End Set
    End Property    
    Public Property TTelefono() As Integer
        Get
            Return intTelefono
        End Get
        Set(ByVal value As Integer)
            intTelefono = value
        End Set
    End Property
    Public Property Contacto() As String
        Get
            Return strContacto
        End Get
        Set(ByVal value As String)
            strContacto = value
        End Set
    End Property
    Public Property LargaDist() As String
        Get
            Return strLargaDist
        End Get
        Set(ByVal value As String)
            strLargaDist = value
        End Set
    End Property
    Public Property Lada() As String
        Get
            Return strLada
        End Get
        Set(ByVal value As String)
            strLada = value
        End Set
    End Property
    Public Property Extension() As String
        Get
            Return strExtension
        End Get
        Set(ByVal value As String)
            strExtension = value
        End Set
    End Property        
    Public Property EmailContacto() As String
        Get
            Return strEmailContacto
        End Get
        Set(ByVal value As String)
            strEmailContacto = value
        End Set
    End Property
    Public Property Email() As String
        Get
            Return strEmail
        End Get
        Set(ByVal value As String)
            strEmail = value
        End Set
    End Property
    Public Property NoCreditos() As Integer
        Get
            Return intNoCreditos
        End Get
        Set(ByVal value As Integer)
            intNoCreditos = value
        End Set
    End Property
    Public Property Importe() As Double
        Get
            Return dblImporte
        End Get
        Set(ByVal value As Double)
            dblImporte = value
        End Set
    End Property

    Public Property IDUsuario As Integer
        Get
            Return intidusuario
        End Get
        Set(value As Integer)
            intidusuario = value
        End Set
    End Property
    
    Public Sub CargaAgencia(Optional ByVal intAse As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intAgencia = intAse
            dtsRes = ManejaAgencia(1)
            intAgencia = 0
            If Trim$(strErrAgencia) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intAgencia = dtsRes.Tables(0).Rows(0).Item("ID_AGENCIA")
                    ''intMarca = dtsRes.Tables(0).Rows(0).Item("ID_MARCA")
                    intEstado = dtsRes.Tables(0).Rows(0).Item("ID_ESTADO")
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    sngRegDef = dtsRes.Tables(0).Rows(0).Item("REG_DEFAULT")
                    strNombre = dtsRes.Tables(0).Rows(0).Item("NOMBRE")
                    strDomicilio = dtsRes.Tables(0).Rows(0).Item("CALLE")
                    strTelefono = dtsRes.Tables(0).Rows(0).Item("TELEFONO")
                    intAlianza = dtsRes.Tables(0).Rows(0).Item("ID_ALIANZA")
                Else
                    strErrAgencia = "No se encontró información para poder cargar la agencia"
                End If
            End If
        Catch ex As Exception
            strErrAgencia = ex.Message
        End Try
    End Sub

    Public Function ManejaAgencia(ByVal intOper As Integer) As DataSet
        strErrAgencia = ""
        ManejaAgencia = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta Agencia
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If intMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    If intEstado > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idEstado", intEstado.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If Trim(strNombre) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                Case 2 ' inserta Agencia
                    'DATOS GENERALES AGENCIA                    
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)                    
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "RFC", strRFC)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "CuentaAgencia", strCuentaAgencia)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "TitularCuenta", strTitularCuenta)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "ResponsableFact", strResponsableFact)
                    ArmaParametros(strParamStored, TipoDato.Entero, "IdTipoBaja", intTipoBaja.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "FecReg", strFecReg)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "FecVig", strFecVig)
                    ArmaParametros(strParamStored, TipoDato.Entero, "IdAlianza", intAlianza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "IdGrupo", intGrupo.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "IdDivision", intDivision.ToString)                    
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_ESQUEMAS", intEsquemas.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    'DATOS COMISIONES AGENCIA
                    ArmaParametros(strParamStored, TipoDato.Entero, "ComAgencia", intComisionAgencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ComVendedor", intComisionVendedor.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "PrcUdis", dblPrcUdis.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "Dividendos", dblDividendos.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "BonoVendedor", dblBonoVendedor.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "PagFYI", dblPagoFYI.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "Regalias", dblRegalias.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ComiSeguro", dblComiSeguro.ToString)
                    'DATOS DOMICILIO AGENCIA                   
                    ArmaParametros(strParamStored, TipoDato.Entero, "idEstado", intEstado.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "domicilio", strDomicilio)                    
                    ArmaParametros(strParamStored, TipoDato.Cadena, "NoExt", strNoExt)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "NoInt", strNoInt)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "CPostal", strCodPos)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Colonia", intColonia)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMuicipio", intMunicipio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCiudad", intCiudad.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoDomicilio", intTipoDomi.ToString)
                    'DATOS TELEFONO AGENCIA                    
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTTelefono", intTelefono.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "ContactoTel", strContacto)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "LdistTel", strLargaDist)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Lada", strLada)                    
                    ArmaParametros(strParamStored, TipoDato.Cadena, "tel", strTelefono)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Extension", strExtension)
                    'DATOS EMAIL AGENCIA                                        
                    ArmaParametros(strParamStored, TipoDato.Cadena, "EmailContacto", strEmailContacto)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Email", strEmail)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ValidaBloqueo", intValidaBloqueo)
                    ArmaParametros(strParamStored, TipoDato.Entero, "TieneBiometrico", intTieneBiometrico)
                Case 3 ' actualiza Agencia
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "RFC", strRFC)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "CuentaAgencia", strCuentaAgencia)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "TitularCuenta", strTitularCuenta)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "ResponsableFact", strResponsableFact)
                    ArmaParametros(strParamStored, TipoDato.Entero, "IdTipoBaja", intTipoBaja.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "FecReg", strFecReg)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "FecVig", strFecVig)
                    ArmaParametros(strParamStored, TipoDato.Entero, "IdAlianza", intAlianza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "IdGrupo", intGrupo.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "IdDivision", intDivision.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_ESQUEMAS", intEsquemas.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    'DATOS COMISIONES AGENCIA
                    ArmaParametros(strParamStored, TipoDato.Entero, "ComAgencia", intComisionAgencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ComVendedor", intComisionVendedor.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "PrcUdis", dblPrcUdis.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "Dividendos", dblDividendos.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "BonoVendedor", dblBonoVendedor.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "PagFYI", dblPagoFYI.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "Regalias", dblRegalias.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ComiSeguro", dblComiSeguro.ToString)
                    'DATOS DOMICILIO AGENCIA                   
                    ArmaParametros(strParamStored, TipoDato.Entero, "idEstado", intEstado.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "domicilio", strDomicilio)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "NoExt", strNoExt)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "NoInt", strNoInt)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "CPostal", strCodPos)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Colonia", intColonia)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMuicipio", intMunicipio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCiudad", intCiudad.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoDomicilio", intTipoDomi.ToString)
                    'DATOS TELEFONO AGENCIA                    
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTTelefono", intTelefono.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "ContactoTel", strContacto)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "LdistTel", strLargaDist)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Lada", strLada)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "tel", strTelefono)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Extension", strExtension)
                    'DATOS EMAIL AGENCIA                                        
                    ArmaParametros(strParamStored, TipoDato.Cadena, "EmailContacto", strEmailContacto)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Email", strEmail)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ValidaBloqueo", intValidaBloqueo)
                    ArmaParametros(strParamStored, TipoDato.Entero, "TieneBiometrico", intTieneBiometrico)
                Case 4 ' borra Agencia
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                Case 5 ' borra agencias - paquetes
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If intPaquete > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                Case 6 ' inserta agencia - paquete
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 7 'consulta agencia - paquete
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If intPaquete > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", intPaquete.ToString)
                    If intProducto > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idProducto", intProducto.ToString)
                    If intTipoProd > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoProd", intTipoProd.ToString)
                    If intTipoOperacion > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoOper", intTipoOperacion.ToString)
                    If intPerJurid > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPerJurid", intPerJurid.ToString)
                    If intMoneda > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMoneda", intMoneda.ToString)
                    If intClasif > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", intClasif.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If intEstatusOtro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                    If Trim(strFecVigPaq) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "fecVigPaquete", strFecVigPaq)
                Case 8 ' borra agencias - empresas
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If intEmpresa > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idEmpresa", intEmpresa.ToString)
                Case 9 ' inserta agencia - empresa
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idEmpresa", intEmpresa.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 10 ' consulta agencia - empresa
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If intEmpresa > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idEmpresa", intEmpresa.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If intEstatusOtro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                    If strNombre.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                Case 11 ' borra agencias - productos
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If intProducto > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idProducto", intProducto.ToString)
                Case 12 ' inserta agencia - producto
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idProducto", intProducto.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 13 ' consulta agencia - producto
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If intProducto > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idProducto", intProducto.ToString)
                    If intMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca)
                    If intMarcaProd > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarcaProd", intMarcaProd.ToString)
                    If intTipoProd > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoProd", intTipoProd.ToString)
                    If intClasif > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", intClasif.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If intEstatusOtro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                Case 14 ' borra agencias - promotores
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If intPromotor > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPromotor", intPromotor.ToString)
                Case 15 ' inserta agencia - promotor
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPromotor", intPromotor.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 16 ' consulta agencia - promotor
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If intPromotor > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPromotor", intPromotor.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If intEstatusOtro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                Case 17 ' borra agencias - asesores
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If intAsesor > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAsesor", intAsesor.ToString)
                Case 18 ' inserta agencia - asesor
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAsesor", intAsesor.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 19 ' consulta agencia - asesor
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If intAsesor > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAsesor", intAsesor.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If intEstatusOtro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                Case 20 ' borra agencias - vendedores
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If intVendedor > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idVendedor", intVendedor.ToString)
                Case 21 ' inserta agencia - vendedor
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idVendedor", intVendedor.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 22 ' consulta agencia - vendedor
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If intVendedor > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idVendedor", intVendedor.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If intEstatusOtro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                Case 23 ' consulta agencia - marcas
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If intProducto > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idProducto", intProducto.ToString)
                    If intMarcaProd > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarcaProd", intMarcaProd.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If intEstatusOtro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatOtro", intEstatusOtro.ToString)
                Case 24 ' consulta Marcas asignadas a Agencias
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                Case 25 'BBV-P-412:AVH:21/07/2016 RQ20:MODIFICACION MASIVA DE REL_COM_AGENCIA
                    If intAlianza >= 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", intAlianza.ToString)
                    If intGrupo >= 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idGrupo", intGrupo.ToString)
                    If intDivision >= 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idDivision", intDivision.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "PrcUdis", dblPrcUdis.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "PagFYI", dblPagoFYI.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "Dividendos", dblDividendos.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "Regalias", dblRegalias.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "BonoVendedor", dblBonoVendedor.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "ComiSeguro", dblComiSeguro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 26 ' consulta Agencias
                    If intEstado > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idEstado", intEstado.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idusuario", intidusuario.ToString)
               Case 27 'BBV-P-412:AVH:02/08/2016 RQ20.2 OPCION 27 INSERTA BonoCC
                    If intAlianza >= 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", intAlianza.ToString)
                    If intGrupo >= 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idGrupo", intGrupo.ToString)
                    If intDivision >= 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idDivision", intDivision.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "NO_CREDITOS", intNoCreditos.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "Importe", dblImporte.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                Case 28 'BBV-P-412:AVH:02/08/2016 RQ20.2 OPCION 28 CONSULTA BonoCC
                    If intAlianza >= 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", intAlianza.ToString)
                    If intGrupo >= 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idGrupo", intGrupo.ToString)
                    If intDivision >= 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idDivision", intDivision.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                Case 29 'BBV-P-412:AVH:03/08/2016 RQ20.2 OPCION 29 ELIMINA BonoCC
                    If intAlianza >= 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", intAlianza.ToString)
                    If intGrupo >= 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idGrupo", intGrupo.ToString)
                    If intDivision >= 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idDivision", intDivision.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "NO_CREDITOS", intNoCreditos.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "Importe", dblImporte.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                Case 30 'Consulta cotizador alianza, grupo, division
                    If intAlianza <> -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", intAlianza.ToString)
                    If intGrupo <> -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idGrupo", intGrupo.ToString)
                    If intDivision <> -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idDivision", intDivision.ToString)
                    If intEstado > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idEstado", intEstado.ToString)
                    If intidusuario > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idusuario", intidusuario.ToString)
                'BUG-PC-30
                Case 31 ' consulta Agencias
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idusuario", intidusuario.ToString)
                Case 32 'Valida el nombre de la Agencia
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
            End Select

            ManejaAgencia = objSD.EjecutaStoredProcedure("spManejaAgencias", strErrAgencia, strParamStored)
            If strErrAgencia = "" Then
                If intOper = 2 Then
                    If "ERROR" = ManejaAgencia.Tables(0).Rows(0).Item(0).ToString Then
                        strErrAgencia = "ERROR AL TRATAR DE INSERTAR EN PRODESKNET"
                        Exit Try
                    Else
                    intAgencia = ManejaAgencia.Tables(0).Rows(0).Item(0)
                End If

                End If

                If intOper = 2 Or intOper = 3 Or intOper = 25 Then
                    GuardaLog("AGENCIAS", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            Else
                If "ERROR" = ManejaAgencia.Tables(0).Rows(0).Item(0) Then
                    strErrAgencia = "ERROR AL TRATAR DE INSERTAR EN PRODESKNET"
                    Exit Try
                End If
            End If

        Catch ex As Exception
            strErrAgencia = ex.Message
        End Try
    End Function
End Class
