'BBV-P-412:AVH:06/07/2016 RQ19: SE CREA CLASE DE DIVISIONES
'BBVA-P-412: 25-08-2016: AMR: RQCOT-01 Ajustes a la pantalla de Cotización.
'BBV-P-412:BUG-PC-19 24/11/2016 SE CAMBIO LA FORMA COMO SE RELACIONAN LAS AGENCIAS CON LAS DIVISIONES.
'BUG-PC-30 AMR 03/01/2017: Al ingresar al portal procotiza e ingresar a la opcion de cotizacion manda un mensaje de warning.
'RQ-PI7-PC9: CGARCIA: 23/11/2017: SE AGREGO OPCION 12 PARA TRAER EL id_EXTERNO PARA EL WS DE AGENCIAS

Imports SDManejaBD
Public Class clsDivisiones
    Inherits clsSession

    Private intDivision As Integer = -1
    Private strErrDivision As String = ""
    Private strDivision As String = ""
    Private strDescripcion As String = ""
    Private strURL As String = ""
    Private intEstatus As Integer = 0
    Private sngRegDef As Single = 0

    Private intAgencia As Integer = 0
    Private strAgencia As String = ""

    Private intDivisionFiltro As Integer = -1


    Private strUsuReg As String = ""
    Private intidusuario As Integer = 0
    Private intidedo As Integer = 0


    Public Property IDDivisionFiltro() As Integer
        Get
            Return intDivisionFiltro
        End Get
        Set(ByVal value As Integer)
            intDivisionFiltro = value
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

    Public Property Division() As String
        Get
            Return strDivision
        End Get
        Set(ByVal value As String)
            strDivision = value
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
    Public ReadOnly Property ErrorDivision() As String
        Get
            Return strErrDivision
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


    Public Function ManejaDivision(ByVal intOper As Integer) As DataSet
        strErrDivision = ""
        ManejaDivision = New DataSet
        Dim strParamStored As String = ""
        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta Division
                    If intDivision > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idDivision", intDivision.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    If strDivision <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Division", strDivision.ToString)

                Case 2 ' inserta Division
                    ArmaParametros(strParamStored, TipoDato.Entero, "idDivision", intDivision.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Division", strDivision.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Descripcion", strDescripcion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "URL", strURL.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 3 ' actualiza Division
                    ArmaParametros(strParamStored, TipoDato.Entero, "idDivision", intDivision.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Division", strDivision.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Descripcion", strDescripcion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "URL", strURL.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 4 ' borra Division
                    ArmaParametros(strParamStored, TipoDato.Entero, "idDivision", intDivision.ToString)
                Case 7 'Relación Agencia-Alianza --> Consulta
                    If intDivisionFiltro > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idDivisionFiltro", intDivisionFiltro.ToString)
                    If intDivision > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idDivision", intDivision.ToString)
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If strAgencia <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Agencia", strAgencia.ToString)
                Case 8 'Relación Agencia-Alianza --> Borra
                    If intDivisionFiltro > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idDivisionFiltro", intDivisionFiltro.ToString)
                    If intDivision > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idDivision", intDivision.ToString)
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If strAgencia <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Agencia", strAgencia.ToString)
                Case 9 'Relación Agencia-Alianza --> Inserta
                    If intDivisionFiltro > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idDivisionFiltro", intDivisionFiltro.ToString)
                    If intDivision > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idDivision", intDivision.ToString)
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If strAgencia <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Agencia", strAgencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 10 'Cotizador - consulta Grupos
                    ArmaParametros(strParamStored, TipoDato.Entero, "idusuario", intidusuario.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idedo", intidedo.ToString)
                Case 11 'Consulta Cotizaciones - consulta Grupos
                    ArmaParametros(strParamStored, TipoDato.Entero, "idusuario", intidusuario.ToString)
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                Case 12 'consulta el ID_Externo de las divisiones segun el Id de tabla
                    If intDivision > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idDivision", intDivision.ToString)
            End Select

            ManejaDivision = objSD.EjecutaStoredProcedure("spManejaDivisiones", strErrDivision, strParamStored)
            If strErrDivision = "" Then
                If intOper = 2 Then
                    intDivision = ManejaDivision.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("Division", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrDivision = ex.Message
        End Try
    End Function
    Public Sub CargaDivision(Optional ByVal intAse As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intDivision = intAse
            dtsRes = ManejaDivision(1)
            intDivision = 0
            If Trim$(strErrDivision) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intDivision = dtsRes.Tables(0).Rows(0).Item("ID_DIVISION")
                    strDivision = dtsRes.Tables(0).Rows(0).Item("Division").ToString
                    strDescripcion = dtsRes.Tables(0).Rows(0).Item("DESCRIPCION").ToString
                    strURL = dtsRes.Tables(0).Rows(0).Item("URL").ToString
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    sngRegDef = dtsRes.Tables(0).Rows(0).Item("REG_DEFAULT")
                Else
                    strErrDivision = "No se encontró información para poder cargar la Division"
                End If
            End If
        Catch ex As Exception
            strErrDivision = ex.Message
        End Try
    End Sub
End Class
