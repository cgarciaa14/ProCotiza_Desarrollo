'BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.

Imports SDManejaBD

Public Class clsSubMarcas
    Inherits clsSession

    Private strErrSubMarcas As String = ""

    Private intidsubmarca As Integer = 0
    Private intidmarca As Integer = 0
    Private strdescripcion As String = String.Empty
    Private intidexterno As Integer = 0
    Private intregdefault As Integer = 0
    Private intestatus As Integer = 0
    Private strusureg As String = String.Empty

    Sub New()
    End Sub

    Sub New(ByVal intCveSubMarca As Integer)
        CargaSubMarca(intCveSubMarca)
    End Sub

    Public ReadOnly Property ErrorSubMarcas() As String
        Get
            Return strErrSubMarcas
        End Get
    End Property

    Public Property IDSubmarca As Integer
        Get
            Return intidsubmarca
        End Get
        Set(value As Integer)
            intidsubmarca = value
        End Set
    End Property

    Public Property IDMarca As Integer
        Get
            Return intidmarca
        End Get
        Set(value As Integer)
            intidmarca = value
        End Set
    End Property

    Public Property Descripcion As String
        Get
            Return strdescripcion
        End Get
        Set(value As String)
            strdescripcion = value
        End Set
    End Property

    Public Property IDExterno As Integer
        Get
            Return intidexterno
        End Get
        Set(value As Integer)
            intidexterno = value
        End Set
    End Property

    Public Property IDRegDefault As Integer
        Get
            Return intregdefault
        End Get
        Set(value As Integer)
            intregdefault = value
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

    Public Sub CargaSubMarca(Optional ByVal intSubMar As Integer = 0)
        Dim dtsRes As New DataSet
        Try
            intidsubmarca = intSubMar
            dtsRes = ManejaSubMarca(1)
            intidsubmarca = 0
            If strErrSubMarcas.Trim = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intidsubmarca = dtsRes.Tables(0).Rows(0).Item("ID_SUBMARCA")
                    intidmarca = dtsRes.Tables(0).Rows(0).Item("ID_MARCA")
                    strdescripcion = dtsRes.Tables(0).Rows(0).Item("DESCRIPCION")
                    intidexterno = dtsRes.Tables(0).Rows(0).Item("ID_EXTERNO")
                    intregdefault = dtsRes.Tables(0).Rows(0).Item("REG_DEFAULT")
                    intestatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    strusureg = dtsRes.Tables(0).Rows(0).Item("USU_REG")
                Else
                    strErrSubMarcas = "No se encontró información para poder cargar la SubMarca"
                End If
            End If
        Catch ex As Exception
            strErrSubMarcas = ex.Message
        End Try
    End Sub

    Public Function ManejaSubMarca(ByVal intOper As Integer) As DataSet
        strErrSubMarcas = ""
        ManejaSubMarca = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)

            Select Case intOper
                Case 1 'Consulta
                    If intidsubmarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idsubmarca", intidsubmarca.ToString)
                    If intidmarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idmarca", intidmarca.ToString)
                    If strdescripcion.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "descripcion", strdescripcion)
                    If intestatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intestatus.ToString)
                Case 2 'Inserta
                    ArmaParametros(strParamStored, TipoDato.Entero, "idmarca", intidmarca.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "descripcion", strdescripcion)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "idexterno", intidexterno.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "regdefault", intregdefault.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intestatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "usureg", strusureg)
                Case 3 'Actualiza
                    ArmaParametros(strParamStored, TipoDato.Entero, "idsubmarca", intidsubmarca.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idmarca", intidmarca.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "descripcion", strdescripcion)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "idexterno", intidexterno.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "regdefault", intregdefault.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intestatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "usureg", strusureg)
                Case 4 'Borra
                    ArmaParametros(strParamStored, TipoDato.Entero, "idsubmarca", intidsubmarca.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idmarca", intidmarca.ToString)
            End Select

            ManejaSubMarca = objSD.EjecutaStoredProcedure("spManejaSubMarcas", strErrSubMarcas, strParamStored)
            If strErrSubMarcas = "" Then
                If intOper = 2 Then
                    intidsubmarca = ManejaSubMarca.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("SUBMARCAS", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrSubMarcas = ex.Message
        End Try
    End Function
End Class
