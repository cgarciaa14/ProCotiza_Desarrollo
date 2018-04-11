Imports System.Data
Imports SNProcotiza

Partial Class aspx_manejaEmpresasAfiliadas
    Inherits System.Web.UI.Page
    Dim strErr As String = ""
    Dim strUser As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LimpiaError()

        If Not IsPostBack Then
            txtFecIni.Attributes.Add("onfocus", "document.getElementById('" & chkDefault.ClientID & "').focus();")
            txtFecFin.Attributes.Add("onfocus", "document.getElementById('" & chkDefault.ClientID & "').focus();")

            CargaCombos(1)
            If Val(Request("idEmp")) > 0 Then
                CargaInfo()
            End If
        End If
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

    Public Sub CierraPantalla(ByVal strPant As String)
        lblMensaje.Text += "<script>alert('El registro se guardó con éxito'); document.location.href='" & strPant & "';</script>"
    End Sub

    Private Function CargaCombos(ByVal intOpc As Integer) As Boolean
        Dim objCombo As New clsProcGenerales
        Dim dtsRes As New DataSet
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

            CargaCombos = True
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Function

    Private Sub CargaInfo()
        Try
            Dim intEmp As Integer = Val(Request("idEmp"))
            Dim objEmp As New SNProcotiza.clsEmpresas(intEmp)

            If objEmp.IDEmpresa > 0 Then
                lblId.Text = objEmp.IDEmpresa
                txtRazSoc.Text = objEmp.RazonSocial
                txtNomCto.Text = objEmp.NombreCorto
                txtFecIni.Text = objEmp.InicioVigencia
                txtFecFin.Text = objEmp.FinVigencia
                txtUid.Text = objEmp.IDExterno
                txtURL.Text = objEmp.URLAcceso
                txtImgLogin.Text = objEmp.ImagenLogin
                txtColorLogin.Text = objEmp.ColorFondoLogin
                txtLogoEnc.Text = objEmp.ImagenEncabezado
                txtLogoRep.Text = objEmp.ImagenLogoReporte
                chkDefault.Checked = IIf(objEmp.RegistroDefault = 1, True, False)
                cmbEstatus.SelectedValue = objEmp.IDEstatus
            Else
                MensajeError("No se localizó información para el objeto seleccionado.")
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        strUser = User.Identity.Name
        Dim objsession As New clsSession
        objsession.CargaSession(strUser)

        Try
            If ValidaCampos() Then
                Dim intEmp As Integer = Val(Request("idEmp"))
                Dim objEmp As New SNProcotiza.clsEmpresas
                Dim intOpc As Integer = 2

                objEmp.CargaSession(Val(Session("cveAcceso")))
                If intEmp > 0 Then
                    objEmp.CargaEmpresa(intEmp)
                    intOpc = 3
                Else
                    Dim objSeg As New SNProcotiza.clsSeguridad
                End If

                'guardamos la info de la empresa
                objEmp.RazonSocial = Trim(txtRazSoc.Text)
                objEmp.NombreCorto = Trim(txtNomCto.Text)
                ''objEmp.InicioVigencia = Format(Trim(txtFecIni.Text), "yyyy/MM/dd")
                objEmp.InicioVigencia = Trim(txtFecIni.Text)
                objEmp.FinVigencia = Trim(txtFecFin.Text)
                objEmp.RegistroDefault = IIf(chkDefault.Checked, 1, 0)
                objEmp.IDEstatus = Val(cmbEstatus.SelectedValue)
                objEmp.IDExterno = Trim(txtUid.Text)
                objEmp.URLAcceso = Trim(txtURL.Text)
                objEmp.ImagenLogin = Trim(txtImgLogin.Text)
                objEmp.ColorFondoLogin = Trim(txtColorLogin.Text)
                objEmp.ImagenEncabezado = Trim(txtLogoEnc.Text)
                objEmp.ImagenLogoReporte = Trim(txtLogoRep.Text)
                objEmp.UsuarioRegistro = objsession.UserNameAcceso
                objEmp.ManejaEmpresa(intOpc)
                If objEmp.ErrorEmpresa = "" Then
                    CierraPantalla("./consultaEmpresasAfiliadas.aspx")
                Else
                    MensajeError(objEmp.ErrorEmpresa)
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

        If cmbEstatus.SelectedValue = "" Then Exit Function
        If Trim(txtRazSoc.Text) = "" Then Exit Function
        If Trim(txtNomCto.Text) = "" Then Exit Function
        If Trim(txtFecIni.Text) = "" Then Exit Function
        If Trim(txtFecFin.Text) = "" Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaEmpresasAfiliadas.aspx")
    End Sub
End Class
