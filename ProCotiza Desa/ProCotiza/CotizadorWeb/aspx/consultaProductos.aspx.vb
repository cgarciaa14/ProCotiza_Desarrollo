'BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.
'BUG-PC-02:  09/11/2016: AMR: Error en manejaProductos
'RQ-MN2-6:RHERNANDEZ: 07/09/17: SE AGREGA ID BROKER PARA LA BUSQUEDA MAS AGIL DE PRODUCTOS-
Imports System.Data
Imports SNProcotiza

Partial Class aspx_consultaProductos
    Inherits System.Web.UI.Page
    Dim strErr As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()
        Script = ""

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
        'BUG-PC-02
        cmbEstatus.SelectedValue = "0"
        cmbTipoProd.SelectedValue = "0"
        cmbMarca.SelectedValue = "0"
        CargaCombos(3)
        cmbSubmarca.SelectedValue = "0"
        cmbBroker.SelectedValue = "0"
        txtNom.Text = ""
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
                        objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbEstatus, strErr, True) 'BUG-PC-02
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If

                    'combo de tipos de producto
                    Dim objTP As New SNProcotiza.clsTipoProductos
                    objTP.IDEstatus = 2

                    dtsRes = objTP.ManejaTipoProd(1)
                    strErr = objTP.ErrorTipoProducto

                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "DESCRIPCION", "ID_TIPO_PRODUCTO", cmbTipoProd, strErr, True) 'BUG-PC-02
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
                        objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_MARCA", cmbMarca, strErr, True) 'BUG-PC-02
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    End If

                    Dim objbroker As New SNProcotiza.clsBrokerSeguros
                    objbroker.IDEstatus = 2
                    dtsRes = objbroker.ManejaBroker(1)
                    If objbroker.ErrorBroker = "" Then

                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                objCombo.LlenaCombos(dtsRes, "RAZON_SOCIAL", "ID_BROKER", cmbBroker, strErr, True)
                                If strErr <> "" Then
                                    Exit Function
                                End If
                            Else
                                strErr = "No se encontro información de Brokers."
                                Exit Function
                            End If
                        Else
                            strErr = "No se encontro información."
                            Exit Function
                        End If
                    Else
                        strErr = objbroker.ErrorBroker
                        Exit Function
                    End If

                    dtsRes = objCombo.ObtenInfoParametros(62, strErr)
                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbclasif, strErr, True)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If
                Case 2
                    Dim objSubMarcas As New SNProcotiza.clsSubMarcas
                    Dim objMarcas As New SNProcotiza.clsMarcas
                    objSubMarcas.IDEstatus = 2

                    If cmbMarca.SelectedValue > 0 Then
                        objSubMarcas.IDMarca = cmbMarca.SelectedValue
                    End If

                    dtsRes = objSubMarcas.ManejaSubMarca(1)

                    If objSubMarcas.ErrorSubMarcas = "" Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            ''ddlsubmarca.Items.Clear()
                            objCombo.LlenaCombos(dtsRes, "DESCRIPCION", "ID_SUBMARCA", cmbSubmarca, strErr, True)
                            If strErr <> "" Then
                                MensajeError(strErr)
                                Exit Function
                            End If

                        Else
                            strErr = "No se encontro información de la Submarca."
                            Exit Function
                        End If
                    Else
                        strErr = objMarcas.ErrorMarcas
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
            Dim objProd As New clsProductos
            Dim dtsRes As New DataSet

            objProd.IDEstatus = Val(cmbEstatus.SelectedValue)
            objProd.IDTipoProducto = Val(cmbTipoProd.SelectedValue)
            objProd.Descripcion = Trim(txtNom.Text)
            objProd.IDMarca = Val(cmbMarca.SelectedValue)
            objProd.IDSubmarca = Val(cmbSubmarca.SelectedValue)
            objProd.idbroker = Val(cmbBroker.SelectedValue)
            objProd.IDClasificacion = Val(cmbclasif.SelectedValue)


            dtsRes = objProd.ManejaProducto(1)
            'BBVA-P-412:RQ18
            If objProd.ErrorProducto = "" Then
                Session("dtsConsulta") = Nothing
                grvConsulta.PageIndex = 0
                grvConsulta.DataSource = Nothing

                If dtsRes.Tables(0).Rows.Count > 0 Then
                    Session("dtsConsulta") = dtsRes
                    grvConsulta.DataSource = dtsRes
                End If
            Else
                MensajeError(objProd.ErrorProducto)
                Exit Sub
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
        BuscaDatos()
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As System.EventArgs) Handles btnAgregar.Click
        'BUG-PC-02
        Response.Redirect("./manejaProductos.aspx?idProd=0" & "&idTipo=" & cmbTipoProd.SelectedValue & "&idMarca=" & cmbMarca.SelectedValue)
    End Sub

    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsulta"), DataSet)
        grvConsulta.DataBind()
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "prodId" Then
            Response.Redirect("./manejaProductos.aspx?idProd=" & e.CommandArgument)
        End If
        'If e.CommandName = "prdAseg" Then
        '    Script = "AbrePopup('productosAseguradoras.aspx?idProd=" & e.CommandArgument & "',300,200,800,400)"
        'End If
        'If e.CommandName = "prdAge" Then
        '    Script = "AbrePopup('relacionaAgencias.aspx?idRel=" & e.CommandArgument & "&tipoRel=3',300,200,800,400)"
        'End If
    End Sub

    Protected Sub cmbMarca_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbMarca.SelectedIndexChanged

        If CargaCombos(2) Then
            BuscaDatos()
        Else
            MensajeError(strErr)
            cmbSubmarca.Items.Clear()
            Exit Sub
        End If
    End Sub

    Protected Sub cmbEstatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEstatus.SelectedIndexChanged
        BuscaDatos()
    End Sub
    'BUG-PC-02
    Protected Sub cmbTipoProd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTipoProd.SelectedIndexChanged
        BuscaDatos()
    End Sub

    Protected Sub cmbSubmarca_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSubmarca.SelectedIndexChanged
        BuscaDatos()
    End Sub

    Protected Sub cmbclasif_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbclasif.SelectedIndexChanged
        BuscaDatos()
    End Sub

    Protected Sub cmbBroker_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBroker.SelectedIndexChanged
        BuscaDatos()
    End Sub
End Class
