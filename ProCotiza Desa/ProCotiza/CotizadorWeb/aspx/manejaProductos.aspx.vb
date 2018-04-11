'BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.
'BUG-PC-02:  09/11/2016: AMR: Error en manejaProductos
'BUG-PC-09:AMR:16/11/2016 CUTARMA01:No pertime espacios en blanco al ingresar el nombre de la marca
'BUG-PC-18:  25/11/2016: AMR : CUTARPR01-El campo dice Tipo, se solicitó Tipo de Producto.
'BUG-PC-46 10/02/17 JRHM: SE CORRIGE PROBLEMA DE COMBO DE CLASIFICACION DEL PRODUCTO AL CREAR UN NUEVO PRODUCTO
'BUG-PC-48 15/02/17 JRHM SE CORRIGUE VALIDACION DE CMBCLASIF POR CAMBIO DE VALUE 176
'RQ-SEGIP : RHERNANDEZ: 19/07/17: SE CONFIGURO CODIGO BACK PARA GUARDAR LOS VALORES DE LAS CONSTANTES DE UN PRODUCTO EN ESPECIFICO
'BUG-PC-105: RHERNANDEZ: 07/09/17: SE AGREGA VALOR CUOTA PARA LOS CALCULO DE SEGUROS POR FACTOR
'RQ-MN2-6:RHERNANDEZ: 07/09/17: SE AGREGA ID BROKER PARA LA BUSQUEDA MAS AGIL DE PRODUCTOS-
'BUG-PC-122: RHERNANDEZ: 10/11/17: SE CORRIGE PROBLEMA AL MODIFICAR UN COMBO SE PERDIAN LOS VALORES DE BROKERS
Imports System.Data
Imports SNProcotiza

