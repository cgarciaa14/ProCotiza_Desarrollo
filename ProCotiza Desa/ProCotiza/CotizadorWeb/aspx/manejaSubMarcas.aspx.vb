'BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.
'BUG-PC-09:AMR:16/11/2016 CUTARMA01:No pertime espacios en blanco al ingresar el nombre de la marca
'BBVA-P-412 BUG-PC-23 29/11/16 JRHM Se modifico como txtidexterno toma su valor siendo que si su valor es cero el txtbox quedara con campo vacio,
'BUG-PC-39 25/01/17 Correccion de errores multiples
Imports System.Data

Partial Class aspx_manejaSubMarcas
    Inherits System.Web.UI.Page

    Dim strErr As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()

        If Not IsPostBack Then
            If Not CargaCombos() Then
                MensajeError(strErr)
            End If
            If Val(Request("idSubMarca")) > 0 Then
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

    Private Function CargaCombos() As Boolean
        Dim objCombo As New clsProcGenerales
        Dim objMarcas As New SNProcotiza.clsMarcas
        Dim dtsRes As New DataSet

            CargaCombos = False

        Try
            'Combo Marcas
            objMarcas.IDEstatus = 2
            dtsRes = objMarcas.ManejaMarca(1)

            'BUG-PC-09
            If objMarcas.ErrorMarcas = "" Then
                If dtsRes.Tables.Count > 0 Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                        objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_MARCA", ddlmarca, strErr, False, True, "REG_DEFAULT")
                    If strErr <> "" Then
                        Exit Function
                    End If
                Else
                        strErr = "No se pudo recuperar información de Marcas."
                    Exit Function
                End If
                    ddlmarca.SelectedValue = Val(Request("idMarca"))
            Else
                    strErr = "No se pudo recuperar información de Marcas."
                    Exit Function
                End If
            Else
                strErr = objMarcas.ErrorMarcas
                Exit Function
            End If

            'Combo Estatus
            dtsRes = objCombo.ObtenInfoParametros(1, strErr)
            If Trim$(strErr) = "" Then
                objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", ddlestatus, strErr, False)
                If Trim$(strErr) <> "" Then
                    Exit Function
                End If
            Else
                Exit Function
            End If

            CargaCombos = True
        Catch ex As Exception
            strErr = ex.Message
        End Try
    End Function

    Private Sub CargaInfo()
        Try
            Dim intSubMarca As Integer = Val(Request("idSubMarca"))
            Dim objSubMarca As New SNProcotiza.clsSubMarcas(intSubMarca)

            If objSubMarca.ErrorSubMarcas = "" Then
                lblidsubmarca.Text = objSubMarca.IDSubmarca
                ddlmarca.SelectedValue = objSubMarca.IDMarca
                txtsubmarca.Text = objSubMarca.Descripcion
                ddlestatus.SelectedValue = objSubMarca.IDEstatus
                txtidexterno.Text = IIf(objSubMarca.IDExterno = 0, "", objSubMarca.IDExterno)
                chkdefault.Checked = IIf(objSubMarca.IDRegDefault = 1, True, False)
            Else
                MensajeError(objSubMarca.ErrorSubMarcas)
                Exit Sub
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If ddlmarca.SelectedValue = "" Then Exit Function
        If ddlestatus.SelectedValue = "" Then Exit Function
        If txtsubmarca.Text.Trim = "" Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then
                Dim objSubMarcas As New SNProcotiza.clsSubMarcas
                Dim intOpc As Integer = 2

                objSubMarcas.CargaSession(Val(Session("cveAcceso")))

                If Val(Request("idSubMarca")) > 0 Then
                    objSubMarcas.CargaSubMarca(Val(Request("idSubMarca")))
                    intOpc = 3
                End If

                objSubMarcas.IDMarca = ddlmarca.SelectedValue
                objSubMarcas.Descripcion = txtsubmarca.Text.Trim
                objSubMarcas.IDExterno = Val(txtidexterno.Text.Trim)
                objSubMarcas.IDRegDefault = IIf(chkdefault.Checked, 1, 0)
                objSubMarcas.IDEstatus = ddlestatus.SelectedValue
                objSubMarcas.UsuReg = objSubMarcas.UserNameAcceso
                Dim dts As New DataSet
                dts = objSubMarcas.ManejaSubMarca(intOpc)
                If dts.Tables(0).Rows(0).Item("ID_SUBMARCA") = -1 Then
                    MensajeError("El nombre y marca seleccionada de  ya existe.")
                    Exit Sub
                End If

                If objSubMarcas.ErrorSubMarcas = "" Then
                    CierraPantalla("./consultaSubMarcas.aspx")
                Else
                    MensajeError(objSubMarcas.ErrorSubMarcas)
                    Exit Sub
                End If
            Else
                MensajeError("Todos los campos marcados con * son obligatorios.")
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaSubMarcas.aspx")
    End Sub
End Class
