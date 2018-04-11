'BUG-PC-09:AMR:16/11/2016 CUTARMA01:No pertime espacios en blanco al ingresar el nombre de la marca
'BUG-PC-21: AMR: 28/11/2016 El nombre de la Marca se muestra el texto en Mayúsculas.
'BUG-PC-39 25/01/17 Correccion de errores multiples

Imports System.Data

Partial Class aspx_manejaMarcas
    Inherits System.Web.UI.Page
    Dim strErr As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()

        If Not IsPostBack Then

            'BUG-PC-09
            trtipouso.Visible = False
            If Not CargaCombos(1) Then
                MensajeError(strErr)
                Exit Sub
            End If
            If Val(Request("idMarca")) > 0 Then
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
            Dim intMarca As Integer = Val(Request("idMarca"))
            Dim objMarca As New SNProcotiza.clsMarcas(intMarca)

            If objMarca.IDMarca > 0 Then
                lblId.Text = objMarca.IDMarca
                txtNom.Text = objMarca.Nombre
                cmbEstatus.SelectedValue = objMarca.IDEstatus
                cmbTipoReg.SelectedValue = objMarca.IDTipoRegistro
                chkDefault.Checked = IIf(objMarca.RegistroDefault = 1, True, False)
            Else
                MensajeError("No se localizó información para la marca.")
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

                    'combo de tipo de uso (registro)
                    dtsRes = objCombo.ObtenInfoParametros(111, strErr)
                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbTipoReg, strErr, False)
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
                Dim intMarca As Integer = Val(Request("idMarca"))
                Dim objMarca As New SNProcotiza.clsMarcas
                Dim intOpc As Integer = 2

                objMarca.CargaSession(Val(Session("cveAcceso")))
                If intMarca > 0 Then
                    objMarca.CargaMarca(intMarca)
                    intOpc = 3
                End If

                'guardamos la info del tipo de operación
                objMarca.Nombre = Trim(txtNom.Text) '.ToUpper()
                objMarca.IDEstatus = Val(cmbEstatus.SelectedValue)
                objMarca.IDTipoRegistro = 113 '' Val(cmbTipoReg.SelectedValue)
                objMarca.RegistroDefault = IIf(chkDefault.Checked, 1, 0)
                objMarca.UsuarioRegistro = objMarca.UserNameAcceso

                Dim dts As New DataSet
                dts = objMarca.ManejaMarca(intOpc)
                If dts.Tables(0).Rows(0).Item("ID_MARCA") = -1 Then
                    MensajeError("El nombre de Marca ya existe.")
                    Exit Sub
                End If
                If objMarca.ErrorMarcas = "" Then
                    CierraPantalla("./consultaMarcas.aspx")
                Else
                    MensajeError(objMarca.ErrorMarcas)
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
        If cmbTipoReg.SelectedValue = "" Then Exit Function
        If Trim(txtNom.Text) = "" Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaMarcas.aspx")
    End Sub
End Class
