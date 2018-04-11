'BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.
'BBVA-P-412: 23/08/2016: AVH: RQ11 Se agrega Grid de Porcentajes Adicionales
'BUG-PC-09:AMR:16/11/2016 CUTARMA01:No pertime espacios en blanco al ingresar el nombre de la marca
'BUG-PC-13: AMR: 23/11/2016 Se quita valor por default en los combos.
'BUG-PC-24 14/12/2016 MAUT Se agrega Try Catch y mensaje de guardado
'BUG-PC-39 JRHM 25/01/17 Correccion de errores multiples
'BUG-PC-44 07/02/17 JRHM SE CAMBIA EL ORDEN DE EJECUCION DE METODOS EN PAGE LOAD
'BUG-PC-46 09/02/17 JRHM SE CORRIGE GRID DE COMPRA INTELIGENTE 
'BUG-PC-48 15/02/17 JRHM  SE MODIFICA FUNCIONAMIENTO BOTON DE ELIMINACION DE REGISTROS EN COMPRA INTELIGENTE
'BUG-PC-50 11/04/17 JRHM SE CORRIGE FUNCIONALIDAD DEL BOTON DE AGREGAR VALORES DE COMPRA INTELIGENTE PARA PROHIBIR QUE LA SUMA DE PORCENTAJE SEA SUPERIOR A 100%, QUE NO HAYA PLAZO Y ENGANCHE IGUAL Y CORRECCION A LA ELIMINACION DE PLAZOS

Imports System.Data
Imports SNProcotiza

