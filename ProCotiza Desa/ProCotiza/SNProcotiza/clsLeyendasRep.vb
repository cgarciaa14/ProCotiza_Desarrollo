'BBV-P-412_RQCOT-05:AVH:31/08/2016: SE agrega Alianza
Imports SDManejaBD

Public Class clsLeyendasRep
    Inherits clsSession

    Private strErrLeyenda As String = ""

    Private intLeyenda As Integer = 0
    Private intEstatus As Integer = 0
    Private intClasifProd As Integer = 0
    Private intPerJur As Integer = 0
    Private intOrden As Integer = 0

    Private strTexto As String = ""
    Private strCvesClasif As String = ""
    Private strCvesPerJurid As String = ""
    Private strUsuReg As String = ""

    'BBV-P-412_RQCOT-05:AVH
    Private intAlianza As Integer = 0
    Private intSeccion As Integer = 0
    Private strNom As String = ""


    Sub New()
    End Sub
    Sub New(ByVal intCveLey As Integer)
        CargaLeyenda(intCveLey)
    End Sub

    Public ReadOnly Property ErrorLeyenda() As String
        Get
            Return strErrLeyenda
        End Get
    End Property

    Public WriteOnly Property UsuarioRegistro() As String
        Set(ByVal value As String)
            strUsuReg = value
        End Set
    End Property

    Public Property IDLeyenda() As Integer
        Get
            Return intLeyenda
        End Get
        Set(ByVal value As Integer)
            intLeyenda = value
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

    Public Property Orden() As Integer
        Get
            Return intOrden
        End Get
        Set(ByVal value As Integer)
            intOrden = value
        End Set
    End Property

    Public Property IDClasificacionProducto() As Integer
        Get
            Return intClasifProd
        End Get
        Set(ByVal value As Integer)
            intClasifProd = value
        End Set
    End Property

    Public Property IDPersonalidadJuridica() As Integer
        Get
            Return intPerJur
        End Get
        Set(ByVal value As Integer)
            intPerJur = value
        End Set
    End Property

    Public Property Leyenda() As String
        Get
            Return strTexto
        End Get
        Set(ByVal value As String)
            strTexto = value
        End Set
    End Property

    Public Property ClavesClasificacionProducto() As String
        Get
            Return strCvesClasif
        End Get
        Set(ByVal value As String)
            strCvesClasif = value
        End Set
    End Property

    Public Property ClavesPersonalidadJuridica() As String
        Get
            Return strCvesPerJurid
        End Get
        Set(ByVal value As String)
            strCvesPerJurid = value
        End Set
    End Property
    'BBV-P-412_RQCOT-05:AVH
    Public Property Alianza() As Integer
        Get
            Return intAlianza
        End Get
        Set(ByVal value As Integer)
            intAlianza = value
        End Set
    End Property
    Public Property IDSeccion() As Integer
        Get
            Return intSeccion
        End Get
        Set(ByVal value As Integer)
            intSeccion = value
        End Set
    End Property
    Public Property Nombre() As String
        Get
            Return strNom
        End Get
        Set(ByVal value As String)
            strNom = value
        End Set
    End Property



    Public Sub CargaLeyenda(Optional ByVal intLey As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intLeyenda = intLey
            dtsRes = ManejaLeyenda(1)
            intLeyenda = 0
            If Trim$(strErrLeyenda) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intLeyenda = dtsRes.Tables(0).Rows(0).Item("ID_LEYENDA")
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    intOrden = dtsRes.Tables(0).Rows(0).Item("ORDEN")
                    strTexto = dtsRes.Tables(0).Rows(0).Item("TEXTO")
                    strCvesClasif = dtsRes.Tables(0).Rows(0).Item("CVES_CLASIF_PROD")
                    strCvesPerJurid = dtsRes.Tables(0).Rows(0).Item("CVES_PER_JURID")
                    intSeccion = dtsRes.Tables(0).Rows(0).Item("SECCION")
                Else
                    strErrLeyenda = "No se encontró información para poder cargar la Leyenda"
                End If
            End If
        Catch ex As Exception
            strErrLeyenda = ex.Message
        End Try
    End Sub

    Public Function ManejaLeyenda(ByVal intOper As Integer) As DataSet
        strErrLeyenda = ""
        ManejaLeyenda = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta Leyenda
                    If intLeyenda > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idLeyenda", intLeyenda.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If intClasifProd > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idClasifProd", intClasifProd.ToString)
                    If intPerJur > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPerJuridica", intPerJur.ToString)
                    If Trim(strTexto) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "texto", strTexto)
                    If intSeccion > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idSeccion", intSeccion.ToString)
                Case 2 ' inserta Leyenda
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "orden", intOrden.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "texto", strTexto)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "cvesClasif", strCvesClasif)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "cvesPerJur", strCvesPerJurid)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    If intSeccion > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idSeccion", intSeccion.ToString)
                Case 3 ' actualiza Leyenda
                    ArmaParametros(strParamStored, TipoDato.Entero, "idLeyenda", intLeyenda.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "orden", intOrden.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "texto", strTexto)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "cvesClasif", strCvesClasif)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "cvesPerJur", strCvesPerJurid)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    If intSeccion > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idSeccion", intSeccion.ToString)
                Case 4 ' borra Leyenda
                    ArmaParametros(strParamStored, TipoDato.Entero, "idLeyenda", intLeyenda.ToString)
                Case 5 ' obtiene clasificaciones de productos
                    ArmaParametros(strParamStored, TipoDato.Entero, "idLeyenda", intLeyenda.ToString)
                    If intClasifProd > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idClasifProd", intClasifProd.ToString)
                Case 6 ' obtiene personalidades jurídicas
                    ArmaParametros(strParamStored, TipoDato.Entero, "idLeyenda", intLeyenda.ToString)
                    If intPerJur > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPerJuridica", intPerJur.ToString)
                Case 8 'BBV-P-412_RQCOT-05:AVH' consulta relacion leyenda-alianza
                    ArmaParametros(strParamStored, TipoDato.Entero, "idLeyenda", intLeyenda.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", intAlianza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    If intSeccion > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idSeccion", intSeccion.ToString)
                    If strNom <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Nom", strNom.ToString)
                Case 9 'BBV-P-412_RQCOT-05:AVH' consulta relacion leyenda-alianza
                    ArmaParametros(strParamStored, TipoDato.Entero, "idLeyenda", intLeyenda.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", intAlianza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    If intSeccion > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idSeccion", intSeccion.ToString)
                    If strNom <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Nom", strNom.ToString)
                Case 10 'BBV-P-412_RQCOT-05:AVH' consulta relacion leyenda-alianza
                    If intLeyenda > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idLeyenda", intLeyenda.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", intAlianza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    If intSeccion > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idSeccion", intSeccion.ToString)
                    If strNom <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Nom", strNom.ToString)
                Case 11 'BBV-P-412_RQCOT-05:AVH' consulta relacion leyenda-alianza
                    ArmaParametros(strParamStored, TipoDato.Entero, "idLeyenda", intLeyenda.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAlianza", intAlianza.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                    If intSeccion > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idSeccion", intSeccion.ToString)
                    If strNom <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Nom", strNom.ToString)
            End Select

            ManejaLeyenda = objSD.EjecutaStoredProcedure("spManejaLeyendasReporte", strErrLeyenda, strParamStored)
            If strErrLeyenda = "" Then
                If intOper = 2 Then
                    intLeyenda = ManejaLeyenda.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("LEYENDAS_REPORTE", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrLeyenda = ex.Message
        End Try
    End Function
End Class
