'Tracker:INC-B-1554:JDRA:Se pierde sesion
'BBV-P-412:AVH:22/07/2016 RQB: SE AGREGA CAMPO CORREO ELECTRONICO
'BBV-P-412:GVARGAS:27/10/2016 RQ WSE: Agregadas propiedades a la clase
'BUG-PC-103: JBEJAR:21/08/2017 CAMBIO 
'BUG-PC-140: DCORNEJO:20/12/2017 CAMBIO EN ACTUALIZA PARAMETRO SE AGREGO LA SIGUIENTE LINEA ArmaParametros(strParamStored, TipoDato.Cadena, "usuReg", strUsuReg)
'RQ-PC9: DCORNEJO: 15/05/2018: SE AGREGAN COMBOS PARA LA PANTALLA relacionaAgenciaUsuario consulta la opc 11,12,13
Imports SDManejaBD

Public Class clsUsuariosSistema
    Inherits clsSession

    Private strErrUsuario As String = ""

    Private intUsuario As Integer = 0
    Private intEmpresa As Integer = 0
    Private intTipoSeguridad As Integer = 0
    Private intPerfil As Integer = 0
    Private intEstatus As Integer = 0

    Private strUser As String = ""
    Private strPwd As String = ""
    Private strFecCambioPwd As String = ""
    Private strUsuReg As String = ""

    Private strNombre As String = ""
    Private strApaterno As String = ""
    Private strAmaterno As String = ""
    Private intIdObjeto As Integer = 0
    Private strAgencia As String = String.Empty
    Private intIDAgencia As Integer = 0
    Private intIdEstado As Integer = 0
    Private strCorreo As String = ""
    Private intUsuInterno As Integer = 0
    Private strFecha_Reg As String = String.Empty
    'RQ-PC9 DCORNEJO
    Private strAlianza As String = String.Empty
    Private intIDAlianza As Integer = -1
    Private strGrupo As String = String.Empty
    Private intIDGrupo As Integer = -1
    Private strDivision As String = String.Empty
    Private intIDDivision As Integer = -1
    Private strEntidad As String = String.Empty
    Private intIDEntidad As Integer = -1


    Sub New()
    End Sub
    Sub New(ByVal intIDAcceso As Integer, ByVal intCveUsu As Integer)
        CargaSession(intIDAcceso)
        CargaUsuario(intCveUsu)
    End Sub

    Public ReadOnly Property ErrorUsuario() As String
        Get
            Return strErrUsuario
        End Get
    End Property

    Public WriteOnly Property UsuarioRegistro() As String
        Set(ByVal value As String)
            strUsuReg = value
        End Set
    End Property

    Public Property IDUsuario() As Integer
        Get
            Return intUsuario
        End Get
        Set(ByVal value As Integer)
            intUsuario = value
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

    Public Property IDTipoSeguridad() As Integer
        Get
            Return intTipoSeguridad
        End Get
        Set(ByVal value As Integer)
            intTipoSeguridad = value
        End Set
    End Property

    Public Property IDPerfil() As Integer
        Get
            Return intPerfil
        End Get
        Set(ByVal value As Integer)
            intPerfil = value
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


    Public Property UserName() As String
        Get
            Return strUser
        End Get
        Set(ByVal value As String)
            strUser = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return strPwd
        End Get
        Set(ByVal value As String)
            strPwd = value
        End Set
    End Property

    Public Property FechaCambioPwd() As String
        Get
            Return strFecCambioPwd
        End Get
        Set(ByVal value As String)
            strFecCambioPwd = value
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
    Public Property Apaterno() As String
        Get
            Return strApaterno
        End Get
        Set(ByVal value As String)
            strApaterno = value
        End Set
    End Property
    Public Property Amaterno() As String
        Get
            Return strAmaterno
        End Get
        Set(ByVal value As String)
            strAmaterno = value
        End Set
    End Property

    Public Property IdObjeto() As Integer
        Get
            Return intIdObjeto
        End Get
        Set(value As Integer)
            intIdObjeto = value
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

    Public Property IDAgencia() As Integer
        Get
            Return intIDAgencia
        End Get
        Set(value As Integer)
            intIDAgencia = value
        End Set
    End Property

    Public Property IDEstado() As Integer
        Get
            Return intIdEstado
        End Get
        Set(value As Integer)
            intIdEstado = value
        End Set
    End Property

    Public Property Correo() As String
        Get
            Return strCorreo
        End Get
        Set(value As String)
            strCorreo = value
        End Set
    End Property

    Public Property UsuInterno() As Integer
        Get
            Return intUsuInterno
        End Get
        Set(value As Integer)
            intUsuInterno = value
        End Set
    End Property

    Public Property Fecha_Reg() As String
        Get
            Return strFecha_Reg
        End Get
        Set(value As String)
            strFecha_Reg = value
        End Set
    End Property
    'RQ-PC9 DCORNEJO
    Public Property Alianza() As String
        Get
            Return strAlianza
        End Get
        Set(value As String)
            strAlianza = value
        End Set
    End Property

    Public Property IDAlianza() As Integer
        Get
            Return intIDAlianza
        End Get
        Set(value As Integer)
            intIDAlianza = value
        End Set
    End Property
    Public Property Grupo() As String
        Get
            Return strGrupo
        End Get
        Set(value As String)
            strGrupo = value
        End Set
    End Property
    Public Property IDGrupo() As Integer
        Get
            Return intIDGrupo
        End Get
        Set(value As Integer)
            intIDGrupo = value
        End Set
    End Property
    Public Property Division() As String
        Get
            Return strDivision
        End Get
        Set(value As String)
            strDivision = value
        End Set
    End Property
    Public Property IDDivision() As Integer
        Get
            Return intIDDivision
        End Get
        Set(value As Integer)
            intIDDivision = value
        End Set
    End Property
    Public Property Entidad() As String
        Get
            Return strEntidad
        End Get
        Set(value As String)
            strEntidad = value
        End Set
    End Property
    Public Property IDEntidad() As Integer
        Get
            Return intIDEntidad
        End Get
        Set(value As Integer)
            intIDEntidad = value
        End Set
    End Property

    Public Sub CargaUsuario(Optional ByVal intUsu As Integer = 0)
        Dim dtsRes As New DataSet
        Try
            If intUsu > 0 Then
                intUsuario = intUsu
                dtsRes = ManejaUsuario(1)
                intUsuario = 0
                If Trim$(strErrUsuario) = "" Then
                    If dtsRes.Tables(0).Rows.Count > 0 Then
                        intUsuario = dtsRes.Tables(0).Rows(0).Item("ID_USUARIO")
                        intEmpresa = dtsRes.Tables(0).Rows(0).Item("ID_EMPRESA")
                        intTipoSeguridad = dtsRes.Tables(0).Rows(0).Item("ID_TIPO_SEGURIDAD")
                        intPerfil = dtsRes.Tables(0).Rows(0).Item("ID_PERFIL")
                        intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                        strUser = dtsRes.Tables(0).Rows(0).Item("USERNAME")
                        strPwd = dtsRes.Tables(0).Rows(0).Item("PASSWORD")
                        strFecCambioPwd = dtsRes.Tables(0).Rows(0).Item("FEC_CAMBIO_PWD")
                        strFecha_Reg = dtsRes.Tables(0).Rows(0).Item("FEC_REG")
                    Else
                        strErrUsuario = "No se encontr� informaci�n para poder cargar el usuario"
                    End If
                End If
            Else
                strErrUsuario = "Sesion Terminada"
                'PAGE.Response.Redirect("../login.aspx")
            End If
        Catch ex As Exception
            strErrUsuario = ex.Message
        End Try
    End Sub

    Public Function ManejaUsuario(ByVal intOper As Integer) As DataSet
        strErrUsuario = ""
        ManejaUsuario = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta usuario
                    If intUsuario > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idUsuario", intUsuario.ToString)
                    If intEmpresa > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idEmpresa", intEmpresa.ToString)
                    If intPerfil > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPerfil", intPerfil.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intEstatus.ToString)
                    If Trim(strUser).Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "username", strUser)
                    If Trim(strNombre).Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nombre", strNombre)
                    If Trim(strFecha_Reg).Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "Fecha_Reg", strFecha_Reg)
                Case 2 ' inserta usuario
                    ArmaParametros(strParamStored, TipoDato.Entero, "idEmpresa", intEmpresa.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nombre", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "apaterno", strApaterno)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "amaterno", strAmaterno)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoSeg", intTipoSeguridad.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPerfil", intPerfil.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "username", strUser)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "password", strPwd)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "fecCambio", strFecCambioPwd)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usuReg", strUsuReg)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "correo", strCorreo)
                    ArmaParametros(strParamStored, TipoDato.Entero, "UsuInterno", intUsuInterno)
                Case 3 ' actualiza usuario
                    ArmaParametros(strParamStored, TipoDato.Entero, "idUsuario", intUsuario.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nombre", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "apaterno", strApaterno)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "amaterno", strAmaterno)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idEmpresa", intEmpresa.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoSeg", intTipoSeguridad.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPerfil", intPerfil.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "username", strUser)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "password", strPwd)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "fecCambio", strFecCambioPwd)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usuReg", strUsuReg)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "correo", strCorreo)
                    ArmaParametros(strParamStored, TipoDato.Entero, "UsuInterno", intUsuInterno)
                Case 4 ' borra usuario
                    ArmaParametros(strParamStored, TipoDato.Entero, "idUsuario", intUsuario.ToString)
                Case 5 ' Logueo
                    ArmaParametros(strParamStored, TipoDato.Cadena, "username", strUser)
                Case 6 ' Alta / Modificacion
                    If intUsuario > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idUsuario", intUsuario.ToString)
                    If Trim(strNombre) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "nombre", strNombre)
                    If Trim(strApaterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "apaterno", strApaterno)
                    If Trim(strAmaterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "amaterno", strAmaterno)
                    If intEmpresa > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idEmpresa", intEmpresa.ToString)
                    If intPerfil > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPerfil", intPerfil.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intEstatus.ToString)
                    If Trim(strUser) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "username", strUser)
                Case 7 ' actualiza password primera vez
                    ArmaParametros(strParamStored, TipoDato.Entero, "idUsuario", intUsuario.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "password", strPwd)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "fecCambio", strFecCambioPwd)
                Case 8 'consulta menu-perfil --IdObjeto
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPerfil", intPerfil.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "IdObjeto", IdObjeto.ToString)
                Case 9 'Asigna menu-perfil --IdObjeto
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPerfil", intPerfil.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "IdObjeto", IdObjeto.ToString)
                Case 10 'Elimina menu-perfil --IdObjeto
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPerfil", intPerfil.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "IdObjeto", IdObjeto.ToString)
                Case 11 ''Consulta Asigna Agencia
                    If intUsuario > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idUsuario", intUsuario.ToString)
                    If Trim(strAgencia).Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "agencia", strAgencia)
                    If intIDAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idagencia", intIDAgencia.ToString)
                    'RQ-PC9 DCORNEJO
                    If intIDAlianza > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idalianza", intIDAlianza.ToString)
                    If intIDGrupo > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idgrupo", intIDGrupo.ToString)
                    If intIDDivision > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "iddivision", intIDDivision.ToString)
                    If intIDEntidad > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "identidad", intIDEntidad.ToString)

                Case 12 ''Borra Agencia
                    If intUsuario > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idUsuario", intUsuario.ToString)
                    If Trim(strAgencia).Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "agencia", strAgencia)
                    If intIDAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idagencia", intIDAgencia.ToString)
                    'RQ-PC9 DCORNEJO
                    If intIDAlianza > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idalianza", intIDAlianza.ToString)
                    If intIDGrupo > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idgrupo", intIDGrupo.ToString)
                    If intIDDivision > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "iddivision", intIDDivision.ToString)
                    If intIDEntidad > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "identidad", intIDEntidad.ToString)

                Case 13 ''Inserta Asigna Agencia
                    If intUsuario > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idUsuario", intUsuario.ToString)
                    If Trim(strAgencia).Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "agencia", strAgencia)
                    If intIDAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idagencia", intIDAgencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usuReg", strUsuReg)
                    'RQ-PC9 DCORNEJO
                    If intIDAlianza > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idalianza", intIDAlianza.ToString)
                    If intIDGrupo > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idgrupo", intIDGrupo.ToString)
                    If intIDDivision > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "iddivision", intIDDivision.ToString)
                    If intIDEntidad > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "identidad", intIDEntidad.ToString)
                Case 14 ''Consulta Cotizador
                    If intUsuario > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idUsuario", intUsuario.ToString)
                Case 15 ''Consulta Cotizador-Vendedores
                    If intIDAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idagencia", intIDAgencia.ToString)
                    If intIdEstado > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idestado", intIdEstado.ToString)
                Case 16 ''Consulta Cotizador-Promotores
                    If intIDAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idagencia", intIDAgencia.ToString)
                    If intIdEstado > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idestado", intIdEstado.ToString)
            End Select

            ManejaUsuario = objSD.EjecutaStoredProcedure("spManejaUsuarios", strErrUsuario, strParamStored)
            If strErrUsuario = "" Then
                If intOper = 2 Then
                    intUsuario = ManejaUsuario.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("USUARIOS_SISTEMA", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrUsuario = ex.Message
        End Try
    End Function

    Public Function ObtenPermisosUsuario(Optional ByVal intMenu As Integer = 0) As DataSet
        ObtenPermisosUsuario = New DataSet
        Dim dtsRes As New DataSet
        Dim objPerm As New clsPermisos
        Dim objRow As DataRow
        Dim intR As Integer = 0
        Dim intUsu As Integer = 0
        Dim intObj As Integer = 0
        Dim strCves() As String

        Try
            'cargamos la esturctura
            ObtenPermisosUsuario = objPerm.CreaEstructuraPermisos(intMenu)
            If objPerm.ErrorPermisos = "" Then
                strCves = Split(objPerm.ClavesPermisos, ",")

                'obtenemos los permisos por usuario
                dtsRes = ManejaUsuario(1)
                If strErrUsuario = "" Then
                    If dtsRes.Tables(0).Rows.Count > 0 Then
                        For intR = 0 To dtsRes.Tables(0).Rows.Count - 1

                            'CREAMOS REGISTRO POR REGISTRO
                            objRow = ObtenPermisosUsuario.Tables(0).NewRow
                            objRow.Item(0) = CStr(dtsRes.Tables(0).Rows(intR).Item("USERNAME"))
                            intUsu = dtsRes.Tables(0).Rows(intR).Item("ID_USUARIO")

                            For intObj = 0 To UBound(strCves, 1)
                                objPerm = New clsPermisos(0, Val(strCves(intObj)), intUsu)
                                objRow.Item(intObj + 1) = intUsu & "|" & strCves(intObj) & "|" & IIf(objPerm.cveEstatus = 2, 1, 0)
                            Next

                            ObtenPermisosUsuario.Tables(0).Rows.Add(objRow)
                        Next
                    Else
                        strErrUsuario = "No se encontraron usuarios con el filtro proporcionado"
                    End If
                End If
            Else
                strErrUsuario = objPerm.ErrorPermisos
            End If
        Catch ex As Exception
            strErrUsuario = ex.Message
        End Try
    End Function

    Public Function ObtenPermisosPerfil(Optional ByVal intMenu As Integer = 0) As DataSet
        ObtenPermisosPerfil = New DataSet
        Dim dtsRes As New DataSet
        Dim objPerm As New clsPermisos
        Dim objPerfil As New clsParametros
        Dim objRow As DataRow
        Dim intR As Integer = 0
        Dim intPerf As Integer = 0
        Dim intObj As Integer = 0
        Dim strCves() As String

        Try
            'cargamos la esturctura
            ObtenPermisosPerfil = objPerm.CreaEstructuraPermisos(intMenu, True)
            If objPerm.ErrorPermisos = "" Then
                strCves = Split(objPerm.ClavesPermisos, ",")

                'obtenemos los permisos por perfil
                objPerfil.IDPadre = 70
                If intPerfil > 0 Then objPerfil.IDParametro = intPerfil

                dtsRes = objPerfil.ManejaParametro(1)
                If objPerfil.ErrorParametros = "" Then
                    If dtsRes.Tables(0).Rows.Count > 0 Then
                        For intR = 0 To dtsRes.Tables(0).Rows.Count - 1

                            'CREAMOS REGISTRO POR REGISTRO
                            objRow = ObtenPermisosPerfil.Tables(0).NewRow
                            objRow.Item(0) = CStr(dtsRes.Tables(0).Rows(intR).Item("TEXTO"))
                            intPerf = dtsRes.Tables(0).Rows(intR).Item("ID_PARAMETRO")

                            For intObj = 0 To UBound(strCves, 1)
                                objPerm = New clsPermisos(0, Val(strCves(intObj)), 0, intPerf)
                                objRow.Item(intObj + 1) = intPerf & "|" & strCves(intObj) & "|" & IIf(objPerm.cveEstatus = 2, 1, 0)
                            Next

                            ObtenPermisosPerfil.Tables(0).Rows.Add(objRow)
                        Next
                    Else
                        strErrUsuario = "No se encontraron usuarios con el filtro proporcionado"
                    End If
                Else
                    strErrUsuario = objPerfil.ErrorParametros
                End If
            Else
                strErrUsuario = objPerm.ErrorPermisos
            End If
        Catch ex As Exception
            strErrUsuario = ex.Message
        End Try
    End Function
End Class
