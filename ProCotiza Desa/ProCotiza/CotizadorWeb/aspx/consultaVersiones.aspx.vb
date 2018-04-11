'BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.
'BUG-PC-09:AMR:16/11/2016 CUTARMA01:No pertime espacios en blanco al ingresar el nombre de la marca
'BUG-PC-39 25/01/17 JRHM Se modifico carga de pantalla para mostrar submarca default
'BUG-PC-43 01/02/17 JRHM Se limita a que se busque la submarca default solo cuando se seleccione una marca
Imports System.Data

Partial Class aspx_consultaVersiones
    Inherits System.Web.UI.Page
    Dim strErr As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()
        'BUG-PC-09
        If Not IsPostBack Then
            If Not CargaCombos(1) Then
                MensajeError(strErr)
                Exit Sub
            End If

            If Not CargaCombos(2) Then
                MensajeError(strErr)
                Exit Sub
            End If

            If Not CargaCombos(3) Then
                MensajeError(strErr)
                Exit Sub
            End If
            BuscaDatos()
        End If
    End Sub

    Public Sub LimpiaError()
        lblMensaje.Text = ""
    End Sub

    Private Sub LimpiaFiltros()
        cmbEstatus.SelectedValue = 0
        ddlmarca.SelectedValue = 0
        ddlsubmarca.SelectedValue = 0
        txtNom.Text = ""
    End Sub

    Public Sub MensajeError(ByVal strMsj As String)
        strMsj = Replace(strMsj, "'", "")
        strMsj = Replace(strMsj, """", "")
        strMsj = Replace(strMsj, "(", "")
        strMsj = Replace(strMsj, ")", "")
        lblMensaje.Text = "<script>alert('" & strMsj & "')</script>"
    End Sub

    Private Function CargaCombos(opc As Integer) As Boolean
        Dim objCombo As New clsProcGenerales
        Dim objMarcas As New SNProcotiza.clsMarcas
        Dim objSubMarcas As New SNProcotiza.clsSubMarcas
        Dim dtsRes As New DataSet
        Try
            CargaCombos = False

            'BUG-PC-09
            Select Case opc
                Case 1
                    'Combo Estatus
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
                    'Combo Marcas
                    If cmbEstatus.SelectedValue <> 0 Then
                        objMarcas.IDEstatus = cmbEstatus.SelectedValue
                    End If

                    dtsRes = objMarcas.ManejaMarca(1)

                    If objMarcas.ErrorMarcas = "" Then

                        If dtsRes.Tables.Count > 0 Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_MARCA", ddlmarca, strErr, True)
                            If strErr <> "" Then
                                Exit Function
                            End If
                    Else
                                strErr = "No se encontro información."
                            End If
                        Else
                            strErr = "No se encontro información."
                        End If
                    Else
                        strErr = objMarcas.ErrorMarcas
                        Exit Function
                    End If
                Case 3
                    'Combo SubMarcas
                    If cmbEstatus.SelectedValue <> 0 Then
                        objSubMarcas.IDEstatus = cmbEstatus.SelectedValue
                    End If

                    objSubMarcas.IDEstatus = 2

                    If ddlmarca.SelectedValue > 0 Then
                        objSubMarcas.IDMarca = ddlmarca.SelectedValue
                    End If

                    dtsRes = objSubMarcas.ManejaSubMarca(1)

                    If objSubMarcas.ErrorSubMarcas = "" Then
                        If dtsRes.Tables.Count > 0 Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            ddlsubmarca.Items.Clear()
                                If (ddlmarca.SelectedValue = 0) Then
                                    objCombo.LlenaCombos(dtsRes, "DESCRIPCION", "ID_SUBMARCA", ddlsubmarca, strErr, True)
                                Else
                                objCombo.LlenaCombos(dtsRes, "DESCRIPCION", "ID_SUBMARCA", ddlsubmarca, strErr, True, True, "REG_DEFAULT")
                                End If
                            If strErr <> "" Then
                                Exit Function
                            End If
                    Else
                                objCombo.LlenaCombos(dtsRes, "DESCRIPCION", "ID_SUBMARCA", ddlsubmarca, strErr, True)
                                If strErr <> "" Then
                                    Exit Function
                                End If
                            End If
                        Else
                            strErr = "No se encontro información."
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
            Dim objVersiones As New SNProcotiza.clsVersiones
            Dim dtsRes As New DataSet

            Session("dtsConsultaVersiones") = Nothing
            grvConsulta.PageIndex = 0
            grvConsulta.DataSource = Nothing

            If cmbEstatus.SelectedValue > 0 Then
                objVersiones.IDEstatus = cmbEstatus.SelectedValue
            End If

            If ddlmarca.SelectedValue > 0 Then
                objVersiones.IDMarca = ddlmarca.SelectedValue
            End If

            If ddlsubmarca.SelectedValue > 0 Then
                objVersiones.IDSubMarca = ddlsubmarca.SelectedValue
            End If

            If txtNom.Text.Trim.Length > 0 Then
                objVersiones.Descripcion = txtNom.Text.Trim
            End If

            dtsRes = objVersiones.ManejaVersion(1)

            If objVersiones.ErrorVersion = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    Session("dtsConsultaVersiones") = dtsRes
                    grvConsulta.DataSource = dtsRes
                End If
            Else
                MensajeError(objVersiones.ErrorVersion)
                Exit Sub
            End If

            grvConsulta.DataBind()

        Catch ex As Exception
            MensajeError(ex.Message)
            Exit Sub
        End Try
    End Sub

    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsultaVersiones"), DataSet)
        grvConsulta.DataBind()
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "versionId" Then
            Response.Redirect("./manejaVersiones.aspx?idVersion=" & e.CommandArgument)
        End If
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        BuscaDatos()
    End Sub

    Protected Sub cmbEstatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEstatus.SelectedIndexChanged
        BuscaDatos()
    End Sub

    Protected Sub ddlmarca_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlmarca.SelectedIndexChanged
        'BUG-PC-09
        If Not CargaCombos(3) Then
            MensajeError(strErr)
        End If
        BuscaDatos()
    End Sub

    Protected Sub ddlsubmarca_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlsubmarca.SelectedIndexChanged
        BuscaDatos()
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As System.EventArgs) Handles btnAgregar.Click
        Response.Redirect("./manejaVersiones.aspx?idVersion=0" & "&idMarca=" & ddlmarca.SelectedValue & "&idSubmarca=" & ddlsubmarca.SelectedValue)
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As System.EventArgs) Handles btnLimpiar.Click
        LimpiaFiltros()
        BuscaDatos()
    End Sub
End Class
