'BBV-P-412:BUG-PC-10 JRHM: 18/11/2016 Se agregaron eventos de busqueda para los dropdownlist para consulta y limpieza de parametros busqueda
Imports System.Data

Partial Class aspx_consultaPlazos
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
    End Sub

    Private Sub LimpiaFiltros()
        ddlestatus.SelectedValue = 0
        ddlPeriodicidad.SelectedValue = 0
    End Sub

    Public Sub MensajeError(ByVal strMsj As String)
        strMsj = Replace(strMsj, "'", "")
        strMsj = Replace(strMsj, """", "")
        strMsj = Replace(strMsj, "(", "")
        strMsj = Replace(strMsj, ")", "")
        lblMensaje.Text = "<script>alert('" & strMsj & "')</script>"
    End Sub

    Private Function CargaCombos() As Boolean
        Dim objCombo As New clsProcGenerales
        Dim dtsRes As New DataSet
        Try
            CargaCombos = False

            'Combo Estatus
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
            'Combo Periodicidad
            dtsRes = objCombo.ObtenInfoParametros(82, strErr, False, "1")
            If Trim$(strErr) = "" Then
                objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", ddlPeriodicidad, strErr, True)
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
            Dim objPlazo As New SNProcotiza.clsPlazo
            Dim dtsRes As New DataSet

            objPlazo.Status = Val(ddlestatus.SelectedValue)
            objPlazo.Id_Periodicidad = Val(ddlPeriodicidad.SelectedValue)

            Session("dtsPlazos") = Nothing
            dtsRes = objPlazo.ManejaPlazos(1)

            If dtsRes.Tables.Count > 0 Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    Session("dtsPlazos") = dtsRes
                    grvConsulta.DataSource = dtsRes
                    grvConsulta.DataBind()
                Else
                    MensajeError("No se encontró información con los parámetros proporcionados.")
                End If
            Else
                MensajeError("No se encontró información para los parámetros proporcionados.")
            End If

        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        BuscaDatos()
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As System.EventArgs) Handles btnAgregar.Click
        Response.Redirect("./ManejaPlazos.aspx?idPlazo=0")
    End Sub

    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        Dim dts As New DataSet
        Dim imgbtn As New ImageButton
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsPlazos"), DataSet)
        grvConsulta.DataBind()
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "plazoId" Then
            Response.Redirect("./ManejaPlazos.aspx?plazoId=" & e.CommandArgument)
        End If
    End Sub
    Protected Sub ddlPeriodicidad_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlPeriodicidad.SelectedIndexChanged
        BuscaDatos() 'JRHM 18/11/16 SE AGREGO EVENTO PARA ACTUALIZAR LA BUSQUEDA POR TIPO PRODUCTO
    End Sub
    Protected Sub ddlestatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlestatus.SelectedIndexChanged
        BuscaDatos() 'JRHM 18/11/16 SE AGREGO EVENTO PARA ACTUALIZAR LA BUSQUEDA POR TIPO PRODUCTO
    End Sub
    Protected Sub btnLimpiar_Click(sender As Object, e As System.EventArgs) Handles btnLimpiar.Click
        LimpiaFiltros()
        BuscaDatos()
    End Sub
End Class
