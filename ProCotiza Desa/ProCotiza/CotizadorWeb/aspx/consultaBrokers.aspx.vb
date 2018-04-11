'BUG-PC-13: AMR: 23/11/2016 Se quita valor por default en los combos.
'BUG-PC-26: JRHM: 20/12/2016 Se agrega metodo faltante para pager de grid
Imports System.Data

Partial Class aspx_consultaBrokers
    Inherits System.Web.UI.Page
    Dim strErr As String = ""


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()

        If Not IsPostBack Then
            CargaCombos()
            BuscaDatos()
            LimpiaFiltros()
        End If
    End Sub
    Public Sub LimpiaError()
        lblMensaje.Text = ""
        lblScript.Text = ""
    End Sub

    Private Sub LimpiaFiltros()
        txtnombroker.Text = ""
        ddlestatus.SelectedValue = 0
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

    Private Function CargaCombos() As Boolean
        Dim objCombo As New clsProcGenerales
        Dim dtsRes As New DataSet
        Try
            CargaCombos = False

            'combo de estatus
            dtsRes = objCombo.ObtenInfoParametros(1, strErr)
            If Trim$(strErr) = "" Then
                objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", ddlestatus, strErr, True)
                If Trim$(strErr) <> "" Then
                    MensajeError(strErr)
                    Exit Function
                End If
            Else
                MensajeError(strErr)
                Exit Function
            End If

            CargaCombos = True
        Catch ex As Exception
            CargaCombos = False
            MensajeError(ex.Message)
        End Try
    End Function

    Private Sub BuscaDatos()
        Try
            Dim objBro As New SNProcotiza.clsBrokerSeguros
            Dim dtsRes As New DataSet

            objBro.RazonSocial = txtnombroker.Text
            objBro.IDEstatus = ddlestatus.SelectedValue

            dtsRes = objBro.ManejaBroker(1)

            Session("dtsConsulta") = Nothing
            grvConsulta.PageIndex = 0
            grvConsulta.DataSource = Nothing

            If dtsRes.Tables.Count > 0 Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    Session("dtsConsulta") = dtsRes
                    grvConsulta.DataSource = dtsRes
                Else
                    'MensajeError("No se encontró información con los parámetros proporcionados")
                End If
            Else
                'MensajeError("No se encontró información para los parámetros proporcionados")
            End If

            grvConsulta.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        BuscaDatos()
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As System.EventArgs) Handles btnAgregar.Click
        Response.Redirect("./manejaBrokers.aspx?brokerId=0")
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "brokerId" Then
            Response.Redirect("./manejaBrokers.aspx?idBroker=" & e.CommandArgument)
        End If
        If e.CommandName = "AsigAse" Then
            Script = "AbrePopup('relacionaBrokerAseguradora.aspx?idBroker=" & e.CommandArgument & "&tipoRel=1',300,100,800,550)"
        End If
    End Sub

    Protected Sub ddlestatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlestatus.SelectedIndexChanged
        BuscaDatos()
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As System.EventArgs) Handles btnLimpiar.Click
        LimpiaFiltros()
        BuscaDatos()
    End Sub
    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        If CType(Session("dtsConsulta"), DataSet) Is Nothing Then
            BuscaDatos()
        End If

        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsulta"), DataSet)
        grvConsulta.DataBind()
    End Sub
End Class
