Imports System.Data
Imports SNProcotiza

Partial Class aspx_manejaTiposOperacion
    Inherits System.Web.UI.Page
    Dim strErr As String = ""
    Dim strPersonas As String = ""



    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()

        If Not IsPostBack Then


            CargaCombos(1)
            If Val(Request("tipOpId")) > 0 Then
                CargaInfo()
            Else
                HabilitaDeshabilita(1)
                HabilitaDeshabilita(2)
                HabilitaDeshabilita(3)
            End If
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

    Public Sub CierraPantalla(ByVal strPant As String)
        lblMensaje.Text += "<script>alert('El registro se guardó con éxito'); document.location.href='" & strPant & "';</script>"
    End Sub

    Private Sub CargaInfo()
        Try
            Dim intTipOp As Integer = Val(Request("tipOpId"))
            Dim objTO As New clsTiposOperacion(intTipOp)

            If objTO.IDTipoOperacion > 0 Then
                lblId.Text = objTO.IDTipoOperacion
                txtNom.Text = objTO.Nombre
                txtClave.Text = objTO.Clave
                cmbTipEsq.SelectedValue = objTO.IDTipoEsquemaFinanciamiento
                cmbEstatus.SelectedValue = objTO.IDEstatus
                chkOpcComp.Checked = IIf(objTO.PideOpcionCompra = 1, True, False)
                chkCapIncIva.Checked = IIf(objTO.CapitalFinaciarIncluyeIVA = 1, True, False)

                chkValResid.Checked = IIf(objTO.PideValorResidual = 1, True, False)
                txtLeyValRes.Text = objTO.LeyendaValorResidual
                HabilitaDeshabilita(1)

                chkDepGar.Checked = IIf(objTO.PideDepositoGarantia = 1, True, False)
                txtLeyDepGar.Text = objTO.LeyendaDepositoGarantia
                HabilitaDeshabilita(2)

                chkGtosExt.Checked = IIf(objTO.CobraGastosExtra = 1, True, False)
                HabilitaDeshabilita(3)
                cmbGtoExt.SelectedValue = objTO.IDTipoGastoExtra

                chkPagoIni.Checked = IIf(objTO.PermitecalculoPagoInicial = 1, True, False)
                HabilitaDeshabilita(4)
                cmbForPagIni.SelectedValue = objTO.IDTipoFormulaPagoInicial
            Else
                MensajeError("No se localizó información para el el tipo de operación.")
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Private Function CargaCombos(ByVal intOpc As Integer) As Boolean
        Dim objCombo As New clsProcGenerales
        Dim dtsRes As New DataSet
        Try
            CargaCombos = False

            'combo de estatus
            dtsRes = objCombo.ObtenInfoParametros(1, strErr)
            If Trim$(strErr) = "" Then
                objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbEstatus, strErr)
                If Trim$(strErr) <> "" Then
                    MensajeError(strErr)
                    Exit Function
                End If
            Else
                MensajeError(strErr)
                Exit Function
            End If

            'combo de tipos esquemas de financiamiento
            dtsRes = objCombo.ObtenInfoParametros(46, strErr)
            If Trim$(strErr) = "" Then
                objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbTipEsq, strErr)
                If Trim$(strErr) <> "" Then
                    MensajeError(strErr)
                    Exit Function
                End If
            Else
                MensajeError(strErr)
                Exit Function
            End If

            'combo de tipos de gasto
            dtsRes = objCombo.ObtenInfoParametros(91, strErr)
            If Trim$(strErr) = "" Then
                objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbGtoExt, strErr, True)
                If Trim$(strErr) <> "" Then
                    MensajeError(strErr)
                    Exit Function
                End If
            Else
                MensajeError(strErr)
                Exit Function
            End If

            'combo fórmulas pago inicial
            dtsRes = objCombo.ObtenInfoParametros(108, strErr)
            If Trim$(strErr) = "" Then
                objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbForPagIni, strErr, True)
                If Trim$(strErr) <> "" Then
                    MensajeError(strErr)
                    Exit Function
                End If
            Else
                MensajeError(strErr)
                Exit Function
            End If

            'grid personalidad jurídica
            Dim objPJ As New SNProcotiza.clsTiposOperacion

            objPJ.IDTipoOperacion = Val(Request("tipOpId"))
            dtsRes = objPJ.ManejaTipoOperacion(10)

            If Trim$(objPJ.ErrorTipoOperacion) = "" Then
                gdvPerJur.DataSource = dtsRes
                gdvPerJur.DataBind()
                gdvPerJur.Columns(3).Visible = False
            Else
                MensajeError(objPJ.ErrorTipoOperacion)
                Exit Function
            End If

            CargaCombos = True
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Function

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        Try
            strPersonas = ""
            If ValidaCampos() Then
                Dim intTipOp As Integer = Val(Request("tipOpId"))
                Dim objTO As New clsTiposOperacion
                Dim intOpc As Integer = 2

                objTO.CargaSession(Val(Session("cveAcceso")))

                If intTipOp > 0 Then
                    objTO.CargaTipoOperacion(intTipOp)
                    intOpc = 3
                End If

                'guardamos la info del tipo de operación
                objTO.IDTipoEsquemaFinanciamiento = Val(cmbTipEsq.SelectedValue)
                objTO.Nombre = Trim(txtNom.Text)
                objTO.Clave = Trim(txtClave.Text)
                objTO.IDEstatus = Val(cmbEstatus.SelectedValue)
                objTO.PideValorResidual = IIf(chkValResid.Checked, 1, 0)
                objTO.PideOpcionCompra = IIf(chkOpcComp.Checked, 1, 0)
                objTO.PideDepositoGarantia = IIf(chkDepGar.Checked, 1, 0)
                objTO.CobraGastosExtra = IIf(chkGtosExt.Checked, 1, 0)
                objTO.IDTipoGastoExtra = IIf(chkGtosExt.Checked, Val(cmbGtoExt.SelectedValue), 0)
                objTO.CapitalFinaciarIncluyeIVA = IIf(chkCapIncIva.Checked, 1, 0)
                objTO.PermitecalculoPagoInicial = IIf(chkPagoIni.Checked, 1, 0)
                objTO.IDTipoFormulaPagoInicial = IIf(chkPagoIni.Checked, Val(cmbForPagIni.SelectedValue), 0)
                objTO.LeyendaDepositoGarantia = Trim(txtLeyDepGar.Text)
                objTO.LeyendaValorResidual = Trim(txtLeyValRes.Text)
                objTO.UsuarioRegistro = objTO.UserNameAcceso

                objTO.ManejaTipoOperacion(intOpc)
                If objTO.ErrorTipoOperacion = "" Then
                    If GuardaCobroIVA(objTO.IDTipoOperacion) Then
                        CierraPantalla("./consultaTiposOperacion.aspx")
                    End If
                Else
                    MensajeError(objTO.ErrorTipoOperacion)
                End If
            Else
                MensajeError("Todos los campos marcados con * son obligatorios.")
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If cmbTipEsq.SelectedValue = "" Then Exit Function
        If cmbEstatus.SelectedValue = "" Then Exit Function
        If Trim(txtNom.Text) = "" Then Exit Function
        If Trim(txtClave.Text) = "" Then Exit Function

        If chkValResid.Checked Then
            If Trim(txtLeyValRes.Text) = "" Then Exit Function
        End If

        If chkDepGar.Checked Then
            If Trim(txtLeyDepGar.Text) = "" Then Exit Function
        End If

        If chkGtosExt.Checked Then
            If cmbGtoExt.SelectedValue = "" Then Exit Function
        End If

        If chkPagoIni.Checked Then
            If cmbForPagIni.SelectedValue = "" Then Exit Function
        End If

        ValidaCampos = True
    End Function

    Private Sub HabilitaDeshabilita(ByVal sngOpc As Single)
        Try
            Select Case sngOpc
                Case 1 'valor residual
                    If chkValResid.Checked Then
                        txtLeyValRes.Enabled = True
                    Else
                        txtLeyValRes.Text = ""
                        txtLeyValRes.Enabled = False
                    End If
                Case 2 'deposito en garantia
                    If chkDepGar.Checked Then
                        txtLeyDepGar.Enabled = True
                    Else
                        txtLeyDepGar.Text = ""
                        txtLeyDepGar.Enabled = False
                    End If
                Case 3 'gastos
                    If chkGtosExt.Checked Then
                        cmbGtoExt.Enabled = True
                    Else
                        cmbGtoExt.SelectedValue = ""
                        cmbGtoExt.Enabled = False
                    End If
                Case 4 'pagoinicial
                    If chkPagoIni.Checked Then
                        cmbForPagIni.Enabled = True
                    Else
                        cmbForPagIni.SelectedValue = ""
                        cmbForPagIni.Enabled = False
                    End If
            End Select
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Function GuardaCobroIVA(ByVal intCveTO As Integer) As Boolean
        GuardaCobroIVA = False
        Try
            Dim objRow As GridViewRow
            Dim objChk As CheckBox
            Dim objTO As New SNProcotiza.clsTiposOperacion(intCveTO)

            'primero borramos la información que ya existe
            objTO.ManejaTipoOperacion(8)
            If objTO.ErrorTipoOperacion <> "" Then
                MensajeError(objTO.ErrorTipoOperacion)
            Else
                'guardamos las personalidades
                For Each objRow In gdvPerJur.Rows
                    If objRow.RowType = DataControlRowType.DataRow Then
                        objTO.IDPersonalidadJuridica = Val(objRow.Cells(3).Text)

                        'iva sobre interés
                        objChk = objRow.Cells(1).Controls(1)
                        objTO.CobraIVASobreInteres = IIf(objChk.Checked, 1, 0)

                        'iva sobre capital
                        objChk = objRow.Cells(2).Controls(1)
                        objTO.CobraIVASobreCapital = IIf(objChk.Checked, 1, 0)

                        objTO.ManejaTipoOperacion(9)
                        If objTO.ErrorTipoOperacion <> "" Then
                            MensajeError(objTO.ErrorTipoOperacion)
                            Exit Function
                        End If
                    End If
                Next

                GuardaCobroIVA = True
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Function

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaTiposOperacion.aspx")
    End Sub

    Protected Sub chkGtosExt_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkGtosExt.CheckedChanged
        HabilitaDeshabilita(3)
    End Sub

    Protected Sub chkDepGar_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkDepGar.CheckedChanged
        HabilitaDeshabilita(2)
    End Sub

    Protected Sub chkValResid_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkValResid.CheckedChanged
        HabilitaDeshabilita(1)
    End Sub

    Protected Sub chkPagoIni_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkPagoIni.CheckedChanged
        HabilitaDeshabilita(4)
    End Sub

    Protected Sub gdvPerJur_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvPerJur.RowDataBound
        Try
            If Val(Request("tipOpId")) > 0 Then
                If e.Row.RowType = DataControlRowType.DataRow Then
                    Dim intPerJur = Val(e.Row.Cells(3).Text)
                    Dim objTO As New SNProcotiza.clsTiposOperacion
                    Dim objChk As New CheckBox

                    objTO.IDTipoOperacion = Val(Request("tipOpId"))
                    objTO.IDPersonalidadJuridica = intPerJur
                    Dim dtsRes As DataSet = objTO.ManejaTipoOperacion(10)

                    If Trim(objTO.ErrorTipoOperacion) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                objChk = e.Row.Cells(1).Controls(1)
                                If dtsRes.Tables(0).Rows(0).Item("IVA_SOBRE_INTERES") = 1 Then
                                    objChk.Checked = True
                                End If

                                objChk = e.Row.Cells(2).Controls(1)
                                If dtsRes.Tables(0).Rows(0).Item("IVA_SOBRE_CAPITAL") = 1 Then
                                    objChk.Checked = True
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
