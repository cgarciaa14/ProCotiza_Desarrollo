'BBV-P-412:AVH:21/07/2016 RQ20: SE CREA VENTANA DE COMISIONES
'BBV-P-412:AVH:02/08/2016 RQ20.2 OPCION BonoCC
'BBV-P-412:BUG-PC-19 24/11/2016 Se reparo error al eliminar montos de comision
'BBV-P-412:BUG-PC-35 18/01/2017 JRHM Se agrega objeto para solucion de fallas
'BUG-PC-42 JRHM 30/01/17 Se agregan validaciones para la inserccion de # de comisiones y montos en comisiones
Imports System.Data
Imports SNProcotiza
Partial Class aspx_manejaComisiones
    Inherits System.Web.UI.Page
    Dim objAgencias As New clsAgencias
    Dim strErr As String = String.Empty

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()

        If Not IsPostBack Then
            CargaCombos(1)
            CargaCombos(2)
            CargaCombos(3)
            'SetInitialRow()
            llenaGRV()
        End If
        
    End Sub
    Public Sub LimpiaError()
        lblMensaje.Text = String.Empty
        'lblScript.Text = String.Empty
    End Sub
    Public Sub MensajeError(ByVal strMsj As String)
        strMsj = Replace(strMsj, "'", "")
        strMsj = Replace(strMsj, """", "")
        strMsj = Replace(strMsj, "(", "")
        strMsj = Replace(strMsj, ")", "")
        lblMensaje.Text = "<script>alert('" & strMsj & "')</script>"
    End Sub
    Public Sub CierraPantalla(ByVal strPant As String)
        lblMensaje.Text += "<script>alert('El registro se guardó con éxito'); </script>"
    End Sub
    Private Function CargaCombos(ByVal intOpc As Integer) As Boolean

        Dim objAlianzas As New clsAlianzas
        Dim objGrupos As New clsGrupos
        Dim objDivisiones As New clsDivisiones
        Dim objCombo As New clsProcGenerales
        Dim dtsRes As New DataSet

        Try
            CargaCombos = False

            Select Case intOpc
                Case 1
                    dtsRes = objAlianzas.ManejaAlianza(1)
                    strErr = objAlianzas.ErrorAlianza
                    If Trim$(strErr) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                objCombo.LlenaCombos(dtsRes, "ALIANZA", "ID_ALIANZA", cmbAlianza, strErr, False)
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

                Case 2
                    dtsRes = objGrupos.ManejaGrupo(1)
                    strErr = objGrupos.ErrorGrupo
                    If Trim$(strErr) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                objCombo.LlenaCombos(dtsRes, "GRUPO", "ID_GRUPO", cmbGrupo, strErr, False)
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

                Case 3
                    dtsRes = objDivisiones.ManejaDivision(1)
                    strErr = objDivisiones.ErrorDivision
                    If Trim$(strErr) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                objCombo.LlenaCombos(dtsRes, "DIVISION", "ID_DIVISION", cmbDivision, strErr, False)
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
            End Select

            CargaCombos = True
        Catch ex As Exception
            CargaCombos = False
            MensajeError(ex.Message)
        End Try
    End Function

    Protected Sub rbAlianza_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbAlianza.CheckedChanged
        Me.cmbAlianza.Enabled = True
        Me.cmbGrupo.Enabled = False
        Me.cmbDivision.Enabled = False
        Me.cmbGrupo.SelectedValue = 0
        Me.cmbDivision.SelectedValue = 0
        ViewState("CurrentTable") = Nothing
        llenaGRV()
    End Sub

    Protected Sub rbGrupo_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbGrupo.CheckedChanged
        Me.cmbAlianza.Enabled = False
        Me.cmbGrupo.Enabled = True
        Me.cmbDivision.Enabled = False
        Me.cmbAlianza.SelectedValue = 0
        Me.cmbDivision.SelectedValue = 0
        ViewState("CurrentTable") = Nothing
        llenaGRV()
    End Sub

    Protected Sub rbDivision_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbDivision.CheckedChanged
        Me.cmbAlianza.Enabled = False
        Me.cmbGrupo.Enabled = False
        Me.cmbDivision.Enabled = True
        Me.cmbAlianza.SelectedValue = 0
        Me.cmbGrupo.SelectedValue = 0
        ViewState("CurrentTable") = Nothing
        llenaGRV()
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        Dim cred As TextBox
        Dim bono As TextBox
        Dim totreg As Integer = grvBonoCC.Rows.Count - 1

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then

            If Not ValidaCampos() Then
                MensajeError(strErr)
                Exit Sub
            End If



            For Each row As GridViewRow In grvBonoCC.Rows
                Dim ndx As Integer = row.RowIndex
                cred = TryCast(row.FindControl("txtImporte"), TextBox)
                bono = TryCast(row.FindControl("txtNoCreditos"), TextBox)

                If totreg = ndx Then
                    If cred.Text <> "" Or bono.Text <> "" Then
                        MensajeError("Existe un registro de 'Bono por créditos colocados' sin agregar.")
                        Exit Sub
                    End If
                End If
            Next


            Dim objComisionesAgen As New clsAgencias
            Dim dtsRes As New DataSet
            Dim Alianza As Integer = -1
            Dim Grupo As Integer = -1
            Dim Division As Integer = -1

            If rbAlianza.Checked = True Then
                Alianza = cmbAlianza.SelectedValue
                Me.cmbGrupo.SelectedValue = 0
                Me.cmbDivision.SelectedValue = 0
            ElseIf rbGrupo.Checked = True Then
                Grupo = cmbGrupo.SelectedValue
                Me.cmbAlianza.SelectedValue = 0
                Me.cmbDivision.SelectedValue = 0
            ElseIf rbDivision.Checked = True Then
                Division = cmbDivision.SelectedValue
                Me.cmbAlianza.SelectedValue = 0
                Me.cmbGrupo.SelectedValue = 0
            End If

            If String.IsNullOrEmpty(txtUDIS.Text) Then
                txtUDIS.Text = 0
            End If
            If String.IsNullOrEmpty(txtPagoFI.Text) Then
                txtPagoFI.Text = 0
            End If
            If String.IsNullOrEmpty(txtDIvidendos.Text) Then
                txtDIvidendos.Text = 0
            End If
            If String.IsNullOrEmpty(txtRegalias.Text) Then
                txtRegalias.Text = 0
            End If
            If String.IsNullOrEmpty(txtBonoEspVen.Text) Then
                txtBonoEspVen.Text = 0
            End If
            If String.IsNullOrEmpty(txtSeguroRegalado.Text) Then
                txtSeguroRegalado.Text = 0
            End If

            objComisionesAgen.CargaSession(Val(Session("cveAcceso")))

            objComisionesAgen.IDAlianza = Alianza
            objComisionesAgen.IDGrupo = Grupo
            objComisionesAgen.IDDivision = Division
            objComisionesAgen.PrcUdis = CDbl(txtUDIS.Text)
            objComisionesAgen.PagoFYI = CDbl(txtPagoFI.Text)
            objComisionesAgen.Dividendos = CDbl(txtDIvidendos.Text)
            objComisionesAgen.Regalias = CDbl(txtRegalias.Text)
            objComisionesAgen.BonoVendedor = CDbl(txtBonoEspVen.Text)
            objComisionesAgen.ComiSeguro = CDbl(txtSeguroRegalado.Text)
            objComisionesAgen.UsuarioRegistro = objComisionesAgen.UserNameAcceso

            dtsRes = objComisionesAgen.ManejaAgencia(25)

            GuardaBonoCC(Alianza, Grupo, Division)

            LimpiaDatos()

            If objComisionesAgen.ErrorAgencia = "" Then
                CierraPantalla("")
            Else
                MensajeError(objComisionesAgen.ErrorAgencia)
            End If
        End If
    End Sub

    Public Sub LimpiaDatos()
        Me.cmbAlianza.SelectedValue = 0
        Me.cmbGrupo.SelectedValue = 0
        Me.cmbDivision.SelectedValue = 0

        Me.txtUDIS.Text = ""
        Me.txtPagoFI.Text = ""
        Me.txtDIvidendos.Text = ""
        Me.txtRegalias.Text = ""
        Me.txtBonoEspVen.Text = ""
        Me.txtSeguroRegalado.Text = ""
        'Me.txtBonoCreCol.Text = ""

        llenaGRV()
    End Sub
    Private Sub SetInitialRow()

        Dim dt As New DataTable()
        Dim dr As DataRow = Nothing

        dt.Columns.Add(New DataColumn("RowNumber", GetType(String)))
        dt.Columns.Add(New DataColumn("Column1", GetType(String)))
        'for DropDownList selected item
        dt.Columns.Add(New DataColumn("Column2", GetType(String)))
        'for DropDownList selected item   
        dt.Columns.Add(New DataColumn("Column3", GetType(String)))
        'for TextBox value   
        dr = dt.NewRow()
        dr("RowNumber") = 1
        dr("Column1") = String.Empty
        dt.Rows.Add(dr)

        'Store the DataTable in ViewState for future reference   
        ViewState("CurrentTable") = dt

        'Bind the Gridview   
        grvBonoCC.DataSource = dt
        grvBonoCC.DataBind()

    End Sub
    Protected Sub ImageButton1_Click(sender As Object, e As EventArgs)
        ''Protected Sub LinkButton1_Click(sender As Object, e As EventArgs)

        Dim lb As ImageButton = DirectCast(sender, ImageButton)
        Dim gvRow As GridViewRow = DirectCast(lb.NamingContainer, GridViewRow)
        Dim rowID As Integer = gvRow.RowIndex
        Dim Filtro As String() = Split(sender.CommandArgument.ToString(), "|")

        Dim Alianza As Integer = -1
        Dim Grupo As Integer = -1
        Dim Division As Integer = -1
        Dim objBonoCC As New clsAgencias

        If rbAlianza.Checked = True Then
            Alianza = cmbAlianza.SelectedValue
            Me.cmbGrupo.SelectedValue = 0
            Me.cmbDivision.SelectedValue = 0
        ElseIf rbGrupo.Checked = True Then
            Grupo = cmbGrupo.SelectedValue
            Me.cmbAlianza.SelectedValue = 0
            Me.cmbDivision.SelectedValue = 0
        ElseIf rbDivision.Checked = True Then
            Division = cmbDivision.SelectedValue
            Me.cmbAlianza.SelectedValue = 0
            Me.cmbGrupo.SelectedValue = 0
        End If

        If ViewState("CurrentTable") IsNot Nothing Then

            Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            If dt.Rows.Count > 1 Then
                If gvRow.RowIndex < dt.Rows.Count - 1 Then
                    'Remove the Selected Row data and reset row number  
                    dt.Rows.Remove(dt.Rows(rowID))
                    ResetRowID(dt)
                End If
            End If


            ViewState("CurrentTable") = dt

            objBonoCC.CargaSession(Val(Session("cveAcceso")))

            grvBonoCC.DataSource = dt
            grvBonoCC.DataBind()

            objBonoCC.IDAlianza = Alianza
            objBonoCC.IDGrupo = Grupo
            objBonoCC.IDDivision = Division
            If Filtro(0) <> "" Then
            objBonoCC.NoCreditos = Filtro(0)
            objBonoCC.Importe = Filtro(1)
            
            objBonoCC.UsuarioRegistro = objBonoCC.UserNameAcceso

            objBonoCC.ManejaAgencia(29)
            End If
            
        End If

        'Set Previous Data on Postbacks  
        SetPreviousData()
        
    End Sub
    Private Sub ResetRowID(dt As DataTable)
        Dim rowNumber As Integer = 1
        If dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                row(0) = rowNumber
                rowNumber += 1
            Next
        End If
    End Sub
    Private Sub SetPreviousData()

        Dim rowIndex As Integer = 0
        If ViewState("CurrentTable") IsNot Nothing Then

            Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            If dt.Rows.Count > 0 Then

                For i As Integer = 0 To dt.Rows.Count - 1

                    Dim box1 As TextBox = DirectCast(grvBonoCC.Rows(i).Cells(3).FindControl("TextBox1"), TextBox)
                    Dim campo1 As TextBox = DirectCast(grvBonoCC.Rows(i).Cells(1).FindControl("txtNoCreditos"), TextBox)
                    Dim campo2 As TextBox = DirectCast(grvBonoCC.Rows(i).Cells(2).FindControl("txtImporte"), TextBox)



                    If i < dt.Rows.Count - 1 Then

                        campo1.Text = dt.Rows(i)("Column1").ToString()
                        campo1.Enabled = False

                        campo2.Text = dt.Rows(i)("Column2").ToString()
                        campo2.Enabled = False

                        'Assign the value from DataTable to the TextBox   
                        box1.Text = dt.Rows(i)("Column3").ToString()
                        box1.Enabled = False



                    End If

                    rowIndex += 1
                Next
            End If
        End If
    End Sub
    Protected Sub cmdAgregaAcc_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles cmdAgregaAcc.Click

        For Each row As GridViewRow In grvBonoCC.Rows

            Dim Importe As TextBox = TryCast(row.FindControl("txtImporte"), TextBox)
            Dim NoCreditos As TextBox = TryCast(row.FindControl("txtNoCreditos"), TextBox)
            Dim ndx As Integer = row.RowIndex

            If Not ValidaCreditos(NoCreditos, Importe, ndx) Then
                MensajeError(strErr)
                Exit Sub
            End If
        Next

            AddNewRowToGrid()
    End Sub
    Protected Sub ButtonAdd_Click(sender As Object, e As EventArgs)
        AddNewRowToGrid()
    End Sub
    Private Sub AddNewRowToGrid()

        If ViewState("CurrentTable") IsNot Nothing Then

            Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            Dim drCurrentRow As DataRow = Nothing

            If dtCurrentTable.Rows.Count > 0 Then
                drCurrentRow = dtCurrentTable.NewRow()
                drCurrentRow("RowNumber") = dtCurrentTable.Rows.Count + 1

                'add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow)

                'Store the current data to ViewState for future reference   
                ViewState("CurrentTable") = dtCurrentTable


                For i As Integer = 0 To dtCurrentTable.Rows.Count - 2

                    'extract the TextBox values   

                    Dim box1 As TextBox = DirectCast(grvBonoCC.Rows(i).Cells(3).FindControl("TextBox1"), TextBox)

                    dtCurrentTable.Rows(i)("Column3") = box1.Text

                    'extract the DropDownList Selected Items   
                    Dim campo1 As TextBox = DirectCast(grvBonoCC.Rows(i).Cells(1).FindControl("txtNoCreditos"), TextBox)
                    Dim campo2 As TextBox = DirectCast(grvBonoCC.Rows(i).Cells(2).FindControl("txtImporte"), TextBox)

                    ' Update the DataRow with the DDL Selected Items   

                    dtCurrentTable.Rows(i)("Column1") = Val(campo1.Text)

                    dtCurrentTable.Rows(i)("Column2") = Val(campo2.Text)
                Next

                'Rebind the Grid with the current data to reflect changes   
                grvBonoCC.DataSource = dtCurrentTable
                grvBonoCC.DataBind()
            End If
        Else

            Response.Write("ViewState is null")
        End If
        'Set Previous Data on Postbacks   
        SetPreviousData()
    End Sub
    Protected Sub grvBonoCC_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvBonoCC.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            Dim imgbtn As ImageButton = DirectCast(e.Row.FindControl("ImageButton1"), ImageButton)
            If imgbtn IsNot Nothing Then
                If dt.Rows.Count > 1 Then
                    If e.Row.RowIndex = dt.Rows.Count - 1 Then
                        imgbtn.Visible = False
                    End If
                Else
                    imgbtn.Visible = False
                End If
            End If
        End If
    End Sub
    Private Function GuardaBonoCC(Alianza As Integer, Grupo As Integer, Division As Integer) As Boolean
        Try
            Dim strError As String = ""
            GuardaBonoCC = False

            Dim objBonoCC As New clsAgencias
            Dim dtsRes As New DataSet
            Dim objRow As GridViewRow

            Dim objTxt As Object
            Dim objNoCreditos As Object
            Dim objImporte As Object

            objBonoCC.CargaSession(Val(Session("cveAcceso")))


            For Each objRow In grvBonoCC.Rows
                objTxt = objRow.Cells(3).Controls(1)
                objNoCreditos = objRow.Cells(1).Controls(1)
                objImporte = objRow.Cells(2).Controls(1)

                If Trim$(objNoCreditos.Text) <> "" And Trim$(objImporte.Text) <> "" Then
                    objBonoCC.IDAlianza = Alianza
                    objBonoCC.IDGrupo = Grupo
                    objBonoCC.IDDivision = Division
                    objBonoCC.NoCreditos = objNoCreditos.Text
                    objBonoCC.Importe = objImporte.Text
                    objBonoCC.IDEstatus = 2
                    objBonoCC.UsuarioRegistro = objBonoCC.UserNameAcceso

                    objBonoCC.ManejaAgencia(27)
                End If
            Next

            GuardaBonoCC = True
            ViewState("CurrentTable") = Nothing
            llenaGRV()
        Catch ex As Exception
            strErr = ex.Message
            GuardaBonoCC = False
        End Try
    End Function
    Private Sub llenaGRV()
        Dim Alianza As Integer = -1
        Dim Grupo As Integer = -1
        Dim Division As Integer = -1

        If rbAlianza.Checked = True Then
            Alianza = cmbAlianza.SelectedValue
            Me.cmbGrupo.SelectedValue = 0
            Me.cmbDivision.SelectedValue = 0
        ElseIf rbGrupo.Checked = True Then
            Grupo = cmbGrupo.SelectedValue
            Me.cmbAlianza.SelectedValue = 0
            Me.cmbDivision.SelectedValue = 0
        ElseIf rbDivision.Checked = True Then
            Division = cmbDivision.SelectedValue
            Me.cmbAlianza.SelectedValue = 0
            Me.cmbGrupo.SelectedValue = 0
        End If

        'Alianza = 0
        Dim objBonoCC As New clsAgencias
        Dim dsBonoCC As DataSet

        objBonoCC.IDAlianza = Alianza
        objBonoCC.IDGrupo = Grupo
        objBonoCC.IDDivision = Division


        dsBonoCC = objBonoCC.ManejaAgencia(28)


        If dsBonoCC.Tables(0).Rows.Count > 0 Then
            ViewState("CurrentTable") = dsBonoCC.Tables(0)
            grvBonoCC.DataSource = ViewState("CurrentTable")
            grvBonoCC.DataBind()
            AddNewRowToGrid()

        Else
            SetInitialRow()

        End If

    End Sub

    Protected Sub cmbAlianza_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbAlianza.SelectedIndexChanged
        ViewState("CurrentTable") = Nothing
        llenaGRV()
    End Sub

    Protected Sub cmbGrupo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbGrupo.SelectedIndexChanged
        ViewState("CurrentTable") = Nothing
        llenaGRV()
    End Sub

    Protected Sub cmbDivision_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbDivision.SelectedIndexChanged
        ViewState("CurrentTable") = Nothing
        llenaGRV()
    End Sub

    Private Function ValidaCampos() As Boolean

        ValidaCampos = False

        'Validación de UDIS
        If Trim(txtUDIS.Text) <> "" Then
            If IsNumeric(txtUDIS.Text) Then
                If CInt(txtUDIS.Text) < 0 Or CInt(txtUDIS.Text) > 100 Then
                    strErr = "'% Udis' introducido no valido."
                    Exit Function
                End If
            Else
                strErr = "Formato de Udis no es válido."
                txtUDIS.Text = ""
                Exit Function
            End If
        End If

        'Validación de Pago al F&I
        If Trim(txtPagoFI.Text) <> "" Then
            If IsNumeric(txtPagoFI.Text) Then
            Else
                strErr = "Formato de Pago al F&I no es válido."
                txtPagoFI.Text = ""
                Exit Function
            End If
        End If

        'Validación de Dividendos
        If Trim(txtDIvidendos.Text) <> "" Then
            If IsNumeric(txtDIvidendos.Text) Then
            Else
                strErr = "Formato de Dividendos no es válido."
                txtDIvidendos.Text = ""
                Exit Function
            End If
        End If

        'Validación de Dividendos
        If Trim(txtRegalias.Text) <> "" Then
            If IsNumeric(txtRegalias.Text) Then
            Else
                strErr = "Formato de Regalias no es válido."
                txtRegalias.Text = ""
                Exit Function
            End If
        End If

        'Validación de Bono Especial Vendedor
        If Trim(txtBonoEspVen.Text) <> "" Then
            If IsNumeric(txtBonoEspVen.Text) Then
            Else
                strErr = "Formato de Bono Especial Vendedor no es válido."
                txtBonoEspVen.Text = ""
                Exit Function
            End If
        End If

        'Validación de Seguro Regalado
        If Trim(txtSeguroRegalado.Text) <> "" Then
            If IsNumeric(txtSeguroRegalado.Text) Then
            Else
                strErr = "Formato de Seguro Regalado no es válido."
                txtSeguroRegalado.Text = ""
                Exit Function
            End If
        End If

        ValidaCampos = True

        Return ValidaCampos

    End Function

    Private Function ValidaCreditos(ByVal nocreditos As TextBox, ByVal monto As TextBox, index As Integer) As Boolean
        ValidaCreditos = False
        Dim repetidos As Integer = 0
        LimpiaError()

        If nocreditos.Text = "" And monto.Text = "" Then
            strErr = "Debe ingresar # Créditos Colocados e Importe de Bono."
            Exit Function
        End If

        If nocreditos.Text = "" Then
            strErr = "Debe ingresar número de Creditos."
            Exit Function
        End If

        If monto.Text = "" Then
            strErr = "Debe ingresar Importe del Bono."
            Exit Function
        End If


        If Not IsNumeric(monto.Text) Then
            strErr = "Formato incorrecto de Importe de Bono."
            Exit Function
        Else
            If Val(monto.Text.Trim) = 0 Then
                strErr = "El importe del Bono no puede ser $0.00."
                Exit Function
            End If
        End If

        'se evalua que el valor de los creditos colocados sea diferente de los que ya existen

        For Each subrow As GridViewRow In grvBonoCC.Rows
            If index <> subrow.RowIndex Then
                Dim Creditos As TextBox = TryCast(subrow.FindControl("txtNoCreditos"), TextBox)

                If nocreditos.Text = Creditos.Text Then
                    repetidos = repetidos + 1
                    Exit For
                End If
            End If
        Next


        If repetidos > 0 Then
            strErr = "No se puede repetir valor para # Créditos Colocados"
            Exit Function
        End If

        ValidaCreditos = True
        Return ValidaCreditos
    End Function
End Class
