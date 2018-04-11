
Partial Class aspx_cambiaPwd
    Inherits System.Web.UI.Page
    Private objGral As New clsProcGenerales

    Protected Sub cmdCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCerrar.Click
        lblMensaje.Text = "<script>window.close();</script>"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim intUsu As Integer

        intUsu = Val(Request("idUsu"))
        lblMensaje.Text = ""

        If Not IsPostBack Then
            If intUsu > 0 Then
                'se cargan los datos del usuario)
                Dim objUsu As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), intUsu)
                lblUsu.Text = objUsu.UserName
            End If
        End If
    End Sub

    Protected Sub btnGuarda_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuarda.Click
        Dim strVal As String
        Dim strFecNac As String = ""
        Dim intUsu As Integer = Val(Request("idUsu"))
        Dim intPost As Integer = Val(Request("post"))
        Dim objUsu As New SNProcotiza.clsUsuariosSistema
        Dim objSeg As New SNProcotiza.clsSeguridad(Session("cveAcceso"))

        strVal = "No capturó el campo "
        If Len(Trim(txtPwdAct.Text)) = 0 Then strVal += "Contraseña Actual" : objGral.MensajeError(Me, strVal) : Exit Sub
        If Len(Trim(txtPwd.Text)) = 0 Then strVal += "Contraseña" : objGral.MensajeError(Me, strVal) : Exit Sub
        If Len(Trim(txtConfirm.Text)) = 0 Then strVal += "Confirmación" : objGral.MensajeError(Me, strVal) : Exit Sub

        If intUsu > 0 Then objUsu.CargaUsuario(intUsu)
        If objSeg.EncriptarCadena(Trim(txtPwdAct.Text)) <> Trim(objUsu.Password) Then objGral.MensajeError(Me, "La contraseña actual no es válida") : Exit Sub
        If Trim(txtPwd.Text) <> Trim(txtConfirm.Text) Then objGral.MensajeError(Me, "El campo confirmación no coincide con el campo contraseña") : Exit Sub

        objUsu.Password = objSeg.EncriptarCadena(Trim(txtPwd.Text))
        objUsu.FechaCambioPwd = DateAdd(DateInterval.Month, 1, Now()).ToString("yyyy-MM-dd")

        'se guardan los datos
        objUsu.ManejaUsuario(7)
        If Trim(objUsu.ErrorUsuario) <> "" Then
            objGral.MensajeError(Me, objUsu.ErrorUsuario)
        Else
            If intPost > 0 Then
                lblMensaje.Text = "<script>alert('La contraseña se cambió con éxito'); RegresaDatos('idUsu','" & objUsu.IDUsuario & "'); window.close();</script>"
            Else
                lblMensaje.Text = "<script>alert('La contraseña se cambió con éxito'); window.close();</script>"
            End If
        End If
    End Sub
End Class
