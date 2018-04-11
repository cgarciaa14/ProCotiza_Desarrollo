Imports SDManejaBD
Public Class clsEntidadFederativa
    Inherits clsSession
#Region "Variables"

    ''' <summary>
    ''' Clave de la entidad federativa
    ''' </summary>
    ''' <remarks></remarks>
    Private intEFD_CL_CVE As Integer = 0
    Private intPAI_FL_CVE As Integer = 0
    Private strEFD_DS_ENTIDAD As String = String.Empty
    Private intEFD_FG_STATUS As Integer = 0
    Private intEFD_CL_BNC As Integer = 0
    Private strEFD_DS_BANXICO As String = String.Empty
    Private decEFD_TARIFADDR As Decimal = 0.0
    Private decEFD_TARIFADDR_US As Decimal = 0.0
    Private strEFD_DS_SEPOMEX As String = String.Empty
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
    ''' Clave del pais
    ''' </summary>
    ''' <remarks></remarks>
    Public Property _intPAI_FL_CVE() As Integer
        Get
            Return intPAI_FL_CVE
        End Get
        Set(ByVal value As Integer)
            intPAI_FL_CVE = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Public Property _strEFD_DS_ENTIDAD() As String
        Get
            Return strEFD_DS_ENTIDAD
        End Get
        Set(ByVal value As String)
            strEFD_DS_ENTIDAD = value
        End Set
    End Property

    ''' <summary>
    ''' Clave del status de de la entidad federativa
    ''' </summary>
    ''' <remarks></remarks>
    Public Property _intEFD_FG_STATUS() As Integer
        Get
            Return intEFD_FG_STATUS
        End Get
        Set(ByVal value As Integer)
            intEFD_FG_STATUS = value
        End Set
    End Property

    ''' <summary>
    ''' Clave banco entidad federativa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _intEFD_CL_BNC() As Integer
        Get
            Return intEFD_CL_BNC
        End Get
        Set(ByVal value As Integer)
            intEFD_CL_BNC = value
        End Set
    End Property

    ''' <summary>
    ''' Descripcion de la clave de banxico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _strEFD_DS_BANXICO() As String
        Get
            Return strEFD_DS_BANXICO
        End Get
        Set(ByVal value As String)
            strEFD_DS_BANXICO = value
        End Set
    End Property

    ''' <summary>
    ''' Tarifador de la entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decEFD_TARIFADDR() As Decimal
        Get
            Return decEFD_TARIFADDR
        End Get
        Set(ByVal value As Decimal)
            decEFD_TARIFADDR = value
        End Set
    End Property

    ''' <summary>
    ''' Tarifiador US
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _decEFD_TARIFADDR_US() As Decimal
        Get
            Return decEFD_TARIFADDR_US
        End Get
        Set(ByVal value As Decimal)
            value = value
        End Set
    End Property

    ''' <summary>
    ''' Identificador de sepomex
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _strEFD_DS_SEPOMEX() As String
        Get
            Return strEFD_DS_SEPOMEX
        End Get
        Set(ByVal value As String)
            strEFD_DS_SEPOMEX = value
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

    Sub New(ByVal intEntidad As Integer)
        CargaEntidad(intEntidad)
    End Sub

    Public Function ManejaEntidad(ByVal intOper As Integer) As DataSet

        ManejaEntidad = New DataSet
        Dim strParamStored As String = ""
        Dim strError As String = String.Empty
        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "Opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta entidad
                    If intEFD_CL_CVE > 0 Then
                        ArmaParametros(strParamStored, TipoDato.Entero, "EFD_CL_CVE", intEFD_CL_CVE.ToString)
                    End If

                Case 2 ' inserta entidad
                    ArmaParametros(strParamStored, TipoDato.Entero, "EFD_CL_CVE", intPAI_FL_CVE.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_FL_CVE", intPAI_FL_CVE.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "EFD_DS_ENTIDAD", strEFD_DS_ENTIDAD.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "EFD_FG_STATUS", intEFD_FG_STATUS)
                    ArmaParametros(strParamStored, TipoDato.Entero, "EFD_CL_BNC", intEFD_CL_BNC)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "EFD_DS_BANXICO", strEFD_DS_BANXICO)
                    ArmaParametros(strParamStored, TipoDato.Doble, "EFD_TARIFADDR", decEFD_TARIFADDR)
                    ArmaParametros(strParamStored, TipoDato.Doble, "FD_TARIFADDR_US", decEFD_TARIFADDR_US.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "EFD_DS_SEPOMEX", strEFD_DS_SEPOMEX.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "USR_CL_CVE", strUSR_CL_CVE)

                Case 3 ' actualiza entidad
                    ArmaParametros(strParamStored, TipoDato.Entero, "EFD_CL_CVE", intEFD_CL_CVE.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_FL_CVE", intPAI_FL_CVE.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "EFD_DS_ENTIDAD", strEFD_DS_ENTIDAD.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "EFD_FG_STATUS", intEFD_FG_STATUS)
                    ArmaParametros(strParamStored, TipoDato.Entero, "EFD_CL_BNC", intEFD_CL_BNC)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "EFD_DS_BANXICO", strEFD_DS_BANXICO)
                    ArmaParametros(strParamStored, TipoDato.Doble, "EFD_TARIFADDR", decEFD_TARIFADDR)
                    ArmaParametros(strParamStored, TipoDato.Doble, "EFD_TARIFADDR_US", decEFD_TARIFADDR_US)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "EFD_DS_SEPOMEX", strEFD_DS_SEPOMEX.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "USR_CL_CVE", strUSR_CL_CVE)

                Case 4 ' borra entidad
                    ArmaParametros(strParamStored, TipoDato.Entero, "EFD_CL_CVE", intEFD_CL_CVE.ToString)
            End Select

            ManejaEntidad = objSD.EjecutaStoredProcedure("spManejaEntidadFederativa", strError, strParamStored)
            If strError = "" Then
                If intOper = 2 Then
                    intEFD_CL_CVE = ManejaEntidad.Tables(0).Rows(0).Item(0)
                End If
            End If

        Catch ex As Exception
            strError = ex.Message
            Throw New Exception(strError)
        End Try
    End Function

    Public Sub CargaEntidad(Optional ByVal intEntidad As Integer = 0)
        Dim dtsRes As New DataSet
        Dim strError As String = String.Empty
        Try

            intEFD_CL_CVE = intEntidad
            dtsRes = ManejaEntidad(1)

            If Trim$(strError) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then


                    intEFD_CL_CVE = dtsRes.Tables(0).Rows(0).Item("EFD_CL_CVE")
                    intPAI_FL_CVE = dtsRes.Tables(0).Rows(0).Item("PAI_FL_CVE")
                    strEFD_DS_ENTIDAD = dtsRes.Tables(0).Rows(0).Item("EFD_DS_ENTIDAD")
                    intEFD_FG_STATUS = dtsRes.Tables(0).Rows(0).Item("EFD_FG_STATUS")
                    intEFD_CL_BNC = dtsRes.Tables(0).Rows(0).Item("EFD_CL_BNC")
                    strEFD_DS_BANXICO = dtsRes.Tables(0).Rows(0).Item("EFD_DS_BANXICO")
                    decEFD_TARIFADDR = dtsRes.Tables(0).Rows(0).Item("EFD_TARIFADDR")
                    decEFD_TARIFADDR_US = dtsRes.Tables(0).Rows(0).Item("EFD_TARIFADDR_US")
                    strEFD_DS_SEPOMEX = dtsRes.Tables(0).Rows(0).Item("EFD_DS_SEPOMEX")
                    strUSR_CL_CVE = dtsRes.Tables(0).Rows(0).Item("USR_CL_CVE")

                Else
                    strError = "No se encontró información para poder cargar la entidad"
                End If
            End If
        Catch ex As Exception
            strError = ex.Message
            Throw New Exception(strError)
        End Try
    End Sub

#End Region

End Class
