'BBV-P-412:AVH:09/08/2016 RQ20.2 Se comenta mensaje de error, se mostrara en el GV si no encuentra informacion
'BUGPC03 CO02-CO19: 10/11/2016: GVARGAS: Correccion bugs
'BBVA-P-412: JRHM 14/11/2016: BUG-PC-08 – Inserccion de valor seleccionar a combobox
'BBVA-P-412:BUG-PC-19 JRHM 24/11/2016 Se agrego metodo busca datos a boton limpiar para reiniciar busqueda
'BUG-PC-51 MAPH 17/04/2017 CAMBIOS SOLICITADOS POR MARREDONDO
Imports System.Data
Imports SNProcotiza

Partial Class aspx_consultaAccesorios
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
            BuscaDatos()
            LimpiaFiltros()
        End If
    End Sub

    Public Sub LimpiaError()
        lblMensaje.Text = ""
    End Sub

    Private Sub LimpiaFiltros()
        txtNom.Text = ""
        grvConsulta.DataBind()

        cmbTipoProd.SelectedIndex = 0
        cmbMarca.SelectedIndex = 0
        cmbEstatus.SelectedIndex = 0
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

                    'combo de tipos de producto
                    Dim objTP As New clsTipoProductos
                    objTP.IDEstatus = 2

                    dtsRes = objTP.ManejaTipoProd(1)
                    strErr = objTP.ErrorTipoProducto

                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "DESCRIPCION", "ID_TIPO_PRODUCTO", cmbTipoProd, strErr, True)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    End If

                    'combo de marcas
                    Dim objMarca As New clsMarcas
                    objMarca.IDEstatus = 2
                    objMarca.IDTipoRegistro = 113 'trae marcas que puedan registrar productos

                    dtsRes = objMarca.ManejaMarca(5)
                    strErr = objMarca.ErrorMarcas

                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_MARCA", cmbMarca, strErr, True)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
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
            Dim objAcc As New clsAccesorios
            Dim dtsRes As New DataSet

            objAcc.IDEstatus = Val(cmbEstatus.SelectedValue)
            objAcc.IDTipoProducto = Val(cmbTipoProd.SelectedValue)
            objAcc.IDMarca = Val(cmbMarca.SelectedValue)
            objAcc.Descripcion = Trim(txtNom.Text)

            dtsRes = objAcc.ManejaAccesorio(1)

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
        End Try
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        BuscaDatos()
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As System.EventArgs) Handles btnLimpiar.Click
        LimpiaFiltros()
        BuscaDatos()
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As System.EventArgs) Handles btnAgregar.Click
        Response.Redirect("./manejaAccesorios.aspx?idAccesorio=0")
    End Sub

    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsulta"), DataSet)
        grvConsulta.DataBind()
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "accesorioId" Then
            Response.Redirect("./manejaAccesorios.aspx?idAccesorio=" & e.CommandArgument)
        End If
    End Sub
    Protected Sub cmbTipoProd_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoProd.SelectedIndexChanged
        BuscaDatos() 'JRHM 14/11/16 SE AGREGO EVENTO PARA ACTUALIZAR LA BUSQUEDA POR TIPO PRODUCTO
    End Sub
    Protected Sub cmbMarca_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbMarca.SelectedIndexChanged
        BuscaDatos() 'JRHM 14/11/16 SE AGREGO EVENTO PARA ACTUALIZAR LA BUSQUEDA POR MARCA
    End Sub
    Protected Sub cmbEstatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEstatus.SelectedIndexChanged
        BuscaDatos() 'JRHM 14/11/16 SE AGREGO EVENTO PARA ACTUALIZAR LA BUSQUEDA POR ESTATUS
    End Sub
End Class