Partial Class aspx_manejaProductos
    Inherits System.Web.UI.Page
    Dim strErr As String = ""
    Dim objproducto As New SNProcotiza.clsProductos 'BBVA-P-412:RQ18
    Dim objCombo As New clsProcGenerales
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()
        'BBVA-P-412:RQ18
        If Not IsPostBack Then
            'BUG-PC-02
            If Not CargaCombos(1) Then Exit Sub
            If Not CargaCombos(2) Then
                Exit Sub

            Else
                cmbClasif_SelectedIndexChanged(cmbClasif, Nothing)
            End If
            If Not CargaCombos(3) Then Exit Sub
            If Not CargaCombos(4) Then Exit Sub
            If Not CargaCombos(5) Then Exit Sub
            If Not CargaCombos(6) Then Exit Sub
            If Not CargaCombos(7) Then Exit Sub
            If Not CargaCombos(8) Then Exit Sub
            If Not CargaCombos(9) Then Exit Sub

            If Val(Request("idProd")) > 0 Then
                CargaInfo()
            End If
            'BBVA-P-412:RQ18
            Select Case cmbClasif.SelectedValue
                Case 63 ''Nuevo
                    txtpreciosm.Enabled = False
                    txtPrecio.Enabled = True
                    If Val(Request("idProd")) > 0 Then
                        objproducto.CargaProducto(Val(Request("idProd")))
                        txtPrecio.Text = objproducto.Precio
                        txtpreciosm.Text = 0
                    Else
                        txtPrecio.Text = 0
                        txtpreciosm.Text = 0
                    End If
                Case 64 ''SemiNuevo
                    txtpreciosm.Enabled = True
                    txtPrecio.Enabled = False
                    If Val(Request("idProd")) > 0 Then
                        objproducto.CargaProducto(Val(Request("idProd")))
                        txtPrecio.Text = 0
                        txtpreciosm.Text = objproducto.PrecioSemi
                    Else
                        txtPrecio.Text = 0
                        txtpreciosm.Text = 0
                    End If
                Case 176 ''Ambos
                    txtpreciosm.Enabled = True
                    txtPrecio.Enabled = True
                    If Val(Request("idProd")) > 0 Then
                        objproducto.CargaProducto(Val(Request("idProd")))
                        txtPrecio.Text = objproducto.Precio
                        txtpreciosm.Text = objproducto.PrecioSemi
                    Else
                        txtPrecio.Text = 0
                        txtpreciosm.Text = 0
                    End If
            End Select
            llenaGRV()
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

    ''BBVA-P-412
    Private Sub CargaInfo()
        Try
            Dim intProd As Integer = Val(Request("idProd"))
            Dim objProd As New SNProcotiza.clsProductos(intProd)

            If objProd.IDProducto > 0 Then
                lblId.Text = objProd.IDProducto
                cmbTipoProd.SelectedValue = objProd.IDTipoProducto
                cmbClasif.SelectedValue = objProd.IDClasificacion
                cmbMarca.SelectedValue = objProd.IDMarca
                If Not CargaCombos(4) Then Exit Sub
                ddlsubmarca.SelectedValue = objProd.IDSubmarca
                ddlversion.SelectedValue = objProd.IDVersion
                If Not CargaCombos(5) Then Exit Sub
                ddlanio.SelectedValue = objProd.IDAnio
                ''txtNom.Text = objProd.Descripcion ''BBVA-P-412
                txtPrecio.Text = objProd.Precio
                txtpreciosm.Text = objProd.PrecioSemi
                ''txtModelo.Text = IIf(objProd.AñoModelo <= 0, "", objProd.AñoModelo) 
                txtImagen.Text = objProd.ImagenProducto
                txtUid.Text = objProd.IDExterno
                cmbEstatus.SelectedValue = objProd.IDEstatus
                chkDefault.Checked = IIf(objProd.RegistroDefault = 1, True, False)
                ''cmbFamilia.SelectedValue = objProd.IDfamilia
                ddlbroker.SelectedValue = objProd.idbroker
            Else
                MensajeError("No se localizó información para el modelo.")
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub
    'BBVA-P-412:RQ18
    Private Function CargaCombos(ByVal intOpc As Integer) As Boolean
        Dim objCombo As New clsProcGenerales
        Dim dtsRes As New DataSet
        Try
            CargaCombos = False

            Select Case intOpc
                Case 1
                    'combo tipo
                    Dim objTP As New SNProcotiza.clsTipoProductos
                    objTP.IDEstatus = 2
                    dtsRes = objTP.ManejaTipoProd(1)
                    strErr = objTP.ErrorTipoProducto

                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "DESCRIPCION", "ID_TIPO_PRODUCTO", cmbTipoProd, strErr, False)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                        cmbTipoProd.SelectedValue = Val(Request("idTipo"))
                    End If

                Case 2
                    'combo de clasificación
                    dtsRes = objCombo.ObtenInfoParametros(62, strErr)
                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbClasif, strErr, False)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If

                Case 3
                    'combo de marcas
                    Dim objMarca As New SNProcotiza.clsMarcas
                    objMarca.IDEstatus = 2
                    objMarca.IDTipoRegistro = 113 'trae marcas que puedan registrar productos

                    dtsRes = objMarca.ManejaMarca(5)
                    strErr = objMarca.ErrorMarcas

                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_MARCA", cmbMarca, strErr, False, True, "REG_DEFAULT") 'BUG-PC-09
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                        cmbMarca.SelectedValue = Val(Request("idMarca"))
                    End If

                    If Val(Request("idProd")) > 0 Then
                        Dim objprod As New SNProcotiza.clsProductos(Val(Request("idProd")))
                        cmbMarca.SelectedValue = objprod.IDMarca
                    End If

                Case 4
                    'combo submarcas
                    Dim objsubmarca As New SNProcotiza.clsSubMarcas
                    objsubmarca.IDEstatus = 2
                    objsubmarca.IDMarca = cmbMarca.SelectedValue

                    dtsRes = objsubmarca.ManejaSubMarca(1)
                    If objsubmarca.ErrorSubMarcas = "" Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            objCombo.LlenaCombos(dtsRes, "DESCRIPCION", "ID_SUBMARCA", ddlsubmarca, strErr, False, True, "REG_DEFAULT") 'BUG-PC-09
                            If strErr <> "" Then
                                MensajeError(strErr)
                                Exit Function
                            End If
                        Else
                            MensajeError("No se encontro información de Submarcas.")
                            Exit Function 'BUG-PC-02
                        End If
                    Else
                        MensajeError(objsubmarca.ErrorSubMarcas)
                        Exit Function
                    End If
                    If Val(Request("idProd")) > 0 Then
                        Dim objprod As New SNProcotiza.clsProductos(Val(Request("idProd")))
                        ddlsubmarca.SelectedValue = objprod.IDSubmarca
                    End If
                Case 5
                    'version
                    Dim objVersion As New SNProcotiza.clsVersiones
                    objVersion.IDEstatus = 2
                    objVersion.IDMarca = cmbMarca.SelectedValue
                    objVersion.IDSubMarca = ddlsubmarca.SelectedValue
                    dtsRes = objVersion.ManejaVersion(1)

                    If objVersion.ErrorVersion = "" Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                            objCombo.LlenaCombos(dtsRes, "DESCRIPCION", "ID_VERSION", ddlversion, strErr, False, True, "REG_DEFAULT") 'BUG-PC-09
                            If strErr <> "" Then
                                MensajeError(strErr)
                                Exit Function
                            End If
                        Else
                            MensajeError("No se encontro información de Versiones para la marca " & cmbMarca.SelectedItem.Text & ", Submarca " & ddlsubmarca.SelectedItem.Text & ".")
                            ddlversion.Items.Clear()
                            Exit Function
                        End If
                    Else
                        MensajeError(objVersion.ErrorVersion)
                        Exit Function
                    End If
                Case 6
                    'combo años
                    Dim objanio As New SNProcotiza.clsAnios
                    dtsRes = objanio.ConsultaAnio()
                    If dtsRes.Tables(0).Rows.Count > 0 Then
                        objCombo.LlenaCombos(dtsRes, "ANIO", "ID_ANIO", ddlanio, strErr, False)
                        If strErr <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError("No se encontro información de Años.")
                    End If
                Case 7
                    'combo de estatus
                    dtsRes = objCombo.ObtenInfoParametros(1, strErr)
                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbEstatus, strErr, False)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If
                Case 9
                    Dim objbroker As New SNProcotiza.clsBrokerSeguros
                    objbroker.IDEstatus = 2
                    dtsRes = objbroker.ManejaBroker(1)
                    If objbroker.ErrorBroker = "" Then

                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                objCombo.LlenaCombos(dtsRes, "RAZON_SOCIAL", "ID_BROKER", ddlbroker, strErr, True)
                                If strErr <> "" Then
                                    Exit Function
                                End If
                            Else
                                strErr = "No se encontro información de Brokers."
                                Exit Function
                            End If
                        Else
                            strErr = "No se encontro información."
                            Exit Function
                        End If
                    Else
                        strErr = objbroker.ErrorBroker
                        Exit Function
                    End If
                    ''''BBVA-P-412
                    'Case 8
                    '    'combo de familias
                    '    Dim objFamilias As New SNProcotiza.clsFamilias
                    '    objFamilias.IDEstatus = 2

                    '    dtsRes = objFamilias.ManejaFamilia(1)
                    '    strErr = objFamilias.ErrorFamilias

                    '    If Trim$(strErr) = "" Then
                    '        objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_FAMILIA", cmbFamilia, strErr, False)
                    '        If Trim$(strErr) <> "" Then
                    '            MensajeError(strErr)
                    '            Exit Function
                    '        End If
                    '    End If
            End Select

            CargaCombos = True
        Catch ex As Exception
            CargaCombos = False
            MensajeError(ex.Message)
        End Try
    End Function
    'BBVA-P-412:RQ18
    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then

                Dim totreg As Integer = grvAdicionales.Rows.Count - 1
                For Each row As GridViewRow In grvAdicionales.Rows
                    Dim ndx As Integer = row.RowIndex
                    Dim ddlaseg As DropDownList = TryCast(row.FindControl("DropDownList1"), DropDownList)
                    Dim boxConstante As TextBox = TryCast(row.FindControl("txtConstante"), TextBox)
                    Dim boxCuota As TextBox = TryCast(row.FindControl("txtCuota"), TextBox)
                    If totreg = ndx Then
                        If ddlaseg.SelectedValue <> 0 Or boxConstante.Text <> "" Or boxCuota.Text <> "" Then
                            If ddlaseg.SelectedValue = 0 Or boxConstante.Text = "" Or boxCuota.Text = "" Then
                                MensajeError("Todos los campos marcados con * son obligatorios.")
                                Exit Sub
                            End If

                        End If
                    End If
                Next

                Dim intProd As Integer = Val(Request("idProd"))
                Dim objProd As New SNProcotiza.clsProductos
                Dim objanio As New SNProcotiza.clsAnios
                Dim intOpc As Integer = 2
                Dim dts As New DataSet

                objProd.CargaSession(Val(Session("cveAcceso")))
                If intProd > 0 Then
                    objProd.CargaProducto(intProd)
                    intOpc = 3
                End If

                'guardamos la info del tipo de operación
                objProd.IDTipoProducto = Val(cmbTipoProd.SelectedValue)
                objProd.IDMarca = Val(cmbMarca.SelectedValue)
                objProd.IDClasificacion = Val(cmbClasif.SelectedValue)
                objProd.IDFamilia = 1 ''Val(cmbFamilia.SelectedValue) ''BBVA-P-412
                objProd.Descripcion = ddlsubmarca.SelectedItem.Text & " " & ddlversion.SelectedItem.Text ''BBVA-P-412
                objProd.Precio = Val(txtPrecio.Text)

                dts = objanio.CargaAnio(ddlanio.SelectedValue)
                If objanio.ErrAnio = "" Then
                    objProd.AñoModelo = dts.Tables(0).Rows(0).Item("ANIO") ''BBVA-P-412
                Else
                    MensajeError(objanio.ErrAnio)
                    Exit Sub
                End If

                objProd.ImagenProducto = Trim(txtImagen.Text)
                objProd.IDExterno = Trim(txtUid.Text)
                objProd.RegistroDefault = IIf(chkDefault.Checked, 1, 0)
                objProd.IDEstatus = Val(cmbEstatus.SelectedValue)
                objProd.UsuarioRegistro = objProd.UserNameAcceso
                objProd.IDSubmarca = ddlsubmarca.SelectedValue
                objProd.IDVersion = ddlversion.SelectedValue
                objProd.IDAnio = ddlanio.SelectedValue
                objProd.PrecioSemi = Val(txtpreciosm.Text)
                objProd.idbroker = Val(ddlbroker.SelectedValue)
                objProd.ManejaProducto(intOpc)
                If objProd.ErrorProducto = "" Then

                    ''***************************************
                    ''MANDAMOS UN MAIL AVISANDO DE LA ACCIÓN
                    'Try
                    '    Dim objAvi As New SNProcotiza.clsAvisos(1)
                    '    If objAvi.IDEstatus = 2 Then
                    '        Dim dtsMails As New DataSet
                    '        dtsMails = objAvi.ManejaAviso(7)

                    '        If objAvi.ErrorAvisos = "" Then
                    '            If dtsMails.Tables.Count > 0 Then
                    '                If dtsMails.Tables(0).Rows.Count > 0 Then
                    '                    Dim objRow As DataRow
                    '                    Dim objGen As New clsProcGenerales

                    '                    Dim strMails As String = ""
                    '                    Dim strAsunto As String = dtsMails.Tables(0).Rows(0).Item("AVISO")
                    '                    Dim strTexto As String = IIf(intOpc = 2, "agregó", "modificó")

                    '                    strTexto = "Se " & strTexto & " el producto con ID " & objProd.IDProducto & _
                    '                               " con la siguiente información: " & Chr(13) & Chr(13) & _
                    '                               " CLASE: " & cmbTipoProd.SelectedItem.Text & Chr(13) & _
                    '                               " DISTRIBUIDOR: " & cmbMarca.SelectedItem.Text & Chr(13) & _
                    '                               " CLASIFICACIÓN: " & objGen.ObtenDescripcionParametroSis(objProd.IDClasificacion) & Chr(13) & _
                    '                               " DESCRIPCIÓN: " & objProd.Descripcion & Chr(13) & _
                    '                               " ESTATUS: " & objGen.ObtenDescripcionParametroSis(objProd.IDEstatus) & Chr(13) & _
                    '                               " FAMILIA: " & cmbFamilia.SelectedItem.Text & Chr(13) & _
                    '                               " AÑO MODELO: " & objProd.AñoModelo & Chr(13) & _
                    '                               " NOMBRE IMAGEN: " & objProd.ImagenProducto & Chr(13) & _
                    '                               " UID: " & objProd.IDExterno & Chr(13) & _
                    '                               " PRECIO BASE: " & objProd.Precio & Chr(13) & _
                    '                               " PRODUCTO DEFAULT: " & IIf(objProd.RegistroDefault = 1, "SI", "NO")

                    '                    For Each objRow In dtsMails.Tables(0).Rows
                    '                        If objRow.Item("MAIL").ToString <> "" Then
                    '                            strMails += IIf(Trim(strMails) = "", "", "; ") & objRow.Item("MAIL").ToString
                    '                        End If
                    '                    Next

                    '                    objGen.EnviaMail(strAsunto, strMails, strTexto)
                    '                End If
                    '            End If
                    '        End If
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    ''***************************************

                    GuardaConstantes(objProd.IDProducto, strErr)

                    CierraPantalla("./consultaProductos.aspx")
                Else
                    MensajeError(objProd.ErrorProducto)
                End If
            Else
                MensajeError("Todos los campos marcados con * son obligatorios.")
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub
    Private Function GuardaConstantes(ID_PROD As Integer, ByRef strError As String) As Boolean
        Try

            GuardaConstantes = False

            Dim objAdicionales As New clsConstantes
            Dim dtsRes As New DataSet
            Dim objRow As GridViewRow

            Dim objAseg As Object
            Dim objCuota As Object
            Dim objConstantes As Object
            Dim objRegNuevo As Object

            Dim RegNuevo As String = ""
            Dim dtsTabla As New DataSet



            For Each objRow In grvAdicionales.Rows
                objAseg = objRow.Cells(1).Controls(1)
                objCuota = objRow.Cells(2).Controls(1)
                objConstantes = objRow.Cells(3).Controls(1)
                objRegNuevo = objRow.Cells(4).Controls(1)

                objRegNuevo = objRow.FindControl("lblCadena")
                RegNuevo = objRegNuevo.Text

                If RegNuevo = "" And objAseg.selectedvalue > 0 Then
                    If objConstantes.text <> "" Then
                        objAdicionales.ID_PRODUCTO = ID_PROD
                        objAdicionales.opcion = 3
                        objAdicionales.ID_ASEGURADORA = objAseg.selectedvalue
                        objAdicionales.ValCuota = CDbl(objCuota.text.ToString)
                        objAdicionales.Constantes = CDbl(objConstantes.text.ToString)



                        objAdicionales.ManejaConstantes()
                    Else
                        strError = "Todos los campos marcados con * son obligatorios.'"

                        Exit Function
                    End If
                End If




                If objAdicionales.ErrorConstante <> "" Then
                    MensajeError(objAdicionales.ErrorConstante)
                    strError = objAdicionales.ErrorConstante
                    GuardaConstantes = False
                    Exit Function
                End If

            Next

            GuardaConstantes = True
            ViewState("CurrentTable") = Nothing
            llenaGRV()
        Catch ex As Exception
            strError = ex.Message
            GuardaConstantes = False
        End Try
    End Function

    'BBVA-P-412:RQ18
    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If cmbEstatus.SelectedValue = "" Then Exit Function
        If cmbTipoProd.SelectedValue = "" Then Exit Function
        If cmbClasif.SelectedValue = "" Then Exit Function
        If cmbMarca.SelectedValue = "" Then Exit Function
        If ddlbroker.SelectedValue = 0 Then Exit Function

        ''If Trim(txtNom.Text) = "" Then Exit Function ''BBVA-P-412
        ''If Trim(txtPrecio.Text) = "" Then Exit Function

        Select Case cmbClasif.SelectedValue
            Case 63 ''Nuevo
                If Val(txtPrecio.Text.Trim) = 0 Then Exit Function
            Case 64 ''SemiNuevo
                If Val(txtpreciosm.Text.Trim) = 0 Then Exit Function
            Case 176 ''Ambos
                If Val(txtPrecio.Text.Trim) = 0 Then Exit Function
                If Val(txtpreciosm.Text.Trim) = 0 Then Exit Function
        End Select

        ValidaCampos = True
    End Function

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaProductos.aspx")
    End Sub
    'BBVA-P-412:RQ18
    Protected Sub cmbMarca_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbMarca.SelectedIndexChanged

        If Not CargaCombos(4) Then Exit Sub
        If Not CargaCombos(5) Then Exit Sub
        If Not CargaCombos(6) Then Exit Sub
        If Not CargaCombos(7) Then Exit Sub
        If Not CargaCombos(9) Then Exit Sub
    End Sub

    Protected Sub ddlsubmarca_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlsubmarca.SelectedIndexChanged
        If Not CargaCombos(5) Then Exit Sub
        If Not CargaCombos(6) Then Exit Sub
        If Not CargaCombos(7) Then Exit Sub
        If Not CargaCombos(9) Then Exit Sub
    End Sub

    Protected Sub cmbClasif_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbClasif.SelectedIndexChanged
        Select Case cmbClasif.SelectedValue
            Case 63 ''Nuevo
                txtpreciosm.Enabled = False
                txtPrecio.Enabled = True
                If Val(Request("idProd")) > 0 Then
                    objproducto.CargaProducto(Val(Request("idProd")))
                    txtPrecio.Text = objproducto.Precio
                    txtpreciosm.Text = 0
                Else
                    txtPrecio.Text = 0
                    txtpreciosm.Text = 0
                End If
            Case 64 ''SemiNuevo
                txtpreciosm.Enabled = True
                txtPrecio.Enabled = False
                If Val(Request("idProd")) > 0 Then
                    objproducto.CargaProducto(Val(Request("idProd")))
                    txtPrecio.Text = 0
                    txtpreciosm.Text = objproducto.PrecioSemi
                Else
                    txtPrecio.Text = 0
                    txtpreciosm.Text = 0
                End If
            Case 176 ''Ambos
                txtpreciosm.Enabled = True
                txtPrecio.Enabled = True
                If Val(Request("idProd")) > 0 Then
                    objproducto.CargaProducto(Val(Request("idProd")))
                    txtPrecio.Text = objproducto.Precio
                    txtpreciosm.Text = objproducto.PrecioSemi
                Else
                    txtPrecio.Text = 0
                    txtpreciosm.Text = 0
                End If
        End Select
    End Sub

    Protected Sub ddlversion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlversion.SelectedIndexChanged
        If Not CargaCombos(6) Then Exit Sub
        If Not CargaCombos(7) Then Exit Sub
        If Not CargaCombos(9) Then Exit Sub
    End Sub
    Private Sub llenaGRV()


        Dim ObjConstantes As New clsConstantes
        Dim dsConstantes As DataSet = New DataSet

        ObjConstantes.ID_PRODUCTO = IIf(Request("idProd").ToString = "", 0, Request("idProd").ToString)
        ObjConstantes.opcion = 2


        dsConstantes = ObjConstantes.ManejaConstantes()

        If dsConstantes.Tables.Count > 0 Then
            If dsConstantes.Tables(0).Rows.Count > 0 Then
                ViewState("CurrentTable") = dsConstantes.Tables(0)
                grvAdicionales.DataSource = ViewState("CurrentTable")
                grvAdicionales.DataBind()
                AddNewRowToGrid()
            Else
                SetInitialRow()

            End If
        Else
            SetInitialRow()
        End If
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
    Private Sub SetPreviousData()

        Dim rowIndex As Integer = 0
        If ViewState("CurrentTable") IsNot Nothing Then

            Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            If dt.Rows.Count > 0 Then

                For i As Integer = 0 To dt.Rows.Count - 1

                    Dim ddl1 As DropDownList = DirectCast(grvAdicionales.Rows(rowIndex).Cells(1).FindControl("DropDownList1"), DropDownList)
                    Dim boxCuota As TextBox = DirectCast(grvAdicionales.Rows(rowIndex).Cells(1).FindControl("txtCuota"), TextBox)
                    Dim boxConstante As TextBox = DirectCast(grvAdicionales.Rows(i).Cells(2).FindControl("txtConstante"), TextBox)

                    'Fill the DropDownList with Data   
                    FillDropDownList(ddl1, 1)



                    If i < dt.Rows.Count - 1 Then

                        ddl1.ClearSelection()
                        ddl1.SelectedValue = dt.Rows(i)("Column1").ToString()
                        ddl1.Enabled = False

                        boxCuota.Text = dt.Rows(i)("Column2").ToString()
                        boxCuota.Enabled = False

                        boxConstante.Text = dt.Rows(i)("Column3").ToString()
                        boxConstante.Enabled = False



                    End If

                    rowIndex += 1
                Next
            End If
        End If
    End Sub
    Private Sub FillDropDownList(ddl As DropDownList, opc As Integer)
        Dim dtsRes As New DataSet

        Select Case opc
            Case Is = 1
                'cargamos los accesorios disponibles
                Dim objConst As New SNProcotiza.clsConstantes
                ''objAcc.IDTipoProducto = Val(cmbTipoProd.SelectedValue)
                objConst.opcion = 1


                dtsRes = objConst.ManejaConstantes()
                If objConst.ErrorConstante = "" Then
                    If dtsRes.Tables(0).Rows.Count > 0 Then
                        objCombo.LlenaCombos(dtsRes, "RAZON_SOCIAL", "ID_ASEGURADORA", ddl, strErr, True)
                        If strErr <> "" Then
                            MensajeError(strErr)
                            Exit Sub
                        End If
                    Else
                        MensajeError("No se encontro información de Accesorios.")
                        Exit Sub
                    End If
                Else
                    MensajeError(objConst.ErrorConstante)
                    Exit Sub
                End If
        End Select
    End Sub
    Protected Sub cmdAgregaAcc_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles cmdAgregaAcc.Click
        Dim count As Integer = 0
        For Each row As GridViewRow In grvAdicionales.Rows
            Dim ddlaseg As DropDownList = TryCast(row.FindControl("DropDownList1"), DropDownList)
            Dim boxCuota As TextBox = TryCast(row.FindControl("txtCuota"), TextBox)
            Dim boxConstante As TextBox = TryCast(row.FindControl("txtConstante"), TextBox)
            Dim ndx As Integer = row.RowIndex
            If Not ValidaConst(ddlaseg, boxConstante, boxCuota, ndx) Then
                MensajeError(strErr)
                Exit Sub
            End If

        Next
        AddNewRowToGrid()
    End Sub
    Private Function ValidaConst(ByVal aseg As DropDownList, ByVal constan As TextBox, ByVal cuota As TextBox, index As Integer) As Boolean
        ValidaConst = False
        Dim repetidos As Integer = 0
        LimpiaError()

        If aseg.SelectedValue = 0 Then
            strErr = "Debe seleccionar una aseguradora"
            Exit Function
        End If

        If cuota.Text.Trim.Length = 0 Then
            strErr = "Debe ingresar un valor en cuota"
            Exit Function
        End If

        If constan.Text.Trim.Length = 0 Then
            strErr = "Debe ingresar un valor en constante"
            Exit Function
        End If


        'se evalua que el valor de los creditos colocados sea diferente de los que ya existen

        For Each subrow As GridViewRow In grvAdicionales.Rows
            If index <> subrow.RowIndex Then
                Dim ddlAseg As DropDownList = TryCast(subrow.FindControl("DropDownList1"), DropDownList)
                Dim boxCons As TextBox = TryCast(subrow.FindControl("txtConstante"), TextBox)
                Dim boxCuota As TextBox = TryCast(subrow.FindControl("txtCuota"), TextBox)
                If ddlAseg.SelectedValue <> 0 And boxCons.Text <> "" And boxCuota.Text <> "" Then
                    If ddlAseg.SelectedValue = aseg.SelectedValue Then
                        repetidos = repetidos + 1
                        Exit For
                    End If
                End If
            End If
        Next


        If repetidos > 0 Then
            strErr = "No se puede repetir la constante para la aseguradora " + aseg.SelectedItem.Text
            Exit Function
        End If

        ValidaConst = True
        Return ValidaConst
    End Function
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

                    Dim boxCuota As TextBox = DirectCast(grvAdicionales.Rows(i).Cells(2).FindControl("txtCuota"), TextBox)
                    dtCurrentTable.Rows(i)("Column2") = boxCuota.Text

                    Dim boxEnganche As TextBox = DirectCast(grvAdicionales.Rows(i).Cells(2).FindControl("txtConstante"), TextBox)
                    dtCurrentTable.Rows(i)("Column3") = boxEnganche.Text



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

                Dim objAdicionales As New clsConstantes


                If Filtro(0) <> "" Then
                    objAdicionales.opcion = 4
                    objAdicionales.ID_PRODUCTO = lblId.Text.ToString
                    objAdicionales.ID_ASEGURADORA = Filtro(0)
                    objAdicionales.ManejaConstantes()
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
End Class
