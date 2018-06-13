'RQ-PI7-PC6: CGARCIA: 19/10/2017: SE CREA PANTALLA PARA CREAR, ACTUALIZAR DOMICILIOS
'BUG-PC-161: CGARCIA: 27/02/2018: SE AGREGA FILTRO DE BUSQUEDA OPCIONAL EN CIUDAD
'BUG-PC-198: CGARCIA: 23/05/2018: SE VALIDA DUPLICADO DE ID DE MUNICIPIOS Y FILTRO DE CUIDADES DEJA DE DEPENDER DE MUNICIPIO
Imports System.Data
Imports SNProcotiza

Partial Class aspx_manejaCodigoPostal
    Inherits System.Web.UI.Page

    Dim clsCodigoPostal As New clsCodigoPostal
    Dim clsEstados As New clsEstados
    Dim clsMunicipios As New clsMunicipio
    Dim clsCiudad As New clsCiudad
    Dim strErr As String = ""
    Dim dsConsultaCP As New DataSet
    Dim IndexGrv As Integer = 0
    Dim IndexSecundario As Integer = 0
    Dim Num_ID(0) As Integer
    Dim Name_Col(0) As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()

        If Not IsPostBack Then
            CargaCombos(4)
        End If

    End Sub

    Protected Sub grvMuestraExistente_RowCreated(sender As Object, e As GridViewRowEventArgs)

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

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        divMostrarExistente.Visible = True
        divControlesInsert.Visible = False
        clsCodigoPostal._strCPO_CL_CODPOSTAL = CStr(txtCP.Text.ToString.Trim)
        dsConsultaCP = clsCodigoPostal.ManejaCotPost(8)

        If (Not IsNothing(dsConsultaCP) AndAlso dsConsultaCP.Tables.Count > 0 AndAlso
            dsConsultaCP.Tables(0).Rows.Count > 0) Then
            If (dsConsultaCP.Tables(0).Rows(0).Item("RESULT") = 1) Then
                divMostrarExistente.Visible = True
                divControlesInsert.Visible = False
                grvMuestraExistente.DataSource = dsConsultaCP.Tables(1)
                grvMuestraExistente.DataBind()

            ElseIf (dsConsultaCP.Tables(0).Rows(0).Item("RESULT") = 0) Then
                divMostrarExistente.Visible = False
                divControlesInsert.Visible = False
                MensajeError("No existe el Código Postal")
            End If
        Else
            MensajeError("No existe información.")
        End If

    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As System.EventArgs) Handles btnAgregar.Click
        cmbCiudad.ClearSelection()
        CargaCombos(1)
        If (divMostrarExistente.Visible = True) Then
            LimpiaControles()
            divControlesInsert.Visible = True
            ViewState("vwsNuevoRegistro") = 0
        Else
            divMostrarExistente.Visible = False
            divControlesInsert.Visible = True
            ViewState("vwsNuevoRegistro") = 1
        End If
        txtNvoCP.Text = txtCP.Text
        btnGuardarNvo.Visible = True
        btnUpdate.Visible = False

    End Sub

    Protected Sub btnElimina_Click(sender As Object, e As EventArgs)

    End Sub

    Private Function CargaCombos(opc As Integer) As Boolean
        Dim dtsRes As New DataSet
        Dim objCombo As New clsProcGenerales
        Dim _idCiudad As Integer
        CargaCombos = False
        Try
            Select Case opc
                'combo entidad federativa
                Case 1
                    clsEstados.IDEstatus = 2
                    dtsRes = clsEstados.ManejaEstado(1)
                    If clsEstados.ErrorEstados = "" Then
                        If (Not IsNothing(dtsRes) AndAlso dtsRes.Tables.Count > 0 AndAlso dtsRes.Tables(0).Rows.Count > 0) Then
                            objCombo.LlenaCombos(dtsRes, "NOMBRE", "ID_ESTADO", cmbNvoEntidadFederativa, strErr, True)
                            If strErr <> "" Then
                                Exit Function
                            End If
                        End If
                    Else
                        strErr = clsEstados.ErrorEstados
                        Exit Function

                    End If
                    'combo ciudad
                Case 3
                    'BUG-PC-198:
                    '_idCiudad = fn_Ciudad()
                    clsCiudad._intEFD_CL_CVE = cmbNvoEntidadFederativa.SelectedValue
                    'clsCiudad._intCIU_CL_CIUDAD = _idCiudad
                    dtsRes = clsCiudad.ManejaCiudad(1)

                    If (Not IsNothing(dtsRes) AndAlso dtsRes.Tables.Count > 0 AndAlso dtsRes.Tables(0).Rows.Count > 0) Then
                        objCombo.LlenaCombos(dtsRes, "CIU_NB_CIUDAD", "CIU_CL_CIUDAD", cmbCiudad, strErr, True)
                        cmbCiudad.Items(0).Value = -1
                        If strErr <> "" Then
                            Exit Function
                        End If
                    End If
                    'combo municipio
                Case 2
                    clsMunicipios._intEFD_CL_CVE = cmbNvoEntidadFederativa.SelectedValue
                    'clsMunicipios._intCIU_CL_CIUDAD = cmbCiudad.SelectedValue
                    dtsRes = clsMunicipios.ManejaMunicipio(1)
                    ViewState.Add("vwsMunicipio", dtsRes)
                    If (Not IsNothing(dtsRes) AndAlso dtsRes.Tables.Count > 0 AndAlso dtsRes.Tables(0).Rows.Count > 0) Then
                        objCombo.LlenaCombos(dtsRes, "MUN_DS_MUNICIPIO", "MUN_CL_CVE", cmbNvoMunicipio, strErr, True)
                        If strErr <> "" Then
                            Exit Function
                        End If
                    End If
                    'combo estatus
                Case 4
                    dtsRes = objCombo.ObtenInfoParametros(1, strErr)
                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbNvoEstatus, strErr, True)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If


            End Select
            CargaCombos = True
        Catch ex As Exception
            CargaCombos = False
            MensajeError(ex.Message)
        End Try
    End Function

    Protected Sub btnGuardarNvo_Click(sender As Object, e As EventArgs) Handles btnGuardarNvo.Click

        txtNvoCP.Text = txtCP.Text.Trim
        clsCodigoPostal._strCPO_CL_CODPOSTAL = CStr(txtNvoCP.Text.ToString.Trim)
        dsConsultaCP = clsCodigoPostal.ManejaCotPost(8)
        If (Not IsNothing(dsConsultaCP) AndAlso dsConsultaCP.Tables.Count > 0 AndAlso
            dsConsultaCP.Tables(0).Rows.Count > 0) Then
            If (dsConsultaCP.Tables(0).Rows(0).Item("RESULT") = 1) Then
                'MensajeError("El código postal ya existe.")
                'Exit Sub
                clsCodigoPostal._intEFD_CL_CVE = cmbNvoEntidadFederativa.SelectedValue
                clsCodigoPostal._intMUN_CL_CVE = cmbNvoMunicipio.SelectedValue
                clsCodigoPostal._intCIU_CL_CIUDAD = cmbCiudad.SelectedValue
                clsCodigoPostal._strCPO_CL_CODPOSTAL = txtNvoCP.Text.Trim
                clsCodigoPostal._strCPO_DS_COLONIA = txtNvoColonia.Text.Trim
                clsCodigoPostal._intCPO_FG_STATUS = cmbNvoEstatus.SelectedValue
                clsCodigoPostal._strUSR_CL_CVE = "ADMIN"

                dsConsultaCP = clsCodigoPostal.ManejaCotPost(2)
                If (Not IsNothing(dsConsultaCP) AndAlso dsConsultaCP.Tables.Count > 0 AndAlso dsConsultaCP.Tables(0).Rows.Count > 0) Then
                    clsCodigoPostal._strCPO_CL_CODPOSTAL = CStr(txtNvoCP.Text.ToString.Trim)
                    dsConsultaCP = clsCodigoPostal.ManejaCotPost(8)
                    lblMensaje.Text += "<script>alert('El registro se guardó con éxito');</script>"
                    grvMuestraExistente.DataSource = dsConsultaCP.Tables(1)
                    grvMuestraExistente.DataBind()
                    divMostrarExistente.Visible = True
                    LimpiaControles()
                    divControlesInsert.Visible = False
                Else
                    MensajeError("Error al insertar registro.")
                End If
                'CierraPantalla("./manejaCodigoPostal.aspx")
            ElseIf (dsConsultaCP.Tables(0).Rows(0).Item("RESULT") = 0) Then
                clsCodigoPostal._intEFD_CL_CVE = cmbNvoEntidadFederativa.SelectedValue
                clsCodigoPostal._intMUN_CL_CVE = cmbNvoMunicipio.SelectedValue
                clsCodigoPostal._intCIU_CL_CIUDAD = cmbCiudad.SelectedValue
                clsCodigoPostal._strCPO_CL_CODPOSTAL = txtNvoCP.Text.Trim
                clsCodigoPostal._strCPO_DS_COLONIA = txtNvoColonia.Text.Trim
                clsCodigoPostal._intCPO_FG_STATUS = cmbNvoEstatus.SelectedValue
                clsCodigoPostal._strUSR_CL_CVE = "ADMIN"

                dsConsultaCP = clsCodigoPostal.ManejaCotPost(2)
                lblMensaje.Text += "<script>alert('El registro se guardó con éxito');</script>"

                clsCodigoPostal._strCPO_CL_CODPOSTAL = CStr(txtNvoCP.Text.ToString.Trim)
                dsConsultaCP = clsCodigoPostal.ManejaCotPost(8)
                grvMuestraExistente.DataSource = dsConsultaCP.Tables(1)
                grvMuestraExistente.DataBind()
                divMostrarExistente.Visible = True
                divControlesInsert.Visible = False
                'CierraPantalla("./manejaCodigoPostal.aspx")

            End If
        Else
            MensajeError("No existe información.")
        End If
    End Sub

    Public Sub CierraPantalla(ByVal strPant As String, Optional ByVal withMsj As Boolean = True)

        If withMsj Then
            lblMensaje.Text += "<script>alert('El registro se guardó con éxito'); document.location.href='" & strPant & "';</script>"
        Else
            lblMensaje.Text += "<script>document.location.href='" & strPant & "';</script>"
        End If

    End Sub

    Protected Sub cmbNvoEntidadFederativa_SelectedIndexChanged1(sender As Object, e As EventArgs)
        'Dim ddl2 As DropDownList = DirectCast(grvInserta.Rows(0).Cells(2).FindControl("cmbCiudad"), DropDownList)
        'BUG-PC-198:
        CargaCombos(2)
        CargaCombos(3)
    End Sub

    Private Function ValidaControles(ByVal ddl1 As DropDownList, ByVal ddl2 As DropDownList, ByVal ddl3 As DropDownList, txt1 As TextBox, index As Integer) As Boolean
        ValidaControles = False
        Dim repetidos As Integer = 0
        LimpiaError()

        If ddl1.SelectedValue = 0 Then
            strErr = "Debe seleccionar una entidad federativa."
            Exit Function
        End If

        If ddl2.SelectedValue = 0 Then
            strErr = "Debe de seleccionar una ciudad o la opción * - * en caso de no encontrar ciudad."
            Exit Function
        End If

        If ddl3.SelectedValue = 0 Then
            strErr = "Debe seleccionar un municipio."
            Exit Function
        End If

        If txt1.Text.Trim.Length = 0 Then
            strErr = "Debe de ingresar una colonia."
            Exit Function
        End If

        ValidaControles = True
        Return ValidaControles
    End Function

    Protected Sub btnEdit_Click(sender As Object, e As ImageClickEventArgs)

    End Sub

    Public Function CutText(ByVal str As Object) As String
        Dim text = CType(str, String)
        If text.Length > 20 Then
            text = text.Substring(0, 20) + "..."
        End If
        Return text
    End Function

    Protected Sub grvMuestraExistente_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Coman_Delete" Then
            Dim CountRows As Integer
            Dim ValueID As Integer
            CountRows = grvMuestraExistente.Rows.Count.ToString()
            If CountRows > 1 Then
                ValueID = e.CommandArgument

                clsCodigoPostal._intCPO_FL_CP = ValueID

                dsConsultaCP = clsCodigoPostal.ManejaCotPost(4)

                If (Not IsNothing(dsConsultaCP) AndAlso dsConsultaCP.Tables.Count > 0 AndAlso
                   dsConsultaCP.Tables(0).Rows.Count() > 0) Then

                    If dsConsultaCP.Tables(0).Rows(0).Item("RESULTADO") = 1 Then
                        lblMensaje.Text += "<script>alert('El registro se elimino con éxito');</script>"

                        clsCodigoPostal._strCPO_CL_CODPOSTAL = CStr(txtCP.Text.ToString.Trim)
                        dsConsultaCP = clsCodigoPostal.ManejaCotPost(8)

                        If (Not IsNothing(dsConsultaCP) AndAlso dsConsultaCP.Tables.Count > 0 AndAlso
                            dsConsultaCP.Tables(0).Rows.Count > 0) Then
                            If (dsConsultaCP.Tables(0).Rows(0).Item("RESULT") = 1) Then                                
                                grvMuestraExistente.DataSource = dsConsultaCP.Tables(1)
                                grvMuestraExistente.DataBind()

                            ElseIf (dsConsultaCP.Tables(0).Rows(0).Item("RESULT") = 0) Then
                              
                                MensajeError("No existe el Código Postal")
                            End If
                        Else
                            MensajeError("No existe información.")
                        End If
                    Else
                        MensajeError("Error")
                    End If
                Else
                    MensajeError("Error al eliminar el registro.")
                End If

            Else
                MensajeError("No se puede eliminar el ultimo registro.")
                Exit Sub

            End If
        End If

        If e.CommandName = "Coman_Edit" Then
            Dim intID_CP As Integer = CInt(e.CommandArgument)
            divControlesInsert.Visible = True
            CargaCombos(1)
            clsCodigoPostal._intCPO_FL_CP = intID_CP
            dsConsultaCP = clsCodigoPostal.ManejaCotPost(1)
            If (Not IsNothing(dsConsultaCP) AndAlso dsConsultaCP.Tables.Count > 0 AndAlso
                dsConsultaCP.Tables(0).Rows.Count() > 0) Then

                cmbNvoEntidadFederativa.SelectedValue = CInt(dsConsultaCP.Tables(0).Rows(0).Item("EFD_CL_CVE"))
                CargaCombos(2)
                cmbCiudad.SelectedValue = CInt(dsConsultaCP.Tables(0).Rows(0).Item("CIU_CL_CIUDAD"))
                CargaCombos(3)
                cmbNvoMunicipio.SelectedValue = CInt(dsConsultaCP.Tables(0).Rows(0).Item("MUN_CL_CVE"))
                txtNvoColonia.Text = CStr(dsConsultaCP.Tables(0).Rows(0).Item("CPO_DS_COLONIA"))
                cmbNvoEstatus.SelectedValue = CInt(dsConsultaCP.Tables(0).Rows(0).Item("CPO_FG_STATUS"))
                txtNvoCP.Text = CStr(dsConsultaCP.Tables(0).Rows(0).Item("CPO_CL_CODPOSTAL"))
                btnUpdate.Visible = True
                btnGuardarNvo.Visible = False
                ViewState("vwsID_CP") = intID_CP
            Else
                MensajeError("Error al consultar la información.")
                Exit Sub
            End If

        End If
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs)
        'Dim strLocation As String = ("../aspx/manejaCodigoPostal.aspx")
        'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
        CierraPantalla("./manejaCodigoPostal.aspx", False)
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        clsCodigoPostal._intEFD_CL_CVE = cmbNvoEntidadFederativa.SelectedValue
        clsCodigoPostal._intCIU_CL_CIUDAD = cmbCiudad.SelectedValue
        clsCodigoPostal._intCPO_FL_CP = CInt(ViewState("vwsID_CP"))
        clsCodigoPostal._intMUN_CL_CVE = cmbNvoMunicipio.SelectedValue
        clsCodigoPostal._strCPO_CL_CODPOSTAL = CStr(txtNvoCP.Text.ToString.Trim)
        clsCodigoPostal._strCPO_DS_COLONIA = CStr(txtNvoColonia.Text.ToString.Trim)
        clsCodigoPostal._intCPO_FG_STATUS = cmbNvoEstatus.SelectedValue
        clsCodigoPostal._strUSR_CL_CVE = "ADMIN"

        dsConsultaCP = clsCodigoPostal.ManejaCotPost(3)

        If (Not IsNothing(dsConsultaCP) AndAlso dsConsultaCP.Tables.Count > 0 AndAlso dsConsultaCP.Tables(0).Rows.Count > 0) Then
            clsCodigoPostal._strCPO_CL_CODPOSTAL = CStr(txtCP.Text.ToString.Trim)
            dsConsultaCP = clsCodigoPostal.ManejaCotPost(8)
            If (Not IsNothing(dsConsultaCP) AndAlso dsConsultaCP.Tables.Count > 0 AndAlso dsConsultaCP.Tables(0).Rows.Count > 0) Then
                lblMensaje.Text += "<script>alert('El registro se actualizó con éxito');</script>"
                grvMuestraExistente.DataSource = dsConsultaCP.Tables(1)
                grvMuestraExistente.DataBind()
                divControlesInsert.Visible = False
            Else
                MensajeError("Error al consultar la información.")
                Exit Sub
            End If
        Else
            MensajeError("Error al actualizar la información.")
            Exit Sub
        End If

    End Sub

    Public Sub LimpiaControles()
        cmbNvoEntidadFederativa.SelectedValue = 0
        cmbCiudad.SelectedValue = -1
        cmbNvoMunicipio.SelectedValue = 0
        txtNvoColonia.Text = String.Empty
        cmbNvoEstatus.SelectedValue = 0
    End Sub

    Protected Sub grvMuestraExistente_RowDataBound(sender As Object, e As GridViewRowEventArgs)


    End Sub

    Public Class grvAnalizaCP
        Public ID As Integer
        Public ENTIDAD_FEDERATIVA As String
        Public CIUDAD As String
        Public MUNICIPIO As String
        Public COLONIA As String
        Public btn As ImageButton
    End Class

    Protected Sub cmbNvoMunicipio_SelectedIndexChanged(sender As Object, e As EventArgs)
        'BUG-PC-198:
        'CargaCombos(3)
    End Sub

    Public Function fn_Ciudad() As Integer
        Dim dsMun As New DataSet
        Dim dtMun As New DataTable
        Dim row As DataRow()
        Dim idCiudad As Integer
        Dim idMun As Integer

        dsMun = ViewState("vwsMunicipio")
        dtMun = dsMun.Tables(0)
        idMun = cmbNvoMunicipio.SelectedValue.ToString
        row = dtMun.Select("MUN_CL_CVE =" & idMun)
        idCiudad = CInt(row(0).Item("CIU_CL_CIUDAD").ToString)

        Return idCiudad
    End Function

End Class
