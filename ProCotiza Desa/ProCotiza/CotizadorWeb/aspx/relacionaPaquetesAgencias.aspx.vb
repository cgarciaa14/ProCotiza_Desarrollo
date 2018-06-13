'BBVA-P-412: AMR: RQ02,RQ03,RQ04,RQ05,RQ06,RQ08
'BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.
'BBV-P-412:BUG-PC-35 18/01/2017 JRHM correcion de funcionalidad de boton limpiar
'BUG-PC-48 JRHM 15/02/17 SE HABILITO LA OPCION SELECCIONAR EN LOS COMBOS DE BUSQUEDA
'BUG-PC-50 MARREDONDO 06/03/2017 SE CORRIGE INSERT Y DELETE DE ASIGNACION DE AGENCIAS MASIVO
'RQADM2-04: ERODRIGUEZ: 13/09/2017: Se agrego busqueda por ID de agencia.
'BUG-PC-160: CGARCIA: 26/02/2018: SE AGREGA FILTRADO POR ESQUEMA 
'RQ-PC9: DCORNEJO: 18/05/2018: SE MODIFICA EL PAQUETE PARA AGREGAR VALIDACIONES AL FILTRAR POR ESQUEMA
Imports System.Data
Imports SNProcotiza

Partial Class aspx_relacionaPaquetesAgencias
    Inherits System.Web.UI.Page
    Dim strErr As String = ""
    Dim objPaq As New clsPaquetes


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LimpiaError()

        If Not IsPostBack Then
            CargaCombos(1)
            'RQ-PC9: DCORNEJO:
            'objPaq.IDEsquema = -1
            CargaCombos(2)

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

    Private Function CargaCombos(ByVal intOpc As Integer) As Boolean

        Dim objAgencias As New clsAgencias
        Dim objCombo As New clsProcGenerales
        Dim objAlianza As New clsAlianzas  'BBVA-P-412
        Dim objGrupo As New clsGrupos  'BBVA-P-412
        Dim objDivision As New clsDivisiones
        Dim dtsRes As New DataSet
        Dim objEsquema As New clsEsquema

        Try
            CargaCombos = False

            Select Case intOpc
                Case 1
                    Session("dtsConsultaPaqAge") = Nothing
                    grvConsulta.PageIndex = 0
                    grvConsulta.DataSource = Nothing
                    'BBVA-P-412
                    'BBVA-P-412:RQ18
                    ''Combo Paquete
                    objPaq.CargaPaquete(Val(Request("idPaq")))
                    If objPaq.ErrorPaquete = "" Then
                        lbldesc.Text = objPaq.Nombre
                    End If

                    'objPaq.IDEstatus = 2
                    'dtsRes = objPaq.ManejaPaquete(1)
                    'strErr = objPaq.ErrorPaquete
                    'If Trim$(strErr) = "" Then
                    '    If dtsRes.Tables.Count > 0 Then
                    '        If dtsRes.Tables(0).Rows.Count > 0 Then
                    '            objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_PAQUETE", cmbPaquete, strErr, False)
                    '            If Trim$(strErr) <> "" Then
                    '                MensajeError(strErr)
                    '                Exit Function
                    '            End If
                    '        Else
                    '            MensajeError("No se encontro información de los Paquetes.")
                    '        End If
                    '    Else
                    '        MensajeError("No se encontro información de los Paquetes.")
                    '        Exit Function
                    '    End If
                    '    cmbPaquete.SelectedValue = (Val(Request("idPaq")))
                    'Else
                    '    MensajeError(strErr)
                    '    Exit Function
                    'End If

                    ''Combo Alianza
                    objAlianza.Estatus = 2
                    dtsRes = objAlianza.ManejaAlianza(1)
                    If objAlianza.ErrorAlianza = "" Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            'ddlalianza.Items.Clear()
                            'ddlalianza.Items.Insert(0, New ListItem("", "-1"))
                            'ddlalianza.AppendDataBoundItems = True
                            objCombo.LlenaCombos(dtsRes, "ALIANZA", "ID_ALIANZA", ddlalianza, strErr, True, , , -1)
                            If strErr <> "" Then
                                MensajeError(strErr)
                                Exit Function
                            End If
                        Else
                            MensajeError("No se encontro información de las Alianzas.")
                            Exit Function
                        End If
                    Else
                        MensajeError(objAlianza.ErrorAlianza)
                        Exit Function
                    End If

                    ''Combo Grupos
                    objGrupo.Estatus = 2
                    dtsRes = objGrupo.ManejaGrupo(1)
                    If objGrupo.ErrorGrupo = "" Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            'ddlgrupo.Items.Clear()
                            'ddlgrupo.Items.Insert(0, New ListItem("", "-1"))
                            'ddlgrupo.AppendDataBoundItems = True
                            objCombo.LlenaCombos(dtsRes, "GRUPO", "ID_GRUPO", ddlgrupo, strErr, True, , , -1)
                            If strErr <> "" Then
                                MensajeError(strErr)
                                Exit Function
                            End If
                        Else
                            MensajeError("No se encontro información de los Grupos.")
                            Exit Function
                        End If
                    Else
                        MensajeError(objGrupo.ErrorGrupo)
                        Exit Function
                    End If

                    ''Combo Divisiones
                    objDivision.Estatus = 2
                    dtsRes = objDivision.ManejaDivision(1)
                    If objDivision.ErrorDivision = "" Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            'ddldivision.Items.Clear()
                            'ddldivision.Items.Insert(0, New ListItem("", "-1"))
                            'ddldivision.AppendDataBoundItems = True
                            objCombo.LlenaCombos(dtsRes, "DIVISION", "ID_DIVISION", ddldivision, strErr, True, , , -1)
                            If strErr <> "" Then
                                MensajeError("No se encontro información de las Divisiones.")
                            End If
                        End If
                    Else
                        MensajeError(objDivision.ErrorDivision)
                    End If

                    ''Combo de esquemas
                    'BUG-PC-160: CGARCIA: 26/02/2018: SE AGREGA FILTRADO POR ESQUEMA 
                    dtsRes = objEsquema.MAnejaEsquema(1)
                    If objEsquema.ErrorEsquemas = "" Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            'RQ-PC9: DCORNEJO: SE LE QUITA EL VALOR -1 Y SE DEJA EN 0
                            objCombo.LlenaCombos(dtsRes, "CDESCRIPCION", "ID_ESQUEMAS", ddlEsquem, strErr, True)
                            If strErr <> "" Then
                                MensajeError("No se encontro información sobre esquemas.")
                            End If
                        End If
                    Else
                        MensajeError(objEsquema.ErrorEsquemas)
                    End If

                Case 2
                    'BBVA-P-412:RQ18
                    If ddlalianza.SelectedValue >= 0 Then
                        objPaq.IDAlianza = ddlalianza.SelectedValue
                    End If

                    If ddldivision.SelectedValue >= 0 Then
                        objPaq.IDDivision = ddldivision.SelectedValue
                    End If

                    If ddlgrupo.SelectedValue >= 0 Then
                        objPaq.IDGrupo = ddlgrupo.SelectedValue
                    End If

                    objPaq.IDPaquete = Val(Request("idPaq"))
                    If Len(txtNom.Text) > 0 Then
                        objPaq.Agencia = txtNom.Text.Trim
                    End If

                    If Not String.IsNullOrEmpty(Trim(txtId.Text)) Then
                        If Integer.TryParse(Trim(txtId.Text), Nothing) Then
                            objPaq.IDAgencia = Trim(txtId.Text)
                        Else
                            MensajeError("Id Agencia debe ser numérico.")
                        End If
                    Else
                        objPaq.IDAgencia = Nothing
                    End If
                    'BUG-PC-160: CGARCIA: 26/02/2018: SE AGREGA FILTRADO POR ESQUEMA 
                    If ddlEsquem.SelectedValue > 0 Then
                        objPaq.IDEsquema = ddlEsquem.SelectedValue
                    End If

                    dtsRes = objPaq.ManejaPaquete(35)
                    strErr = objPaq.ErrorPaquete
                    If Trim(strErr) = "" Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            Session("dtsConsultaPaqAge") = dtsRes
                            grvConsulta.DataSource = dtsRes
                        End If
                    Else
                        grvConsulta.DataSource = Nothing
                        MensajeError(strErr)
                        Exit Function
                    End If
                    grvConsulta.DataBind()
            End Select

            CargaCombos = True
        Catch ex As Exception
            CargaCombos = False
            MensajeError(ex.Message)
        End Try
    End Function

    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging

        If CType(Session("dtsConsultaPaqAge"), DataSet) Is Nothing Then
            CargaCombos(2)
        End If


        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsultaPaqAge"), DataSet)
        grvConsulta.DataBind()
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "AgeID" Then

            Dim intConsul As Integer = 35
            Dim intInserta As Integer = 36
            Dim intBorra As Integer = 37
            Dim dts As New DataSet
            Dim objRow As GridViewRow = grvConsulta.Rows(Val(e.CommandArgument))
            Dim objImg As ImageButton = objRow.Cells(2).Controls(1)
            Dim idAge As String = grvConsulta.DataKeys(Convert.ToInt32(e.CommandArgument)).Values(0).ToString

            'BBVA-P-412:RQ18
            objPaq.IDPaquete = Val(Request("idPaq"))
            objPaq.IDAgencia = idAge
            dts = objPaq.ManejaPaquete(intConsul)

            If dts.Tables(0).Rows.Count > 0 Then
                If dts.Tables(0).Rows(0).Item("ASIG") = 0 Then
                    objPaq.ManejaPaquete(intInserta)
                    objImg.ImageUrl = "../img/tick.png"
                    objRow.Cells(3).Text = 1
                Else
                    objPaq.ManejaPaquete(intBorra)
                    objImg.ImageUrl = "../img/cross.png"
                    objRow.Cells(3).Text = 0
                End If
            Else
                objPaq.ManejaPaquete(intInserta)
                objImg.ImageUrl = "../img/tick.png"
                objRow.Cells(3).Text = 1
            End If
        End If
        CargaCombos(2)
    End Sub

    Protected Sub grvConsulta_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvConsulta.RowDataBound
        Dim _objImg As New ImageButton

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim datakey As String = Convert.ToString(grvConsulta.DataKeys(e.Row.RowIndex).Values(1).ToString)

            _objImg = e.Row.FindControl("ImageButton1")
            If datakey = 1 Then
                _objImg.ImageUrl = "../img/tick.png"
            Else
                _objImg.ImageUrl = "../img/cross.png"
            End If

            grvConsulta.Columns(0).ItemStyle.Width = 5
        End If
    End Sub

    Protected Sub bntBuscaAgen_Click(sender As Object, e As System.EventArgs) Handles bntBuscaAgen.Click
        CargaCombos(2)
    End Sub

    Protected Sub btnTodas_Click(sender As Object, e As System.EventArgs) Handles btnTodas.Click
        objPaq.IDPaquete = Val(Request("idPaq"))
        If Len(txtNom.Text) > 0 Then
            objPaq.Agencia = txtNom.Text.Trim
        End If
        If ddlalianza.SelectedValue >= 0 Then
            objPaq.IDAlianza = ddlalianza.SelectedValue
        End If
        If ddldivision.SelectedValue >= 0 Then
            objPaq.IDDivision = ddldivision.SelectedValue
        End If
        If ddlgrupo.SelectedValue >= 0 Then
            objPaq.IDGrupo = ddlgrupo.SelectedValue
        End If
        'RQ-PC9: DCORNEJO:
        If ddlEsquem.SelectedValue >= 0 Then
            objPaq.IDEsquema = ddlEsquem.SelectedValue
        End If
        If Not String.IsNullOrEmpty(Trim(txtId.Text)) Then
            If Integer.TryParse(Trim(txtId.Text), Nothing) Then
                objPaq.IDAgencia = Trim(txtId.Text)
            Else
                MensajeError("Id Agencia debe ser numérico.")
            End If

        End If
        objPaq.ManejaPaquete(36)
        CargaCombos(2)
    End Sub

    Protected Sub btnNinguna_Click(sender As Object, e As System.EventArgs) Handles btnNinguna.Click
        objPaq.IDPaquete = Val(Request("idPaq"))
        If Len(txtNom.Text) > 0 Then
            objPaq.Agencia = txtNom.Text.Trim
        End If
        If ddlalianza.SelectedValue >= 0 Then
            objPaq.IDAlianza = ddlalianza.SelectedValue
        End If
        If ddldivision.SelectedValue >= 0 Then
            objPaq.IDDivision = ddldivision.SelectedValue
        End If
        If ddlgrupo.SelectedValue >= 0 Then
            objPaq.IDGrupo = ddlgrupo.SelectedValue
        End If
        'RQ-PC9: DCORNEJO:
        If ddlEsquem.SelectedValue >= 0 Then
            objPaq.IDEsquema = ddlEsquem.SelectedValue
        End If
        If Not String.IsNullOrEmpty(Trim(txtId.Text)) Then
            If Integer.TryParse(Trim(txtId.Text), Nothing) Then
                objPaq.IDAgencia = Trim(txtId.Text)
            Else
                MensajeError("Id Agencia debe ser numérico.")
            End If

        End If

        objPaq.ManejaPaquete(37)
        CargaCombos(2)
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As System.EventArgs) Handles btnLimpiar.Click
        txtNom.Text = ""
        ddlalianza.SelectedValue = -1
        ddldivision.SelectedValue = -1
        ddlgrupo.SelectedValue = -1
        'RQ-PC9: DCORNEJO:
        ddlEsquem.SelectedValue = 0
        txtId.Text = ""
        CargaCombos(2)
    End Sub

    Protected Sub ddlalianza_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlalianza.SelectedIndexChanged
        CargaCombos(2)
    End Sub

    Protected Sub ddldivision_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddldivision.SelectedIndexChanged
        CargaCombos(2)
    End Sub

    Protected Sub ddlgrupo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlgrupo.SelectedIndexChanged
        CargaCombos(2)
    End Sub
    'RQ-PC9: DCORNEJO:
    Protected Sub ddlEsquem_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlEsquem.SelectedIndexChanged
        CargaCombos(2)
    End Sub
End Class
