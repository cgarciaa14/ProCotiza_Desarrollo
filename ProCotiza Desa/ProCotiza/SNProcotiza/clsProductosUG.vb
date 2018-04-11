'BBVA-P-412: 06-07-2016: AMR: RQ02,RQ03,RQ04,RQ05,RQ06,RQ08 SE AGREGA CLASE PARA LA TABLA PRODUCTOS_UG
'BBV-P-412: AVH: RQ 14: 26/07/2016 SE AGREGAN MAS CAMPOS (DESCRIPCION,DEFAULT) PARA PRODUCTOS_UG
Imports SDManejaBD

Public Class clsProductosUG
    Inherits clsSession

    Private strErrProductoUG As String = ""

    Private intidproductoug As Integer = 0
    Private strnombre As String = String.Empty
    Private intestatus As Integer = 0
    Private strusureg As String = String.Empty

    'AVH
    Private strDescripcion As String = ""
    Private sngRegDef As Single = 0

    Sub New()
    End Sub

    Sub New(ByVal IdProdUG As Integer)
        CargaProductoUG(IdProdUG)
    End Sub

    Public ReadOnly Property ErrorProductoUG() As String
        Get
            Return strErrProductoUG
        End Get
    End Property

    Public Property IDProductoUG As Integer
        Get
            Return intidproductoug
        End Get
        Set(value As Integer)
            intidproductoug = value
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

    Public Sub CargaProductoUG(Optional ByVal intProd As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intidproductoug = intProd
            dtsRes = ManejaProductoUG(1)
            intidproductoug = 0
            If strErrProductoUG.Trim = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intidproductoug = dtsRes.Tables(0).Rows(0).Item("ID_PRODUCTO_UG")
                    strnombre = dtsRes.Tables(0).Rows(0).Item("NOMBRE")
                    intestatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    strusureg = dtsRes.Tables(0).Rows(0).Item("USU_REG")
                Else
                    strErrProductoUG = "No se encontró información para poder cargar los productos UG"
                End If
            End If

        Catch ex As Exception
            strErrProductoUG = ex.Message
        End Try
    End Sub

    Public Function ManejaProductoUG(ByVal intOper As Integer) As DataSet
        strErrProductoUG = ""
        ManejaProductoUG = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)

            Select Case intOper
                Case 1 'Consulta
                    If intidproductoug > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idproductoug", intidproductoug.ToString)
                    If strnombre.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nombre", strnombre)
                    If intestatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intestatus)
                Case 2 'Inserta
                    ArmaParametros(strParamStored, TipoDato.Entero, "idproductoug", intidproductoug.ToString)
                    If strnombre.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nombre", strnombre)
                    If intestatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intestatus)
                    If strusureg.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "usureg", strusureg)
                    If strDescripcion.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "Descripcion", strDescripcion)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Default", sngRegDef)

                Case 3 'Actualiza
                    ArmaParametros(strParamStored, TipoDato.Entero, "idproductoug", intidproductoug.ToString)
                    If strnombre.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nombre", strnombre)
                    If intestatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intestatus)
                    If strusureg.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "usureg", strusureg)
                    If strDescripcion.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "Descripcion", strDescripcion)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Default", sngRegDef)
                Case 4 'Borra
                    ArmaParametros(strParamStored, TipoDato.Entero, "idproductoug", intidproductoug.ToString)
            End Select

            ManejaProductoUG = objSD.EjecutaStoredProcedure("Maneja_PRODUCTOS_UG", strErrProductoUG, strParamStored)

            If strErrProductoUG = "" Then
                If intOper = 2 Then
                    intidproductoug = ManejaProductoUG.Tables(0).Rows(0).Item(0)
                End If
            End If

            If intOper = 2 Or intOper = 3 Then
                GuardaLog("PRODUCTOS_UG", strParamStored, IIf(intOper = 2, 117, 118))
            End If

        Catch ex As Exception
            strErrProductoUG = ex.Message
        End Try
    End Function
End Class
