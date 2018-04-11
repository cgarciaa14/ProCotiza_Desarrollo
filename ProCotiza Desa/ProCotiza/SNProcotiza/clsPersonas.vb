'BBVA-P-412: 25-08-2016: AMR: RQCOT-01 Ajustes a la pantalla de Cotización.
'BUG-PC-22 02-11-2016 MAUT Se agrega segundo nombre

Imports SDManejaBD

Public Class clsPersonas
    Inherits clsSession

    Private strErrPersona As String = ""

    Private intPersona As Integer = 0
    Private intTitulo As Integer = 0
    Private intSexo As Integer = 0
    Private intEdoCiv As Integer = 0
    Private intPerJur As Integer = 0
    Private intEstat As Integer = 0

    Private strNombre As String = ""
    Private strNombreComp As String = ""
    Private strPaterno As String = ""
    Private strMaterno As String = ""
    Private strRfc As String = ""
    Private strCurp As String = ""
    Private strUrlFoto As String = ""
    Private strMail As String = ""
    Private strFecNac As String = ""
    Private strNombre2 As String = String.Empty
    Private strcp As String = String.Empty
    Private strusureg As String = String.Empty


    Public Sub New()
    End Sub

    Public Sub New(ByVal intIDAcceso As Integer)
        CargaSession(intIDAcceso)
    End Sub

    Public Sub New(ByVal intIDAcceso As Integer, ByVal intIDPersona As Integer)
        CargaSession(intIDAcceso)
        CargaPersona(intIDPersona)
    End Sub

    Public ReadOnly Property ErrorPersona() As String
        Get
            Return strErrPersona
        End Get
    End Property

    Public Property PersonaID() As Integer
        Get
            Return intPersona
        End Get
        Set(ByVal value As Integer)
            intPersona = value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return strNombre
        End Get

        Set(ByVal Value As String)
            strNombre = Value
        End Set
    End Property

    Public Property Paterno() As String
        Get
            Return strPaterno
        End Get

        Set(ByVal Value As String)
            strPaterno = Value
        End Set
    End Property

    Public Property Materno() As String
        Get
            Return strMaterno
        End Get

        Set(ByVal Value As String)
            strMaterno = Value
        End Set
    End Property

    Public Property NombreCompleto() As String
        Get
            Return strNombreComp
        End Get

        Set(ByVal Value As String)
            strNombreComp = Value
        End Set
    End Property

    Public Property RFC() As String
        Get
            Return strRfc
        End Get

        Set(ByVal Value As String)
            strRfc = Value
        End Set
    End Property

    Public Property Curp() As String
        Get
            Return strCurp
        End Get

        Set(ByVal Value As String)
            strCurp = Value
        End Set
    End Property

    Public Property FechaNacimiento() As String
        Get
            Return strFecNac
        End Get

        Set(ByVal Value As String)
            strFecNac = Value
        End Set
    End Property

    Public Property Mail() As String
        Get
            Return strMail
        End Get

        Set(ByVal Value As String)
            strMail = Value
        End Set
    End Property

    Public Property URLFoto() As String
        Get
            Return strUrlFoto
        End Get

        Set(ByVal Value As String)
            strUrlFoto = Value
        End Set
    End Property

    Public Property cveTitulo() As Integer
        Get
            Return intTitulo
        End Get

        Set(ByVal Value As Integer)
            intTitulo = Value
        End Set
    End Property

    Public Property cveSexo() As Integer
        Get
            Return intSexo
        End Get

        Set(ByVal Value As Integer)
            intSexo = Value
        End Set
    End Property

    Public Property cvePerJuridica() As Integer
        Get
            Return intPerJur
        End Get

        Set(ByVal Value As Integer)
            intPerJur = Value
        End Set
    End Property

    Public Property Estatus() As Integer
        Get
            Return intEstat
        End Get

        Set(ByVal Value As Integer)
            intEstat = Value
        End Set
    End Property

    Public Property EstadoCivil() As Integer
        Get
            Return intEdoCiv
        End Get

        Set(ByVal Value As Integer)
            intEdoCiv = Value
        End Set
    End Property

    Public Property Nombre2() As String
        Get
            Return strNombre2
        End Get
        Set(value As String)
            strNombre2 = value
        End Set
    End Property

    Public Property CodPost As String
        Get
            Return strcp
        End Get
        Set(value As String)
            strcp = value
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

    Public Sub CargaPersona(ByVal intPer As Integer)
        Dim dtsRes As New DataSet

        intPersona = intPer
        dtsRes = ManejaPersona(1)
        intPersona = 0

        If Trim$(strErrPersona) = "" Then
            intPersona = dtsRes.Tables(0).Rows(0).Item("ID_PERSONA")
            intPerJur = dtsRes.Tables(0).Rows(0).Item("ID_PER_JURIDICA")
            intEstat = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
            strNombre = dtsRes.Tables(0).Rows(0).Item("NOMBRE")
            strPaterno = dtsRes.Tables(0).Rows(0).Item("PATERNO")
            strMaterno = dtsRes.Tables(0).Rows(0).Item("MATERNO")
            strNombreComp = dtsRes.Tables(0).Rows(0).Item("NOM_COMPLETO")
            strRfc = dtsRes.Tables(0).Rows(0).Item("RFC")
            strCurp = dtsRes.Tables(0).Rows(0).Item("CURP")
            strFecNac = dtsRes.Tables(0).Rows(0).Item("FEC_NAC")
            strMail = dtsRes.Tables(0).Rows(0).Item("MAIL")
            strUrlFoto = dtsRes.Tables(0).Rows(0).Item("URL_FOTO")
            intSexo = dtsRes.Tables(0).Rows(0).Item("ID_SEXO")
            intTitulo = dtsRes.Tables(0).Rows(0).Item("ID_TITULO")
            intEdoCiv = Val(dtsRes.Tables(0).Rows(0).Item("ID_EDO_CIVIL"))
            strNombre2 = dtsRes.Tables(0).Rows(0).Item("NOMBRE2")
            strcp = dtsRes.Tables(0).Rows(0).Item("CODIGO_POSTAL")
        End If
    End Sub

    Public Function ManejaPersona(ByVal intOper As Integer) As DataSet
        strErrPersona = ""
        ManejaPersona = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta persona
                    Dim strNomConsul As String = ""
                    If Trim(strNombre) <> "" Then strNomConsul = Trim(strNombre)
                    If Trim(strPaterno) <> "" Then strNomConsul += IIf(Trim(strNomConsul) <> "", ".", "") & Trim(strPaterno)
                    If Trim(strMaterno) <> "" Then strNomConsul += IIf(Trim(strNomConsul) <> "", ".", "") & Trim(strMaterno)

                    If intPersona > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPersona", intPersona.ToString)
                    If intPerJur > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPerJur", intPerJur.ToString)
                    If intSexo > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idSexo", intSexo.ToString)
                    If intEdoCiv > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idEdoCiv", intEdoCiv.ToString)
                    If intEstat > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intEstat.ToString)
                    If Trim(strRfc) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "rfc", strRfc)
                    If Trim(strNomConsul) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "nombre", strNomConsul)


                Case 2 ' inserta persona
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPerJur", intPerJur.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nombre", strNombre)
                    If strNombre2.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nombre2", strNombre2)
                    If Trim(strPaterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "paterno", strPaterno)
                    If Trim(strMaterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "materno", strMaterno)
                    If intSexo > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idSexo", intSexo.ToString)
                    If Trim(strFecNac) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "fecNac", strFecNac)
                    If intTitulo > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTitulo", intTitulo.ToString)
                    If Trim(strRfc) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "rfc", strRfc)
                    If Trim(strCurp) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "curp", strCurp)
                    If Trim(strMail) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "mail", strMail)
                    If intEdoCiv > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idEdoCiv", intEdoCiv.ToString)
                    If strcp.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "cp", strcp)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intEstat.ToString)
                    If Trim(strUrlFoto) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "urlFoto", strUrlFoto)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usuReg", strusureg)
                Case 3 ' actualiza persona
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPerJur", intPerJur.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nombre", strNombre)
                    If strNombre2.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "nombre2", strNombre2)
                    If Trim(strPaterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "paterno", strPaterno)
                    If Trim(strMaterno) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "materno", strMaterno)
                    If intSexo > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idSexo", intSexo.ToString)
                    If Trim(strFecNac) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "fecNac", strFecNac)
                    If intTitulo > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTitulo", intTitulo.ToString)
                    If Trim(strRfc) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "rfc", strRfc)
                    If Trim(strCurp) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "curp", strCurp)
                    If Trim(strMail) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "mail", strMail)
                    If intEdoCiv > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idEdoCiv", intEdoCiv.ToString)
                    If strcp.Trim.Length > 0 Then ArmaParametros(strParamStored, TipoDato.Cadena, "cp", strcp)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estatus", intEstat.ToString)
                    If Trim(strUrlFoto) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "urlFoto", strUrlFoto)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usuReg", strusureg)
                Case 4 ' borra persona
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPersona", intPersona.ToString)
            End Select

            ManejaPersona = objSD.EjecutaStoredProcedure("spManejaPersonas", strErrPersona, strParamStored)
            If strErrPersona = "" Then
                If intOper = 2 Then
                    intPersona = ManejaPersona.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("PERSONAS", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrPersona = ex.Message
        End Try
    End Function

    Public Function CalculaRFC(ByVal strAPat As String, _
                               ByVal strAMat As String, _
                               ByVal strNom As String, _
                               ByVal strFecNac As String, _
                               ByRef strMensaje As String, _
                               ByRef strNomSeg As String) As String
        Dim strFec As String
        Dim intCont As Integer
        Dim strPaso() As String
        Dim strApePat As String
        Dim strApeMat As String
        Dim strNombre As String

        Try
            'se revisa que sean carctéres válidos y se limpian los prefijos y nombres Jose y Maria
            CalculaRFC = ""
            CalculaRFC = RevisaCaracteres(strAPat)
            If Len(CalculaRFC) > 0 Then strMensaje = ObtenError(118, "cadena de caractéres inválidos") : Exit Function
            strApePat = LimpiaCadenaRFC(strAPat, 1)

            CalculaRFC = RevisaCaracteres(strAMat)
            If Len(CalculaRFC) > 0 Then strMensaje = ObtenError(118, "cadena de caractéres inválidos") : Exit Function
            strApeMat = LimpiaCadenaRFC(strAMat, 1)

            CalculaRFC = RevisaCaracteres(strNom)
            If Len(CalculaRFC) > 0 Then strMensaje = ObtenError(118, "cadena de caractéres inválidos") : Exit Function
            strNombre = LimpiaCadenaRFC(strNom, 1)

            'BUG-PC-22 MAUT Se agrega segundo nombre
            CalculaRFC = RevisaCaracteres(strNomSeg)
            If Len(CalculaRFC) > 0 Then strMensaje = ObtenError(118, "cadena de caractéres inválidos") : Exit Function
            strNomSeg = LimpiaCadenaRFC(strNomSeg, 1)

            'se arma la fecha considerando que llega en un formato "dd/MM/aaaa"
            If Not IsDate(strFecNac) Then strMensaje = "Fecha Inválida" : Exit Function
            strFecNac = Format$(CDate(strFecNac), "dd/MM/yyyy")
            strPaso = Split(strFecNac, "/")
            strFec = Right(strPaso(2), 2)
            strFec += IIf(Len(Trim$(strPaso(1))) = 1, "0" & strPaso(1), strPaso(1))
            strFec += IIf(Len(Trim$(strPaso(0))) = 1, "0" & strPaso(0), strPaso(0))

            'obtenemos la 4 primeras letras
            CalculaRFC = Left$(Trim$(strApePat), 1)
            For intCont = 2 To Len(Trim$(strApePat))
                Select Case Mid$(strApePat, intCont, 1)
                    Case "A"
                        CalculaRFC += "A" : Exit For
                    Case "E"
                        CalculaRFC += "E" : Exit For
                    Case "I"
                        CalculaRFC += "I" : Exit For
                    Case "O"
                        CalculaRFC += "O" : Exit For
                    Case "U"
                        CalculaRFC += "U" : Exit For
                End Select
            Next
            CalculaRFC += IIf(strAPat = "", Left$(Trim$(strApeMat), 2), Left$(Trim$(strApeMat), 1))
            CalculaRFC += Left$(Trim$(strNombre), 1)
            If Len(CalculaRFC) < 4 Then CalculaRFC += Mid$(Trim$(strNom), 2, 1)

            'se revisa que la palabra que quedo no suene ofensiva
            CalculaRFC = LimpiaCadenaRFC(CalculaRFC, 2)
            'se le agrega la fecha
            CalculaRFC += strFec
            'se agrega la Homoclave
            'BUG-PC-22 MAUT Se agrega segundo nombre
            CalculaRFC = ObtenHomonimia(strApePat, strApeMat, strNom, CalculaRFC, strNomSeg)

            Return CalculaRFC

        Catch ex As Exception
            strMensaje = ObtenError(119)
            Return Nothing
        End Try
    End Function
End Class
