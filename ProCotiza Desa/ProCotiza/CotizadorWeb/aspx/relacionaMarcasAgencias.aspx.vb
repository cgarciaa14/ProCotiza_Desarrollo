'BUG-PC-09:AMR:16/11/2016 CUTARMA01:No pertime espacios en blanco al ingresar el nombre de la marca
'BUG-PC-102:JBEJAR:17/08/2017 Correciones en el filtro de los botones de todos y ninguna.  

Imports System.Data
Imports SNProcotiza

Partial Class aspx_relacionaMarcasAgencias
    Inherits System.Web.UI.Page
    Dim strErr As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LimpiaError()

        If Not IsPostBack Then

            If Not CargaCombos(1) Then
                MensajeError(strErr)
                Exit Sub
            End If

            If Not CargaCombos(2) Then
                MensajeError(strErr)
                Exit Sub
            End If
        End If
    End Sub

    Public Sub LimpiaError()
        lblMensaje.Text = String.Empty
    End Sub

    Public Sub MensajeError(ByVal strMsj As String)
        strMsj = Replace(strMsj, "'", "")
        strMsj = Replace(strMsj, """", "")
        strMsj = Replace(strMsj, "(", "")
        strMsj = Replace(strMsj, ")", "")
        lblMensaje.Text = "<script>alert('" & strMsj & "')</script>"
    End Sub

    Private Function CargaCombos(ByVal intOpc As Integer) As Boolean
        Dim objMarcas As New clsMarcas
        Dim objAgencias As New clsAgencias
        Dim objCombo As New clsProcGenerales
        Dim dtsRes As New DataSet

        Try
            CargaCombos = False

            Select Case intOpc
                Case 1
                    Session("dtsConsultaMarcas") = Nothing
                    grvConsulta.PageIndex = 0
                    grvConsulta.DataSource = Nothing

                    objMarcas.IDEstatus = 2
                    dtsRes = objMarcas.ManejaMarca(1)
                    strErr = objMarcas.ErrorMarcas
                    If Trim$(strErr) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_MARCA", cmbMarca, strErr, False)
                                If Trim$(strErr) <> "" Then 'BUG-PC-09
                                    Exit Function
                                End If
                            Else
                                strErr = "No se encontro información."
                                Exit Function
                            End If
                        Else
                            strErr = "No se encontro información."
                            Exit Function
                        End If
                        cmbMarca.SelectedValue = (Val(Request("idMarca")))
                    Else
                        'BUG-PC-09
                        Exit Function
                    End If
                Case 2
                    objMarcas.IDMarca = cmbMarca.SelectedValue
                    If Len(txtNom.Text) > 0 Then
                        objMarcas.Nombre = txtNom.Text.Trim
                    End If

                    dtsRes = objMarcas.ManejaMarca(7)
                    strErr = objMarcas.ErrorMarcas

                    If Trim(strErr) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            Session("dtsConsultaMarcas") = dtsRes
                            grvConsulta.DataSource = dtsRes
                            End If
                        End If
                            grvConsulta.DataBind()
                    Else
                        Exit Function
                    End If
            End Select

            CargaCombos = True
        Catch ex As Exception
            CargaCombos = False
            MensajeError(ex.Message)
        End Try
    End Function

    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        If Not CargaCombos(2) Then
            MensajeError(strErr)
            Exit Sub
        End If
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsultaMarcas"), DataSet)
        grvConsulta.DataBind()
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "MarcaId" Then
            Dim objTO As New clsMarcas
            Dim intConsul As Integer = 7
            Dim intBorra As Integer = 8
            Dim intInserta As Integer = 9
            Dim dtsRes As New DataSet
            Dim objRow As GridViewRow = grvConsulta.Rows(Val(e.CommandArgument))
            Dim objImg As ImageButton = objRow.Cells(2).Controls(1)

            Dim idMarca As String = grvConsulta.DataKeys(Convert.ToInt32(e.CommandArgument)).Values(1).ToString()

            objTO.IDMarca = Val(cmbMarca.SelectedValue)
            objTO.IDAgencia = Val(idMarca)
            objTO.UsuarioRegistro = objTO.UserNameAcceso
            dtsRes = objTO.ManejaMarca(intConsul)
            If dtsRes.Tables(0).Rows.Count > 0 Then
                If dtsRes.Tables(0).Rows(0).Item("RELACION") = 0 Then
                    objTO.ManejaMarca(intInserta)
                    objImg.ImageUrl = "../img/tick.png"
                    objRow.Cells(5).Text = 1
                Else
                    objTO.ManejaMarca(intBorra)
                    objImg.ImageUrl = "../img/cross.png"
                    objRow.Cells(5).Text = 0
                End If
            Else
                objTO.ManejaMarca(intInserta)
                objImg.ImageUrl = "../img/tick.png"
                objRow.Cells(5).Text = 1
            End If
        End If
    End Sub

    Protected Sub grvConsulta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvConsulta.RowDataBound
        Dim _objImg As New ImageButton

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim datakey As String = Convert.ToString(grvConsulta.DataKeys(e.Row.RowIndex).Values(2).ToString)

            _objImg = e.Row.FindControl("ImageButton1")
            If datakey = 1 Then
                _objImg.ImageUrl = "../img/tick.png"
            Else
                _objImg.ImageUrl = "../img/cross.png"
            End If
        End If
    End Sub

    Protected Sub cmbMarca_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbMarca.SelectedIndexChanged
        If Not CargaCombos(2) Then
            MensajeError(strErr)
            Exit Sub
        End If
    End Sub

    Protected Sub bntBuscaAgen_Click(sender As Object, e As System.EventArgs) Handles bntBuscaAgen.Click
        If Not CargaCombos(2) Then
            MensajeError(strErr)
            Exit Sub
        End If
    End Sub

    Protected Sub btnNinguna_Click(sender As Object, e As System.EventArgs) Handles btnNinguna.Click
        Dim objTO As New clsMarcas
        objTO.IDMarca = cmbMarca.SelectedValue
        objTO.Nombre = txtNom.Text
        objTO.ManejaMarca(8)
        If Not CargaCombos(2) Then
            MensajeError(strErr)
            Exit Sub
        End If
    End Sub

    Protected Sub btnTodas_Click(sender As Object, e As System.EventArgs) Handles btnTodas.Click
        Dim objTO As New clsMarcas
        objTO.IDMarca = cmbMarca.SelectedValue
        objTO.Nombre = txtNom.Text
        objTO.ManejaMarca(9)
        If Not CargaCombos(2) Then
            MensajeError(strErr)
            Exit Sub
        End If
    End Sub
End Class
