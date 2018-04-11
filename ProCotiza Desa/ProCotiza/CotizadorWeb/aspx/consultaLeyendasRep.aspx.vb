'BBV-P-412 RQCOT-05: AVH 08/09/2016 SE AGREGA SECCION
'BUG-PC-40:PVARGAS:27/01/2017:SE CARGAN NUEVAMENTE LOS REGISTROS DE LA TABLA AL LIMPIAR LAS OPCIONES DE BUSQUEDA

Imports System.Data
Imports SNProcotiza

Partial Class aspx_consultaLeyendasRep
    Inherits System.Web.UI.Page
    Dim strErr As String = ""


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()

        If Not IsPostBack Then
            CargaCombos(1)
            CargaCombos(2)
            BuscaDatos()
            LimpiaFiltros()
        End If
    End Sub

    Public Sub LimpiaError()
        lblMensaje.Text = ""
    End Sub

    Private Sub LimpiaFiltros()
        txtNom.Text = ""
        cmbEstatus.SelectedValue = 0
        cmbSeccion.SelectedValue = 0
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
        Dim objLeyenda As New clsLeyendasRep
        Try
            CargaCombos = False

            Select Case intOpc
                Case 1
                    'combo de estatus
                    dtsRes = objCombo.ObtenInfoParametros(1, strErr)
                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbEstatus, strErr, True)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If
                Case 2
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
            End Select

            CargaCombos = True
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try

    End Function

    Private Sub BuscaDatos()
        Try
            Dim objLey As New clsLeyendasRep
            Dim dtsRes As New DataSet

            If cmbSeccion.SelectedValue <> 0 Then
                objLey.IDSeccion = cmbSeccion.SelectedValue
            End If

            objLey.IDEstatus = Val(cmbEstatus.SelectedValue)
            objLey.Leyenda = Trim(txtNom.Text)

            dtsRes = objLey.ManejaLeyenda(1)

            Session("dtsConsulta") = Nothing
            grvConsulta.PageIndex = 0
            grvConsulta.DataSource = Nothing

            If dtsRes.Tables.Count > 0 Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    Session("dtsConsulta") = dtsRes
                    grvConsulta.DataSource = dtsRes
                Else
                    'MensajeError("No se encontró información con los parámetros proporcionados.")
                End If
            Else
                'MensajeError("No se encontró información para los parámetros proporcionados.")
            End If

            grvConsulta.DataBind()
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        BuscaDatos()
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As System.EventArgs) Handles btnLimpiar.Click
        LimpiaFiltros()
        'BUG-PC-40:PVARGAS:27/01/2017:SE CARGAN NUEVAMENTE LOS REGISTROS DE LA TABLA AL LIMPIAR LAS OPCIONES DE BUSQUEDA
        BuscaDatos()
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As System.EventArgs) Handles btnAgregar.Click
        Response.Redirect("./manejaLeyendasRep.aspx?idLeyenda=0")
    End Sub

    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsulta"), DataSet)
        grvConsulta.DataBind()
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "leyendaId" Then
            Response.Redirect("./manejaLeyendasRep.aspx?idLeyenda=" & e.CommandArgument)
        End If
    End Sub

    Protected Sub cmbEstatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEstatus.SelectedIndexChanged
        BuscaDatos()
    End Sub

    Protected Sub cmbSeccion_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbSeccion.SelectedIndexChanged
        BuscaDatos()
    End Sub
End Class
