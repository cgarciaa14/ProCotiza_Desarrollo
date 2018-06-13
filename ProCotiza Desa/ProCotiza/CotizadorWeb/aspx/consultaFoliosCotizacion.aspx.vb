'BBV-P-412 RQCOT-05:AVH: SE VALIDA EN QUE REPORTE SE MUESTRA
'BBV-P-412 RQCOT-04:AVH: 14/09/2016 SE AGREGAN FILTROS DE BUSQUEDA 
'BBV-P-412 RQCOT-04:AVH: 20/09/2016 RECOTIZACION
'BBV-P-412 RQ06:AMR: 11-10-2016 Administración de Planes de Financiamiento: Promociones
'BUG-PC-25 MAUT 16/12/2016 Se corrige la consulta
'BUG-PC-30 AMR 03/01/2017: Al ingresar al portal procotiza e ingresar a la opcion de cotizacion manda un mensaje de warning.
'BUG-PC-33: JRHM: 10/01/2017 Se modifico problema al consultar rango de fecha (cambio de formato al llamar procedure de consulta)
'BUG-PC-48 JRHM 21/02/17 SE AGREGAN NUEVOS PARAMETROS Y TABLAS PARA REPORTES CON PAGO INTELIGENTE
'BUG-PC-50 JRHM 11/04/17 SE MODIFICA NUMERO DE DECIMALES DE CAT A 2 CARACTERES
'BUG-PC-51 MAPH 17/04/2017 CAMBIOS SOLICITADOS POR MARREDONDO
'BUG-PC-54 CGARCIA 19/04/2017 SE MODIFICO LA FUNCION DE LIMPIAR FILTROS
'BUG-PC-99 ERODRIGUEZ SE CORRIGIO MENSAJE CUANDO EL USUARIO NO TENGA AGENCIAS ASIGNADAS.
'RQ-MN2-2 ERODRIGUEZ 14/09/2017 Se agregaron tasas para compra inteligente .
'RQ-MN2-2.3 ERODRIGUEZ 14/09/2017 Se agregaron tablas para CI.
'BUG-PC-126: ERODRIGUEZ: 16/11/2017 Se agrego parametro para etiqueta de periodo
'Bug-PC-189 : EGONZALEZ : 04/05/2018 : Se agrega al reporte de cotización la sección de datos de "Personalización tu Seguro de daños" sólo para seguros Bancomer.
'BUG-PC-191 : JMENDIETA : 09/05/2018 : Se corrige el redirect de cotizador.aspx a Cotizador.aspx
Imports System.Data
Imports SNProcotiza
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.Data.OleDb
Imports System.IO
Imports System.Text
Imports System.Diagnostics
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing



