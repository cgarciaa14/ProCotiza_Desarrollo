Imports System.Data

Partial Class aspx_manejaAseguradoras
    Inherits System.Web.UI.Page
    Dim strErr As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()

        If Not IsPostBack Then
            CargaCombos()
            If Val(Request("idAseguradora")) > 0 Then
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

    Public WriteOnly Property Script() As String
        Set(ByVal value As String)
            lblScript.Text = "<script>" & value & " </script>"
        End Set
    End Property

    Public Sub CierraPantalla(ByVal strPant As String)
        lblMensaje.Text += "<script>alert('El registro se guardó con éxito'); document.location.href='" & strPant & "';</script>"
    End Sub

    Private Sub CargaInfo()
        Try

            Dim intAseg As Integer = Val(Request("idAseguradora"))
            Dim objAseg As New SNProcotiza.clsAseguradoras(intAseg)

            If objAseg.IDAseguradora > 0 Then
                lblidAseg.Text = objAseg.IDAseguradora
                txtrazonsocial.Text = objAseg.RazonSocial
                txtnomcorto.Text = objAseg.NomCorto
                txtidext.Text = IIf(objAseg.IDExterno = 0, "", objAseg.IDExterno)
                ddlestatus.SelectedValue = objAseg.IDEstatus
                chkDefault.Checked = IIf(objAseg.RegistroDefault = 1, True, False)
            Else
                MensajeError("No se localizó información para el Broker.")
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Private Function CargaCombos() As Boolean
        Dim objCombo As New clsProcGenerales
        Dim dtsRes As New DataSet
        Try
            CargaCombos = False

            'combo de estatus
            dtsRes = objCombo.ObtenInfoParametros(1, strErr)
            If Trim$(strErr) = "" Then
                objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", ddlestatus, strErr, False)
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
            CargaCombos = False
            MensajeError(ex.Message)
        End Try
    End Function

    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If txtrazonsocial.Text.Trim = "" Then Exit Function
        If txtnomcorto.Text.Trim = "" Then Exit Function
        If ddlestatus.SelectedValue = "" Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaAseguradoras.aspx")
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then
                Dim intAseg As Integer = Val(Request("idAseguradora"))
                Dim objAseg As New SNProcotiza.clsAseguradoras
                Dim intOpc As Integer = 2

                objAseg.CargaSession(Val(Session("cveAcceso")))
                If intAseg > 0 Then
                    objAseg.CargaAseguradora(intAseg)
                    intOpc = 3
                End If

                'guardamos la info del tipo de operación
                objAseg.RazonSocial = Trim(txtrazonsocial.Text)
                objAseg.NomCorto = Trim(txtnomcorto.Text)

                If txtidext.Text.Trim.Length > 0 Then
                    objAseg.IDExterno = Convert.ToInt64(txtidext.Text.Trim)
                End If
                objAseg.IDEstatus = ddlestatus.SelectedValue
                objAseg.RegistroDefault = IIf(chkDefault.Checked, 1, 0)
                objAseg.UsuarioRegistro = objAseg.UserNameAcceso

                objAseg.ManejaAseguradora(intOpc)
                If objAseg.ErrorAseguradora = "" Then
                    CierraPantalla("./consultaAseguradoras.aspx")
                Else
                    MensajeError(objAseg.ErrorAseguradora)
                End If
            Else
                MensajeError("Todos los campos marcados con * son obligatorios.")
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub
End Class
