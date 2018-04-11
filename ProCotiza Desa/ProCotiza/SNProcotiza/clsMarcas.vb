#Region "Trackers"
'BUG-PC-102:JBEJAR:17/08/2017:CORRECIONES EN LOS BOTONES TODOS Y NINGUNO PARA AÑADIR FILTROS.  EN RELACION MARCA.  
#End Region
Imports SDManejaBD

Public Class clsMarcas
    Inherits clsSession

    Private strErrMarcas As String = ""

    Private intMarca As Integer = 0
    Private intEstatus As Integer = 0
    Private intTipoRegistro As Integer = 0
    Private sngRegDef As Single = 0

    Private strNombre As String = ""
    Private strUsuReg As String = ""
    Private intAgencia As Integer = 0
    Private intClasif As Integer = 0

    Sub New()
    End Sub
    Sub New(ByVal intCveMarca As Integer)
        CargaMarca(intCveMarca)
    End Sub

    Public ReadOnly Property ErrorMarcas() As String
        Get
            Return strErrMarcas
        End Get
    End Property

    Public WriteOnly Property UsuarioRegistro() As String
        Set(ByVal value As String)
            strUsuReg = value
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

    Public Property IDEstatus() As Integer
        Get
            Return intEstatus
        End Get
        Set(ByVal value As Integer)
            intEstatus = value
        End Set
    End Property

    Public Property IDTipoRegistro() As Integer
        Get
            Return intTipoRegistro
        End Get
        Set(ByVal value As Integer)
            intTipoRegistro = value
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

    Public Property IDAgencia() As Integer
        Get
            Return intAgencia
        End Get
        Set(value As Integer)
            intAgencia = value
        End Set
    End Property

    Public Property IDClasif() As Integer
        Get
            Return intClasif
        End Get
        Set(value As Integer)
            intClasif = value
        End Set
    End Property

    Public Sub CargaMarca(Optional ByVal intMar As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intMarca = intMar
            dtsRes = ManejaMarca(1)
            intMarca = 0
            If Trim$(strErrMarcas) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intMarca = dtsRes.Tables(0).Rows(0).Item("ID_MARCA")
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    intTipoRegistro = dtsRes.Tables(0).Rows(0).Item("ID_TIPO_REGISTRO")
                    sngRegDef = dtsRes.Tables(0).Rows(0).Item("REG_DEFAULT")
                    strNombre = dtsRes.Tables(0).Rows(0).Item("NOMBRE")
                Else
                    strErrMarcas = "No se encontró información para poder cargar la marca"
                End If
            End If
        Catch ex As Exception
            strErrMarcas = ex.Message
        End Try
    End Sub

    Public Function ManejaMarca(ByVal intOper As Integer) As DataSet
        strErrMarcas = ""
        ManejaMarca = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta marca
                    If intMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If Trim(strNombre) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                Case 2 ' inserta marca
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoReg", intTipoRegistro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 3 ' actualiza marca
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoReg", intTipoRegistro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 4 ' borra marca
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                Case 5 ' consulta mandando siempre el tipo de registro
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoReg", intTipoRegistro.ToString)
                    If intMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If Trim(strNombre) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                Case 7 'Relación Marca-Agencia --> Consulta
                    If intMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If strNombre.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                Case 8 'Relación Marca-Agencia --> Borra
                    If intMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If strNombre.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre) 'BUG-PC-102 CORRECIONES BOTON NINGUNO CON FILTRO  
                Case 9 'Relación Marca-Agencia --> Inserta
                    If intMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If strNombre.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre) 'BUG-PC-102 CORRECIONES BOTON TODOS  CON FILTRO  
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 10 'Consulta Cotizador 
                    If intMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If intClasif > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", intClasif.ToString)
            End Select

            ManejaMarca = objSD.EjecutaStoredProcedure("spManejaMarcas", strErrMarcas, strParamStored)
            If strErrMarcas = "" Then
                If intOper = 2 Then
                    intMarca = ManejaMarca.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("MARCAS", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrMarcas = ex.Message
        End Try
    End Function
End Class
