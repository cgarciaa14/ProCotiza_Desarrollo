Imports System.Data

Partial Class aspx_manejaEstados
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
            If Val(Request("idEstado")) > 0 Then
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
            Dim intEdo As Integer = Val(Request("idEstado"))
            Dim objEdo As New SNProcotiza.clsEstados(intEdo)

            If objEdo.IDEstado > 0 Then
                lblId.Text = objEdo.IDEstado
                txtNom.Text = objEdo.Nombre
                txtCveEdo.Text = objEdo.Clave
                txtUid.Text = objEdo.IDExterno
                cmbEstatus.SelectedValue = objEdo.IDEstatus
            Else
                MensajeError("No se localizó información para el estado.")
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
                        objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbEstatus, strErr, False)
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
                Dim intEdo As Integer = Val(Request("idEstado"))
                Dim objEdo As New SNProcotiza.clsEstados
                Dim intOpc As Integer = 2

                objEdo.CargaSession(Val(Session("cveAcceso")))
                If intEdo > 0 Then
                    objEdo.CargaEstado(intEdo)
                    intOpc = 3
                End If

                'guardamos la info del tipo de operación
                objEdo.Nombre = Trim(txtNom.Text)
                objEdo.IDExterno = Trim(txtUid.Text)
                objEdo.Clave = Trim(txtCveEdo.Text)
                objEdo.IDEstatus = Val(cmbEstatus.SelectedValue)
                objEdo.UsuarioRegistro = objEdo.UserNameAcceso

                objEdo.ManejaEstado(intOpc)
                If objEdo.ErrorEstados = "" Then
                    CierraPantalla("./consultaEstados.aspx")
                Else
                    MensajeError(objEdo.ErrorEstados)
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
        If Trim(txtCveEdo.Text) = "" Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaEstados.aspx")
    End Sub
End Class
