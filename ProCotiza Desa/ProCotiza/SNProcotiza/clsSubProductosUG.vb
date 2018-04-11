'BBVA-P-412: 06-07-2016: AMR: RQ02,RQ03,RQ04,RQ05,RQ06,RQ08 SE AGREGA LA CLASE PARA LA TABLA SUBPRODUCTOS_UG
'BBV-P-412: AVH: RQ 15: 27/07/2016 SE AGREGAN CAMPOS (DESCRIPCION,DEFAULT)
'BBVA-P-412: 08/08/2016: AMR: RQ09 – Carga Masiva de Plan de Financiamiento
Imports SDManejaBD

Public Class clsSubProductosUG
    Inherits clsSession

    Private strErrSubProductoUG As String = ""

    Private intidsubproductoug As Integer = 0
    Private strnombre As String = String.Empty
    Private intestatus As Integer = 0
    Private strusureg As String = String.Empty
    Private intidproductoug As Integer = 0

    'AVH
    Private strDescripcion As String = ""
    Private sngRegDef As Single = 0

    Sub New()
    End Sub

    Sub New(ByVal IdProdUG As Integer)
        CargaSubProductoUG(IdProdUG)
    End Sub

    Public ReadOnly Property ErrorSubProductoUG() As String
        Get
            Return strErrSubProductoUG
        End Get
    End Property

    Public Property IDSubProductoUG As Integer
        Get
            Return intidsubproductoug
        End Get
        Set(value As Integer)
            intidsubproductoug = value
        End Set
    End Property

    Public Property Nombre As String
        Get
            Return strnombre
        End Get
        Set(value As String)
            strnombre = value
        End Set
    End Property

    Public Property IDEstatus As Integer
        Get
            Return intestatus
        End Get
        Set(value As Integer)
            intestatus = value
        End Set
    End Property

    Public Property UsuReg As String
        Get
            Return strusureg
        End Get
        Set(value As String)
            strusureg = value
        End Set
    End Property

    Public Property IDProductoUG As Integer
        Get
            Return intidproductoug
        End Get
        Set(value As Integer)
            intidproductoug = value
        End Set
    End Property

    Public Property Descripcion As String
        Get
            Return strDescripcion
        End Get
        Set(value As String)
            strDescripcion = value
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

    Public Sub CargaSubProductoUG(Optional ByVal intProd As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intidsubproductoug = intProd
            dtsRes = ManejaSubProductoUG(1)
            intidsubproductoug = 0
            If strErrSubProductoUG.Trim = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intidsubproductoug = dtsRes.Tables(0).Rows(0).Item("ID_SUBPRODUCTO_UG")
                    strnombre = dtsRes.Tables(0).Rows(0).Item("NOMBRE")
                    intestatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    strusureg = dtsRes.Tables(0).Rows(0).Item("USU_REG")
                    intidproductoug = dtsRes.Tables(0).Rows(0).Item("ID_PRODUCTO_UG")
                Else
                    strErrSubProductoUG = "No se encontró información para poder cargar los Subproductos UG"
                End If
            End If

        Catch ex As Exception
            strErrSubProductoUG = ex.Message
        End Try
    End Sub

    Public Function ManejaSubProductoUG(ByVal intOper As Integer) As DataSet
        strErrSubProductoUG = ""
        ManejaSubProductoUG = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)

            Select Case intOper
                Case 1 'Consulta
                    If intidsubproductoug > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idsubproductoug", intidsubproductoug.ToString)
                    If strnombre.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nombre", strnombre)
                    If intestatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intestatus)
                    If intidproductoug > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idproductoug", intidproductoug.ToString)
                Case 2 'Inserta
                    'ArmaParametros(strParamStored, TipoDato.Cadena, "idsubproductoug", intidsubproductoug.ToString)
                    If strnombre.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nombre", strnombre)
                    If intestatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intestatus)
                    If strusureg.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "usureg", strusureg)
                    If intidproductoug > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idproductoug", intidproductoug.ToString)
                    If strDescripcion.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "Descripcion", strDescripcion)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Default", sngRegDef)
                Case 3 'Actualiza
                    If intidsubproductoug > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idsubproductoug", intidsubproductoug.ToString)
                    If strnombre.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nombre", strnombre)
                    If intestatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intestatus)
                    If strusureg.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "usureg", strusureg)
                    If intidproductoug > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idproductoug", intidproductoug.ToString)
                    If strDescripcion.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "Descripcion", strDescripcion)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Default", sngRegDef)
                Case 4 'Borra
                    ArmaParametros(strParamStored, TipoDato.Entero, "idsubproductoug", intidsubproductoug.ToString)
            End Select

            ManejaSubProductoUG = objSD.EjecutaStoredProcedure("Maneja_SUBPRODUCTOS_UG", strErrSubProductoUG, strParamStored)

            If strErrSubProductoUG = "" Then
                If intOper = 2 Then
                    intidproductoug = ManejaSubProductoUG.Tables(0).Rows(0).Item(0)
                End If
            End If

            If intOper = 2 Or intOper = 3 Then
                GuardaLog("SUBPRODUCTOS_UG", strParamStored, IIf(intOper = 2, 117, 118))
            End If

        Catch ex As Exception
            strErrSubProductoUG = ex.Message
        End Try
    End Function
End Class
