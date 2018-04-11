'BBV-P-412:AVH:22/07/2016 RQB: SE AGREGA CAMPO CORREO ELECTRONICO
'BBV-P-412:GVARGAS:27/10/2016 RQ WSE: Se agrega clase para captura de informacion de usuario, se agrega metodo de alta de usuarios mediante REST Service
'BBV-P-412:RQ 10 AVH: 01/11/2016 Cambio de las clases de WS
'BBV-P-412: GVARGAS: 07/11/2016 RQ F: Cambios bussiness alta usuarios
'BUG-PC-04: AVH: 11/11/2016 se modifica la carga de cmb perfil
'BUG-PC-11: AVH: 23/11/2016 SE AGREGA VALIDACION AL CORREO
'BBV-P-412:RQ LOGIN GVARGAS: 23/12/2016 Actualizacion REST Service iv_ticket & userID
'BUG-PC-43 01/02/17 JRHM Se cambio la forma en que se valida el username debido a que antes solo validaba al insertar uno nuevo
'BUG-PC-57:RHERNANDEZ:03/05/17:Se modifica objeto para guardado de nuevos usuarios MAPH
'BUG-PC-75:ERODRIGUEZ:13/06/2017:Se cambio la validacion del Check Interno para que cuando este Chekeado se use el web service create employee o modify employee
'BUG-PC-78:ERODRIGUEZ:16/06/2017: Se realizo validacion para habilitar el checkbox de usuario interno solo para los perfiles habilitados en la tabla PARAMETROS_SISTEMA
'BUG-PC-109 :ERODRIGUEZ:22/09/2017: Se realizo la validacion para cuando usuario interno sea cero.
Imports System.Data
Imports SNProcotiza
Imports System.Data.SqlClient
Imports SDManejaBD

