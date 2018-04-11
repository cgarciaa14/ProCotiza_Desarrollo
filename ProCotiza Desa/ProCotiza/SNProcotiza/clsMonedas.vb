Imports SDManejaBD

Public Class clsMonedas
    Inherits clsSession

    Private strErrMoneda As String = ""

    Private intMoneda As Integer = 0
    Private intEstatus As Integer = 0
    Private sngRegDef As Single = 0

    Private dblValorCambio As Double = 0

    Private strIdExterno As String = ""
    Private strNombre As String = ""
    Private strUsuReg As String = ""

    Sub New()
    End Sub
    Sub New(ByVal intCveMoneda As Integer)
        CargaMoneda(intCveMoneda)
    End Sub

    Public ReadOnly Property ErrorMoneda() As String
        Get
            Return strErrMoneda
        End Get
    End Property

    Public WriteOnly Property UsuarioRegistro() As String
        Set(ByVal value As String)
            strUsuReg = value
        End Set
    End Property

    Public Property IDMoneda() As Integer
        Get
            Return intMoneda
        End Get
        Set(ByVal value As Integer)
            intMoneda = value
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

    Public Property EsMonedaBase() As Single
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

    Public Property ValorCambio() As Double
        Get
            Return dblValorCambio
        End Get
        Set(ByVal value As Double)
            dblValorCambio = value
        End Set
    End Property

    Public Sub CargaMoneda(Optional ByVal intMon As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intMoneda = intMon
            dtsRes = ManejaMoneda(1)
            intMoneda = 0
            If Trim$(strErrMoneda) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intMoneda = dtsRes.Tables(0).Rows(0).Item("ID_MONEDA")
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    sngRegDef = dtsRes.Tables(0).Rows(0).Item("MONEDA_BASE")
                    strNombre = dtsRes.Tables(0).Rows(0).Item("NOMBRE")
                    strIdExterno = dtsRes.Tables(0).Rows(0).Item("ID_EXTERNO")
                    dblValorCambio = dtsRes.Tables(0).Rows(0).Item("VALOR_CAMBIO")
                Else
                    strErrMoneda = "No se encontró información para poder cargar la moneda"
                End If
            End If
        Catch ex As Exception
            strErrMoneda = ex.Message
        End Try
    End Sub

    Public Function ManejaMoneda(ByVal intOper As Integer) As DataSet
        strErrMoneda = ""
        ManejaMoneda = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta 
                    If intMoneda > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMoneda", intMoneda.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If Trim(strNombre) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    If Trim(strIdExterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "idExterno", strIdExterno)
                Case 2 ' inserta 
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "monedaBase", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Doble, "valorCambio", dblValorCambio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    If Trim(strIdExterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "idExterno", strIdExterno)
                Case 3 ' actualiza 
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMoneda", intMoneda.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "monedaBase", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    If Trim(strIdExterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "idExterno", strIdExterno)
                    ArmaParametros(strParamStored, TipoDato.Doble, "valorCambio", dblValorCambio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 4 ' borra 
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMoneda", intMoneda.ToString)
                Case 5 ' consulta moneda mas moneda base (para cotizaciones navistar)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMoneda", intMoneda.ToString)
            End Select

            ManejaMoneda = objSD.EjecutaStoredProcedure("spManejaMonedas", strErrMoneda, strParamStored)
            If strErrMoneda = "" Then
                If intOper = 2 Then
                    intMoneda = ManejaMoneda.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("MONEDAS", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrMoneda = ex.Message
        End Try
    End Function
End Class
