'BBV-P-412: AVH: RQ 15: 27/07/2016 SE AGREGA VENTANA DE PRODUCTOS UG
'BBVA-P-412: JRHM 14/11/2016: BUG-PC-08 se modifico evento para traer productos default
'BUG-PC-20: AVH: 28/11/2016 SE QUITAN VALORES POR DEFAULT EN COMBOS
Imports System.Data
Partial Class aspx_manejaSubProductoUG
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
            CargaCombos(2)
            If Val(Request("idSubProductoUG")) <> 0 Then
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
            Dim objProducto As New SNProcotiza.clsSubProductosUG
            objProducto.IDSubProductoUG = Val(Request("idSubProductoUG"))

            dts = objProducto.ManejaSubProductoUG(1)

            If dts.Tables(0).Rows.Count > 0 Then

                lblIDProducto.Text = dts.Tables(0).Rows(0).Item("ID_SUBPRODUCTO_UG")
                txtSubProducto.Text = dts.Tables(0).Rows(0).Item("NOMBRE")
                txtDes.Text = dts.Tables(0).Rows(0).Item("DESCRIPCION")
                cmbEstatus.SelectedValue = dts.Tables(0).Rows(0).Item("ESTATUS")
                ddlProductoUG.SelectedValue = dts.Tables(0).Rows(0).Item("ID_PRODUCTO_UG")
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
        Dim objProducto As New SNProcotiza.clsProductosUG
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
                Case 2
                    'combo de estatus
                    dtsRes = objProducto.ManejaProductoUG(1)
                    'JRHM 14/11/16 SE AGREGA UN FILTRO PARA QUITAR DE LAS OPCIONES PRODUCTOS INACTIVOS
                    Dim rows As DataRow() = (From t In dtsRes.Tables(0).AsEnumerable().Cast(Of DataRow)()
                                          Where t.Field(Of Integer)("ESTATUS") = 3).ToArray()
                    For Each row As DataRow In rows
                        dtsRes.Tables(0).Rows.Remove(row)
                    Next
                    'JRHM 14/11/16 AQUI TERMINA EL FILTRO
                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_PRODUCTO_UG", ddlProductoUG, strErr, False, True, "REG_DEFAULT") 'JRHM 14/11/16 SE AGREGARON PARAMETROS PARA QUE SE SELECCIONE EL PRODUCTO DEFAULT EN EL COMBO DE PRODUCTOS.
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
                Dim strProducto As Integer = Val(Request("idSubProductoUG"))
                Dim objProducto As New SNProcotiza.clsSubProductosUG
                Dim intOpc As Integer = 2

                objProducto.CargaSession(Val(Session("cveAcceso")))

                If lblIDProducto.Text <> "" Then
                    objProducto.IDSubProductoUG = strProducto
                    intOpc = 3
                End If

                'Guardamos
                objProducto.Nombre = Trim(txtSubProducto.Text)
                objProducto.Descripcion = Trim(txtDes.Text)
                objProducto.IDEstatus = Val(cmbEstatus.SelectedValue)
                objProducto.RegistroDefault = IIf(chkDefault.Checked, 1, 0)
                objProducto.UsuReg = objProducto.UserNameAcceso
                objProducto.IDProductoUG = Val(ddlProductoUG.SelectedValue)


                objProducto.ManejaSubProductoUG(intOpc)
                If objProducto.ErrorSubProductoUG = "" Then
                    CierraPantalla("./consultaSubProductoUG.aspx")
                Else
                    MensajeError(objProducto.ErrorSubProductoUG)
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

        If Trim(txtSubProducto.Text) = "" Then Exit Function
        If Trim(txtDes.Text) = "" Then Exit Function
        If ddlProductoUG.SelectedValue = 0 Then Exit Function
        If cmbEstatus.SelectedValue = 0 Then Exit Function

        ValidaCampos = True
    End Function
    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaSubProductoUG.aspx")
    End Sub

End Class
