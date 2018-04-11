Imports SDManejaBD

Public Class clsTipoProductos
    Inherits clsSession

    Private strErrTipoProd As String = ""

    Private intTipoProd As Integer = 0
    Private intEstatus As Integer = 0
    Private sngRegDef As Single = 0
    Private sngReqValorAdap As Single = 0
    Private sngIncluyeRC As Single = 0

    Private strIdExterno As String = ""
    Private strNombre As String = ""
    Private strUsuReg As String = ""

    Sub New()
    End Sub
    Sub New(ByVal intCveTipoProducto As Integer)
        CargaTipoProducto(intCveTipoProducto)
    End Sub

    Public ReadOnly Property ErrorTipoProducto() As String
        Get
            Return strErrTipoProd
        End Get
    End Property

    Public WriteOnly Property UsuarioRegistro() As String
        Set(ByVal value As String)
            strUsuReg = value
        End Set
    End Property

    Public Property IDTipoProducto() As Integer
        Get
            Return intTipoProd
        End Get
        Set(ByVal value As Integer)
            intTipoProd = value
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

    Public Property RequiereValorAdaptacion() As Single
        Get
            Return sngReqValorAdap
        End Get
        Set(ByVal value As Single)
            sngReqValorAdap = value
        End Set
    End Property

    Public Property IncluyeRC() As Single
        Get
            Return sngIncluyeRC
        End Get
        Set(ByVal value As Single)
            sngIncluyeRC = value
        End Set
    End Property

    Public Property Descripcion() As String
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

    Public Sub CargaTipoProducto(Optional ByVal intTipo As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intTipoProd = intTipo
            dtsRes = ManejaTipoProd(1)
            intTipoProd = 0
            If Trim$(strErrTipoProd) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intTipoProd = dtsRes.Tables(0).Rows(0).Item("ID_TIPO_PRODUCTO")
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    sngRegDef = dtsRes.Tables(0).Rows(0).Item("REG_DEFAULT")
                    sngReqValorAdap = dtsRes.Tables(0).Rows(0).Item("REQUIERE_VALOR_ADAPTACION")
                    sngIncluyeRC = dtsRes.Tables(0).Rows(0).Item("INCLUYE_RC")
                    strNombre = dtsRes.Tables(0).Rows(0).Item("DESCRIPCION")
                    strIdExterno = dtsRes.Tables(0).Rows(0).Item("ID_EXTERNO")
                Else
                    strErrTipoProd = "No se encontró información para poder cargar el tipo de producto"
                End If
            End If
        Catch ex As Exception
            strErrTipoProd = ex.Message
        End Try
    End Sub

    Public Function ManejaTipoProd(ByVal intOper As Integer) As DataSet
        strErrTipoProd = ""
        ManejaTipoProd = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta tipo producto
                    If intTipoProd > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoProd", intTipoProd.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If Trim(strNombre) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "descrip", strNombre)
                    If Trim(strIdExterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "idExterno", strIdExterno)
                Case 2 ' inserta tipo producto
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "reqValorAdap", sngReqValorAdap.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "incluyeRC", sngIncluyeRC.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "descrip", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    If Trim(strIdExterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "idExterno", strIdExterno)
                Case 3 ' actualiza tipo producto
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoProd", intTipoProd.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "reqValorAdap", sngReqValorAdap.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "incluyeRC", sngIncluyeRC.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "descrip", strNombre)
                    If Trim(strIdExterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "idExterno", strIdExterno)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 4 ' borra tipo producto
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoProd", intTipoProd.ToString)
            End Select

            ManejaTipoProd = objSD.EjecutaStoredProcedure("spManejaTiposProducto", strErrTipoProd, strParamStored)
            If strErrTipoProd = "" Then
                If intOper = 2 Then
                    intTipoProd = ManejaTipoProd.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("TIPOS_PRODUCTO", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrTipoProd = ex.Message
        End Try
    End Function
End Class
