Imports SDManejaBD

Public Class clsFamilias
    Inherits clsSession

    Private strErrFamilias As String = ""

    Private intFamilia As Integer = 0
    Private intEstatus As Integer = 0
    'EOST - 14082013
    'Se agrega el campo Origen para ligarlo con la tabla del mismo nombre
    Private intOrigen As Integer = 0
    Private sngRegDef As Single = 0

    Private strIdExterno As String = ""
    Private strNombre As String = ""
    Private strUsuReg As String = ""

    Sub New()
    End Sub
    Sub New(ByVal intCveFamilia As Integer)
        CargaFamilia(intCveFamilia)
    End Sub

    Public ReadOnly Property ErrorFamilias() As String
        Get
            Return strErrFamilias
        End Get
    End Property

    Public WriteOnly Property UsuarioRegistro() As String
        Set(ByVal value As String)
            strUsuReg = value
        End Set
    End Property

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

    Public Property Nombre() As String
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

    Public Sub CargaFamilia(Optional ByVal intFam As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intFamilia = intFam
            dtsRes = ManejaFamilia(1)
            intFamilia = 0
            If Trim$(strErrFamilias) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intFamilia = dtsRes.Tables(0).Rows(0).Item("ID_FAMILIA")
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    sngRegDef = dtsRes.Tables(0).Rows(0).Item("REG_DEFAULT")
                    strNombre = dtsRes.Tables(0).Rows(0).Item("NOMBRE")
                    intOrigen = dtsRes.Tables(0).Rows(0).Item("ID_ORIGEN")
                    strIdExterno = dtsRes.Tables(0).Rows(0).Item("ID_EXTERNO")
                Else
                    strErrFamilias = "No se encontró información para poder cargar la familia"
                End If
            End If
        Catch ex As Exception
            strErrFamilias = ex.Message
        End Try
    End Sub

    Public Function ManejaFamilia(ByVal intOper As Integer) As DataSet
        strErrFamilias = ""
        ManejaFamilia = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta Familia
                    If intFamilia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idFamilia", intFamilia.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If Trim(strNombre) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    If Trim(strIdExterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "idExterno", strIdExterno)
                Case 2 ' inserta Familia
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ori", intOrigen.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    If Trim(strIdExterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "idExterno", strIdExterno)
                Case 3 ' actualiza Familia
                    ArmaParametros(strParamStored, TipoDato.Entero, "idFamilia", intFamilia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ori", intOrigen.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    If Trim(strIdExterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "idExterno", strIdExterno)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 4 ' borra Familia
                    ArmaParametros(strParamStored, TipoDato.Entero, "idFamilia", intFamilia.ToString)
                Case 5 ' consulta Familia ligada a la tabla de Origen
                    If intFamilia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idFamilia", intFamilia.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If intOrigen > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "ori", intOrigen.ToString)
                Case 6 ' consulta Familia ligada a la tabla de Origen/Familia
                    If intFamilia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idFamilia", intFamilia.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If intOrigen > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "ori", intOrigen.ToString)
            End Select

            ManejaFamilia = objSD.EjecutaStoredProcedure("spManejaFamilias", strErrFamilias, strParamStored)
            If strErrFamilias = "" Then
                If intOper = 2 Then
                    intFamilia = ManejaFamilia.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("FAMILIAS", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrFamilias = ex.Message
        End Try
    End Function
End Class
