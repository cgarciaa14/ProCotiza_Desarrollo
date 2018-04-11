Imports System.Data

Partial Class aspx_manejaPromocionesCte
    Inherits System.Web.UI.Page
    Dim strErr As String = ""
    Dim objPromo As New SNProcotiza.clsPromocionesCte()


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()

        If Not IsPostBack Then
            CargaCombos()

            If Val(Request("IdPromo")) > 0 Then
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

    Private Function CargaCombos() As Boolean
        Dim objCombo As New clsProcGenerales
        Dim dtsRes As New DataSet
        Try
            CargaCombos = False


            'Combo Estatus
            dtsRes = objCombo.ObtenInfoParametros(1, strErr)
            If Trim$(strErr) = "" Then
                objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", ddlEstatus, strErr, False)
                If Trim$(strErr) <> "" Then
                    MensajeError(strErr)
                    Exit Function
                End If
            Else
                MensajeError(strErr)
                Exit Function
            End If

            'Combo Periodicidad
            dtsRes = objCombo.ObtenInfoParametros(82, strErr, False, "1")
            If Trim$(strErr) = "" Then
                objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", ddlPeriodicidad, strErr, False)
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

    Private Sub CargaInfo()
        Try

            objPromo.CargaPromocion(Val(Request("IdPromo")))

            If objPromo.IDPROMOCION > 0 Then
                lblid.Text = objPromo.IDPROMOCION
                txtDesc.Text = objPromo.DESCRIPCION
                ddlEstatus.SelectedValue = objPromo.IDESTATUS
                txtidExterno.Text = objPromo.IDEXTERNO
                txtIdCte.Text = objPromo.IDCLIENTE
                ddlPeriodicidad.SelectedValue = objPromo.IDPERIODICIDAD
            Else
                MensajeError("No se localizó información para la Promoción.")
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then
                Dim intOpc As Integer = 2
                objPromo.CargaSession(Val(Session("cveAcceso")))
                If Val(Request("IdPromo")) > 0 Then
                    objPromo.CargaPromocion(Val(Request("IdPromo")))
                    intOpc = 3
                End If

                'guardamos la info

                objPromo.DESCRIPCION = txtDesc.Text.Trim
                objPromo.IDESTATUS = ddlEstatus.SelectedValue
                objPromo.IDEXTERNO = txtidExterno.Text.Trim
                objPromo.IDCLIENTE = txtIdCte.Text.Trim
                objPromo.IDPERIODICIDAD = ddlPeriodicidad.SelectedValue
                objPromo.UsuarioRegistro = objPromo.UserNameAcceso

                objPromo.ManejaPromocion(intOpc)
                If objPromo.ErrorPromociono = "" Then
                    CierraPantalla("./consultaPromocionesCte.aspx")
                Else
                    MensajeError(objPromo.ErrorPromociono)
                    Exit Sub
                End If
            Else
                MensajeError("Todos los campos marcados con * son obligatorios.")
                Exit Sub
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If txtDesc.Text.Trim.Length = 0 Then Exit Function
        If ddlEstatus.SelectedValue = 0 Then Exit Function
        If txtidExterno.Text.Trim.Length = 0 Then Exit Function
        If txtIdCte.Text.Trim.Length = 0 Then Exit Function
        If ddlPeriodicidad.SelectedValue = 0 Then Exit Function

        ValidaCampos = True

    End Function

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./ConsultaPromocionesCte.aspx")
    End Sub
End Class
