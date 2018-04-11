Imports System.Data

Partial Class aspx_manejaTiposProducto
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
            If Val(Request("idTipoProd")) > 0 Then
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
            Dim intTipProd As Integer = Val(Request("idTipoProd"))
            Dim objTP As New SNProcotiza.clsTipoProductos(intTipProd)

            If objTP.IDTipoProducto > 0 Then
                lblId.Text = objTP.IDTipoProducto
                txtNom.Text = objTP.Descripcion
                txtUid.Text = objTP.IDExterno
                cmbEstatus.SelectedValue = objTP.IDEstatus
                chkDefault.Checked = IIf(objTP.RegistroDefault = 1, True, False)
                chkValAdap.Checked = IIf(objTP.RequiereValorAdaptacion = 1, True, False)
                chkIncRC.Checked = IIf(objTP.IncluyeRC = 1, True, False)
            Else
                MensajeError("No se localizó información para el el tipo de producto.")
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
            CargaCombos = False
            MensajeError(ex.Message)
        End Try
    End Function

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then
                Dim intTipProd As Integer = Val(Request("idTipoProd"))
                Dim objTP As New SNProcotiza.clsTipoProductos
                Dim intOpc As Integer = 2

                objTP.CargaSession(Val(Session("cveAcceso")))
                If intTipProd > 0 Then
                    objTP.CargaTipoProducto(intTipProd)
                    intOpc = 3
                End If

                'guardamos la info del tipo de operación
                objTP.Descripcion = Trim(txtNom.Text)
                objTP.IDExterno = Trim(txtUid.Text)
                objTP.IDEstatus = Val(cmbEstatus.SelectedValue)
                objTP.RegistroDefault = IIf(chkDefault.Checked, 1, 0)
                objTP.RequiereValorAdaptacion = IIf(chkValAdap.Checked, 1, 0)
                objTP.IncluyeRC = IIf(chkIncRC.Checked, 1, 0)
                objTP.UsuarioRegistro = objTP.UserNameAcceso

                objTP.ManejaTipoProd(intOpc)
                If objTP.ErrorTipoProducto = "" Then
                    CierraPantalla("./consultaTiposProducto.aspx")
                Else
                    MensajeError(objTP.ErrorTipoProducto)
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

        ValidaCampos = True
    End Function

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaTiposProducto.aspx")
    End Sub
End Class
