'BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.
'RQ-MN2-6:RHERNANDEZ: 07/09/17: Se agrega valor de id_broker para que se consulten los productos correspondientes al broker
'RQ-PI7-PC1:RHERNANDEZ: 12/10/17: Se agregan opciones 11, 12 y 13 para la nueva cascada del cotizador
Imports SDManejaBD

Public Class clsProductos
    Inherits clsSession

    Private strErrProducto As String = ""

    Private intProducto As Integer = 0
    Private intMarca As Integer = 0
    Private intTipoProd As Integer = 0
    Private intEstatus As Integer = 0
    Private intClasif As Integer = 0
    Private intAnioModelo As Integer = 0
    Private intFamilia As Integer = 0

    Private sngRegDef As Single = 0

    Private dblPrecio As Double = 0
    Private strNombre As String = ""
    Private strImagen As String = ""
    Private strIdExterno As String = ""
    Private strUsuReg As String = ""
    Private intIdTipoOper As Integer = 0
    Private intIdTipoPer As Integer = 0
    Private intIdAgencia As Integer = 0
    'BBVA-P-412
    Private intidsubmarca As Integer = 0
    Private intidversion As Integer = 0
    Private intidanio As Integer = 0
    Private dblpreciosem As Double = 0
    Private intidbroker As Integer = 0

    Sub New()
    End Sub
    Sub New(ByVal intCveProd As Integer)
        CargaProducto(intCveProd)
    End Sub

    Public ReadOnly Property ErrorProducto() As String
        Get
            Return strErrProducto
        End Get
    End Property

    Public Property UsuarioRegistro() As String
        Get
            Return strUsuReg
        End Get
        Set(ByVal value As String)
            strUsuReg = value
        End Set
    End Property

    Public Property IDProducto() As Integer
        Get
            Return intProducto
        End Get
        Set(ByVal value As Integer)
            intProducto = value
        End Set

    End Property

    Public Property IDMarca() As Integer
        Get
            Return intMarca
        End Get
        Set(ByVal value As Integer)
            intMarca = value
        End Set
    End Property

    Public Property IDClasificacion() As Integer
        Get
            Return intClasif
        End Get
        Set(ByVal value As Integer)
            intClasif = value
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

    Public Property IDFamilia() As Integer
        Get
            Return intFamilia
        End Get
        Set(ByVal value As Integer)
            intFamilia = value
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

    Public Property AñoModelo() As Integer
        Get
            Return intAnioModelo
        End Get
        Set(ByVal value As Integer)
            intAnioModelo = value
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

    Public Property ImagenProducto() As String
        Get
            Return strImagen
        End Get
        Set(ByVal value As String)
            strImagen = value
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

    Public Property Precio() As Double
        Get
            Return dblPrecio
        End Get
        Set(ByVal value As Double)
            dblPrecio = value
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

    'BBVA-P-412
    Public Property IDSubmarca As Integer
        Get
            Return intidsubmarca
        End Get
        Set(value As Integer)
            intidsubmarca = value
        End Set
    End Property

    Public Property IDVersion As Integer
        Get
            Return intidversion
        End Get
        Set(value As Integer)
            intidversion = value
        End Set
    End Property

    Public Property IDAnio As Integer
        Get
            Return intidanio
        End Get
        Set(value As Integer)
            intidanio = value
        End Set
    End Property

    Public Property PrecioSemi As Double
        Get
            Return dblpreciosem
        End Get
        Set(value As Double)
            dblpreciosem = value
        End Set
    End Property

    Public Property idbroker As Integer
        Get
            Return intidbroker
        End Get
        Set(value As Integer)
            intidbroker = value
        End Set
    End Property

    Public Sub CargaProducto(Optional ByVal intProd As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intProducto = intProd
            dtsRes = ManejaProducto(1)
            intProducto = 0
            If Trim$(strErrProducto) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intProducto = dtsRes.Tables(0).Rows(0).Item("ID_PRODUCTO")
                    intMarca = dtsRes.Tables(0).Rows(0).Item("ID_MARCA")
                    intTipoProd = dtsRes.Tables(0).Rows(0).Item("ID_TIPO_PRODUCTO")
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    intClasif = dtsRes.Tables(0).Rows(0).Item("ID_CLASIFICACION")
                    intAnioModelo = dtsRes.Tables(0).Rows(0).Item("ANIO_MODELO")
                    sngRegDef = dtsRes.Tables(0).Rows(0).Item("REG_DEFAULT")
                    strNombre = dtsRes.Tables(0).Rows(0).Item("DESCRIPCION")
                    strImagen = dtsRes.Tables(0).Rows(0).Item("IMAGEN")
                    strIdExterno = dtsRes.Tables(0).Rows(0).Item("ID_EXTERNO")
                    dblPrecio = dtsRes.Tables(0).Rows(0).Item("PRECIO")
                    intFamilia = dtsRes.Tables(0).Rows(0).Item("ID_FAMILIA")
                    'BBVA-P-412
                    intidsubmarca = dtsRes.Tables(0).Rows(0).Item("ID_SUBMARCA")
                    intidversion = dtsRes.Tables(0).Rows(0).Item("ID_VERSION")
                    intidanio = dtsRes.Tables(0).Rows(0).Item("ID_ANIO")
                    dblpreciosem = dtsRes.Tables(0).Rows(0).Item("PRECIO_SEMINUEVO")
                    intidbroker = dtsRes.Tables(0).Rows(0).Item("ID_BROKER")
                Else
                    strErrProducto = "No se encontró información para poder cargar el producto"
                End If
            End If
        Catch ex As Exception
            strErrProducto = ex.Message
        End Try
    End Sub

    Public Function ManejaProducto(ByVal intOper As Integer) As DataSet
        strErrProducto = ""
        ManejaProducto = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta producto
                    If intProducto > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idProducto", intProducto.ToString)
                    If intMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    If intTipoProd > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoProd", intTipoProd.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If intClasif > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", intClasif.ToString)
                    If Trim(strNombre) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "descrip", strNombre)
                    If Trim(strIdExterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "idExterno", strIdExterno)
                    If intidsubmarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idsubmarca", intidsubmarca.ToString)
                    If intidversion > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idversion", intidversion.ToString)
                    If intidanio > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idanio", intidanio.ToString)
                    If intidbroker > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "id_broker", intidbroker.ToString)
                Case 2 ' inserta producto
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoProd", intTipoProd.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", intClasif.ToString)
                    If intFamilia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idFamilia", intFamilia.ToString)
                    If intAnioModelo > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "anioModelo", intAnioModelo.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "precio", dblPrecio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "descrip", strNombre)
                    If Trim(strImagen) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "nomImagen", strImagen)
                    If Trim(strIdExterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "idExterno", strIdExterno)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    ''BBVA-P-412
                    ArmaParametros(strParamStored, TipoDato.Cadena, "idsubmarca", intidsubmarca.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "idversion", intidversion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "idanio", intidanio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "preciosm", dblpreciosem.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "id_broker", intidbroker.ToString)
                Case 3 ' actualiza producto
                    ArmaParametros(strParamStored, TipoDato.Entero, "idProducto", intProducto.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoProd", intTipoProd.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", intClasif.ToString)
                    If intFamilia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idFamilia", intFamilia.ToString)
                    If intAnioModelo > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "anioModelo", intAnioModelo.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "precio", dblPrecio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "descrip", strNombre)
                    If Trim(strImagen) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "nomImagen", strImagen)
                    If Trim(strIdExterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "idExterno", strIdExterno)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    ''BBVA-P-412
                    ArmaParametros(strParamStored, TipoDato.Cadena, "idsubmarca", intidsubmarca.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "idversion", intidversion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "idanio", intidanio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "preciosm", dblpreciosem.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "id_broker", intidbroker.ToString)
                Case 4 ' borra producto
                    ArmaParametros(strParamStored, TipoDato.Entero, "idProducto", intProducto.ToString)
                Case 5 ' Consulta Cotizador "PRODUCTO"
                    If intMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    If intIdTipoOper > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoOper", intIdTipoOper.ToString)
                    If intIdTipoPer > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoPer", intIdTipoPer.ToString)
                    If intIdAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intIdAgencia.ToString)
                Case 6 ' Consulta Cotizador "Version" RQ18
                    If intMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    If intIdTipoOper > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoOper", intIdTipoOper.ToString)
                    If intIdTipoPer > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoPer", intIdTipoPer.ToString)
                    If intProducto > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idProducto", intProducto.ToString)
                    If intIdAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intIdAgencia.ToString)
                    If intidsubmarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idsubmarca", intidsubmarca.ToString)
                Case 7 ' Consulta Cotizador "Año" RQ18
                    If intMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    If intIdTipoOper > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoOper", intIdTipoOper.ToString)
                    If intIdTipoPer > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoPer", intIdTipoPer.ToString)
                    If intProducto > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idProducto", intProducto.ToString)
                    If intIdAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intIdAgencia.ToString)
                    If intidsubmarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idsubmarca", intidsubmarca.ToString)
                    If intidversion > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idversion", intidversion.ToString)
                Case 8 ' Consulta Cotizador "Precio" RQ18
                    If intMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    If intIdTipoOper > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoOper", intIdTipoOper.ToString)
                    If intIdTipoPer > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoPer", intIdTipoPer.ToString)
                    If intProducto > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idProducto", intProducto.ToString)
                    If intIdAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intIdAgencia.ToString)
                    If intidsubmarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idsubmarca", intidsubmarca.ToString)
                    If intidversion > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idversion", intidversion.ToString)
                    If intidanio > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idanio", intidanio.ToString)
                Case 9 ' Consulta Cotizador "Clasificación" RQ18
                    If intMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    If intIdAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intIdAgencia.ToString)
                    If intidsubmarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idsubmarca", intidsubmarca.ToString)
                    If intidversion > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idversion", intidversion.ToString)
                    If intidanio > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idanio", intidanio.ToString)
                Case 11 'Nueva busqueda de clasificacion para nueva cascada de cotizador
                    If intMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    If intIdAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intIdAgencia.ToString)
                    If intidsubmarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idsubmarca", intidsubmarca.ToString)
                Case 12 'Nueva busqueda de Años para nueva cascada de cotizador
                    If intMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    If intIdAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intIdAgencia.ToString)
                    If intidsubmarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idsubmarca", intidsubmarca.ToString)
                    If intClasif > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", intClasif.ToString)
                Case 13 'Nueva busqueda de Versiones para la nueva cascada del cotizador
                    If intMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    If intIdAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intIdAgencia.ToString)
                    If intidsubmarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idsubmarca", intidsubmarca.ToString)
                    If intClasif > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", intClasif.ToString)
                    If intidanio > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idanio", intidanio.ToString)
            End Select

            ManejaProducto = objSD.EjecutaStoredProcedure("spManejaProductos", strErrProducto, strParamStored)
            If strErrProducto = "" Then
                If intOper = 2 Then
                    intProducto = ManejaProducto.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("PRODUCTOS", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrProducto = ex.Message
        End Try
    End Function
End Class
