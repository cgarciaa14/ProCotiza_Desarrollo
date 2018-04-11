'BBV-P-412:AVH:06/07/2016 RQ17: SE CREA CATALOGO DE GRUPOS
'BUG-PC-39 25/01/17 JRHM Se corrigen varios errores
'BUG-PC-44 07/02/17 JRHM se agrega funcion del boton limpiar y funcion para la busqueda al cambiar la asignacion de una agencia
Imports System.Data
Imports SNProcotiza
Partial Class aspx_RelacionaAgenciaGrupo
    Inherits System.Web.UI.Page
    Dim strErr As String = String.Empty
    Dim objPaq As New clsPaquetes
    Dim objAgencias As New clsAgencias
    Dim ID_GRUPO As Integer

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()
        ID_GRUPO = Val(Request("idGrupo"))

        Dim dts As New DataSet
        Dim objGrupo As New SNProcotiza.clsGrupos

        objGrupo.idGrupo = Val(Request("idGrupo"))
        dts = objGrupo.ManejaGrupo(1)

        If dts.Tables(0).Rows(0).Item("ESTATUS") = 3 Then
            lblMensaje.Text += "<script>alert('No se pueden asignar Agencias a Grupos inactivos.'); window.close();</script>"
        End If


        lblTitulo.Text = "RELACIONA AGENCIA - GRUPO (" & (dts.Tables(0).Rows(0).Item("GRUPO")).ToString & ")"


        If Not IsPostBack Then
            CargaCombos(1)
            cmbGrupo.SelectedValue = Val(Request("idGrupo"))
            CargaCombos(2)
        End If
    End Sub
    Private Sub CargaInfo()
        Try
            Dim dts As New DataSet
            Dim objGrupo As New SNProcotiza.clsGrupos
            objGrupo.idGrupo = Val(Request("idGrupo"))

            dts = objGrupo.ManejaGrupo(1)

        Catch ex As Exception
            ' MensajeError(ex.Message)
        End Try
    End Sub
    Public Sub LimpiaError()
        lblMensaje.Text = String.Empty
        lblScript.Text = String.Empty
    End Sub
    Public Sub MensajeError(ByVal strMsj As String)
        strMsj = Replace(strMsj, "'", "")
        strMsj = Replace(strMsj, """", "")
        strMsj = Replace(strMsj, "(", "")
        strMsj = Replace(strMsj, ")", "")
        lblMensaje.Text = "<script>alert('" & strMsj & "')</script>"
    End Sub
    Private Function CargaCombos(ByVal intOpc As Integer) As Boolean

        Dim objGrupos As New clsGrupos
        Dim objCombo As New clsProcGenerales
        Dim dtsRes As New DataSet

        Try
            CargaCombos = False

            Select Case intOpc
                Case 1

                    objAgencias.IDEstatus = 2
                    dtsRes = objGrupos.ManejaGrupo(1)
                    strErr = objGrupos.ErrorGrupo
                    If Trim$(strErr) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                objCombo.LlenaCombos(dtsRes, "GRUPO", "ID_GRUPO", cmbGrupo, strErr, True, , , -1)
                                cmbGrupo.SelectedValue = dtsRes.Tables(0).Rows(0).Item("ID_GRUPO").ToString
                                If Trim$(strErr) <> "" Then
                                    MensajeError(strErr)
                                    Exit Function
                                End If
                            End If
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If

                Case 2
                    'If cmbGrupo.SelectedValue <> 0 Then
                    '    objGrupos.IDGrupoFiltro = cmbGrupo.SelectedValue

                    'End If

                    objGrupos.IDEstatus = 2
                    objGrupos.IDGrupo = Val(Request("idGrupo"))
                    objGrupos.IDGrupoFiltro = Val(Request("idGrupo"))

                    dtsRes = objGrupos.ManejaGrupo(7)
                    strErr = objGrupos.ErrorGrupo
                    If Trim$(strErr) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                Session("dtsConsultaAG") = dtsRes
                                grvConsulta.DataSource = dtsRes
                                grvConsulta.DataBind()
                            Else
                                grvConsulta.DataSource = Nothing
                                grvConsulta.DataBind()
                            End If
                        End If
                    Else
                        MensajeError(strErr)
                    End If
            End Select

            CargaCombos = True
        Catch ex As Exception
            CargaCombos = False
            MensajeError(ex.Message)
        End Try
    End Function
    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsultaAG"), DataSet)
        grvConsulta.DataBind()
    End Sub
    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "ID_GRUPO" Then
            Dim objTO As New clsGrupos
            Dim intConsul As Integer = 7
            Dim intBorra As Integer = 8
            Dim intInserta As Integer = 9

            Dim dtsRes As New DataSet
            Dim objRow As GridViewRow = grvConsulta.Rows(Val(e.CommandArgument))
            Dim objImg As ImageButton = objRow.Cells(3).Controls(1)

            Dim idAgencia As String = grvConsulta.DataKeys(Convert.ToInt32(e.CommandArgument)).Values(0).ToString()

            objTO.idGrupo = ID_GRUPO
            objTO.IDAgencia = Val(idAgencia)
            objTO.UsuarioRegistro = objTO.UserNameAcceso
            dtsRes = objTO.ManejaGrupo(intConsul)

            If dtsRes.Tables(0).Rows.Count > 0 Then
                If dtsRes.Tables(0).Rows(0).Item("ID_GRUPO") = 0 Then
                    objTO.ManejaGrupo(intInserta)
                    objImg.ImageUrl = "../img/tick.png"
                Else
                    objTO.ManejaGrupo(intBorra)
                    objImg.ImageUrl = "../img/cross.png"
                End If
            Else
                objTO.ManejaGrupo(intInserta)
                objImg.ImageUrl = "../img/tick.png"
            End If

        End If
        'CargaCombos(2)
        Buscar()

    End Sub
    Protected Sub grvConsulta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvConsulta.RowDataBound
        Dim _objImg As New ImageButton

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim datakey As String = Convert.ToString(grvConsulta.DataKeys(e.Row.RowIndex).Values(2).ToString)

            _objImg = e.Row.FindControl("ImageButton1")
            If datakey <> 0 Then
                _objImg.ImageUrl = "../img/tick.png"
            Else : datakey = 0
                _objImg.ImageUrl = "../img/cross.png"
            End If
        End If
    End Sub

    Protected Sub cmbGrupo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbGrupo.SelectedIndexChanged
        Dim objGrupos As New clsGrupos
        Dim dtsRes As New DataSet

        If cmbGrupo.SelectedValue <> -1 Then
            objGrupos.IDGrupoFiltro = cmbGrupo.SelectedValue
        End If

        If (txtNom.Text <> "") Then
        objGrupos.Agencia = txtNom.Text
        End If

        'objAlianzas.IDAgencia = cmbAgencia.SelectedValue
        objGrupos.IDGrupo = Val(Request("idGrupo"))

        dtsRes = objGrupos.ManejaGrupo(7)
        strErr = objGrupos.ErrorGrupo

        If Trim$(strErr) = "" Then
            If dtsRes.Tables.Count > 0 Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    Session("dtsConsultaAG") = dtsRes
                    grvConsulta.DataSource = dtsRes
                    grvConsulta.DataBind()
                Else
                    grvConsulta.DataSource = Nothing
                    grvConsulta.DataBind()
                End If
            End If


        Else
            MensajeError(strErr)
        End If
        Buscar()
    End Sub

    Protected Sub bntBuscaProd_Click(sender As Object, e As System.EventArgs) Handles bntBuscaProd.Click
        Buscar()
    End Sub

    Protected Sub btnTodas_Click(sender As Object, e As System.EventArgs) Handles btnTodas.Click
        Dim objGrupos As New clsGrupos
        Dim dtsRes As New DataSet
        Dim intInserta As Integer = 9

        If cmbGrupo.SelectedValue <> -1 Then
            objGrupos.IDGrupoFiltro = cmbGrupo.SelectedValue
        End If

        If txtNom.Text <> "" Then
            objGrupos.Agencia = txtNom.Text
        End If

        objGrupos.IDGrupo = Val(Request("idGrupo"))
        objGrupos.ManejaGrupo(intInserta)
        'cmbGrupo_SelectedIndexChanged(cmbGrupo, Nothing)
        Buscar()
    End Sub

    Protected Sub btnNinguna_Click(sender As Object, e As System.EventArgs) Handles btnNinguna.Click
        Dim objGrupos As New clsGrupos
        Dim dtsRes As New DataSet
        Dim intBorra As Integer = 8

        If cmbGrupo.SelectedValue <> -1 Then
            objGrupos.IDGrupoFiltro = cmbGrupo.SelectedValue
        End If

        If txtNom.Text <> "" Then
            objGrupos.Agencia = txtNom.Text
        End If

        objGrupos.idGrupo = Val(Request("idGrupo"))
        objGrupos.ManejaGrupo(intBorra)
        cmbGrupo_SelectedIndexChanged(cmbGrupo, Nothing)
        Buscar()
    End Sub
    Public Sub Limpiar()
        txtNom.Text = ""
        cmbGrupo.SelectedValue = -1
    End Sub
    Public Sub Buscar()
        Dim objGrupos As New clsGrupos
        Dim dtsRes As New DataSet

        If cmbGrupo.SelectedValue <> -1 Then
            objGrupos.IDGrupoFiltro = cmbGrupo.SelectedValue
        End If

        objGrupos.IDGrupo = Val(Request("idGrupo"))
        objGrupos.Agencia = txtNom.Text
        dtsRes = objGrupos.ManejaGrupo(7)
        strErr = objGrupos.ErrorGrupo

        If Trim$(strErr) = "" Then
            If dtsRes.Tables.Count > 0 Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    Session("dtsConsultaAG") = dtsRes
                    grvConsulta.DataSource = dtsRes
                    grvConsulta.DataBind()
                Else
                    grvConsulta.DataSource = Nothing
                    grvConsulta.DataBind()
                End If
            End If
        Else
            MensajeError(strErr)
        End If
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        Limpiar()
        Buscar()
    End Sub
End Class
