'BUG-PC-13: AMR: 23/11/2016 Se quita valor por default en los combos.

Imports SDManejaBD

Public Class clsBrokerSeguros
    Inherits clsSession

    Private strErrBroker As String = ""

    Private intidbroker As Integer = 0
    Private strrazonsocial As String = String.Empty
    Private strnomcorto As String = String.Empty
    Private dblconstanteprima As Double = 0
    Private intRegDefault As Integer = 0
    Private intidexterno As Integer = 0
    Private intestatus As Integer = 0
    Private strlink As String = String.Empty
    Private strusureg As String = String.Empty
    Private intAseg As Integer = 0
    Private strNomAseg As String = String.Empty
    Private intTipoCalc As Integer = 0
    Private strVia As String = String.Empty

    Sub New()
    End Sub

    Sub New(ByVal intCveBroker As Integer)
        CargaBroker(intCveBroker)
    End Sub

    Public ReadOnly Property ErrorBroker() As String
        Get
            Return strErrBroker
        End Get
    End Property

    Public Property IDBroker() As Integer
        Get
            Return intidbroker
        End Get
        Set(value As Integer)
            intidbroker = value
        End Set
    End Property

    Public Property RazonSocial() As String
        Get
            Return strrazonsocial
        End Get
        Set(value As String)
            strrazonsocial = value
        End Set
    End Property

    Public Property NomCorto() As String
        Get
            Return strnomcorto
        End Get
        Set(value As String)
            strnomcorto = value
        End Set
    End Property

    Public Property ConstantePrima() As Double
        Get
            Return dblconstanteprima
        End Get
        Set(value As Double)
            dblconstanteprima = value
        End Set
    End Property

    Public Property RegistroDefault() As Integer
        Get
            Return intRegDefault
        End Get
        Set(value As Integer)
            intRegDefault = value
        End Set
    End Property

    Public Property IDExterno() As Integer
        Get
            Return intidexterno
        End Get
        Set(value As Integer)
            intidexterno = value
        End Set
    End Property

    Public Property IDEstatus() As Integer
        Get
            Return intestatus
        End Get
        Set(value As Integer)
            intestatus = value
        End Set
    End Property

    Public Property Link() As String
        Get
            Return strlink
        End Get
        Set(value As String)
            strlink = value
        End Set
    End Property

    Public Property UsuarioRegistro() As String
        Get
            Return strusureg
        End Get
        Set(ByVal value As String)
            strusureg = value
        End Set
    End Property

    Public Property ASeguradora() As Integer
        Get
            Return intAseg
        End Get
        Set(value As Integer)
            intAseg = value
        End Set
    End Property

    Public Property NomAseg() As String
        Get
            Return strNomAseg
        End Get
        Set(value As String)
            strNomAseg = value
        End Set
    End Property

    Public Property TipoCalc As Integer
        Get
            Return intTipoCalc
        End Get
        Set(value As Integer)
            intTipoCalc = value
        End Set
    End Property

    Public Property Via As String
        Get
            Return strVia
        End Get
        Set(value As String)
            strVia = value
        End Set
    End Property

    Public Sub CargaBroker(Optional ByVal intbroker As Integer = 0)
        Dim dtsRes As New DataSet
        Try
            intidbroker = intbroker
            dtsRes = ManejaBroker(1)
            intidbroker = 0

            If strErrBroker.Trim = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intidbroker = dtsRes.Tables(0).Rows(0).Item("ID_BROKER")
                    intestatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    strlink = dtsRes.Tables(0).Rows(0).Item("LINK")
                    intRegDefault = dtsRes.Tables(0).Rows(0).Item("REG_DEFAULT")
                    strrazonsocial = dtsRes.Tables(0).Rows(0).Item("RAZON_SOCIAL")
                    strnomcorto = dtsRes.Tables(0).Rows(0).Item("NOM_CORTO")
                    intidexterno = dtsRes.Tables(0).Rows(0).Item("ID_EXTERNO")
                    dblconstanteprima = dtsRes.Tables(0).Rows(0).Item("CONSTANTE_PRIMA")
                    intTipoCalc = dtsRes.Tables(0).Rows(0).Item("ID_TIPO_CALCULO_SEGURO")
                    strVia = dtsRes.Tables(0).Rows(0).Item("VIA")
                Else
                    strErrBroker = "No se encontro información para poder cargar el Broker"
                End If
            End If

        Catch ex As Exception
            strErrBroker = ex.Message
        End Try
    End Sub

    Public Function ManejaBroker(ByVal intOper As Integer) As DataSet
        strErrBroker = ""
        ManejaBroker = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim ObjCon As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString())

            Select Case intOper
                Case 1 'Consulta
                    If intidbroker > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idbroker", intidbroker.ToString())
                    If intestatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intestatus.ToString())
                    If strrazonsocial.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "razonsocial", strrazonsocial)
                    If strnomcorto.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nomcorto ", strnomcorto)
                    If intidexterno > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "idexterno", intidexterno.ToString)
                Case 2 'Inserta
                    If strrazonsocial.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "razonsocial", strrazonsocial)
                    If strnomcorto.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nomcorto ", strnomcorto)
                    If dblconstanteprima > 0 Then ArmaParametros(strParamStored, TipoDato.Doble, "prima ", dblconstanteprima)
                    ArmaParametros(strParamStored, TipoDato.Entero, "RegDefault ", intRegDefault)
                    If intidexterno > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "idexterno", intidexterno.ToString)
                    If intestatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intestatus.ToString())
                    If strlink.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "link", strlink)
                    If strusureg.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "usureg ", strusureg)
                    If intTipoCalc > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "tipocalc", intTipoCalc.ToString)
                Case 3 'Actualiza
                    If intidbroker > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idbroker", intidbroker.ToString())
                    If strrazonsocial.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "razonsocial", strrazonsocial)
                    If strnomcorto.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nomcorto ", strnomcorto)
                    If dblconstanteprima > 0 Then ArmaParametros(strParamStored, TipoDato.Doble, "prima ", dblconstanteprima)
                    ArmaParametros(strParamStored, TipoDato.Entero, "RegDefault ", intRegDefault)
                    If intidexterno > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "idexterno", intidexterno.ToString)
                    If intestatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intestatus.ToString())
                    If strlink.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "link", strlink)
                    If strusureg.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "usureg ", strusureg)
                    If intTipoCalc > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "tipocalc", intTipoCalc.ToString)
                Case 4 'Borra
                    If intidbroker > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idbroker", intidbroker.ToString())
                Case 5 'Consulta Asigna Aseguradoras
                    If intidbroker > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idbroker", intidbroker.ToString())
                    If intestatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intestatus.ToString())
                    If intAseg > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idaseg", intAseg.ToString())
                    If strNomAseg.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nomaseg ", strNomAseg)
            End Select

            ManejaBroker = ObjCon.EjecutaStoredProcedure("spManejaBroker", strErrBroker, strParamStored)
            If strErrBroker = "" Then
                If intOper = 2 Then
                    intidbroker = ManejaBroker.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("BROKERS", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If
        Catch ex As Exception
            strErrBroker = ex.Message
        End Try
    End Function
End Class
