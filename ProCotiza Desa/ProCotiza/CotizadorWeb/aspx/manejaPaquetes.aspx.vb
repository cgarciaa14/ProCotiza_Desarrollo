'BBVA-P-412: 06-07-2016: AMR: RQ02,RQ03,RQ04,RQ05,RQ06,RQ08 SE AGREGA FUNCIONALIDAD PARA LOS CAMPOS NUEVOS.
'BBVA-P-412:AVH:29/07/2016: RQ 15: SE CAMBIA LA COLUMNA DE LLENADO
'BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.
'BBVA-P-412: 25-08-2016: AMR: RQCOT-01 Ajustes a la pantalla de Cotización.
'BBVA-P-412 RQ06: AMR: 20/09/2016 Administración de Planes de Financiamiento: Promociones
'BBVA-P-412 RQ WSA. WS GetLoan Products (RQ06, RQ09): AMR: 25/10/2016 WS GetLoan Products
'BBV-P-412:RQ 10 AVH: 01/11/2016 Cambio de las clases de WS
'BUG-PC - 13: AMR :  23/11/2016 Se quita valor por default en los combos.
'BUG-PC-21: AMR: 28/11/2016 El nombre de la Marca se muestra el texto en Mayúsculas.
'BUG-PC-22 2016-12-02 MAUT Se válidan comisiones.
'BUG-PC-25 2016-12-15 MAUT Se validan montos máximos y mínimos.
'BUG-PC-27 MAUT 23/12/2016 Si es compra inteligente se desmarca pago irregular
'BBV-P-412:RQ LOGIN GVARGAS: 23/12/2016 Actualizacion REST Service iv_ticket & userID
'BBV-P-412:BUG-PC-35 18/01/2017 JRHM se pone valor default 0 a textbox inhabilitados de valor residual
'BUG-PC-38 MAUT 23/01/2017 Se valida el porcentaje de accesorios y check Promociones
'BUG-PC-42 JRHM 30/01/17 Se  agrega valor 80 a % tasa nominal cuando checkbox es selccionado y se selecciona un nuevo periodo
'BUG-PC-43 JRHM 01/02/17 se agregan validaciones de que en porcentajes no se agreguen valores menores a 0 y mayores a 100
'BUG-PC-44 JRHM 07/02/17 Se habilitan los campos de valor residual solo cuando el tipo de calculo sea baloon
'BUG-PC-46 JRHM 09/02/17 Se toma en cuenta el valor del combo de tipo calculo al crear un nuevo paquete
'BUG-PC-49 17/03/2017 MAPH Corrección de Validación de CheckBoxes para Cajas de Texto realizada desde Page_Load
'BBVA-P-412: 18/04/2017: CGARCIA: RQTARESQ-06  - SE AGREGA LA OPCION PARA EL COMOBO DE ESQUEMAS 
'BUG-PC-53 AMR 21/04/2017 Correcciones Multicotiza.
'BUG-PC-56:AMATA:28/04/2017:Correccion Paquete Multicoza
'BUG-PC-64: RHERNANDEZ: 18/05/17: Se cambio packageId de payload para nueve digitos con ceros a la izquierda
'BUG-PC-70: RHERNANDEZ: 30/05/17 SE MODIFICO CICLO DE VALIDACION DE PLAZOS DEBIDO A QUE SALIA ERROR AL DEJARLOS VACIOS
'RQ-INB-218: ERODRIGUEZ: 21/08/2017 Se agrego parametro de usuario al modificar un paquete.
'RQ-MN2-2: ERODRIGUEZ: 21/08/2017 Requerimiento para agregar Tasa Nominal Dos, Comision Dos, para cuando el tipo de calculo sea igual a Balloon
'RQ-MN2-2.2 ERODRIGUEZ: 27/09/2017 Se elimina porcentaje de refinanciamiento
'RQ-MN2-2.3 ERODRIGUEZ 14/09/2017 Se corrigio Tasa Nominal Dos, Comision Dos para plazos diferentes de 4 y 6.
'BUG-PC-123: CGARCIA: 10/11/2017: SE PUSIERON DATOS POR DEFAULT PRA LOS COMBOS CALENDARIO, TIPO VENCIMIENTO Y TIPO CALCULO
'BUG-PC-131: CGARCIA: 28/11/2017: SE HABILITA COLUMNAS DEL GRID DE CONDICIONES CUANDO EN RELACIONES SE TENGA ARRENDAMIENTO PURO.
'RQ-PC7: CGARCIA: 02/04/2018: SE MODIFICA EL PAYLOAD DEL WS
Imports System.Data
Imports System.Web.ApplicationServices
Imports WCF
Imports Newtonsoft.Json
Imports System.Configuration

