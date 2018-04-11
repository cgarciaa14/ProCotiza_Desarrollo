Imports SDManejaBD

Public Class clsGiros
    Inherits clsSession

    Private strErrGiros As String = ""

    Private intGiro As Integer = 0
    Private intEstatus As Integer = 0
    Private sngRegDef As Single = 0

    Private strIdExt As String = ""
    Private strNombre As String = ""
    Private strUsuReg As String = ""
    Private strTipoOpr As String = ""


    Sub New()
    End Sub
    Sub New(ByVal intCveGiro As Integer)
        CargaGiro(intCveGiro)
    End Sub

    Public ReadOnly Property ErrorGiros() As String
        Get
            Return strErrGiros
        End Get
    End Property

    Public WriteOnly Property UsuarioRegistro() As String
        Set(ByVal value As String)
            strUsuReg = value
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

    Public Property IDEstatus() As Integer
        Get
            Return intEstatus
        End Get
        Set(ByVal value As Integer)
            intEstatus = value
        End Set
    End Property

    Public Property IDExterno() As String
        Get
            Return strIdExt
        End Get
        Set(ByVal value As String)
            strIdExt = value
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

    Public Property TipoOperacion() As String
        Get
            Return strTipoOpr
        End Get
        Set(ByVal value As String)
            strTipoOpr = value
        End Set
    End Property


    Public Sub CargaGiro(Optional ByVal intGir As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intGiro = intGir
            dtsRes = ManejaGiro(1)
            intGiro = 0
            If Trim$(strErrGiros) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intGiro = dtsRes.Tables(0).Rows(0).Item("ID_GIRO")
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    sngRegDef = dtsRes.Tables(0).Rows(0).Item("REG_DEFAULT")
                    strNombre = dtsRes.Tables(0).Rows(0).Item("NOMBRE")
                    strIdExt = dtsRes.Tables(0).Rows(0).Item("ID_EXTERNO")
                Else
                    strErrGiros = "No se encontr� informaci�n para poder cargar el giro"
                End If
            End If
        Catch ex As Exception
            strErrGiros = ex.Message
        End Try
    End Sub

    Public Function ManejaGiro(ByVal intOper As Integer) As DataSet
        strErrGiros = ""
        ManejaGiro = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta Giro
                    If intGiro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idGiro", intGiro.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If Trim(strTipoOpr) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "TipoOper", strTipoOpr)
                    If Trim(strNombre) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                Case 2 ' inserta Giro
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "idExterno", strIdExt)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    If Trim(strTipoOpr) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "TipoOper", strTipoOpr)
                Case 3 ' actualiza Giro
                    ArmaParametros(strParamStored, TipoDato.Entero, "idGiro", intGiro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "idExterno", strIdExt)
                    If Trim(strTipoOpr) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "TipoOper", strTipoOpr)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 4 ' borra Giro
                    ArmaParametros(strParamStored, TipoDato.Entero, "idGiro", intGiro.ToString)
                Case 5 ' Consulta Giro sin Tipo Operacion
                    If intGiro > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idGiro", intGiro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
            End Select

            ManejaGiro = objSD.EjecutaStoredProcedure("spManejaGiros", strErrGiros, strParamStored)
            If strErrGiros = "" Then
                If intOper = 2 Then
                    intGiro = ManejaGiro.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("GIROS", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrGiros = ex.Message
        End Try
    End Function
End Class
