Imports System.Data
Imports SNProcotiza

Partial Class aspx_relacionaAseguradorasBroker
    Inherits System.Web.UI.Page

    Dim strErr As String = String.Empty
    Dim objAseg As New clsAseguradoras
    Dim objBro As New SNProcotiza.clsBrokerSeguros

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LimpiaError()

        If Not IsPostBack Then
            CargaCombos(1)
            CargaCombos(2)
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
        Dim dtsRes As New DataSet

        Try
            CargaCombos = False

            Select Case intOpc
                Case 1
                    Session("dtsConsultaAseguradoras") = Nothing
                    grvConsulta.PageIndex = 0
                    grvConsulta.DataSource = Nothing

                    objAseg.IDEstatus = 2
                    dtsRes = objAseg.ManejaAseguradora(1)
                    strErr = objAseg.ErrorAseguradora
                    If Trim$(strErr) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                objCombo.LlenaCombos(dtsRes, "RAZON_SOCIAL", "ID_ASEGURADORA", ddlAseguradora, strErr, False)
                                If Trim$(strErr) <> "" Then
                                    MensajeError(strErr)
                                    Exit Function
                                End If
                            End If
                        End If
                        ddlAseguradora.SelectedValue = Val(Request("IDAseg"))
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If
                Case 2
                    objAseg.IDAseguradora = ddlAseguradora.SelectedValue
                    If txtBroker.Text.Length > 0 Then
                        objAseg.NomBroker = txtBroker.Text.Trim
                    End If

                    dtsRes = objAseg.ManejaAseguradora(5)
                    strErr = objAseg.ErrorAseguradora
                    If strErr = "" Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            Session("dtsConsultaAseguradoras") = dtsRes
                            grvConsulta.DataSource = dtsRes
                            grvConsulta.DataBind()
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
        CargaCombos(2)
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = Session("dtsConsultaAseguradoras")
        grvConsulta.DataBind()
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "AsegId" Then
            Dim intConsul As Integer = 5
            Dim intInserta As Integer = 6
            Dim intBorra As Integer = 7
            Dim dtsRes As New DataSet
            Dim objRow As GridViewRow = grvConsulta.Rows(Val(e.CommandArgument))
            Dim objImg As ImageButton = objRow.Cells(1).Controls(1)

            Dim IdBroker As String = grvConsulta.DataKeys(Convert.ToInt32(e.CommandArgument)).Values(0).ToString()

            objAseg.IDAseguradora = ddlAseguradora.SelectedValue
            objAseg.IDBroker = IdBroker
            objAseg.UsuarioRegistro = objAseg.UserNameAcceso

            dtsRes = objAseg.ManejaAseguradora(intConsul)
            If dtsRes.Tables(0).Rows.Count > 0 Then
                If dtsRes.Tables(0).Rows(0).Item("ASIG") = 0 Then
                    objAseg.ManejaAseguradora(intInserta)
                    objImg.ImageUrl = "../img/tick.png"
                    objRow.Cells(3).Text = 1
                Else
                    objAseg.ManejaAseguradora(intBorra)
                    objImg.ImageUrl = "../img/cross.png"
                    objRow.Cells(3).Text = 0
                End If
            Else
                objAseg.ManejaAseguradora(intInserta)
                objImg.ImageUrl = "../img/tick.png"
                objRow.Cells(3).Text = 1
            End If

        End If
    End Sub

    Protected Sub grvConsulta_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvConsulta.RowDataBound
        Dim _objImg As New ImageButton

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim datakey As String = Convert.ToString(grvConsulta.DataKeys(e.Row.RowIndex).Values(1).ToString)
            _objImg = e.Row.FindControl("ImageButton1")

            If datakey = 1 Then
                _objImg.ImageUrl = "../img/tick.png"
            Else
                _objImg.ImageUrl = "../img/cross.png"
            End If
        End If
    End Sub

    Protected Sub ddlAseguradora_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAseguradora.SelectedIndexChanged
        CargaCombos(2)
    End Sub

    Protected Sub bntBuscaBroker_Click(sender As Object, e As System.EventArgs) Handles bntBuscaBroker.Click
        CargaCombos(2)
    End Sub

    Protected Sub btnNinguna_Click(sender As Object, e As System.EventArgs) Handles btnNinguna.Click
        objAseg.IDAseguradora = ddlAseguradora.SelectedValue
        objAseg.ManejaAseguradora(7)
        CargaCombos(2)
    End Sub

    Protected Sub btnTodas_Click(sender As Object, e As System.EventArgs) Handles btnTodas.Click
        objAseg.IDAseguradora = ddlAseguradora.SelectedValue
        objAseg.ManejaAseguradora(6)
        CargaCombos(2)
    End Sub
End Class
