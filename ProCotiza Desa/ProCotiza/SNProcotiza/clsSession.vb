Imports SDManejaBD

#Region "Trackers"
'AOC 29052013 Mejoras procotiza

#End Region

Public Class clsSession
    Private strErrSession As String = ""

    Private intUsuAcc As Integer = 0
    Private intEmpresaAcc As Integer = 0
    Private intPersonaAcc As Integer = 0
    Private intIDAcc As Integer = 0

    Private strUserAcc As String = ""
    Private strIPAcc As String = ""
    Private strUrlAccSession As String = ""

    Private blnAccesoVal As Boolean = False

    Public Sub New()
        blnAccesoVal = True
    End Sub

    Public Sub New(ByVal intIDAcceso As Integer)
        CargaSession(intIDAcceso)
    End Sub

    Public ReadOnly Property ErrorSession() As String
        Get
            Return strErrSession
        End Get
    End Property

    Public ReadOnly Property AccesoID() As Integer
        Get
            Return intIDAcc
        End Get
    End Property

    Public ReadOnly Property AccesoValido() As Integer
        Get
            Return blnAccesoVal
        End Get
    End Property

    Public Property UserNameAcceso() As String
        Get
            Return strUserAcc
        End Get

        Set(ByVal Value As String)
            strUserAcc = Value
        End Set
    End Property

    Public Property UsuarioAccesoID() As Integer
        Get
            Return intUsuAcc
        End Get

        Set(ByVal Value As Integer)
            intUsuAcc = Value
        End Set
    End Property

    Public Property PersonaAccesoID() As Integer
        Get
            Return intPersonaAcc
        End Get

        Set(ByVal Value As Integer)
            intPersonaAcc = Value
        End Set
    End Property

    Public Property EmpresaAccesoID() As Integer
        Get
            Return intEmpresaAcc
        End Get

        Set(ByVal Value As Integer)
            intEmpresaAcc = Value
        End Set
    End Property

    Public Property IP() As String
        Get
            Return strIPAcc
        End Get

        Set(ByVal Value As String)
            strIPAcc = Value
        End Set
    End Property

    Public Property URLAccesoSession() As String
        Get
            Return strUrlAccSession
        End Get

        Set(ByVal Value As String)
            strUrlAccSession = Value
        End Set
    End Property

    Public Sub RegistraAcceso()
        Dim dtsRes As New DataSet

        Try
            dtsRes = ManejaSession(2)
        Catch ex As Exception
            strErrSession = ex.Message
        End Try
    End Sub

    Public Sub RegistraSalida()
        Dim dtsRes As New DataSet

        Try
            dtsRes = ManejaSession(3)
        Catch ex As Exception
            strErrSession = ex.Message
        End Try
    End Sub

    Public Sub GuardaLog(ByVal strTabla As String, _
                         ByVal strValores As String, _
                         ByVal intAccion As Integer)

        Dim strParamStored As String = ""
        Try
            Dim objSD As New clsConexion
            Dim dtsRes As New DataSet

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", "2")
            ArmaParametros(strParamStored, TipoDato.Entero, "idPersona", intPersonaAcc.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "idEmpresa", intEmpresaAcc.ToString)
            ArmaParametros(strParamStored, TipoDato.Entero, "idTipoAcc", intAccion.ToString)
            ArmaParametros(strParamStored, TipoDato.Cadena, "tabla", strTabla)
            ArmaParametros(strParamStored, TipoDato.Cadena, "valores", strValores)
            ArmaParametros(strParamStored, TipoDato.Cadena, "usuReg", strUserAcc)

            dtsRes = objSD.EjecutaStoredProcedure("spManejaLog", strErrSession, strParamStored)

        Catch ex As Exception
            strErrSession = ex.Message
        End Try
    End Sub

    Public Sub CargaSession(ByVal intIDAcceso As Integer)
        Dim dtsRes As New DataSet
        'AOC 29052013 Mejoras procotiza
        If intIDAcceso > 0 Then

            intIDAcc = intIDAcceso
            dtsRes = ManejaSession(1)
            intIDAcc = 0

            If Trim$(strErrSession) = "" Then
                blnAccesoVal = True
                intIDAcc = dtsRes.Tables(0).Rows(0).Item("ID_ACCESO")
                intUsuAcc = dtsRes.Tables(0).Rows(0).Item("ID_USUARIO")
                intEmpresaAcc = dtsRes.Tables(0).Rows(0).Item("ID_EMPRESA")
                strUserAcc = dtsRes.Tables(0).Rows(0).Item("USERNAME")
                strIPAcc = dtsRes.Tables(0).Rows(0).Item("IP")
                strUrlAccSession = dtsRes.Tables(0).Rows(0).Item("URL_ACCESO")
            End If
        End If
        'AOC 29052013 Mejoras procotiza
    End Sub

    Public Function ManejaSession(ByVal intOper As Integer) As DataSet
        strErrSession = ""
        ManejaSession = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta session
                    If intIDAcc > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAcceso", intIDAcc.ToString)
                    If intUsuAcc > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idUsuario", intUsuAcc.ToString)
                    If intEmpresaAcc > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idEmpresa", intEmpresaAcc.ToString)
                    If intPersonaAcc > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPersona", intPersonaAcc.ToString)
                Case 2 ' inserta session
                    ArmaParametros(strParamStored, TipoDato.Entero, "idUsuario", intUsuAcc.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idEmpresa", intEmpresaAcc.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "userName", strUserAcc)
                    If Trim(strIPAcc) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "ip", strIPAcc)
                    If Trim(strUrlAccSession) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "urlAcceso", strUrlAccSession)
                Case 3 ' actualiza session
                    'no se mandan parametros
                Case 4 ' borra session
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAcceso", intIDAcc.ToString)
            End Select

            ManejaSession = objSD.EjecutaStoredProcedure("spManejaSession", strErrSession, strParamStored)
            If strErrSession = "" Then
                If intOper = 2 Then
                    intIDAcc = ManejaSession.Tables(0).Rows(0).Item(0)
                End If
            End If

        Catch ex As Exception
            strErrSession = ex.Message
        End Try
    End Function
End Class
