'--BBV-P-412:AUBALDO:06/07/2016 RQ10 – Agregar funcionalidad en la pantalla Alta de Agencias (Brecha 63,64, Ampliación de Brecha)
'BBV-P-412:AUBALDO:26/07/2016 RQ A – Copia de Alta de Agencias de ProCotiza a Prodesk
'BBV-P-412:AVH:04/08/2016 RQ20.2 OPCION BonoCC
'BBV-P-412:RQ 10 AVH: 28/10/2016 WS ALTA Y MODIFICACION DE AGENCIAS
'BBV-P-412:RQ F GVARGAS: 07/11/2016 Correccion errores.
'BUG-PC-14 25/11/2016 MAUT Validaciones
'BUG-PC-27 MAUT 26/12/2016 Se validan comisiones
'BUG-PC-30 AMR 03/01/2017: Al ingresar al portal procotiza e ingresar a la opcion de cotizacion manda un mensaje de warning.
'BBV-P-412:RQ LOGIN GVARGAS: 23/12/2016 Actualizacion REST Service iv_ticket & userID														 
'BBV-P-423: 04/01/17: JRHM RQCONYFOR-06 Se modifico codigo fuente para manejar el nuevo valor de validacion bloqueo agencia. 
'BBV-P-412:BUG-PC-35 18/01/2017 JRHM Se cambia comportamiento de la pagina en base al status y el evento al consultar agencia del combo de telefono
'BUG-PC-38 MAUT 23/01/2017 Se hace obligatorio el campo de responsable de facturas
'BUG-PC-39 25/01/17 JRHM  Correccion de errores multiples
'BUG-PC-42 30/01/17 JRHM  Se corrigio funcionalidad de pestaña domicilio
'BUG-PC-43 01/02/17 JRHM Se agrego la validacion de formato de fecha fin vigencia de la agencia
'BUG-PC-44 07/02/17 JRHM SE INHABILITA CAJA DE TEXTO DE FECHA FIN VIGENCIA Y EN CASO DE INACTIVIDAD COLOCAR LA FECHA DEL SISTEMA
'BBVA-P-412 18/04/2017 RQTARESQ-06 CGARCIA SE AGREGO LA OPCION PARA LOS ESQUEMAS EN CUESTION DE MANDARLO COMO PARAMETRO PARA TODO EL PROCESO.
'BUG-PC-53 AMR 21/04/2017 Correcciones Multicotiza.
'BUG-PC-64:MPUESTO:13/05/2017 CORRECCION DE VALIDACION DE NUMERO DE CUENTA Y TITULAR.
'BUG-PC-65:MPUESTO:22/05/2017:Bloqueo del combo correspondiente al estado de la agencia para que solo se pueda dar de alta como activa.
'BUG-PC-74:MPUESTO:06/06/2017:ATENCION DE LOS SIGUIENTES PUNTOS:
'                               + SEGMENTACION DE CLASES PARA INVOCAR WEB SERVICE DE INSERCION Y ACTUALIZACION DE AGENCIAS
'                               + ACTUALIZACION DE RELACION AGENCIA <--> DIVISION POR SERVICIO WEB 
'BUG-PC-75:ERODRIGUEZ:15/06/2017:Se agrego checkbox para validar cuando la agencia cuenta con biometrico.
'BUG-PC-99: ERODRIGUEZ: 01/08/2017:Se agrego validacion para cuando el parametro de biometrico sea nulo
'RQ-INB217: RHERNANDEZ: 15/08/17: SE AGREGO LA OPCION DE SELECCIONAR UNA MARCA DEFAULT A UNA AGENCIA
Imports System.Data
Imports SNProcotiza
Imports Newtonsoft.Json
Imports WCF.clsAgenciasWS
Imports WCF

