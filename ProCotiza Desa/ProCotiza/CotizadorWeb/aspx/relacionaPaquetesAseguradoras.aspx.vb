'BBVA-P-412: 06-07-2016: AMR: RQ02,RQ03,RQ04,RQ05,RQ06,RQ08 SE AGREGA FUNCIONALIDAD PARA LA RELACIÓN PAQUETES ASEGURADORAS

Imports System.Data
Imports SNProcotiza

Partial Class aspx_relacionaPaquetesAseguradoras
    Inherits System.Web.UI.Page

    Dim strErr As String = String.Empty
    Dim objBro As New clsBrokerSeguros
    Dim objPaq As New clsPaquetes

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
                    Session("dtsConsultaAseguradorasBrok") = Nothing
                    grvConsulta.PageIndex = 0
                    grvConsulta.DataSource = Nothing

                    objBro.IDEstatus = 2
                    dtsRes = objBro.ManejaBroker(1)
                    strErr = objBro.ErrorBroker
                    If Trim$(strErr) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                objCombo.LlenaCombos(dtsRes, "RAZON_SOCIAL", "ID_BROKER", ddlbroker, strErr, False)
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
                    objPaq.IDPaquete = Val(Request("idPaq"))

                    If ddlbroker.SelectedValue > 0 Then
                        objPaq.IDBroker = ddlbroker.SelectedValue
                    End If

                    If txtAseg.Text.Trim.Length > 0 Then
                        objPaq.NomAseguradora = txtAseg.Text.Trim
                    End If

                    dtsRes = objPaq.ManejaPaquete(47)
                    strErr = objPaq.ErrorPaquete
                    If strErr = "" Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            Session("dtsConsultaAseguradorasBrok") = dtsRes
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

    Protected Sub ddlbroker_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlbroker.SelectedIndexChanged
        CargaCombos(2)
    End Sub

    Protected Sub bntBuscaAseg_Click(sender As Object, e As System.EventArgs) Handles bntBuscaAseg.Click
        CargaCombos(2)
    End Sub

    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        CargaCombos(2)
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = Session("dtsConsultaAseguradorasBrok")
        grvConsulta.DataBind()
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "AsegID" Then
            Dim intConsul As Integer = 47
            Dim intInserta As Integer = 48
            Dim intBorra As Integer = 49
            Dim dtsRes As New DataSet
            Dim objRow As GridViewRow = grvConsulta.Rows(Val(e.CommandArgument))
            Dim objImg As ImageButton = objRow.Cells(2).Controls(1)

            Dim IdAseg As String = grvConsulta.DataKeys(Convert.ToInt32(e.CommandArgument)).Values(0).ToString()

            objPaq.IDPaquete = Val(Request("idPaq"))
            objPaq.IDBroker = ddlbroker.SelectedValue
            objPaq.IDAseguradora = IdAseg
            objPaq.UsuarioRegistro = objPaq.UserNameAcceso

            dtsRes = objPaq.ManejaPaquete(intConsul)
            If dtsRes.Tables(0).Rows.Count > 0 Then
                If dtsRes.Tables(0).Rows(0).Item("ASIG") = 0 Then
                    objPaq.ManejaPaquete(intInserta)
                    objImg.ImageUrl = "../img/tick.png"
                    objRow.Cells(3).Text = 1
                Else
                    objPaq.ManejaPaquete(intBorra)
                    objImg.ImageUrl = "../img/cross.png"
                    objRow.Cells(3).Text = 0
                End If
            Else
                objPaq.ManejaPaquete(intInserta)
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

    Protected Sub btnNinguna_Click(sender As Object, e As System.EventArgs) Handles btnNinguna.Click
        objPaq.IDPaquete = Val(Request("idPaq"))
        objPaq.IDBroker = ddlbroker.SelectedValue
        objPaq.ManejaPaquete(49)
        CargaCombos(2)

    End Sub

    Protected Sub btnTodas_Click(sender As Object, e As System.EventArgs) Handles btnTodas.Click
        objPaq.IDPaquete = Val(Request("idPaq"))
        objPaq.IDBroker = ddlbroker.SelectedValue
        objPaq.ManejaPaquete(48)
        CargaCombos(2)
    End Sub
End Class
