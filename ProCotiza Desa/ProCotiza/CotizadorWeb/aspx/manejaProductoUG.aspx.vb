'BBV-P-412: AVH: RQ 14: 26/07/2016 SE AGREGA VENTANA DE PRODUCTOS UG
Imports System.Data
Partial Class aspx_manejaProductoUG
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
            If Val(Request("idProductoUG")) > 0 Then
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
            Dim objProducto As New SNProcotiza.clsProductosUG
            objProducto.IDProductoUG = Val(Request("idProductoUG"))

            dts = objProducto.ManejaProductoUG(1)

            If dts.Tables(0).Rows.Count > 0 Then

                lblIDProducto.Text = dts.Tables(0).Rows(0).Item("ID_PRODUCTO_UG")
                txtProducto.Text = dts.Tables(0).Rows(0).Item("NOMBRE")
                txtDes.Text = dts.Tables(0).Rows(0).Item("DESCRIPCION")
                cmbEstatus.SelectedValue = dts.Tables(0).Rows(0).Item("ESTATUS")
                chkDefault.Checked = IIf(dts.Tables(0).Rows(0).Item("REG_DEFAULT") = 1, True, False)
            Else
                MensajeError("No se localizó información para la plaza.")
                Exit Sub
            End If
        Catch ex As Exception
            ' MensajeError(ex.Message)
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
                Dim intProducto As Integer = Val(Request("idProductoUG"))
                Dim objProducto As New SNProcotiza.clsProductosUG
                Dim intOpc As Integer = 2

                objProducto.CargaSession(Val(Session("cveAcceso")))

                If intProducto > 0 Then
                    objProducto.CargaProductoUG(intProducto)
                    intOpc = 3
                End If

                'Guardamos
                objProducto.Nombre = Trim(txtProducto.Text)
                objProducto.Descripcion = Trim(txtDes.Text)
                objProducto.IDEstatus = Val(cmbEstatus.SelectedValue)
                objProducto.RegistroDefault = IIf(chkDefault.Checked, 1, 0)
                objProducto.UsuReg = objProducto.UserNameAcceso


                objProducto.ManejaProductoUG(intOpc)
                If objProducto.ErrorProductoUG = "" Then
                    CierraPantalla("./consultaProductoUG.aspx")
                Else
                    MensajeError(objProducto.ErrorProductoUG)
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

        If Trim(txtProducto.Text) = "" Then Exit Function
        If Trim(txtDes.Text) = "" Then Exit Function
        If Trim(txtDes.Text) = "" Then Exit Function
        If cmbEstatus.SelectedValue = 0 Then Exit Function

        ValidaCampos = True
    End Function
    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaProductoUG.aspx")
    End Sub
End Class
