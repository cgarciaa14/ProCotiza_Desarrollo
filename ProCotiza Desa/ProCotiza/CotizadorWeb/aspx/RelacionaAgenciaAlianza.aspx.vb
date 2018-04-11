'BBV-P-412:AVH:28/06/2016 RQ16: SE CREA CATALOGO ALIANZAS
'BUG-PC-06: AVH: 14/11/2016 se agrega boton Limpiar
'BUG-PC-17: AVH 25/11/2016 SE MODIFICA EL PAGINADOR
'BUG-PC-27 22/12/2016 MAUT No se deben asignar agencias a alianzas inactivas
'BUG-PC-34 12/01/2017 MAUT Se agregan validaciones para relacion de agencias
'BUG-PC-62:MPUESTO:13/05/2017:CORRECCION DE FUNCIONES DE LOS BOTONES NINGUNO Y TODOS

Imports System.Data
Imports SNProcotiza
Partial Class aspx_RelacionaAgenciaAlianza
    Inherits System.Web.UI.Page
    Dim strErr As String = String.Empty
    Dim objPaq As New clsPaquetes
    Dim objAgencias As New clsAgencias
    Dim ID_ALIANZA As Integer

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()
        ID_ALIANZA = Val(Request("idAlianza"))

        Dim dts As New DataSet
        Dim objAlianza As New SNProcotiza.clsAlianzas

        objAlianza.IDAlianza = Val(Request("idAlianza"))
        dts = objAlianza.ManejaAlianza(1)
        lblTitulo.Text = "RELACIONA AGENCIA - ALIANZA (" & (dts.Tables(0).Rows(0).Item("ALIANZA")).ToString & ")"

        'BUG-PC-27 22/12/2016 MAUT No se deben asignar agencias a alianzas inactivas
        If dts.Tables(0).Rows(0).Item("ESTATUS") = 3 Then
            lblMensaje.Text += "<script>alert('No se pueden asignar Agencias a Alianzas inactivas.'); window.close();</script>"
        End If


        If Not IsPostBack Then
            CargaCombos(1)
            CargaCombos(2)
            Limpiar()
        End If
    End Sub
    Private Sub CargaInfo()
        Try
            Dim dts As New DataSet
            Dim objAlianza As New SNProcotiza.clsAlianzas
            objAlianza.IDAlianza = Val(Request("idAlianza"))

            dts = objAlianza.ManejaAlianza(1)
            
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

        Dim objAlianzas As New clsAlianzas
        Dim objCombo As New clsProcGenerales
        Dim dtsRes As New DataSet

        Try
            CargaCombos = False

            Select Case intOpc
                Case 1

                    objAgencias.IDEstatus = 2
                    dtsRes = objAlianzas.ManejaAlianza(1)
                    strErr = objAlianzas.ErrorAlianza
                    If Trim$(strErr) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                objCombo.LlenaCombos(dtsRes, "ALIANZA", "ID_ALIANZA", cmbAlianza, strErr, True, , , -1)
                                cmbAlianza.SelectedValue = -1
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
                    'If cmbAgencia.SelectedValue <> 0 Then
                    '    objAlianzas.IDAgencia = cmbAgencia.SelectedValue

                    'End If

                    objAlianzas.IDEstatus = 2
                    objAlianzas.IDAlianza = Val(Request("idAlianza"))
                    'BUG-PC-34 12/01/2017 MAUT Se agrega filtro para la alianza
                    objAlianzas.IDAlianzaFiltro = cmbAlianza.SelectedValue


                    dtsRes = objAlianzas.ManejaAlianza(7)
                    strErr = objAlianzas.ErrorAlianza
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
        If CType(Session("dtsConsultaAG"), DataSet) Is Nothing Then
            CargaCombos(2)
        End If
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsultaAG"), DataSet)
        grvConsulta.DataBind()
    End Sub
    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "ID_ALIANZA" Then
            Dim objTO As New clsAlianzas
            Dim intConsul As Integer = 7
            Dim intBorra As Integer = 8
            Dim intInserta As Integer = 9

            Dim dtsRes As New DataSet
            Dim objRow As GridViewRow = grvConsulta.Rows(Val(e.CommandArgument))
            Dim objImg As ImageButton = objRow.Cells(3).Controls(1)

            Dim idAgencia As String = grvConsulta.DataKeys(Convert.ToInt32(e.CommandArgument)).Values(0).ToString()

            objTO.IDAlianza = ID_ALIANZA
            objTO.IDAgencia = Val(idAgencia)
            objTO.UsuarioRegistro = objTO.UserNameAcceso
            dtsRes = objTO.ManejaAlianza(intConsul)

            If dtsRes.Tables(0).Rows.Count > 0 Then
                If dtsRes.Tables(0).Rows(0).Item("ID_ALIANZA") = 0 Then
                    objTO.ManejaAlianza(intInserta)
                    objImg.ImageUrl = "../img/tick.png"
                Else
                    objTO.ManejaAlianza(intBorra)
                    objImg.ImageUrl = "../img/cross.png"
                End If
            Else
                objTO.ManejaAlianza(intInserta)
                objImg.ImageUrl = "../img/tick.png"
            End If
            'Se cambia el select fuera del cargacombo
            Me.cmbAlianza.SelectedValue = Val(Request("idAlianza"))
        End If
        CargaCombos(2)
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

    Protected Sub cmbAlianza_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbAlianza.SelectedIndexChanged
        Dim objAlianzas As New clsAlianzas
        Dim dtsRes As New DataSet

        If cmbAlianza.SelectedValue <> -1 Then
            objAlianzas.IDAlianzaFiltro = cmbAlianza.SelectedValue
        End If



        'objAlianzas.IDAgencia = cmbAgencia.SelectedValue
        objAlianzas.IDAlianza = Val(Request("idAlianza"))

        dtsRes = objAlianzas.ManejaAlianza(7)
        strErr = objAlianzas.ErrorAlianza

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

    Protected Sub bntBuscaProd_Click(sender As Object, e As System.EventArgs) Handles bntBuscaProd.Click
        Buscar()
    End Sub

    Protected Sub btnTodas_Click(sender As Object, e As System.EventArgs) Handles btnTodas.Click
        Dim objAlianzas As New clsAlianzas
        Dim dtsRes As New DataSet
        Dim intInserta As Integer = 9

        If cmbAlianza.SelectedValue <> -1 Then
            objAlianzas.IDAlianzaFiltro = cmbAlianza.SelectedValue
        End If

        If txtNom.Text <> "" Then
            objAlianzas.Agencia = txtNom.Text
        End If

        objAlianzas.IDAlianza = Val(Request("idAlianza"))
        objAlianzas.ManejaAlianza(intInserta)

        CargaCombos(2)
        Buscar()
    End Sub

    Protected Sub btnNinguna_Click(sender As Object, e As System.EventArgs) Handles btnNinguna.Click
        Dim objAlianzas As New clsAlianzas
        Dim dtsRes As New DataSet
        Dim intBorra As Integer = 8

        If cmbAlianza.SelectedValue <> 0 Then
            objAlianzas.IDAlianzaFiltro = cmbAlianza.SelectedValue
        End If

        If txtNom.Text <> "" Then
            objAlianzas.Agencia = txtNom.Text
        End If

        objAlianzas.IDAlianza = Val(Request("idAlianza"))
        objAlianzas.ManejaAlianza(intBorra)

        CargaCombos(2)
        Buscar()
    End Sub

    Protected Sub btnLimpias_Click(sender As Object, e As System.EventArgs) Handles btnLimpias.Click
        Limpiar()

    End Sub
    Public Sub Buscar()
        Dim objAlianzas As New clsAlianzas
        Dim dtsRes As New DataSet

        If cmbAlianza.SelectedValue <> -1 Then
            objAlianzas.IDAlianzaFiltro = cmbAlianza.SelectedValue
        End If

        objAlianzas.IDAlianza = Val(Request("idAlianza"))
        objAlianzas.Agencia = txtNom.Text
        dtsRes = objAlianzas.ManejaAlianza(7)
        strErr = objAlianzas.ErrorAlianza

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
    Public Sub Limpiar()
        Me.cmbAlianza.SelectedValue = -1
        Me.txtNom.Text = ""
        Buscar()
    End Sub
End Class
