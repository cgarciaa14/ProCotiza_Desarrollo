'RQ-PI7-PC6: CGARCIA: 19/10/2017: SE AGREGA OPCION DE BUSQUEDA

Imports SDManejaBD

Public Class clsCodigoPostal
    Inherits clsSession

#Region "Trackers"
#End Region

#Region "Variables"

    Private intCPO_FL_CP As Integer = 0
    Private intCIU_CL_CIUDAD As Integer = 0
    Private intEFD_CL_CVE As Integer = 0
    Private intMUN_CL_CVE As Integer = 0
    Private strCPO_CL_CODPOSTAL As String = String.Empty
    Private strCPO_DS_COLONIA As String = String.Empty
    Private intCPO_FG_STATUS As Integer = 0
    Private strUSR_CL_CVE As String = String.Empty

#End Region

#Region "Propiedades"

    ''' <summary>
    ''' Clave del codigo postal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _intCPO_FL_CP() As Integer
        Get
            Return intCPO_FL_CP
        End Get
        Set(ByVal value As Integer)
            intCPO_FL_CP = value
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
    ''' Clave del municipio
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
    ''' Numero de codigo postal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _strCPO_CL_CODPOSTAL() As String
        Get
            Return strCPO_CL_CODPOSTAL
        End Get
        Set(ByVal value As String)
            strCPO_CL_CODPOSTAL = value
        End Set
    End Property

    ''' <summary>
    ''' Colonia que pertenece al codigo postal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _strCPO_DS_COLONIA() As String
        Get
            Return strCPO_DS_COLONIA
        End Get
        Set(ByVal value As String)
            strCPO_DS_COLONIA = value
        End Set
    End Property

    ''' <summary>
    ''' Status del codigo postal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _intCPO_FG_STATUS() As Integer
        Get
            Return intCPO_FG_STATUS
        End Get
        Set(ByVal value As Integer)
            intCPO_FG_STATUS = value
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

    Sub New(ByVal intCodPost As Integer)
        CargaCotPost(intCodPost)
    End Sub

    Public Function ManejaCotPost(ByVal intOper As Integer) As DataSet

        ManejaCotPost = New DataSet
        Dim strParamStored As String = ""
        Dim strError As String = String.Empty
        Try
            Dim objSD As New clsConexion


            ArmaParametros(strParamStored, TipoDato.Entero, "Opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta entidad
                    If intCPO_FL_CP > 0 Then
                        ArmaParametros(strParamStored, TipoDato.Entero, "CPO_FL_CP", intCPO_FL_CP.ToString)
                    End If
                    If intEFD_CL_CVE > 0 Then
                        ArmaParametros(strParamStored, TipoDato.Entero, "EFD_CL_CVE", intEFD_CL_CVE.ToString)
                    End If
                Case 2 ' inserta entidad
                    ArmaParametros(strParamStored, TipoDato.Entero, "EFD_CL_CVE", intEFD_CL_CVE.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "CIU_CL_CIUDAD", intCIU_CL_CIUDAD.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "CPO_FL_CP", intCPO_FL_CP.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "MUN_CL_CVE", intMUN_CL_CVE)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "CPO_CL_CODPOSTAL", strCPO_CL_CODPOSTAL.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "CPO_DS_COLONIA", strCPO_DS_COLONIA)
                    ArmaParametros(strParamStored, TipoDato.Entero, "CPO_FG_STATUS", intCPO_FG_STATUS)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "USR_CL_CVE", strUSR_CL_CVE)

                Case 3 ' actualiza entidad
                    ArmaParametros(strParamStored, TipoDato.Entero, "EFD_CL_CVE", intEFD_CL_CVE.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "CIU_CL_CIUDAD", intCIU_CL_CIUDAD.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "CPO_FL_CP", intCPO_FL_CP.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "MUN_CL_CVE", intMUN_CL_CVE)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "CPO_CL_CODPOSTAL", strCPO_CL_CODPOSTAL.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "CPO_DS_COLONIA", strCPO_DS_COLONIA)
                    ArmaParametros(strParamStored, TipoDato.Entero, "CPO_FG_STATUS", intCPO_FG_STATUS)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "USR_CL_CVE", strUSR_CL_CVE)

                Case 4 ' borra entidad
                    ArmaParametros(strParamStored, TipoDato.Entero, "CPO_FL_CP", intCPO_FL_CP.ToString)
                Case 5 ' Obtiene colonias
                    If strCPO_CL_CODPOSTAL.Length > 0 Then
                        ArmaParametros(strParamStored, TipoDato.Cadena, "CPO_CL_CODPOSTAL", strCPO_CL_CODPOSTAL.ToString)
                    End If
                    If intEFD_CL_CVE > 0 Then
                        ArmaParametros(strParamStored, TipoDato.Entero, "EFD_CL_CVE", intEFD_CL_CVE.ToString)
                    End If
                Case 6 ' Recupera Estado, Ciudad y Municipio por Código Postal
                    ArmaParametros(strParamStored, TipoDato.Cadena, "CPO_CL_CODPOSTAL", strCPO_CL_CODPOSTAL.ToString)
                Case 7 ' Recupera Colonias por Código Postal
                    ArmaParametros(strParamStored, TipoDato.Cadena, "CPO_CL_CODPOSTAL", strCPO_CL_CODPOSTAL.ToString)                    
                Case 8 'recupera EF, municipio y colonia
                    ArmaParametros(strParamStored, TipoDato.Cadena, "CPO_CL_CODPOSTAL", strCPO_CL_CODPOSTAL.ToString)              
            End Select

            ManejaCotPost = objSD.EjecutaStoredProcedure("spManejaCodPostal", strError, strParamStored)
            If strError = "" Then
                If intOper = 2 Then
                    intCPO_FL_CP = ManejaCotPost.Tables(0).Rows(0).Item(0)
                End If
            End If

        Catch ex As Exception
            strError = ex.Message
            Throw New Exception(strError)
        End Try
    End Function

    Public Sub CargaCotPost(Optional ByVal intCodPost As Integer = 0)
        Dim dtsRes As New DataSet
        Dim strError As String = String.Empty
        Try

            intCPO_FL_CP = intCodPost
            dtsRes = ManejaCotPost(1)

            If Trim$(strError) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then

                    intCPO_FL_CP = dtsRes.Tables(0).Rows(0).Item("CPO_FL_CP")
                    intEFD_CL_CVE = dtsRes.Tables(0).Rows(0).Item("EFD_CL_CVE")
                    intCIU_CL_CIUDAD = dtsRes.Tables(0).Rows(0).Item("CIU_CL_CIUDAD")
                    intMUN_CL_CVE = dtsRes.Tables(0).Rows(0).Item("MUN_CL_CVE")
                    strCPO_CL_CODPOSTAL = dtsRes.Tables(0).Rows(0).Item("CPO_CL_CODPOSTAL")
                    strCPO_DS_COLONIA = dtsRes.Tables(0).Rows(0).Item("CPO_DS_COLONIA")
                    intCPO_FG_STATUS = dtsRes.Tables(0).Rows(0).Item("CPO_FG_STATUS")
                    strUSR_CL_CVE = dtsRes.Tables(0).Rows(0).Item("USR_CL_CVE")

                Else
                    strError = "No se encontró información para poder cargar el codigo postal"
                End If
            End If
        Catch ex As Exception
            strError = ex.Message
            Throw New Exception(strError)
        End Try
    End Sub

#End Region

End Class
