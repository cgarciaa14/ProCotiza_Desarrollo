'BUG-PC-38 23/01/2017 MAUT No se deben asignar aseguradoras a brokers inactivos

Imports System.Data
Imports SNProcotiza

Partial Class aspx_relacionaBrokerAseguradora
    Inherits System.Web.UI.Page
    Dim objBroker As New clsBrokerSeguros
    Dim objAseg As New clsAseguradoras
    Dim strErr As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LimpiaError()
        Dim dts As New DataSet

        objBroker.IDBroker = Val(Request("idBroker"))
        dts = objBroker.ManejaBroker(1)

        'BUG-PC-38 23/01/2017 MAUT No se deben asignar aseguradoras a brokers inactivos
        If dts.Tables.Count > 0 Then
            If dts.Tables(0).Rows.Count > 0 Then
                If dts.Tables(0).Rows(0).Item("ESTATUS") = 3 Then
                    lblMensaje.Text += "<script>alert('No se pueden asignar aseguradoras a brokers inactivos.'); window.close();</script>"
                    Exit Sub
                End If
            End If
        End If

        If Not IsPostBack Then
            Script = "document.getElementById('cuerpoConsul').style.width='70%';"
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

    Public WriteOnly Property Script() As String
        Set(ByVal value As String)
            lblScript.Text = "<script>" & value & " </script>"
        End Set
    End Property

    Private Function CargaCombos(ByVal intOpc As Integer) As Boolean
        Dim objCombo As New clsProcGenerales


        Dim dtsRes As New DataSet
        Try
            CargaCombos = False

            Select Case intOpc
                Case 1
                    'combo Empresas
                    objBroker.IDEstatus = 2
                    dtsRes = objBroker.ManejaBroker(1)
                    strErr = objBroker.ErrorBroker

                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "RAZON_SOCIAL", "ID_BROKER", ddlbroker, strErr)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If
                    ddlbroker.SelectedValue = Val(Request("idBroker"))
                Case 2

                    If txtNom.Text.Length > 0 Then
                        objBroker.NomAseg = txtNom.Text.Trim()
                    End If

                    objBroker.IDBroker = ddlbroker.SelectedValue
                    objBroker.IDEstatus = 2
                    dtsRes = objBroker.ManejaBroker(5)
                    strErr = objBroker.ErrorBroker
                    If Trim$(strErr) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                Session("dtsConsultaASEG") = dtsRes
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
        CargaCombos(2)
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsultaASEG"), DataSet)
        grvConsulta.DataBind()
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand

        If e.CommandName = "AsegId" Then


            Dim intConsul As Integer = 5
            Dim intInserta As Integer = 6
            Dim intBorra As Integer = 7
            Dim dtsRes As New DataSet
            Dim objRow As GridViewRow = grvConsulta.Rows(Val(e.CommandArgument))
            Dim objImg As ImageButton = objRow.Cells(2).Controls(1)

            Dim idAseg As String = grvConsulta.DataKeys(Convert.ToInt32(e.CommandArgument)).Values(0).ToString()

            objBroker.ASeguradora = idAseg
            objBroker.IDBroker = ddlbroker.SelectedValue

            objAseg.IDAseguradora = idAseg
            objAseg.IDBroker = ddlbroker.SelectedValue

            dtsRes = objBroker.ManejaBroker(intConsul)
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
        Dim _objImg As ImageButton

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

    Protected Sub ddlbroker_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlbroker.SelectedIndexChanged
        CargaCombos(2)
    End Sub

    Protected Sub bntBuscaAseg_Click(sender As Object, e As System.EventArgs) Handles bntBuscaAseg.Click
        CargaCombos(2)
    End Sub

    Protected Sub btnTodas_Click(sender As Object, e As System.EventArgs) Handles btnTodas.Click
        objAseg.IDBroker = ddlbroker.SelectedValue
        objAseg.ManejaAseguradora(6)
        CargaCombos(2)
    End Sub

    Protected Sub btnNinguna_Click(sender As Object, e As System.EventArgs) Handles btnNinguna.Click
        objAseg.IDBroker = ddlbroker.SelectedValue
        objAseg.ManejaAseguradora(7)
        CargaCombos(2)
    End Sub
End Class
