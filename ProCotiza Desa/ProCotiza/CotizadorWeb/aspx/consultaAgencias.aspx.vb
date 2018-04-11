'BUG-PC-40:PVARGAS:27/01/2017:SE CARGAN NUEVAMENTE LOS DATOS AL MOMENTO DE LIMPIAR.
'RQADM2-04: ERODRIGUEZ: 13/09/2017: Se agrego busqueda por ID de agencia.
Imports System.Data

Partial Class aspx_consultaAgencias
    Inherits System.Web.UI.Page
    Dim strErr As String = ""


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            MensajeError(objUsuFirma.ErrorUsuario)
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
        lblMensaje.Text = String.Empty
        lblScript.Text = String.Empty
    End Sub

    Private Sub LimpiaFiltros()
        cmbEstatus.SelectedValue = 0
        txtNom.Text = ""
        txtIdAgencia.Text = ""
        'BUG-PC-40:PVARGAS:27/01/2017:SE CARGAN NUEVAMENTE LOS DATOS AL MOMENTO DE LIMPIAR.
        BuscaDatos()
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
                        objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbEstatus, strErr, True)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If

                    ''combo de marcas
                    'Dim objMarca As New SNProcotiza.clsMarcas
                    'objMarca.IDEstatus = 2
                    'objMarca.IDTipoRegistro = 113 'trae marcas que puedan registrar agencias

                    'dtsRes = objMarca.ManejaMarca(5)
                    'strErr = objMarca.ErrorMarcas

                    'If Trim$(strErr) = "" Then
                    '    objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_MARCA", cmbMarca, strErr, True)
                    '    If Trim$(strErr) <> "" Then
                    '        MensajeError(strErr)
                    '        Exit Function
                    '    End If
                    'End If
            End Select

            CargaCombos = True
        Catch ex As Exception
            CargaCombos = False
            MensajeError(ex.Message)
        End Try
    End Function

    Private Sub BuscaDatos()
        Try
            Dim objAgen As New SNProcotiza.clsAgencias
            Dim dtsRes As New DataSet

            ''objAgen.IDMarca = Val(cmbMarca.SelectedValue)
            objAgen.IDEstatus = Val(cmbEstatus.SelectedValue)
            objAgen.Nombre = Trim(txtNom.Text)
            If Not String.IsNullOrEmpty(Trim(txtIdAgencia.Text)) Then
                If Integer.TryParse(Trim(txtIdAgencia.Text), Nothing) Then
                    objAgen.IDAgencia = Trim(txtIdAgencia.Text)
                Else
                    MensajeError("Id Agencia debe ser numérico.")
                End If

            End If
         
            dtsRes = objAgen.ManejaAgencia(1)

            Session("dtsConsulta") = Nothing
            grvConsulta.PageIndex = 0
            grvConsulta.DataSource = Nothing

            If dtsRes.Tables.Count > 0 Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    Session("dtsConsulta") = dtsRes
                    grvConsulta.DataSource = dtsRes
                Else
                    ''MensajeError("No se encontró información con los parámetros proporcionados.")
                End If
            Else
                ''MensajeError("No se encontró información para los parámetros proporcionados.")
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
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As System.EventArgs) Handles btnAgregar.Click
        Response.Redirect("./manejaAgencias.aspx?idAgencia=0")
    End Sub

    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging

        If CType(Session("dtsConsulta"), DataSet) Is Nothing Then
            BuscaDatos()
        End If

        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsulta"), DataSet)
        grvConsulta.DataBind()
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "agenciaId" Then
            Response.Redirect("./manejaAgencias.aspx?idAgencia=" & e.CommandArgument)
        End If

        If e.CommandName = "MarcasAsig" Then
            Script = "AbrePopup('./relacionAgenciaMarcas.aspx?AgeID=" & e.CommandArgument & "&tipoRel=1',300,100,800,550)"
        End If
    End Sub

    Protected Sub cmbEstatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEstatus.SelectedIndexChanged
        BuscaDatos()
    End Sub
End Class
