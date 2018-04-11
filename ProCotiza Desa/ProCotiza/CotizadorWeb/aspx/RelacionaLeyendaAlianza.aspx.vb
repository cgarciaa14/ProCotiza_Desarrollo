'BBV-P-412_RQCOT-05:AVH:31/08/2016: SE CREA PARA ASIGNAR LEYENDAS
'BUG-PC-06: AVH: 14/11/2016 Se agrega boton Limpiar
'BUG-PC-17: AVH 25/11/2016 SE corrige paginador
'BUG-PC-39 27/01/17 JRHM Se agrego boton limpiar
'BUG-PC-48 JRHM 21/02/17 SE CORRIGE ACTUALIZACION DE CONSULTA AL CAMBIAR VALOR DE COMBOBOX

Imports System.Data
Imports SNProcotiza
Partial Class aspx_RelacionaLeyendaAlianza
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
        lblTitulo.Text = "RELACIONA LEYENDA - ALIANZA (" & (dts.Tables(0).Rows(0).Item("ALIANZA")).ToString & ")"


        If Not IsPostBack Then
            CargaCombos(1)
            CargaCombos(2)
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
        Dim objLeyenda As New clsLeyendasRep
        Dim objCombo As New clsProcGenerales
        Dim dtsRes As New DataSet

        Try
            CargaCombos = False

            Select Case intOpc
                Case 1

                    objLeyenda.IDEstatus = 2
                    dtsRes = objLeyenda.ManejaLeyenda(1)
                    strErr = objLeyenda.ErrorLeyenda
                    If Trim$(strErr) = "" Then
                         Dim objParam As New clsParametros
                        Dim dtsPar As DataSet

                        objParam.IDPadre = 183
                        dtsPar = objParam.ManejaParametro(1)

                        objCombo.LlenaCombos(dtsPar, "TEXTO", "ID_PARAMETRO", cmbSeccion, strErr, True)
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If

                Case 2
                    'If cmbAgencia.SelectedValue <> 0 Then
                    '    objAlianzas.IDAgencia = cmbAgencia.SelectedValue

                    'End If


                    objLeyenda.Alianza = Val(Request("idAlianza"))
                    If cmbSeccion.SelectedValue <> 0 Then
                        objLeyenda.IDSeccion = cmbSeccion.SelectedValue
                    End If


                    dtsRes = objLeyenda.ManejaLeyenda(8)
                    strErr = objLeyenda.ErrorLeyenda
                    If Trim$(strErr) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                Session("dtsConsultaAG") = dtsRes
                                grvConsulta.DataSource = dtsRes
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
            Dim objTO As New clsLeyendasRep
            Dim intConsul As Integer = 9
            Dim intBorra As Integer = 10
            Dim intInserta As Integer = 11

            Dim dtsRes As New DataSet
            Dim objRow As GridViewRow = grvConsulta.Rows(Val(e.CommandArgument))
            Dim objImg As ImageButton = objRow.Cells(5).Controls(1)

            Dim idLeyenda As String = grvConsulta.DataKeys(Convert.ToInt32(e.CommandArgument)).Values(0).ToString()

            objTO.CargaSession(Val(Session("cveAcceso")))

            objTO.Alianza = ID_ALIANZA
            objTO.IDLeyenda = Val(idLeyenda)
            objTO.UsuarioRegistro = objTO.UserNameAcceso
            dtsRes = objTO.ManejaLeyenda(intConsul)

            If dtsRes.Tables(0).Rows.Count > 0 Then
                If dtsRes.Tables(0).Rows(0).Item("ID_ALIANZA") = 0 Then
                    objTO.ManejaLeyenda(intInserta)
                    objImg.ImageUrl = "../img/tick.png"
                Else
                    If dtsRes.Tables(0).Rows(0).Item("ESTATUS") = 2 Then
                        objTO.IDEstatus = 3
                    Else
                        objTO.IDEstatus = 2
                    End If
                    objTO.ManejaLeyenda(intBorra)
                    objImg.ImageUrl = "../img/cross.png"
                End If
            Else
                objTO.ManejaLeyenda(intInserta)
                objImg.ImageUrl = "../img/tick.png"
            End If

        End If

        CargaCombos(2)
    End Sub
    Protected Sub grvConsulta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvConsulta.RowDataBound
        Dim _objImg As New ImageButton

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim datakey As String = Convert.ToString(grvConsulta.DataKeys(e.Row.RowIndex).Values(2).ToString)

            _objImg = e.Row.FindControl("ImageButton1")
            If datakey = 2 Then
                _objImg.ImageUrl = "../img/tick.png"
            Else : datakey = 0 Or datakey = 3
                _objImg.ImageUrl = "../img/cross.png"
            End If
        End If
    End Sub

    Protected Sub cmbSeccion_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbSeccion.SelectedIndexChanged
        Dim objLeyenda As New clsLeyendasRep
        Dim dtsRes As New DataSet

        If cmbSeccion.SelectedValue <> 0 Then
            objLeyenda.IDSeccion = cmbSeccion.SelectedValue
        End If
        If cmbSeccion.SelectedValue <> 0 Then
            objLeyenda.IDSeccion = cmbSeccion.SelectedValue
        End If

        objLeyenda.Alianza = Val(Request("idAlianza"))
            objLeyenda.Nombre = txtNom.Text

        dtsRes = objLeyenda.ManejaLeyenda(8)
        strErr = objLeyenda.ErrorLeyenda

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
        Dim objLeyenda As New clsLeyendasRep
        Dim dtsRes As New DataSet
        Dim intInserta As Integer = 10

        objLeyenda.CargaSession(Val(Session("cveAcceso")))
        objLeyenda.UsuarioRegistro = objLeyenda.UserNameAcceso

        If cmbSeccion.SelectedValue <> 0 Then
            objLeyenda.IDSeccion = cmbSeccion.SelectedValue
        End If

        If txtNom.Text <> "" Then
            objLeyenda.Nombre = txtNom.Text
        End If

        objLeyenda.Alianza = Val(Request("idAlianza"))
        objLeyenda.IDEstatus = 2
        objLeyenda.ManejaLeyenda(intInserta)

        Buscar()

    End Sub

    Protected Sub btnNinguna_Click(sender As Object, e As System.EventArgs) Handles btnNinguna.Click
        Dim objLeyenda As New clsLeyendasRep
        Dim dtsRes As New DataSet
        Dim intBorra As Integer = 10

        objLeyenda.CargaSession(Val(Session("cveAcceso")))
        objLeyenda.UsuarioRegistro = objLeyenda.UserNameAcceso

        If cmbSeccion.SelectedValue <> 0 Then
            objLeyenda.IDSeccion = cmbSeccion.SelectedValue
        End If

        If txtNom.Text <> "" Then
            objLeyenda.Nombre = txtNom.Text
        End If

        objLeyenda.Alianza = Val(Request("idAlianza"))
        objLeyenda.IDEstatus = 3
        objLeyenda.ManejaLeyenda(intBorra)

        Buscar()
    End Sub
    Public Sub LIMPIA_CAMPOS()

        Me.txtNom.Text = ""
        Me.cmbSeccion.SelectedValue = 0
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As System.EventArgs) Handles btnLimpiar.Click
        LIMPIA_CAMPOS()
        Buscar()
    End Sub
    Public Sub Buscar()
        Dim objLeyenda As New clsLeyendasRep
        Dim dtsRes As New DataSet

        If cmbSeccion.SelectedValue <> 0 Then
            objLeyenda.IDSeccion = cmbSeccion.SelectedValue
        End If

        objLeyenda.Alianza = Val(Request("idAlianza"))
        objLeyenda.Nombre = txtNom.Text
        dtsRes = objLeyenda.ManejaLeyenda(8)
        strErr = objLeyenda.ErrorSession

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
End Class
