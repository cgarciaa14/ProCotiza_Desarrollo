'RQ-PC8: CGARCIA: 09/05/2018: SE CREA PANTALLA

Imports System.Data
Imports SNProcotiza

Partial Class aspx_consultaCoberturas
    Inherits System.Web.UI.Page

    Dim strErr As String = String.Empty
    Dim objCobertura As New clsCoberturas
    Dim objalianzas As New clsAlianzas

    Protected Sub aspx_consultaCoberturas_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            cargaCombos(1)
            _fnConsultaInfo(1)

        End If
    End Sub

    Private Function cargaCombos(ByVal opc As Integer) As Boolean
        Dim dsRes As New DataSet
        Dim objParam As New clsParametros
        Dim objCombo As New clsProcGenerales
        Dim objAlianza As New clsAlianzas
        cargaCombos = False

        Try
            Select Case opc
                Case 1
                    'combo de clasificacion
                    dsRes = objParam.ManejaParametro(11)
                    If objParam.ErrorParametros.Trim = String.Empty Then
                        If (Not IsNothing(dsRes) AndAlso dsRes.Tables.Count > 0 AndAlso dsRes.Tables(0).Rows.Count > 0) Then
                            objCombo.LlenaCombos(dsRes, "TEXTO", "ID_PARAMETRO", ddlClasificacion, strErr, True)                            
                        Else
                            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", "alert('" + ReplaceMSJ(strErr) + "');", True)
                            Exit Function
                        End If
                    Else
                        ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", "alert('" + ReplaceMSJ(strErr) + "');", True)
                        Exit Function
                    End If

                    'como de alianzas
                    objAlianza.IDEstatus = 2
                    objAlianza.Alianza = String.Empty
                    dsRes = objAlianza.ManejaAlianza(1)

                    If objAlianza.ErrorAlianza.Trim = String.Empty Then
                        If (Not IsNothing(dsRes) AndAlso dsRes.Tables.Count > 0 AndAlso dsRes.Tables(0).Rows.Count > 0) Then
                            objCombo.LlenaCombos(dsRes, "ALIANZA", "ID_ALIANZA", ddlAlianza, strErr, True)                            
                        Else
                            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", "alert('" + ReplaceMSJ(strErr) + "');", True)
                            Exit Function
                        End If
                    Else
                        ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", "alert('" + ReplaceMSJ(strErr) + "');", True)
                        Exit Function
                    End If

            End Select
            cargaCombos = True
        Catch ex As Exception
            cargaCombos = False
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", "alert('" + ReplaceMSJ(ex.Message) + "');", True)
        End Try

    End Function

    Private Function ReplaceMSJ(ByVal strMsj As String) As String
        strMsj = Replace(strMsj, "'", "")
        strMsj = Replace(strMsj, """", "")
        strMsj = Replace(strMsj, "(", "")
        strMsj = Replace(strMsj, ")", "")

        Return strMsj
    End Function

    Protected Sub btnConsulta_Click(sender As Object, e As EventArgs)
        '_fnConsultaInfo(1)
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs)
        'Response.Redirect("./manejaCoberturas.aspx?idCobertura=0", False)
        Dim strLocation As String = ("../aspx/manejaCoberturas.aspx?idCobertura=0")
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As ImageClickEventArgs)

    End Sub

    Protected Sub btnEliminar_Click(sender As Object, e As ImageClickEventArgs)

    End Sub

    Sub _fnConsultaInfo(ByVal _tipo As Integer)        
        Dim dsRes As New DataSet
        Dim dsAl As New DataSet

        Select Case _tipo
            Case 1
                'CONSULTA TODO
                objCobertura._intOpcion = _tipo
                objCobertura._intClasificacion = ddlClasificacion.SelectedValue
                objCobertura._intAlianza = ddlAlianza.SelectedValue
                objCobertura._intCobertura = 0

                dsRes = objCobertura.ManejaCoberturas()
                If (dsRes.Tables.Count > 0 AndAlso dsRes.Tables(0).Rows.Count > 0) Then

                    Dim dt2 As New DataTable
                    Dim dc As DataColumn
                    Dim dt = New DataTable

                    dt = dsRes.Tables(0)

                    Dim dtRemove As DataTable = dsRes.Tables(0)
                    If (dsRes.Tables.CanRemove(dtRemove)) Then
                        dsRes.Tables.Remove(dtRemove)
                    End If

                    dc = New DataColumn("ID", Type.GetType("System.Int32"))
                    dt2.Columns.Add(dc)
                    dc = New DataColumn("NAME", Type.GetType("System.String"))
                    dt2.Columns.Add(dc)
                    dc = New DataColumn("ESTATUS", Type.GetType("System.String"))
                    dt2.Columns.Add(dc)
                    dc = New DataColumn("CLASIFICACION", Type.GetType("System.String"))
                    dt2.Columns.Add(dc)
                    dc = New DataColumn("FECHA_ALTA", Type.GetType("System.String"))
                    dt2.Columns.Add(dc)
                    dc = New DataColumn("ALIANZA", Type.GetType("System.String"))
                    dt2.Columns.Add(dc)

                    Dim row As DataRow
                    For index As Integer = 0 To dt.Rows.Count - 1 Step 1
                        row = dt2.NewRow
                        row("ID") = dt.Rows(index).Item(0).ToString
                        row("NAME") = dt.Rows(index).Item(1).ToString
                        row("ESTATUS") = IIf((CInt(dt.Rows(index).Item(2).ToString) = 2), "ACTIVO", "INACTIVO")
                        Select Case CInt(dt.Rows(index).Item(3).ToString)
                            Case 0
                                row("CLASIFICACION") = "SIN CLASIFICACIÓN"
                            Case 63
                                row("CLASIFICACION") = "NUEVO"
                            Case 64
                                row("CLASIFICACION") = "SEMINUEVO"
                        End Select
                        row("FECHA_ALTA") = dt.Rows(index).Item(4).ToString
                        Select Case CInt(dt.Rows(index).Item(5).ToString)
                            Case Is <> 0                                
                                objalianzas.IDAlianza = CInt(dt.Rows(index).Item(5).ToString)
                                dsAl = objalianzas.ManejaAlianza(1)
                                row("ALIANZA") = dsAl.Tables(0).Rows(0).Item(1).ToString
                            Case Else
                                row("ALIANZA") = "SIN ALIANZA"
                        End Select
                        dt2.Rows.Add(row)
                    Next

                    dsRes.Tables.Add(dt2)

                    Session("dtsConsulta") = Nothing
                    grvInfo.PageIndex = 0
                    grvInfo.DataSource = Nothing

                    Session("dtsConsulta") = dsRes
                    grvInfo.DataSource = dsRes
                    grvInfo.DataBind()

                Else
                    IsNothing(grvInfo.DataSource)
                    grvInfo.DataBind()
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", "alert('No se tiene información de coberturas');", True)
                    Exit Sub
                End If            

        End Select

    End Sub

    Protected Sub grvInfo_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grvInfo.PageIndexChanging
        If CType(Session("dtsConsulta"), DataSet) Is Nothing Then
            _fnConsultaInfo(1)
        End If

        grvInfo.PageIndex = e.NewPageIndex
        grvInfo.DataSource = CType(Session("dtsConsulta"), DataSet)
        grvInfo.DataBind()

    End Sub

    Protected Sub grvInfo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grvInfo.RowCommand
        If e.CommandName = "Command_edit" Then
            'Response.Redirect("./manejaCoberturas.aspx?idCobertura=" + e.CommandArgument, False)
            Dim strLocation As String = ("../aspx/manejaCoberturas.aspx?idCobertura=" + e.CommandArgument)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
        ElseIf e.CommandName = "Commnad_delete" Then
            Dim dsRes As New DataSet
            Dim _intCobertura As Integer = e.CommandArgument

            objCobertura._intOpcion = 2
            objCobertura._intCobertura = _intCobertura

            dsRes = objCobertura.ManejaCoberturas()

            If (dsRes.Tables.Count > 0 AndAlso dsRes.Tables(0).Rows.Count > 0 AndAlso CInt(dsRes.Tables(0).Rows(0).Item("RESULTADO").ToString) = 1) Then
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", "alert('Elemento eliminado correctamente.');", True)
                _fnConsultaInfo(1)
            Else
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", "alert('Error al eliminar elemento.');", True)
                Exit Sub
            End If


        End If
    End Sub

    Protected Sub ddlAlianza_SelectedIndexChanged(sender As Object, e As EventArgs)
        _fnConsultaInfo(1)
    End Sub

    Protected Sub ddlClasificacion_SelectedIndexChanged(sender As Object, e As EventArgs)
        _fnConsultaInfo(1)
    End Sub
End Class
