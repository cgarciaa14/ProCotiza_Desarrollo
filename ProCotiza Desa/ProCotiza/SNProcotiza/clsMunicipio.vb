Imports SDManejaBD

Public Class clsMunicipio
    Inherits clsSession

#Region "Variables"

    Private intEFD_CL_CVE As Integer = 0
    Private intCIU_CL_CIUDAD As Integer = 0
    Private intMUN_CL_CVE As Integer = 0
    Private strMUN_DS_MUNICIPIO As String = String.Empty
    Private intMUN_FG_STATUS As Integer = 0
    Private strUSR_CL_CVE As String = String.Empty

#End Region

#Region "Propiedades"

    ''' <summary>
    ''' Clave de la entidad federativa
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
    Public Property _intCIU_CL_CIUDAD() As Integer
        Get
            Return intCIU_CL_CIUDAD
        End Get
        Set(ByVal value As Integer)
            intCIU_CL_CIUDAD = value
        End Set
    End Property

    ''' <summary>
    ''' Clave de municipio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _intMUN_CL_CVE() As Integer
        Get
            Return intMUN_CL_CVE
        End Get
        Set(ByVal value As Integer)
            intMUN_CL_CVE = value
        End Set
    End Property

    ''' <summary>
    ''' Descripcion del municipio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _strMUN_DS_MUNICIPIO() As String
        Get
            Return strMUN_DS_MUNICIPIO
        End Get
        Set(ByVal value As String)
            strMUN_DS_MUNICIPIO = value
        End Set
    End Property

    ''' <summary>
    ''' Estatus del municipio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _intMUN_FG_STATUS() As Integer
        Get
            Return intMUN_FG_STATUS
        End Get
        Set(ByVal value As Integer)
            value = value
        End Set
    End Property

    ''' <summary>
    ''' Clave del usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
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

    Sub New(ByVal intMunicipio As Integer)
        CargaMunicipio(intMunicipio)
    End Sub

    Public Function ManejaMunicipio(ByVal intOper As Integer) As DataSet

        ManejaMunicipio = New DataSet
        Dim strParamStored As String = ""
        Dim strError As String = String.Empty
        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "Opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta municipio
                    If intMUN_CL_CVE > 0 Then
                        ArmaParametros(strParamStored, TipoDato.Entero, "MUN_CL_CVE", intMUN_CL_CVE.ToString)
                    End If
                    If intEFD_CL_CVE > 0 Then
                        ArmaParametros(strParamStored, TipoDato.Entero, "EFD_CL_CVE", intEFD_CL_CVE.ToString)
                    End If
                    If intCIU_CL_CIUDAD > 0 Then
                        ArmaParametros(strParamStored, TipoDato.Entero, "CIU_CL_CIUDAD", intCIU_CL_CIUDAD.ToString)
                    End If


                Case 2 ' inserta municipio
                    ArmaParametros(strParamStored, TipoDato.Entero, "EFD_CL_CVE", intEFD_CL_CVE.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "CIU_CL_CIUDAD", intCIU_CL_CIUDAD.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "MUN_CL_CVE", intMUN_CL_CVE.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "MUN_DS_MUNICIPIO", strMUN_DS_MUNICIPIO.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "MUN_FG_STATUS", intMUN_FG_STATUS)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "USR_CL_CVE", strUSR_CL_CVE)

                Case 3 ' actualiza municipio
                    ArmaParametros(strParamStored, TipoDato.Entero, "EFD_CL_CVE", intEFD_CL_CVE.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "CIU_CL_CIUDAD", intCIU_CL_CIUDAD.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "MUN_CL_CVE", intMUN_CL_CVE.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "MUN_DS_MUNICIPIO", strMUN_DS_MUNICIPIO.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "MUN_FG_STATUS", intMUN_FG_STATUS)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "USR_CL_CVE", strUSR_CL_CVE)

                Case 4 ' borra municipio
                    ArmaParametros(strParamStored, TipoDato.Entero, "MUN_CL_CVE", intMUN_CL_CVE.ToString)
            End Select

            ManejaMunicipio = objSD.EjecutaStoredProcedure("spManejaMunicipio", strError, strParamStored)
            If strError = "" Then
                If intOper = 2 Then
                    intMUN_CL_CVE = ManejaMunicipio.Tables(0).Rows(0).Item(0)
                End If
            End If

        Catch ex As Exception
            strError = ex.Message
            Throw New Exception(strError)
        End Try
    End Function

    Public Sub CargaMunicipio(Optional ByVal intMunicipio As Integer = 0)
        Dim dtsRes As New DataSet
        Dim strError As String = String.Empty
        Try

            intMUN_CL_CVE = intMunicipio
            dtsRes = ManejaMunicipio(1)

            If Trim$(strError) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intEFD_CL_CVE = dtsRes.Tables(0).Rows(0).Item("EFD_CL_CVE")
                    intCIU_CL_CIUDAD = dtsRes.Tables(0).Rows(0).Item("CIU_CL_CIUDAD")
                    intMUN_CL_CVE = dtsRes.Tables(0).Rows(0).Item("MUN_CL_CVE")
                    strMUN_DS_MUNICIPIO = dtsRes.Tables(0).Rows(0).Item("MUN_DS_MUNICIPIO")
                    intMUN_FG_STATUS = dtsRes.Tables(0).Rows(0).Item("MUN_FG_STATUS")
                    strUSR_CL_CVE = dtsRes.Tables(0).Rows(0).Item("USR_CL_CVE")

                Else
                    strError = "No se encontró información para poder cargar el municipio"
                End If
            End If
        Catch ex As Exception
            strError = ex.Message
            Throw New Exception(strError)
        End Try
    End Sub
#End Region

End Class
