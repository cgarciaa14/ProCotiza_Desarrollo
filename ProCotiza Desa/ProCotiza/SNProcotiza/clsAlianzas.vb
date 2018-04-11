'BBV-P-412:AVH:28/06/2016 RQ16: SE CREA CATALOGO ALIANZAS
'BBVA-P-412: 25-08-2016: AMR: RQCOT-01 Ajustes a la pantalla de Cotización.
'BBV-P-412_RQCOT-05:AVH:31/08/2016 SE AGREGA RUTA DE IMAGEN PARA EL REPORTE
'BUG-PC-06: AVH: 14/11/2016 Se cambia valor por default de intAlianzaFiltro
'BUG-PC-30 AMR 03/01/2017: Al ingresar al portal procotiza e ingresar a la opcion de cotizacion manda un mensaje de warning.
'BUG-PC-38 MAUT 23/01/2017 Se quita campo de imagen reporte
'BBV-P412 RQCOT-01.2: AMR 23/02/2017: Seguros.
'BBV-P-412 RQADM-04 ERV 20/04/2017 Se agrego funcionalidad para VALIDA_PROSPECTOR

Imports SDManejaBD
Public Class clsAlianzas
    Inherits clsSession

    Private intAlianza As Integer = -1
    Private strErrAlianza As String = ""
    Private strAlianza As String = ""
    Private strDescripcion As String = ""
    Private strURL As String = ""
    Private intEstatus As Integer = 0
    Private intValidaProspector As Integer = 0

    Private sngRegDef As Single = 0

    Private intAgencia As Integer = 0
    Private strAgencia As String = ""

    Private intAlianzaFiltro As Integer = -1


    Private strUsuReg As String = ""
    Private strImgRep As String = ""

    Private intidBroker As Integer = 0
    Private intidusuario As Integer = 0
    Private intidedo As Integer = 0


    Public Property IDAlianzaFiltro() As Integer
        Get
            Return intAlianzaFiltro
        End Get
        Set(ByVal value As Integer)
            intAlianzaFiltro = value
        End Set
    End Property
    Public Property IDAlianza() As Integer
        Get
            Return intAlianza
        End Get
        Set(ByVal value As Integer)
            intAlianza = value
        End Set
    End Property
    Public Property IDAgencia() As Integer
        Get
            Return intAgencia
        End Get
        Set(ByVal value As Integer)
            intAgencia = value
        End Set
    End Property
    Public Property Agencia() As String
        Get
            Return strAgencia
        End Get
        Set(ByVal value As String)
            strAgencia = value
        End Set
    End Property

    Public Property Alianza() As String
        Get
            Return strAlianza
        End Get
        Set(ByVal value As String)
            strAlianza = value
        End Set
    End Property
    Public Property Descripcion() As String
        Get
            Return strDescripcion
        End Get
        Set(ByVal value As String)
            strDescripcion = value
        End Set
    End Property
    Public Property URL As String
        Get
            Return strURL
        End Get
        Set(ByVal value As String)
            strURL = value
        End Set
    End Property
    Public Property Valida_Prospector As Integer
        Get
            Return intValidaProspector
        End Get
        Set(ByVal value As Integer)
            intValidaProspector = value
        End Set
    End Property
    Public Property Estatus As Integer
        Get
            Return intEstatus
        End Get
        Set(ByVal value As Integer)
            intEstatus = value
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
    Public ReadOnly Property ErrorAlianza() As String
        Get
            Return strErrAlianza
        End Get
    End Property
    Public WriteOnly Property UsuarioRegistro() As String
        Set(ByVal value As String)
            strUsuReg = value
        End Set
    End Property


    'Buscar
    Public Property IDEstatus() As Integer
        Get
            Return intEstatus
        End Get
        Set(ByVal value As Integer)
            intEstatus = value
        End Set
    End Property
    Public Property Img_Rep() As String
        Get
            Return strImgRep
        End Get
        Set(ByVal value As String)
            strImgRep = value
        End Set
    End Property

