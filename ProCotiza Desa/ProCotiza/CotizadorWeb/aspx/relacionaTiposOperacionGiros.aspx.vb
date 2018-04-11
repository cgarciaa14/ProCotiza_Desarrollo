Imports System.Data
Imports SNProcotiza

Partial Class aspx_relacionaTiposOperacionGiros
    Inherits System.Web.UI.Page
    Dim strErr As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LimpiaError()

        If Not IsPostBack Then
            Script = "document.getElementById('cuerpoConsul').style.width='70%';"

            Dim objNom As New SNProcotiza.clsGiros
            objNom.CargaGiro(Val(Request("idRel")))
            CargaCombos(1)
            Try
                ''ocultamos el boton de agregar y el de limpiar y e cambiamos el nombre al boton de buscar
                'Dim objBot As Button = MyMaster.FindControl("btnAgregar")
                'objBot.Visible = False
                'objBot = MyMaster.FindControl("btnLimpiar")
                'objBot.Text = "Ninguno"
                'objBot = MyMaster.FindControl("btnBuscar")
                'objBot.Text = "Todos"
            Catch ex As Exception

            End Try
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
                    Session("dtsConsultaGiro") = Nothing
                    grvConsulta.PageIndex = 0
                    grvConsulta.DataSource = Nothing

                    'Dim objBotTodo As Button = MyMaster.FindControl("btnAgregar")
                    'Dim objBotNada As Button = MyMaster.FindControl("btnLimpiar")
                    'objBotTodo.Enabled = False
                    'objBotNada.Enabled = False

                    'grid giros
                    Dim objGiro As New SNProcotiza.clsGiros
                    If Trim(txtGiro.Text) <> "" Then
                        objGiro.Nombre = Trim(txtGiro.Text)
                    End If

                    dtsRes = objGiro.ManejaGiro(1)
                    strErr = objGiro.ErrorGiros
                    If Trim$(strErr) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                Session("dtsConsultaGiro") = dtsRes
                                grvConsulta.DataSource = dtsRes
                                'objBotTodo.Enabled = True
                                'objBotNada.Enabled = True
                            End If
                        End If
                    Else
                        MensajeError(strErr)
                    End If
                    grvConsulta.DataBind()
            End Select

            CargaCombos = True
        Catch ex As Exception
            CargaCombos = False
            MensajeError(ex.Message)
        End Try
    End Function

    Protected Sub btnTodas_Click(sender As Object, e As System.EventArgs) Handles btnTodas.Click
        Try
            Dim objTO As New SNProcotiza.clsTiposOperacion
            Dim intConsul As Integer = 7
            Dim intBorra As Integer = 5
            Dim intInserta As Integer = 6

            Dim objRow As DataRow
            Dim dtsConsul As DataSet = CType(Session("dtsConsultaGiro"), DataSet)

            For Each objRow In dtsConsul.Tables(0).Rows
                objTO.IDTipoOperacion = Val(Request("idRel"))
                objTO.IDGiro = objRow.Item("id_giro")
                objTO.ManejaTipoOperacion(intConsul)

                If objTO.ErrorTipoOperacion = "" Then
                    'primero se borra y despues se inserta
                    objTO.ManejaTipoOperacion(intBorra)
                    objTO.ManejaTipoOperacion(intInserta)
                Else
                    MensajeError(objTO.ErrorTipoOperacion)
                End If
            Next
            CargaCombos(1)
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Protected Sub btnNinguna_Click(sender As Object, e As System.EventArgs) Handles btnNinguna.Click
        Try
            Dim objTO As New SNProcotiza.clsTiposOperacion
            Dim intConsul As Integer = 7
            Dim intBorra As Integer = 5
            Dim intInserta As Integer = 6

            Dim objRow As DataRow
            Dim dtsConsul As DataSet = CType(Session("dtsConsultaGiro"), DataSet)

            For Each objRow In dtsConsul.Tables(0).Rows
                objTO.IDTipoOperacion = Val(Request("idRel"))
                objTO.IDGiro = objRow.Item("id_giro")
                objTO.ManejaTipoOperacion(intConsul)

                If objTO.ErrorTipoOperacion = "" Then
                    'se borra
                    objTO.ManejaTipoOperacion(intBorra)
                Else
                    MensajeError(objTO.ErrorTipoOperacion)
                End If
            Next
            CargaCombos(1)
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsultaGiro"), DataSet)
        grvConsulta.DataBind()
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "giroId" Then
            Dim objTO As New SNProcotiza.clsTiposOperacion
            Dim intConsul As Integer = 7
            Dim intBorra As Integer = 5
            Dim intInserta As Integer = 6
            Dim dtsRes As New DataSet
            Dim objRow As GridViewRow = grvConsulta.Rows(Val(e.CommandArgument))
            Dim objImg As ImageButton = objRow.Cells(2).Controls(1)

            objTO.IDTipoOperacion = Val(Request("idRel"))
            objTO.IDGiro = Val(objRow.Cells(0).Text)
            dtsRes = objTO.ManejaTipoOperacion(intConsul)

            If objTO.ErrorTipoOperacion = "" Then
                intConsul = 0
                If dtsRes.Tables.Count > 0 Then
                    If dtsRes.Tables(0).Rows.Count > 0 Then
                        intConsul = 1
                    End If
                End If

                objImg.ImageUrl = "../img/cross.png"
                objTO.ManejaTipoOperacion(intBorra)
                If intConsul = 0 Then
                    objTO.ManejaTipoOperacion(intInserta)
                    objImg.ImageUrl = "../img/tick.png"
                End If
            Else
                MensajeError(objTO.ErrorTipoOperacion)
            End If
        End If
    End Sub

    Protected Sub grvConsulta_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvConsulta.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim objImg As ImageButton = e.Row.Cells(2).Controls(1)
            Dim objTO As New SNProcotiza.clsTiposOperacion
            Dim intOpc As Integer = 0
            Dim dtsRes As New DataSet

            objImg.CommandArgument = e.Row.RowIndex
            objTO.IDGiro = Val(e.Row.Cells(0).Text)
            objTO.IDTipoOperacion = Val(Request("idRel"))
            dtsRes = objTO.ManejaTipoOperacion(7)

            If objTO.ErrorTipoOperacion = "" Then
                objImg.ImageUrl = "../img/cross.png"
                If dtsRes.Tables.Count > 0 Then
                    If dtsRes.Tables(0).Rows.Count > 0 Then
                        objImg.ImageUrl = "../img/tick.png"
                    End If
                End If
            Else
                MensajeError(objTO.ErrorTipoOperacion)
            End If
        End If
    End Sub

    Protected Sub bntBuscaGiro_Click(sender As Object, e As System.EventArgs) Handles bntBuscaGiro.Click
        CargaCombos(1)
    End Sub
End Class
