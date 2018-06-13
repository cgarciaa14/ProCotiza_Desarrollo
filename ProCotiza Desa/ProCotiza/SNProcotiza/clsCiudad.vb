Imports SDManejaBD

Public Class clsCiudad
    Inherits clsSession
    'BUG-PC-161:CGARCIA: 27/02/2018; SE AGREGA FILTRO DE CIUDAD
    'BUG-PC-198: CGARCIA: 23/05/2018: SE VALIDA DUPLICADO DE ID DE MUNICIPIOS Y FILTRO DE CUIDADES DEJA DE DEPENDER DE MUNICIPIO
#Region "Variables"

    Private intEFD_CL_CVE As Integer = 0
    Private intCIU_CL_CIUDAD As Integer = 0
    Private strCIU_NB_CIUDAD As String = String.Empty
    Private intCIU_FG_STATUS As Integer = 0
    Private strUSR_CL_CVE As String = String.Empty

#End Region

#Region "Propiedades"

    ''' <summary>
    ''' Clave de la entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _intEFD_CL_CVE() As Integer
        Get
            Return intEFD_CL_CVE
        End Get
        Set(ByVal value As Integer)
            intEFD_CL_CVE = value
        End Set
    End Property

    ''' <summary>
    ''' Clave de la ciudad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _intCIU_CL_CIUDAD() As String
        Get
            Return intCIU_CL_CIUDAD
        End Get
        Set(ByVal value As String)
            intCIU_CL_CIUDAD = value
        End Set
    End Property

    ''' <summary>
    ''' Descripcion de la ciudad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _strCIU_NB_CIUDAD() As Integer
        Get
            Return strCIU_NB_CIUDAD
        End Get
        Set(ByVal value As Integer)
            strCIU_NB_CIUDAD = value
        End Set
    End Property

    ''' <summary>
    ''' Status de la ciudad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _intCIU_FG_STATUS() As Integer
        Get
            Return intCIU_FG_STATUS
        End Get
        Set(ByVal value As Integer)
            intCIU_FG_STATUS = value
        End Set
    End Property

    Public Property _strUSR_CL_CVE() As String
        Get
            Return strUSR_CL_CVE
        End Get
        Set(ByVal value As String)
            strUSR_CL_CVE = value
        End Set
    End Property

#End Region

#Region "Metodos"

    Sub New()
    End Sub

    Sub New(ByVal intCiudad As Integer)
        CargaCiudad(intCiudad)
    End Sub

    Public Function ManejaCiudad(ByVal intOper As Integer) As DataSet

        ManejaCiudad = New DataSet
        Dim strParamStored As String = ""
        Dim strError As String = String.Empty
        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "Opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta ciudad
                    'If intCIU_CL_CIUDAD > -1 Then
                    '    ArmaParametros(strParamStored, TipoDato.Entero, "CIU_CL_CIUDAD", intCIU_CL_CIUDAD.ToString)
                    'End If
                    If intEFD_CL_CVE > 0 Then
                        ArmaParametros(strParamStored, TipoDato.Entero, "EFD_CL_CVE", intEFD_CL_CVE.ToString)
                    End If

                Case 2 ' inserta entidad
                    ArmaParametros(strParamStored, TipoDato.Entero, "EFD_CL_CVE", intEFD_CL_CVE.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "CIU_CL_CIUDAD", intCIU_CL_CIUDAD.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "CIU_NB_CIUDAD", strCIU_NB_CIUDAD.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "CIU_FG_STATUS", intCIU_FG_STATUS)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "USR_CL_CVE", strUSR_CL_CVE)

                Case 3 ' actualiza ciudad
                    ArmaParametros(strParamStored, TipoDato.Entero, "EFD_CL_CVE", intEFD_CL_CVE.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "CIU_CL_CIUDAD", intCIU_CL_CIUDAD.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "CIU_NB_CIUDAD", strCIU_NB_CIUDAD.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "CIU_FG_STATUS", intCIU_FG_STATUS)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "USR_CL_CVE", strUSR_CL_CVE)

                Case 4 ' borra ciudad
                    ArmaParametros(strParamStored, TipoDato.Entero, "CIU_CL_CIUDAD", intCIU_CL_CIUDAD.ToString)
            End Select

            ManejaCiudad = objSD.EjecutaStoredProcedure("spManejaCiudad", strError, strParamStored)
            If strError = "" Then
                If intOper = 2 Then
                    intCIU_CL_CIUDAD = ManejaCiudad.Tables(0).Rows(0).Item(0)
                End If
            End If

        Catch ex As Exception
            strError = ex.Message
            Throw New Exception(strError)
        End Try
    End Function

    Public Sub CargaCiudad(Optional ByVal intCiudad As Integer = 0)
        Dim dtsRes As New DataSet
        Dim strError As String = String.Empty
        Try

            intCIU_CL_CIUDAD = intCiudad
            dtsRes = ManejaCiudad(1)

            If Trim$(strError) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then

                    intEFD_CL_CVE = dtsRes.Tables(0).Rows(0).Item("EFD_CL_CVE")
                    intCIU_CL_CIUDAD = dtsRes.Tables(0).Rows(0).Item("CIU_CL_CIUDAD")
                    strCIU_NB_CIUDAD = dtsRes.Tables(0).Rows(0).Item("CIU_NB_CIUDAD")
                    intCIU_FG_STATUS = dtsRes.Tables(0).Rows(0).Item("CIU_FG_STATUS")
                    strUSR_CL_CVE = dtsRes.Tables(0).Rows(0).Item("USR_CL_CVE")

                Else
                    strError = "No se encontró información para poder cargar la ciudad"
                End If
            End If
        Catch ex As Exception
            strError = ex.Message
            Throw New Exception(strError)
        End Try
    End Sub

#End Region


End Class