Partial Class aspx_manejaUsuariosSistema
    Inherits System.Web.UI.Page
    Dim strErr As String = ""
    Dim strUser As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LimpiaError()

        If Not IsPostBack Then
            CargaCombos(1)
            If Val(Request("idUsu")) > 0 Then
                CargaInfo()
            End If
        End If
        cbUsuarioInterno.Enabled = PuedeGuardarInterno()
    End Sub

    Public Sub LimpiaError()
        lblMensaje.Text = ""
    End Sub

    Public Sub MensajeError(ByVal strMsj As String)
        strMsj = Replace(strMsj, "'", "")
        strMsj = Replace(strMsj, """", "")
        strMsj = Replace(strMsj, "(", "")
        strMsj = Replace(strMsj, ")", "")
        lblMensaje.Text = "<script>alert('" & strMsj & "')</script>"
    End Sub
    Function PuedeGuardarInterno() As Boolean
        'para verificar que el usuario pueda guardar usuarios internos
        Dim dtsRes As New DataSet
        Dim guardainterno As Boolean = False
        Dim usu As String = Val(Session("cveUsu"))
        Dim objUsuarioLoged As New SNProcotiza.clsUsuariosSistema
        Dim idususario As New Int32
        Dim isentero As Boolean = Int32.TryParse(usu, idususario)
        If (isentero) Then
            objUsuarioLoged.CargaUsuario(idususario)
            Dim paramsist As New clsParametros() 'Consulta parametros de sistema para obtener los perfiles validos
            paramsist.IDPadre = 192
            dtsRes = paramsist.ManejaParametro(1)
            If dtsRes.Tables(0).Rows.Count > 0 Then
                For Each row In dtsRes.Tables(0).Rows
                    If row.Item("VALOR").ToString() = objUsuarioLoged.IDPerfil.ToString() Then
                        guardainterno = True
                    End If
                Next
            End If

        End If
        Return guardainterno
    End Function
    Private Sub CargaInfo()
        Try
            Dim intUsu As Integer = Val(Request("idUsu"))
            Dim objUsu As New clsUsuariosSistema()
            Dim dts As New DataSet



            objUsu.IDUsuario = intUsu
            dts = objUsu.ManejaUsuario(6)
            ''Dim objPer As New SNProcotiza.clsPersonas(Val(Session("cveAcceso")), objUsu.IDPersona)

            If dts.Tables(0).Rows.Count > 0 Then
                lblId.Text = dts.Tables(0).Rows(0).Item("ID_USUARIO").ToString
                txtUser.Text = dts.Tables(0).Rows(0).Item("USERNAME").ToString.Trim
                txtnombre.Text = dts.Tables(0).Rows(0).Item("NOMBRE").ToString.Trim
                txtpaterno.Text = dts.Tables(0).Rows(0).Item("PATERNO").ToString.Trim
                txtmaterno.Text = dts.Tables(0).Rows(0).Item("MATERNO").ToString
                cmbEmpresa.SelectedValue = dts.Tables(0).Rows(0).Item("ID_EMPRESA").ToString
                cmbTipoSeguridad.SelectedValue = dts.Tables(0).Rows(0).Item("ID_TIPO_SEGURIDAD").ToString
                cmbPerfil.SelectedValue = dts.Tables(0).Rows(0).Item("ID_PERFIL").ToString
                cmbEstatus.SelectedValue = dts.Tables(0).Rows(0).Item("ESTATUS").ToString
                txtCorreo.Text = dts.Tables(0).Rows(0).Item("CORREO").ToString 'BBV-P-412:AVH
                If Not IsDBNull(dts.Tables(0).Rows(0).Item("USU_INTERNO")) Then
                    cbUsuarioInterno.Checked = IIf(dts.Tables(0).Rows(0).Item("USU_INTERNO").ToString = 1, True, False)
                Else

                    cbUsuarioInterno.Checked = False
                End If



            Else
                MensajeError("No se localizó información para el objeto seleccionado.")
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Private Function CargaCombos(ByVal intOpc As Integer) As Boolean
        Dim objCombo As New clsProcGenerales
        Dim dtsRes As New DataSet
        Dim objDetalle As New SNProcotiza.clsDetallePerfil
        Try
            CargaCombos = False

            'combo de estatus
            dtsRes = objCombo.ObtenInfoParametros(1, strErr)
            If Trim$(strErr) = "" Then
                objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbEstatus, strErr)
                If Trim$(strErr) <> "" Then
                    MensajeError(strErr)
                    Exit Function
                End If
            Else
                MensajeError(strErr)
                Exit Function
            End If

            'combo de empresas
            Dim objEmp As New clsEmpresas
            objEmp.IDEstatus = 2
            dtsRes = objEmp.ManejaEmpresa(1)
            strErr = objEmp.ErrorEmpresa

            If Trim$(strErr) = "" Then
                objCombo.LlenaCombos(dtsRes, "RAZON_SOCIAL", "ID_EMPRESA", cmbEmpresa, strErr)
                If Trim$(strErr) <> "" Then
                    MensajeError(strErr)
                    Exit Function
                End If
            End If

            'combo de manejo de seguridad
            dtsRes = objCombo.ObtenInfoParametros(67, strErr, False, 1)
            If Trim$(strErr) = "" Then
                objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbTipoSeguridad, strErr)
                If Trim$(strErr) <> "" Then
                    MensajeError(strErr)
                    Exit Function
                End If
            Else
                MensajeError(strErr)
                Exit Function
            End If

            'combo de perfiles
            objDetalle.Estatus = 2
            dtsRes = objDetalle.ManejaPerfil(3)


            If Trim$(strErr) = "" Then
                objCombo.LlenaCombos(dtsRes, "PERFIL", "ID_PERFIL", cmbPerfil, strErr)
                If Trim$(strErr) <> "" Then
                    MensajeError(strErr)
                    Exit Function
                End If
            Else
                MensajeError(strErr)
                Exit Function
            End If

            CargaCombos = True
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try

    End Function

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaUsuariosSistema.aspx")
    End Sub

    Public Sub CierraPantalla(ByVal strPant As String)
        lblMensaje.Text += "<script>alert('El registro se guardó con éxito'); document.location.href='" & strPant & "';</script>"
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        strUser = User.Identity.Name
        Dim objsession As New clsSession

        objsession.CargaSession(strUser)

        Try
            If ValidaCampos() Then

                Dim objVal As New SNProcotiza.clsValidaCampo
                objVal.ValidaEmail(Me.txtCorreo.Text)

                    Dim intUsu As Integer = Val(Request("idUsu"))
                    Dim objUsu As New SNProcotiza.clsUsuariosSistema
                    Dim intOpc As Integer = 2

                    objUsu.CargaSession(Val(Session("cveAcceso")))
                    If intUsu > 0 Then
                        objUsu.CargaUsuario(intUsu)
                        intOpc = 3
                    Else
                        Dim objSeg As New SNProcotiza.clsSeguridad
                        objUsu.Password = objSeg.EncriptarCadena("123456")
                    End If

                'If intOpc = 2 Then
                '    If ValidaUserName(Trim(txtUser.Text), CInt(lblId.Text)) = False Then
                '        MensajeError("El UserName ya existe.")
                '        Exit Sub
                '    End If
                'End If

                    'guardamos la info de usuario
                    objUsu.IDEmpresa = Val(cmbEmpresa.SelectedValue)
                    objUsu.Nombre = txtnombre.Text.Trim
                    objUsu.Apaterno = txtpaterno.Text.Trim
                    objUsu.Amaterno = txtmaterno.Text.Trim
                    objUsu.IDTipoSeguridad = Val(cmbTipoSeguridad.SelectedValue)
                    objUsu.IDPerfil = Val(cmbPerfil.SelectedValue)
                    objUsu.UserName = Trim(txtUser.Text)
                    objUsu.IDEstatus = Val(cmbEstatus.SelectedValue)
                    objUsu.UsuarioRegistro = objsession.UserNameAcceso
                    objUsu.Correo = txtCorreo.Text 'BBV-P-412:AVH
                    objUsu.UsuInterno = IIf(cbUsuarioInterno.Checked, 1, 0)


                Dim dts As New DataSet()
                Dim dtsID As New DataSet()
                dts = objUsu.ManejaUsuario(intOpc)
                'BUG-PC-XX MAPH 03-05-2017 Corrección del mensaje referente a que no se encontró la tabla 0
                If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows(0).Item("ID_USUARIO") = -1 Then
                    MensajeError("Username ya existe")
                    Exit Sub
                Else
                    MensajeError("Usuario guardado con éxito")

                End If
                End If

                Dim StrEjecuta As String = String.Empty
                Dim objSD As New clsConexion
                StrEjecuta = "SELECT MAX(ID_USUARIO) AS ID_USUARIO FROM USUARIOS_SISTEMA"
                dtsID = objSD.EjecutaQueryConsulta(StrEjecuta)

                    Dim action As Integer = intOpc ' 2 : crear, 3 modificar
                    Dim model As personBase = New personBase()
                If (objUsu.UsuInterno = 1) Then
                        Dim idUser As String = objUsu.IDUsuario
                        Dim fecha As String = DateTime.Now.ToString("yyyy-MM-dd")
                        Dim fechaUpdate As String = DateTime.Now.ToString("yyyy-MM-dd")
                        Dim status_name As String = String.Empty
                        Dim status_id As String = String.Empty
                        Dim perfil_id = objUsu.IDPerfil
                        If (action = 3) Then
                            Dim Cfecha As Date = CDate(objUsu.Fecha_Reg.ToString)
                            fecha = Cfecha.ToString("yyyy-MM-dd")
                        End If
                        If (Me.cmbEstatus.SelectedValue = 3) Then
                            status_name = "CANCELADO"
                            status_id = "2"
                        Else
                            status_name = "ACTIVO"
                            status_id = "1"
                        End If

                        model.person.firstName = txtpaterno.Text.Trim
                        model.person.lastName = txtmaterno.Text.Trim
                        model.person.telephone = ""
                        model.person.mail = txtCorreo.Text
                        model.person.name = txtnombre.Text.Trim

                        model.extendedData.status.name = status_name
                        model.extendedData.status.id = status_id

                        model.extendedData.registerDate = fecha
                        model.extendedData.lastUpdate = fechaUpdate
                        model.extendedData.registerNumber = idUser
                        model.extendedData.figureId = perfil_id

                        model.userCode = idUser
                    End If


                    If objUsu.ErrorUsuario = "" Then
                    If (objUsu.UsuInterno = 1) Then
                            Dim str As String = String.Empty
                            If (action = 3) Then
                                str = model.modifyEmployee(model)
                            ElseIf (action = 2) Then
                                str = model.createEmployee(model)
                            End If

                        If (str = "INS") Then
                            str = model.createEmployee(model)
                        End If
                            If (str = "NO_VALUE") Then
                                objUsu.IDEstatus = 3
                                objUsu.ManejaUsuario(3)
                            End If
                        End If
                    MensajeError("Usuario guardado con éxito")
                        CierraPantalla("./consultaUsuariosSistema.aspx")
                    Else
                        MensajeError(objUsu.ErrorUsuario)
                    End If


            Else
                MensajeError("Todos los campos marcados con * son obligatorios.")
            End If

        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If cmbEmpresa.SelectedValue = "" Then Exit Function
        If cmbTipoSeguridad.SelectedValue = "" Then Exit Function
        If cmbPerfil.SelectedValue = "" Then Exit Function
        If cmbEstatus.SelectedValue = "" Then Exit Function
        If Trim(txtUser.Text) = "" Then Exit Function
        If Trim(txtnombre.Text) = "" Then Exit Function
        
        ValidaCampos = True
    End Function

    'Private Function ValidaUserName(strUserName As String, Optional ByVal id_user As Integer = 0) As Boolean
    '    ValidaUserName = False
    '    Dim objUsu As New SNProcotiza.clsUsuariosSistema
    '    Dim dst As New DataSet

    '    If (id_user <> 0) Then
    '        objUsu.IDUsuario = id_user
    '    End If
    '    objUsu.UserName = strUserName
    '    dst = objUsu.ManejaUsuario(1)

    '    If objUsu.ErrorUsuario <> "" Then
    '        MensajeError(objUsu.ErrorUsuario.ToString)
    '        Exit Function
    '    Else
    '        If dst.Tables(0).Rows.Count > 0 Then
    '            ValidaUserName = False
    '        Else
    '            ValidaUserName = True
    '        End If
    '    End If

    '    Return ValidaUserName
    'End Function

    Public Class personBase
        Public person As person = New person()
        Public extendedData As extendedData = New extendedData()
        Public userCode As Integer

        Public Function createEmployee(ByVal model As personBase) As String
            Dim Header As String = connection(model, 1)
            Return Header
        End Function

        Public Function modifyEmployee(ByVal model As personBase) As String
            Dim Header As String = connection(model, 2)
            Return Header
        End Function

        Public Function deleteEmployee(ByVal model As personBase) As String
            Dim Header As String = connection(model, 3)
            Return Header
        End Function

        Private Function connection(ByVal model As personBase, ByVal opcion As Integer) As String
			Dim Header As String = String.Empty
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim modelJSON As String = serializer.Serialize(model)

            Dim uri As String = String.Empty
            If (opcion = 1) Then
                uri = System.Configuration.ConfigurationManager.AppSettings("createEmployee").ToString()
            ElseIf (opcion = 2) Then
                uri = System.Configuration.ConfigurationManager.AppSettings("modifyEmployee").ToString()
            ElseIf (opcion = 3) Then
                uri = System.Configuration.ConfigurationManager.AppSettings("deleteEmployee").ToString()
            End If

            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            Dim rest As WCF.RESTful = New WCF.RESTful()
            rest.Uri = uri
            rest.buscarHeader("ResponseWarningDescription")

            Dim jsonResult As String = String.Empty
            If (opcion = 1) Then
                jsonResult = rest.ConnectionPost(userID, iv_ticket1, modelJSON)
            ElseIf (opcion = 2) Then
                jsonResult = rest.ConnectionPut(userID, iv_ticket1, modelJSON)
            ElseIf (opcion = 3) Then
                jsonResult = rest.ConnectionDelete(userID, iv_ticket1, modelJSON)
            End If

            If (rest.IsError) Then
				If rest.StatusHTTP = 0 Then
                Header = "NO_VALUE"
				Else
					If rest.MensajeError = "ERROR EN INSERTAR GESTOR" Then
						Header = "NO_VALUE"
					ElseIf rest.MensajeError = ("ERROR EN NO EXISTE GESTOR " + model.extendedData.registerNumber) Then
						Header = "INS"
					End If
					If rest.MensajeError = "" Then
						Header = "NO_VALUE"
					End If
				End If
            Else
                Header = rest.valorHeader
            End If

            Return Header
        End Function
    End Class

    Public Class person
        Public firstName As String
        Public lastName As String
        Public telephone As String
        Public mail As String
        Public name As String
    End Class

    Public Class extendedData
        Public status As status = New status()
        Public registerDate As String
        Public lastUpdate As String
        Public registerNumber As String
        Public figureId As Integer
    End Class

    Public Class status
        Public name As String
        Public id As String
    End Class

End Class


