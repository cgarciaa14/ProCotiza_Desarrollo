'BBVA-P-412: 06-07-2016: AMR: RQ02,RQ03,RQ04,RQ05,RQ06,RQ08 SE AGREGA CLASE
'BBVA-P-412: 25-08-2016: AMR: RQCOT-01 Ajustes a la pantalla de Cotización.
'BUG-PC-26: JRHM: 21/12/2016 Se modifica opcion 8 de consulta de aseguradora para consultar paquete y alianza 0
'BUG-PC-30 AMR 03/01/2017: Al ingresar al portal procotiza e ingresar a la opcion de cotizacion manda un mensaje de warning.

Imports SDManejaBD

Public Class clsAseguradoras
    Inherits clsSession

    Private strErrAseguradora As String = String.Empty

    Private intAseguradora As Integer = 0
    Private strrazonsocial As String = String.Empty
    Private strNomCorto As String = String.Empty
    Private intRegDef As Integer = 0
    Private intIdExterno As Integer = 0
    Private intEstatus As Integer = 0
    Private strUsuReg As String = String.Empty
    Private intIdBroker As Integer = 0
    Private strBroker As String = String.Empty
    Private intIdPaquete As Integer = 0 'BBVA-P-412
    Private intidalianza As Integer = Nothing


    Sub New()
    End Sub

    Sub New(ByVal intCveAseguradora As Integer)
        CargaAseguradora(intCveAseguradora)
    End Sub

    Public ReadOnly Property ErrorAseguradora() As String
        Get
            Return strErrAseguradora
        End Get
    End Property

    Public Property IDAseguradora() As Integer
        Get
            Return intAseguradora
        End Get
        Set(ByVal value As Integer)
            intAseguradora = value
        End Set
    End Property

    Public Property RazonSocial() As String
        Get
            Return strrazonsocial
        End Get
        Set(ByVal value As String)
            strrazonsocial = value
        End Set
    End Property

    Public Property NomCorto() As String
        Get
            Return strNomCorto
        End Get
        Set(ByVal value As String)
            strNomCorto = value
        End Set
    End Property

    Public Property RegistroDefault() As Single
        Get
            Return intRegDef
        End Get
        Set(ByVal value As Single)
            intRegDef = value
        End Set
    End Property

    Public Property IDExterno() As String
        Get
            Return intIdExterno
        End Get
        Set(ByVal value As String)
            intIdExterno = value
        End Set
    End Property

    Public Property IDEstatus() As Integer
        Get
            Return intEstatus
        End Get
        Set(ByVal value As Integer)
            intEstatus = value
        End Set
    End Property

    Public Property IDBroker() As Integer
        Get
            Return intIdBroker
        End Get
        Set(value As Integer)
            intIdBroker = value
        End Set
    End Property

    Public Property NomBroker() As String
        Get
            Return strBroker
        End Get
        Set(value As String)
            strBroker = value
        End Set
    End Property

    Public Property UsuarioRegistro() As String
        Get
            Return strUsuReg
        End Get
        Set(ByVal value As String)
            strUsuReg = value
        End Set
    End Property
	
   'BBVA-P-412
    Public Property IDPaquete As Integer
        Get
            Return intIdPaquete
        End Get
        Set(value As Integer)
            intIdPaquete = value
        End Set
    End Property

    Public Property IDAlianza As Integer
        Get
            Return intidalianza
        End Get
        Set(value As Integer)
            intidalianza = value
        End Set
    End Property

    Public Sub CargaAseguradora(Optional ByVal intAseg As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intAseguradora = intAseg
            dtsRes = ManejaAseguradora(1)
            intAseguradora = 0
            If Trim$(strErrAseguradora) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intAseguradora = dtsRes.Tables(0).Rows(0).Item("ID_ASEGURADORA")
                    strrazonsocial = dtsRes.Tables(0).Rows(0).Item("RAZON_SOCIAL")
                    strNomCorto = dtsRes.Tables(0).Rows(0).Item("NOM_CORTO")
                    intRegDef = dtsRes.Tables(0).Rows(0).Item("REG_DEFAULT")
                    intIdExterno = dtsRes.Tables(0).Rows(0).Item("ID_EXTERNO")
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                Else
                    strErrAseguradora = "No se encontró información para poder cargar la aseguradora"
                End If
            End If
        Catch ex As Exception
            strErrAseguradora = ex.Message
        End Try
    End Sub
    Public Function ManejaAseguradora(ByVal intOper As Integer) As DataSet
        strErrAseguradora = ""
        ManejaAseguradora = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta aseguradora
                    If intAseguradora > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAseguradora", intAseguradora.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intEstatus.ToString)
                    If strrazonsocial.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "razonsocial", strrazonsocial)
                    If strNomCorto.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nomcorto", strNomCorto)
                    If intIdExterno > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "idexterno", intIdExterno.ToString)
                    If intIdBroker > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idBroker", intIdBroker.ToString)
                Case 2 ' inserta aseguradora
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "RegDefault", intRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "razonsocial", strrazonsocial)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nomcorto", strNomCorto)
                    If intIdExterno > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idExterno", intIdExterno.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usureg", strUsuReg)
                Case 3 ' actualiza aseguradora
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAseguradora", intAseguradora.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "RegDefault", intRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "razonsocial", strrazonsocial)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nomcorto", strNomCorto)
                    If intIdExterno > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idexterno", intIdExterno.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usureg", strUsuReg)
                Case 4 ' borra aseguradora
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAseguradora", intAseguradora.ToString)
                Case 5 'Consulta para Asignación de Broker
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAseguradora", intAseguradora.ToString)
                    If intIdBroker > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idBroker", intIdBroker.ToString)
                    If strBroker.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nomBroker", strBroker)
                Case 6 'Asigna de Broker
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAseguradora", intAseguradora.ToString)
                    If intIdBroker > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idBroker", intIdBroker.ToString)
                Case 7 'Borra de Broker
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAseguradora", intAseguradora.ToString)
                    If intIdBroker > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idBroker", intIdBroker.ToString)
                	'BBVA-P-412
                    Case 8 'Consulta Aseguradoras Cotizador
                    If intIdPaquete > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idpaquete", intIdPaquete.ToString)
                    If intidalianza >= 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idalianza", intidalianza.ToString)
            End Select

            ManejaAseguradora = objSD.EjecutaStoredProcedure("spManejaAseguradoras", strErrAseguradora, strParamStored)
            If strErrAseguradora = "" Then
                If intOper = 2 Then
                    intAseguradora = ManejaAseguradora.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("ASEGURADORAS", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrAseguradora = ex.Message
        End Try
    End Function
End Class
