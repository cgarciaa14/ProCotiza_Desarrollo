Imports System.Data
Imports SNProcotiza

Partial Class aspx_relacionaAgencias
    Inherits System.Web.UI.Page
    Dim strErr As String = ""


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LimpiaError()

        If Not IsPostBack Then
            Script = "document.getElementById('cuerpoConsul').style.width='70%';"
            CargaCombos(1)
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

    Public WriteOnly Property Script() As String
        Set(ByVal value As String)
            lblScript.Text = "<script>" & value & " </script>"
        End Set
    End Property

    Private Function CargaCombos(ByVal intOpc As Integer) As Boolean
        Dim objCombo As New clsProcGenerales
        Dim objAgen As New clsAgencias
        Dim objEmpresas As New SNProcotiza.clsEmpresas
        Dim dtsRes As New DataSet
        Try
            CargaCombos = False

            Select Case intOpc
                Case 1
                    'combo Empresas
                    objEmpresas.IDEstatus = 2
                    dtsRes = objEmpresas.ManejaEmpresa(1)
                    strErr = objEmpresas.ErrorEmpresa

                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "RAZON_SOCIAL", "ID_EMPRESA", cmbEmpresa, strErr)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If

                Case 2

                    If txtNom.Text.Length > 0 Then
                        objAgen.Nombre = txtNom.Text.Trim()
                    End If

                    objAgen.IDEmpresa = cmbEmpresa.SelectedValue
                    objAgen.IDEstatus = 2
                    dtsRes = objAgen.ManejaAgencia(10)
                    strErr = objAgen.ErrorAgencia
                    If Trim$(strErr) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                Session("dtsConsultaAG") = dtsRes
                                grvConsulta.DataSource = dtsRes
                                grvConsulta.DataBind()
                            End If
                        End If
                    Else
                        MensajeError(strErr)
                    End If
            End Select

            CargaCombos = True
        Catch ex As Exception
            CargaCombos = False
            MensajeError(ex.Message)
        End Try

    End Function

    Protected Sub btnTodas_Click(sender As Object, e As System.EventArgs) Handles btnTodas.Click
        Try
            Dim objAge As New clsAgencias
            Dim intConsul As Integer = 0
            Dim intBorra As Integer = 0
            Dim intInserta As Integer = 0

            Dim objRow As DataRow
            Dim dtsConsul As DataSet = CType(Session("dtsConsultaAG"), DataSet)
            ObtenRelacion(objAge, intConsul, intBorra, intInserta)
            objAge.IDEstatusOtro = 2

            For Each objRow In dtsConsul.Tables(0).Rows
                objAge.IDAgencia = objRow.Item("id_agencia")
                objAge.ManejaAgencia(intConsul)

                If objAge.ErrorAgencia = "" Then
                    'primero se borra y despues se inserta
                    objAge.ManejaAgencia(intBorra)
                    objAge.ManejaAgencia(intInserta)
                Else
                    MensajeError(objAge.ErrorAgencia)
                End If
            Next
            CargaCombos(2)
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub


    Protected Sub btnNinguna_Click(sender As Object, e As System.EventArgs) Handles btnNinguna.Click
        Try
            Dim objAge As New clsAgencias
            Dim intConsul As Integer = 0
            Dim intBorra As Integer = 0
            Dim intInserta As Integer = 0

            Dim objRow As DataRow
            Dim dtsConsul As DataSet = CType(Session("dtsConsultaAG"), DataSet)
            ObtenRelacion(objAge, intConsul, intBorra, intInserta)

            For Each objRow In dtsConsul.Tables(0).Rows
                objAge.IDAgencia = objRow.Item("id_agencia")
                objAge.ManejaAgencia(intConsul)

                If objAge.ErrorAgencia = "" Then
                    'se borra
                    objAge.ManejaAgencia(intBorra)
                Else
                    MensajeError(objAge.ErrorAgencia)
                End If
            Next
            CargaCombos(2)
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        CargaCombos(2)
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsultaAG"), DataSet)
        grvConsulta.DataBind()
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "ageId" Then
            Dim objAge As New clsAgencias
            Dim intConsul As Integer = 10
            Dim intBorra As Integer = 8
            Dim intInserta As Integer = 9
            Dim dtsRes As New DataSet
            Dim objRow As GridViewRow = grvConsulta.Rows(Val(e.CommandArgument))
            Dim objImg As ImageButton = objRow.Cells(4).Controls(1)

            Dim AgeID As Integer = grvConsulta.DataKeys(Convert.ToInt32(e.CommandArgument)).Values(1)

            objAge.IDEmpresa = cmbEmpresa.SelectedValue
            objAge.IDAgencia = AgeID 
            dtsRes = objAge.ManejaAgencia(intConsul)

            If dtsRes.Tables(0).Rows.Count > 0 Then
                If dtsRes.Tables(0).Rows(0).Item("ASIG") = 0 Then
                    objAge.ManejaAgencia(intInserta)
                    objImg.ImageUrl = "../img/tick.png"
                    objRow.Cells(5).Text = 1
                Else
                    objAge.ManejaAgencia(intBorra)
                    objImg.ImageUrl = "../img/cross.png"
                    objRow.Cells(5).Text = 0
                End If
            Else
                objAge.ManejaAgencia(intInserta)
                objImg.ImageUrl = "../img/tick.png"
                objRow.Cells(5).Text = 1
            End If
        End If
    End Sub

    Protected Sub grvConsulta_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvConsulta.RowDataBound
        Dim objImg As ImageButton

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim datakey As String = Convert.ToString(grvConsulta.DataKeys(e.Row.RowIndex).Values(2).ToString)

            objImg = e.Row.FindControl("ImageButton1")
            If datakey = 1 Then
                objImg.ImageUrl = "../img/tick.png"
            Else
                objImg.ImageUrl = "../img/cross.png"
            End If

        End If
    End Sub

    Private Sub ObtenRelacion(ByRef objAge As clsAgencias, _
                              Optional ByRef intOpcConsulta As Integer = 0, _
                              Optional ByRef intOpcElimina As Integer = 0, _
                              Optional ByRef intOpcInserta As Integer = 0)
        Try
            Select Case Request("tipoRel")
                Case 1 'paquetes
                    objAge.IDPaquete = Val(Request("idRel"))
                    intOpcElimina = 5
                    intOpcInserta = 6
                    intOpcConsulta = 7
                Case 2 'empresas
                    objAge.IDEmpresa = Val(Request("idRel"))
                    intOpcElimina = 8
                    intOpcInserta = 9
                    intOpcConsulta = 10
                Case 3 'productos
                    objAge.IDProducto = Val(Request("idRel"))
                    intOpcElimina = 11
                    intOpcInserta = 12
                    intOpcConsulta = 13
                Case 4 'promotores
                    objAge.IDPromotor = Val(Request("idRel"))
                    intOpcElimina = 14
                    intOpcInserta = 15
                    intOpcConsulta = 16
                Case 5 'asesores
                    objAge.IDAsesor = Val(Request("idRel"))
                    intOpcElimina = 17
                    intOpcInserta = 18
                    intOpcConsulta = 19
                Case 6 'vendedores
                    objAge.IDVendedor = Val(Request("idRel"))
                    intOpcElimina = 20
                    intOpcInserta = 21
                    intOpcConsulta = 22
            End Select
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub


    Protected Sub bntBuscaAgen_Click(sender As Object, e As System.EventArgs) Handles bntBuscaAgen.Click
        CargaCombos(2)
    End Sub

End Class
