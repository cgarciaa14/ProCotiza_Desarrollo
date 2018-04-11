'BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.
'BUG-PC-154: CGARCIA: 16/02/2018: SE AGREGA OPCION 8 EN MANEJA_PARAMETROS PARA CARGAR PARAMETROS DE COTIZACION DE DAÑOS
Imports SDManejaBD

Public Class clsParametros
    Private strErrParam As String = ""

    Private intParam As Integer = -1
    Private intPadre As Integer = -1
    Private intOrden As Integer = 0
    Private sngRegDef As Single = 0
    Private strDescrip As String = ""
    Private strValor As String = ""
    Private strUsuReg As String = ""
    Private intPaquete As Integer = 0
    Private intIdAgencia As Integer = 0


    Sub New()
    End Sub

    Sub New(ByVal intCveParam As Integer)
        CargaParametro(intCveParam)
    End Sub

    Public ReadOnly Property ErrorParametros() As String
        Get
            Return strErrParam
        End Get
    End Property

    Public WriteOnly Property UsuarioRegistro() As String
        Set(ByVal value As String)
            strUsuReg = value
        End Set
    End Property

    Public Property IDParametro() As Integer
        Get
            Return intParam
        End Get
        Set(ByVal value As Integer)
            intParam = value
        End Set
    End Property

    Public Property IDPadre() As Integer
        Get
            Return intPadre
        End Get
        Set(ByVal value As Integer)
            intPadre = value
        End Set
    End Property

    Public Property OrdenarXValor() As Boolean
        Get
            Return IIf(intOrden = 1, True, False)
        End Get
        Set(ByVal value As Boolean)
            intOrden = IIf(value, 1, 0)
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Return strDescrip
        End Get
        Set(ByVal value As String)
            strDescrip = value
        End Set
    End Property

    Public Property Valor() As String
        Get
            Return strValor
        End Get
        Set(ByVal value As String)
            strValor = value
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

    Public Property IDPaquete As Integer
        Get
            Return intPaquete
        End Get
        Set(value As Integer)
            intPaquete = value
        End Set
    End Property

    Public Property IDAgencia As Integer
        Get
            Return intIdAgencia
        End Get
        Set(value As Integer)
            intIdAgencia = value
        End Set
    End Property

    Public Sub CargaParametro(ByVal intPar As Integer)
        Dim dtsParam As New DataSet
        Try

            intParam = intPar
            dtsParam = ManejaParametro(1)
            intParam = 0
            If Trim$(strErrParam) = "" Then
                If dtsParam.Tables(0).Rows.Count > 0 Then
                    intParam = dtsParam.Tables(0).Rows(0).Item("ID_PARAMETRO")
                    intPadre = dtsParam.Tables(0).Rows(0).Item("ID_PADRE")
                    strDescrip = dtsParam.Tables(0).Rows(0).Item("TEXTO")
                    strValor = dtsParam.Tables(0).Rows(0).Item("VALOR")
                    sngRegDef = dtsParam.Tables(0).Rows(0).Item("REG_DEFAULT")
                Else
                    strErrParam = "No se encontró información para poder cargar el parámetro"
                End If
            End If
        Catch ex As Exception
            strErrParam = ex.Message
        End Try
    End Sub

    Public Function ManejaParametro(ByVal intOper As Integer) As DataSet
        strErrParam = ""
        ManejaParametro = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta parametro
                    ArmaParametros(strParamStored, TipoDato.Entero, "idParam", intParam.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPadre", intPadre.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "order", intOrden.ToString)
                    If Trim$(strDescrip) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "descrip", strDescrip)
                    If Trim$(strValor) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "valor", strValor)
                Case 2 ' inserta parametro
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPadre", intPadre.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "descrip", strDescrip)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "valor", strValor)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 3 ' actualiza parametro
                    ArmaParametros(strParamStored, TipoDato.Entero, "idParam", intParam.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPadre", intPadre.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "descrip", strDescrip)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "valor", strValor)
                Case 4 ' borra parametro
                    ArmaParametros(strParamStored, TipoDato.Entero, "idParam", intParam.ToString)
                Case 5 'Consulta Personalidad Cotizador
                    If IDPaquete > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "id_paquete", IDPaquete.ToString)
                Case 6 'SEG REG
                    If intParam > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idParam", intParam.ToString)
                    If intPaquete > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "id_paquete", intPaquete.ToString)
                Case 8
                    ArmaParametros(strParamStored, TipoDato.Cadena, "valor", strValor)

            End Select

            ManejaParametro = objSD.EjecutaStoredProcedure("spManejaParametros", strErrParam, strParamStored)
            If strErrParam = "" Then
                If intOper = 2 Then
                    intParam = ManejaParametro.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    'GuardaLog("PARAMETROS_SISTEMA", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrParam = ex.Message
        End Try
    End Function
End Class
