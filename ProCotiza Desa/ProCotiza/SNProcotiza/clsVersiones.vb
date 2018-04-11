'BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.
'BBVA-P-412: 23/08/2016: AVH: RQ11 Se agrega opciones de Porcentajes Adicionales 5,6,7
'BUG-PC-48 JRHM 28/02/17 SE CREAN OPCIONES 8 Y 9 PARA LA CONSULTA DE ENGANCHE CI EN EL COTIZADOR
'BUG-PC-50 JRHM 29/03/17 SE MODIFICA OPCION 7 DE MANEJAVERSIONES PARA QUE ACEPTE EL PARAMETRO ENGANCHE Y ESTE ELIMINE SOLO AQUEL QUE TIENE DICHO PLAZO Y ENGANCHE CI
Imports SDManejaBD

Public Class clsVersiones
    Inherits clsSession

    Private strErrVersiones As String = ""

    Private intidversion As Integer = 0
    Private intidmarca As Integer = 0
    Private intidsubmarca As Integer = 0
    Private strdescripcion As String = String.Empty
    Private intidexterno As Integer = 0
    Private intregdefault As Integer = 0
    Private intidestatus As Integer = 0
    Private strusureg As String = String.Empty
    'BBVA-P-412: AVH
    Private intPlazo As Integer = 0
    Private dblEnganche As Double = 0
    Private dblBalloon As Double = 0

    Sub New()
    End Sub

    Sub New(ByVal intCveVersion As Integer)
        CargaVersion(intCveVersion)
    End Sub

    Public ReadOnly Property ErrorVersion As String
        Get
            Return strErrVersiones
        End Get
    End Property

    Public Property IDVersion As Integer
        Get
            Return intidversion
        End Get
        Set(value As Integer)
            intidversion = value
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

    Public Property IDSubMarca As Integer
        Get
            Return intidsubmarca
        End Get
        Set(value As Integer)
            intidsubmarca = value
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

    Public Property RegDefault As Integer
        Get
            Return intregdefault
        End Get
        Set(value As Integer)
            intregdefault = value
        End Set
    End Property

    Public Property IDEstatus As Integer
        Get
            Return intidestatus
        End Get
        Set(value As Integer)
            intidestatus = value
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
    'BBVA-P-412: AVH
    Public Property Plazo As Integer
        Get
            Return intPlazo
        End Get
        Set(value As Integer)
            intPlazo = value
        End Set
    End Property
    Public Property Enganche As Double
        Get
            Return dblEnganche
        End Get
        Set(value As Double)
            dblEnganche = value
        End Set
    End Property
    Public Property Balloon As Double
        Get
            Return dblBalloon
        End Get
        Set(value As Double)
            dblBalloon = value
        End Set
    End Property

    Public Sub CargaVersion(Optional ByVal intVer As Integer = 0)
        Dim dtsRes As New DataSet
        Try
            intidversion = intVer
            dtsRes = ManejaVersion(1)
            intidversion = 0

            If strErrVersiones.Trim = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intidversion = dtsRes.Tables(0).Rows(0).Item("ID_VERSION")
                    intidmarca = dtsRes.Tables(0).Rows(0).Item("ID_MARCA")
                    intidsubmarca = dtsRes.Tables(0).Rows(0).Item("ID_SUBMARCA")
                    strdescripcion = dtsRes.Tables(0).Rows(0).Item("DESCRIPCION")
                    intidexterno = dtsRes.Tables(0).Rows(0).Item("ID_EXTERNO")
                    intregdefault = dtsRes.Tables(0).Rows(0).Item("REG_DEFAULT")
                    intidestatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    strusureg = dtsRes.Tables(0).Rows(0).Item("USU_REG")
                Else
                    strErrVersiones = "No se encontró información para poder cargar la Version."
                End If
            End If
        Catch ex As Exception
            strErrVersiones = ex.Message
        End Try
    End Sub

    Public Function ManejaVersion(ByVal intOper As Integer) As DataSet
        strErrVersiones = ""
        ManejaVersion = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 'Consulta
                    If intidversion > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idversion", intidversion.ToString)
                    If intidmarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idmarca", intidmarca.ToString)
                    If intidsubmarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idsubmarca", intidsubmarca.ToString)
                    If strdescripcion.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "descripcion", strdescripcion)
                    If intidestatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intidestatus.ToString)
                Case 2 'Inserta
                    ArmaParametros(strParamStored, TipoDato.Entero, "idmarca", intidmarca.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idsubmarca", intidsubmarca.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "descripcion", strdescripcion)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idexterno", intidexterno.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "regdefault", intregdefault.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intidestatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usureg", strusureg)
                Case 3 'Actualiza
                    ArmaParametros(strParamStored, TipoDato.Entero, "idversion", intidversion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idmarca", intidmarca.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idsubmarca", intidsubmarca.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "descripcion", strdescripcion)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idexterno", intidexterno.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "regdefault", intregdefault.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intidestatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usureg", strusureg)
                Case 4 'Borra
                    ArmaParametros(strParamStored, TipoDato.Entero, "idversion", intidversion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idmarca", intidmarca.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idsubmarca", intidsubmarca.ToString)
                Case 5 'BBVA-P-412: AVH:CONSULTA ADICIONALES
                    ArmaParametros(strParamStored, TipoDato.Entero, "idversion", intidversion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intidestatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usureg", strusureg)
                Case 6 'BBVA-P-412: AVH:INSERTA ADICIONALES
                    ArmaParametros(strParamStored, TipoDato.Entero, "idversion", intidversion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Plazo", intPlazo.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "Enganche", dblEnganche.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "Balloon", dblBalloon.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intidestatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usureg", strusureg)
                Case 7 'BBVA-P-412: AVH:BORRA ADICIONALES
                    ArmaParametros(strParamStored, TipoDato.Entero, "idversion", intidversion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Plazo", intPlazo.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "Enganche", dblEnganche.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usureg", strusureg)
                Case 8
                    ArmaParametros(strParamStored, TipoDato.Entero, "idversion", intidversion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Plazo", intPlazo.ToString)
                Case 9
                    ArmaParametros(strParamStored, TipoDato.Entero, "idversion", intidversion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Plazo", intPlazo.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "Enganche", dblEnganche.ToString)
            End Select

            ManejaVersion = objSD.EjecutaStoredProcedure("spManejaVersiones", strErrVersiones, strParamStored)

            If strErrVersiones = "" Then
                If intOper = 2 Then
                    intidversion = ManejaVersion.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("VERSIONES", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If
        Catch ex As Exception
            strErrVersiones = ex.Message
        End Try
    End Function
End Class
