'BBVA-P-412: 06-07-2016: AMR: RQ02,RQ03,RQ04,RQ05,RQ06,RQ08 SE AGREGA FUNCIONALIDAD PARA LA RELACIÓN PAQUETES PRODUCTOS
'BUG-PC-42 JRHM 30/01/17 Se agrega funcionalidad de boton limpiar
'BUG-PC-48 JRHM 17/02/17 SE CORRIGE PROBLEMA DE PAGEINDEX DE RELACION PAQUETE-PRODUCTO
'BUG-PC-65:MPUESTO:19/05/2017:CORRECCION DE ADICION Y BORRADO DE RELACION PAQUETES-PRODUCTOS
'RQ-MN2-6: RHERNANDEZ: 15/09/17: SE AGREGAN NUEVOS FILTROS PARA LA OPCION 11 DE CONSULTA PAQUETES-PRODUCTOS
Imports System.Data
Imports SNProcotiza

Partial Class aspx_relacionaPaquetesProductos
    Inherits System.Web.UI.Page
    Dim strErr As String = String.Empty
    Dim objPaq As New clsPaquetes
    Dim objMarca As New clsMarcas

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()

        If Not IsPostBack Then
            CargaCombos(1)
            CargaCombos(3)
            CargaCombos(2)
        End If
    End Sub

    Public Sub LimpiaError()
        lblMensaje.Text = String.Empty
        lblScript.Text = String.Empty
    End Sub

    Public Sub MensajeError(ByVal strMsj As String)
        strMsj = Replace(strMsj, "'", "")
        strMsj = Replace(strMsj, """", "")
        strMsj = Replace(strMsj, "(", "")
        strMsj = Replace(strMsj, ")", "")
        lblMensaje.Text = "<script>alert('" & strMsj & "')</script>"
    End Sub

    Private Function CargaCombos(ByVal intOpc As Integer) As Boolean

        Dim objAgencias As New clsAgencias
        Dim objCombo As New clsProcGenerales
        Dim dtsRes As New DataSet

        Try
            CargaCombos = False

            Select Case intOpc
                Case 1

                    objMarca.IDEstatus = 2
                    dtsRes = objMarca.ManejaMarca(1)
                    strErr = objMarca.ErrorMarcas
                    If Trim$(strErr) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
							    'BBVA-P-412
                                objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_MARCA", cmbMarca, strErr, True)
                                cmbMarca.SelectedItem.Text = "< TODAS >"
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
                    Session("dtsConsultaPaqProd") = Nothing
                    grvConsulta.PageIndex = 0
                    grvConsulta.DataSource = Nothing

                    objPaq.IDPaquete = cmbMarca.SelectedValue
					'BBVA-P-412
                    objPaq.IDPaquete = Val(Request("idPaq"))
                    objPaq.IDMarca = cmbMarca.SelectedValue
                    ''sprint1
                    If Len(txtNom.Text) > 0 Then
                        objPaq.ProductoDesc = txtNom.Text.Trim
                    End If

                    If txtmodelo.Text.Trim.Length > 0 Then
                        objPaq.AnioModelo = Convert.ToInt16(txtmodelo.Text.Trim)
                    End If

                    objPaq.IDSubmarca = cmbSubmarca.SelectedValue
                    objPaq.IDClasificacionProd = cmbclasif.SelectedValue
                    objPaq.IDBroker = cmbBroker.SelectedValue

                    dtsRes = objPaq.ManejaPaquete(11)
                    strErr = objPaq.ErrorPaquete
                    If Trim(strErr) = "" Then
                        Session("dtsConsultaPaqProd") = dtsRes
                        grvConsulta.DataSource = dtsRes
                        grvConsulta.DataBind()
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If
                Case 3
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
   'BBVA-P-412
    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        CargaCombos(2)
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsultaPaqProd"), DataSet)
        grvConsulta.DataBind()
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "ProdID" Then

            Dim intConsul = 11
            Dim intInserta = 10
            Dim intBorra = 9
            Dim dts As New DataSet
            Dim objRow As GridViewRow = grvConsulta.Rows(Val(e.CommandArgument))
            Dim _objImg As ImageButton = objRow.Cells(6).Controls(1)
            Dim idProd As String = grvConsulta.DataKeys(Convert.ToInt32(e.CommandArgument)).Values(0).ToString

            objPaq.IDProducto = idProd
            objPaq.IDPaquete = Val(Request("idPaq"))
            objPaq.IDMarca = cmbMarca.SelectedValue

            dts = objPaq.ManejaPaquete(intConsul)

            If dts.Tables(0).Rows.Count > 0 Then
                If dts.Tables(0).Rows(0).Item("ASIG") = 0 Then
                    objPaq.IDProducto = dts.Tables(0).Rows(0).Item("ID_PRODUCTO")
                    objPaq.ManejaPaquete(intInserta)
                    _objImg.ImageUrl = "../img/tick.png"
                    objRow.Cells(9).Text = 1
                Else
                    objPaq.IDProducto = dts.Tables(0).Rows(0).Item("ID_PRODUCTO")
                    objPaq.ManejaPaquete(intBorra)
                    _objImg.ImageUrl = "../img/cross.png"
                    objRow.Cells(9).Text = 0
                End If
            Else
                objPaq.ManejaPaquete(intInserta)
                _objImg.ImageUrl = "../img/tick.png"
                objRow.Cells(9).Text = 1
            End If
        End If
    End Sub

    Protected Sub grvConsulta_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvConsulta.RowDataBound
        Dim _objImg As New ImageButton

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim datakey As String = Convert.ToString(grvConsulta.DataKeys(e.Row.RowIndex).Values(2).ToString)

            _objImg = e.Row.FindControl("ImageButton1")

            If datakey = 1 Then
                _objImg.ImageUrl = "../img/tick.png"
            Else
                _objImg.ImageUrl = "../img/cross.png"
            End If
        End If
    End Sub

    Protected Sub bntBuscaProd_Click(sender As Object, e As System.EventArgs) Handles bntBuscaProd.Click
        CargaCombos(2)
    End Sub

    'BUG-PC-65:MPUESTO:19/05/2017:CORRECCION DE ADICION Y BORRADO DE RELACION PAQUETES-PRODUCTOS
    Protected Sub btnNinguna_Click(sender As Object, e As System.EventArgs) Handles btnNinguna.Click
        objPaq.IDPaquete = Val(Request("idPaq"))
        If Len(txtNom.Text) > 0 Then
            objPaq.ProductoDesc = txtNom.Text.Trim
        End If
        If Len(txtmodelo.Text) > 0 Then
            objPaq.AnioModelo = txtmodelo.Text.Trim
        End If
        If cmbMarca.SelectedValue > 0 Then
        objPaq.IDMarca = cmbMarca.SelectedValue
        End If
        If cmbSubmarca.SelectedValue > 0 Then
            objPaq.IDSubmarca = cmbSubmarca.SelectedValue
        End If
        If cmbclasif.SelectedValue > 0 Then
            objPaq.IDClasificacionProd = cmbclasif.SelectedValue
        End If
        If cmbBroker.SelectedValue > 0 Then
            objPaq.IDBroker = cmbBroker.SelectedValue
        End If
        objPaq.ManejaPaquete(9)
        CargaCombos(2)
    End Sub

    'BUG-PC-65:MPUESTO:19/05/2017:CORRECCION DE ADICION Y BORRADO DE RELACION PAQUETES-PRODUCTOS
    Protected Sub btnTodas_Click(sender As Object, e As System.EventArgs) Handles btnTodas.Click
        objPaq.IDPaquete = Val(Request("idPaq"))
        If Len(txtNom.Text) > 0 Then
            objPaq.ProductoDesc = txtNom.Text.Trim
        End If
        If Len(txtmodelo.Text) > 0 Then
            objPaq.AnioModelo = txtmodelo.Text.Trim
        End If
        If cmbMarca.SelectedValue >= 0 Then
        objPaq.IDMarca = cmbMarca.SelectedValue
        End If
        If cmbSubmarca.SelectedValue > 0 Then
            objPaq.IDSubmarca = cmbSubmarca.SelectedValue
        End If
        If cmbclasif.SelectedValue > 0 Then
            objPaq.IDClasificacionProd = cmbclasif.SelectedValue
        End If
        If cmbBroker.SelectedValue > 0 Then
            objPaq.IDBroker = cmbBroker.SelectedValue
        End If
        objPaq.ManejaPaquete(10)
        CargaCombos(2)
    End Sub

	'BBVA-P-412
    Protected Sub cmbMarca_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbMarca.SelectedIndexChanged
        CargaCombos(3)
        CargaCombos(2)
    End Sub
    Protected Sub cmbSubmarca_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSubmarca.SelectedIndexChanged
        CargaCombos(2)
    End Sub

    Protected Sub cmbclasif_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbclasif.SelectedIndexChanged
        CargaCombos(2)
    End Sub

    Protected Sub cmbBroker_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBroker.SelectedIndexChanged
        CargaCombos(2)
    End Sub
    Protected Sub btnLimpiar_Click(sender As Object, e As System.EventArgs) Handles btnLimpiar.Click
        txtNom.Text = ""
        txtmodelo.Text = ""
        cmbMarca.SelectedValue = 0
        cmbSubmarca.SelectedValue = 0
        cmbBroker.SelectedValue = 0
        cmbclasif.SelectedValue = 0
        CargaCombos(3)
        CargaCombos(2)
    End Sub

End Class