Partial Class aspx_consultaFoliosCotizacion
    Inherits System.Web.UI.Page
    Dim strErr As String = ""
    Dim parbtnsol As Integer
    Dim objEdo As New clsEstados
    Dim objCombo As New clsProcGenerales
    Dim objPlazos As New clsPlazo

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))
        Dim dtsRes As New DataSet
        Dim objCombo As New clsProcGenerales

        'If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
        'Response.Redirect("../login.aspx", True)
        'End If

        LimpiaError()

        If Not IsPostBack Then
            If CargaCombos() Then
                LimpiaFiltros()

                dtsRes = objCombo.ObtenInfoParametros(157, strErr)
                parbtnsol = dtsRes.Tables(0).Rows(0).Item(3).ToString()
            Else
                If strErr <> "" Then
                    MensajeError(strErr)
                End If
            End If

        End If
    End Sub

    Public Sub LimpiaError()
        lblMensaje.Text = ""
    End Sub

    Private Sub LimpiaFiltros()
        txtFolio.Text = String.Empty
        txtFecIni.Text = String.Empty
        txtFecFin.Text = String.Empty
        txtNombreCli.Text = String.Empty

        'BUG-PC-25 16/12/2016 Se limpia el grid
        grvConsulta.DataSource = Nothing
        grvConsulta.DataBind()

        cmbAgencia.Items.Clear()
        cmbAlianza.Items.Clear()
        cmbGrupo.Items.Clear()
        cmbDivision.Items.Clear()
        cmbEstado.Items.Clear()

        CargaCombos()

    End Sub

    Public Sub MensajeError(ByVal strMsj As String)
        strMsj = Replace(strMsj, "'", "")
        strMsj = Replace(strMsj, """", "")
        strMsj = Replace(strMsj, "(", "")
        strMsj = Replace(strMsj, ")", "")
        lblMensaje.Text = "<script>alert('" & strMsj & "')</script>"
    End Sub

    Private Function CargaCombos() As Boolean
        Try
            CargaCombos = False

            'revisamos el perfil del usuario que esta firmado
            Dim objAge As New SNProcotiza.clsAgencias
            Dim objUsr As New SNProcotiza.clsUsuariosSistema
            Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))
            Dim intEmp As Integer = objUsuFirma.IDEmpresa
            Dim intPaso As Integer = 0
            Dim dtsRes As New DataSet

            Dim strsql As String = String.Empty

            'obtenemos la agencia del usuario si es que la tiene y llenamos el combo
            If objUsuFirma.IDPerfil <> 71 Then
                ''If intPaso > 0 Then
                objUsr.IDUsuario = objUsuFirma.IDUsuario
                dtsRes = objUsr.ManejaUsuario(14)
                strErr = objUsr.ErrorUsuario
                If Trim$(strErr) <> "" Then
                    MensajeError(strErr)
                    Exit Function
                End If
            Else
                'si no es vendedor, promotor o asesor llenamos el combo de agencias de acuerdo con la empresa
                objAge.IDEmpresa = objUsuFirma.IDEmpresa
                dtsRes = objAge.ManejaAgencia(10)
                strErr = objAge.ErrorAgencia
                If Trim$(strErr) <> "" Then
                    MensajeError(strErr)
                    Exit Function
                End If
            End If




            'Combo Agencias
            objAge.IDEstatus = 2

            objAge.IDUsuario = objUsuFirma.IDUsuario
            dtsRes = objAge.ManejaAgencia(31)
            If objAge.ErrorAgencia = "" Then
                If dtsRes.Tables.Count > 0 Then
                    If dtsRes.Tables(0).Rows.Count > 0 Then
                        objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_AGENCIA", cmbAgencia, strErr, True)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError("No se encontro información de Agencias.")
                        Exit Function
                    End If
                Else
                    MensajeError("No se encontro información de Agencias.")
                    Exit Function
                End If
            Else
                MensajeError(strErr)
                Exit Function
            End If

            'Combo Alianza
            Dim objalianza As New clsAlianzas
            Dim dataset As New DataSet
            objalianza.IDUsuario = objUsuFirma.IDUsuario ''dtsRes.Tables(0).Rows(0).Item("ID_ALIANZA")
            objalianza.IDAgencia = cmbAgencia.SelectedValue
            dataset = objalianza.ManejaAlianza(11)
            If objalianza.ErrorAlianza = "" Then
                If dataset.Tables.Count > 0 Then
                    If dataset.Tables(0).Rows.Count >= 0 Then
                        objCombo.LlenaCombos(dataset, "ALIANZA", "ID_ALIANZA", cmbAlianza, strErr, True, False, , -1)
                        If strErr <> "" Then
                            Exit Function
                        End If


                    Else
                        strErr = "No se encontro información de Alianza."
                        Exit Function
                    End If
                Else
                    strErr = "No se encontro información de Alianza."
                    Exit Function
                End If

            Else
                strErr = objalianza.ErrorAlianza
            End If

            'Combo Grupo
            Dim objgrupo As New clsGrupos
            objgrupo.IDUsuario = objUsuFirma.IDUsuario ''dtsRes.Tables(0).Rows(0).Item("ID_GRUPO")
            objgrupo.IDAgencia = cmbAgencia.SelectedValue
            dataset = objgrupo.ManejaGrupo(11)
            If objgrupo.ErrorGrupo = "" Then
                If dataset.Tables.Count > 0 Then
                    If dataset.Tables(0).Rows.Count >= 0 Then
                        objCombo.LlenaCombos(dataset, "GRUPO", "ID_GRUPO", cmbGrupo, strErr, True, False, , -1)
                        If strErr <> "" Then
                            Exit Function
                        End If

                    Else
                        strErr = "No se encontro información de Grupo."
                        Exit Function
                    End If
                Else
                    strErr = "No se encontro información de Grupo."
                    Exit Function
                End If
            Else
                strErr = objgrupo.ErrorGrupo
                Exit Function
            End If

            'Combo División
            Dim objdivision As New clsDivisiones
            objdivision.IDUsuario = objUsuFirma.IDUsuario ''dtsRes.Tables(0).Rows(0).Item("ID_DIVISION")
            objdivision.IDAgencia = cmbAgencia.SelectedValue
            dataset = objdivision.ManejaDivision(11)
            If objdivision.ErrorDivision = "" Then
                If dataset.Tables.Count > 0 Then
                    If dataset.Tables(0).Rows.Count >= 0 Then
                        objCombo.LlenaCombos(dataset, "DIVISION", "ID_DIVISION", cmbDivision, strErr, True, False, , -1)
                        If strErr <> "" Then
                            Exit Function
                        End If

                    Else
                        strErr = "No se encontro información de División."
                        Exit Function
                    End If
                Else
                    strErr = "No se encontro información de División."
                    Exit Function
                End If
            Else
                strErr = objdivision.ErrorDivision
                Exit Function
            End If


            objEdo.IDUsuario = objUsuFirma.IDUsuario
            objEdo.IDAgencia = cmbAgencia.SelectedValue
            dtsRes = objEdo.ManejaEstado(5)

            'Combo Estado
            If objEdo.ErrorEstados = "" Then
                If dtsRes.Tables.Count > 0 Then
                    If dtsRes.Tables(0).Rows.Count >= 0 Then
                        objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_ESTADO", cmbEstado, strErr, True)
                        If strErr <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If

                    Else
                        strErr = "No se encontro información de Estado."
                        Exit Function
                    End If
                Else
                    MensajeError(objEdo.ErrorEstados)
                    Exit Function
                End If
            Else
                MensajeError(objEdo.ErrorEstados)
                Exit Function
            End If

            CargaCombos = True

        Catch ex As Exception
            MensajeError(ex.Message)
            CargaCombos = False
            Exit Function
        End Try
    End Function

    Private Sub BuscaDatos()
        Try
            Dim objCot As New SNProcotiza.clsCotizaciones
            Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))
            Dim dtsRes As New DataSet

            objCot.IDEmpresa = objUsuFirma.IDEmpresa
            objCot.IDAgencia = Val(cmbAgencia.SelectedValue)
            objCot.FolioCotizacion = Val(txtFolio.Text)
            Dim fechaini As Date
            Dim fechafin As Date
            If (txtFecIni.Text <> "" Or txtFecFin.Text <> "") Then
                If ((txtFecIni.Text <> "" And txtFecFin.Text = "") Or (txtFecIni.Text = "" And txtFecFin.Text <> "")) Then

                    MensajeError("Debe Elegir un rango de fechas.")
                    Exit Sub
                Else
                    fechaini = DateTime.Parse(txtFecIni.Text)
                    fechafin = DateTime.Parse(txtFecFin.Text)
                    If (fechafin < fechaini) Then
                        MensajeError("Fecha fin no puede ser menor a fecha inicio")
                        Exit Sub
                    Else
                        objCot.FechaInicioConsultaCotizacion = fechaini.ToString("yyyy-MM-dd")

                        objCot.FechaFinalConsultaCotizacion = fechafin.ToString("yyyy-MM-dd")
                    End If
                End If
            End If

            ''objCot.Nombre = Trim(txtNom.Text)
            ''objCot.IDVigenciaCotizacion = Val(cmbVigencia.SelectedValue)
            objCot.IDAsesor = Val(lblIdAse.Text)
            objCot.IDPromotor = Val(lblIdProm.Text)
            objCot.IDVendedor = Val(lblIdVend.Text)

            If cmbEstado.SelectedValue <> -1 Then
                objCot.IDEstado = cmbEstado.SelectedValue
            End If

            '*****************************************************
            'revisamos si permite consultar cotizaciones abiertas
            Dim objPerm As New SNProcotiza.clsPermisos

            If objUsuFirma.IDTipoSeguridad = 68 Then
                'PERMISOS POR PERFIL
                objPerm = New SNProcotiza.clsPermisos(0, 32, 0, objUsuFirma.IDPerfil)
            Else
                'PERMISOS POR USUARIO
                objPerm = New SNProcotiza.clsPermisos(0, 32, objUsuFirma.IDUsuario)
            End If
            '*****************************************************
            If objPerm.cveEstatus = 2 Then
                objCot.PermiteConsultaCotizacionAbierta = 1
            End If

            'BUG-PC-25 MAUT 16/12/2016
            'Se manda la clave del usuario según su perfil
            If objUsuFirma.IDPerfil = 72 Then
                objCot.IDAsesor = objUsuFirma.IDUsuario
            ElseIf objUsuFirma.IDPerfil = 73 Then
                objCot.IDPromotor = objUsuFirma.IDUsuario
            ElseIf objUsuFirma.IDPerfil = 74 Then
                objCot.IDVendedor = objUsuFirma.IDUsuario
            End If

            'BUG-PC-25 MAUT 16/12/2016 Se cambian los valores para enviar parametros
            If cmbAlianza.SelectedValue > -1 Then
                objCot.IDAlianza = cmbAlianza.SelectedValue
            End If
            If cmbGrupo.SelectedValue > -1 Then
                objCot.IDGrupo = cmbGrupo.SelectedValue
            End If
            If cmbDivision.SelectedValue > -1 Then
                objCot.IDDivision = cmbDivision.SelectedValue
            End If

            If Me.txtNombreCli.Text <> "" Then
                objCot.Nombre = txtNombreCli.Text
            End If
            dtsRes = objCot.ManejaCotizacion(13)

            Session("dtsConsulta") = Nothing
            grvConsulta.PageIndex = 0
            grvConsulta.DataSource = Nothing

            If dtsRes.Tables.Count > 0 Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    Session("dtsConsulta") = dtsRes
                    grvConsulta.DataSource = dtsRes
                Else
                    MensajeError("No se encontró información con los parámetros proporcionados")
                End If
            Else
                MensajeError("No se encontró información con los parámetros proporcionados")
            End If

            grvConsulta.DataBind()

            Dim objpar As New clsProcGenerales
            Dim dtspar As New DataSet
            dtspar = objpar.ObtenInfoParametros(157, strErr)
            If strErr = "" Then
                parbtnsol = dtspar.Tables(0).Rows(0).Item(3).ToString()
            Else
                MensajeError("Error al consultar parametro")
                Exit Sub
            End If

            If parbtnsol = 1 Then
                Dim imgbtn As ImageButton

                For i = 0 To dtsRes.Tables(0).Rows.Count - 1
                    If Convert.ToInt16(dtsRes.Tables(0).Rows(i).Item(9).ToString) = 14 Then
                        imgbtn = grvConsulta.Rows(i).Cells(13).FindControl("ImgBtnSol")
                        imgbtn.Visible = True
                    Else
                        imgbtn = grvConsulta.Rows(i).Cells(13).FindControl("ImgBtnSol")
                        imgbtn.Visible = False
                    End If
                Next
            Else
                grvConsulta.Columns(13).Visible = False  'RQ06
            End If

        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        BuscaDatos()
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As System.EventArgs) Handles btnLimpiar.Click
        LimpiaFiltros()
    End Sub

    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        Dim dts As New DataSet
        Dim imgbtn As New ImageButton
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsulta"), DataSet)
        grvConsulta.DataBind()

        dts = CType(Session("dtsConsulta"), DataSet)

        Dim idcotiza As Integer
        Dim idcotgv As Integer
        For i = 0 To grvConsulta.Rows.Count - 1
            idcotgv = Convert.ToInt64(grvConsulta.Rows(i).Cells(0).Text)
            For j = 0 To dts.Tables(0).Rows.Count - 1
                idcotiza = Convert.ToInt64(dts.Tables(0).Rows(j)(0).ToString())

                If idcotgv = idcotiza Then   'RQ06
                    If Convert.ToInt16(dts.Tables(0).Rows(j).Item(9).ToString) = 14 Then
                        imgbtn = grvConsulta.Rows(i).Cells(13).FindControl("ImgBtnSol")
                        imgbtn.Visible = True
                    Else
                        imgbtn = grvConsulta.Rows(i).Cells(13).FindControl("ImgBtnSol")
                        imgbtn.Visible = False
                    End If
                End If
            Next
        Next
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "folioId" Then

            Dim strReporte As String = System.Configuration.ConfigurationManager.AppSettings.Item("EligeRep")

            If strReporte = "1" Then
                CargaCotizacionPDF(Val(e.CommandArgument))
            End If

            If strReporte = "2" Then
                'BBV-P-412 RQCOT-05:AVH
                CargaCotizacion(Val(e.CommandArgument))
            End If

        End If
        If e.CommandName = "SolId" Then
            Response.Redirect("cotSolicitud.aspx?idCotizacion=" & Val(e.CommandArgument).ToString() & "&idCotSeguro=" & Val(Request("IdCotSeguro")) & "&Propuesta=" & 0)
        End If

        If e.CommandName = "EditId" Then
            'BUG-PC-191
            Response.Redirect("Cotizador.aspx?idCotizacion=" & Val(e.CommandArgument).ToString() & "&idCotSeguro=" & Val(Request("IdCotSeguro")) & "&Propuesta=" & 0)
        End If

    End Sub

    Private Sub CargaCotizacionPDF(IDCot As Integer)

        Dim objCot As New SNProcotiza.clsCotizaciones
        Dim dtsRep As New DataSet
        Dim dtsFiltros As New DataSet
        Dim dtsCargIni As New DataSet
        Dim dtsLeyendas As New DataSet
        Dim rpvCotiza As New Microsoft.Reporting.WebForms.ReportViewer

        Try
            objCot.CargaCotizacion(IDCot)


            Dim TipOperRG As Integer = objCot.IDTipoOperacion
            ''Dim NoAgenRG As Integer = Val(Request("NoAgen"))

            dtsRep = objCot.ConsultaTablaAmort

            If Trim$(objCot.ErrorCotizacion) = "" Then

                dtsCargIni = objCot.ObtenCargosIniciales(TipOperRG) 'los cargos iniciales van antes que los filtros
                dtsFiltros = objCot.ObtenFiltros
                dtsLeyendas = objCot.ObtenLeyendasReporte

                Dim objParams(5) As Microsoft.Reporting.WebForms.ReportParameter
                'Parametros
                Dim strCadRep As String = System.Configuration.ConfigurationManager.AppSettings.Item("Reportes")
                Dim strEncabezado As String = System.Configuration.ConfigurationManager.AppSettings.Item("RepEncabezado")
                Dim strImagenProd As String = System.Configuration.ConfigurationManager.AppSettings.Item("ImgProductos")
                Dim strImagenLogo As String = System.Configuration.ConfigurationManager.AppSettings.Item("RepImgLogo")
                Dim strPasoLogo = System.Configuration.ConfigurationManager.AppSettings.Item("imagenLogoReporte")
                Dim strPasoImgProd = System.Configuration.ConfigurationManager.AppSettings.Item("imagenProdReporte")

                Dim objProd As New SNProcotiza.clsProductos(objCot.IDProducto)
                Dim objEmp As New SNProcotiza.clsEmpresas()

                'se maneja que el logo dependa de la url por donde se acceso y no por la empresa del usuario
                objEmp.URLAcceso = Trim(Session("urlAcceso"))
                objEmp.CargaEmpresa(0)

                If objCot.IDTipoOperacion = 1 Then
                    strCadRep += System.Configuration.ConfigurationManager.AppSettings.Item("RepCotizacion")

                    strImagenProd += IIf(objProd.ImagenProducto <> "", objProd.ImagenProducto, strPasoImgProd)
                    strImagenLogo += IIf(objEmp.ImagenLogoReporte <> "", objEmp.ImagenLogoReporte, strPasoLogo)

                    'CALCULAMOS EL CAT Y LO PASAMOS COMO PARAMETRO
                    Dim dblCAT As Double = 0
                    dtsCargIni = objCot.ObtenCargosIniciales(TipOperRG)
                    dblCAT = objCot.CalculaCat(dtsRep, dtsCargIni, dtsFiltros)

                    'pasamos la ruta de la imagen como parámetro
                    Dim paramImagen = New Microsoft.Reporting.WebForms.ReportParameter("imagenProducto", strImagenProd)

                    'Vacios
                    Dim paramPaquete = New Microsoft.Reporting.WebForms.ReportParameter("leyenda1", dtsFiltros.Tables(0).Rows(0).Item("ASEGURADORA").ToString)
                    Dim paramModelo = New Microsoft.Reporting.WebForms.ReportParameter("leyenda2", dtsFiltros.Tables(0).Rows(0).Item("ESTADO_SEG").ToString)

                    Dim paramCAT = New Microsoft.Reporting.WebForms.ReportParameter("leyenda_CAT", dblCAT.ToString)
                    Dim paramImagenLogo = New Microsoft.Reporting.WebForms.ReportParameter("imagen_logo", strImagenLogo)
                    Dim paramEncabezado = New Microsoft.Reporting.WebForms.ReportParameter("leyenda_encabezado", strEncabezado)

                    objParams(0) = paramImagen
                    objParams(1) = paramPaquete
                    objParams(2) = paramModelo
                    objParams(3) = paramCAT
                    objParams(4) = paramImagenLogo
                    objParams(5) = paramEncabezado

                    'generamos el reporte
                    If dtsRep.Tables.Count > 0 Then
                        If dtsRep.Tables(0).Rows.Count > 0 Then
                            rpvCotiza.ProcessingMode = ProcessingMode.Local
                            rpvCotiza.LocalReport.ReportPath = strCadRep
                            rpvCotiza.LocalReport.EnableExternalImages = True
                            rpvCotiza.LocalReport.SetParameters(objParams)

                            rpvCotiza.LocalReport.DataSources.Clear()
                            Dim objDS As New ReportDataSource("ProcotizaCalculo_spCalculaPagosNC", dtsRep.Tables(0))
                            rpvCotiza.LocalReport.DataSources.Add(objDS)
                            Dim objDSFilt As New ReportDataSource("ProcotizaWebDataSet_spObtenFiltrosCot", dtsFiltros.Tables(0))
                            rpvCotiza.LocalReport.DataSources.Add(objDSFilt)
                            Dim objDSCI As New ReportDataSource("ProcotizaCargosIniciales_spCargosIniciales", dtsCargIni.Tables(0))
                            rpvCotiza.LocalReport.DataSources.Add(objDSCI)
                            Dim objLeyRep As New ReportDataSource("ProcotizaLeyendas_spManejaLeyendasReporte", dtsLeyendas.Tables(0))
                            rpvCotiza.LocalReport.DataSources.Add(objLeyRep)


                            Dim warnings() As Warning = Nothing
                            Dim streams() As String = Nothing
                            Dim renderedBytes() As Byte
                            Dim reportType As String = "PDF"
                            Dim mimeType As String = ""
                            Dim encoding As String = ""
                            Dim fileNameExtension As String = ""
                            'Dim strFile As String = "cot" & Request("idCot") & ".pdf"
                            Dim strFile As String = "cot" & IDCot.ToString() & ".pdf"

                            'The DeviceInfo settings should be changed based on the reportType
                            Dim deviceInfo As String = "<DeviceInfo>" & _
                                                       "  <OutputFormat>PDF</OutputFormat>" & _
                                                       "</DeviceInfo>"

                            renderedBytes = rpvCotiza.LocalReport.Render(reportType, deviceInfo, mimeType, encoding, _
                                                                         fileNameExtension, streams, warnings)

                            fileNameExtension = Server.MapPath(strFile)
                            fileNameExtension = Replace(fileNameExtension, "\aspx\", "\Docs\")
                            My.Computer.FileSystem.WriteAllBytes(fileNameExtension, renderedBytes, False)

                            Try
                                Response.Clear()
                                Response.ClearContent()
                                Response.ClearHeaders()
                                Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile)
                                Response.ContentType = "application/pdf"
                                Response.TransmitFile(fileNameExtension)

                            Catch ex As Exception
                                MensajeError(ex.Message)
                            End Try

                        End If
                    End If
                Else
                End If
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub
    Private Sub CargaCotizacion(IDCot As Integer) 'BBV-P-412 RQCOT-05:AVH
        Dim Fname As String
        Dim crReportDocument As ReportDocument
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim crExportOptions As ExportOptions

        Dim dtsDatos As New DataSet
        Dim dtsTabla As New DataSet
        Dim objRep As New SNProcotiza.clsReportes
        Dim objTab As New SNProcotiza.clsCotizaciones
        Dim objCot As New SNProcotiza.clsCotizaciones
        Dim dtsFiltros As New DataSet
        Dim dtsCargIni As New DataSet
        Dim dtsRep As New DataSet
        Dim dtsTA_6 As New DataSet
        Dim strRuta As String
        objRep.IDCotizacion = IDCot
        objTab.CargaCotizacion(IDCot)
        Dim objPaq As New clsPaquetes
        objPaq.CargaPaquete(objTab.IDPaquete)
        If objPaq.IDTipoCalculo = 167 Then
            strRuta = Server.MapPath("..\Reportes\rptCotizacion2int.rpt")
        Else
            strRuta = Server.MapPath("..\Reportes\rptCotizacion2.rpt")
        End If

        dtsDatos = objRep.ManejaReporte(1)

        dtsDatos.Tables(0).TableName = "Datos"
        dtsDatos.Tables(1).TableName = "TablaAmort"
        dtsDatos.Tables(2).TableName = "Leyendas"
        dtsDatos.Tables(3).TableName = "TablaPorc"

        If dtsDatos.Tables.Count > 0 Then
            If dtsDatos.Tables(0).Rows.Count > 0 Then
            Else
                MensajeError("Las condiciones de la cotización no están vigentes, genere una nueva cotización")
                Exit Sub
            End If
        Else
            MensajeError("Las condiciones de la cotización no están vigentes, genere una nueva cotización")
            Exit Sub
        End If


        If objPaq.IDTipoCalculo = 167 Then
            dtsDatos.Tables(4).TableName = "AditionalData"

            dtsDatos.Tables(5).TableName = "MontosCI"
            dtsDatos.Tables(6).TableName = "LeyendaComparativo"
            dtsDatos.Tables(7).TableName = "Tabla_Amort_6"


        End If

        dtsDatos.Tables(8).TableName = "PER_SEG_DANIOS"

        crReportDocument = New ReportDocument
        crReportDocument.Load(strRuta)

        'RQ06
        Fname = Server.MapPath("Cotizacion_" & IDCot & "_" & Format$(Now(), "dd-MM-yyyy HH-mm-ss") & ".pdf")
        Fname = Replace(Fname, "\aspx\", "\Docs\")

        System.IO.File.Delete(Fname)

        crDiskFileDestinationOptions = New DiskFileDestinationOptions
        crDiskFileDestinationOptions.DiskFileName = Fname
        crExportOptions = crReportDocument.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat

        End With

        crReportDocument.SetDataSource(dtsDatos)

        Dim IMG As String
        If dtsDatos.Tables(0).Rows.Count > 0 Then
            IMG = dtsDatos.Tables(0).Rows(0).Item("IMG_REP").ToString
        Else
            IMG = ""
        End If

        If IMG <> "" Then
            crReportDocument.SetParameterValue("PicturePath", IMG)
        Else
            crReportDocument.SetParameterValue("PicturePath", "")  'RQ06
        End If

        crReportDocument.SetParameterValue("No_Cotizacion", IDCot.ToString)

        objCot.CargaCotizacion(IDCot)
        Dim TipOperRG As Integer = objCot.IDTipoOperacion
        dtsCargIni = objCot.ObtenCargosIniciales(TipOperRG) 'los cargos iniciales van antes que los filtros
        dtsFiltros = objCot.ObtenFiltros
        dtsRep = objCot.ConsultaTablaAmort
        dtsTA_6 = objCot.ConsultaTablaAmort_6

        Dim dblCAT As Double = 0
        Dim dblCAT_DOS As Double = 0
        Dim strFechaPeriodoT6 As String = ""
        dblCAT = objCot.CalculaCat(dtsRep, dtsCargIni, dtsFiltros)
        dblCAT_DOS = objCot.CalculaCat_Dos(dtsTA_6, dtsCargIni, dtsFiltros)
        crReportDocument.SetParameterValue("leyenda_CAT", FormatNumber(CDbl(dblCAT.ToString()), 1))

        If objPaq.IDTipoCalculo = 167 Then
            If dtsTA_6.Tables(0).Rows.Count > 0 Then
                strFechaPeriodoT6 = dtsTA_6.Tables(0).Rows(0).Item("FEC_PAGO")
            End If
            crReportDocument.SetParameterValue("FechaPeriodo6", strFechaPeriodoT6)
            crReportDocument.SetParameterValue("Valor_Garantizado", CDbl(objTab.MontoValorResidual))
            crReportDocument.SetParameterValue("leyenda_CAT_DOS", FormatNumber(CDbl(dblCAT_DOS.ToString()), 1))

            Dim ArD() As Double
            ReDim ArD(1)
            ArD = ObtenTasa36_72(objCot.IDPlazo, objPaq.IDPaquete)

            'If objCot.IDPlazo = 4 Or objCot.IDPlazo = 6 Then


            Dim t32 As String = ArD(0)
            Dim tr32 As String = ArD(1)
            If Not t32.Contains(".") Then
                t32 = t32 + ".00"
            Else
                Dim i As Integer
                i = t32.IndexOf(".")
                Dim pdec As String
                pdec = t32.Substring(i)
                If (pdec.Length < 3) Then
                    t32 = t32 + "0"
                End If

            End If



            crReportDocument.SetParameterValue("Tasa_32", ":" + t32 + "%")
            crReportDocument.SetParameterValue("Tasa_R", tr32)
            crReportDocument.SetParameterValue("Tasa_R_S", tr32)

            Dim t1 As String = "1-" + objCot.ValorPlazo.ToString() + " Meses"
            Dim t2 As String = (Convert.ToInt16(objCot.ValorPlazo.ToString()) + 1).ToString() + "-" + (Convert.ToInt16(objCot.ValorPlazo.ToString()) * 2).ToString() + " Meses"

            crReportDocument.SetParameterValue("tasa1label", t1)
            crReportDocument.SetParameterValue("tasa2label", t2)
            crReportDocument.SetParameterValue("tasa2label_2", t2)
            'Else
            '    crReportDocument.SetParameterValue("Valor_Garantizado", 0)
            '    crReportDocument.SetParameterValue("leyenda_CAT_DOS", "")

            '    crReportDocument.SetParameterValue("FechaPeriodo6", "")
            '    crReportDocument.SetParameterValue("Tasa_32", "")
            '    crReportDocument.SetParameterValue("Tasa_R", "")
            '    crReportDocument.SetParameterValue("tasa1label", "Auto")
            '    crReportDocument.SetParameterValue("tasa2label", "")
            '    crReportDocument.SetParameterValue("tasa2label_2", "")
            'End If



        End If


        crReportDocument.Export()
        crReportDocument.Close()
        crReportDocument.Dispose()


        'Dim psi As New ProcessStartInfo()
        'psi.UseShellExecute = True
        'psi.FileName = Fname
        'Process.Start(psi)

        Response.Clear()
        Response.ClearContent()
        Response.ClearHeaders()
        Response.AddHeader("Content-Disposition", "attachment; filename=" + Fname)
        Response.ContentType = "application/pdf"
        Response.TransmitFile(Fname)

        'Response.TransmitFile(Fname)
        'Response.Flush()
        'Response.Close()
        'Response.Clear()
        'Response.ClearContent()


    End Sub

    Private Function ObtenTasa36_72(plazo As Integer, idpaquete As Integer) As Double()
        Dim ArD() As Double
        ReDim ArD(1)
        Dim dtsRes As New DataSet
        objPlazos.IDPaquete = idpaquete
        objPlazos.Id_Plazo = plazo 'plazo 
        dtsRes = objPlazos.ManejaPlazos(6)
        If objPlazos.StrErrPlazo = "" Then
            If Not IsDBNull(dtsRes.Tables(0).Rows(0).Item("TASA_NOMINAL_DOS")) Then
                ArD(0) = dtsRes.Tables(0).Rows(0).Item("TASA_NOMINAL_DOS")
            Else
                ArD(0) = 0
            End If

            If Not IsDBNull(dtsRes.Tables(0).Rows(0).Item("PTJ_SERV_FINAN_DOS")) Then
                ArD(1) = dtsRes.Tables(0).Rows(0).Item("PTJ_SERV_FINAN_DOS")
            Else
                ArD(1) = 0
            End If
            Return ArD
        Else
            Return Nothing
        End If

    End Function

    Protected Sub cmbAgencia_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))
        cmbAlianza.Items.Clear()
        cmbDivision.Items.Clear()
        cmbGrupo.Items.Clear()
        cmbEstado.Items.Clear()

        'Combo Alianza
        Dim objalianza As New clsAlianzas
        Dim dataset As New DataSet
        objalianza.IDUsuario = objUsuFirma.IDUsuario ''dtsRes.Tables(0).Rows(0).Item("ID_ALIANZA")
        objalianza.IDAgencia = cmbAgencia.SelectedValue
        dataset = objalianza.ManejaAlianza(11)
        If objalianza.ErrorAlianza = "" Then
            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count >= 0 Then
                    objCombo.LlenaCombos(dataset, "ALIANZA", "ID_ALIANZA", cmbAlianza, strErr, True, False, , -1)
                    If strErr <> "" Then
                        Exit Sub
                    End If
                Else
                    strErr = "No se encontro información de Alianza."
                    Exit Sub
                End If
            Else
                strErr = "No se encontro información de Alianza."
                Exit Sub
            End If

        Else
            strErr = objalianza.ErrorAlianza
        End If

        'Combo Grupo
        Dim objgrupo As New clsGrupos
        objgrupo.IDUsuario = objUsuFirma.IDUsuario ''dtsRes.Tables(0).Rows(0).Item("ID_GRUPO")
        objgrupo.IDAgencia = cmbAgencia.SelectedValue
        dataset = objgrupo.ManejaGrupo(11)
        If objgrupo.ErrorGrupo = "" Then
            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count >= 0 Then
                    objCombo.LlenaCombos(dataset, "GRUPO", "ID_GRUPO", cmbGrupo, strErr, True, False, , -1)
                    If strErr <> "" Then
                        Exit Sub
                    End If

                Else
                    strErr = "No se encontro información de Grupo."
                    Exit Sub
                End If
            Else
                strErr = "No se encontro información de Grupo."
                Exit Sub
            End If
        Else
            strErr = objgrupo.ErrorGrupo
            Exit Sub
        End If

        'Combo División
        Dim objdivision As New clsDivisiones
        objdivision.IDUsuario = objUsuFirma.IDUsuario ''dtsRes.Tables(0).Rows(0).Item("ID_DIVISION")
        objdivision.IDAgencia = cmbAgencia.SelectedValue
        dataset = objdivision.ManejaDivision(11)
        If objdivision.ErrorDivision = "" Then
            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count >= 0 Then
                    objCombo.LlenaCombos(dataset, "DIVISION", "ID_DIVISION", cmbDivision, strErr, True, False, , -1)
                    If strErr <> "" Then
                        Exit Sub
                    End If


                Else
                    strErr = "No se encontro información de División."
                    Exit Sub
                End If
            Else
                strErr = objdivision.ErrorDivision
                Exit Sub
            End If
        Else
            strErr = objdivision.ErrorDivision
            Exit Sub
        End If
        Dim dtsRes As New DataSet
        objEdo.IDUsuario = objUsuFirma.IDUsuario
        objEdo.IDAgencia = cmbAgencia.SelectedValue
        dtsRes = objEdo.ManejaEstado(5)

        'Combo Estado
        If objEdo.ErrorEstados = "" Then
            If dtsRes.Tables.Count > 0 Then
                If dtsRes.Tables(0).Rows.Count >= 0 Then
                    objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_ESTADO", cmbEstado, strErr, True)
                    If strErr <> "" Then
                        MensajeError(strErr)
                        Exit Sub
                    End If

                Else
                    strErr = "No se encontro información de Estado."
                    Exit Sub
                End If
            Else
                strErr = "No se encontro información de Estado."
                Exit Sub
            End If
        Else
            MensajeError(objEdo.ErrorEstados)
            Exit Sub
        End If
    End Sub
End Class
