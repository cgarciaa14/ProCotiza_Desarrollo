'BBV-P-412:AVH:12/07/2016 RQ01: SE CREA DETALLE PERFIL
'BUG-PC-40:PVARGAS:27/01/2017:SE CAMBIA LA OPCION DEL PARAMETRO blnAgregaBlanco DE LA FUNSION LlenaCombos.
'BUG-PC-43 01/02/17 JRHM Se limpia combo status para realizar correctamente la limpieza del formulario 
Imports System.Data

Partial Class aspx_consultaDetallePerfil
    Inherits System.Web.UI.Page
    Dim strErr As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LimpiaError()

        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        If Not IsPostBack Then
            CargaCombos()
            BuscaDatos()
            LimpiaFiltros()
        End If
    End Sub

    Public Sub LimpiaError()
        lblMensaje.Text = ""
        lblScript.Text = ""
    End Sub

    Private Sub LimpiaFiltros()
        txtnombre.Text = ""
        txtusername.Text = ""
        cmbEstatus.SelectedValue = 0
        BuscaDatos()
    End Sub

    Private Function CargaCombos() As Boolean
        Dim objCombo As New clsProcGenerales
        Dim dtsRes As New DataSet
        Try
            CargaCombos = False

            'combo estatus
            dtsRes = objCombo.ObtenInfoParametros(1, strErr)
            If Trim$(strErr) = "" Then
                'BUG-PC-40:PVARGAS:27/01/2017:SE CAMBIA LA OPCION DEL PARAMETRO blnAgregaBlanco DE LA FUNSION LlenaCombos.
                objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbEstatus, strErr, True)
                If Trim$(strErr) <> "" Then
                    MensajeError(strErr)
                    Exit Function
                End If
            Else
                MensajeError(strErr)
                Exit Function
            End If

            'combo perfil
            dtsRes = objCombo.ObtenInfoParametros(70, strErr, False, "1")
            If Trim$(strErr) = "" Then
                objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", ddlperfil, strErr, False)
                Me.ddlperfil.SelectedValue = 74
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

    Private Sub BuscaDatos()
        Try
            Dim objUsu As New SNProcotiza.clsUsuariosSistema
            Dim dtsRes As New DataSet

            objUsu.UserName = txtusername.Text.Trim
            objUsu.Nombre = txtnombre.Text.Trim
            objUsu.IDPerfil = ddlperfil.SelectedValue
            objUsu.IDEstatus = cmbEstatus.SelectedValue

            dtsRes = objUsu.ManejaUsuario(1)

            Session("dtsConsulta") = Nothing
            grvConsulta.PageIndex = 0
            grvConsulta.DataSource = Nothing

            If dtsRes.Tables(0).Rows.Count > 0 Then
                Session("dtsConsulta") = dtsRes
                grvConsulta.DataSource = dtsRes
            Else
                ''MensajeError("No se encontró información con los parámetros proporcionados")
            End If
            grvConsulta.DataBind()

        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
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

    Protected Sub grvConsulta_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "usuId" Then
            Response.Redirect("./manejaDetallePerfil.aspx?idUsu=" & e.CommandArgument)
        End If

        If e.CommandName = "resetPwd" Then
            Dim objUsu As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(e.CommandArgument))
            If objUsu.IDUsuario > 0 Then
                Dim objSeg As New SNProcotiza.clsSeguridad
                objUsu.Password = objSeg.EncriptarCadena("123456")
                objUsu.FechaCambioPwd = DateAdd(DateInterval.Day, -1, Now()).ToString("yyyy-MM-dd")
                objUsu.ManejaUsuario(3)
                If objUsu.ErrorUsuario = "" Then
                    MensajeError("La contraseña se cambió con éxito.")
                Else
                    MensajeError(objUsu.ErrorUsuario)
                End If
            Else
                MensajeError("No se logro cargar la información del usuario.")
            End If
        End If

        If e.CommandName = "AsigAge" Then
            Script = "AbrePopup('relacionaUsuarioAgencias.aspx?UsuarioId=" & e.CommandArgument & "&tipoRel=1',300,100,800,550)"
        End If
    End Sub

    Protected Sub grvConsulta_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsulta"), DataSet)
        grvConsulta.DataBind()
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        BuscaDatos()
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As System.EventArgs) Handles btnLimpiar.Click
        LimpiaFiltros()
        grvConsulta.DataBind()
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As System.EventArgs) Handles btnAgregar.Click
        Response.Redirect("./manejaUsuariosSistema.aspx?idUsu=0")
    End Sub

    Protected Sub ddlperfil_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlperfil.SelectedIndexChanged
        BuscaDatos()
    End Sub

    Protected Sub cmbEstatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEstatus.SelectedIndexChanged
        BuscaDatos()
    End Sub
End Class

