'BBV-P-412:AVH:14/07/2016 RQB: SE CREA CATALOGO PERFIL

Imports System.Data
Partial Class aspx_manejaPerfil
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
            If Val(Request("idPerfil")) >= 0 Then
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
            Dim dts As New DataSet
            Dim objPerfil As New SNProcotiza.clsDetallePerfil
            objPerfil.IdPerfil = Val(Request("idPerfil"))

            dts = objPerfil.ManejaPerfil(3)

            If dts.Tables(0).Rows.Count > 0 Then

                lblIDPerfil.Text = dts.Tables(0).Rows(0).Item("ID_PERFIL")
                txtPerfil.Text = dts.Tables(0).Rows(0).Item("PERFIL")
                cmbEstatus.SelectedValue = dts.Tables(0).Rows(0).Item("ESTATUS")
            Else
                MensajeError("No se localizó información para la plaza.")
                Exit Sub
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

            Select Case intOpc
                Case 1
                    'combo de estatus
                    dtsRes = objCombo.ObtenInfoParametros(1, strErr)
                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbEstatus, strErr, True)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If
            End Select

            CargaCombos = True
        Catch ex As Exception
            CargaCombos = False
            MensajeError(ex.Message)
        End Try
    End Function
    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then
                Dim intPerfil As Integer = Val(Request("idPerfil"))
                Dim objPerfil As New SNProcotiza.clsDetallePerfil
                Dim intOpc As Integer = 4

                objPerfil.CargaSession(Val(Session("cveAcceso")))
                If intPerfil >= 0 Then
                    objPerfil.ManejaPerfil(intPerfil)
                End If

                'Guardamos
                objPerfil.IdPerfil = intPerfil
                objPerfil.Perfil = Trim(txtPerfil.Text)
                objPerfil.Estatus = Val(cmbEstatus.SelectedValue)
                objPerfil.UsuarioRegistro = objPerfil.UserNameAcceso
                objPerfil.UsuarioCveRegistro = Val(Session("cveUsu"))


                objPerfil.ManejaPerfil(intOpc)
                If objPerfil.ErrorDetalle = "" Then
                    CierraPantalla("./consultaPerfil.aspx")
                Else
                    MensajeError(objPerfil.ErrorDetalle)
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

        If Trim(txtPerfil.Text) = "" Then Exit Function
        If cmbEstatus.SelectedValue = 0 Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaPerfil.aspx")
    End Sub
End Class
