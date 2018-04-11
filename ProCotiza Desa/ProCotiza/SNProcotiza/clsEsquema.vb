''BBVA RQTARESQ-06 CGARCIA 19/04/2017 SE AGREGA LA VARIABLE ESQUEMA PATRA LA RELACION DEL CATÁLOGO DE ESQUEMAS 

Imports SDManejaBD
Public Class clsEsquema
    Inherits clsSession

    Private strErrEsquema As String = ""
    Private intidEsquema As Integer = 0
    Private strEsquema As String = String.Empty
    Private intestatus As Integer = 0
    Private strusureg As String = String.Empty
    Private intidproductoug As Integer = 0
    Private strDescripcion As String = ""
    Private sngRegDef As Single = 0

    Sub New()
    End Sub

    Public ReadOnly Property ErrorEsquemas() As String
        Get
            Return strErrEsquema
        End Get
    End Property

    Public Property IDEsquema As Integer
        Get
            Return intidEsquema
        End Get
        Set(value As Integer)
            intidEsquema = value
        End Set
    End Property

    Public Property Nombre As String
        Get
            Return strEsquema
        End Get
        Set(value As String)
            strEsquema = value
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

    Public Sub CargaEsquemas(Optional ByVal intProd As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            'intidEsquema = intProd
            dtsRes = MAnejaEsquema(1)
            intidEsquema = 0
            If strErrEsquema.Trim = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intidEsquema = dtsRes.Tables(0).Rows(0).Item("ID_ESQUEMAS")
                    strEsquema = dtsRes.Tables(0).Rows(0).Item("C_ESQUEMAS")
                    intestatus = dtsRes.Tables(0).Rows(0).Item("C_ESTATUS")
                    strusureg = dtsRes.Tables(0).Rows(0).Item("C_USU_REG")
                    'intidproductoug = dtsRes.Tables(0).Rows(0).Item("ID_PRODUCTO_UG")
                Else
                    strErrEsquema = "No se encontró información para poder cargar los Esquemas"
                End If
            End If

        Catch ex As Exception
            strErrEsquema = ex.Message
        End Try
    End Sub

    Public Function MAnejaEsquema(ByVal intOper As Integer) As DataSet
        strErrEsquema = ""
        MAnejaEsquema = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "INT_OPCION", intOper.ToString)

            Select Case intOper
                Case 1 'Consulta
                    If intidEsquema > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "ID_ESQUEMA", intidEsquema.ToString)
                    If strEsquema.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "C_ESQUEMA", strEsquema)
                    If intestatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "C_ESTATUS", intestatus)                    
                Case 2 'Inserta
                    'ArmaParametros(strParamStored, TipoDato.Cadena, "idsubproductoug", intidsubproductoug.ToString)
                    If strEsquema.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "C_ESQUEMA", strEsquema)
                    If intestatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "C_ESTATUS", intestatus)
                    If strusureg.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "C_USR_REG", strusureg)
                    'If intidproductoug > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idproductoug", intidproductoug.ToString)
                    If strDescripcion.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "CDESCRIPCION", strDescripcion)
                    ArmaParametros(strParamStored, TipoDato.Entero, "DEFAULT", sngRegDef)
                Case 3 'Actualiza
                    If intidEsquema > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "ID_ESQUEMA", intidEsquema.ToString)
                    If strEsquema.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "C_ESQUEMA", strEsquema)
                    If intestatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "C_ESTATUS", intestatus)
                    If strusureg.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "C_USR_REG", strusureg)
                    'If intidproductoug > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idproductoug", intidproductoug.ToString)
                    If strDescripcion.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "CDESCRIPCION", strDescripcion)
                    ArmaParametros(strParamStored, TipoDato.Entero, "DEFAULT", sngRegDef)
                Case 4 'Borra
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_ESQUEMA", intidEsquema.ToString)
            End Select

            MAnejaEsquema = objSD.EjecutaStoredProcedure("SPD_MANEJA_ESQUEMAS", strErrEsquema, strParamStored)

            If strErrEsquema = "" Then
                If intOper = 2 Then
                    intidproductoug = MAnejaEsquema.Tables(0).Rows(0).Item(0)
                End If
            End If

            If intOper = 2 Or intOper = 3 Then
                GuardaLog("SUBPRODUCTOS_UG", strParamStored, IIf(intOper = 2, 117, 118))
            End If

        Catch ex As Exception
            strErrEsquema = ex.Message
        End Try
    End Function
End Class
