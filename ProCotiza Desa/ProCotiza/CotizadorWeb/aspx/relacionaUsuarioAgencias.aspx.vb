'BUG-PC-04: AVH: 11/11/2016 se agrega valor por default en los combos
'BUG-PC-13: AMR: 23/11/2016 Se quita valor por default en los combos.
'BUG-PC-30 AMR 03/01/2017: Al ingresar al portal procotiza e ingresar a la opcion de cotizacion manda un mensaje de warning.
'BUG-PC-43 01/02/17 JRHM Se agregara nueva funcion limpiar
'RQ-INB221: 18/07/2017:ERODRIGUEZ: permitir para vendedores seleccionar mas de una agencia siempre y cuando sean de la misma alianza.
'BUG-PC-103:JBEJAR: 21/08/2017 CAMBIO
'RQ-PC9: DCORNEJO: 15/05/2018: SE AGREGAN COMBOS ALIANZA,DIVISION,GRUPO,ESTADO
Imports System.Data
Imports SNProcotiza

Partial Class aspx_relacionaUsuarioAgencias
    Inherits System.Web.UI.Page
    Dim strErr As String = String.Empty
    Dim objUser As New clsUsuariosSistema

    Dim iduser As Integer = 0


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))
        Session("usrreg") = objUsuFirma.UserName
        iduser = objUsuFirma.IDUsuario
        ViewState("wvsUsr") = iduser
        LimpiaError()

        If Not IsPostBack Then
            If CargaCombos(1) Then
                CargaCombos(2)
                CargaCombos(3)
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

    Private Function CargaCombos(ByVal intOpc As Integer) As Boolean

        Dim objCombo As New clsProcGenerales
        Dim objEdo As New clsEstados
        Dim objalianza As New clsAlianzas
        Dim objgrupo As New clsGrupos
        Dim objdivision As New clsDivisiones
        Dim dtsRes As New DataSet

        Try
            CargaCombos = False

            Select Case intOpc
                Case 1
                    Session("dtsConsultaAgencias") = Nothing
                    grvConsulta.PageIndex = 0
                    grvConsulta.DataSource = Nothing

                    objUser.IDUsuario = (Val(Request("UsuarioId")))
                    objUser.IDEstatus = 2
                    dtsRes = objUser.ManejaUsuario(1)
                    strErr = objUser.ErrorUsuario
                    If Trim$(strErr) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_USUARIO", cmbUsuario, strErr, False)
                                If Trim$(strErr) <> "" Then
                                    Exit Function
                                End If
                            Else
                                lblMensaje.Text += "<script>alert('No se pueden asignar Agencias a usuarios inactivos.'); window.close();</script>"
                                Exit Function
                            End If
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If
                    txtUsuario.Text = cmbUsuario.SelectedItem.Text.ToString()
                Case 2
                    'RQ-PC9: DCORNEJO
                    If Trim$(strErr) = "" Then
                        objalianza.IDEstatus = 2
                        dtsRes = objalianza.ManejaAlianza(1)
                        objCombo.LlenaCombos(dtsRes, "ALIANZA", "ID_ALIANZA", cmbAlianza, strErr, True, , , -1)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If

                    If Trim$(strErr) = "" Then
                        objgrupo.IDEstatus = 2
                        dtsRes = objgrupo.ManejaGrupo(1)
                        objCombo.LlenaCombos(dtsRes, "GRUPO", "ID_GRUPO", cmbGrupo, strErr, True, , , -1)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If


                    If Trim$(strErr) = "" Then
                        objdivision.IDEstatus = 2
                        dtsRes = objdivision.ManejaDivision(1)
                        objCombo.LlenaCombos(dtsRes, "DIVISION", "ID_DIVISION", cmbDivision, strErr, True, , , -1)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If

                    If Trim$(strErr) = "" Then
                        objEdo.IDUsuario = (Val(Request("UsuarioId")))
                        dtsRes = objEdo.ManejaEstado(5)
                        objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_ESTADO", cmbEstado, strErr, True, , , -1)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If

                Case 3
                    'RQ-PC9: DCORNEJO
                    objUser.IDUsuario = cmbUsuario.SelectedValue
                    If Len(txtage.Text) > 0 Then
                        objUser.Agencia = txtage.Text.Trim
                    End If

                    If cmbAlianza.SelectedValue >= 0 Then
                        objUser.IDAlianza = cmbAlianza.SelectedValue
                    End If

                    If cmbGrupo.SelectedValue >= 0 Then
                        objUser.IDGrupo = cmbGrupo.SelectedValue
                    End If

                    If cmbDivision.SelectedValue >= 0 Then
                        objUser.IDDivision = cmbDivision.SelectedValue
                    End If

                    If cmbEstado.SelectedValue >= 0 Then
                        objUser.IDEntidad = cmbEstado.SelectedValue
                    End If

                    dtsRes = objUser.ManejaUsuario(11)
                    strErr = objUser.ErrorUsuario

                    If Trim(strErr) <> "" Then
                        Throw New Exception(strErr)
                    Else

                            Session("dtsConsultaAgencias") = dtsRes
                            grvConsulta.DataSource = dtsRes
                            grvConsulta.DataBind()
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
        CargaCombos(3)
        'grvConsulta.DataSource = CType(Session("dtsConsultaAgencias"), DataSet)
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "UsuarioId" Then

            Dim intConsul As Integer = 11
            Dim intBorra As Integer = 12
            Dim intInserta As Integer = 13
            Dim dtsRes As New DataSet
            Dim dtsRes_1 As New DataSet
            Dim objRow As GridViewRow = grvConsulta.Rows(Val(e.CommandArgument))
            Dim objImg As ImageButton = objRow.Cells(7).Controls(1)

            Dim idAgencia As String = grvConsulta.DataKeys(Convert.ToInt32(e.CommandArgument)).Values(0).ToString()
            Dim idAlianza As String = grvConsulta.DataKeys(Convert.ToInt32(e.CommandArgument)).Values(2).ToString()

            objUser.IDUsuario = cmbUsuario.SelectedValue
            objUser.IDAgencia = idAgencia
            objUser.IDAlianza = idAlianza
            objUser.UsuarioRegistro = objUser.UserNameAcceso
            dtsRes = objUser.ManejaUsuario(intConsul)

            If dtsRes.Tables(0).Rows.Count > 0 Then
                If dtsRes.Tables(0).Rows(0).Item("ASIG") = 0 Then
                    dtsRes_1 = objUser.ManejaUsuario(intInserta)
                    If dtsRes_1.Tables.Count > 0 Then
                        If dtsRes_1.Tables(0).Rows.Count > 0 Then
                            If dtsRes_1.Tables(0).Rows(0).Item("MENSAJE") = "TAREA EXITOSA" Then
                                objImg.ImageUrl = "../img/tick.png"
                                objRow.Cells(2).Text = 1
                                LimpiaAgeVendedor(idAgencia)
                            End If
                        End If
                    Else
                        MensajeError("La agencia debe ser de la misma alianza")
                    End If
                Else
                    objUser.ManejaUsuario(intBorra)
                    objImg.ImageUrl = "../img/cross.png"
                    objRow.Cells(2).Text = 0
                End If
            Else
                objUser.ManejaUsuario(intInserta)
                objImg.ImageUrl = "../img/tick.png"
                objRow.Cells(2).Text = 1
            End If
        End If
    End Sub

    Protected Sub grvConsulta_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvConsulta.RowDataBound
        Dim _objImg As New ImageButton

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim datakey As String = Convert.ToString(grvConsulta.DataKeys(e.Row.RowIndex).Values(1).ToString())
            _objImg = e.Row.FindControl("ImageButton1")
            If datakey = 1 Then
                _objImg.ImageUrl = "../img/tick.png"
            Else
                _objImg.ImageUrl = "../img/cross.png"
            End If
        End If
    End Sub

    Protected Sub bntBuscaAgen_Click(sender As Object, e As System.EventArgs) Handles bntBuscaAgen.Click
        CargaCombos(3)
    End Sub

    Protected Sub btnNinguna_Click(sender As Object, e As System.EventArgs) Handles btnNinguna.Click

        objUser.IDUsuario = cmbUsuario.SelectedValue
        objUser.Agencia = txtage.Text.Trim

        If cmbAlianza.SelectedValue >= 0 Then
            objUser.IDAlianza = cmbAlianza.SelectedValue
        End If
        If cmbDivision.SelectedValue >= 0 Then
            objUser.IDDivision = cmbDivision.SelectedValue
        End If
        If cmbGrupo.SelectedValue >= 0 Then
            objUser.IDGrupo = cmbGrupo.SelectedValue
        End If
        If cmbEstado.SelectedValue >= 0 Then
            objUser.IDEntidad = cmbEstado.SelectedValue
        End If
        objUser.ManejaUsuario(12)
        CargaCombos(3)
    End Sub

    Protected Sub btnTodas_Click(sender As Object, e As System.EventArgs) Handles btnTodas.Click
        objUser.IDUsuario = cmbUsuario.SelectedValue
        objUser.CargaUsuario(cmbUsuario.SelectedValue)
        objUser.Agencia = txtage.Text.Trim

        If cmbAlianza.SelectedValue >= 0 Then
            objUser.IDAlianza = cmbAlianza.SelectedValue
        End If
        If cmbDivision.SelectedValue >= 0 Then
            objUser.IDDivision = cmbDivision.SelectedValue
        End If
        If cmbGrupo.SelectedValue >= 0 Then
            objUser.IDGrupo = cmbGrupo.SelectedValue
        End If
        If cmbEstado.SelectedValue >= 0 Then
            objUser.IDEntidad = cmbEstado.SelectedValue
        End If

        If objUser.ErrorUsuario <> "" Then
            MensajeError(objUser.ErrorUsuario)
            Exit Sub
        Else
            If objUser.IDPerfil = 74 Then
                MensajeError("Por regla de negocio no esta permitido asignar más de una agencia al perfil VENDEDOR")
                Exit Sub
            End If
        End If

        objUser.ManejaUsuario(13)
        CargaCombos(3)
    End Sub

    Public Sub LimpiaAgeVendedor(ByVal AgeID As Integer)

        objUser.CargaUsuario(cmbUsuario.SelectedValue)

        If objUser.IDPerfil = 74 Then
            'Dim _objImg As New ImageButton
            'Dim rw As GridViewRow
            'Dim key As Integer

            'For Each rw In grvConsulta.Rows
            '    key = grvConsulta.DataKeys(rw.RowIndex).Value

            '    If key <> AgeID Then
            '        rw.Cells(2).Text = ""
            '        _objImg = rw.FindControl("ImageButton1")
            '        _objImg.ImageUrl = "../img/cross.png"
            '    End If
            'Next
        End If

    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        txtage.Text = ""
        cmbAlianza.SelectedValue = -1
        cmbGrupo.SelectedValue = -1
        cmbDivision.SelectedValue = -1
        cmbEstado.SelectedValue = -1
        CargaCombos(3)
    End Sub

    Protected Sub cmbAlianza_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbAlianza.SelectedIndexChanged
        CargaCombos(3)
    End Sub
    Protected Sub cmbGrupo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbGrupo.SelectedIndexChanged
        CargaCombos(3)
    End Sub
    Protected Sub cmbDivision_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbDivision.SelectedIndexChanged
        CargaCombos(3)
    End Sub
    Protected Sub cmbEstado_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        CargaCombos(3)
    End Sub
End Class
