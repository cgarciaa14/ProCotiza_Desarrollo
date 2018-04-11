'BUGPC03 CO02-CO19: 10/11/2016: GVARGAS: Correccion bugs

Imports System.Data

Partial Class aspx_manejaAccesorios
    Inherits System.Web.UI.Page
    Dim strErr As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()

        If Not IsPostBack Then
            'chkAfectSeg.Checked = True
            CargaCombos(1)
            If Val(Request("idAccesorio")) > 0 Then
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
            Dim intAcc As Integer = Val(Request("idAccesorio"))
            Dim objAcc As New SNProcotiza.clsAccesorios(intAcc)

            If objAcc.IDAccesorio > 0 Then
                lblId.Text = objAcc.IDAccesorio
                txtNom.Text = objAcc.Descripcion
                txtPrecio.Text = objAcc.Precio
                cmbEstatus.SelectedValue = objAcc.IDEstatus
                chkAfectSeg.Checked = IIf(objAcc.AfectaCalculoSeguro = 1, True, False)
                chkDefault.Checked = IIf(objAcc.RegistroDefault = 1, True, False)
                cmbTipoProd.SelectedValue = objAcc.IDTipoProducto
                cmbMarca.SelectedValue = objAcc.IDMarca
            Else
                MensajeError("No se localizó información para el accesorio.")
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

                    'combo de tipos de producto
                    Dim objTP As New SNProcotiza.clsTipoProductos
                    objTP.IDEstatus = 2

                    dtsRes = objTP.ManejaTipoProd(1)
                    strErr = objTP.ErrorTipoProducto

                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "DESCRIPCION", "ID_TIPO_PRODUCTO", cmbTipoProd, strErr, False)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    End If

                    'combo de marcas
                    Dim objMarca As New SNProcotiza.clsMarcas
                    objMarca.IDEstatus = 2
                    objMarca.IDTipoRegistro = 113 'trae marcas que puedan registrar productos

                    dtsRes = objMarca.ManejaMarca(5)
                    strErr = objMarca.ErrorMarcas

                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_MARCA", cmbMarca, strErr, False)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
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
                Dim intAcc As Integer = Val(Request("idAccesorio"))
                Dim objAcc As New SNProcotiza.clsAccesorios
                Dim intOpc As Integer = 2

                objAcc.CargaSession(Val(Session("cveAcceso")))
                If intAcc > 0 Then
                    objAcc.CargaAccesorio(intAcc)
                    intOpc = 3
                End If

                If (txtPrecio.Text = "") Then
                    Throw New System.Exception("Debe ingresar un precio.")
                End If

                'guardamos la info del tipo de operación
                objAcc.Descripcion = Trim(txtNom.Text)
                objAcc.Precio = Val(txtPrecio.Text)
                objAcc.IDTipoProducto = Val(cmbTipoProd.SelectedValue)
                objAcc.IDMarca = Val(cmbMarca.SelectedValue)
                objAcc.IDEstatus = Val(cmbEstatus.SelectedValue)
                objAcc.AfectaCalculoSeguro = IIf(chkAfectSeg.Checked, 1, 0)
                objAcc.RegistroDefault = IIf(chkDefault.Checked, 1, 0)
                objAcc.UsuarioRegistro = objAcc.UserNameAcceso

                objAcc.ManejaAccesorio(intOpc)
                If objAcc.ErrorAccesorios = "" Then
                    CierraPantalla("./consultaAccesorios.aspx")
                Else
                    MensajeError(objAcc.ErrorAccesorios)
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
        If cmbTipoProd.SelectedValue = "" Then Exit Function
        If cmbMarca.SelectedValue = "" Then Exit Function
        If Trim(txtNom.Text) = "" Then Exit Function
        'If Trim(txtPrecio.Text) = "" Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaAccesorios.aspx")
    End Sub
End Class
