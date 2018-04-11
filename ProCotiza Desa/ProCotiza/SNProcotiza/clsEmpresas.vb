Imports SDManejaBD

Public Class clsEmpresas
    Inherits clsSession

    Private strErrEmpresa As String = ""

    Private intEmpresa As Integer = 0
    Private intEstatus As Integer = 0
    Private sngRegDef As Single = 0

    Private strNombre As String = ""
    Private strNomCto As String = ""
    Private strIniVig As String = ""
    Private strFinVig As String = ""
    Private strUsuReg As String = ""
    Private strIdExterno As String = ""
    Private strURLAcceso As String = ""
    Private strImagenLogin As String = ""
    Private strColorFondoLogin As String = ""
    Private strLogoEncabezado As String = ""
    Private strLogoReporte As String = ""

    Sub New()
    End Sub
    Sub New(ByVal intCveEmp As Integer)
        CargaEmpresa(intCveEmp)
    End Sub

    Public ReadOnly Property ErrorEmpresa() As String
        Get
            Return strErrEmpresa
        End Get
    End Property

    Public WriteOnly Property UsuarioRegistro() As String
        Set(ByVal value As String)
            strUsuReg = value
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

    Public Property IDEstatus() As Integer
        Get
            Return intEstatus
        End Get
        Set(ByVal value As Integer)
            intEstatus = value
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

    Public Property RazonSocial() As String
        Get
            Return strNombre
        End Get
        Set(ByVal value As String)
            strNombre = value
        End Set
    End Property

    Public Property IDExterno() As String
        Get
            Return strIdExterno
        End Get
        Set(ByVal value As String)
            strIdExterno = value
        End Set
    End Property

    Public Property NombreCorto() As String
        Get
            Return strNomCto
        End Get
        Set(ByVal value As String)
            strNomCto = value
        End Set
    End Property

    Public Property URLAcceso() As String
        Get
            Return strURLAcceso
        End Get
        Set(ByVal value As String)
            strURLAcceso = value
        End Set
    End Property

    Public Property ImagenLogin() As String
        Get
            Return strImagenLogin
        End Get
        Set(ByVal value As String)
            strImagenLogin = value
        End Set
    End Property

    Public Property ColorFondoLogin() As String
        Get
            Return strColorFondoLogin
        End Get
        Set(ByVal value As String)
            strColorFondoLogin = value
        End Set
    End Property

    Public Property ImagenEncabezado() As String
        Get
            Return strLogoEncabezado
        End Get
        Set(ByVal value As String)
            strLogoEncabezado = value
        End Set
    End Property

    Public Property ImagenLogoReporte() As String
        Get
            Return strLogoReporte
        End Get
        Set(ByVal value As String)
            strLogoReporte = value
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

    Public Sub CargaEmpresa(Optional ByVal intEmp As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intEmpresa = intEmp
            dtsRes = ManejaEmpresa(1)
            intEmpresa = 0
            If Trim$(strErrEmpresa) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intEmpresa = dtsRes.Tables(0).Rows(0).Item("ID_EMPRESA")
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    sngRegDef = dtsRes.Tables(0).Rows(0).Item("REG_DEFAULT")
                    strNombre = dtsRes.Tables(0).Rows(0).Item("RAZON_SOCIAL")
                    strIdExterno = dtsRes.Tables(0).Rows(0).Item("ID_EXTERNO")
                    strNomCto = dtsRes.Tables(0).Rows(0).Item("NOMBRE_CORTO")
                    strIniVig = dtsRes.Tables(0).Rows(0).Item("INICIO_VIGENCIA")
                    strFinVig = dtsRes.Tables(0).Rows(0).Item("FIN_VIGENCIA")
                    strURLAcceso = dtsRes.Tables(0).Rows(0).Item("URL_ACCESO")
                    strImagenLogin = dtsRes.Tables(0).Rows(0).Item("IMAGEN_LOGIN")
                    strColorFondoLogin = dtsRes.Tables(0).Rows(0).Item("COLOR_FONDO_LOGIN")
                    strLogoEncabezado = dtsRes.Tables(0).Rows(0).Item("LOGO_ENCABEZADO")
                    strLogoReporte = dtsRes.Tables(0).Rows(0).Item("LOGO_REPORTE")
                Else
                    strErrEmpresa = "No se encontró información para poder cargar la empresa"
                End If
            End If
        Catch ex As Exception
            strErrEmpresa = ex.Message
        End Try
    End Sub

    Public Function ManejaEmpresa(ByVal intOper As Integer) As DataSet
        strErrEmpresa = ""
        ManejaEmpresa = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta empresa
                    If intEmpresa > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idEmp", intEmpresa.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If Trim(strNombre) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    If Trim(strNomCto) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "nom_cto", strNomCto)
                    If Trim(strIniVig) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "ini_vig", strIniVig.ToString)
                    If Trim(strFinVig) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "fin_vig", strFinVig.ToString)
                    If Trim(strIdExterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "idExterno", strIdExterno)
                    If Trim(strURLAcceso) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "urlAcceso", strURLAcceso)
                Case 2 ' inserta empresa
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom_cto", strNomCto)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "ini_vig", strIniVig)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "fin_vig", strFinVig)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    If Trim(strURLAcceso) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "urlAcceso", strURLAcceso)
                    If Trim(strImagenLogin) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "imagenLogin", strImagenLogin)
                    If Trim(strColorFondoLogin) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "colorFondoLogin", strColorFondoLogin)
                    If Trim(strLogoEncabezado) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "logoEncabezado", strLogoEncabezado)
                    If Trim(strLogoReporte) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "logoRep", strLogoReporte)
                    If Trim(strIdExterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "idExterno", strIdExterno)
                Case 3 ' actualiza empresa
                    ArmaParametros(strParamStored, TipoDato.Entero, "idEmp", intEmpresa.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom_cto", strNomCto)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "ini_vig", strIniVig)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "fin_vig", strFinVig)
                    If Trim(strURLAcceso) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "urlAcceso", strURLAcceso)
                    If Trim(strImagenLogin) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "imagenLogin", strImagenLogin)
                    If Trim(strColorFondoLogin) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "colorFondoLogin", strColorFondoLogin)
                    If Trim(strLogoEncabezado) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "logoEncabezado", strLogoEncabezado)
                    If Trim(strLogoReporte) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "logoRep", strLogoReporte)
                    If Trim(strIdExterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "idExterno", strIdExterno)
                Case 4 ' borra empresa
                    ArmaParametros(strParamStored, TipoDato.Entero, "idEmp", intEmpresa.ToString)
            End Select

            ManejaEmpresa = objSD.EjecutaStoredProcedure("spManejaEmpresasAfiliadas", strErrEmpresa, strParamStored)
            If strErrEmpresa = "" Then
                If intOper = 2 Then
                    intEmpresa = ManejaEmpresa.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("EMPRESAS_AFILIADAS", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrEmpresa = ex.Message
        End Try
    End Function
End Class
