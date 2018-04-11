Imports SDManejaBD

Public Class clsPais
    Inherits clsSession

#Region "Variables"

    ''' <summary>
    ''' Clave del pais
    ''' </summary>
    ''' <remarks></remarks>
    Private intPAI_FL_CVE As Integer = 0

    ''' <summary>
    ''' Nombre en ingles del pais
    ''' </summary>
    ''' <remarks></remarks>
    Private strPAI_DS_NOMBRE_INGLES As String = String.Empty

    ''' <summary>
    ''' Nombre en español del pais
    ''' </summary>
    ''' <remarks></remarks>
    Private strPAI_DS_NOMBRE_ESPANOL As String = String.Empty

    ''' <summary>
    ''' Numero de 
    ''' </summary>
    ''' <remarks></remarks>
    Private intPAI_NO_APALANCAMIENTO As Integer = 0

    ''' <summary>
    ''' Clave hereda renta
    ''' </summary>
    ''' <remarks></remarks>
    Private intPAI_FG_HEREDAR_RENTA As Integer = 0

    ''' <summary>
    ''' Cuenta contable del pais
    ''' </summary>
    ''' <remarks></remarks>
    Private strPAI_DS_CUENTA_CONTABLE As String = ""

    ''' <summary>
    ''' Obliga a que tenga codigo postal
    ''' </summary>
    ''' <remarks></remarks>
    Private intPAI_FG_OBLIGA_CP As Integer = 0

    ''' <summary>
    ''' Pais por default 1 es por default
    ''' </summary>
    ''' <remarks></remarks>
    Private intPAI_FG_REGDEFAULT As Integer = 0

    ''' <summary>
    ''' Si aplica iva el pais
    ''' </summary>
    ''' <remarks></remarks>
    Private intPAI_FG_APLICA_IVA As Integer = 0

    ''' <summary>
    ''' Clave del usuario
    ''' </summary>
    ''' <remarks></remarks>
    Private strUSR_CL_CVE As String = String.Empty

    ''' <summary>
    ''' Fecha de la última modificación al pais
    ''' </summary>
    ''' <remarks></remarks>
    Private datePAI_FE_ULTMOD As DateTime = "1900-01-01"

    ''' <summary>
    ''' Pais calcula ROE
    ''' </summary>
    ''' <remarks></remarks>
    Private intPAI_NO_PROVISION_ROE As Integer = 0

    ''' <summary>
    ''' Servico del mail por pais
    ''' </summary>
    ''' <remarks></remarks>
    Private strPAI_DS_EMAIL_SRVCTE As String = String.Empty

    ''' <summary>
    ''' Servico de telefono por pais
    ''' </summary>
    ''' <remarks></remarks>
    Private strPAI_DS_TEL_SRVCTE As String = String.Empty

    ''' <summary>
    ''' Clave si aplica gastos de cobranza el pais
    ''' </summary>
    ''' <remarks></remarks>
    Private intPAI_FG_APLICA_GCOB As Integer = 0

    ''' <summary>
    ''' Dias de renta por pais
    ''' </summary>
    ''' <remarks></remarks>
    Private intPAI_FG_DIAS_RENTA As Integer = 0

#End Region

