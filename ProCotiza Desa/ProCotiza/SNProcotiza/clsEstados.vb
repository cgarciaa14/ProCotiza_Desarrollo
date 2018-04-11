'BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.
'BBVA-P-412: 25-08-2016: AMR: RQCOT-01 Ajustes a la pantalla de Cotización.

Imports SDManejaBD

Public Class clsEstados
    Inherits clsSession

    Private strErrEstado As String = ""

    Private intEstado As Integer = 0
    Private intEstatus As Integer = 0

    Private strIdExterno As String = ""
    Private strNombre As String = ""
    Private strClave As String = ""
    Private strUsuReg As String = ""

    Private intIdProducto As Integer = 0
    Private intIdTipoProd As Integer = 0
    Private intIdTipoOper As Integer = 0
    Private intIdTipoPer As Integer = 0
    Private intIdAgencia As Integer = 0
    Private intidusuario As Integer = 0


    Sub New()
    End Sub
    Sub New(ByVal intCveEstado As Integer)
        CargaEstado(intCveEstado)
    End Sub

    Public ReadOnly Property ErrorEstados() As String
        Get
            Return strErrEstado
        End Get
    End Property

    Public WriteOnly Property UsuarioRegistro() As String
        Set(ByVal value As String)
            strUsuReg = value
        End Set
    End Property

    Public Property IDEstado() As Integer
        Get
            Return intEstado
        End Get
        Set(ByVal value As Integer)
            intEstado = value
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

    Public Property Nombre() As String
        Get
            Return strNombre
        End Get
        Set(ByVal value As String)
            strNombre = value
        End Set
    End Property

    Public Property Clave() As String
        Get
            Return strClave
        End Get
        Set(ByVal value As String)
            strClave = value
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

    Public Property IDProducto() As Integer
        Get
            Return intIdProducto
        End Get
        Set(value As Integer)
            intIdProducto = value
        End Set
    End Property

    Public Property IDTipoProd() As Integer
        Get
            Return intIdTipoProd
        End Get
        Set(value As Integer)
            intIdTipoProd = value
        End Set
    End Property

    Public Property IDTipoOper() As Integer
        Get
            Return intIdTipoOper
        End Get
        Set(value As Integer)
            intIdTipoOper = value
        End Set
    End Property

    Public Property IDTipoPer() As Integer
        Get
            Return intIdTipoPer
        End Get
        Set(value As Integer)
            intIdTipoPer = value
        End Set
    End Property

    Public Property IDAgencia() As Integer
        Get
            Return intIdAgencia
        End Get
        Set(value As Integer)
            intIdAgencia = value
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


    Public Sub CargaEstado(Optional ByVal intEdo As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intEstado = intEdo
            dtsRes = ManejaEstado(1)
            intEstado = 0
            If Trim$(strErrEstado) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intEstado = dtsRes.Tables(0).Rows(0).Item("ID_ESTADO")
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    strClave = dtsRes.Tables(0).Rows(0).Item("CLAVE")
                    strNombre = dtsRes.Tables(0).Rows(0).Item("NOMBRE")
                    strIdExterno = dtsRes.Tables(0).Rows(0).Item("ID_EXTERNO")
                Else
                    strErrEstado = "No se encontró información para poder cargar el estado"
                End If
            End If
        Catch ex As Exception
            strErrEstado = ex.Message
        End Try
    End Sub

    Public Function ManejaEstado(ByVal intOper As Integer) As DataSet
        strErrEstado = ""
        ManejaEstado = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta
                    If intEstado > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idEstado", intEstado.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If Trim(strNombre) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    If Trim(strIdExterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "idExterno", strIdExterno)
                Case 2 ' inserta
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "clave", strClave)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    If Trim(strIdExterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "idExterno", strIdExterno)
                Case 3 ' actualiza
                    ArmaParametros(strParamStored, TipoDato.Entero, "idEstado", intEstado.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "clave", strClave)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    If Trim(strIdExterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "idExterno", strIdExterno)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 4 ' borra
                    ArmaParametros(strParamStored, TipoDato.Entero, "idEstado", intEstado.ToString)
                Case 5 ' Consulta Cotizador
                    If intIdAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intIdAgencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idusuario", intidusuario.ToString)
            End Select

            ManejaEstado = objSD.EjecutaStoredProcedure("spManejaEstados", strErrEstado, strParamStored)
            If strErrEstado = "" Then
                If intOper = 2 Then
                    intEstado = ManejaEstado.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("ESTADOS", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrEstado = ex.Message
        End Try
    End Function
End Class