Public Property IDBroker As Integer
        Get
            Return intidBroker
        End Get
        Set(value As Integer)
            intidBroker = value
        End Set
    End Property

    Public Property IDUsuario As Integer
        Get
            Return intidusuario
        End Get
        Set(value As Integer)
            intidusuario = value
        End Set
    End Property

    Public Property IDEdo As Integer
        Get
            Return intidedo
        End Get
        Set(value As Integer)
            intidedo = value
        End Set
    End Property


    Public Function ManejaAlianza(ByVal intOper As Integer) As DataSet
        strErrAlianza = ""
        ManejaAlianza = New DataSet
        Dim strParamStored As String = ""
        Try
            Dim objSD As New clsConexion

            'BUG-PC-38 MAUT 23/01/2017 Se quitan parametros de imagen reporte

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta Alianza
                    If intAlianza > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", intAlianza.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    If strAlianza <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Alianza", strAlianza.ToString)

                Case 2 ' inserta Alianza
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", intAlianza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Alianza", strAlianza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Descripcion", strDescripcion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "URL", strURL.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Valida_Prospector", intValidaProspector.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    'ArmaParametros(strParamStored, TipoDato.Cadena, "Img_Rep", strImgRep)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idbroker", intidBroker.ToString)
                Case 3 ' actualiza Alianza
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", intAlianza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Alianza", strAlianza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Descripcion", strDescripcion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "URL", strURL.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Valida_Prospector", intValidaProspector.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    'If strImgRep <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Img_Rep", strImgRep)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idbroker", intidBroker.ToString)
                Case 4 ' borra Alianza
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", intAlianza.ToString)
                Case 7 'Relación Agencia-Alianza --> Consulta
                    If intAlianzaFiltro >= 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAlianzaFiltro", intAlianzaFiltro.ToString)
                    If intAlianza > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", intAlianza.ToString)
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If strAgencia <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Agencia", strAgencia.ToString)
                Case 8 'Relación Agencia-Alianza --> Borra
                    If intAlianzaFiltro >= 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAlianzaFiltro", intAlianzaFiltro.ToString)
                    If intAlianza > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", intAlianza.ToString)
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If strAgencia <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Agencia", strAgencia.ToString)
                Case 9 'Relación Agencia-Alianza --> Inserta
                    If intAlianzaFiltro >= 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAlianzaFiltro", intAlianzaFiltro.ToString)
                    If intAlianza > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", intAlianza.ToString)
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
                    If strAgencia <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Agencia", strAgencia.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 10 'Cotizador - consulta Alianzas
                    ArmaParametros(strParamStored, TipoDato.Entero, "idusuario", intidusuario.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idedo", intidedo.ToString)
                Case 11 'Consulta cotizaciones - consulta Alianzas
                    ArmaParametros(strParamStored, TipoDato.Entero, "idusuario", intidusuario.ToString)
                    If intAgencia > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", intAgencia.ToString)
            End Select

            ManejaAlianza = objSD.EjecutaStoredProcedure("spManejaAlianzas", strErrAlianza, strParamStored)
            If strErrAlianza = "" Then
                If intOper = 2 Then
                    intAlianza = ManejaAlianza.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("Alianza", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrAlianza = ex.Message
        End Try
    End Function
    Public Sub CargaAlianza(Optional ByVal intAse As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intAlianza = intAse
            dtsRes = ManejaAlianza(1)
            intAlianza = 0
            If Trim$(strErrAlianza) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intAlianza = dtsRes.Tables(0).Rows(0).Item("ID_ALIANZA")
                    strAlianza = dtsRes.Tables(0).Rows(0).Item("ALIANZA").ToString
                    strDescripcion = dtsRes.Tables(0).Rows(0).Item("DESCRIPCION").ToString
                    strURL = dtsRes.Tables(0).Rows(0).Item("URL").ToString
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    intValidaProspector = dtsRes.Tables(0).Rows(0).Item("VALIDA_PROSPECTOR")
                    sngRegDef = dtsRes.Tables(0).Rows(0).Item("REG_DEFAULT")
                    intidBroker = dtsRes.Tables(0).Rows(0).Item("ID_BROKER")
                Else
                    strErrAlianza = "No se encontró información para poder cargar la Alianza"
                End If
            End If
        Catch ex As Exception
            strErrAlianza = ex.Message
        End Try
    End Sub
End Class