#Region "Propiedades"


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
    ''' Nombre en ingles del pais
    ''' </summary>
    ''' <remarks></remarks>
    Public Property _strPAI_DS_NOMBRE_INGLES() As String
        Get
            Return strPAI_DS_NOMBRE_INGLES
        End Get
        Set(ByVal value As String)
            strPAI_DS_NOMBRE_INGLES = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre en español del pais
    ''' </summary>
    ''' <remarks></remarks>
    Public Property _PAI_DS_NOMBRE_ESPANOL() As String
        Get
            Return strPAI_DS_NOMBRE_ESPANOL
        End Get
        Set(ByVal value As String)
            strPAI_DS_NOMBRE_ESPANOL = value
        End Set
    End Property

    ''' <summary>
    ''' Numero de 
    ''' </summary>
    ''' <remarks></remarks>
    Public Property _intPAI_NO_APALANCAMIENTO() As Integer
        Get
            Return intPAI_NO_APALANCAMIENTO
        End Get
        Set(ByVal value As Integer)
            intPAI_NO_APALANCAMIENTO = value
        End Set
    End Property

    ''' <summary>
    ''' Clave hereda renta
    ''' </summary>
    ''' <remarks></remarks>
    Public Property _intPAI_FG_HEREDAR_RENTA() As Integer
        Get
            Return intPAI_FG_HEREDAR_RENTA
        End Get
        Set(ByVal value As Integer)
            intPAI_FG_HEREDAR_RENTA = value
        End Set
    End Property

    ''' <summary>
    ''' Cuenta contable del pais
    ''' </summary>
    ''' <remarks></remarks>
    Public Property _strPAI_DS_CUENTA_CONTABLE() As String
        Get
            Return strPAI_DS_CUENTA_CONTABLE
        End Get
        Set(ByVal value As String)
            strPAI_DS_CUENTA_CONTABLE = value
        End Set
    End Property

    ''' <summary>
    ''' Obliga a que tenga codigo postal
    ''' </summary>
    ''' <remarks></remarks>
    Public Property _intPAI_FG_OBLIGA_CP() As Integer
        Get
            Return intPAI_FG_OBLIGA_CP
        End Get
        Set(ByVal value As Integer)
            intPAI_FG_OBLIGA_CP = value
        End Set
    End Property

    ''' <summary>
    ''' Pais por default 1 es por default
    ''' </summary>
    ''' <remarks></remarks>
    Public Property _intPAI_FG_REGDEFAULT() As Integer
        Get
            Return intPAI_FG_REGDEFAULT
        End Get
        Set(ByVal value As Integer)
            intPAI_FG_REGDEFAULT = value
        End Set
    End Property

    ''' <summary>
    ''' Si aplica iva el pais
    ''' </summary>
    ''' <remarks></remarks>
    Public Property _intPAI_FG_APLICA_IVA() As Integer
        Get
            Return intPAI_FG_APLICA_IVA
        End Get
        Set(ByVal value As Integer)
            intPAI_FG_APLICA_IVA = value
        End Set
    End Property

    ''' <summary>
    ''' Clave del usuario
    ''' </summary>
    ''' <remarks></remarks>
    Public Property _strUSR_CL_CVE() As String
        Get
            Return strUSR_CL_CVE
        End Get
        Set(ByVal value As String)
            strUSR_CL_CVE = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha de la última modificación al pais
    ''' </summary>
    ''' <remarks></remarks>
    Public Property _datePAI_FE_ULTMOD() As DateTime
        Get
            Return datePAI_FE_ULTMOD
        End Get
        Set(ByVal value As DateTime)
            datePAI_FE_ULTMOD = value
        End Set
    End Property

    ''' <summary>
    ''' Pais calcula ROE
    ''' </summary>
    ''' <remarks></remarks>
    Public Property _intPAI_NO_PROVISION_ROE() As Integer
        Get
            Return intPAI_NO_PROVISION_ROE
        End Get
        Set(ByVal value As Integer)
            intPAI_NO_PROVISION_ROE = value
        End Set
    End Property

    ''' <summary>
    ''' Servico del mail por pais
    ''' </summary>
    ''' <remarks></remarks>
    Public Property _strPAI_DS_EMAIL_SRVCTE() As String
        Get
            Return strPAI_DS_EMAIL_SRVCTE
        End Get
        Set(ByVal value As String)
            strPAI_DS_EMAIL_SRVCTE = value
        End Set
    End Property

    ''' <summary>
    ''' Clave si aplica gastos de cobranza el pais
    ''' </summary>
    ''' <remarks></remarks>
    Public Property _intPAI_FG_APLICA_GCOB() As Integer
        Get
            Return intPAI_FG_APLICA_GCOB
        End Get
        Set(ByVal value As Integer)
            intPAI_FG_APLICA_GCOB = value
        End Set
    End Property

    ''' <summary>
    ''' Dias de renta por pais
    ''' </summary>
    ''' <remarks></remarks>
    Public Property _intPAI_FG_DIAS_RENTA() As Integer
        Get
            Return intPAI_FG_DIAS_RENTA
        End Get
        Set(ByVal value As Integer)
            intPAI_FG_DIAS_RENTA = value
        End Set
    End Property

    ''' <summary>
    ''' Servicio de telefono por pais
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _strPAI_DS_TEL_SRVCTE() As String
        Get
            Return strPAI_DS_TEL_SRVCTE
        End Get
        Set(ByVal value As String)
            strPAI_DS_TEL_SRVCTE = value
        End Set
    End Property

#End Region

#Region "Metodos"

    Sub New()
    End Sub

    Sub New(ByVal intPais As Integer)
        CargaPais(intPais)
    End Sub

    Public Function ManejaPais(ByVal intOper As Integer) As DataSet

        ManejaPais = New DataSet
        Dim strParamStored As String = ""
        Dim strError As String = String.Empty
        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "Opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta empresa
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_FL_CVE", intPAI_FL_CVE.ToString)
                Case 2 ' inserta empresa
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_FL_CVE", intPAI_FL_CVE.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "PAI_DS_NOMBRE_INGLES", strPAI_DS_NOMBRE_INGLES.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "PAI_DS_NOMBRE_ESPANOL", strPAI_DS_NOMBRE_ESPANOL.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_NO_APALANCAMIENTO", intPAI_NO_APALANCAMIENTO)
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_FG_HEREDAR_RENTA", intPAI_FG_HEREDAR_RENTA)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "PAI_DS_CUENTA_CONTABLE", strPAI_DS_CUENTA_CONTABLE)
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_FG_OBLIGA_CP", intPAI_FG_OBLIGA_CP)
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_FG_REGDEFAULT", intPAI_FG_REGDEFAULT.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_FG_APLICA_IVA", intPAI_FG_APLICA_IVA.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "USR_CL_CVE", strUSR_CL_CVE)
                    ArmaParametros(strParamStored, TipoDato.Fecha, "PAI_FE_ULTMOD", datePAI_FE_ULTMOD)
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_NO_PROVISION_ROE", intPAI_NO_PROVISION_ROE)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "PAI_DS_EMAIL_SRVCTE", strPAI_DS_EMAIL_SRVCTE)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "PAI_DS_TEL_SRVCTE", strPAI_DS_TEL_SRVCTE)
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_FG_APLICA_GCOB", intPAI_FG_APLICA_GCOB)
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_FG_DIAS_RENTA", intPAI_FG_DIAS_RENTA)
                Case 3 ' actualiza empresa
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_FL_CVE", intPAI_FL_CVE.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "PAI_DS_NOMBRE_INGLES", strPAI_DS_NOMBRE_INGLES.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "PAI_DS_NOMBRE_ESPANOL", strPAI_DS_NOMBRE_ESPANOL.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_NO_APALANCAMIENTO", intPAI_NO_APALANCAMIENTO)
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_FG_HEREDAR_RENTA", intPAI_FG_HEREDAR_RENTA)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "PAI_DS_CUENTA_CONTABLE", strPAI_DS_CUENTA_CONTABLE)
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_FG_OBLIGA_CP", intPAI_FG_OBLIGA_CP)
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_FG_REGDEFAULT", intPAI_FG_REGDEFAULT.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_FG_APLICA_IVA", intPAI_FG_APLICA_IVA.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "USR_CL_CVE", strUSR_CL_CVE)
                    ArmaParametros(strParamStored, TipoDato.Fecha, "PAI_FE_ULTMOD", datePAI_FE_ULTMOD)
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_NO_PROVISION_ROE", intPAI_NO_PROVISION_ROE)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "PAI_DS_EMAIL_SRVCTE", strPAI_DS_EMAIL_SRVCTE)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "PAI_DS_TEL_SRVCTE", strPAI_DS_EMAIL_SRVCTE)
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_FG_APLICA_GCOB", intPAI_FG_APLICA_GCOB)
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_FG_DIAS_RENTA", intPAI_FG_DIAS_RENTA)
                Case 4 ' borra empresa
                    ArmaParametros(strParamStored, TipoDato.Entero, "PAI_FL_CVE", intPAI_FL_CVE.ToString)
            End Select

            ManejaPais = objSD.EjecutaStoredProcedure("spManejaPais", strError, strParamStored)
            If strError = "" Then
                If intOper = 2 Then
                    intPAI_FL_CVE = ManejaPais.Tables(0).Rows(0).Item(0)
                End If
            End If

        Catch ex As Exception
            strError = ex.Message
            Throw New Exception(strError)
        End Try
    End Function

    Public Sub CargaPais(Optional ByVal intPais As Integer = 0)
        Dim dtsRes As New DataSet
        Dim strError As String = String.Empty
        Try

            intPAI_FL_CVE = intPais
            dtsRes = ManejaPais(1)

            If Trim$(strError) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then

                    intPAI_FL_CVE = dtsRes.Tables(0).Rows(0).Item("PAI_FL_CVE")
                    strPAI_DS_NOMBRE_INGLES = dtsRes.Tables(0).Rows(0).Item("PAI_DS_NOMBRE_INGLES")
                    strPAI_DS_NOMBRE_ESPANOL = dtsRes.Tables(0).Rows(0).Item("PAI_DS_NOMBRE_ESPANOL")
                    intPAI_NO_APALANCAMIENTO = dtsRes.Tables(0).Rows(0).Item("PAI_NO_APALANCAMIENTO")
                    intPAI_FG_HEREDAR_RENTA = dtsRes.Tables(0).Rows(0).Item("PAI_FG_HEREDAR_RENTA")
                    strPAI_DS_CUENTA_CONTABLE = dtsRes.Tables(0).Rows(0).Item("PAI_DS_CUENTA_CONTABLE")
                    intPAI_FG_OBLIGA_CP = dtsRes.Tables(0).Rows(0).Item("PAI_FG_APLICA_IVA")
                    intPAI_FG_REGDEFAULT = dtsRes.Tables(0).Rows(0).Item("PAI_FG_REGDEFAULT")
                    intPAI_FG_APLICA_IVA = dtsRes.Tables(0).Rows(0).Item("PAI_FG_APLICA_IVA")
                    strUSR_CL_CVE = dtsRes.Tables(0).Rows(0).Item("USR_CL_CVE")
                    datePAI_FE_ULTMOD = dtsRes.Tables(0).Rows(0).Item("PAI_FE_ULTMOD")
                    intPAI_NO_PROVISION_ROE = dtsRes.Tables(0).Rows(0).Item("PAI_NO_PROVISION_ROE")
                    strPAI_DS_EMAIL_SRVCTE = dtsRes.Tables(0).Rows(0).Item("PAI_DS_EMAIL_SRVCTE")
                    strPAI_DS_TEL_SRVCTE = dtsRes.Tables(0).Rows(0).Item("PAI_DS_TEL_SRVCTE")
                    intPAI_FG_APLICA_GCOB = dtsRes.Tables(0).Rows(0).Item("PAI_FG_APLICA_GCOB")
                    intPAI_FG_DIAS_RENTA = dtsRes.Tables(0).Rows(0).Item("PAI_FG_DIAS_RENTA")
                Else
                    strError = "No se encontró información para poder cargar la empresa"
                End If
            End If
        Catch ex As Exception
            strError = ex.Message
            Throw New Exception(strError)
        End Try
    End Sub

#End Region

End Class
