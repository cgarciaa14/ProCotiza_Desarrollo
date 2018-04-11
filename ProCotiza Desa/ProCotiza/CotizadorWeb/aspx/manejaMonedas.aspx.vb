Imports System.Data

Partial Class aspx_manejaMonedas
    Inherits System.Web.UI.Page
    Dim strErr As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()

        If Not IsPostBack Then
            CargaCombos(1)
            If Val(Request("idMoneda")) > 0 Then
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

    Private Sub CargaInfo()
        Try
            Dim intMon As Integer = Val(Request("idMoneda"))
            Dim objMon As New SNProcotiza.clsMonedas(intMon)

            If objMon.IDMoneda > 0 Then
                lblId.Text = objMon.IDMoneda
                txtNom.Text = objMon.Nombre
                txtValCamb.Text = objMon.ValorCambio
                txtUid.Text = objMon.IDExterno
                cmbEstatus.SelectedValue = objMon.IDEstatus
                chkBase.Checked = IIf(objMon.EsMonedaBase = 1, True, False)
            Else
                MensajeError("No se localizó información para la moneda.")
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Private Function CargaCombos(ByVal intOpc As Integer) As Boolean
        Dim objCombo As New clsProcGenerales
        Dim dtsRes As New DataSet
        Try
            CargaCombos = False

            'combo de estatus
            dtsRes = objCombo.ObtenInfoParametros(1, strErr)
            If Trim$(strErr) = "" Then
                objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbEstatus, strErr, False)
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

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then
                Dim intMon As Integer = Val(Request("idMoneda"))
                Dim objMon As New SNProcotiza.clsMonedas
                Dim intOpc As Integer = 2

                objMon.CargaSession(Val(Session("cveAcceso")))
                If intMon > 0 Then
                    objMon.CargaMoneda(intMon)
                    intOpc = 3
                End If

                'guardamos la info dea moneda
                objMon.Nombre = Trim(txtNom.Text)
                objMon.ValorCambio = Val(Trim(txtValCamb.Text))
                objMon.IDEstatus = Val(cmbEstatus.SelectedValue)
                objMon.EsMonedaBase = IIf(chkBase.Checked, 1, 0)
                objMon.IDExterno = Trim(txtUid.Text)
                objMon.UsuarioRegistro = objMon.UserNameAcceso

                objMon.ManejaMoneda(intOpc)
                If objMon.ErrorMoneda = "" Then
                    CierraPantalla("./consultaMonedas.aspx")
                Else
                    MensajeError(objMon.ErrorMoneda)
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
        If Trim(txtNom.Text) = "" Then Exit Function
        If Trim(txtValCamb.Text) = "" Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaMonedas.aspx")
    End Sub
End Class