Partial Class aspx_manejaPaquetes
    Inherits System.Web.UI.Page
    Dim strErr As String = String.Empty
    Dim strPersonas As String = String.Empty
    Dim strClasif As String = String.Empty
    Dim strTiposProd As String = String.Empty
    Dim strTiposOper As String = String.Empty
    Dim strPlazos As String = String.Empty
    Dim band As Integer = 0
    Dim ptj_tasa_ref As Double
    Dim ptj_comision_ref As Double

    Dim objCombo As New clsProcGenerales
    Dim objPaq As New SNProcotiza.clsPaquetes
    Dim objPlazos As New SNProcotiza.clsPlazo
    Dim PaqID As Integer = 0
    Dim conexion As New SDManejaBD.clsConexion()


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
            CargaCombos(4)
            CargaCombos(5)
            If Val(Request("paqId")) > 0 Then
                CargaInfo()
            Else
                txtporsubArma.Text = "0.00"
                txtporsubAgencia.Text = "0.00"
                cmbCalendario.SelectedValue = 28
                cmbTipoCalc.SelectedValue = 23
                cmbTipoVenc.SelectedValue = 102
            End If
            cmbTipoCalc_SelectedIndexChanged(cmbTipoCalc, Nothing)
        End If
        'BUG-PC-49 17/03/2017 MAPH Corrección de Validación de CheckBoxes para Cajas de Texto realizada desde Page_Load
        If chkPtjSubsidio.Checked = True Or chkSubsidio.Checked = True Then
            txtporsubArma.Enabled = True
            txtporsubAgencia.Enabled = True
        Else
            txtporsubArma.Enabled = False
            txtporsubAgencia.Enabled = False
            txtporsubArma.Text = "0.00"
            txtporsubAgencia.Text = "0.00"
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

    Private Function CargaCombos(ByVal intOpc As Integer) As Boolean
        Dim dtsRes As New DataSet

        Try
            CargaCombos = False

            Select Case intOpc

                Case 1 '' INFORMACIÓN GENERAL
                    'Combo de Moneda
                    Dim objMon As New SNProcotiza.clsMonedas
                    objMon.IDEstatus = 2
                    dtsRes = objMon.ManejaMoneda(1)
                    strErr = objMon.ErrorMoneda

                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_MONEDA", cmbMoneda, strErr)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    End If

                    'Combo Estatus
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

                    'Combo Tipo Calendario
                    dtsRes = objCombo.ObtenInfoParametros(26, strErr)
                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbCalendario, strErr)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If

                    'Combo Tipo de Cálculo
                    dtsRes = objCombo.ObtenInfoParametros(22, strErr)
                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbTipoCalc, strErr)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If

                    'BBVA-P-412
                    If cmbTipoCalc.SelectedValue = 167 Then 'RQ18
                        txtporanualidad.Text = CDbl(2.5).ToString("###,###.00")
                    Else
                        txtporanualidad.Text = CDbl(2.0).ToString("###,###.00")
                    End If

                    'Combo Tipos Vencimiento
                    dtsRes = objCombo.ObtenInfoParametros(100, strErr, True, 2)
                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbTipoVenc, strErr)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If
                    'BBVA-P-412
                    'Combo Producto UG
                    Dim objprodUG As New SNProcotiza.clsProductosUG
                    objprodUG.IDEstatus = 2
                    dtsRes = objprodUG.ManejaProductoUG(1)

                    If objprodUG.ErrorProductoUG = "" Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_PRODUCTO_UG", ddlprod, strErr)
                            If strErr.Trim <> "" Then
                                MensajeError(strErr)
                                Exit Function
                            End If
                        Else
                            MensajeError("No se encontro información de los Productos UG")
                            Exit Function
                        End If
                    Else
                        MensajeError("Error al cargar la información de los Productos UG")
                    End If

                    'Combo SubProducto UG
                    Dim objSubprodUG As New SNProcotiza.clsSubProductosUG
                    objSubprodUG.IDEstatus = 2
                    objSubprodUG.IDProductoUG = ddlprod.SelectedValue
                    dtsRes = objSubprodUG.ManejaSubProductoUG(1)

                    'AVH:RQ 15 SE CAMBIA LA COLUMNA DE LLENADO
                    If objSubprodUG.ErrorSubProductoUG = "" Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            objCombo.LlenaCombos(dtsRes, "DESCRIPCION", "ID_SUBPRODUCTO_UG", ddlsubprod, strErr)
                            If strErr.Trim <> "" Then
                                MensajeError(strErr)
                                Exit Function
                            End If
                        Else
                            MensajeError("No se encontro información de los SubProductos UG")
                            Exit Function
                        End If
                    Else
                        MensajeError("Error al cargar la información de los SubProductos UG")
                    End If

                    'Combo Esquemas
                    'Dim objEsquema As New SNProcotiza.clsEsquema
                    'objEsquema.IDEstatus = 2
                    'dtsRes = objEsquema.MAnejaEsquema(1)

                    'If objEsquema.ErrorEsquemas = "" Then
                    '    If dtsRes.Tables(0).Rows.Count > 0 Then
                    '        objCombo.LlenaCombos(dtsRes, "CDESCRIPCION", "ID_ESQUEMAS", ddlsubprod, strErr)
                    '        If strErr.Trim <> "" Then
                    '            MensajeError(strErr)
                    '            Exit Function
                    '        End If
                    '    End If
                    'End If
                    'Combo Tipo Calculo Seguro
                    'dtsRes = objCombo.ObtenInfoParametros(86, strErr)
                    'If Trim$(strErr) = "" Then
                    '    objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbTipoCalcSeg, strErr)
                    '    If Trim$(strErr) <> "" Then
                    '        MensajeError(strErr)
                    '        Exit Function
                    '    End If
                    'Else
                    '    MensajeError(strErr)
                    '    Exit Function
                    'End If

                Case 2 ''RELACIONES

                    'Grid Tipos Producto
                    objPaq.IDPaquete = Val(Request("paqId"))
                    dtsRes = objPaq.ManejaPaquete(27)

                    If Trim$(objPaq.ErrorPaquete) = "" Then
                        gdvTP.DataSource = dtsRes
                        gdvTP.DataBind()
                        gdvTP.Columns(2).Visible = False
                    Else
                        gdvTP.Columns(2).Visible = False
                        MensajeError(objPaq.ErrorPaquete)
                        Exit Function
                    End If

                    'Grid Clasificación
                    objPaq.IDPaquete = Val(Request("paqId"))
                    dtsRes = objPaq.ManejaPaquete(21)

                    If Trim$(objPaq.ErrorPaquete) = "" Then
                        gdvClasif.DataSource = dtsRes
                        gdvClasif.DataBind()
                        gdvClasif.Columns(2).Visible = False
                    Else
                        MensajeError(objPaq.ErrorPaquete)
                        gdvClasif.Columns(2).Visible = False
                        Exit Function
                    End If

                    'Grid Tipo Operación
                    objPaq.IDPaquete = Val(Request("paqId"))
                    dtsRes = objPaq.ManejaPaquete(30)

                    If Trim$(objPaq.ErrorPaquete) = "" Then
                        gdvTO.DataSource = dtsRes
                        gdvTO.DataBind()
                        gdvTO.Columns(2).Visible = False
                    Else
                        gdvTO.Columns(2).Visible = False
                        MensajeError(objPaq.ErrorPaquete)
                        Exit Function
                    End If

                    'Grid Personalidad Jurídica
                    objPaq.IDPaquete = Val(Request("paqId"))
                    dtsRes = objPaq.ManejaPaquete(18)

                    If Trim$(objPaq.ErrorPaquete) = "" Then
                        gdvPerJur.DataSource = dtsRes
                        gdvPerJur.DataBind()
                        gdvPerJur.Columns(2).Visible = False
                    Else
                        gdvPerJur.Columns(2).Visible = False
                        MensajeError(objPaq.ErrorPaquete)
                        Exit Function
                    End If

                    'BBVA-P-412
                    'Grid Canales
                    objPaq.IDPaquete = Val(Request("paqId"))
                    dtsRes = objPaq.ManejaPaquete(43)

                    If Trim$(objPaq.ErrorPaquete) = "" Then
                        gdvCanales.DataSource = dtsRes
                        gdvCanales.DataBind()
                        gdvCanales.Columns(2).Visible = False
                    Else
                        gdvCanales.Columns(2).Visible = False
                        MensajeError(objPaq.ErrorPaquete)
                        Exit Function
                    End If


                Case 3 ''TIPOS DE SEGUROS

                    'Grid Tipos Seguros
                    objPaq.IDPaquete = Val(Request("paqId"))
                    dtsRes = objPaq.ManejaPaquete(14)

                    If Trim$(objPaq.ErrorPaquete) = "" Then
                        gdvTipoSeg.DataSource = dtsRes
                        gdvTipoSeg.DataBind()
                        gdvTipoSeg.Columns(2).Visible = False
                    Else
                        gdvTipoSeg.Columns(2).Visible = False
                        MensajeError(objPaq.ErrorPaquete)
                        Exit Function
                    End If

                    'combo de aplicacion de seguros de vida
                    dtsRes = objCombo.ObtenInfoParametros(49, strErr)
                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbTipoSegVid, strErr)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                        cmbTipoSegVid.SelectedValue = 50
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If
                    'BBVA-P-412
                    'Grid Tipos Seguros de Vida
                    objPaq.IDPaquete = Val(Request("paqId"))
                    dtsRes = objPaq.ManejaPaquete(44)

                    If Trim$(objPaq.ErrorPaquete) = "" Then
                        gdvTipoSegVida.DataSource = dtsRes
                        gdvTipoSegVida.DataBind()
                        gdvTipoSegVida.Columns(2).Visible = False
                    Else
                        gdvTipoSegVida.Columns(2).Visible = False
                        MensajeError(objPaq.ErrorPaquete)
                        Exit Function
                    End If

                Case 4 ''CONDICIONES
                    'combo de periodicidad
                    If band = 0 Then
                        dtsRes = objCombo.ObtenInfoParametros(82, strErr, False, "1")
                        If Trim$(strErr) = "" Then
                            objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbPeriodicidad, strErr)
                            If Trim$(strErr) <> "" Then
                                MensajeError(strErr)
                                Exit Function
                            End If
                        Else
                            MensajeError(strErr)
                            Exit Function
                        End If
                    End If

                    objPaq.IDPaquete = Val(Request("paqId"))
                    objPaq.IDPeriodicidad = cmbPeriodicidad.SelectedValue
                    dtsRes = objPaq.ManejaPaquete(34)

                    'grid con plazos
                    If Trim$(objPaq.ErrorPaquete) = "" Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            gdvPlazos.DataSource = dtsRes
                        End If

                        gdvPlazos.DataBind()

                        If cmbTipoCalc.SelectedItem.Text = "BALLOON" Then
                            gdvPlazos.Columns(11).Visible = True
                            gdvPlazos.Columns(12).Visible = True
                        Else
                            gdvPlazos.Columns(11).Visible = False
                            gdvPlazos.Columns(12).Visible = False
                        End If

                        gdvPlazos.Columns(9).Visible = False
                        gdvPlazos.Columns(10).Visible = False
                        grvpromociones.Columns(3).Visible = True
                    Else
                        MensajeError(objPaq.ErrorPaquete)
                        Exit Function
                    End If

                Case 5 ''PROMOCIONES

                    If Val(Request("paqId")) > 0 Then
                        objPaq.IDPaquete = Val(Request("paqId"))
                    End If

                    objPaq.IDPeriodicidad = cmbPeriodicidad.SelectedValue
                    dtsRes = objPaq.ManejaPaquete(33)
                    If objPaq.ErrorPaquete = "" Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            grvpromociones.DataSource = dtsRes
                            grvpromociones.DataBind()
                            grvpromociones.Columns(3).Visible = False
                        Else
                            MensajeError("No se encontro información de las promociones.")
                        End If
                    Else
                        MensajeError(objPaq.ErrorPaquete)
                    End If
            End Select

            CargaCombos = True
        Catch ex As Exception
            CargaCombos = False
            MensajeError(ex.Message)
        End Try
    End Function

    Private Sub CargaInfo()
        Try
            Dim intPaq As Integer = Val(Request("paqId"))
            objPaq.CargaPaquete(intPaq)

            If objPaq.ErrorPaquete = "" Then
                ''INFORMACIÓN GENERAL
                lblid.Text = objPaq.IDPaquete
                txtNom.Text = objPaq.Nombre
                cmbMoneda.SelectedValue = objPaq.IDMoneda
                txtIniVig.Text = objPaq.InicioVigencia
                txtFinVig.Text = objPaq.FinVigencia
                cmbEstatus.SelectedValue = objPaq.IDEstatus
                cmbCalendario.SelectedValue = objPaq.IDCalendario
                cmbTipoCalc.SelectedValue = objPaq.IDTipoCalculo
                cmbTipoCalc_SelectedIndexChanged(cmbTipoCalc, Nothing)
                cmbTipoVenc.SelectedValue = objPaq.IDTipoVencimiento
                txtDepGarMin.Text = objPaq.RentasDepositoMinimas
                txtDepGarMax.Text = objPaq.RentasDepositoMaximas
                txtPtjAcc.Text = objPaq.PtjMaximoAccesorios
                chkPagoIrreg.Checked = IIf(objPaq.PrimerPagoIrregular = 1, True, False)
                chkEngAcc.Checked = IIf(objPaq.ConsideraEngancheAccesorios = 1, True, False)
                chkPagosEsp.Checked = IIf(objPaq.PermitePagosEspeciales = 1, True, False)
                chkSubsidio.Checked = IIf(objPaq.PermiteMontoSubsidio = 1, True, False)
                chkPtjSubsidio.Checked = IIf(objPaq.PermitePtjSubsidio = 1, True, False)
                chkGraCap.Checked = IIf(objPaq.PermiteGraciaCapital = 1, True, False)
                chkGraInt.Checked = IIf(objPaq.PermiteGraciaInteres = 1, True, False)
                chkCotAb.Checked = IIf(objPaq.PlantillaCotizadorAbierto = 1, True, False)
                chkCaptSegManual.Checked = IIf(objPaq.PermiteCapturaPrimaSeguroManual = 1, True, False)
                'cmbTipoCalcSeg.SelectedValue = objPaq.IDTipoCalculoSeguro
                txtIncentivoVtas.Text = objPaq.IncentivoVentas
                chkDefault.Checked = IIf(objPaq.RegDefault = 1, True, False)
                'BBVA-P-412
                ''chkTasaIntVar.Checked = IIf(objPaq.ManejaTasaInteresVariable = 1, True, False)
                'If chkTasaIntVar.Checked = True Then
                '    cmbTasaIntVar.Enabled = True
                'Else
                '    cmbTasaIntVar.Enabled = False
                'End If

                'BBVA-P-412
                cmbTipoSegVid.SelectedValue = objPaq.IDAplicacionSeguroVida

                If objPaq.IDViaCalcSegVida = 1 Then ''web service
                    rdbsegvidaws.Checked = True
                    txtFactSegVid.Text = ""
                    chkIvaSegVida.Checked = False
                    chkIvaSegVida.Enabled = False
                    txtFactSegVid.Enabled = False
                ElseIf objPaq.IDViaCalcSegVida = 2 Then ''broker - factor
                    rdbsegvidafact.Checked = True
                    txtFactSegVid.Text = objPaq.FactorSeguroVida
                    chkIvaSegVida.Checked = IIf(objPaq.CalculaIVASeguroVida = 0, False, True)
                End If

                'Porcentaje Pago Especial
                If objPaq.IDTipoCalculo = 167 Then 'RQ18
                    txtporanualidad.Text = CDbl(2.5).ToString("###,###.00")
                Else
                    txtporanualidad.Text = CDbl(2.0).ToString("###,###.00")
                End If

                txtporsubArma.Text = CDbl(objPaq.PorSubsidioArmadora).ToString()
                txtporsubAgencia.Text = objPaq.PorSubsidioAgencia.ToString()
                txtComvtaseg.Text = objPaq.ComisionVtaSeg.ToString()
                txtComvendMin.Text = objPaq.PorMinComisionVendedor.ToString()
                txtComvendMax.Text = objPaq.PorMaxComisionVendedor.ToString()
                txtComAgenMin.Text = objPaq.PorMinComisionAgencia.ToString()
                txtComAgenMax.Text = objPaq.PorMaxComisionAgencia.ToString()
                chkcerocom.Checked = IIf(objPaq.CEROCOMISION = 1, True, False)
                ddlprod.SelectedValue = objPaq.IDProdUG
                ddlsubprod.SelectedValue = objPaq.IDSubProdUG
                txtimpming.Text = objPaq.ImporteMinG
                txtimpmaxg.Text = objPaq.ImporteMaxG
            Else
                MensajeError(objPaq.ErrorPaquete)
            End If

        Catch ex As Exception
            MensajeError(ex.Message)
            Exit Sub
        End Try
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaPaquetes.aspx?paqId=0")
    End Sub

    Protected Sub gdvPerJur_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvPerJur.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim _objChk As New CheckBox
            Dim datakey As String = Convert.ToString(gdvPerJur.DataKeys(e.Row.RowIndex).Values(1).ToString())
            Dim datakey2 As String = Convert.ToString(gdvPerJur.DataKeys(e.Row.RowIndex).Values(2).ToString())

            _objChk = e.Row.FindControl("chkPlazo")

            If datakey = 2 Then
                _objChk.Checked = True
            Else
                _objChk.Checked = False
            End If

            If datakey2 = 2 Then
                _objChk.Enabled = True
            Else
                _objChk.Enabled = False
            End If

        End If
    End Sub

    'BBVA-P-412
    Protected Sub gdvCanales_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvCanales.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _objChk As New CheckBox
            Dim datakey As String = Convert.ToString(gdvCanales.DataKeys(e.Row.RowIndex).Values(1).ToString())
            Dim datakey2 As String = Convert.ToString(gdvCanales.DataKeys(e.Row.RowIndex).Values(2).ToString())

            _objChk = e.Row.FindControl("chkPlazo")

            If datakey = 2 Then
                _objChk.Checked = True
            Else
                _objChk.Checked = False
            End If

            If datakey2 = 2 Then
                _objChk.Enabled = True
            Else
                _objChk.Enabled = False
            End If
        End If
    End Sub

    Protected Sub gdvTP_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvTP.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _objChk As New CheckBox
            Dim datakey As String = Convert.ToString(gdvTP.DataKeys(e.Row.RowIndex).Values(1).ToString())
            Dim datakey2 As String = Convert.ToString(gdvTP.DataKeys(e.Row.RowIndex).Values(2).ToString())

            _objChk = e.Row.FindControl("chkClase")

            If datakey = 2 Then
                _objChk.Checked = True
            Else
                _objChk.Checked = False
            End If

            If datakey2 = 2 Then
                _objChk.Enabled = True
            Else
                _objChk.Enabled = False
            End If

        End If
    End Sub

    Protected Sub gdvClasif_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvClasif.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _objChk As New CheckBox
            Dim datakey As String = Convert.ToString(gdvClasif.DataKeys(e.Row.RowIndex).Values(1).ToString())
            Dim datakey2 As String = Convert.ToString(gdvClasif.DataKeys(e.Row.RowIndex).Values(2).ToString())

            _objChk = e.Row.FindControl("chkclasif")

            If datakey = 2 Then
                _objChk.Checked = True
            Else
                _objChk.Checked = False
            End If

            If datakey2 = 2 Then
                _objChk.Enabled = True
            Else
                _objChk.Enabled = False
            End If
        End If
    End Sub

    Protected Sub gdvTO_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvTO.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _objChk As New CheckBox
            Dim datakey As String = Convert.ToString(gdvTO.DataKeys(e.Row.RowIndex).Values(1).ToString())
            Dim datakey2 As String = Convert.ToString(gdvTO.DataKeys(e.Row.RowIndex).Values(2).ToString())


            _objChk = e.Row.FindControl("chkTO")

            If datakey = 2 Then
                _objChk.Checked = True
            Else
                _objChk.Checked = False
            End If

            If datakey2 = 2 Then
                _objChk.Enabled = True
            Else
                _objChk.Enabled = False
            End If

        End If
    End Sub

    Protected Sub gdvTipoSeg_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvTipoSeg.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _objChk As New CheckBox
            Dim datakey As String = Convert.ToString(gdvTipoSeg.DataKeys(e.Row.RowIndex).Values(1).ToString())
            Dim datakey2 As String = Convert.ToString(gdvTipoSeg.DataKeys(e.Row.RowIndex).Values(2).ToString())

            _objChk = e.Row.FindControl("chkTSeg")

            If datakey = 2 Then
                _objChk.Enabled = True
            Else
                _objChk.Enabled = False
            End If

            If datakey2 = 2 Then
                _objChk.Checked = True
            Else
                _objChk.Checked = False
            End If
        End If
    End Sub

    'BBVA-P-412
    Protected Sub gdvTipoSegVida_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvTipoSegVida.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _objChk As New CheckBox
            Dim datakey As String = Convert.ToString(gdvTipoSegVida.DataKeys(e.Row.RowIndex).Values(1).ToString())
            Dim datakey2 As String = Convert.ToString(gdvTipoSegVida.DataKeys(e.Row.RowIndex).Values(2).ToString())

            _objChk = e.Row.FindControl("chkTSegVida")

            If datakey = 2 Then
                _objChk.Enabled = True
            Else
                _objChk.Enabled = False
            End If

            If datakey2 = 2 Then
                _objChk.Checked = True
            Else
                _objChk.Checked = False
            End If
        End If
    End Sub

    Protected Sub gdvPlazos_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvPlazos.RowDataBound
        Try
            If Val(Request("paqId")) > 0 Then
                If e.Row.RowType = DataControlRowType.DataRow Then
                    Dim intPlazo As String = Convert.ToString(gdvPlazos.DataKeys(e.Row.RowIndex).Values(0).ToString())

                    objPaq.IDPaquete = Val(Request("paqId"))
                    objPaq.IDPeriodicidad = cmbPeriodicidad.SelectedValue
                    objPaq.IDPlazo = Convert.ToInt16(intPlazo.Trim)
                    Dim dtsRes As DataSet = objPaq.ManejaPaquete(7)

                    If Trim(objPaq.ErrorPaquete) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                If dtsRes.Tables(0).Rows(0).Item("ESTATUS_PLAZO") = 2 Then
                                    Dim objChk As New CheckBox
                                    objChk = e.Row.Cells(0).Controls(1)
                                    objChk.Checked = True

                                    Dim objTxt As New TextBox
                                    objTxt = e.Row.Cells(2).Controls(1)
                                    objTxt.Text = dtsRes.Tables(0).Rows(0).Item("PTJ_ENGANCHE")

                                    objTxt = New TextBox
                                    objTxt = e.Row.Cells(3).Controls(1)
                                    objTxt.Text = dtsRes.Tables(0).Rows(0).Item("TASA_NOMINAL")
                                    'BBVA-P-412
                                    If objPaq.CEROCOMISION = 1 Then
                                        objTxt.Enabled = False
                                    End If

                                    objTxt = New TextBox
                                    objTxt = e.Row.Cells(4).Controls(1)
                                    objTxt.Text = dtsRes.Tables(0).Rows(0).Item("TASA_NOMINAL_SEGURO")

                                    objTxt = New TextBox
                                    objTxt = e.Row.Cells(5).Controls(1)
                                    objTxt.Text = dtsRes.Tables(0).Rows(0).Item("PUNTOS_SEGURO_CLIENTE")

                                    objTxt = New TextBox
                                    objTxt = e.Row.Cells(6).Controls(1)
                                    objTxt.Text = dtsRes.Tables(0).Rows(0).Item("PTJ_SERV_FINAN")

                                    objTxt = New TextBox
                                    objTxt = e.Row.Cells(7).Controls(1)
                                    objTxt.Text = dtsRes.Tables(0).Rows(0).Item("PTJ_OPCION_COMPRA")

                                    'objTxt = New TextBox
                                    'objTxt = e.Row.Cells(8).Controls(1)
                                    'objTxt.Text = dtsRes.Tables(0).Rows(0).Item("TASA_PCP")

                                    objTxt = New TextBox
                                    objTxt = e.Row.Cells(8).Controls(1)
                                    objTxt.Text = dtsRes.Tables(0).Rows(0).Item("PTJ_BLIND_DISCOUNT")

                                    'ERODRIGUEZ:Para columnas Tasa Nominal Dos y Comision Dos
                                    'If cmbTipoCalc.SelectedItem.Text = "BALLOON" Then
                                    'If objPaq.IDPlazo = 4 Or objPaq.IDPlazo = 6 Then
                                    objTxt = New TextBox
                                    objTxt = e.Row.Cells(11).Controls(1)
                                    objTxt.Text = dtsRes.Tables(0).Rows(0).Item("TASA_NOMINAL_DOS")
                                    ptj_tasa_ref = CDbl(dtsRes.Tables(0).Rows(0).Item("TASA_NOMINAL_DOS"))

                                    objTxt = New TextBox
                                    objTxt = e.Row.Cells(12).Controls(1)
                                    objTxt.Text = dtsRes.Tables(0).Rows(0).Item("PTJ_SERV_FINAN_DOS")
                                    ptj_comision_ref = CDbl(dtsRes.Tables(0).Rows(0).Item("PTJ_SERV_FINAN_DOS"))
                                    '    e.Row.Cells(11).Enabled = True
                                    'Else
                                    '    e.Row.Cells(11).Enabled = False
                                    'End If

                                    'End If

                                Else  'BBVA-P-412
                                    objPaq.CargaPaquete(Val(Request("paqId")))
                                    If objPaq.CEROCOMISION = 1 Then
                                        Dim objTxt As TextBox
                                        objTxt = New TextBox
                                        objTxt = e.Row.Cells(3).Controls(1)
                                        If objPaq.CEROCOMISION = 1 Then
                                            objTxt.Enabled = False
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grvpromociones_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvpromociones.RowDataBound
        Dim dtsddl As New DataSet
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _objChk As New CheckBox
            Dim _objDdl As New DropDownList
            Dim datakey As String = Convert.ToString(grvpromociones.DataKeys(e.Row.RowIndex).Values(2).ToString()) ''Valida si la promoción esta asignada al paquete.
            Dim datakey2 As String = Convert.ToString(grvpromociones.DataKeys(e.Row.RowIndex).Values(0).ToString()) ''Valida si es la promoción TRADICIONAL deshabilita el dropdownlist.
            Dim datakey3 As String = Convert.ToString(grvpromociones.DataKeys(e.Row.RowIndex).Values(1).ToString()) ''Recupera la periodicidad.
            Dim dts As New DataSet  'BBVA-P-412

            _objChk = e.Row.FindControl("chkPromocion")
            _objDdl = e.Row.FindControl("ddlperiodos")

            If datakey = 1 Then
                _objChk.Checked = True
            Else
                _objChk.Checked = False
            End If

            dtsddl = LlenaDDL_Promociones(datakey3)
            If strErr = "" Then
                objCombo.LlenaCombos(dtsddl, "NOMBRE", "ID_PLAZO", _objDdl, strErr)
                If strErr <> "" Then
                    MensajeError(strErr)
                    Exit Sub
                End If
                'BBVA-P-412
                If datakey = 1 Then
                    If Val(Request("paqId")) > 0 Then
                        Dim objpaqselect As New SNProcotiza.clsPaquetes
                        Dim objplazoselect As New SNProcotiza.clsPlazo
                        objpaqselect.CargaPaquete(Val(Request("paqId")))
                        objplazoselect.Valor = objpaqselect.noperiodos
                        objplazoselect.Id_Periodicidad = cmbPeriodicidad.SelectedValue
                        dts = objplazoselect.ManejaPlazos(1)
                        _objDdl.SelectedValue = dts.Tables(0).Rows(0).Item("ID_PLAZO")
                    End If
                End If
            Else
                MensajeError(strErr)
                Exit Sub
            End If

            If datakey2 = 1 Then
                _objDdl.Items.Insert(0, New ListItem("", "0"))
                _objDdl.SelectedValue = 0
                _objDdl.Enabled = False
                'BUG-PC-38 MAUT 23/01/2017 Se pide que la promocion TRADICIONAL se habilite por default (solo si es alta de Paq)
                If Val(Request("paqId")) = 0 Then
                    _objChk.Checked = True
                End If
            Else
                _objDdl.Enabled = True
            End If
        End If
    End Sub

    Public Function LlenaDDL_Promociones(esquemaid As Integer) As DataSet
        Dim dts As New DataSet

        objPlazos.Id_Periodicidad = esquemaid
        dts = objPlazos.ManejaPlazos(1)

        If objPlazos.StrErrPlazo = "" Then
            If dts.Tables(0).Rows.Count > 0 Then
                Return dts
            Else
                strErr = "No se pudo recuperar información de los plazos."
                Return Nothing
                Exit Function
            End If
        Else
            strErr = objPlazos.StrErrPlazo
            Return Nothing
            Exit Function
        End If
    End Function

    Private Function ValidaCampos() As Boolean
        ValidaCampos = False
        strErr = "Todos los campos marcados con * son obligatorios."

        If cmbCalendario.SelectedValue = "" Then Exit Function
        If cmbPeriodicidad.SelectedValue = "" Then Exit Function
        If cmbTipoCalc.SelectedValue = "" Then Exit Function
        'If cmbTipoCalcSeg.SelectedValue = "" Then Exit Function
        If cmbTipoSegVid.SelectedValue = "" Then Exit Function
        If cmbEstatus.SelectedValue = "" Then Exit Function
        If cmbTipoVenc.SelectedValue = "" Then Exit Function
        If cmbMoneda.SelectedValue = "" Then Exit Function
        If Trim(txtNom.Text) = "" Then Exit Function
        If Trim(txtPtjAcc.Text) = "" Then Exit Function
        If Trim(txtIniVig.Text) = "" Then Exit Function
        If Trim(txtFinVig.Text) = "" Then Exit Function

        Dim fecha As DateTime
        If Not DateTime.TryParse(txtIniVig.Text, fecha) OrElse Not DateTime.TryParse(txtFinVig.Text, fecha) Then
            strErr = "El dato ingresado no es una fecha valida."
            Exit Function
        Else
            If Convert.ToDateTime(txtIniVig.Text) > Convert.ToDateTime(txtFinVig.Text) Then
                strErr = "La fecha de inicio de vigencia no puede ser mayor a la fecha final."
                Exit Function
            End If
        End If

        'BBVA-P-412
        If Trim(txtimpming.Text) = "" Then Exit Function
        If Trim(txtimpmaxg.Text) = "" Then Exit Function



        '% Comisión Venta Seguro
        If Trim(txtComvtaseg.Text) <> "" Then
            If IsNumeric(txtComvtaseg.Text) Then
                If CDbl(txtComvtaseg.Text) < 0 Or CDbl(txtComvtaseg.Text) > 100 Then
                    strErr = "'% Comisión Venta Seguro' introducido no puede ser menor a 0 o mayor a 100."
                    txtComvtaseg.Text = ""
                    Exit Function
                End If
            Else
                strErr = "'% Comisión Venta Seguro' introducido no es valido."
                txtComvtaseg.Text = ""
                Exit Function
            End If
        End If

        'Min  % Comisión Vendedor
        If Trim(txtComvendMin.Text) <> "" Then
            If IsNumeric(txtComvendMin.Text) Then
                If CDbl(txtComvendMin.Text) < 0 Or CDbl(txtComvendMin.Text) > 100 Then
                    strErr = "Valor Min de '% Comisión Vendedor' introducido no puede ser menor a 0 o mayor a 100."
                    txtComvendMin.Text = ""
                    Exit Function
                End If
            Else
                strErr = "Valor Min de '% Comisión Vendedor' introducido no es valido."
                txtComvendMin.Text = ""
                Exit Function
            End If
        End If

        'Max  % Comisión Vendedor
        If Trim(txtComvendMax.Text) <> "" Then
            If IsNumeric(txtComvendMax.Text) Then
                If CDbl(txtComvendMax.Text) < 0 Or CDbl(txtComvendMax.Text) > 100 Then
                    strErr = "Valor Max de '% Comisión Vendedor' introducido no puede ser menor a 0 o mayor a 100."
                    txtComvendMax.Text = ""
                    Exit Function
                End If
            Else
                strErr = "Valor Max de '% Comisión Vendedor' introducido no es valido."
                txtComvendMax.Text = ""
                Exit Function
            End If
        End If

        'Min % Comisión Agencia
        If Trim(txtComAgenMin.Text) <> "" Then
            If IsNumeric(txtComAgenMin.Text) Then
                If CDbl(txtComAgenMin.Text) < 0 Or CDbl(txtComAgenMin.Text) > 100 Then
                    strErr = "Valor Min de '% Comisión Agencia' introducido no puede ser menor a 0 o mayor a 100."
                    txtComAgenMin.Text = ""
                    Exit Function
                End If
            Else
                strErr = "Valor Min de '% Comisión Agencia' introducido no es valido."
                txtComAgenMin.Text = ""
                Exit Function
            End If
        End If

        'Max % Comisión Agencia
        If Trim(txtComAgenMax.Text) <> "" Then
            If IsNumeric(txtComAgenMax.Text) Then
                If CDbl(txtComAgenMax.Text) < 0 Or CDbl(txtComAgenMax.Text) > 100 Then
                    strErr = "Valor Max de '% Comisión Agencia' introducido no puede ser menor a 0 o mayor a 100."
                    txtComAgenMax.Text = ""
                    Exit Function
                End If
            Else
                strErr = "Valor Max de '% Comisión Agencia' introducido no es valido."
                txtComAgenMax.Text = ""
                Exit Function
            End If
        End If



        If Val(Trim(txtimpming.Text)) = 0 Or Val(Trim(txtimpmaxg.Text)) = 0 Then
            strErr = "El importe mínimo o máximo no puede ser 0."
            Exit Function
        End If

        If chkPtjSubsidio.Checked Or chkSubsidio.Checked Then
            If Val(txtporsubArma.Text) = 0 And Val(txtporsubAgencia.Text) = 0 Then
                strErr = "El porcentaje de subsidio no puede ser 0."
                Exit Function
            End If
        End If

        If CDbl(Val(txtporsubArma.Text.Trim)) > 0 Or CDbl(Val(txtporsubAgencia.Text.Trim)) > 0 Then
            If (CDbl(Val(txtporsubArma.Text.Trim)) + CDbl(Val(txtporsubAgencia.Text.Trim)) > 100) Then
                strErr = "La suma de los subsidios no puede ser mayor a 100."
                Exit Function
            ElseIf (CDbl(Val(txtporsubArma.Text.Trim)) + CDbl(Val(txtporsubAgencia.Text.Trim)) < 100) Then
                strErr = "La suma de los subsidios no puede ser menor a 100."
                Exit Function
            End If
        End If

        'BUG-PC-22 2016-12-02 MAUT Se válida formato y valores máximos y mínimos para comisión Vendedor
        If CDbl(IIf(txtComvendMin.Text = "", 0, txtComvendMin.Text)) > CDbl(IIf(txtComvendMax.Text = "", 0, txtComvendMax.Text)) Then
            strErr = "EL valor mínimo de la comisión Vendedor, no puede ser mayor al máximo."
            Exit Function
        End If

        'BUG-PC-22 2016-12-02 MAUT Se válida formato y valores máximos y mínimos para comisión Agencia
        If CDbl(IIf(txtComAgenMin.Text = "", 0, txtComAgenMin.Text)) > CDbl(IIf(txtComAgenMax.Text = "", 0, txtComAgenMax.Text)) Then
            strErr = "EL valor mínimo de la comisión Agencia, no puede ser mayor al máximo."
            Exit Function
        End If

        'BUG-PC-25 2016-12-15 MAUT Se validan montos máximos y mínimos.
        If CDbl(IIf(txtimpming.Text = "", 0, txtimpming.Text)) > CDbl(IIf(txtimpmaxg.Text = "", 0, txtimpmaxg.Text)) Then
            strErr = "El valor mínimo del crédito no puede ser mayor al máximo"
            Exit Function
        End If

        'BUG-PC-38 MAUT 23/01/2017 Se valida el porcentaje de accesorios
        If CDbl(Trim(txtPtjAcc.Text)) > 99.99 Then
            strErr = "El porcentaje de accesorios no puede ser mayor a 99.99"
            Exit Function
        End If

        strErr = String.Empty
        ValidaCampos = True
    End Function

    Private Function ValidaRelaciones() As Boolean
        ValidaRelaciones = False
        Try
            Dim objRow As GridViewRow
            Dim objChk As CheckBox
            Dim intSel As Integer = 0

            'SE AGERGA AQUI LA VALIDACION DE RENTAS EN DEPOSITO
            If Val(txtDepGarMin.Text) > Val(txtDepGarMax.Text) Then
                strErr = "El número de rentas en depósito mínimo no puede ser mayor al número de rentas en depósito máximo"
                Exit Function
            End If

            'SE VALIDA QUE SE HAYA SELECCIONADO UNA PERSONALIDAD JURÍDICA
            For Each objRow In gdvPerJur.Rows
                If objRow.RowType = DataControlRowType.DataRow Then
                    objChk = objRow.Cells(0).Controls(1)
                    If objChk.Checked = True Then
                        'id
                        strPersonas += IIf(Trim(strPersonas) = "", objRow.Cells(2).Text, "|" & objRow.Cells(2).Text)
                        intSel += 1
                    End If
                End If
            Next

            If intSel = 0 Then
                strErr = "Debe seleccionar al menos una PERSONALIDAD JURÍDICA en la parte de RELACIONES"
                Exit Function
            End If


            'SE VALIDA QUE SE HAYA SELECCIONADO UNA CLASIFICACIÓN DE PRODUCTO
            intSel = 0
            For Each objRow In gdvClasif.Rows
                If objRow.RowType = DataControlRowType.DataRow Then
                    objChk = objRow.Cells(0).Controls(1)
                    If objChk.Checked = True Then
                        'id
                        strClasif += IIf(Trim(strClasif) = "", objRow.Cells(2).Text, "|" & objRow.Cells(2).Text)
                        intSel += 1
                    End If
                End If
            Next

            If intSel = 0 Then
                strErr = "Debe seleccionar al menos una CLASIFICACIÓN DE PRODUCTO en la parte de RELACIONES"
                Exit Function
            End If

            'SE VALIDA QUE SE HAYA SELECCIONADO UN TIPO DE PRODUCTO
            intSel = 0
            For Each objRow In gdvTP.Rows
                If objRow.RowType = DataControlRowType.DataRow Then
                    objChk = objRow.Cells(0).Controls(1)
                    If objChk.Checked = True Then
                        'id
                        strTiposProd += IIf(Trim(strTiposProd) = "", objRow.Cells(2).Text, "|" & objRow.Cells(2).Text)
                        intSel += 1
                    End If
                End If
            Next

            If intSel = 0 Then
                strErr = "Debe seleccionar al menos un TIPO DE PRODUCTO en la parte de RELACIONES"
                Exit Function
            End If

            'SE VALIDA QUE SE HAYA SELECCIONADO UN TIPO DE OPERACION
            intSel = 0
            For Each objRow In gdvTO.Rows
                If objRow.RowType = DataControlRowType.DataRow Then
                    objChk = objRow.Cells(0).Controls(1)
                    If objChk.Checked = True Then
                        'id
                        strTiposOper += IIf(Trim(strTiposOper) = "", objRow.Cells(2).Text, "|" & objRow.Cells(2).Text)
                        intSel += 1
                    End If
                End If
            Next

            If intSel = 0 Then
                strErr = "Debe seleccionar al menos un TIPO DE OPERACION en la parte de RELACIONES"
                Exit Function
            End If

            'BBVA-P-412
            'SE VALIDA QUE SE HAYA SELECCIONADO UN TIPO DE CANAL
            intSel = 0
            For Each objRow In gdvCanales.Rows
                If objRow.RowType = DataControlRowType.DataRow Then
                    objChk = objRow.Cells(0).Controls(1)
                    If objChk.Checked = True Then
                        strTiposOper += IIf(Trim(strTiposOper) = "", objRow.Cells(2).Text, "|" & objRow.Cells(2).Text)
                        intSel += 1
                    End If
                End If
            Next

            If intSel = 0 Then
                strErr = "Debe seleccionar al menos un TIPO DE CANAL en la parte de RELACIONES"
                Exit Function
            End If

            ValidaRelaciones = True

        Catch ex As Exception
            strErr = ex.Message
        End Try
    End Function

    Private Function ValidaSeguros() As Boolean
        ValidaSeguros = False

        Dim _Chk As New CheckBox
        Dim Total As Integer = 0

        For i = 0 To gdvTipoSeg.Rows.Count - 1
            If gdvTipoSeg.Rows(i).RowType = DataControlRowType.DataRow Then
                _Chk = gdvTipoSeg.Rows(i).Cells(0).FindControl("chkTSeg")

                If _Chk.Checked = True Then
                    Total = Total + 1
                End If
            End If
        Next

        If Total = 0 Then  'BBVA-P-412
            strErr = "Debe selecionar al menos un tipo de Seguro."
            Exit Function
        End If
        'BBVA-P-412
        If rdbsegvidafact.Checked = True Then
            If txtFactSegVid.Text.Trim.Length = 0 Then
                strErr = "Debe ingresar Monto de Factor de Seguro de Vida."
                Exit Function
            End If
        End If

        Total = 0
        For i = 0 To gdvTipoSegVida.Rows.Count - 1
            If gdvTipoSegVida.Rows(i).RowType = DataControlRowType.DataRow Then
                _Chk = gdvTipoSegVida.Rows(i).Cells(0).FindControl("chkTSegVida")

                If _Chk.Checked = True Then
                    Total = Total + 1
                End If
            End If
        Next

        If Total = 0 Then
            strErr = "Debe selecionar al menos un tipo de Seguro de Vida."
            Exit Function
        End If

        ValidaSeguros = True

    End Function

    Private Function ValidaPlazos() As Boolean
        ValidaPlazos = False
        Try
            Dim objRow As GridViewRow
            Dim objChk As CheckBox
            Dim objTxt As TextBox
            Dim intSel As Integer = 0

            'BBVA-P-412
            'If chkTasaIntVar.Checked Then
            '    If cmbTasaIntVar.SelectedValue = "" Then
            '        strErr = "Falta información de Tasa de Interes Variable."
            '        Exit Function
            '    End If
            'End If

            'If cmbTipoCalc.SelectedItem.Text = "BALLOON" Then
            gdvPlazos.Columns(9).Visible = True
            'Else
            'gdvPlazos.Columns(9).Visible = True
            'End If


            For Each objRow In gdvPlazos.Rows
                If objRow.RowType = DataControlRowType.DataRow Then
                    objChk = objRow.Cells(0).Controls(1)
                    If objChk.Checked = True Then
                        'id

                        'If cmbTipoCalc.SelectedItem.Text = "BALLOON" Then
                        strPlazos += IIf(Trim(strPlazos) = "", objRow.Cells(9).Text, "|" & objRow.Cells(9).Text)
                        'Else
                        'strPlazos += IIf(Trim(strPlazos) = "", objRow.Cells(9).Text, "|" & objRow.Cells(9).Text)
                        'End If



                        'enganche
                        objTxt = objRow.Cells(2).Controls(1)
                        If Trim(objTxt.Text) = "" Then
                            strErr = "Debe llenar todos los campos solicitados para los plazos seleccionados"
                            Exit Function
                        Else
                            strPlazos += "," & Val(objTxt.Text)
                        End If

                        'tasa nominal
                        objTxt = New TextBox
                        objTxt = objRow.Cells(3).Controls(1)
                        'BBVA-P-412
                        If chkcerocom.Checked = False Then
                            If CDbl(objTxt.Text.Trim) = 0 Then
                                strErr = "La Tasa Nominal no puede ser Cero."
                                Exit Function
                            End If
                        Else
                            objTxt.Text = "0"
                        End If

                        If Trim(objTxt.Text) = "" Then
                            strErr = "Debe llenar todos los campos solicitados para los plazos seleccionados"
                            Exit Function
                        Else
                            strPlazos += "," & Val(objTxt.Text)
                        End If

                        'tasa nominal seguro
                        objTxt = New TextBox
                        objTxt = objRow.Cells(4).Controls(1)
                        If Trim(objTxt.Text) = "" Then
                            strErr = "Debe llenar todos los campos solicitados para los plazos seleccionados"
                            Exit Function
                        Else
                            strPlazos += "," & Val(objTxt.Text)
                        End If

                        'puntos adic seguro X cta cliente
                        objTxt = New TextBox
                        objTxt = objRow.Cells(5).Controls(1)
                        'BBVA-P-412
                        If objTxt.Enabled = False Then
                            objTxt.Text = "0"
                        End If
                        If Trim(objTxt.Text) = "" Then
                            strErr = "Debe llenar todos los campos solicitados para los plazos seleccionados"
                            Exit Function
                        Else
                            strPlazos += "," & Val(objTxt.Text)
                        End If

                        '% servFinan
                        objTxt = New TextBox
                        objTxt = objRow.Cells(6).Controls(1)
                        If Trim(objTxt.Text) = "" Then
                            strErr = "Debe llenar todos los campos solicitados para los plazos seleccionados"
                            Exit Function
                        Else
                            strPlazos += "," & Val(objTxt.Text)
                        End If

                        '% opcion compra
                        objTxt = New TextBox
                        objTxt = objRow.Cells(7).Controls(1)
                        'BBVA-P-412
                        If objTxt.Enabled = False Then
                            objTxt.Text = "0"
                        End If
                        If Trim(objTxt.Text) = "" Then
                            strErr = "Debe llenar todos los campos solicitados para los plazos seleccionados"
                            Exit Function
                        Else
                            strPlazos += "," & Val(objTxt.Text)
                        End If

                        ''tasa pcp
                        'objTxt = New TextBox
                        'objTxt = objRow.Cells(9).Controls(1)
                        'If Trim(objTxt.Text) = "" Then
                        '    strErr = "Debe llenar todos los campos solicitados para los plazos seleccionados"
                        '    Exit Function
                        'Else
                        '    strPlazos += "," & Val(objTxt.Text)
                        'End If

                        '% blind discount
                        objTxt = New TextBox
                        objTxt = objRow.Cells(8).Controls(1)
                        If (objTxt.Enabled = False) Then
                            objTxt.Text = "0"
                        ElseIf (objTxt.Enabled = True) And (IsNumeric(objTxt.Text)) Then
                            If CDbl(objTxt.Text) <= 0 Then
                                strErr = "Valor Residual de los plazos no puede ser menor 0"
                                gdvPlazos.Columns(9).Visible = False
                                Exit Function
                            End If
                        End If
                        If Trim(objTxt.Text) = "" Then
                            strErr = "Debe llenar todos los campos solicitados para los plazos seleccionados"
                            Exit Function
                        Else
                            strPlazos += "," & Val(objTxt.Text)
                        End If

                        '% Comision Dos, Tasa Nominal Dos

                        If (objRow.Cells(10).Text = "24" Or objRow.Cells(10).Text = "36") And cmbTipoCalc.SelectedItem.Text = "BALLOON" Then
                            'para tasa nominal dos
                            objTxt = objRow.Cells(11).Controls(1)
                            If (objTxt.Enabled = False) Then
                                objTxt.Text = "0"
                            ElseIf (objTxt.Enabled = True) And (IsNumeric(objTxt.Text)) Then
                                If CDbl(objTxt.Text) <= 0 Then
                                    'strErr = "Tasa Nominal Dos no puede ser menor 0 para los plazos 24 y 36"
                                    gdvPlazos.Columns(9).Visible = False
                                    'Exit Function
                                End If
                            End If
                            If Trim(objTxt.Text) = "" Then
                                'strErr = "Debe llenar todos los campos solicitados para los plazos seleccionados"
                                'Exit Function
                            Else
                                strPlazos += "," & Val(objTxt.Text)
                            End If
                            'para comision dos
                            objTxt = objRow.Cells(12).Controls(1)
                            If (objTxt.Enabled = False) Then
                                objTxt.Text = "0"
                            ElseIf (objTxt.Enabled = True) And (IsNumeric(objTxt.Text)) Then
                                If CDbl(objTxt.Text) <= 0 Then
                                    'strErr = "Comision Apertura Dos no puede ser menor 0 para los plazos 24 y 36"
                                    gdvPlazos.Columns(9).Visible = False
                                    'Exit Function
                                End If
                            End If
                            If Trim(objTxt.Text) = "" Then
                                'strErr = "Debe llenar todos los campos solicitados para los plazos seleccionados"
                                'Exit Function
                            Else
                                strPlazos += "," & Val(objTxt.Text)
                            End If

                        End If





                        intSel += 1
                    End If
                End If
            Next

            'If cmbTipoCalc.SelectedItem.Text = "BALLOON" Then
            gdvPlazos.Columns(9).Visible = False
            'Else
            'gdvPlazos.Columns(9).Visible = False
            'End If

            If intSel = 0 Then
                strErr = "Debe seleccionar al menos un plazo"
                'If cmbTipoCalc.SelectedItem.Text = "BALLOON" Then
                gdvPlazos.Columns(9).Visible = False
                'Else
                '    gdvPlazos.Columns(9).Visible = False
                'End If

            Else
                'If cmbTipoCalc.SelectedItem.Text = "BALLOON" Then
                gdvPlazos.Columns(9).Visible = False
                '    Else
                '    gdvPlazos.Columns(9).Visible = False
                'End If

                ValidaPlazos = True
            End If

        Catch ex As Exception
            'If cmbTipoCalc.SelectedItem.Text = "BALLOON" Then
            gdvPlazos.Columns(9).Visible = False
            'Else
            'gdvPlazos.Columns(9).Visible = False
            'End If


            strErr = ex.Message
        End Try
    End Function

    Private Function ValidaPromociones() As Boolean

        Dim _Chk As New CheckBox
        Dim Total As Integer = 0

        ValidaPromociones = False

        For i = 0 To grvpromociones.Rows.Count - 1
            If grvpromociones.Rows(i).RowType = DataControlRowType.DataRow Then
                _Chk = grvpromociones.Rows(i).Cells(0).FindControl("chkPromocion")

                If _Chk.Checked = True Then
                    Total = Total + 1
                End If
            End If
        Next

        If Total > 0 Then
            ValidaPromociones = True
        Else
            strErr = "Debe selecionar un tipo de Promoción."
        End If
    End Function

    Private Function RecuperaPromocion(opc As Integer) As Integer

        Dim _Chk As New CheckBox
        Dim _Ddl As New DropDownList
        Dim promo As Integer = 0
        Dim dts As New DataSet

        Try

            grvpromociones.Columns(3).Visible = True

            For i = 0 To grvpromociones.Rows.Count - 1
                If grvpromociones.Rows(i).RowType = DataControlRowType.DataRow Then

                    _Chk = grvpromociones.Rows(i).Cells(0).FindControl("chkPromocion")
                    _Ddl = grvpromociones.Rows(i).Cells(0).FindControl("ddlperiodos")

                    If _Chk.Checked = True Then
                        If opc = 1 Then
                            promo = Convert.ToInt16(grvpromociones.Rows(i).Cells(3).Text)
                        Else

                            If _Ddl.SelectedValue = 0 Then
                                promo = 0
                            Else
                                objPlazos.Id_Plazo = _Ddl.SelectedValue
                                dts = objPlazos.ManejaPlazos(1)
                                If objPlazos.StrErrPlazo = "" Then
                                    If dts.Tables(0).Rows.Count > 0 Then
                                        promo = dts.Tables(0).Rows(0).Item("VALOR")
                                    Else
                                        ''grvpromociones.Columns(3).Visible = False
                                        strErr = "No se pudo recuperar información."
                                        Return Nothing
                                        Exit Function
                                    End If
                                Else
                                    ''grvpromociones.Columns(3).Visible = False
                                    strErr = objPlazos.StrErrPlazo
                                End If
                            End If

                        End If
                    End If
                Else
                    ''grvpromociones.Columns(3).Visible = False
                    strErr = objPaq.ErrorPaquete
                End If
            Next

            grvpromociones.Columns(3).Visible = False
            Return promo
        Catch ex As Exception
            ''grvpromociones.Columns(3).Visible = False
            strErr = ex.Message
            Return promo
        End Try
    End Function

    Private Function GuardaInformacion() As Boolean
        GuardaInformacion = False
        strErr = String.Empty

        conexion = New SDManejaBD.clsConexion(True)
        conexion.AbreConexion()

        Try
            Dim intOpc As Integer = 2
            Dim objpromocte = New SNProcotiza.clsPromocionesCte

            objPaq.CargaSession(Val(Session("cveAcceso")))
            If Val(Request("paqId")) > 0 Then
                objPaq.CargaPaquete(Val(Request("paqId")))
                intOpc = 3
            End If

            'guardamos la info del paquete
            objPaq.IDClasificacionProd = 0
            objPaq.IDMoneda = Val(cmbMoneda.SelectedValue)
            objPaq.IDTipoOperacion = 1
            objPaq.IDPersonalidadJuridica = 0
            objPaq.IDTipoProducto = 0
            'objPaq.IDTipoCalculoSeguro = Val(cmbTipoCalcSeg.SelectedValue)
            objPaq.IDCalendario = Val(cmbCalendario.SelectedValue)
            objPaq.IDPeriodicidad = Val(cmbPeriodicidad.SelectedValue)
            objPaq.IDAplicacionSeguroVida = Val(cmbTipoSegVid.SelectedValue)
            objPaq.IDTipoCalculo = Val(cmbTipoCalc.SelectedValue)
            objPaq.IDEstatus = Val(cmbEstatus.SelectedValue)
            objPaq.IDTasaInteresVariable = 0 ''Val(cmbTasaIntVar.SelectedValue) 'BBVA-P-412
            objPaq.IDTipoVencimiento = Val(cmbTipoVenc.SelectedValue)
            objPaq.Nombre = Trim(txtNom.Text).ToUpper  'BBVA-P-412
            objPaq.TasaIVA = 0
            objPaq.PtjServiciosFinancieros = 0
            objPaq.PtjMaximoAccesorios = Val(txtPtjAcc.Text)
            objPaq.FactorSeguroVida = Val(txtFactSegVid.Text)
            objPaq.InicioVigencia = Trim(txtIniVig.Text)
            objPaq.FinVigencia = Trim(txtFinVig.Text)

            objPaq.RentasDepositoMinimas = Val(Trim(txtDepGarMin.Text))
            objPaq.RentasDepositoMaximas = Val(Trim(txtDepGarMax.Text))
            objPaq.IncentivoVentas = Val(Trim(txtIncentivoVtas.Text))

            objPaq.PermitePagosEspeciales = IIf(chkPagosEsp.Checked, 1, 0)
            objPaq.PermiteMontoSubsidio = IIf(chkSubsidio.Checked, 1, 0)
            objPaq.ConsideraEngancheAccesorios = IIf(chkEngAcc.Checked, 1, 0)
            objPaq.PermiteSeguroFinanciado = 0 'IIf(chkSegFinan.Checked, 1, 0)
            objPaq.PermiteSeguroContado = 0 'IIf(chkSegCont.Checked, 1, 0)
            objPaq.PermiteSeguroCuentaCliente = 0 'IIf(chkSegCtaCte.Checked, 1, 0)
            objPaq.PermiteSeguroMultianualFinanciado = 0 'IIf(chkSegMultiFin.Checked, 1, 0)
            objPaq.PermiteSeguroMultianualContado = 0 'IIf(chkSegMultiCont.Checked, 1, 0)
            objPaq.PermiteSeguroMultianualFinanciadoAñoGratis = 0
            objPaq.PermiteSeguroMultianualContadoAñoGratis = 0

            objPaq.IDPROMOCION = RecuperaPromocion(1)
            If strErr <> "" Then
                MensajeError(strErr)
                current_tab.Value = 4
                Exit Function
            End If

            If objPaq.IDPROMOCION = 1 Then
                objPaq.PermiteSeguroRegalado = 0
            Else
                objPaq.PermiteSeguroRegalado = 1
            End If

            objPaq.PrimerPagoIrregular = IIf(chkPagoIrreg.Checked, 1, 0)
            objPaq.RegDefault = IIf(chkDefault.Checked, 1, 0)
            objPaq.PermiteCapturaPrimaSeguroManual = IIf(chkCaptSegManual.Checked, 1, 0)
            objPaq.CalculaIVASeguroVida = IIf(chkIvaSegVida.Checked, 1, 0)
            objPaq.PermiteDiaPago = IIf(chkDiaPago.Checked, 1, 0)
            objPaq.PermitePtjSubsidio = IIf(chkPtjSubsidio.Checked, 1, 0)
            objPaq.ManejaTasaInteresVariable = 0 ''IIf(chkTasaIntVar.Checked, 1, 0) 'BBVA-P-412
            objPaq.PermiteGraciaCapital = IIf(chkGraCap.Checked, 1, 0)
            objPaq.PermiteGraciaInteres = IIf(chkGraInt.Checked, 1, 0)
            objPaq.PlantillaCotizadorAbierto = IIf(chkCotAb.Checked, 1, 0)

            objPaq.noperiodos = RecuperaPromocion(2)

            'BBVA-P-412
            objPaq.PorPagEspeciales = CDbl(Val(txtporanualidad.Text.Trim))
            objPaq.PorSubsidioArmadora = IIf(txtporsubArma.Text = "", 0, CDbl(Val(txtporsubArma.Text.Trim)))
            objPaq.PorSubsidioAgencia = IIf(txtporsubAgencia.Text = "", 0, CDbl(Val(txtporsubAgencia.Text.Trim)))
            objPaq.ComisionVtaSeg = IIf(txtComvtaseg.Text = "", 0, CDbl(Val(txtComvtaseg.Text.Trim)))
            objPaq.PorMinComisionVendedor = IIf(txtComvendMin.Text = "", 0, CDbl(Val(txtComvendMin.Text.Trim)))
            objPaq.PorMaxComisionVendedor = IIf(txtComvendMax.Text = "", 0, CDbl(Val(txtComvendMax.Text.Trim)))
            objPaq.PorMinComisionAgencia = IIf(txtComAgenMin.Text = "", 0, CDbl(Val(txtComAgenMin.Text.Trim)))
            objPaq.PorMaxComisionAgencia = IIf(txtComAgenMax.Text = "", 0, CDbl(Val(txtComAgenMax.Text.Trim)))
            objPaq.ImporteMinG = CDbl(Val(txtimpming.Text.Trim))
            objPaq.ImporteMaxG = CDbl(Val(txtimpmaxg.Text.Trim))


            If rdbsegvidaws.Checked = True Then
                objPaq.IDViaCalcSegVida = 1
            End If
            If rdbsegvidafact.Checked = True Then
                objPaq.IDViaCalcSegVida = 2
            End If

            objPaq.CEROCOMISION = IIf(chkcerocom.Checked, 1, 0)
            objPaq.IDProdUG = ddlprod.SelectedValue
            objPaq.IDSubProdUG = ddlsubprod.SelectedValue

            objPaq.ManejaPaquete(intOpc, conexion)
            If objPaq.ErrorPaquete = "" Then

                PaqID = objPaq.IDPaquete
                If ConsulWS() Then
                    conexion.TerminaTransaccion()
                Else
                    Throw New Exception(strErr)
                    Exit Function
                End If

                If GuardaPlazos(objPaq.IDPaquete) Then
                    If GuardaRelaciones(objPaq.IDPaquete) Then
                        If GuardaSeguros(objPaq.IDPaquete) Then
                            GuardaInformacion = True
                        Else   'BBVA-P-412
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
            Else
                strErr = objPaq.ErrorPaquete
                Exit Function
            End If

        Catch ex As Exception
            conexion.TerminaTransaccion(True)
            strErr = ex.Message
        End Try
    End Function

    Function GuardaPlazos(ByVal intCvePaq As Integer) As Boolean
        GuardaPlazos = False
        Try
            Dim strPaso() As String = Split(strPlazos, "|")
            Dim strVal() As String
            Dim intR As Integer

            'primero se borran los plazos actuales
            objPaq.IDPaquete = intCvePaq
            objPaq.ManejaPaquete(5)

            If Trim$(objPaq.ErrorPaquete) = "" Then
                For intR = 0 To UBound(strPaso, 1)
                    strVal = Split(strPaso(intR), ",")


                    'Dim aBuscar As Char
                    'aBuscar = ","
                    'Dim n As Integer = 0
                    'For Each c As Char In strVal
                    '    If c = aBuscar Then
                    '        n += 1
                    '    End If
                    'Next



                    objPaq.IDPlazo = Val(strVal(0))
                    objPaq.PtjEngancheMinimo = Val(strVal(1))
                    objPaq.TasaNominal = Val(strVal(2))
                    objPaq.TasaNominalSeguro = Val(strVal(3))
                    objPaq.PuntosSeguroCliente = Val(strVal(4))
                    objPaq.PtjServiciosFinancieros = Val(strVal(5))
                    objPaq.PtjOpcionCompra = Val(strVal(6))
                    'objPaq.TasaPCP = Val(strVal(7))
                    objPaq.PtjBlindDiscount = Val(strVal(7))


                    If strVal.Length = 10 Then
                        objPaq.TasaNominalDos = Val(strVal(8))
                        objPaq.PtjServiciosFinancierosDos = Val(strVal(9))
                    Else
                        objPaq.TasaNominalDos = Nothing
                        objPaq.PtjServiciosFinancierosDos = Nothing
                    End If


                    objPaq.IDEstatusPlazo = 2
                    objPaq.UsuarioRegistro = objPaq.UserNameAcceso


                    objPaq.ManejaPaquete(6)
                    If Trim$(objPaq.ErrorPaquete) <> "" Then
                        strErr = objPaq.ErrorPaquete
                        Exit Function
                    End If
                Next
                GuardaPlazos = True
            Else
                strErr = objPaq.ErrorPaquete
            End If

        Catch ex As Exception
            strErr = ex.Message
        End Try
    End Function

    Function GuardaRelaciones(ByVal intCvePaq As Integer) As Boolean
        GuardaRelaciones = False
        Try
            Dim _Chk As New CheckBox

            'primero se borran las relaciones actuales
            objPaq.IDPaquete = intCvePaq
            objPaq.ManejaPaquete(16) 'personalidad jurídica
            If Trim$(objPaq.ErrorPaquete) <> "" Then strErr = objPaq.ErrorPaquete : Exit Function
            objPaq.ManejaPaquete(19) 'clasificaciones
            If Trim$(objPaq.ErrorPaquete) <> "" Then strErr = objPaq.ErrorPaquete : Exit Function
            objPaq.ManejaPaquete(25) 'tipos producto
            If Trim$(objPaq.ErrorPaquete) <> "" Then strErr = objPaq.ErrorPaquete : Exit Function
            objPaq.ManejaPaquete(28) 'tipos operación
            If Trim$(objPaq.ErrorPaquete) <> "" Then strErr = objPaq.ErrorPaquete : Exit Function
            objPaq.ManejaPaquete(12) 'tipos seguros
            If Trim$(objPaq.ErrorPaquete) <> "" Then strErr = objPaq.ErrorPaquete : Exit Function
            'BBVA-P-412
            objPaq.ManejaPaquete(41) 'canales
            If Trim$(objPaq.ErrorPaquete) <> "" Then strErr = objPaq.ErrorPaquete : Exit Function

            If Trim$(objPaq.ErrorPaquete) = "" Then

                'tipos producto
                gdvTP.Columns(2).Visible = True
                For i = 0 To gdvTP.Rows.Count - 1
                    If gdvTP.Rows(i).RowType = DataControlRowType.DataRow Then

                        _Chk = gdvTP.Rows(i).Cells(0).FindControl("chkClase")

                        If _Chk.Checked = True Then
                            objPaq.IDTipoProducto = Convert.ToInt16(gdvTP.Rows(i).Cells(2).Text)
                            objPaq.IDEstatusOtro = 2
                            objPaq.ManejaPaquete(26)

                            If Trim$(objPaq.ErrorPaquete) <> "" Then
                                gdvTP.Columns(2).Visible = False
                                strErr = objPaq.ErrorPaquete
                                Exit Function
                            End If
                        End If
                    Else
                        gdvTP.Columns(2).Visible = False
                        strErr = objPaq.ErrorPaquete
                    End If
                Next
                gdvTP.Columns(2).Visible = False


                'clasificaciones
                gdvClasif.Columns(2).Visible = True
                For i = 0 To gdvClasif.Rows.Count - 1
                    If gdvClasif.Rows(i).RowType = DataControlRowType.DataRow Then

                        _Chk = gdvClasif.Rows(i).Cells(0).FindControl("chkclasif")

                        If _Chk.Checked = True Then
                            objPaq.IDClasificacionProd = Convert.ToInt16(gdvClasif.Rows(i).Cells(2).Text)
                            objPaq.IDEstatusOtro = 2
                            objPaq.ManejaPaquete(20)

                            If Trim$(objPaq.ErrorPaquete) <> "" Then
                                gdvClasif.Columns(2).Visible = False
                                strErr = objPaq.ErrorPaquete
                                Exit Function
                            End If
                        End If
                    Else
                        gdvClasif.Columns(2).Visible = False
                        strErr = objPaq.ErrorPaquete
                    End If
                Next
                gdvClasif.Columns(2).Visible = False


                'tipos operación
                gdvTO.Columns(2).Visible = True
                For i = 0 To gdvTO.Rows.Count - 1
                    If gdvTO.Rows(i).RowType = DataControlRowType.DataRow Then

                        _Chk = gdvTO.Rows(i).Cells(0).FindControl("chkTO")

                        If _Chk.Checked = True Then
                            objPaq.IDTipoOperacion = Convert.ToInt16(gdvTO.Rows(i).Cells(2).Text)
                            objPaq.IDEstatusOtro = 2
                            objPaq.ManejaPaquete(29)

                            If Trim$(objPaq.ErrorPaquete) <> "" Then
                                gdvTO.Columns(2).Visible = False
                                strErr = objPaq.ErrorPaquete
                                Exit Function
                            End If
                        End If
                    Else
                        gdvTO.Columns(2).Visible = False
                        strErr = objPaq.ErrorPaquete
                    End If
                Next
                gdvTO.Columns(2).Visible = False

                'personalidad jurídica
                gdvPerJur.Columns(2).Visible = True
                For i = 0 To gdvPerJur.Rows.Count - 1
                    If gdvPerJur.Rows(i).RowType = DataControlRowType.DataRow Then

                        _Chk = gdvPerJur.Rows(i).Cells(0).FindControl("chkPlazo")

                        If _Chk.Checked = True Then
                            objPaq.IDPersonalidadJuridica = Convert.ToInt16(gdvPerJur.Rows(i).Cells(2).Text)
                            objPaq.IDEstatusOtro = 2
                            objPaq.ManejaPaquete(17)

                            If Trim$(objPaq.ErrorPaquete) <> "" Then
                                gdvPerJur.Columns(2).Visible = False
                                strErr = objPaq.ErrorPaquete
                                Exit Function
                            End If
                        End If
                    Else
                        gdvPerJur.Columns(2).Visible = False
                        strErr = objPaq.ErrorPaquete
                    End If
                Next
                gdvPerJur.Columns(2).Visible = False

                'BBVA-P-412
                'canales
                gdvCanales.Columns(2).Visible = True
                For i = 0 To gdvCanales.Rows.Count - 1
                    If gdvCanales.Rows(i).RowType = DataControlRowType.DataRow Then

                        _Chk = gdvCanales.Rows(i).Cells(0).FindControl("chkPlazo")

                        If _Chk.Checked = True Then
                            objPaq.IDCanal = Convert.ToInt16(gdvCanales.Rows(i).Cells(2).Text)
                            objPaq.IDEstatusOtro = 2
                            objPaq.UsuarioRegistro = objPaq.UserNameAcceso
                            objPaq.ManejaPaquete(42)

                            If Trim$(objPaq.ErrorPaquete) <> "" Then
                                gdvCanales.Columns(2).Visible = False
                                strErr = objPaq.ErrorPaquete
                                Exit Function
                            End If
                        End If
                    Else
                        gdvCanales.Columns(2).Visible = False
                        strErr = objPaq.ErrorPaquete
                    End If
                Next
                gdvCanales.Columns(2).Visible = False

                GuardaRelaciones = True
            Else
                strErr = objPaq.ErrorPaquete
            End If

        Catch ex As Exception
            strErr = ex.Message
        End Try
    End Function

    Function GuardaSeguros(ByVal intCvePaq As Integer) As Boolean
        GuardaSeguros = False

        Try
            Dim _Chk As New CheckBox
            'Se borran las relaciones existentes de Seguros	 'BBVA-P-412
            objPaq.IDPaquete = intCvePaq
            objPaq.ManejaPaquete(12)
            If Trim$(objPaq.ErrorPaquete) <> "" Then strErr = objPaq.ErrorPaquete : Exit Function

            If Trim$(objPaq.ErrorPaquete) = "" Then

                'Seguros
                gdvTipoSeg.Columns(2).Visible = True

                For i = 0 To gdvTipoSeg.Rows.Count - 1
                    If gdvTipoSeg.Rows(i).RowType = DataControlRowType.DataRow Then

                        _Chk = gdvTipoSeg.Rows(i).Cells(0).FindControl("chkTSeg")

                        If _Chk.Checked = True Then
                            objPaq.IDTipoSeg = Convert.ToInt16(gdvTipoSeg.Rows(i).Cells(2).Text)
                            objPaq.IDEstatusOtro = 2
                            objPaq.ManejaPaquete(13)

                            If Trim$(objPaq.ErrorPaquete) <> "" Then
                                gdvTipoSeg.Columns(2).Visible = False
                                strErr = objPaq.ErrorPaquete
                                Exit Function
                            End If
                        End If
                    Else
                        gdvTipoSeg.Columns(2).Visible = False
                        strErr = objPaq.ErrorPaquete
                        Exit Function  'BBVA-P-412
                    End If
                Next
                gdvTipoSeg.Columns(2).Visible = False
            Else
                gdvTipoSeg.Columns(2).Visible = False
                strErr = objPaq.ErrorPaquete
                Exit Function
            End If
            'BBVA-P-412
            'Se borran las relaciones existentes de Seguros de Vida
            objPaq.IDPaquete = intCvePaq
            objPaq.ManejaPaquete(46)
            If Trim$(objPaq.ErrorPaquete) <> "" Then
                strErr = objPaq.ErrorPaquete
                Exit Function
            End If

            If Trim$(objPaq.ErrorPaquete) = "" Then

                'Seguros de Vida
                gdvTipoSegVida.Columns(2).Visible = True

                For i = 0 To gdvTipoSegVida.Rows.Count - 1
                    If gdvTipoSegVida.Rows(i).RowType = DataControlRowType.DataRow Then

                        _Chk = gdvTipoSegVida.Rows(i).Cells(0).FindControl("chkTSegVida")

                        If _Chk.Checked = True Then
                            objPaq.IDTipoSegVida = Convert.ToInt16(gdvTipoSegVida.Rows(i).Cells(2).Text)
                            objPaq.IDEstatusOtro = 2
                            objPaq.ManejaPaquete(45)

                            If Trim$(objPaq.ErrorPaquete) <> "" Then
                                gdvTipoSegVida.Columns(2).Visible = False
                                strErr = objPaq.ErrorPaquete
                                Exit Function
                            End If
                        End If
                    Else
                        gdvTipoSegVida.Columns(2).Visible = False
                        strErr = objPaq.ErrorPaquete
                        Exit Function
                    End If
                Next
                gdvTipoSegVida.Columns(2).Visible = False
            Else
                gdvTipoSegVida.Columns(2).Visible = False
                strErr = objPaq.ErrorPaquete
                Exit Function
            End If

            GuardaSeguros = True

        Catch ex As Exception
            strErr = ex.Message
        End Try
    End Function


    Protected Sub cmbPeriodicidad_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbPeriodicidad.SelectedIndexChanged
        band = 1
        'If cmbTipoCalc.SelectedItem.Text = "BALLOON" Then
        'gdvPlazos.Columns(11).Visible = True
        'gdvPlazos.Columns(12).Visible = True
        'Else
        gdvPlazos.Columns(9).Visible = True
        gdvPlazos.Columns(10).Visible = True

        'End If


        CargaCombos(4)
        CargaCombos(5)
        current_tab.Value = 3
    End Sub

    'BBVA-P-412
    'Protected Sub chkTasaIntVar_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkTasaIntVar.CheckedChanged
    '    If chkTasaIntVar.Checked Then
    '        cmbTasaIntVar.Enabled = True
    '    Else
    '        cmbTasaIntVar.SelectedValue = ""
    '        cmbTasaIntVar.Enabled = False
    '    End If
    '    current_tab.Value = 3
    'End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then ''1
                If ValidaRelaciones() Then ''2
                    If ValidaSeguros() Then ''3
                        If ValidaPlazos() Then ''4
                            If GuardaInformacion() Then
                                CierraPantalla("./consultaPaquetes.aspx")
                            Else
                                MensajeError(strErr)
                                Exit Sub
                            End If
                        Else ''4
                            current_tab.Value = 3
                            MensajeError(strErr)
                            Exit Sub
                        End If
                    Else ''3
                        current_tab.Value = 2
                        MensajeError(strErr)
                        Exit Sub
                    End If
                Else ''2
                    current_tab.Value = 1
                    MensajeError(strErr)
                    Exit Sub
                End If
            Else ''1
                current_tab.Value = 0
                MensajeError(strErr)
                Exit Sub
            End If

        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub
    'BBVA-P-412
    Protected Sub cmbTipoCalc_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoCalc.SelectedIndexChanged
        ''Sprint1
        'CargaCombos(4) 'Agregado 
        If cmbTipoCalc.SelectedItem.Text = "BALLOON" Then
            gdvPlazos.Columns(11).Visible = True
            gdvPlazos.Columns(12).Visible = True

            Dim gvrrow As GridViewRow

            Dim objTxt1 As TextBox
            Dim objTxt2 As TextBox
            Dim objChk As CheckBox
            Dim estatus As Boolean

            If chkcerocom.Checked = True Then
                estatus = False
            Else
                estatus = True
            End If

            For Each gvrrow In gdvPlazos.Rows


                objTxt1 = New TextBox
                objTxt2 = New TextBox

                objChk = gvrrow.Cells(0).Controls(1)

                objTxt1 = gvrrow.Cells(11).Controls(1)
                objTxt2 = gvrrow.Cells(12).Controls(1)
                If objChk.Checked = True Then
                    If String.IsNullOrEmpty(objTxt1.Text) Then
                        objTxt1.Text = "0.00"

                    End If
                    If String.IsNullOrEmpty(objTxt2.Text) Then
                        objTxt2.Text = "0.00"
                    End If


                Else

                    objTxt1.Text = ""
                    objTxt2.Text = ""
                End If

            Next


        Else
            gdvPlazos.Columns(11).Visible = False
            gdvPlazos.Columns(12).Visible = False
        End If

        If cmbTipoCalc.SelectedValue = 167 Then 'RQ18
            txtporanualidad.Text = CDbl(2.5).ToString("###,###.00")
            Dim gvrrow As GridViewRow

            For Each gvrrow In gdvPlazos.Rows
                Dim txt As TextBox
                txt = gvrrow.FindControl("TextBox8")
                txt.Enabled = True

            Next
            'BUG-PC-27 MAUT 23/12/2016 Si es compra inteligente se desmarca pago irregular
            'chkPagoIrreg.Checked = False
        Else
            txtporanualidad.Text = CDbl(2.0).ToString("###,###.00")
            Dim gvrrow As GridViewRow



            For Each gvrrow In gdvPlazos.Rows
                Dim txt As TextBox

                txt = gvrrow.FindControl("TextBox8")
                txt.Enabled = False

            Next




        End If
    End Sub

    'Sprint1
    Protected Sub rdbsegvidaws_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdbsegvidaws.CheckedChanged
        If rdbsegvidaws.Checked = True Then
            txtFactSegVid.Enabled = False
            txtFactSegVid.Text = ""
            chkIvaSegVida.Enabled = False
            chkIvaSegVida.Checked = False
            current_tab.Value = 2
        End If
    End Sub

    Protected Sub rdbsegvidafact_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdbsegvidafact.CheckedChanged
        If rdbsegvidafact.Checked = True Then
            txtFactSegVid.Enabled = True
            chkIvaSegVida.Enabled = True
            current_tab.Value = 2
        End If
    End Sub

    Protected Sub chkcerocom_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkcerocom.CheckedChanged

        Dim objRow As GridViewRow
        Dim objTxt As TextBox
        Dim objChk As CheckBox
        Dim estatus As Boolean

        If chkcerocom.Checked = True Then
            estatus = False
        Else
            estatus = True
        End If

        For Each objRow In gdvPlazos.Rows
            If objRow.RowType = DataControlRowType.DataRow Then
                objTxt = New TextBox

                objChk = New CheckBox
                objChk = objRow.Cells(0).Controls(1)
                objTxt = objRow.Cells(3).Controls(1)



                objTxt.Enabled = estatus

                If objChk.Checked = True Then
                    objTxt.Text = "0.00"

                Else
                    objTxt.Text = ""
                End If

            End If
        Next
        current_tab.Value = 3
    End Sub

    Private Function ConsulWS() As Boolean
        ConsulWS = False

        Dim dts As DataSet = New DataSet()
        Dim produg As SNProcotiza.clsSubProductosUG = New SNProcotiza.clsSubProductosUG()
        produg.CargaSubProductoUG(ddlsubprod.SelectedValue)

        Dim loanBASE As loanBASE = New loanBASE()

        Dim objpaqws As SNProcotiza.clsPaquetes = New SNProcotiza.clsPaquetes()
        dts = objpaqws.consultaWS(1)
        If objpaqws.ErrorPaquete <> "" Then
            strErr = "Eror al cargar información."
            Exit Function
        End If

        Dim plazos(0) As Integer
        Dim tasas(0) As Decimal
        Dim objChk As CheckBox
        Dim objText As TextBox
        Dim objRow As GridViewRow
        Try

            'If cmbTipoCalc.SelectedItem.Text = "BALLOON" Then
            gdvPlazos.Columns(10).Visible = True
            'Else
            'gdvPlazos.Columns(10).Visible = True
            'End If



            For Each objRow In gdvPlazos.Rows
                If objRow.RowType = DataControlRowType.DataRow Then
                    objChk = objRow.Cells(0).Controls(1)
                    If objChk.Checked = True Then
                        Dim str As String = objRow.Cells(4).Text
                        'If cmbTipoCalc.SelectedItem.Text = "BALLOON" Then
                        plazos(UBound(plazos)) = Convert.ToInt16(objRow.Cells(10).Text)
                        'Else
                        '    plazos(UBound(plazos)) = Convert.ToInt16(objRow.Cells(10).Text)
                        'End If

                        objText = objRow.Cells(3).Controls(1)
                        tasas(UBound(tasas)) = objText.Text.Trim
                        ReDim Preserve plazos(UBound(plazos) + 1)
                        ReDim Preserve tasas(UBound(tasas) + 1)
                    End If
                End If
            Next

        Catch ex As Exception

        End Try
        'If cmbTipoCalc.SelectedItem.Text = "BALLOON" Then
        gdvPlazos.Columns(10).Visible = False
        'Else
        'gdvPlazos.Columns(10).Visible = False
        'End If



        ReDim Preserve plazos(UBound(plazos) - 1)
        ReDim Preserve tasas(UBound(tasas) - 1)

        Dim i As Integer
        Dim maxindex As Integer = 0
        Dim minindex As Integer = 0
        Dim maxtasa As Double = 0

        For i = 1 To UBound(plazos)
            If plazos(i) > plazos(maxindex) Then
                maxindex = i
            End If
            If plazos(i) < plazos(minindex) Then
                minindex = i
            End If
        Next

        For i = 1 To UBound(tasas)
            If tasas(i) > tasas(maxtasa) Then
                maxtasa = i
            End If
        Next

        If objpaqws.ErrorPaquete = "" Then
            If dts.Tables(0).Rows.Count > 0 Then
                loanBASE.productCode = ddlprod.SelectedValue
                loanBASE.subProductCode = produg.Nombre
                loanBASE.startLoanDate = txtFinVig.Text & " 00:00:00:000000"
                loanBASE.loan.dueDate = txtIniVig.Text
                loanBASE.loan.loanProduct.extendedData.planType.id = "00002"
                loanBASE.loan.loanProduct.extendedData.operationType.id = "TR"
                loanBASE.loan.loanProduct.extendedData.money.currency = "0001"
                loanBASE.loan.loanProduct.extendedData.paymentType.id = ObtenValor(dts, cmbTipoVenc.SelectedValue, 5, 0)
                loanBASE.loan.loanProduct.extendedData.maximumAmount.amount = formatsendws(txtimpmaxg.Text.Trim, 15, 1)
                loanBASE.loan.loanProduct.extendedData.minimumCapital.amount = formatsendws(txtimpming.Text.Trim, 15, 1)
                loanBASE.loan.loanProduct.extendedData.maximumTerm = formatsendws(plazos(maxindex), 4, 0) ''"0072"
                loanBASE.loan.loanProduct.extendedData.minimumTerm = formatsendws(plazos(minindex), 4, 0)
                loanBASE.loan.loanProduct.extendedData.gracePeriodUnit = "000"

                loanBASE.loan.period.timeUnit.name = IIf(cmbPeriodicidad.SelectedValue = 83, "MEN", IIf(cmbPeriodicidad.SelectedValue = 95, "QUI", "SEM"))


                Dim l1 As listDtoRate = New listDtoRate()
                l1.type.name = "real"
                l1.percentage = formatsendws(tasas(maxtasa), 7, 1, 1)
                loanBASE.iLoanDetail.loanCar.listDtoRate.Add(l1)

                Dim l2 As listDtoRate = New listDtoRate()
                l2.type.name = "mora"
                l2.percentage = "0000000"
                loanBASE.iLoanDetail.loanCar.listDtoRate.Add(l2)

                loanBASE.iLoanDetail.loanCar.packageId = New String("0"c, 9 - Len(PaqID.ToString)) & PaqID.ToString 'PaqID
                loanBASE.iLoanDetail.loanCar.packageDescription = txtNom.Text.Trim()

                Dim rate As New rate()

                rate.percentage = CStr(ptj_tasa_ref.ToString) '"0000"
                loanBASE.refinancing.rate.Add(rate)
                rate = New rate
                rate.percentage = CStr(ptj_comision_ref.ToString) '"1111"
                loanBASE.refinancing.rate.Add(rate)

                Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                Dim jsonBODY As String = serializer.Serialize(loanBASE)

                Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
                Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
                Dim restGT As RESTful = New RESTful()
                restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("url") & System.Configuration.ConfigurationManager.AppSettings.Item("metodo")

                restGT.buscarHeader("ResponseWarningDescription")

                Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)

                Dim str As String = restGT.valorHeader

                Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()
                Dim alert = srrSerialer.Deserialize(Of msjerr)(jsonResult)

                If restGT.IsError Then
                    strErr = IIf(restGT.StatusHTTP = 500, "Error al consultar Servicio Web: ", "Mensaje del Servicio Web: ") & restGT.MensajeError & ". Estatus:" & restGT.StatusHTTP ''& "Estatus:" & restGT.StatusHTTP & ". Descripción: "  & "."
                Else
                    ConsulWS = True
                End If
            Else
                strErr = "No se encontro información."
                Exit Function
            End If
        Else
            strErr = "Error al consultar la información."
            Exit Function
        End If

        Return ConsulWS
    End Function

    Private Function formatsendws(valor As String, lng As Integer, isDec As Integer, Optional ispercent As Integer = 0) As String
        Dim strresult As String = String.Empty
        Dim Pos As Integer = 0

        Select Case ispercent
            Case 0
                If isDec = 1 Then
                    Pos = InStr(valor, ".")
                    If Pos > 0 Then
                        strresult = ((valor).Substring(0, Pos) & (valor).Substring(Pos, 2)).Replace(".", "")
                    Else
                        strresult = valor & "00"
                    End If
                    strresult = New String("0"c, lng - Len(strresult)) & strresult
                Else
                    strresult = New String("0"c, lng - Len(valor)) & valor
                End If
            Case 1
                Pos = InStr(valor, ".")
                If Pos > 0 Then
                    Dim strdec As String = valor.Substring(Pos, valor.Length - Pos)
                    If strdec.Length < 4 Then
                        strdec = strdec & New String("0"c, 4 - Len(strdec))
                    End If

                    Dim strent As String = valor.Substring(0, Pos - 1)
                    strresult = New String("0"c, lng - Len(strent & strdec)) & (strent & strdec)
                Else
                    strresult = valor & "0000"
                    strresult = New String("0"c, lng - Len(strresult)) & strresult
                End If
        End Select

        Return strresult
    End Function

    Private Function ObtenValor(dts As DataSet, valor As Integer, lng As Integer, isDec As Integer) As String
        Dim strdato As String = String.Empty

        Dim renglones() As DataRow

        renglones = dts.Tables(0).Select("IDINTERNO = " & valor.ToString)

        For Each dato As DataRow In renglones
            strdato = dato("IDEXTERNO").ToString
        Next

        strdato = formatsendws(strdato, lng, isDec)

        Return strdato
    End Function

    'BUG-PC?? 17/03/2017 MAPH Corrección de Validación de CheckBoxes para Cajas de Texto realizada desde Page_Load
    'Private Sub chkPtjSubsidio_CheckedChanged(sender As Object, e As EventArgs) Handles chkPtjSubsidio.CheckedChanged, chkSubsidio.CheckedChanged
    '    If Val(Request("paqId")) > 0 Then
    '        If chkPtjSubsidio.Checked = False And chkPtjSubsidio.Checked = False Then
    '            txtporsubArma.Enabled = False
    '            txtporsubAgencia.Enabled = False
    '            txtporsubArma.Text = "0.00"
    '            txtporsubAgencia.Text = "0.00"
    '        Else
    '            txtporsubArma.Enabled = False
    '            txtporsubAgencia.Enabled = False
    '            objPaq.CargaPaquete(Val(Request("paqId")))
    '            If objPaq.ErrorPaquete = "" Then
    '                txtporsubArma.Text = objPaq.PorSubsidioArmadora
    '                txtporsubAgencia.Text = objPaq.PorSubsidioAgencia
    '            Else
    '                MensajeError(objPaq.ErrorPaquete)
    '                Exit Sub
    '            End If
    '        End If
    '    Else
    '        If chkPtjSubsidio.Checked = False Then
    '            txtporsubArma.Text = "0.00"
    '            txtporsubArma.Enabled = False
    '            txtporsubAgencia.Text = "0.00"
    '            txtporsubAgencia.Enabled = False
    '        Else
    '            txtporsubArma.Enabled = True
    '            txtporsubAgencia.Enabled = True
    '        End If
    '    End If
    'End Sub

    Public Class loanBASE
        Public productCode As String
        Public subProductCode As String
        Public startLoanDate As String
        Public loan As loan = New loan()
        Public iLoanDetail As iLoanDetail = New iLoanDetail()
        Public refinancing As refinancing = New refinancing()
    End Class

    Public Class loan
        Public loanProduct As loanProduct = New loanProduct()
        Public dueDate As String
        Public period As period = New period
    End Class

    Public Class loanProduct
        Public extendedData As extendedData = New extendedData()
    End Class

    Public Class extendedData
        Public planType As planType = New planType()
        Public operationType As operationType = New operationType()
        Public money As money = New money()
        Public paymentType As paymentType = New paymentType()
        Public maximumAmount As maximumAmount = New maximumAmount()
        Public minimumCapital As minimumCapital = New minimumCapital()
        Public maximumTerm As String
        Public minimumTerm As String
        Public gracePeriodUnit As String
    End Class

    Public Class planType
        Public id As String
    End Class

    Public Class operationType
        Public id As String
    End Class

    Public Class money
        Public currency As String
    End Class

    Public Class paymentType
        Public id As String
    End Class

    Public Class maximumAmount
        Public amount As String
    End Class

    Public Class minimumCapital
        Public amount As String
    End Class

    Public Class period
        Public timeUnit As timeUnit = New timeUnit
    End Class

    Public Class timeUnit
        Public name As String
    End Class

    Public Class iLoanDetail
        Public loanCar As loanCar = New loanCar()
    End Class

    Public Class loanCar
        Public listDtoRate As List(Of listDtoRate) = New List(Of listDtoRate)
        Public packageId As String
        Public packageDescription As String
    End Class

    Public Class listDtoRate
        Public type As Tipe = New Tipe()
        Public percentage As String
    End Class

    Public Class Tipe
        Public name As String
    End Class

    Public Class msjerr
        Public message As String
        Public status As String
    End Class

    Public Class refinancing
        Public rate As List(Of rate) = New List(Of rate)
    End Class

    Public Class rate
        Public percentage As String = String.Empty
    End Class

    Protected Sub chkTO_CheckedChanged(sender As Object, e As EventArgs)
        Dim var1 As CheckBox
        var1 = CType(Me.gdvTO.Rows.Item(0).FindControl("chkTO"), CheckBox)

        If var1.Checked = True Then
            Dim gvrrow As GridViewRow
            For Each gvrrow In gdvPlazos.Rows
                Dim txt As TextBox
                Dim txt1 As TextBox
                txt = gvrrow.FindControl("TextBox8")
                txt1 = gvrrow.FindControl("TextBox6")
                txt.Enabled = True
                txt1.Enabled = True
            Next
        Else
            Dim gvrrow As GridViewRow
            For Each gvrrow In gdvPlazos.Rows
                Dim txt As TextBox
                Dim txt1 As TextBox
                txt = gvrrow.FindControl("TextBox8")
                txt1 = gvrrow.FindControl("TextBox6")
                txt.Enabled = False
                txt1.Enabled = False
            Next
        End If
    End Sub
End Class


