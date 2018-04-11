Imports System.Data
''BBVA-P-412: 08/08/2016: AMR: RQ09 – Se crea scroll en la pantalla
'BUG-PC-27 MAUT 22/12/2016 Se corrige carga de perfil
'BUG PC 32 GVARGAS 06/01/2017 Mostrar solo perfiles <> inativos
'BUG-PC-40:PVARGAS:27/01/2017:SE AGREGA LA OPCION SELECCIONAR AL COMBO.
Partial Class aspx_manejaPermisosPerfil
    Inherits System.Web.UI.Page
    Dim strErr As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()

        If Not IsPostBack Then
            Script = "document.getElementById('trBotones').style.display = 'none';"
            CargaCombos()
            BuscaDatos()
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

    Private Function CargaCombos() As Boolean
        Dim objCombo As New clsProcGenerales
        Dim dtsRes As New DataSet
        Try
            CargaCombos = False

            'combo de menus
            Dim objMenu As New SNProcotiza.clsObjetosSistema
            objMenu.IDObjetoPadre = 0
            dtsRes = objMenu.ManejaObjetoSis(1)
            If Trim$(objMenu.ErrorObjetos) = "" Then
                'BUG-PC-40:PVARGAS:27/01/2017:SE AGREGA LA OPCION SELECCIONAR AL COMBO.
                objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_OBJETO", cmbMenu, strErr, True)
                If Trim$(strErr) <> "" Then
                    MensajeError(strErr)
                    Exit Function
                End If
            Else
                MensajeError(strErr)
                Exit Function
            End If

            'combo de perfiles

            'BUG-PC-27 MAUT 22/12/2016 Se cambia carga de perfiles por ManejaPerfil
            'BUG PC 32 Mostrar solo perfiles <> inativos
            Dim objPerfil As New SNProcotiza.clsDetallePerfil
            dtsRes = objPerfil.ManejaPerfil(5)



            'dtsRes = objCombo.ObtenInfoParametros(70, strErr, False, "1")
            If Trim$(strErr) = "" Then
                'BUG-PC-40:PVARGAS:27/01/2017:SE AGREGA LA OPCION SELECCIONAR AL COMBO.
                objCombo.LlenaCombos(dtsRes, "PERFIL", "ID_PERFIL", cmbPerfil, strErr, True)
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

            objUsu.IDPerfil = cmbPerfil.SelectedValue
            objUsu.IdObjeto = cmbMenu.SelectedValue

            dtsRes = objUsu.ManejaUsuario(8)

            If objUsu.ErrorUsuario = "" Then
                If dtsRes.Tables.Count > 0 Then
                    If dtsRes.Tables(0).Rows.Count > 0 Then
                        grvConsulta.DataSource = dtsRes
                        grvConsulta.DataBind()
                    End If
                End If
            Else
                MensajeError(objUsu.ErrorUsuario)
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        BuscaDatos()
    End Sub

    Protected Sub chkmenu_CheckedChanged(sender As Object, e As System.EventArgs)
        Dim chk As CheckBox = DirectCast(sender, CheckBox)
        Dim gr As GridViewRow = DirectCast(chk.Parent.Parent, GridViewRow)
        Dim idMenu As Integer
        Dim objmenu As New SNProcotiza.clsUsuariosSistema

        idMenu = Convert.ToInt16(grvConsulta.DataKeys(gr.RowIndex).Value.ToString())

        If chk.Checked = True Then
            objmenu.IdObjeto = idMenu
            objmenu.IDPerfil = cmbPerfil.SelectedValue
            objmenu.ManejaUsuario(9)
        Else
            objmenu.IdObjeto = idMenu
            objmenu.IDPerfil = cmbPerfil.SelectedValue
            objmenu.ManejaUsuario(10)
        End If

    End Sub

    Protected Sub cmbMenu_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbMenu.SelectedIndexChanged
        BuscaDatos()
    End Sub

    Protected Sub cmbPerfil_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbPerfil.SelectedIndexChanged
        BuscaDatos()
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As System.EventArgs) Handles btnLimpiar.Click
        'BUG-PC-40:PVARGAS:27/01/2017:SE CARGAN NUEVAMENTE LOS REGISTROS DE LA TABLA AL LIMPIAR LAS OPCIONES DE BUSQUEDA
        CargaCombos()
        BuscaDatos()
    End Sub
End Class
