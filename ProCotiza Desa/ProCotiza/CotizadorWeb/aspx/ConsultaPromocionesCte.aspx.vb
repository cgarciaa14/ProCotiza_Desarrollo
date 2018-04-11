''BUG-PC-25 MAUT 15/12/2016 Se limpia el grid

Imports System.Data
Imports SNProcotiza

Partial Class aspx_ConsultaPromocionesCte
    Inherits System.Web.UI.Page
    Dim strErr As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LimpiaError()

        If Not IsPostBack Then
            CargaCombos(1)
            LimpiaFiltros()
            BuscaDatos()
        End If
    End Sub

    Public Sub LimpiaError()
        lblMensaje.Text = ""
    End Sub

    Private Sub LimpiaFiltros()
        ddlEstatus.SelectedValue = 0
        txtPromo.Text = ""
        ''BUG-PC-25 MAUT 15/12/2016 Se limpia el grid
        grvResult.DataSource = Nothing
        grvResult.DataBind()
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
                    'combo de estatus
                    dtsRes = objCombo.ObtenInfoParametros(1, strErr)
                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", ddlEstatus, strErr, True)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If
            End Select

            CargaCombos = True
        Catch ex As Exception
            CargaCombos = False
            MensajeError(ex.Message)
        End Try
    End Function

    Private Sub BuscaDatos()
        Try
            Dim objPromo As New clsPromocionesCte
            Dim dtsRes As New DataSet

            objPromo.IDESTATUS = Val(ddlEstatus.SelectedValue)
            objPromo.DESCRIPCION = txtPromo.Text

            dtsRes = objPromo.ManejaPromocion(1)

            Session("dtsConsulta") = Nothing
            grvResult.PageIndex = 0
            grvResult.DataSource = Nothing

            If dtsRes.Tables.Count > 0 Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    Session("dtsConsulta") = dtsRes
                    grvResult.DataSource = dtsRes
                End If
            End If

            grvResult.DataBind()
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        BuscaDatos()
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As System.EventArgs) Handles btnLimpiar.Click
        LimpiaFiltros()
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As System.EventArgs) Handles btnAgregar.Click
        Response.Redirect("./manejaPromocionesCte.aspx?idcte=0")
    End Sub

    Protected Sub grvResult_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvResult.PageIndexChanging
        grvResult.PageIndex = e.NewPageIndex
        grvResult.DataSource = CType(Session("dtsConsulta"), DataSet)
        grvResult.DataBind()
    End Sub

    Protected Sub grvResult_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvResult.RowCommand
        If e.CommandName = "promoid" Then
            Response.Redirect("./manejaPromocionesCte.aspx?IdPromo=" & e.CommandArgument)
        End If
    End Sub

    Protected Sub ddlEstatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlEstatus.SelectedIndexChanged
        BuscaDatos()
    End Sub
End Class
