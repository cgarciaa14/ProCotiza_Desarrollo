' BBV-P-412  RQE  gvargas  06/09/2016 Se agrego en el metodo ManejaObjetoSis la opcion para menu nuevo (6) y menu antiguo (7).

Imports SDManejaBD

Public Class clsObjetosSistema
    Inherits clsSession

    Private strErrObjetoSis As String = ""

    Private intObjeto As Integer = 0
    Private intPadre As Integer = -1
    Private intTipoObj As Integer = 0
    Private intTipoPerm As Integer = 0
    Private intNivel As Integer = 0

    Private strNombre As String = ""
    Private strLink As String = ""
    Private strUsuReg As String = ""
    Private intPerfil As Integer = 0

    Sub New()
    End Sub

    Sub New(ByVal intCveObj As Integer)
        CargaObjetoSis(intCveObj)
    End Sub

    Public ReadOnly Property ErrorObjetos() As String
        Get
            Return strErrObjetoSis
        End Get
    End Property

    Public WriteOnly Property UsuarioRegistro() As String
        Set(ByVal value As String)
            strUsuReg = value
        End Set
    End Property

    Public Property IDObjeto() As Integer
        Get
            Return intObjeto
        End Get
        Set(ByVal value As Integer)
            intObjeto = value
        End Set
    End Property

    Public Property IDObjetoPadre() As Integer
        Get
            Return intPadre
        End Get
        Set(ByVal value As Integer)
            intPadre = value
        End Set
    End Property

    Public Property cveTipoObjeto() As Integer
        Get
            Return intTipoObj
        End Get
        Set(ByVal value As Integer)
            intTipoObj = value
        End Set
    End Property

    Public Property cveTipoPermiso() As Integer
        Get
            Return intTipoPerm
        End Get
        Set(ByVal value As Integer)
            intTipoPerm = value
        End Set
    End Property

    Public Property Nivel() As Integer
        Get
            Return intNivel
        End Get
        Set(ByVal value As Integer)
            intNivel = value
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

    Public Property Link() As String
        Get
            Return strLink
        End Get
        Set(ByVal value As String)
            strLink = value
        End Set
    End Property

    Public Property Perfil() As Integer
        Get
            Return intPerfil
        End Get
        Set(value As Integer)
            intPerfil = value
        End Set
    End Property

    Public Sub CargaObjetoSis(Optional ByVal intObj As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intObjeto = intObj
            dtsRes = ManejaObjetoSis(1)
            intObjeto = 0
            If Trim$(strErrObjetoSis) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intObjeto = dtsRes.Tables(0).Rows(0).Item("ID_OBJETO")
                    intTipoObj = dtsRes.Tables(0).Rows(0).Item("ID_TIPO_OBJ")
                    intPadre = dtsRes.Tables(0).Rows(0).Item("ID_OBJ_PADRE")
                    intTipoPerm = dtsRes.Tables(0).Rows(0).Item("ID_TIPO_PERMISO")
                    intNivel = dtsRes.Tables(0).Rows(0).Item("nivel")
                    strNombre = dtsRes.Tables(0).Rows(0).Item("TEXTO")
                    strLink = dtsRes.Tables(0).Rows(0).Item("LINK")
                Else
                    strErrObjetoSis = "No se encontró información para poder cargar la empresa"
                End If
            End If
        Catch ex As Exception
            strErrObjetoSis = ex.Message
        End Try
    End Sub

    Public Function ManejaObjetoSis(ByVal intOper As Integer) As DataSet
        strErrObjetoSis = ""
        ManejaObjetoSis = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta
                    If intObjeto > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idObjeto", intObjeto.ToString)
                    If intTipoObj > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoObj", intTipoObj.ToString)
                    If intPadre <> -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPadre", intPadre.ToString)
                Case 2 ' inserta
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPadre", intPadre.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoObj", intTipoObj.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoPerm", intTipoPerm.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "nivel", intNivel.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "texto", strNombre)
                    If Trim(strLink) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "link", strLink)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usuReg", strUsuReg)
                Case 3 ' actualiza
                    ArmaParametros(strParamStored, TipoDato.Entero, "idObjeto", intObjeto.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPadre", intPadre.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoObj", intTipoObj.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoPerm", intTipoPerm.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "nivel", intNivel.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "texto", strNombre)
                    If Trim(strLink) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "link", strLink)
                Case 4 ' borra
                    ArmaParametros(strParamStored, TipoDato.Entero, "idObjeto", intObjeto.ToString)
                Case 5 'consulta padres
                    'no requiere paràmetros
                Case 6 'Recupera Menu
                    ArmaParametros(strParamStored, TipoDato.Entero, "Perfil", intPerfil.ToString)
                Case 7 'Recupera Menu Antiguo
                    ArmaParametros(strParamStored, TipoDato.Entero, "Perfil", intPerfil.ToString)
            End Select

            ManejaObjetoSis = objSD.EjecutaStoredProcedure("spManejaObjetosSis", strErrObjetoSis, strParamStored)
            If strErrObjetoSis = "" Then
                If intOper = 2 Then
                    intObjeto = ManejaObjetoSis.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("OBJETOS_SISTEMA", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrObjetoSis = ex.Message
        End Try
    End Function
End Class
