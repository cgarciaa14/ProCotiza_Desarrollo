Imports SDManejaBD

Public Class clsPermisos
    Inherits clsSession

    Private strErrPermisos As String = ""

    Private intPermiso As Integer = 0
    Private intObjeto As Integer = 0
    Private intUsuario As Integer = 0
    Private intPerfil As Integer = 0
    Private intEstatus As Integer = 0

    Private strUsuReg As String = ""
    Private strCvesPermisos As String = ""

    Sub New()
    End Sub

    Sub New(ByVal intCvePerm As Integer, _
            Optional ByVal intCveObj As Integer = 0, _
            Optional ByVal intCveUsu As Integer = 0, _
            Optional ByVal intCvePerf As Integer = 0)
        CargaPermiso(intCvePerm, intCveObj, intCveUsu, intCvePerf)
    End Sub

    Public ReadOnly Property ErrorPermisos() As String
        Get
            Return strErrPermisos
        End Get
    End Property

    Public ReadOnly Property ClavesPermisos() As String
        Get
            Return strCvesPermisos
        End Get
    End Property

    Public WriteOnly Property UsuarioRegistro() As String
        Set(ByVal value As String)
            strUsuReg = value
        End Set
    End Property

    Public Property IDPermiso() As Integer
        Get
            Return intPermiso
        End Get
        Set(ByVal value As Integer)
            intPermiso = value
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

    Public Property IDUsuario() As Integer
        Get
            Return intUsuario
        End Get
        Set(ByVal value As Integer)
            intUsuario = value
        End Set
    End Property

    Public Property IDPeril() As Integer
        Get
            Return intPerfil
        End Get
        Set(ByVal value As Integer)
            intPerfil = value
        End Set
    End Property

    Public Property cveEstatus() As Integer
        Get
            Return intEstatus
        End Get
        Set(ByVal value As Integer)
            intEstatus = value
        End Set
    End Property

    Public Sub CargaPermiso(Optional ByVal intPerm As Integer = 0, _
                            Optional ByVal intObj As Integer = 0, _
                            Optional ByVal intUsu As Integer = 0, _
                            Optional ByVal intPerf As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intPermiso = intPerm
            intObjeto = intObj
            intUsuario = intUsu
            intPerfil = intPerf
            dtsRes = ManejaPermisos(1)
            intPermiso = 0
            intObjeto = 0
            intUsuario = 0
            intPerfil = 0
            If Trim$(strErrPermisos) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intPermiso = dtsRes.Tables(0).Rows(0).Item("ID_PERMISO")
                    intObjeto = dtsRes.Tables(0).Rows(0).Item("ID_OBJETO")
                    intUsuario = dtsRes.Tables(0).Rows(0).Item("ID_USUARIO")
                    intPerfil = dtsRes.Tables(0).Rows(0).Item("ID_PERFIL")
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                Else
                    strErrPermisos = "No se encontró información para poder cargar permiso"
                End If
            End If
        Catch ex As Exception
            strErrPermisos = ex.Message
        End Try
    End Sub

    Public Function ManejaPermisos(ByVal intOper As Integer) As DataSet
        strErrPermisos = ""
        ManejaPermisos = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta
                    If intPermiso > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPermiso", intPermiso.ToString)
                    If intObjeto > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idObjeto", intObjeto.ToString)
                    If intUsuario > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idUsuario", intUsuario.ToString)
                    If intPerfil > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPerfil", intPerfil.ToString)
                Case 2 ' inserta
                    ArmaParametros(strParamStored, TipoDato.Entero, "idObjeto", intObjeto.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intEstatus.ToString)
                    If intUsuario > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idUsuario", intUsuario.ToString)
                    If intPerfil > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPerfil", intPerfil.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usuReg", strUsuReg)
                Case 3 ' actualiza
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPermiso", intPermiso.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idObjeto", intObjeto.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intEstatus.ToString)
                    If intUsuario > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idUsuario", intUsuario.ToString)
                    If intPerfil > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPerfil", intPerfil.ToString)
                Case 4 ' borra
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPermiso", intPermiso.ToString)
            End Select

            ManejaPermisos = objSD.EjecutaStoredProcedure("spManejaPermisosSis", strErrPermisos, strParamStored)
            If strErrPermisos = "" Then
                If intOper = 2 Then
                    intPermiso = ManejaPermisos.Tables(0).Rows(0).Item(0)
                End If
            End If

        Catch ex As Exception
            strErrPermisos = ex.Message
        End Try
    End Function

    Public Function CreaEstructuraPermisos(Optional ByVal intMenu As Integer = 0, _
                                           Optional ByVal blnEsPerfil As Boolean = False) As DataSet
        CreaEstructuraPermisos = New DataSet
        Dim objTabla As New DataTable
        Dim objCol As New DataColumn
        Dim intReg As Integer = 0

        Try
            strCvesPermisos = ""

            'agregamos la primer columna
            objCol.DataType = GetType(String)
            objCol.ColumnName = IIf(blnEsPerfil, "Perfil", "Usuario")
            objTabla.Columns.Add(objCol)

            'cargamos la estructura
            CargaColumnasPermisos(0, objTabla, intMenu)

            If objTabla.Columns.Count > 1 Then
                'cargamos la tabla al dataset
                CreaEstructuraPermisos.Tables.Add(objTabla)
            Else
                strErrPermisos = "No se encontraron objetos de sistema registrados"
            End If
        Catch ex As Exception
            strErrPermisos = ex.Message
        End Try
    End Function

    Private Sub CargaColumnasPermisos(ByVal intPadre As Integer, _
                                      ByRef objTabla As DataTable, _
                                      Optional ByVal intMenu As Integer = 0)
        Dim objCol As New DataColumn
        Dim intReg As Integer = 0
        Dim dtsPaso As New DataSet

        Try
            'cargamos los hijos
            Dim objColPerm As New clsObjetosSistema
            objColPerm.IDObjetoPadre = intPadre
            If intMenu > 0 Then
                objColPerm.IDObjeto = intMenu
            End If

            dtsPaso = objColPerm.ManejaObjetoSis(1)
            If Trim(objColPerm.ErrorObjetos) = "" Then
                If dtsPaso.Tables.Count > 0 Then
                    If dtsPaso.Tables(0).Rows.Count > 0 Then
                        For intReg = 0 To dtsPaso.Tables(0).Rows.Count - 1
                            'agregamos las columnas
                            objCol = New DataColumn
                            objCol.DataType = GetType(String)
                            objCol.ColumnName = dtsPaso.Tables(0).Rows(intReg).Item("TEXTO")
                            intPadre = dtsPaso.Tables(0).Rows(intReg).Item("ID_OBJETO")
                            strCvesPermisos += IIf(Trim(strCvesPermisos) = "", CStr(intPadre), "," & CStr(intPadre))
                            objTabla.Columns.Add(objCol)

                            CargaColumnasPermisos(intPadre, objTabla)
                            If strErrPermisos <> "" Then Exit Sub
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            strErrPermisos = ex.Message
        End Try
    End Sub
End Class