Partial Class aspx_manejaVersiones
    Inherits System.Web.UI.Page
    Dim objCombo As New clsProcGenerales

    Dim strErr As String = ""
    Dim band As Integer = 0


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()
        'BUG-PC-09
        If Not IsPostBack Then
            llenaGRV()
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
            If Val(Request("idVersion")) > 0 Then
                CargaInfo()
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

    'Public WriteOnly Property Script() As String
    '    Set(ByVal value As String)
    '        lblScript.Text = "<script>" & value & " </script>"
    '    End Set
    'End Property

    Public Sub CierraPantalla(ByVal strPant As String)
        lblMensaje.Text += "<script>alert('El registro se guardó con éxito'); document.location.href='" & strPant & "';</script>"
    End Sub

    Private Function CargaCombos(opc As Integer) As Boolean
        Dim objCombo As New clsProcGenerales
        Dim objMarcas As New SNProcotiza.clsMarcas
        Dim objSubMarcas As New SNProcotiza.clsSubMarcas
        Dim objPlazos As New SNProcotiza.clsPlazo
        Dim dtsRes As New DataSet
        Dim objVersiones As New SNProcotiza.clsVersiones
        Try
            CargaCombos = False

            Select Case opc
                Case 1
                    'Combo Estatus
                    dtsRes = objCombo.ObtenInfoParametros(1, strErr)
                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", ddlestatus, strErr, False)
                        If Trim$(strErr) <> "" Then
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                Case 2
                    'Combo Marcas
                    'BUG-PC-09
                    objMarcas.IDEstatus = 2
                    dtsRes = objMarcas.ManejaMarca(1)

                    If objMarcas.ErrorMarcas = "" Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_MARCA", ddlmarca, strErr, False, True, "REG_DEFAULT")
                            If strErr <> "" Then
                                Exit Function
                            End If
                            If Val(Request("idVersion")) <> 0 Then
                                Dim objversion As SNProcotiza.clsVersiones = New clsVersiones()
                                objversion.CargaVersion(Val(Request("idVersion")))
                                ddlmarca.SelectedValue = objversion.IDMarca
                    Else
                                ddlmarca.SelectedValue = Val(Request("idMarca"))
                            End If
                        Else
                            strErr = "No se encontro información de la Marca."
                            Exit Function
                        End If
                    Else
                        strErr = objMarcas.ErrorMarcas
                        Exit Function
                    End If
                Case 3
                    'Combo SubMarcas
                    objSubMarcas.IDEstatus = 2

                    If ddlmarca.SelectedValue > 0 Then
                        objSubMarcas.IDMarca = ddlmarca.SelectedValue
                    End If

                    dtsRes = objSubMarcas.ManejaSubMarca(1)

                    If objSubMarcas.ErrorSubMarcas = "" Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            ''ddlsubmarca.Items.Clear()
                            objCombo.LlenaCombos(dtsRes, "DESCRIPCION", "ID_SUBMARCA", ddlsubmarca, strErr, False, True, "REG_DEFAULT")
                            If strErr <> "" Then
                                MensajeError(strErr)
                                Exit Function
                            End If

                            If band = 0 Then
                                ddlsubmarca.SelectedValue = Val(Request("idSubmarca"))
                        End If


                    Else
                            strErr = "No se encontro información de la Submarca."
                            Exit Function
                        End If
                    Else
                        strErr = objMarcas.ErrorMarcas
                        Exit Function
                    End If
                Case 4
                    'Combo Plazos
                    dtsRes = objPlazos.ManejaPlazos(1)

                    If objPlazos.StrErrPlazo = "" Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            objCombo.LlenaCombos(dtsRes, "DESCRIPCION", "ID_PLAZO", ddlmarca, strErr, False)
                            If strErr <> "" Then
                                Exit Function
                            End If
                        End If
                    Else
                        strErr = objPlazos.StrErrPlazo
                        Exit Function
                    End If
            End Select

            CargaCombos = True
        Catch ex As Exception
            CargaCombos = False
            strErr = ex.Message
        End Try
    End Function

    Private Sub CargaInfo()
        Try
            Dim intVersion As Integer = Val(Request("idVersion"))
            Dim objVersion As New SNProcotiza.clsVersiones(intVersion)

            If objVersion.ErrorVersion = "" Then
                lblidversion.Text = objVersion.IDVersion
                ddlmarca.SelectedValue = objVersion.IDMarca
                ''CargaCombos(3)
                ddlsubmarca.SelectedValue = objVersion.IDSubMarca
                txtversion.Text = objVersion.Descripcion
                ddlestatus.SelectedValue = objVersion.IDEstatus
                txtidexterno.Text = objVersion.IDExterno
                chkdefault.Checked = IIf(objVersion.RegDefault = 1, True, False)
            Else
                MensajeError(objVersion.ErrorVersion)
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If ddlmarca.SelectedValue = "" Then Exit Function
        If ddlsubmarca.SelectedValue = "" Then Exit Function
        If ddlestatus.SelectedValue = "" Then Exit Function
        If txtversion.Text.Trim = "" Then Exit Function

        ValidaCampos = True

    End Function

    Protected Sub ddlmarca_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlmarca.SelectedIndexChanged
        band = 1
        If Not CargaCombos(3) Then
            MensajeError(strErr)
            ddlsubmarca.Items.Clear()
            Exit Sub
        End If
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaVersiones.aspx")
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then

                ' Se agrega revalidacion de campos al guradar en caso de que de que los campos de tipo plazo o enganche o balloon esten llenos 
                Dim totreg As Integer = grvAdicionales.Rows.Count - 1
                For Each row As GridViewRow In grvAdicionales.Rows
                    Dim ndx As Integer = row.RowIndex
                    Dim ddlpla As DropDownList = TryCast(row.FindControl("DropDownList1"), DropDownList)
                    Dim boxEnganche As TextBox = TryCast(row.FindControl("txtEnganche"), TextBox)
                    Dim boxBalloon As TextBox = TryCast(row.FindControl("txtBalloon"), TextBox)
                    If totreg = ndx Then
                        If ddlpla.SelectedValue <> 0 Or boxEnganche.Text <> "" Or boxBalloon.Text <> "" Then
                        If ddlpla.SelectedValue = 0 Or boxEnganche.Text = "" Or boxBalloon.Text = "" Then
                            MensajeError("Todos los campos marcados con * son obligatorios.")
                            Exit Sub
                            End If

                        End If
                    End If
                Next

                Dim dts As New DataSet()

                Dim objVersiones As New SNProcotiza.clsVersiones
                Dim intOpc As Integer = 2
                Dim strError As String = ""

                objVersiones.CargaSession(Val(Session("cveAcceso")))

                If Val(Request("idVersion")) > 0 Then
                    objVersiones.CargaVersion(Val(Request("idVersion")))
                    intOpc = 3
                End If

                objVersiones.IDMarca = ddlmarca.SelectedValue
                objVersiones.IDSubMarca = ddlsubmarca.SelectedValue
                objVersiones.Descripcion = txtversion.Text.Trim

                If txtidexterno.Text.Trim.Length > 0 Then
                    objVersiones.IDExterno = txtidexterno.Text.Trim
                End If

                objVersiones.RegDefault = IIf(chkdefault.Checked = True, 1, 0)
                objVersiones.IDEstatus = ddlestatus.SelectedValue
                objVersiones.UsuReg = objVersiones.UserNameAcceso
                dts = objVersiones.ManejaVersion(intOpc)

                If dts.Tables(0).Rows(0).Item("ID_VERSION") = -1 Then
                    MensajeError("El nombre de versión para la Marca y SubMarca seleccionadas ya existe.")
                    Exit Sub
                End If

                GuardaAdicionales(objVersiones.IDVersion, strError)



                If objVersiones.ErrorVersion = "" And strError = "" Then
                    CierraPantalla("./consultaVersiones.aspx")
                Else
                    If strError <> "" Then
                        MensajeError(strError)
                        ViewState("CurrentTable") = Nothing
                        llenaGRV()
                    Else
                        MensajeError(objVersiones.ErrorVersion)
                    End If
                    Exit Sub
                End If

            Else
                MensajeError("Todos los campos marcados con * son obligatorios.")
                Exit Sub
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
            Exit Sub
        End Try

    End Sub
    Private Sub SetInitialRow()

        Dim dt As New DataTable()
        Dim dr As DataRow = Nothing

        dt.Columns.Add(New DataColumn("RowNumber", GetType(String)))
        dt.Columns.Add(New DataColumn("Column1", GetType(String)))
        dt.Columns.Add(New DataColumn("Column2", GetType(String)))  
        dt.Columns.Add(New DataColumn("Column3", GetType(String)))
        dt.Columns.Add(New DataColumn("Column4", GetType(String)))

        dr = dt.NewRow()
        dr("RowNumber") = 1
        dr("Column1") = String.Empty
        dt.Rows.Add(dr)

        'Store the DataTable in ViewState for future reference   
        ViewState("CurrentTable") = dt

        'Bind the Gridview   
        grvAdicionales.DataSource = dt
        grvAdicionales.DataBind()

        Dim ddl1 As DropDownList = DirectCast(grvAdicionales.Rows(0).Cells(1).FindControl("DropDownList1"), DropDownList)
        FillDropDownList(ddl1, 1)

    End Sub
    Protected Sub ImageButton1_Click(sender As Object, e As EventArgs)
        ''Protected Sub LinkButton1_Click(sender As Object, e As EventArgs)

        Try

        Dim lb As ImageButton = DirectCast(sender, ImageButton)
        Dim gvRow As GridViewRow = DirectCast(lb.NamingContainer, GridViewRow)
        Dim rowID As Integer = gvRow.RowIndex
        Dim Filtro As String() = Split(sender.CommandArgument.ToString(), "|")
        

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

            grvAdicionales.DataSource = dt
            grvAdicionales.DataBind()

            Dim objAdicionales As New clsVersiones
            

                If Filtro(0) <> "" Then
            objAdicionales.CargaSession(Val(Session("cveAcceso")))
            objAdicionales.IDVersion = lblidversion.Text
            objAdicionales.Plazo = Filtro(0)
                    objAdicionales.Enganche = Filtro(1)
            objAdicionales.UsuReg = objAdicionales.UserNameAcceso
            objAdicionales.ManejaVersion(7)
            End If
                'BUG-PC-24 14/12/2016 MAUT Se agrega Try Catch y mensaje de guardado
                MensajeError("Registro guardado exitosamente")

        End If

        'Set Previous Data on Postbacks  
        SetPreviousData()

        Catch ex As Exception
            MensajeError(ex.Message)
        End Try

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

                    Dim ddl1 As DropDownList = DirectCast(grvAdicionales.Rows(rowIndex).Cells(1).FindControl("DropDownList1"), DropDownList)
                    Dim boxEnganche As TextBox = DirectCast(grvAdicionales.Rows(i).Cells(2).FindControl("txtEnganche"), TextBox)
                    Dim boxBalloon As TextBox = DirectCast(grvAdicionales.Rows(i).Cells(3).FindControl("txtBalloon"), TextBox)

                    'Fill the DropDownList with Data   
                    FillDropDownList(ddl1, 1)



                    If i < dt.Rows.Count - 1 Then

                        ddl1.ClearSelection()
                        ddl1.SelectedValue = dt.Rows(i)("Column1").ToString()
                        ddl1.Enabled = False

                        boxEnganche.Text = dt.Rows(i)("Column2").ToString()
                        boxEnganche.Enabled = False

                        boxBalloon.Text = dt.Rows(i)("Column3").ToString()
                        boxBalloon.Enabled = False

                    End If

                    rowIndex += 1
                Next
            End If
        End If
    End Sub
    Protected Sub cmdAgregaAcc_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles cmdAgregaAcc.Click
        Dim count As Integer = 0
        For Each row As GridViewRow In grvAdicionales.Rows
            Dim ddlpla As DropDownList = TryCast(row.FindControl("DropDownList1"), DropDownList)
            Dim boxEnganche As TextBox = TryCast(row.FindControl("txtEnganche"), TextBox)
            Dim boxBalloon As TextBox = TryCast(row.FindControl("txtBalloon"), TextBox)
            Dim ndx As Integer = row.RowIndex

            If Not ValidaBaloon(ddlpla, boxEnganche, boxBalloon, ndx) Then
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

                    Dim ddl1 As DropDownList = DirectCast(grvAdicionales.Rows(i).Cells(1).FindControl("DropDownList1"), DropDownList)
                    Dim VALOR As Integer = Val(ddl1.SelectedValue)
                    If VALOR <> 0 Then
                        dtCurrentTable.Rows(i)("Column1") = Val(ddl1.SelectedValue)
                    End If


                    Dim boxEnganche As TextBox = DirectCast(grvAdicionales.Rows(i).Cells(2).FindControl("txtEnganche"), TextBox)
                    dtCurrentTable.Rows(i)("Column2") = boxEnganche.Text

                    Dim boxBalloon As TextBox = DirectCast(grvAdicionales.Rows(i).Cells(3).FindControl("txtBalloon"), TextBox)
                    dtCurrentTable.Rows(i)("Column3") = boxBalloon.Text


                Next

                'Rebind the Grid with the current data to reflect changes   
                grvAdicionales.DataSource = dtCurrentTable
                grvAdicionales.DataBind()
            End If
        Else

            Response.Write("ViewState is null")
        End If
        'Set Previous Data on Postbacks   
        SetPreviousData()
    End Sub
    Protected Sub grvAdicionales_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvAdicionales.RowCreated
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
    Private Function GuardaAdicionales(ID_Version As Integer, ByRef strError As String) As Boolean
        Try

            GuardaAdicionales = False

            Dim objAdicionales As New clsVersiones
            Dim dtsRes As New DataSet
            Dim objRow As GridViewRow

            Dim objPlazo As Object
            Dim objEnganche As Object
            Dim objBalloon As Object
            Dim objRegNuevo As Object
            
            Dim RegNuevo As String = ""
            Dim dtsTabla As New DataSet

            objAdicionales.CargaSession(Val(Session("cveAcceso")))
           

            For Each objRow In grvAdicionales.Rows
                objPlazo = objRow.Cells(1).Controls(1)
                objEnganche = objRow.Cells(2).Controls(1)
                objBalloon = objRow.Cells(3).Controls(1)
                objRegNuevo = objRow.Cells(4).Controls(1)

                objRegNuevo = objRow.FindControl("lblCadena")
                RegNuevo = objRegNuevo.Text

                If RegNuevo = "" And objPlazo.selectedvalue > 0 Then
                    If objEnganche.text <> "" And objBalloon.text <> "" Then
                        If Val(objEnganche.text) + Val(objBalloon.text) > 100 Then
                            strError = "La suma de los Porcentajes no puede ser mayor al 100%"
                            Exit Function
                        End If
                        objAdicionales.IDVersion = ID_Version
                        objAdicionales.Plazo = objPlazo.selectedvalue
                        objAdicionales.Enganche = objEnganche.text
                        objAdicionales.Balloon = objBalloon.text
                        objAdicionales.IDEstatus = 2
                        objAdicionales.UsuReg = objAdicionales.UserNameAcceso

                        objAdicionales.ManejaVersion(6)
                    Else
                        strError = "Todos los campos marcados con * son obligatorios.'"

                        Exit Function
                    End If
                End If



                If objAdicionales.ErrorVersion <> "" Then
                    MensajeError(objAdicionales.ErrorVersion)
                    strError = objAdicionales.ErrorVersion
                    GuardaAdicionales = False
                    Exit Function
                End If

            Next

            GuardaAdicionales = True
            ViewState("CurrentTable") = Nothing
            llenaGRV()
        Catch ex As Exception
            strError = ex.Message
            GuardaAdicionales = False
        End Try
    End Function
    Private Sub llenaGRV()
       

        Dim objAdicionales As New clsVersiones
        Dim dsAdicionalesVersion As DataSet

        objAdicionales.IDVersion = Val(Request("idVersion"))
        objAdicionales.IDEstatus = 2


        dsAdicionalesVersion = objAdicionales.ManejaVersion(5)


        If dsAdicionalesVersion.Tables(0).Rows.Count > 0 Then
            ViewState("CurrentTable") = dsAdicionalesVersion.Tables(0)
            grvAdicionales.DataSource = ViewState("CurrentTable")
            grvAdicionales.DataBind()
            AddNewRowToGrid()

        Else
            SetInitialRow()

        End If

    End Sub
    Private Sub FillDropDownList(ddl As DropDownList, opc As Integer)
        Dim dtsRes As New DataSet

        Select Case opc
            Case Is = 1
                'cargamos los accesorios disponibles
                Dim objPlazos As New SNProcotiza.clsPlazo
                ''objAcc.IDTipoProducto = Val(cmbTipoProd.SelectedValue)
                objPlazos.Status = 2


                dtsRes = objPlazos.ManejaPlazos(1)
                If objPlazos.StrErrPlazo = "" Then
                    If dtsRes.Tables(0).Rows.Count > 0 Then
                        objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_PLAZO", ddl, strErr, True)
                        If strErr <> "" Then
                            MensajeError(strErr)
                            Exit Sub
                        End If
                    Else
                        MensajeError("No se encontro información de Accesorios.")
                        Exit Sub
                    End If
                Else
                    MensajeError(objPlazos.StrErrPlazo)
                    Exit Sub
                End If
        End Select
    End Sub

    Private Function ValidaBaloon(ByVal plazo As DropDownList, ByVal eng As TextBox, ByVal ball As TextBox, index As Integer) As Boolean
        ValidaBaloon = False
        Dim repetidos As Integer = 0
        LimpiaError()

        If plazo.SelectedValue = 0 Then
            strErr = "Debe seleccionar un plazo."
            Exit Function
        End If

        If eng.Text.Trim.Length = 0 Or ball.Text.Trim.Length = 0 Then
            strErr = "Debe ingresar un valor de enganche y % baloon"
            Exit Function
        End If
        If Val(eng.Text) + Val(ball.Text) > 100 Then
            strErr = "La suma de los Porcentajes no puede ser mayor al 100%"
            Exit Function
        End If
        
        'se evalua que el valor de los creditos colocados sea diferente de los que ya existen

        For Each subrow As GridViewRow In grvAdicionales.Rows
            If index <> subrow.RowIndex Then
                Dim ddlpla As DropDownList = TryCast(subrow.FindControl("DropDownList1"), DropDownList)
                Dim boxEnganche As TextBox = TryCast(subrow.FindControl("txtEnganche"), TextBox)
                If ddlpla.SelectedValue <> 0 And boxEnganche.Text <> "" Then
                If plazo.SelectedValue = ddlpla.SelectedValue And CDbl(boxEnganche.Text) = CDbl(eng.Text) Then
                    repetidos = repetidos + 1
                    Exit For
                    End If
                End If
            End If
        Next


        If repetidos > 0 Then
            strErr = "No se puede repetir valor enganche para el plazo de " + plazo.SelectedItem.Text
            Exit Function
        End If

        ValidaBaloon = True
        Return ValidaBaloon
    End Function
End Class