Partial Class aspx_manejaAgencias
    Inherits System.Web.UI.Page
    Dim strErr As String = ""
    Dim intOpc As Integer = 2


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()

        If Not IsPostBack Then
            CargaCombos(1)
            If Val(Request("idAgencia")) > 0 Then
                CargaInfo()
                llenaGRV()
            Else
                'BUG-PC-65:MPUESTO:22/05/2017:Bloqueo del combo correspondiente al estado de la agencia para que solo se pueda dar de alta como activa.
                Me.cmbEstatus.Enabled = False
                SetInitialRow()
            End If

        End If
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "DisFechaFin", "$('[id$=txtFinVig]').datepicker();", True)
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

    Public Sub CierraPantalla(ByVal strPant As String, Optional ByVal withMsj As Boolean = True)
        'BUG-PC-14 2016-11-24 MAUT Si se envia el parametro como falso, cierra la pantalla pero no manda mensaje de guardado
        If withMsj Then
            lblMensaje.Text += "<script>alert('El registro se guardó con éxito'); document.location.href='" & strPant & "';</script>"
        Else
            lblMensaje.Text += "<script>document.location.href='" & strPant & "';</script>"
        End If

    End Sub

    Private Sub CargaInfo()
        Try
            Dim dts As New DataSet
            Dim objAge As New SNProcotiza.clsAgencias
            objAge.IDAgencia = Val(Request("idAgencia"))
            objAge.IDMarca = Val(Request("idMarca"))
            dts = objAge.ManejaAgencia(1)

            If dts.Tables(0).Rows.Count > 0 Then
                'GENERALES AGENCIA
                lblId.Text = dts.Tables(0).Rows(0).Item("ID_AGENCIA")
                txtNom.Text = dts.Tables(0).Rows(0).Item("NOMBRE")
                TextRFC.Text = dts.Tables(0).Rows(0).Item("RFC")
                TextCuentaA.Text = dts.Tables(0).Rows(0).Item("CUENTA_AGENCIA")
                hdnCuentaAgencia.Value = dts.Tables(0).Rows(0).Item("CUENTA_AGENCIA")
                TextTCuenta.Text = dts.Tables(0).Rows(0).Item("TITULAR_CUENTA")
                TextRespFact.Text = dts.Tables(0).Rows(0).Item("RESPONSABLE_FACT")

                TextUsuMod.Text = dts.Tables(0).Rows(0).Item("USU_ULT_MOD")
                txtIniVig.Text = IIf(dts.Tables(0).Rows(0).Item("FEC_REG") = "1900-01-01", "", dts.Tables(0).Rows(0).Item("FEC_REG"))
                txtFinVig.Text = IIf(dts.Tables(0).Rows(0).Item("FEC_VIG") = "1900-01-01", "", dts.Tables(0).Rows(0).Item("FEC_VIG"))
                TextFecUltMod.Text = dts.Tables(0).Rows(0).Item("FEC_ULT_MOD")
                cmbAlianza.SelectedValue = dts.Tables(0).Rows(0).Item("ID_ALIANZA")
                cmbGrupo.SelectedValue = dts.Tables(0).Rows(0).Item("ID_GRUPO")
                cmbDivision.SelectedValue = dts.Tables(0).Rows(0).Item("ID_DIVISION")
                CmbEsquema.SelectedValue = dts.Tables(0).Rows(0).Item("ID_ESQUEMAS")
                'DOMICILIO AGENCIA
                TextCodPos.Text = dts.Tables(0).Rows(0).Item("CPOSTAL")
                CargaUbicacion(dts.Tables(0).Rows(0).Item("CPOSTAL"), 1)
                cmbEstados.SelectedValue = dts.Tables(0).Rows(0).Item("ID_ESTADO")
                txtDomi.Text = dts.Tables(0).Rows(0).Item("CALLE")
                TextNoext.Text = dts.Tables(0).Rows(0).Item("NOEXT")
                TextNoint.Text = dts.Tables(0).Rows(0).Item("NOINT")
                cmbColoniaCliente.SelectedValue = dts.Tables(0).Rows(0).Item("ID_COLONIA")
                cmbDelMunCliente.SelectedValue = dts.Tables(0).Rows(0).Item("ID_MUNICIPIO")
                cmbCiudadCliente.SelectedValue = dts.Tables(0).Rows(0).Item("ID_CIUDAD")
                cmbTDomicilio.SelectedValue = dts.Tables(0).Rows(0).Item("ID_DOMICILIO")
                cmbTDomicilio_SelectedIndexChanged(cmbTDomicilio, Nothing)
                'TELEFONO AGENCIA
                cmbTipoTel.SelectedValue = dts.Tables(0).Rows(0).Item("ID_TELEFONO")
                cmbTipoTel_SelectedIndexChanged(cmbTipoTel, Nothing)
                TextContacto.Text = dts.Tables(0).Rows(0).Item("CONTACTO")
                textLDist.Text = dts.Tables(0).Rows(0).Item("LDIST")
                TextLada.Text = dts.Tables(0).Rows(0).Item("LADA")
                txtTel.Text = dts.Tables(0).Rows(0).Item("TELEFONO")
                TextExt.Text = dts.Tables(0).Rows(0).Item("EXTENSION")
                'EMAIL AGENCIA
                TextEmailContact.Text = dts.Tables(0).Rows(0).Item("EMAIL_CONTACTO")
                TextEmail.Text = dts.Tables(0).Rows(0).Item("EMAIL")
                'COMISIONES AGENCIA
                ChkComAgencia.Checked = dts.Tables(0).Rows(0).Item("COMISION_AGENCIA")
                ChkComVendedor.Checked = dts.Tables(0).Rows(0).Item("COMISION_VENDEDOR")
                TextUdis.Text = dts.Tables(0).Rows(0).Item("PRC_UDIS")
                TextDivi.Text = dts.Tables(0).Rows(0).Item("DIVIDENDOS")
                TextBonoV.Text = dts.Tables(0).Rows(0).Item("BONO_VEN")
                TextPagoFI.Text = dts.Tables(0).Rows(0).Item("PAG_FYI")
                TextRegalias.Text = dts.Tables(0).Rows(0).Item("REGALIAS")
                TextSegReg.Text = dts.Tables(0).Rows(0).Item("COMI_SEGURO")
                chkServBloq.Checked = dts.Tables(0).Rows(0).Item("VALIDA_AGENCIA_BLOQUEADA")
                If Not IsDBNull(dts.Tables(0).Rows(0).Item("VALIDA_BIOMETRICO")) Then
				ChkBiom.Checked = dts.Tables(0).Rows(0).Item("VALIDA_BIOMETRICO")
                Else
                    ChkBiom.Checked = False
                End If
                'ChkBiom.Checked = dts.Tables(0).Rows(0).Item("VALIDA_BIOMETRICO")

                'txtDomi.Text = dts.Tables(0).Rows(0).Item("DOMICILIO")
                'txtTel.Text = dts.Tables(0).Rows(0).Item("TELEFONO")
                If Not IsDBNull(dts.Tables(0).Rows(0).Item("ID_MARCA")) Then
                    cmbMarca.SelectedValue = dts.Tables(0).Rows(0).Item("ID_MARCA")
                End If

                'cmbEstados.SelectedValue = dts.Tables(0).Rows(0).Item("ID_ESTADO")

                cmbEstatus.SelectedValue = dts.Tables(0).Rows(0).Item("ESTATUS")
                cmbEstatus_SelectedIndexChanged(cmbEstatus, Nothing)
                cmbMotivoBaja.SelectedValue = dts.Tables(0).Rows(0).Item("CVE_PCO_TIPO_BAJA") 'JRHM Se mueve esta carga de valor debido a evento de cmbestatus
                chkDefault.Checked = IIf(dts.Tables(0).Rows(0).Item("REG_DEFAULT") = 1, True, False)
                current_tab.Value = 0
            Else
                MensajeError("No se localizó información para la plaza.")
                Exit Sub
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

            Select Case intOpc
                Case 1
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
                    cmbEstatus_SelectedIndexChanged(cmbEstatus, Nothing)

                    ''se carga el combo de marcas
                    Dim objMarca As New SNProcotiza.clsMarcas
                    objMarca.IDEstatus = 2
                    objMarca.IDTipoRegistro = 113 'trae marcas que puedan registrar agencias

                    dtsRes = objMarca.ManejaMarca(5)
                    strErr = objMarca.ErrorMarcas

                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_MARCA", cmbMarca, strErr, False)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    End If

                    'combo de estados
                    'Dim objEdo As New SNProcotiza.clsEstados
                    'objEdo.IDEstatus = 2
                    'dtsRes = objEdo.ManejaEstado(1)
                    'strErr = objEdo.ErrorEstados

                    'If Trim$(strErr) = "" Then
                    '    objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_ESTADO", cmbEstados, strErr, False)
                    '    If Trim$(strErr) <> "" Then
                    '        MensajeError(strErr)
                    '        Exit Function
                    '    End If
                    'End If
                    'combo de Alianza
                    Dim objAli As New SNProcotiza.clsAlianzas
                    objAli.IDEstatus = 2
                    dtsRes = objAli.ManejaAlianza(1)
                    strErr = objAli.ErrorAlianza

                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "ALIANZA", "ID_ALIANZA", cmbAlianza, strErr, False)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    End If
                    'combo de Grupos
                    Dim objGrup As New SNProcotiza.clsGrupos
                    objGrup.IDEstatus = 2
                    dtsRes = objGrup.ManejaGrupo(1)
                    strErr = objGrup.ErrorGrupo

                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "GRUPO", "ID_GRUPO", cmbGrupo, strErr, False)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    End If
                    'combo de Divisiones
                    Dim objDiv As New SNProcotiza.clsDivisiones
                    objDiv.IDEstatus = 2
                    dtsRes = objDiv.ManejaDivision(1)
                    strErr = objDiv.ErrorDivision

                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "DIVISION", "ID_DIVISION", cmbDivision, strErr, False)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    End If
                    'Combo de Esquemas 
                    Dim objEsq As New SNProcotiza.clsEsquema
                    objEsq.IDEstatus = 2
                    dtsRes = objEsq.MAnejaEsquema(1)
                    strErr = objEsq.ErrorEsquemas

                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "C_ESQUEMAS", "ID_ESQUEMAS", CmbEsquema, strErr, False)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    End If
                    'combo de Motivo de Baja
                    Dim objMBaja As New SNProcotiza.clsMotivoBaja
                    objMBaja.IDEstatus = 2
                    dtsRes = objMBaja.ManejaMotivoBaja(1)
                    strErr = objMBaja.ErrorMBaja

                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "TIPO_BAJA", "CVE_PCO_TIPO_BAJA", cmbMotivoBaja, strErr)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    End If
                    'combo de Tipo Telefono
                    Dim objTTelefono As New SNProcotiza.clsTipoTelefono
                    Dim dv As DataView
                    Dim dt As New DataTable


                    objTTelefono.IDEstatus = 2
                    dtsRes = objTTelefono.ManejaTTelefono(1)
                    strErr = objTTelefono.ErrorTTelefono
                    dv = New DataView(dtsRes.Tables(0))

                    If Trim$(strErr) = "" Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            dv.RowFilter = "MASCARA = ''"
                            dtsRes = New DataSet()
                            dtsRes.Tables.Add(dv.ToTable())
                            objCombo.LlenaCombos(dtsRes, "TIPO_TELEFONO", "CVE_PCO_TIPO_TELEFONO", cmbTipoTel, strErr)
                            If Trim$(strErr) <> "" Then
                                MensajeError(strErr)
                                Exit Function
                            End If
                            cmbTipoTel.SelectedValue = 0
                            TextContacto.Text = ""
                            TextContacto.Enabled = False
                            textLDist.Text = ""
                            textLDist.Enabled = False
                            TextLada.Text = ""
                            TextLada.Enabled = False
                            txtTel.Text = ""
                            txtTel.Enabled = False
                            TextExt.Text = ""
                            TextExt.Enabled = False
                            '    cmbTipoTel.DataValueField = "CVE_PCO_TIPO_TELEFONO"
                            '    cmbTipoTel.DataTextField = "TIPO_TELEFONO"
                            '    cmbTipoTel.DataBind()
                        Else
                            MensajeError("NO SE ENCONTRO INFORMACION DE TIPO TELEFONO")
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If
                    'combo de Tipo Domicilio
                    Dim objTDomicilio As New SNProcotiza.clsTipoDomicilio
                    objTDomicilio.IDEstatus = 2
                    dtsRes = objTDomicilio.ManejaTDomicilio(1)
                    strErr = objTDomicilio.ErrorTDomicilio

                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "TIPO_DOMICILIO", "CVE_PCO_TIPO_DOM", cmbTDomicilio, strErr, False)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    End If
            End Select

            CargaCombos = True
        Catch ex As Exception
            CargaCombos = False
            MensajeError(ex.Message)
        End Try
    End Function

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        Try
            Dim intAge As Integer = Val(Request("idAgencia"))
            Dim objAge As New SNProcotiza.clsAgencias
            Dim strError = ""

            objAge.CargaSession(Val(Session("cveAcceso")))
            If intAge > 0 Then
                objAge.CargaAgencia(intAge)
                intOpc = 3
            End If

            If ValidaCamposDomicilio() Then
                If ValidaCamposTelefono() Then
                    If validarCorreoElectronico() Then
                        If ValidaCampos() Then
                            'guardamos la info del tipo de operación                
                            'DATOS GENERALES AGENCIA
                            objAge.IDMarca = Val(cmbMarca.SelectedValue)
                            objAge.IDEstatus = Val(cmbEstatus.SelectedValue)
                            objAge.RegistroDefault = IIf(chkDefault.Checked, 1, 0)
                            objAge.Nombre = Trim(txtNom.Text)
                            objAge.RFC = Trim(TextRFC.Text)
                            objAge.CuentaAgencia = Trim(TextCuentaA.Text)
                            'BUG-PC-60:MPUESTO:13/05/2017 CORRECCION DE VALIDACION DE NUMERO DE CUENTA Y TITULAR.
                            objAge.TitularCuenta = Trim(hdnTitularCuenta.Value) 'objAge.TitularCuenta = Trim(TextTCuenta.Text)
                            objAge.ResponsableFact = Trim(TextRespFact.Text)
                            objAge.IDTipoBaja = Val(cmbMotivoBaja.SelectedValue)
                            objAge.FechaRegistro = Trim(txtIniVig.Text)
                            objAge.FechaVigencia = Trim(txtFinVig.Text)
                            objAge.IDAlianza = Val(cmbAlianza.SelectedValue)
                            objAge.IDGrupo = Val(cmbGrupo.SelectedValue)
                            objAge.IDDivision = Val(cmbDivision.SelectedValue)
                            objAge.IDDEsquema = Val(CmbEsquema.SelectedValue)
                            objAge.UsuarioRegistro = objAge.UserNameAcceso
                            'DATOS COMISIONES AGENCIA
                            objAge.ComisionAgencia = IIf(ChkComAgencia.Checked, 1, 0)
                            objAge.ComisionVendedor = IIf(ChkComVendedor.Checked, 1, 0)
                            objAge.PrcUdis = IIf(Trim(TextUdis.Text) = "", 0, Trim(TextUdis.Text))
                            objAge.Dividendos = IIf(Trim(TextDivi.Text) = "", 0, Trim(TextDivi.Text))
                            objAge.BonoVendedor = IIf(Trim(TextBonoV.Text) = "", 0, Trim(TextBonoV.Text))
                            objAge.PagoFYI = IIf(Trim(TextPagoFI.Text) = "", 0, Trim(TextPagoFI.Text))
                            objAge.Regalias = IIf(Trim(TextRegalias.Text) = "", 0, Trim(TextRegalias.Text))
                            objAge.ComiSeguro = IIf(Trim(TextSegReg.Text) = "", 0, Trim(TextSegReg.Text))
                            'DATOS DOMICILIO AGENCIA 
                            objAge.IDEstado = Val(cmbEstados.SelectedValue)
                            objAge.Domicilio = Trim(txtDomi.Text)
                            objAge.NoExt = Trim(TextNoext.Text)
                            objAge.NoInt = Trim(TextNoint.Text)
                            objAge.CodPos = Trim(TextCodPos.Text)
                            objAge.Colonia = Val(cmbColoniaCliente.SelectedValue)
                            objAge.Municipio = Val(cmbDelMunCliente.SelectedValue)
                            objAge.Ciudad = Val(cmbCiudadCliente.SelectedValue)
                            objAge.TTipoDomicilio = Val(cmbTDomicilio.SelectedValue)
                            'DATOS TELEFONO AGENCIA                    
                            objAge.TTelefono = Val(cmbTipoTel.SelectedValue)
                            objAge.Contacto = Trim(TextContacto.Text)
                            objAge.LargaDist = Trim(textLDist.Text)
                            objAge.Lada = Trim(TextLada.Text)
                            objAge.Telefono = Trim(txtTel.Text)
                            objAge.Extension = Trim(TextExt.Text)
                            'DATOS EMAIL AGENCIA                 
                            objAge.EmailContacto = Trim(TextEmailContact.Text)
                            objAge.Email = Trim(TextEmail.Text)
                            objAge.IDValidaBloqueo = IIf(chkServBloq.Checked, 1, 0)
							objAge.IDTieneBiometrico = IIf(ChkBiom.Checked, 1, 0)
                            '===================================RQ 10 AVH:WS ALTA Y MODIFICACION DE AGENCIAS
                            If intAge > 0 Then
                                If cmbEstatus.SelectedValue = 3 Then
                                    If EliminaAgenciaWS(intAge) = False Then
                                        CierraPantalla("./consultaAgencias.aspx", False)
                                        Exit Sub
                                    End If
                                Else

                                    If ModificaAgenciaWS(intAge, strError) = False Then
                                        If strError <> "" Then
                                            'BUG-PC-14 2016-11-24 MAUT: Si el falla el WS se guardan los cambios pero se desactiva la agencia
                                            If AltaAgenciaWS(Val(intAge)) = False Then
                                                ''And strError = "ERROR EN AGENCIA AL CONSULTAR NO EXISTE AGENCIA" Then
                                                CierraPantalla("./consultaAgencias.aspx", False)
                                                Exit Sub
                                            Else
                                                objAge.IDEstatus = 2
                                            End If
                                        End If
                                    Else
                                        objAge.IDEstatus = 2
                                    End If
                                End If
                                'Else
                                '    If AltaAgenciaWS(Val(objAge.IDAgencia)) = False Then
                                '        Exit Sub
                                '    End If
                            End If

                            Dim dts As New DataSet()
                            dts = objAge.ManejaAgencia(intOpc)
                            If dts.Tables.Count > 0 Then
                                If dts.Tables(0).Columns.Contains("ID_AGENCIA") Then
                                    If dts.Tables(0).Rows(0).Item("ID_AGENCIA") = -1 Then
                                        MensajeError("El nombre de Agencia ya existe.")
                                        Exit Sub
                                    End If
                                End If
                            End If

                            If intAge = 0 Then
                                If AltaAgenciaWS(Val(objAge.IDAgencia)) = False Then
                                    'BUG-PC-14 2016-11-24 MAUT Si falla el WS se guardan los cambios pero la agencia queda inactiva.
                                    objAge.IDEstatus = 3
                                    objAge.ManejaAgencia(3)
                                    CierraPantalla("./consultaAgencias.aspx", False)
                                    Exit Sub
                                End If
                            End If

                            If objAge.ErrorAgencia = "" Then
                                If GuardaBonoCC() = False Then
                                    Exit Sub
                                End If
                                If strError <> "" Then
                                    CierraPantalla("./consultaAgencias.aspx", False)
                                Else
                                    CierraPantalla("./consultaAgencias.aspx")
                                End If
                            Else
                                MensajeError(objAge.ErrorAgencia)
                            End If
                        Else
                            'BUG-PC-14 2016-11-24 MAUT Se manda mensaje especificos para validar los campos
                            'BUG-PC-60:MPUESTO:13/05/2017 CORRECCION DE VALIDACION DE NUMERO DE CUENTA Y TITULAR.
                            If Trim(hdnTitularCuenta.Value) = "" Then 'If Trim(TextTCuenta.Text) = "" Then
                                'El titular de la cuenta solo puede ser llenado por el proceso del botón validar
                                MensajeError("Debe validar la cuenta de la agencia para obtener al titular.")
                            ElseIf TextCuentaA.Text.Length < 10 Then
                                MensajeError("El numero de cuenta es incorrecto.")
                            ElseIf Regex.IsMatch(TextRFC.Text.Trim, "^([A-Z\s]{3})\d{6}$") = False AndAlso Regex.IsMatch(TextRFC.Text.Trim, "^([A-Z\s]{3})\d{6}([A-Z\w]{3})$") = False Then
                                'Si el RFC no cuenta con el formato correcto, se manda error especifico
                                MensajeError("El formato del RFC no es válido.")
                            ElseIf Len(TextUdis.Text) >= 6 AndAlso Not (IsNumeric(TextUdis.Text) AndAlso (CInt(TextUdis.Text) < 0 Or CInt(TextUdis.Text) > 100)) Then
                                'Se válida que el campo Udis sea numerico y entre 0 y 100
                                MensajeError("Formato de Udis no es válido.")
                                'fin validacion de Udis
                                'validacion de Lada
                            ElseIf InStr(textLDist.Text, "+") <> 0 And InStr(textLDist.Text, "+") <> 1 Then
                                MensajeError("Formato de Clave Lada incorrecto.")
                            Else
                                'Error genérico de llenado de campos
                                'MensajeError("Todos los campos marcados con * son obligatorios.")
                                MensajeError(strErr)
                            End If

                            current_tab.Value = 0
                        End If
                    Else
                        'BUG-PC-38 MAUT 23/01/2017 Se despliega mensaje de error
                        'Error genérico de validacion de email
                        'MensajeError("Ingresar un Correo Electrónico válido.")
                        MensajeError(strErr)
                        current_tab.Value = 3
                    End If
                Else
                    'BUG-PC-38 MAUT 23/01/2017 Se despliega mensaje de error
                    'Error genérico de validacion de telefonos
                    'MensajeError("El numero de Telefono es incorrecto")
                    MensajeError(strErr)
                    current_tab.Value = 2
                End If
            Else
                MensajeError("Todos los campos marcados con * son obligatorios.")
                current_tab.Value = 1
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Public Function ValidaCampos() As Boolean
        ValidaCampos = False

        If cmbMarca.SelectedValue = "" Then Exit Function
        'If ChkComAgencia.Checked = False And ChkComVendedor.Checked = False Then Exit Function
        If cmbDivision.SelectedValue = "" Then Exit Function
        If cmbGrupo.SelectedValue = "" Then Exit Function
        If cmbAlianza.SelectedValue = "" Then Exit Function
        If cmbEstatus.SelectedValue = "" Then Exit Function
        'If CmbEsquema.SelectedValue = 0 Then Exit Function
        If Trim(txtNom.Text) = "" Then
            strErr = "Por favor agregar un nombre agencia valido."
            Exit Function
        End If
        If Trim(TextCuentaA.Text) = "" Then Exit Function
        If Trim(TextRFC.Text) = "" Then Exit Function
        'BUG-PC-60:MPUESTO:13/05/2017 CORRECCION DE VALIDACION DE NUMERO DE CUENTA Y TITULAR.
        If Trim(hdnTitularCuenta.Value) = "" Then Exit Function 'If Trim(TextTCuenta.Text) = "" Then Exit Function

        'BUG-PC-38 MAUT 23/01/2017 Se hace obligatorio el campo
        If Trim(TextRespFact.Text) = "" Then
            strErr = "Por favor ingresa un responsable de facturas"
            Exit Function
        End If

        If (txtIniVig.Text <> "" Or IsNothing(txtIniVig.Text)) Then
            Dim test As Date
            If Date.TryParse(txtIniVig.Text, test) Then
                If Date.TryParseExact(txtIniVig.Text.ToString(), "yyyy-mm-dd", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, test) Then
                Else
                    txtIniVig.Text = ""
                    strErr = "Fecha Introducida no valida (ingresar AAAA-MM-dd)."
                    Exit Function
                End If
            Else
                txtIniVig.Text = ""
                strErr = "Fecha Introducida no valida."
                Exit Function
            End If
        Else
            txtIniVig.Text = ""
            strErr = "Ingresa una fecha inicio vigencia valida."
            Exit Function
        End If
        'BUG-PC-14 2016-11-24 MAUT Se validan Udis
        If Trim(TextUdis.Text) <> "" Then
            If IsNumeric(TextUdis.Text) Then
                If CInt(TextUdis.Text) < 0 Or CInt(TextUdis.Text) > 100 Then
                    Exit Function
                End If
            Else
                strErr = "'% Udis' introducido no valido."
                TextUdis.Text = ""
                Exit Function
            End If
            'Else
            'Exit Function
        End If

        'Dividendos
        If Trim(TextDivi.Text) <> "" Then
            If Not IsNumeric(TextDivi.Text) Then
                strErr = "Formato de Dividendos no valido."
                TextDivi.Text = ""
                Exit Function
            End If
        End If

        'Bono Vendedor
        If Trim(TextBonoV.Text) <> "" Then
            If Not IsNumeric(TextBonoV.Text) Then
                strErr = "Formato de Bono Vendedor no valido."
                TextBonoV.Text = ""
                Exit Function
            End If
        End If

        'Pago F&I
        If Trim(TextPagoFI.Text) <> "" Then
            If Not IsNumeric(TextPagoFI.Text) Then
                strErr = "Formato de Pago al F&I no valido."
                TextPagoFI.Text = ""
                Exit Function
            End If
        End If

        'Seguro Regalado 
        If Trim(TextSegReg.Text) <> "" Then
            If Not IsNumeric(TextSegReg.Text) Then
                strErr = "Formato de Seguro Regalado no valido."
                TextSegReg.Text = ""
                Exit Function
            End If
        End If

        'Regalias
        If Trim(TextRegalias.Text) <> "" Then
            If Not IsNumeric(TextRegalias.Text) Then
                strErr = "Formato de Regalias no valido."
                TextRegalias.Text = ""
                Exit Function
            End If
        End If
        'Validacion de txtFinVig ni  no este vacia solo cuando el cmbstatus esta en inactivo
        If (cmbEstatus.SelectedValue = 3) Then
            If Trim(txtFinVig.Text) = "" Then
                strErr = "Al inactivar agencia se debe seleccionar una fecha fin vigencia"
                txtFinVig.Focus()
                Exit Function
            End If
            If cmbMotivoBaja.SelectedValue = 2 Then
                strErr = "Debe seleccionar un motivo de baja para poder continuar"
                cmbMotivoBaja.Focus()
                Exit Function
            End If
            If (txtFinVig.Text <> "" Or IsNothing(txtFinVig.Text)) Then
                Dim test As Date
                If Date.TryParse(txtFinVig.Text, test) Then
                    If Date.TryParseExact(txtFinVig.Text.ToString(), "yyyy-mm-dd", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, test) Then
                    Else
                        txtFinVig.Text = ""
                        strErr = "Fecha fin Vigencia no válida (ingresar AAAA-MM-dd)."
                        Exit Function
                    End If
                Else
                    txtFinVig.Text = ""
                    strErr = "Fecha fin Vigencia no válida."
                    Exit Function
                End If
            Else
                txtFinVig.Text = ""
                strErr = "Ingresa una fecha fin Vigencia vigencia válida."
                Exit Function
            End If
        End If


        'BUG-PC-14 2016-11-24 MAUT Se valida formato del RFC
        If Regex.IsMatch(TextRFC.Text.Trim, "^([A-Z\s]{3})\d{6}$") = False AndAlso Regex.IsMatch(TextRFC.Text.Trim, "^([A-Z\s]{3})\d{6}([A-Z\w]{3})$") = False Then
            Exit Function
        End If

        'BUG-PC-14 2016-11-24 MAUT Se valida formato de Lada
        If InStr(textLDist.Text, "+") <> 0 And InStr(textLDist.Text, "+") <> 1 Then
            Exit Function
        End If

        ValidaCampos = True
    End Function

    Private Function ValidaCamposDomicilio() As Boolean
        ValidaCamposDomicilio = False
        If cmbTDomicilio.SelectedValue = "" Then Exit Function
        If cmbTDomicilio.SelectedValue <> 1 Then
        If cmbEstados.SelectedValue = "" Then Exit Function
        If cmbCiudadCliente.SelectedValue = "" Then Exit Function
        If cmbDelMunCliente.SelectedValue = "" Then Exit Function
        If cmbColoniaCliente.SelectedValue = "" Then Exit Function
        If Trim(TextCodPos.Text) = "" Then Exit Function
        If Trim(txtDomi.Text) = "" Then Exit Function
        End If
        ValidaCamposDomicilio = True
    End Function

    Private Function ValidaCamposTelefono() As Boolean
        ValidaCamposTelefono = False
        Dim objTTelefono As New SNProcotiza.clsTipoTelefono
        'Dim dv As DataView
        Dim dt As New DataTable
        Dim dtsRes As New DataSet
        Dim strCadena As String
        Dim arr As String()
        Dim i As Integer

        'BUG-PC-38 MAUT 23/01/2017 Se envia mensaje de error
        strErr = "El numero de Telefono es incorrecto"

        If cmbTipoTel.SelectedValue = "" Then Exit Function
        'If Trim(textLDist.Text) = "" Then Exit Function
        'If Trim(TextLada.Text) = "" Then Exit Function

        'BUG-PC-14 2016-11-25 MAUT Si se selecciona sin telefono, no lo pide obligatorio
        If cmbTipoTel.SelectedValue <> 0 Then
            'BUG-PC-38 MAUT 23/01/2017 Se envian errores especificos
            'If Trim(TextContacto.Text) = "" Then
            'strErr = "Todos los campos marcados con * son obligatorios."
            'Exit Function
            'End If

            If Trim(txtTel.Text) = "" Then
                strErr = "Todos los campos marcados con * son obligatorios."
                Exit Function
            End If
        End If
        'If Trim(TextExt.Text) = "" Then Exit Function

        If Trim(txtTel.Text) <> "" Then
            'BUG-PC-38 MAUT 23/01/2017 Se valida longitud de telefono
            If Len(Trim(txtTel.Text)) < 10 Then Exit Function
        End If
        objTTelefono.IDEstatus = 2
        objTTelefono.IDTTelefono = cmbTipoTel.SelectedValue
        dtsRes = objTTelefono.ManejaTTelefono(1)
        strErr = objTTelefono.ErrorTTelefono
        'dv = New DataView(dtsRes.Tables(0))

        If Trim$(strErr) = "" Then
            If dtsRes.Tables(0).Rows.Count > 0 Then
                strCadena = dtsRes.Tables(0).Rows(0).Item("EXPRESION").ToString
            Else
                MensajeError("NO SE ENCONTRO INFORMACION DE TIPO TELEFONO")
                Exit Function
            End If
        Else
            MensajeError(strErr)
            Exit Function
        End If
        arr = txtTel.Text.Trim.Split(";")
        For i = 0 To arr.Length - 1
            If Not arr(i).Contains("@") Then ValidaCamposTelefono = False
            If arr(i).Split("@").Length > 2 Then
                ValidaCamposTelefono = False
                'ElseIf Regex.IsMatch(arr(i), "([0-9a-z][\W]*[0-9a-z])(@{1,2})([0-9a-z][-\w]*[0-9a-z]*\.)+([0-9a-z][-\w]*[a-z0-9])$") Then
            ElseIf Regex.IsMatch(arr(i), strCadena) Then
                ValidaCamposTelefono = True
                'ElseIf Regex.IsMatch(arr(i), "([0-9a-z][\W]*[0-9a-z])*[\w]*(@{1,2})([0-9a-z][-\w]*[0-9a-z]*\.)+([0-9a-z][-\w]*[a-z0-9])$") Then
            ElseIf Regex.IsMatch(arr(i), strCadena) Then
                ValidaCamposTelefono = True 'ECASH-B-2905 * KM
            Else
                ValidaCamposTelefono = False
            End If
        Next

        ValidaCamposTelefono = True
    End Function

    Private Function validarCorreoElectronico() As Boolean
        Dim objTTelefono As New SNProcotiza.clsTipoTelefono
        'Dim dv As DataView
        Dim dt As New DataTable
        Dim dtsRes As New DataSet
        Dim strCadena As String
        Dim arr As String()
        'Dim arr2 As String()
        Dim i As Integer
        If Trim(TextEmailContact.Text) <> "" Then
            validarCorreoElectronico = False

            strErr = "Ingresar un Correo Electrónico válido."

            Try
                objTTelefono.IDEstatus = 2
                objTTelefono.IDTTelefono = 4
                dtsRes = objTTelefono.ManejaTTelefono(1)
                strErr = objTTelefono.ErrorTTelefono
                'dv = New DataView(dtsRes.Tables(0))

                If Trim$(strErr) = "" Then
                    If dtsRes.Tables(0).Rows.Count > 0 Then
                        strCadena = dtsRes.Tables(0).Rows(0).Item("EXPRESION").ToString
                    Else
                        MensajeError("NO SE ENCONTRO INFORMACION DE CORREO ELECTRONICO")
                        Exit Function
                    End If
                Else
                    MensajeError(strErr)
                    Exit Function
                End If

                'BUG-PC-38 MAUT 23/01/2017 Se envian mensajes especificos para campos obligatorios
                If Trim(TextEmailContact.Text) = "" Then
                    strErr = "Por favor ingrese un nombre de contacto de email."
                    Exit Function
                End If
                If Trim(TextEmail.Text) = "" Then
                    strErr = "Por favor ingrese un email"
                    Exit Function
                End If

                arr = TextEmail.Text.Trim.Split(";")
                For i = 0 To arr.Length - 1
                    If Not arr(i).Contains("@") Then validarCorreoElectronico = False
                    If arr(i).Split("@").Length > 2 Then
                        validarCorreoElectronico = False
                        'ElseIf Regex.IsMatch(arr(i), "([0-9a-z][\W]*[0-9a-z])(@{1,2})([0-9a-z][-\w]*[0-9a-z]*\.)+([0-9a-z][-\w]*[a-z0-9])$") Then
                    ElseIf Regex.IsMatch(arr(i), strCadena) Then
                        validarCorreoElectronico = True
                        'ElseIf Regex.IsMatch(arr(i), "([0-9a-z][\W]*[0-9a-z])*[\w]*(@{1,2})([0-9a-z][-\w]*[0-9a-z]*\.)+([0-9a-z][-\w]*[a-z0-9])$") Then
                        '    validarCorreoElectronico = True 'ECASH-B-2905 * KM
                    Else
                        validarCorreoElectronico = False
                    End If
                Next
                'For i = 0 To arr.Length - 1
                '    If arr(i).Trim <> String.Empty Then
                '        If Not arr(i).Contains("@") Then Return 1
                '        arr2 = arr(i).Split("@")
                '        If arr2.Length > 2 Then Return 2
                '        If i < arr.Length - 1 And Not arr(i).Contains(";") Then Return 2
                '    End If
                'Next

            Catch ex As Exception
                validarCorreoElectronico = False
            End Try
        Else
            validarCorreoElectronico = True
        End If
    End Function

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaAgencias.aspx")
    End Sub

    Protected Sub TextCodPos_TextChanged(sender As Object, e As System.EventArgs) Handles TextCodPos.TextChanged

        strErr = String.Empty

        If Len(TextCodPos.Text) < 5 Then
            strErr = "El Código Postal debe tener 5 carácteres."
            MensajeError(strErr)
            TextCodPos.Text = ""
            TextCodPos.Focus()
            cmbEstados.Items.Clear()
            cmbCiudadCliente.Items.Clear()
            cmbDelMunCliente.Items.Clear()
            cmbColoniaCliente.Items.Clear()
            txtDomi.Text = ""
            TextNoext.Text = ""
            TextNoint.Text = ""
            Exit Sub
        End If

        CargaUbicacion(TextCodPos.Text.Trim, 1)
        If strErr <> "" Then
            MensajeError(strErr)
            Exit Sub
        End If

    End Sub

    Protected Function CargaUbicacion(codpost As String, opcion As Integer) As Boolean
        Try
            CargaUbicacion = False

            Dim _ObjDs As New DataSet
            Dim _ObjColds As New DataSet

            Dim _ObjCodPost As New clsCodigoPostal
            Dim _ObjCol As New clsCodigoPostal

            _ObjCodPost._strCPO_CL_CODPOSTAL = codpost
            _ObjDs = _ObjCodPost.ManejaCotPost(6)

            Select Case opcion
                Case 1 ''Cliente
                    If _ObjDs.Tables(0).Rows.Count > 0 Then

                        cmbEstados.DataValueField = "EFD_CL_CVE"
                        cmbEstados.DataTextField = "EFD_DS_ENTIDAD"
                        cmbEstados.DataSource = _ObjDs.Tables(0)
                        cmbEstados.DataBind()

                        cmbCiudadCliente.DataValueField = "CIU_CL_CIUDAD"
                        cmbCiudadCliente.DataTextField = "CIU_NB_CIUDAD"
                        cmbCiudadCliente.DataSource = _ObjDs.Tables(0)
                        cmbCiudadCliente.DataBind()

                        cmbDelMunCliente.DataValueField = "MUN_CL_CVE"
                        cmbDelMunCliente.DataTextField = "MUN_DS_MUNICIPIO"
                        cmbDelMunCliente.DataSource = _ObjDs.Tables(0)
                        cmbDelMunCliente.DataBind()

                    Else
                        strErr = "El Código Postal no existe."
                        MensajeError(strErr)
                        TextCodPos.Text = ""
                        TextCodPos.Focus()
                        cmbEstados.Items.Clear()
                        cmbCiudadCliente.Items.Clear()
                        cmbDelMunCliente.Items.Clear()
                        cmbColoniaCliente.Items.Clear()
                        txtDomi.Text = ""
                        TextNoext.Text = ""
                        TextNoint.Text = ""
                        Exit Function
                    End If

                    _ObjCol._strCPO_CL_CODPOSTAL = TextCodPos.Text
                    _ObjColds = _ObjCodPost.ManejaCotPost(7)

                    If _ObjColds.Tables(0).Rows.Count > 0 Then
                        cmbColoniaCliente.DataSource = _ObjColds.Tables(0)
                        cmbColoniaCliente.DataValueField = "CPO_FL_CP"
                        cmbColoniaCliente.DataTextField = "CPO_DS_COLONIA"
                        cmbColoniaCliente.DataBind()
                        current_tab.Value = 1
                    Else
                        strErr = "No se pudo cargar la información de las Colonias."
                        If strErr <> "" Then
                            'MensajeError(strErr)
                            current_tab.Value = 1
                        End If
                        Exit Function
                    End If
            End Select
            CargaUbicacion = True

        Catch ex As Exception
            strErr = ex.Message
            CargaUbicacion = False
            Exit Function
        End Try

    End Function

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

        Dim IdAgencia As Integer = Val(Request("idAgencia"))

        'If rbAlianza.Checked = True Then
        '    Alianza = cmbAlianza.SelectedValue
        '    Me.cmbGrupo.SelectedValue = 0
        '    Me.cmbDivision.SelectedValue = 0
        'ElseIf rbGrupo.Checked = True Then
        '    Grupo = cmbGrupo.SelectedValue
        '    Me.cmbAlianza.SelectedValue = 0
        '    Me.cmbDivision.SelectedValue = 0
        'ElseIf rbDivision.Checked = True Then
        '    Division = cmbDivision.SelectedValue
        '    Me.cmbAlianza.SelectedValue = 0
        '    Me.cmbGrupo.SelectedValue = 0
        'End If

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

            'BUG-PC-14 2016-11-25 MAUT Si el filtro está vacío, no elimina de la base
            If Filtro(0) <> "" Then
                objBonoCC.NoCreditos = Filtro(0)
                objBonoCC.Importe = Filtro(1)
                objBonoCC.IDAgencia = IdAgencia

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
        Dim count As Integer = 0
        Dim repetidos As Integer = 0

        'BUG-PC-14 2016-11-25 MAUT Se hacen validaciones para agregar bono por crédito colocado

        For Each row As GridViewRow In grvBonoCC.Rows
            'Dim Importe As TextBox = TryCast(row.FindControl("TextBox1"), TextBox)
            Dim Importe As TextBox = TryCast(row.FindControl("txtImporte"), TextBox)
            Dim NoCreditos As TextBox = TryCast(row.FindControl("txtNoCreditos"), TextBox)

            Dim numCreditos As Integer
            Dim linea As Integer = row.RowIndex

            'Se valida que el valor de no este vacio
            If NoCreditos.Text = "" Then
                MensajeError("El valor para # Créditos colocados no puede estar vacío")

                    Exit Sub
                End If
                If Not IsNumeric(NoCreditos.Text) Then
                    MensajeError("El valor para # Créditos no es valido")
                Exit Sub
            End If

            'Se valida que el valor sea mayor a 0
            If CInt(NoCreditos.Text) <= 0 Then
                MensajeError("El valor para # Créditos colocados no puede ser 0")
                Exit Sub
            End If

            'Se cambia la validacion para cada registro
            If Importe.Text.Trim.Length > 0 Then
                If IsNumeric(Importe.Text) Then
                    If CDbl(Importe.Text) <= 0 Then
                        MensajeError("El importe no puede ser $0.00")
                        Exit Sub
                    End If
                Else
                    MensajeError("El importe introducido es invalido")
                    Exit Sub
                End If
            Else
                MensajeError("El importe no puede ser $0.00")
                Exit Sub
            End If


            'se toma el valor de los creditos colocados
            numCreditos = CInt(NoCreditos.Text)
            For Each subrow As GridViewRow In grvBonoCC.Rows
                'se evalua que el valor de los creditos colocados sea diferente de los que ya existen
                'no se evalua si es el mismo registro
                If linea <> subrow.RowIndex Then
                    Dim Creditos As TextBox = TryCast(subrow.FindControl("txtNoCreditos"), TextBox)
                    'si el valor ya fue ingresado anteriormente sale del for
                        If (Creditos.Text) <> "" And IsNumeric(Creditos.Text) Then
                    If numCreditos = Creditos.Text Then
                        repetidos = repetidos + 1
                        Exit For
                    End If
                        Else
                            Exit For
                End If
                    End If
            Next

        Next


        'si hubo al menos un registro que se repitió, no se guarda el registro
        If repetidos > 0 Then
            LimpiaError()
            MensajeError("No se puede repetir valor para # Créditos Colocados")
            Exit Sub
        End If

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

    Private Function GuardaBonoCC() As Boolean
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
                    If objNoCreditos.Text = "" Then
                        MensajeError("El valor para # Créditos colocados no puede estar vacío")
                        GuardaBonoCC = False
                        Exit Function
                    End If
                    If Not IsNumeric(objNoCreditos.Text) Then
                        MensajeError("El valor para # Créditos no es valido")
                        GuardaBonoCC = False
                        Exit Function
                    End If

                    'Se valida que el valor sea mayor a 0
                    If CInt(objNoCreditos.Text) <= 0 Then
                        MensajeError("El valor para # Créditos colocados no puede ser 0")
                        GuardaBonoCC = False
                        Exit Function
                    End If

                    'Se cambia la validacion para cada registro
                    If objImporte.Text.Trim.Length > 0 Then
                        If IsNumeric(objImporte.Text) Then
                            If CDbl(objImporte.Text) <= 0 Then
                                MensajeError("El importe no puede ser $0.00")
                                GuardaBonoCC = False
                                Exit Function
                            End If
                        Else
                            MensajeError("El importe introducido es invalido")
                            GuardaBonoCC = False
                            Exit Function
                        End If
                    Else
                        MensajeError("El importe no puede ser $0.00")
                        GuardaBonoCC = False
                        Exit Function
                    End If
                    objBonoCC.IDAlianza = -1
                    objBonoCC.IDGrupo = -1
                    objBonoCC.IDDivision = -1
                    objBonoCC.IDAgencia = Val(Request("idAgencia"))
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
        Dim IdAgencia As Integer = Val(Request("idAgencia"))

        'If rbAlianza.Checked = True Then
        '    Alianza = cmbAlianza.SelectedValue
        '    Me.cmbGrupo.SelectedValue = 0
        '    Me.cmbDivision.SelectedValue = 0
        'ElseIf rbGrupo.Checked = True Then
        '    Grupo = cmbGrupo.SelectedValue
        '    Me.cmbAlianza.SelectedValue = 0
        '    Me.cmbDivision.SelectedValue = 0
        'ElseIf rbDivision.Checked = True Then
        '    Division = cmbDivision.SelectedValue
        '    Me.cmbAlianza.SelectedValue = 0
        '    Me.cmbGrupo.SelectedValue = 0
        'End If

        'Alianza = 0
        Dim objBonoCC As New clsAgencias
        Dim dsBonoCC As DataSet

        objBonoCC.IDAlianza = Alianza
        objBonoCC.IDGrupo = Grupo
        objBonoCC.IDDivision = Division
        objBonoCC.IDAgencia = IdAgencia



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

    Public Function AltaAgenciaWS(IdAgencia As Integer) As Boolean

        Dim result = False
        Dim messageError As String = String.Empty
        Dim Agency As Agency = New Agency()

        Agency.agencyId = IdAgencia
        Agency.name = txtNom.Text
        Agency.fiscalId = TextRFC.Text
        Agency.divisionId = cmbDivision.SelectedValue
        Agency.phoneNumber = txtTel.Text
        Agency.email = TextEmail.Text

        If (cmbTDomicilio.SelectedValue <> 1) Then
            'Modificacion para cuando no tenga
            If cmbCiudadCliente.SelectedValue = 500 Then
                Agency.address.city = cmbDelMunCliente.SelectedItem.Text
            Else
        Agency.address.city = cmbCiudadCliente.SelectedItem.Text
            End If
            'Agency.address.city = cmbCiudadCliente.SelectedItem.Text

        Agency.address.neightborthood = cmbColoniaCliente.SelectedItem.Text
        Agency.address.streetNumber = txtDomi.Text + " " + TextNoext.Text
        Agency.address.zipCode = Trim(TextCodPos.Text)
        Agency.address.state = cmbEstados.SelectedValue
        Else
            Agency.address.city = ""
            Agency.address.neightborthood = ""
            Agency.address.streetNumber = ""
            Agency.address.zipCode = ""
            Agency.address.state = ""
        End If

        Agency.iCarDetail.car.usedCar = "true"
        Agency.iCarDetail.car.newCar = "true"
        Agency.iCarDetail.car.tax = "16"
        Agency.iCarDetail.car.makerId = cmbMarca.SelectedValue

        Agency.extendedData.paymentPercentageSellerId = IIf(ChkComVendedor.Checked = True, 1, 0)
        Agency.extendedData.paymentPercentageAgencyId = IIf(ChkComAgencia.Checked = True, 1, 0)
        Agency.account.accountNumber = TextCuentaA.Text.Substring(10) & New String(" "c, 10) 'TextCuentaA.Text

        result = New clsAgenciasWS().InvokeNewAgencyWS(Agency, messageError)

        If Not result Then
            MensajeError("Error WS - " & messageError)
        Else
            LimpiaError()
        End If

        Return result
    End Function

    Public Function ModificaAgenciaWS(IdAgencia As Integer, ByRef strError As String) As Boolean
        Dim result = False
        Dim messageError As String = String.Empty
        Dim Agency As Agency = New Agency()

        Agency.agencyId = IdAgencia
        Agency.name = txtNom.Text
        Agency.fiscalId = TextRFC.Text
        Agency.divisionId = cmbDivision.SelectedValue
        Agency.phoneNumber = txtTel.Text
        Agency.email = TextEmail.Text

        If (cmbTDomicilio.SelectedValue <> 1) Then
            'Modificacion para cuando no tenga
            If cmbCiudadCliente.SelectedValue = 500 Then
                Agency.address.city = cmbDelMunCliente.SelectedItem.Text
            Else
        Agency.address.city = cmbCiudadCliente.SelectedItem.Text
            End If

            'Agency.address.city = cmbCiudadCliente.SelectedItem.Text
        Agency.address.neightborthood = cmbColoniaCliente.SelectedItem.Text
        Agency.address.streetNumber = txtDomi.Text + " " + TextNoext.Text
        Agency.address.zipCode = Trim(TextCodPos.Text)
        Agency.address.state = cmbEstados.SelectedValue
        Else
            Agency.address.city = ""
            Agency.address.neightborthood = ""
            Agency.address.streetNumber = ""
            Agency.address.zipCode = ""
            Agency.address.state = ""
        End If


        Agency.iCarDetail.car.usedCar = "true"
        Agency.iCarDetail.car.newCar = "true"
        Agency.iCarDetail.car.tax = "16"
        Agency.iCarDetail.car.makerId = cmbMarca.SelectedValue
        Agency.iCarDetail.car.deliveryManagerName = "PRUEBAS_MODIFACION"

        Agency.extendedData.agreementStartDate = txtIniVig.Text
        Agency.extendedData.agreementEndDate = txtFinVig.Text

        Agency.extendedData.paymentPercentageSellerId = IIf(ChkComVendedor.Checked = True, 1, 0)
        Agency.extendedData.paymentPercentageAgencyId = IIf(ChkComAgencia.Checked = True, 1, 0)

        Agency.extendedData.managerId = 21112
        Agency.extendedData.floorAdvisorId = 7777
        Agency.extendedData.assistantId = 7777
        Agency.extendedData.chiefPromoterId = 7777
        Agency.extendedData.floorManagerId = 7777


        Agency.account.accountNumber = TextCuentaA.Text.Substring(10) & New String(" "c, 10) 'TextCuentaA.Text

        result = New clsAgenciasWS().InvokeModifyAgencyWS(Agency, messageError)

        If Not result Then
            MensajeError("Error WS - " & messageError)
            Else
            LimpiaError()
            End If

        Return result
    End Function

    Public Function EliminaAgenciaWS(IdAgencia As Integer) As Boolean

        EliminaAgenciaWS = False
        Dim Agency As Agency2 = New Agency2()

        Agency.agencyId = IdAgencia
        Agency.extendedData.statusId = "false"
        Agency.extendedData.comment = cmbMotivoBaja.SelectedItem.Text
        Agency.extendedData.causeId.id = "2"


        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonBODY As String = serializer.Serialize(Agency)

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

        Dim restGT As WCF.RESTful = New WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("Agency") + "deleteAgency/"

        restGT.buscarHeader("ResponseWarningDescription")
        Dim jsonResult As String = restGT.ConnectionDelete(userID, iv_ticket1, jsonBODY)

        Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim alert = srrSerialer.Deserialize(Of msjerr)(jsonResult)
        Dim STR2 As String = restGT.MensajeError

        'BUG-PC-14 2016-11-25 MAUT Se agrega la leyenda Error WS a los errores por parte del Web Services
        If restGT.IsError Then
            MensajeError("Error WS - " & IIf(STR2 = "", alert.message & " Estatus: " & alert.status & ".", STR2))
            EliminaAgenciaWS = False
            Exit Function
        End If

        Dim str As String = restGT.valorHeader
        EliminaAgenciaWS = True


    End Function

    'BUG-PC-14 2016-11-25 MAUT Se agrega funcion al boton validar
    Public Sub btnValidarC_Click(sender As Object, e As System.EventArgs) Handles BtnValidarC.Click
        If (TextCuentaA.Text <> "" And Not IsNothing(TextCuentaA.Text)) Then
            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

            'BUG-PC-60:MPUESTO:13/05/2017 CORRECCION DE VALIDACION DE NUMERO DE CUENTA Y TITULAR.
            Me.hdnTitularCuenta.Value = "" 'Me.TextTCuenta.Text = ""
            LimpiaError()

            Dim rest As WCF.RESTful = New WCF.RESTful()
            rest.buscarHeader("ResponseWarningDescription")

            rest.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("Customer") + "?$filter=(accountNumber==" + Me.TextCuentaA.Text + ")"

            Dim respuesta As String = rest.ConnectionGet(userID, iv_ticket1, String.Empty)

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim jresul2 As Customer = serializer.Deserialize(Of Customer)(respuesta)

            Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim alert = srrSerialer.Deserialize(Of msjerr)(respuesta)

            'BUG-PC-14 2016-11-25 MAUT Se agrega la leyenda Error WS a los errores por parte del Web Services
            'BUG-PC-38 MAUT 23/01/2017 Se pone focus
            If rest.IsError Then
                If rest.MensajeError <> "" Then
                    MensajeError("Error WS - " & rest.MensajeError)
                    Me.TextCuentaA.Focus()
                    current_tab.Value = 0
                Else
                    MensajeError("Error WS - " & alert.message & " Estatus: " & alert.status & "." & "Mensaje:" & rest.MensajeError)
                    Me.TextCuentaA.Focus()
                    current_tab.Value = 0
                End If
                Exit Sub
            Else
                If rest.valorHeader <> "" And jresul2.Person.id Is Nothing Then
                    MensajeError("Error WS - " & rest.valorHeader)
                    Me.TextCuentaA.Focus()
                    current_tab.Value = 0
                Else
                    Me.TextTCuenta.Text = jresul2.Person.name + " " + jresul2.Person.lastName + " " + jresul2.Person.mothersLastName
					'BUG-PC-64:MPUESTO:13/05/2017 CORRECCION DE VALIDACION DE NUMERO DE CUENTA Y TITULAR.
                    Me.hdnCuentaAgencia.Value = TextCuentaA.Text
                    current_tab.Value = 0
                End If
            End If
        Else
            MensajeError("Por favor, Ingresa cuenta de agencia antes de validar.")
            Me.TextCuentaA.Focus()
            current_tab.Value = 0
        End If


    End Sub

    Protected Sub cmbTipoTel_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoTel.SelectedIndexChanged

        'BUG-PC-14 2016-11-25 MAUT Si se selecciona sin telefono, deshabilita los campos del telefono
        If cmbTipoTel.SelectedValue = 0 Then
            TextContacto.Text = ""
            TextContacto.Enabled = False
            textLDist.Text = ""
            textLDist.Enabled = False
            TextLada.Text = ""
            TextLada.Enabled = False
            txtTel.Text = ""
            txtTel.Enabled = False
            TextExt.Text = ""
            TextExt.Enabled = False
        Else
            TextContacto.Enabled = True
            textLDist.Enabled = True
            TextLada.Enabled = True
            txtTel.Enabled = True
            TextExt.Enabled = True
        End If
        current_tab.Value = 2
    End Sub

    ''BUG-PC-27 MAUT 26/12/2016 Solo se puede seleccionar un tipo de comision
    'Protected Sub ChkComAgencia_CheckedChanged(sender As Object, e As System.EventArgs) Handles ChkComAgencia.CheckedChanged
    '    If ChkComAgencia.Checked Then
    '        ChkComVendedor.Checked = False
    '    End If
    'End Sub

    'Protected Sub ChkComVendedor_CheckedChanged(sender As Object, e As System.EventArgs) Handles ChkComVendedor.CheckedChanged
    '    If ChkComVendedor.Checked Then
    '        ChkComAgencia.Checked = False
    '    End If
    'End Sub

    Protected Sub cmbEstatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEstatus.SelectedIndexChanged
        If cmbEstatus.SelectedValue = 3 Then
            lblFinVig.Text = "* Fin Vigencia: "
            txtFinVig.Text = Date.Now.Date.ToString("yyyy-MM-dd")
            txtFinVig.Enabled = False
            cmbMotivoBaja.Enabled = True
            cmbMotivoBaja.SelectedValue = 2
            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "EnaFechaFin", "pickerSettins2();", True)
        Else
            lblFinVig.Text = "Fin Vigencia: "
            txtFinVig.Enabled = False
            txtFinVig.Text = ""
            cmbMotivoBaja.Enabled = False
            cmbMotivoBaja.SelectedValue = 2

        End If
    End Sub

    Protected Sub cmbTDomicilio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTDomicilio.SelectedIndexChanged
        If cmbTDomicilio.SelectedValue = 1 Then
            TextCodPos.Text = ""
            cmbEstados.Items.Clear()
            cmbCiudadCliente.Items.Clear()
            cmbDelMunCliente.Items.Clear()
            cmbColoniaCliente.Items.Clear()
            txtDomi.Text = ""
            TextNoext.Text = ""
            TextNoint.Text = ""
            TextCodPos.Enabled = False
            cmbEstados.Enabled = False
            cmbCiudadCliente.Enabled = False
            cmbDelMunCliente.Enabled = False
            cmbColoniaCliente.Enabled = False
            txtDomi.Enabled = False
            TextNoext.Enabled = False
            TextNoint.Enabled = False
        Else
            TextCodPos.Enabled = True
            cmbEstados.Enabled = True
            cmbCiudadCliente.Enabled = True
            cmbDelMunCliente.Enabled = True
            cmbColoniaCliente.Enabled = True
            txtDomi.Enabled = True
            TextNoext.Enabled = True
            TextNoint.Enabled = True
        End If
        current_tab.Value = 1
    End Sub

End Class
