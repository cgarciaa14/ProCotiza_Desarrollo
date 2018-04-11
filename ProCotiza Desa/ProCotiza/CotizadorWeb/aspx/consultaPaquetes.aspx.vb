'BBVA-P-412: AMR: RQ02,RQ03,RQ04,RQ05,RQ06,RQ08
'BBVA-P-412: 08/08/2016: AMR: RQ09 – Carga Masiva de Plan de Financiamiento
'BUG-PC-02:  09/11/2016: AMR: Error en manejaProductos
'BUG-PC-30 AMR 03/01/2017: Al ingresar al portal procotiza e ingresar a la opcion de cotizacion manda un mensaje de warning.
'BUG-PC-42 JRHM 30/01/17 Se agrega funcionalidad de boton limpiar
'BUG-PC-40:PVARGAS:27/01/2017:SE CARGAN NUEVAMENTE LOS REGISTROS DE LA TABLA AL LIMPIAR LAS OPCIONES DE BUSQUEDA
'RQ-MN2-6: RHERNANDEZ: 15/09/17: AUMENTA TAMAÑO DE PANTALLA PAQUETES-PRODUCTOS POR NUEVOS CONTROLES
Imports System.Data
Imports CargaExcel
Imports System.Net

Partial Class aspx_consultaPaquetes
    Inherits System.Web.UI.Page
    Dim strErr As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ''Opción WebClient
        'Dim url As String = "http://ltelmxac/svcticket/api/ticket"
        'Dim synClient As WebClient = New WebClient
        'Dim content As String = synClient.DownloadString(url)

        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
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
        ddlmoneda.SelectedValue = 0
        ddlestatus.SelectedValue = 0
        txtnombre.Text = ""
        txtidpaq.Text = "" 'BBVA-P-412
    End Sub

    Public Sub MensajeError(ByVal strMsj As String)
        strMsj = Replace(strMsj, "'", "")
        strMsj = Replace(strMsj, """", "")
        strMsj = Replace(strMsj, "(", "")
        strMsj = Replace(strMsj, ")", "")
        strMsj = Replace(strMsj, "-", " ")
        strMsj = Replace(strMsj, vbCrLf, " ")
        lblMensaje.Text = "<script>alert('" & strMsj & "')</script>"
    End Sub

    Public WriteOnly Property Script() As String
        Set(ByVal value As String)
            lblScript.Text = "<script>" & value & " </script>"
        End Set
    End Property

    Public Sub CierraPantalla(ByVal strPant As String)
        lblMensaje.Text += "<script>alert('El registro se guardó con éxito'); document.location.href='" & strPant & "';</script>"
    End Sub

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
                        objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", ddlestatus, strErr, True)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If

                    'combo de monedas
                    Dim objMon As New SNProcotiza.clsMonedas
                    objMon.IDEstatus = 2

                    dtsRes = objMon.ManejaMoneda(1)
                    strErr = objMon.ErrorMoneda

                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_MONEDA", ddlmoneda, strErr, True)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    End If
            End Select
            CargaCombos = True
        Catch ex As Exception
            MensajeError(ex.Message)
            CargaCombos = False
        End Try
    End Function

    Private Sub BuscaDatos()
        Try
            Dim objPaq As New SNProcotiza.clsPaquetes
            Dim dtsRes As New DataSet

            If ddlmoneda.SelectedValue <> 0 Then
                objPaq.IDMoneda = ddlmoneda.SelectedValue
            End If

            If ddlestatus.SelectedValue <> 0 Then
                objPaq.IDEstatus = ddlestatus.SelectedValue
            End If
			'BBVA-P-412
            If txtidpaq.Text.Trim.Length > 0 Then
                objPaq.IDPaquete = txtidpaq.Text.Trim
            End If
            objPaq.Nombre = Trim(txtnombre.Text)
            dtsRes = objPaq.ManejaPaquete(1)

            Session("dtsConsulta") = Nothing
            grvConsulta.PageIndex = 0
            grvConsulta.DataSource = Nothing

            If dtsRes.Tables.Count > 0 Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    Session("dtsConsulta") = dtsRes
                    grvConsulta.DataSource = dtsRes
                Else
                    'MensajeError("No se encontró información con los parámetros proporcionados.")
                End If
            Else
                'MensajeError("No se encontró información para los parámetros proporcionados.")
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
        'BUG-PC-40:PVARGAS:27/01/2017:SE CARGAN NUEVAMENTE LOS REGISTROS DE LA TABLA AL LIMPIAR LAS OPCIONES DE BUSQUEDA
        BuscaDatos()
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
		'BBVA-P-412
        If e.CommandName = "copyPaq" Then
            Response.Redirect("./copiaPaquete.aspx?paqId=" & e.CommandArgument)
        End If

        If e.CommandName = "paqId" Then
            Response.Redirect("./manejaPaquetes.aspx?paqId=" & e.CommandArgument)
        End If

        If e.CommandName = "paqAge" Then
            Script = "AbrePopup('relacionaPaquetesAgencias.aspx?idPaq=" & e.CommandArgument & "&tipoRel=1',300,100,800,550)"
        End If

        If e.CommandName = "paqProd" Then
            Script = "AbrePopup('relacionaPaquetesProductos.aspx?idPaq=" & e.CommandArgument & "&tipoRel=1',300,100,920,550)"
        End If
		'BBVA-P-412
        If e.CommandName = "paqAseg" Then
            Script = "AbrePopup('relacionaPaquetesAseguradoras.aspx?idPaq=" & e.CommandArgument & "&tipoRel=1',300,100,800,550)"
        End If
    End Sub

    Protected Sub ddlestatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlestatus.SelectedIndexChanged
        BuscaDatos()
    End Sub

    Protected Sub ddlmoneda_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlmoneda.SelectedIndexChanged
        BuscaDatos()
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As System.EventArgs) Handles btnAgregar.Click
        Response.Redirect("./manejaPaquetes.aspx?paqId=0")
    End Sub

    'Protected Sub btncargaplan_Click(sender As Object, e As System.EventArgs) Handles btncargaplan.Click
    '    Dim dts As New DataSet
    '    Dim fileNameExtension As String = ""

    '    fileNameExtension = System.Configuration.ConfigurationManager.AppSettings.Item("Repositorio") & _
    '        System.Configuration.ConfigurationManager.AppSettings.Item("FileName")

    '    Dim objcarga As New clsCargaPaquetes
    '    objcarga.CveUsu = Val(Session("cveUsu"))
    '    objcarga.CveAcceso = Val(Session("cveAcceso"))

    '    dts = objcarga.Read(fileNameExtension)

    '    If objcarga.intTotErrors > 0 And objcarga.intTotSuccess = 0 Then
    '        MensajeError("El archivo no fue procesado, revisar archivo log.")
    '    ElseIf objcarga.intTotErrors > 0 And objcarga.intTotSuccess > 0 Then
    '        MensajeError("Archivo se proceso parcialmente, revisar archivo log.")
    '    ElseIf objcarga.intTotSuccess > 0 And objcarga.intTotErrors = 0 Then
    '        MensajeError("Archivo procesado satisfactoriamente.")
    '    End If
    'End Sub

    'BUG-PC-02
    Protected Sub grvConsulta_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grvConsulta.RowDataBound

        Dim objparam As New SNProcotiza.clsParametros(178) 'Consulta por Alianza

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim divrelPaqAseg As HtmlGenericControl = DirectCast(e.Row.FindControl("divrelPaqAseg"), HtmlGenericControl)

                If objparam.Valor = 1 Then
                    divrelPaqAseg.Visible = False
                Else
                    divrelPaqAseg.Visible = True
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub grvConsulta_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsulta"), DataSet)
        grvConsulta.DataBind()
    End Sub
End Class
