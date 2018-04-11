'BBV-P-412:AVH:04/07/2016 RQ17: SE CREA CATALOGO DE GRUPOS
'BBVA-P-412: 25-08-2016: AMR: RQCOT-01 Ajustes a la pantalla de Cotización.
'BUG-PC-30 AMR 03/01/2017: Al ingresar al portal procotiza e ingresar a la opcion de cotizacion manda un mensaje de warning.
'BUG-PC-44 07/02/17 JRHM SE AGREGO VALOR DE GRUPO FILTRO -1 PARA SOLUCIONAR PROBLEMAS DE ASIGNACION

Imports SDManejaBD
Public Class clsGrupos
    Inherits clsSession

    Private intGrupo As Integer = -1
    Private strErrGrupo As String = ""
    Private strGrupo As String = ""
    Private strDescripcion As String = ""
    Private strURL As String = ""
    Private intEstatus As Integer = 0
    Private sngRegDef As Single = 0

    Private intAgencia As Integer = 0
    Private strAgencia As String = ""

    Private intGrupoFiltro As Integer = -1


    Private strUsuReg As String = ""
    Private intidusuario As Integer = 0
    Private intidedo As Integer = 0

Public Property IDGrupoFiltro() As Integer
        Get
            Return intGrupoFiltro
        End Get
        Set(ByVal value As Integer)
            intGrupoFiltro = value
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

    Public Property Grupo() As String
        Get
            Return strGrupo
        End Get
        Set(ByVal value As String)
            strGrupo = value
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
    Public Property Agencia() As String
        Get
            Return strAgencia
        End Get
        Set(ByVal value As String)
            strAgencia = value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Return strDescripcion
        End Get
        Set(ByVal value As String)
            strDescripcion = value
        End Set
    End Property

    Public Property URL As String
        Get
            Return strURL
        End Get
        Set(ByVal value As String)
            strURL = value
        End Set
    End Property

    Public Property Estatus As Integer
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

    Public ReadOnly Property ErrorGrupo() As String
        Get
            Return strErrGrupo
        End Get
    End Property

    Public WriteOnly Property UsuarioRegistro() As String
        Set(ByVal value As String)
            strUsuReg = value
        End Set
    End Property


    'Buscar
    Public Property IDEstatus() As Integer
        Get
            Return intEstatus
        End Get
        Set(ByVal value As Integer)
            intEstatus = value
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

    Public Property IDEdo As Integer
        Get
            Return intidedo
        End Get
        Set(value As Integer)
            intidedo = value
        End Set
    End Property


    Public Function ManejaGrupo(ByVal intOper As Integer) As DataSet
        strErrGrupo = ""
        ManejaGrupo = New DataSet
        Dim strParamStored As String = ""
        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta Grupo
                    If intGrupo > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idGrupo", intGrupo.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    If strGrupo <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Grupo", strGrupo.ToString)

                Case 2 ' inserta Grupo
                    ArmaParametros(strParamStored, TipoDato.Entero, "idGrupo", intGrupo.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Grupo", strGrupo.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Descripcion", strDescripcion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "URL", strURL.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 3 ' actualiza Grupo
                    ArmaParametros(strParamStored, TipoDato.Entero, "idGrupo", intGrupo.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Grupo", strGrupo.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Descripcion", strDescripcion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "URL", strURL.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 4 ' borra Grupo
                    ArmaParametros(strParamStored, TipoDato.Entero, "idGrupo", intGrupo.ToString)
                Case 7 'Relación Agencia-Alianza --> Consulta
                    If intGrupoFiltro > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idGrupoFiltro", intGrupoFiltro.ToString)
                    If intGrupo > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idGrupo", intGrupo.ToString)
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If strAgencia <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Agencia", strAgencia.ToString)
                Case 8 'Relación Agencia-Alianza --> Borra
                    If intGrupoFiltro > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idGrupoFiltro", intGrupoFiltro.ToString)
                    If intGrupo > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idGrupo", intGrupo.ToString)
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If strAgencia <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Agencia", strAgencia.ToString)
                Case 9 'Relación Agencia-Alianza --> Inserta
                    If intGrupoFiltro > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idGrupoFiltro", intGrupoFiltro.ToString)
                    If intGrupo > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idGrupo", intGrupo.ToString)
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If strAgencia <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Agencia", strAgencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 10 'Cotizador - consulta Grupos
                    ArmaParametros(strParamStored, TipoDato.Entero, "idusuario", intidusuario.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idedo", intidedo.ToString)
                Case 11 'ConsultaCotizaciones - consulta Grupos
                    ArmaParametros(strParamStored, TipoDato.Entero, "idusuario", intidusuario.ToString)
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
            End Select

            ManejaGrupo = objSD.EjecutaStoredProcedure("spManejaGrupos", strErrGrupo, strParamStored)
            If strErrGrupo = "" Then
                If intOper = 2 Then
                    intGrupo = ManejaGrupo.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("Grupo", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrGrupo = ex.Message
        End Try
    End Function
    Public Sub CargaGrupo(Optional ByVal intAse As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intGrupo = intAse
            dtsRes = ManejaGrupo(1)
            intGrupo = 0
            If Trim$(strErrGrupo) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intGrupo = dtsRes.Tables(0).Rows(0).Item("ID_Grupo")
                    strGrupo = dtsRes.Tables(0).Rows(0).Item("Grupo").ToString
                    strDescripcion = dtsRes.Tables(0).Rows(0).Item("DESCRIPCION").ToString
                    strURL = dtsRes.Tables(0).Rows(0).Item("URL").ToString
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    sngRegDef = dtsRes.Tables(0).Rows(0).Item("REG_DEFAULT")
                Else
                    strErrGrupo = "No se encontró información para poder cargar la Grupo"
                End If
            End If
        Catch ex As Exception
            strErrGrupo = ex.Message
        End Try
    End Sub
End Class
