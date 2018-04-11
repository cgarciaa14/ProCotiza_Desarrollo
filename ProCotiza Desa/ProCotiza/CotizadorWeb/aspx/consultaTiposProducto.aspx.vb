'BUG-PC-34 12/01/2017 MAUT Se limpia el grid
'BUG-PC-42 30/01/17 JRHM Se agrega opcion default en combo box de status
'BUG-PC-40:PVARGAS:27/01/2017:SE COMENTA LA LIMPIEZA DEL GRIDVIEW PARA MOSTRAR DESDE UN INICIO TODOS LOS REGISTROS.//REVISAR TRACKER BUG_

Imports System.Data
Imports SNProcotiza

Partial Class aspx_consultaTiposProducto
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
        End If
    End Sub

    Public Sub LimpiaError()
        lblMensaje.Text = ""
    End Sub

    Private Sub LimpiaFiltros()
        ''cmbEstatus.SelectedValue = ""
        txtNom.Text = ""
        cmbEstatus.SelectedValue=0
        'BUG-PC-34 12/01/2017 MAUT Se limpiar el grid
        'BUG-PC-40:PVARGAS:27/01/2017:SE COMENTA LA LIMPIEZA DEL GRIDVIEW PARA MOSTRAR DESDE UN INICIO TODOS LOS REGISTROS.//REVISAR TRACKER BUG_
        'grvConsulta.DataSource = Nothing
        'grvConsulta.DataBind()
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

            'combo de estatus
            dtsRes = objCombo.ObtenInfoParametros(1, strErr)
            If Trim$(strErr) = "" Then
                'BUG-PC-40:PVARGAS:27/01/2017:SE CAMBIA LA OPCION DEL PARAMETRO blnAgregaBlanco DE LA FUNSION LlenaCombos.
                objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbEstatus, strErr, True)
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
            MensajeError(ex.Message)
        End Try
    End Function

    Private Sub BuscaDatos()
        Try
            Dim objTP As New clsTipoProductos
            Dim dtsRes As New DataSet

            objTP.IDEstatus = Val(cmbEstatus.SelectedValue)
            objTP.Descripcion = Trim(txtNom.Text)

            dtsRes = objTP.ManejaTipoProd(1)

            Session("dtsConsulta") = Nothing
            grvConsulta.PageIndex = 0
            grvConsulta.DataSource = Nothing
            'BUG-PC-40:PVARGAS:27/01/2017:SE DEJA DE MOSTRAR EL MENSAJE EMERGENTE CUANDO NO ENCUETRA REGISTROS Y SE MUESTRA SOLO EN LA TABLA.
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
        Response.Redirect("./manejaTiposProducto.aspx?idTipoProd=0")
    End Sub

    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsulta"), DataSet)
        grvConsulta.DataBind()
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "tipProdId" Then
            Response.Redirect("./manejaTiposProducto.aspx?idTipoProd=" & e.CommandArgument)
        End If
    End Sub

    Protected Sub cmbEstatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEstatus.SelectedIndexChanged
        BuscaDatos()
    End Sub
End Class
