'BUG-PC-22 02-11-2016 MAUT Se agrega segundo nombre

Imports System.Configuration
Imports System.Data
Imports SNProcotiza
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Partial Class aspx_cotSolicitud
    Inherits System.Web.UI.Page
    Dim strErr As String = String.Empty
    Dim _CveSolicitud As Integer = 0
    Dim _CveCotizacion As Integer = 0
    Dim _CveCotSeguro As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()

        _CveCotizacion = Val(Request("idCotizacion"))
        _CveCotSeguro = Val(Request("idCotSeguro"))
        _CveSolicitud = Val(Request("Propuesta"))

        If Not IsPostBack Then
            CargaCatalogos()
            ObtenSolicitud()

            If txtNumSolicitud.Text = 0 Then
                cmdImprime.Enabled = False
            Else
                cmdImprime.Enabled = True
            End If

        End If
    End Sub

    Public Sub LimpiaError()
        lblMensaje.Text = ""
    End Sub

    Private Sub MensajeError(ByVal strMsj As String)
        strMsj = Replace(strMsj, "'", "")
        strMsj = Replace(strMsj, """", "")
        strMsj = Replace(strMsj, "(", "")
        strMsj = Replace(strMsj, ")", "")
        lblMensaje.Text = "<script>alert('" & strMsj & "')</script>"
    End Sub

    Private Sub CargaCatalogos()

        Dim _ObjDs As New DataSet

        Me.cmbSexo.Items.Add(New ListItem("FEMENINO", "1"))
        Me.cmbSexo.Items.Add(New ListItem("MASCULINO", "2"))

        Me.cmbVive.Items.Add(New ListItem("PROPIA", "1"))
        Me.cmbVive.Items.Add(New ListItem("RENTADA", "2"))
        Me.cmbVive.Items.Add(New ListItem("HIPOTECA", "3"))
        Me.cmbVive.Items.Add(New ListItem("CON FAM", "4"))

        Me.cmbPropiedad.Items.Add(New ListItem("SI", "1"))
        Me.cmbPropiedad.Items.Add(New ListItem("NO", "2"))

        Me.cmbEstadoCivil.Items.Add(New ListItem("CASADO", "1"))
        Me.cmbEstadoCivil.Items.Add(New ListItem("VIUDO(A)", "2"))
        Me.cmbEstadoCivil.Items.Add(New ListItem("DIVORCIADO(A)", "3"))
        Me.cmbEstadoCivil.Items.Add(New ListItem("SOLTERO(A)", "4"))
        Me.cmbEstadoCivil.Items.Add(New ListItem("UNIÓN LIBRE", "5"))

        Me.cmbNacionalidad.Items.Add(New ListItem("MEXICANA", "1"))
        Me.cmbNacionalidad.Items.Add(New ListItem("EXTRANJERA", "2"))

        Me.cmbParentesco1RefPer.Items.Add(New ListItem("FAMILIAR", "1"))
        Me.cmbParentesco1RefPer.Items.Add(New ListItem("AMISTADES", "2"))
        Me.cmbParentesco2RefPer.Items.Add(New ListItem("FAMILIAR", "1"))
        Me.cmbParentesco2RefPer.Items.Add(New ListItem("AMISTADES", "2"))
        Me.cmbParentesco3RefPer.Items.Add(New ListItem("FAMILIAR", "1"))
        Me.cmbParentesco3RefPer.Items.Add(New ListItem("AMISTADES", "2"))

        Dim _ObjBanco As New clsBancos
        _ObjDs = _ObjBanco.ManejaBanco(1)
        Me.cmbBanco1RefBancaria.DataValueField = "Valor"
        Me.cmbBanco1RefBancaria.DataTextField = "Texto"
        Me.cmbBanco1RefBancaria.DataSource = _ObjDs.Tables(0)
        Me.cmbBanco1RefBancaria.DataBind()

        Me.cmbBancoCarDir.DataValueField = "Valor"
        Me.cmbBancoCarDir.DataTextField = "Texto"
        Me.cmbBancoCarDir.DataSource = _ObjDs.Tables(0)
        Me.cmbBancoCarDir.DataBind()

        Try
            If cmbBancoCarDir.SelectedValue = 8 Then
                lblcardir.Text = "Cuenta Bancaria"
            Else
                lblcardir.Text = "CLABE"
            End If
        Catch ex As Exception

        End Try

        Dim i As Integer = 0
        For i = 1 To 40
            Me.cmbAnioRecDomCliente.Items.Add(New ListItem(i, i))
            Me.cmbAnioRecCiudadCliente.Items.Add(New ListItem(i, i))
            Me.cmbDesdeAnioCoaEmp.Items.Add(New ListItem(i, i))
            Me.cmbDesdeAnioCliEmpleo.Items.Add(New ListItem(i, i))
        Next


        Dim _ObjEstado As New clsEntidadFederativa
        _ObjDs = _ObjEstado.ManejaEntidad(1)

        If _ObjDs IsNot Nothing OrElse _ObjDs.Tables.Count > 0 OrElse _ObjDs.Tables(0).Rows.Count > 0 Then

            cmbEstadoNac.DataValueField = "EFD_CL_CVE"
            cmbEstadoNac.DataTextField = "EFD_DS_ENTIDAD"
            cmbEstadoNac.DataSource = _ObjDs.Tables(0)
            cmbEstadoNac.DataBind()

        End If

        Dim _ObjPais As New clsPais
        _ObjPais._intPAI_FL_CVE = 1
        _ObjDs = _ObjPais.ManejaPais(1)

        If _ObjDs IsNot Nothing OrElse _ObjDs.Tables.Count > 0 OrElse _ObjDs.Tables(0).Rows.Count > 0 Then

            cmbPaisNac.DataValueField = "PAI_FL_CVE"
            cmbPaisNac.DataTextField = "PAI_DS_NOMBRE_ESPANOL"
            cmbPaisNac.DataSource = _ObjDs.Tables(0)
            cmbPaisNac.DataBind()

        End If
    End Sub

    Private Sub ObtenSolicitud()
        Try
            Dim _ObjSn As New clsSolicitud()
            Dim _ObjDS As New DataSet

            _ObjSn._intCveCotizacion = _CveCotizacion

            _ObjDS = _ObjSn.ObtenRelacionCotSol()

            If _ObjDS Is Nothing OrElse _ObjDS.Tables.Count = 0 OrElse _ObjDS.Tables(0).Rows.Count = 0 Then
                txtNumSolicitud.Text = 0
                cmdGuarda.Enabled = True
            Else
                txtNumSolicitud.Text = _ObjDS.Tables(0).Rows(0).Item("SOLICITUD")
                txtPropuesta.Text = _ObjDS.Tables(0).Rows(0).Item("PROPUESTA")
                cmdGuarda.Enabled = False
                txtNumSolicitud.Enabled = False
                LlenaDatos()
            End If

        Catch ex As Exception
            MensajeError(ex.Message)
            cmdGuarda.Enabled = False
        End Try
    End Sub

    Private Sub LlenaDatos()
        Try
            Dim _ObjSn As New clsSolicitud

            _ObjSn._intClaveSolicitud = txtNumSolicitud.Text

            _ObjSn.ManejaSolicitud(1)

            If _ObjSn._intClaveSolicitud = 0 Then
                Exit Sub
            Else

                With _ObjSn
                    'Datos generales del solicitante
                    txt1Nombre.Text = _ObjSn._strNombre1
                    txt2Nombre.Text = _ObjSn._strNombre2
                    txtApePatCliente.Text = _ObjSn._strApePaterno
                    txtApeMatCliente.Text = _ObjSn._strApeMaterno
                    txtRFCCliente.Text = _ObjSn._strRFC
                    txtLada.Text = _ObjSn._stLada
                    txtTelCliente.Text = _ObjSn._stTelefono
                    txtTelMovilCliente.Text = _ObjSn._strTelMovil
                    txtCalleCliente.Text = _ObjSn._strCalle
                    txtNumExtCliente.Text = _ObjSn._strNumExt
                    txtNumIntCliente.Text = _ObjSn._strNumInt
                    txtEmailCliente.Text = _ObjSn._strEmail
                    cmbColoniaCliente.SelectedValue = _ObjSn._strColonia
                    cmbCiudadCliente.SelectedValue = _ObjSn._strCiudad
                    cmbEstado.SelectedValue = _ObjSn._strEstado
                    txtCodPostCliente.Text = _ObjSn._strCodPost

                    CargaUbicacion(_ObjSn._strCodPost, 1)
                    If strErr <> "" Then
                        MensajeError(strErr)
                        Exit Sub
                    End If

                    txtCURPCliente.Text = _ObjSn._strCURP
                    ''txtFirma.Text = _ObjSn._strFirmaElec

                    'Datos personales
                    cmbSexo.SelectedValue = _ObjSn._strSexo
                    txtFecNacCliente.Text = IIf(_ObjSn._fecNacimiento = "1900-01-01", "", _ObjSn._fecNacimiento)
                    cmbPaisNac.SelectedValue = _ObjSn._strPaisNac
                    cmbEstadoNac.SelectedValue = _ObjSn._strEstadoNac
                    cmbNacionalidad.SelectedValue = _ObjSn._intNacionalidad
                    cmbVive.SelectedItem.Text = _ObjSn._strViven
                    cmbPropiedad.SelectedValue = _ObjSn._intPropiedad
                    cmbAnioRecDomCliente.SelectedValue = _ObjSn._intAnioRecDomicilio.ToString
                    cmbAnioRecCiudadCliente.SelectedValue = _ObjSn._intAnioRecCiudad.ToString
                    txtBeneficiario1Cliente.Text = _ObjSn._strBeneficiario1
                    cmbEstadoCivil.SelectedValue = _ObjSn._intEdoCivil


                    'Empleos del solicitante
                    txtPuestoCliEmpleo.Text = _ObjSn._strEmpPuesto
                    txtDepAreaCliEmpleo.Text = _ObjSn._strEmpDepArea
                    cmbDesdeAnioCliEmpleo.SelectedValue = _ObjSn._fecEmpDesdeAnio
                    txtCompaniaCliEmpleo.Text = _ObjSn._strCompania
                    txtLadaEmpleo.Text = _ObjSn._strEmpLada
                    txtTelCliEmpleo.Text = _ObjSn._strEmpTelefono
                    txtTelExtCliEmpleo.Text = _ObjSn._strEmpExt
                    txtSueMenCliEmpleo.Text = _ObjSn._strEmpSueldo.ToString
                    txtCalleCliEmpleo.Text = _ObjSn._strEmpCalle
                    txtNumExtCliEmpleo.Text = _ObjSn._strEmpNumExt
                    txtNumIntCliEmpleo.Text = _ObjSn._strEmpInt
                    txtTelExtCliEmpleo.Text = _ObjSn._strEmpExt

                    'Referencias Personales
                    txtNombre1RefPer.Text = _ObjSn._strRPNombre1
                    cmbParentesco1RefPer.SelectedValue = _ObjSn._strRPParentesco1
                    txtTel1RefPer.Text = _ObjSn._strRPTelfono1
                    txtEmail1RefPer.Text = _ObjSn._strRPEmail1
                    txtNombre2RefPer.Text = _ObjSn._strRPNombre2
                    cmbParentesco2RefPer.SelectedValue = _ObjSn._strRPParentesco2
                    txtTel2RefPer.Text = _ObjSn._strRPTelfono2
                    txtEmail2RefPer.Text = _ObjSn._strRPEmail2
                    txtNombre3RefPer.Text = _ObjSn._strRPNombre3
                    cmbParentesco3RefPer.SelectedValue = _ObjSn._strRPParentesco3
                    txtTel3RefPer.Text = _ObjSn._strRPTelfono3
                    txtEmail3RefPer.Text = _ObjSn._strRPEmail3

                    'Referencias Bancarias
                    cmbBanco1RefBancaria.SelectedValue = _ObjSn._strRBBanco1
                    txtTar1RefBancaria.Text = _ObjSn._strRBTarjeta1

                    'Cargo(Directo)
                    cmbBancoCarDir.SelectedValue = _ObjSn._strCDBanco
                    txtCLABECarDir.Text = _ObjSn._strCDCLABE


                    Try
                        If cmbBancoCarDir.SelectedValue = 8 Then
                            lblcardir.Text = "Cuenta Bancaria"
                        Else
                            lblcardir.Text = "CLABE"
                        End If
                    Catch ex As Exception

                    End Try

                    'Coacreditado datos personales
                    txtNombre1Coa.Text = _ObjSn._strCoaNombre1
                    txtNombre2Coa.Text = _ObjSn._strCoaNombre2
                    txtApePatCoa.Text = _ObjSn._strCoaApePaterno
                    txtApeMatCoa.Text = _ObjSn._strCoaApeMaterno
                    txtRFCCoa.Text = _ObjSn._strCoaRFC
                    txtLadaCoa.Text = _ObjSn._strCoaLada
                    txtTelCoa.Text = _ObjSn._strCoaTelefono
                    txtMovilCoa.Text = _ObjSn._strCoaTelMovil
                    txtFecNactCoa.Text = _ObjSn._fecCoaNacimiento
                    txtCalleCoa.Text = _ObjSn._strCoaCalle
                    txtNumExtCoa.Text = _ObjSn._strCoaNumExt
                    txtNumInttCoa.Text = _ObjSn._strCoaNumInt
                    txtNumIntCoaEmp.Text = _ObjSn._strCoaEmpNumInt ''_ObjSn._strCoaNumInt
                    txtEmailCoa.Text = _ObjSn._strCoaEmail
                    cmbColoniaCoa.SelectedValue = _ObjSn._strCoaColonia
                    cmbCiudadCoa.SelectedValue = _ObjSn._strCoaCiudad
                    cmbEstadoCoa.SelectedValue = _ObjSn._strCoaEstado
                    txtCodPostCoa.Text = _ObjSn._strCoaCodPost

                    CargaUbicacion(_ObjSn._strCoaCodPost, 2)
                    If strErr <> "" Then
                        MensajeError(strErr)
                        Exit Sub
                    End If

                    ''txtFirmaCoa.Text = _ObjSn._strCoaFirmaElec

                    'Empleo del coacreditado
                    txtPuestoCoaEmp.Text = _ObjSn._strCoaEmpPuesto
                    txtDepAreaCoaEmp.Text = _ObjSn._strCoaEmpDepArea
                    cmbDesdeAnioCoaEmp.SelectedValue = _ObjSn._strCoaEmpDesAnio
                    txtCompaniaCoaEmp.Text = _ObjSn._strCoaEmpCompania
                    txtLadaCoaEmp.Text = _ObjSn._strCoaEmpLada
                    txtTelCoaEmp.Text = _ObjSn._strCoaEmpTelefono
                    txtExtCoaEmp.Text = _ObjSn._strCoaEmpExt
                    txtSueldoCoaEmp.Text = _ObjSn._decCoaEmpSueldo.ToString
                    txtCalleCoaEmp.Text = _ObjSn._strCoaEmpCalle
                    txtNumExtCoaEmp.Text = _ObjSn._strCoaEmpNumExt
                    txtNumIntCoaEmp.Text = _ObjSn._strCoaEmpNumInt

                    'Datos de la realcion de la cotizacion y la solicitud
                    _CveSolicitud = _ObjSn._intCvePropuesta
                    _CveCotizacion = _ObjSn._intCveCotizacion
                    _CveCotSeguro = _ObjSn._intCveCotSeguro

                    'Delecacion Municipio Solicitud
                    cmbDelMunCliente.SelectedValue = _ObjSn._intDelMun
                    cmbDelMunCoa.SelectedValue = _ObjSn._intCoaDelMun

                End With
            End If

        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Protected Sub txtCodPostCliente_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodPostCliente.TextChanged

        strErr = String.Empty

        If Len(txtCodPostCliente.Text) < 5 Then
            strErr = "El Código Postal debe tener 5 carácteres."
            MensajeError(strErr)
            txtCodPostCliente.Text = ""
            txtCodPostCliente.Focus()
            cmbEstado.Items.Clear()
            cmbCiudadCliente.Items.Clear()
            cmbDelMunCliente.Items.Clear()
            cmbColoniaCliente.Items.Clear()
            txtCalleCliente.Text = ""
            txtNumExtCliente.Text = ""
            txtNumIntCliente.Text = ""
            Exit Sub
        End If

        CargaUbicacion(txtCodPostCliente.Text.Trim, 1)
        If strErr <> "" Then
            MensajeError(strErr)
            Exit Sub
        End If

    End Sub

    Protected Sub txtBeneficiario1Cliente_TextChanged(sender As Object, e As System.EventArgs) Handles txtBeneficiario1Cliente.TextChanged
        Dim objSN As New clsPersonas
        Dim strMensaje As String = ""
        Try
            'BUG-PC-22 MAUT Se agrega segundo nombre
            txtRFCCliente.Text = objSN.CalculaRFC(txtApePatCliente.Text, txtApeMatCliente.Text, txt1Nombre.Text, txtFecNacCliente.Text, strMensaje, txt2Nombre.Text)
            If Len(Trim$(strMensaje)) > 0 Then
                MensajeError(strMensaje)
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtCodPostCoa_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodPostCoa.TextChanged

        If Len(txtCodPostCoa.Text) < 5 Then
            strErr = "El Código Postal debe tener 5 carácteres."
            MensajeError(strErr)
            txtCodPostCoa.Text = ""
            txtCodPostCoa.Focus()
            cmbEstadoCoa.Items.Clear()
            cmbCiudadCoa.Items.Clear()
            cmbDelMunCoa.Items.Clear()
            cmbColoniaCoa.Items.Clear()
            txtCalleCoa.Text = ""
            txtNumExtCoa.Text = ""
            txtNumInttCoa.Text = ""
            Exit Sub
        End If

        CargaUbicacion(txtCodPostCoa.Text.Trim, 2)
        If strErr <> "" Then
            MensajeError(strErr)
            Exit Sub
        End If

    End Sub

    Protected Sub cmbColoniaCliente_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbColoniaCliente.SelectedIndexChanged
        txtCalleCliente.Focus()
    End Sub

    Protected Sub cmbColoniaCoa_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbColoniaCoa.SelectedIndexChanged
        txtCalleCoa.Focus()
    End Sub

    Protected Sub cmbPaisNac_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbPaisNac.SelectedIndexChanged
        Dim _ObjDs As New DataSet
        Dim _ObjEdoNac As New clsEntidadFederativa
        _ObjEdoNac._intPAI_FL_CVE = cmbPaisNac.SelectedValue
        _ObjDs = _ObjEdoNac.ManejaEntidad(1)

        If _ObjDs IsNot Nothing OrElse _ObjDs.Tables.Count > 0 OrElse _ObjDs.Tables(0).Rows.Count > 0 Then

            cmbEstadoNac.DataValueField = "EFD_CL_CVE"
            cmbEstadoNac.DataTextField = "EFD_DS_ENTIDAD"
            cmbEstadoNac.DataSource = _ObjDs.Tables(0)
            cmbEstadoNac.DataBind()
        End If
    End Sub

    Protected Sub cmbBancoCarDir_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbBancoCarDir.SelectedIndexChanged
        Try
            If cmbBancoCarDir.SelectedValue = 8 Then
                lblcardir.Text = "Cuenta Bancaria"
            Else
                lblcardir.Text = "CLABE"
            End If
        Catch ex As Exception

        End Try
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

                        cmbEstado.DataValueField = "EFD_CL_CVE"
                        cmbEstado.DataTextField = "EFD_DS_ENTIDAD"
                        cmbEstado.DataSource = _ObjDs.Tables(0)
                        cmbEstado.DataBind()

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
                        txtCodPostCliente.Text = ""
                        txtCodPostCliente.Focus()
                        cmbEstado.Items.Clear()
                        cmbCiudadCliente.Items.Clear()
                        cmbDelMunCliente.Items.Clear()
                        cmbColoniaCliente.Items.Clear()
                        txtCalleCliente.Text = ""
                        txtNumExtCliente.Text = ""
                        txtNumIntCliente.Text = ""
                        Exit Function
                    End If

                    _ObjCol._strCPO_CL_CODPOSTAL = txtCodPostCliente.Text
                    _ObjColds = _ObjCodPost.ManejaCotPost(7)

                    If _ObjColds.Tables(0).Rows.Count > 0 Then
                        cmbColoniaCliente.DataSource = _ObjColds.Tables(0)
                        cmbColoniaCliente.DataValueField = "CPO_FL_CP"
                        cmbColoniaCliente.DataTextField = "CPO_DS_COLONIA"
                        cmbColoniaCliente.DataBind()
                    Else
                        strErr = "No se pudo cargar la información de las Colonias."
                        Exit Function
                    End If

                Case 2 ''Coacreditado
                    If _ObjDs.Tables(0).Rows.Count > 0 Then

                        cmbEstadoCoa.DataValueField = "EFD_CL_CVE"
                        cmbEstadoCoa.DataTextField = "EFD_DS_ENTIDAD"
                        cmbEstadoCoa.DataSource = _ObjDs.Tables(0)
                        cmbEstadoCoa.DataBind()

                        cmbCiudadCoa.DataValueField = "CIU_CL_CIUDAD"
                        cmbCiudadCoa.DataTextField = "CIU_NB_CIUDAD"
                        cmbCiudadCoa.DataSource = _ObjDs.Tables(0)
                        cmbCiudadCoa.DataBind()

                        cmbDelMunCoa.DataValueField = "MUN_CL_CVE"
                        cmbDelMunCoa.DataTextField = "MUN_DS_MUNICIPIO"
                        cmbDelMunCoa.DataSource = _ObjDs.Tables(0)
                        cmbDelMunCoa.DataBind()

                    Else
                        strErr = "El Código Postal no existe."
                        MensajeError(strErr)
                        txtCodPostCoa.Text = ""
                        txtCodPostCoa.Focus()
                        cmbEstadoCoa.Items.Clear()
                        cmbCiudadCoa.Items.Clear()
                        cmbDelMunCoa.Items.Clear()
                        cmbColoniaCoa.Items.Clear()
                        txtCalleCoa.Text = ""
                        txtNumExtCoa.Text = ""
                        txtNumInttCoa.Text = ""
                        Exit Function

                    End If

                    _ObjCol._strCPO_CL_CODPOSTAL = txtCodPostCoa.Text
                    _ObjColds = _ObjCodPost.ManejaCotPost(7)

                    If _ObjColds.Tables(0).Rows.Count > 0 Then
                        cmbColoniaCoa.DataSource = _ObjColds.Tables(0)
                        cmbColoniaCoa.DataValueField = "CPO_FL_CP"
                        cmbColoniaCoa.DataTextField = "CPO_DS_COLONIA"
                        cmbColoniaCoa.DataBind()
                    Else
                        strErr = "No se pudo cargar la información de las Colonias."
                        MensajeError(strErr)
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

    Protected Sub cmdGuarda_Click(sender As Object, e As System.EventArgs) Handles cmdGuarda.Click
        Try

            Dim _ObjSn As New clsSolicitud
            Dim objSn As New clsPersonas
            Dim _ObjDs As New DataSet
            Dim strMensaje As String = ""
            cmdImprime.Enabled = True

            'Datos generales del solicitante
            _ObjSn._strNombre1 = txt1Nombre.Text
            _ObjSn._strNombre2 = txt2Nombre.Text
            _ObjSn._strApePaterno = txtApePatCliente.Text
            _ObjSn._strApeMaterno = txtApeMatCliente.Text
            'BUG-PC-22 MAUT Se agrega segundo nombre
            _ObjSn._strRFC = objSn.CalculaRFC(txtApePatCliente.Text, txtApeMatCliente.Text, txt1Nombre.Text, txtFecNacCliente.Text, strMensaje, txt2Nombre.Text)
            _ObjSn._stLada = txtLada.Text
            _ObjSn._stTelefono = txtTelCliente.Text
            _ObjSn._strTelMovil = txtTelMovilCliente.Text
            _ObjSn._strCalle = txtCalleCliente.Text
            _ObjSn._strNumExt = txtNumExtCliente.Text
            _ObjSn._strNumInt = txtNumIntCliente.Text
            _ObjSn._strEmail = txtEmailCliente.Text
            _ObjSn._strColonia = cmbColoniaCliente.SelectedValue
            _ObjSn._strCiudad = cmbCiudadCliente.SelectedValue
            _ObjSn._strEstado = cmbEstado.SelectedValue
            _ObjSn._strCodPost = txtCodPostCliente.Text
            _ObjSn._strCURP = txtCURPCliente.Text
            ''_ObjSn._strFirmaElec = txtFirma.Text

            If txt1Nombre.Text.Length = 0 OrElse txtApePatCliente.Text.Length = 0 OrElse txtApeMatCliente.Text.Length = 0 OrElse txtRFCCliente.Text.Length = 0 OrElse txtLada.Text.Length = 0 OrElse txtTelCliente.Text.Length = 0 OrElse txtTelMovilCliente.Text.Length = 0 OrElse txtCalleCliente.Text.Length = 0 OrElse txtNumExtCliente.Text.Length = 0 OrElse txtNumIntCliente.Text.Length = 0 OrElse txtEmailCliente.Text.Length = 0 OrElse cmbColoniaCliente.Items.Count = 0 OrElse cmbCiudadCliente.Items.Count = 0 OrElse cmbEstado.Items.Count = 0 OrElse txtCodPostCliente.Text = "" Then
                MensajeError("Los campos de la sección datos del solicitante son requeridos.")
                txt1Nombre.Focus()
                Exit Sub
            End If

            'Datos Personales
            _ObjSn._strSexo = cmbSexo.SelectedValue
            _ObjSn._fecNacimiento = IIf(txtFecNacCliente.Text.Length = 0, "1900-01-01", txtFecNacCliente.Text)
            _ObjSn._strPaisNac = cmbPaisNac.SelectedValue
            _ObjSn._strEstadoNac = cmbEstadoNac.SelectedValue
            _ObjSn._intNacionalidad = cmbNacionalidad.SelectedValue
            _ObjSn._strViven = cmbVive.SelectedItem.Text
            _ObjSn._intPropiedad = cmbPropiedad.SelectedValue
            _ObjSn._intAnioRecDomicilio = cmbAnioRecDomCliente.SelectedValue
            _ObjSn._intAnioRecCiudad = cmbAnioRecCiudadCliente.SelectedValue
            _ObjSn._strBeneficiario1 = txtBeneficiario1Cliente.Text
            _ObjSn._intEdoCivil = cmbEstadoCivil.SelectedValue

            If txtFecNacCliente.Text.Length = 0 OrElse cmbAnioRecDomCliente.SelectedValue = 0 OrElse cmbAnioRecCiudadCliente.SelectedValue = 0 OrElse txtBeneficiario1Cliente.Text.Length = 0 Then
                MensajeError("Los campos de la sección datos personales son requeridos.")
                cmbSexo.Focus()
                Exit Sub
            End If


            'Empleos del solicitante
            _ObjSn._strEmpPuesto = txtPuestoCliEmpleo.Text
            _ObjSn._strEmpDepArea = txtDepAreaCliEmpleo.Text
            _ObjSn._fecEmpDesdeAnio = cmbDesdeAnioCliEmpleo.SelectedValue
            _ObjSn._strCompania = txtCompaniaCliEmpleo.Text
            _ObjSn._strEmpLada = txtLadaEmpleo.Text
            _ObjSn._strEmpTelefono = txtTelCliEmpleo.Text
            _ObjSn._strEmpExt = txtTelExtCliEmpleo.Text
            _ObjSn._strEmpSueldo = IIf(txtSueMenCliEmpleo.Text.Trim = "", 0.0, txtSueMenCliEmpleo.Text)
            _ObjSn._strEmpCalle = txtCalleCliEmpleo.Text
            _ObjSn._strEmpNumExt = txtNumExtCliEmpleo.Text
            _ObjSn._strEmpInt = txtNumIntCliEmpleo.Text
            _ObjSn._strEmpExt = txtTelExtCliEmpleo.Text

            If txtPuestoCliEmpleo.Text.Trim.Length = 0 OrElse txtDepAreaCliEmpleo.Text.Trim.Length = 0 OrElse cmbDesdeAnioCliEmpleo.SelectedValue = 0 OrElse txtCompaniaCliEmpleo.Text.Trim.Length = 0 OrElse txtLadaEmpleo.Text.Trim.Length = 0 OrElse txtTelCliEmpleo.Text.Trim.Length = 0 OrElse txtTelExtCliEmpleo.Text.Trim.Length = 0 OrElse txtSueMenCliEmpleo.Text.Trim.Length = 0 OrElse txtCalleCliEmpleo.Text.Trim.Length = 0 OrElse txtNumExtCliEmpleo.Text.Trim.Length = 0 OrElse txtNumIntCliEmpleo.Text.Trim.Length = 0 OrElse txtTelExtCliEmpleo.Text.Trim.Length = 0 Then
                MensajeError("Los campos de la sección empleo del solicitante son requeridos.")
                txtPuestoCliEmpleo.Focus()
                Exit Sub
            End If

            'Referencias Personales
            _ObjSn._strRPNombre1 = txtNombre1RefPer.Text
            _ObjSn._strRPParentesco1 = cmbParentesco1RefPer.SelectedValue
            _ObjSn._strRPTelfono1 = txtTel1RefPer.Text
            _ObjSn._strRPEmail1 = txtEmail1RefPer.Text
            _ObjSn._strRPNombre2 = txtNombre2RefPer.Text
            _ObjSn._strRPParentesco2 = cmbParentesco2RefPer.SelectedValue
            _ObjSn._strRPTelfono2 = txtTel2RefPer.Text
            _ObjSn._strRPEmail2 = txtEmail2RefPer.Text
            _ObjSn._strRPNombre3 = txtNombre3RefPer.Text
            _ObjSn._strRPParentesco3 = cmbParentesco3RefPer.SelectedValue
            _ObjSn._strRPEmail3 = txtEmail3RefPer.Text
            _ObjSn._strRPTelfono3 = txtTel3RefPer.Text


            If txtEmail1RefPer.Text.Trim.Length = 0 OrElse txtEmail2RefPer.Text.Trim.Length = 0 OrElse txtEmail3RefPer.Text.Trim.Length = 0 OrElse txtNombre1RefPer.Text.Trim.Length = 0 OrElse cmbParentesco1RefPer.SelectedValue = 0 OrElse txtTel1RefPer.Text.Trim.Length = 0 OrElse txtNombre2RefPer.Text.Trim.Length = 0 OrElse cmbParentesco2RefPer.SelectedValue = 0 OrElse txtTel2RefPer.Text.Trim.Length = 0 OrElse txtNombre3RefPer.Text.Trim.Length = 0 OrElse cmbParentesco3RefPer.SelectedValue = 0 OrElse txtTel3RefPer.Text.Trim.Length = 0 Then
                MensajeError("Los campos de la sección referencias personales son requeridos.")
                txtNombre1RefPer.Focus()
                Exit Sub
            End If


            'Referencias Bancarias
            _ObjSn._strRBBanco1 = cmbBanco1RefBancaria.SelectedValue
            _ObjSn._strRBTarjeta1 = txtTar1RefBancaria.Text

            If txtTar1RefBancaria.Text.Trim.Length = 0 Then
                MensajeError("Los campos de la sección referencia bancaria son requeridos.")
                txtTar1RefBancaria.Focus()
                Exit Sub
            End If

            'Cargo Directo
            _ObjSn._strCDBanco = cmbBancoCarDir.SelectedValue
            _ObjSn._strCDCLABE = txtCLABECarDir.Text

            If txtCLABECarDir.Text.Trim.Length = 0 Then
                MensajeError("Los campos de la sección cargo directo son requeridos.")
                txtCLABECarDir.Focus()
                Exit Sub
            End If

            'Coacreditado datos personales
            _ObjSn._strCoaNombre1 = txtNombre1Coa.Text
            _ObjSn._strCoaNombre2 = txtNombre2Coa.Text
            _ObjSn._strCoaApePaterno = txtApePatCoa.Text
            _ObjSn._strCoaApeMaterno = txtApeMatCoa.Text
            If Trim(txtNombre1Coa.Text) <> "" And Trim(txtApePatCoa.Text) <> "" And Trim(txtApeMatCoa.Text) <> "" And txtFecNactCoa.Text <> "" Then
                'BUG-PC-22 MAUT Se agrega segundo nombre
                _ObjSn._strCoaRFC = objSn.CalculaRFC(txtApePatCoa.Text, txtApeMatCoa.Text, txtNombre1Coa.Text, txtFecNactCoa.Text, strMensaje, txtNombre2Coa.Text)
            Else
                _ObjSn._strCoaRFC = txtRFCCoa.Text
            End If
            _ObjSn._strCoaLada = txtLadaCoa.Text
            _ObjSn._strCoaTelefono = txtTelCoa.Text
            _ObjSn._strCoaTelMovil = txtMovilCoa.Text
            _ObjSn._fecCoaNacimiento = IIf(txtFecNactCoa.Text.Length = 0, "1900-01-01", txtFecNactCoa.Text)
            _ObjSn._strCoaCalle = txtCalleCoa.Text
            _ObjSn._strCoaNumExt = txtNumExtCoa.Text
            _ObjSn._strCoaNumInt = txtNumIntCoaEmp.Text
            _ObjSn._strCoaEmail = txtEmailCoa.Text
            _ObjSn._strCoaColonia = cmbColoniaCoa.SelectedValue
            _ObjSn._strCoaCiudad = cmbCiudadCoa.SelectedValue
            _ObjSn._strCoaEstado = cmbEstadoCoa.SelectedValue
            _ObjSn._strCoaCodPost = txtCodPostCoa.Text
            If txtCodPostCoa.Text = "" Then
                _ObjSn._strCoaEstado = ""
                _ObjSn._strCoaCiudad = ""
                _ObjSn._strCoaColonia = ""
                _ObjSn._strCoaCalle = ""
                _ObjSn._strCoaNumExt = ""
                _ObjSn._strCoaNumInt = ""
            End If


            'Empleo del coacreditado
            _ObjSn._strCoaEmpPuesto = txtPuestoCoaEmp.Text
            _ObjSn._strCoaEmpDepArea = txtDepAreaCoaEmp.Text
            _ObjSn._strCoaEmpDesAnio = cmbDesdeAnioCoaEmp.SelectedValue
            _ObjSn._strCoaEmpCompania = txtCompaniaCoaEmp.Text
            _ObjSn._strCoaEmpLada = txtLadaCoaEmp.Text
            _ObjSn._strCoaEmpTelefono = txtTelCoaEmp.Text
            _ObjSn._strCoaEmpExt = txtExtCoaEmp.Text
            _ObjSn._decCoaEmpSueldo = IIf(txtSueldoCoaEmp.Text.Trim = "", 0.0, txtSueldoCoaEmp.Text.Trim)
            _ObjSn._strCoaEmpCalle = txtCalleCoaEmp.Text
            _ObjSn._strCoaEmpNumExt = txtNumExtCoaEmp.Text
            _ObjSn._strCoaEmpNumInt = txtNumIntCoaEmp.Text

            'Datos de la realcion de la cotizacion y la solicitud
            _ObjSn._intCvePropuesta = _CveSolicitud
            _ObjSn._intCveCotizacion = _CveCotizacion
            _ObjSn._intCveCotSeguro = _CveCotSeguro

            'Datos delegacion solicitud
            _ObjSn._intDelMun = cmbDelMunCliente.SelectedValue
            _ObjSn._intCoaDelMun = IIf(cmbDelMunCoa.SelectedValue = "", Nothing, cmbDelMunCoa.SelectedValue)

            'Guarda Solicitud
            _ObjSn.ManejaSolicitud(2)

            _ObjDs = _ObjSn.ObtenRelacionCotSol()
            _ObjSn._intClaveSolicitud = _ObjDs.Tables(0).Rows(0).Item("SOLICITUD")
            _CveSolicitud = _ObjSn._intClaveSolicitud
            ObtenSolicitud()

            MensajeError("Registro guardado con éxito")
            Exit Sub
        Catch ex As Exception
            MensajeError(ex.Message.Replace("''", "").Replace("\", ""))
        End Try
    End Sub

    Protected Sub cmdImprime_Click(sender As Object, e As System.EventArgs) Handles cmdImprime.Click

        Try
            Dim objSol As New clsSolicitud
            Dim dts As New DataSet
            Dim crReportDocument As ReportDocument
            Dim crExportOptions As ExportOptions
            Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

            Dim strFile As String = "Sol" & txtNumSolicitud.Text & ".pdf"
            Dim fileNameExtension As String = ""
            fileNameExtension = Server.MapPath(strFile)
            fileNameExtension = Replace(fileNameExtension, "\aspx\", "\Sols\")


            dts = objSol.ImprimeSolicitud(1, txtNumSolicitud.Text, strErr)

            If strErr = "" Then
                If dts.Tables(0).Rows.Count > 0 Then

                    dts.Tables(0).TableName = "DatosSolicitud"
                    crReportDocument = New ReportDocument
                    crReportDocument.Load(Server.MapPath("../Reportes/rpt_Solicitud.rpt"))
                    crDiskFileDestinationOptions = New DiskFileDestinationOptions
                    crDiskFileDestinationOptions.DiskFileName = fileNameExtension
                    crExportOptions = crReportDocument.ExportOptions
                    With crExportOptions
                        .DestinationOptions = crDiskFileDestinationOptions
                        .ExportDestinationType = ExportDestinationType.DiskFile
                        .ExportFormatType = ExportFormatType.PortableDocFormat
                    End With
                    crReportDocument.SetDataSource(dts)
                    crReportDocument.Export()

                Else
                    MensajeError("No se pudo recuperar información de la solicitud.")
                    Exit Sub
                End If
            Else
                MensajeError(strErr)
                Exit Sub
            End If

        Catch ex As Exception
            MensajeError(ex.Message)
            Exit Sub
        End Try
    End Sub

    Protected Sub txtFecNactCoa_TextChanged(sender As Object, e As System.EventArgs) Handles txtFecNactCoa.TextChanged
        Dim objSN As New clsPersonas
        Dim strMensaje As String = ""
        Try
            'BUG-PC-22 MAUT Se agrega segundo nombre
            txtRFCCoa.Text = objSN.CalculaRFC(txtApePatCoa.Text, txtApeMatCoa.Text, txtNombre1Coa.Text, txtFecNactCoa.Text, strMensaje, txtNombre2Coa.Text)
            If Len(Trim$(strMensaje)) > 0 Then
                MensajeError(strMensaje)
                Exit Sub
            End If
        Catch ex As Exception

        End Try

    End Sub
End Class
