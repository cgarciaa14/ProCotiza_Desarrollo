'BBV-P-412 RQCOT-05: AVH: 08/09/2016 SE AGREGA SECCION
'BUG-PC-48 JRHM 21/02/17 Se agrega propiedad de maxlength para que respete la longitud de 255 caracteres

Imports System.Data
Imports SNProcotiza

Partial Class aspx_manejaLeyendasRep
    Inherits System.Web.UI.Page
    Dim strErr As String = ""
    Dim strClasif As String = ""
    Dim strPersonas As String = ""
    Dim strUser As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()
        If Not IsPostBack Then
            txtLeyenda.Attributes.Add("maxlength", txtLeyenda.MaxLength.ToString())
            CargaCombos(1)
            CargaCombos(2)
            If Val(Request("idLeyenda")) > 0 Then
                CargaInfo()
            End If
        End If
    End Sub

    Public Sub LimpiaError()
        lblMensaje.Text = ""
    End Sub

    Private Sub LimpiaFiltros()
        'txtIdUsu.Text = ""
        'txtNomUsu.Text = ""
        ''cmbEstatus.SelectedValue = ""
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

    Private Sub CargaInfo()
        Try
            Dim intLey As Integer = Val(Request("idLeyenda"))
            Dim objLey As New SNProcotiza.clsLeyendasRep(intLey)

            If objLey.IDLeyenda > 0 Then
                lblId.Text = objLey.IDLeyenda
                txtLeyenda.Text = objLey.Leyenda
                cmbEstatus.SelectedValue = objLey.IDEstatus
                cmbSeccion.SelectedValue = objLey.IDSeccion
            Else
                MensajeError("No se localizó información para la Leyenda.")
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
                        objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbEstatus, strErr, False)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If

                    'grid clasificación producto
                    Dim objLey As New SNProcotiza.clsLeyendasRep

                    objLey.IDLeyenda = Val(Request("idLeyenda"))
                    dtsRes = objLey.ManejaLeyenda(5)

                    If Trim$(objLey.ErrorLeyenda) = "" Then
                        gdvClasif.DataSource = dtsRes
                        gdvClasif.DataBind()
                        gdvClasif.Columns(0).Visible = False

                    Else
                        MensajeError(objLey.ErrorLeyenda)
                        Exit Function
                    End If

                    'grid personalidad jurídica
                    dtsRes = objLey.ManejaLeyenda(6)

                    If Trim$(objLey.ErrorLeyenda) = "" Then
                        gdvPerJur.DataSource = dtsRes
                        gdvPerJur.DataBind()
                        gdvPerJur.Columns(0).Visible = False
                    Else
                        MensajeError(objLey.ErrorLeyenda)
                        Exit Function
                    End If
                Case 2
                    'BBV-P-412 RQCOT-05: AVH
                    Dim objParam As New clsParametros
                    Dim dtsPar As DataSet

                    objParam.IDPadre = 183
                    dtsPar = objParam.ManejaParametro(1)

                    objCombo.LlenaCombos(dtsPar, "TEXTO", "ID_PARAMETRO", cmbSeccion, strErr, True)

            End Select

            CargaCombos = True
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Function

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click

        strUser = User.Identity.Name
        Dim objsession As New clsSession
        objsession.CargaSession(strUser)

        Try
            strClasif = ""
            strPersonas = ""
            If ValidaCampos() Then
                If ValidaRelaciones() Then

                    Dim intLey As Integer = Val(Request("idLeyenda"))
                    Dim objLey As New SNProcotiza.clsLeyendasRep
                    Dim intOpc As Integer = 2

                    objLey.CargaSession(Val(Session("cveAcceso")))
                    If intLey > 0 Then
                        objLey.CargaLeyenda(intLey)
                        intOpc = 3
                    End If

                    'guardamos la info de la leyenda
                    Dim strDescLey As String = Trim(txtLeyenda.Text)
                    strDescLey = Replace(strDescLey, "<", "")
                    strDescLey = Replace(strDescLey, ">", "")
                    strDescLey = Replace(strDescLey, "/", "")
                    strDescLey = Replace(strDescLey, "!", "")

                    objLey.Leyenda = strDescLey
                    objLey.IDEstatus = Val(cmbEstatus.SelectedValue)
                    objLey.ClavesClasificacionProducto = strClasif
                    objLey.ClavesPersonalidadJuridica = strPersonas
                    objLey.UsuarioRegistro = objsession.UserNameAcceso
                    objLey.IDSeccion = cmbSeccion.SelectedValue

                    objLey.ManejaLeyenda(intOpc)
                    If objLey.ErrorLeyenda = "" Then
                        CierraPantalla("./consultaLeyendasRep.aspx")
                    Else
                        MensajeError(objLey.ErrorLeyenda)
                    End If
                End If
            Else
                MensajeError("Todos los campos marcados con * son obligatorios.")
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If cmbEstatus.SelectedValue = "" Then Exit Function
        If Trim(txtLeyenda.Text) = "" Then Exit Function

        ValidaCampos = True
    End Function

    Private Function ValidaRelaciones() As Boolean
        ValidaRelaciones = False
        Try
            Dim objRow As GridViewRow
            Dim objChk As CheckBox
            Dim intSel As Integer = 0

            'clasificación producto
            For Each objRow In gdvClasif.Rows
                If objRow.RowType = DataControlRowType.DataRow Then
                    objChk = objRow.Cells(1).Controls(1)
                    If objChk.Checked = True Then
                        'id
                        strClasif += IIf(Trim(strClasif) = "", objRow.Cells(0).Text, "@" & objRow.Cells(0).Text)
                        intSel += 1
                    End If
                End If
            Next

            If intSel = 0 Then
                MensajeError("Debe seleccionar almenos una clasificación de producto.")
                Exit Function
            End If

            'personalidad jurídica
            intSel = 0
            For Each objRow In gdvPerJur.Rows
                If objRow.RowType = DataControlRowType.DataRow Then
                    objChk = objRow.Cells(1).Controls(1)
                    If objChk.Checked = True Then
                        'id
                        strPersonas += IIf(Trim(strPersonas) = "", objRow.Cells(0).Text, "@" & objRow.Cells(0).Text)
                        intSel += 1
                    End If
                End If
            Next

            If intSel = 0 Then
                MensajeError("Debe seleccionar almenos una personalidad jurídica.")
                Exit Function
            End If

            If cmbSeccion.SelectedValue = 0 Then
                MensajeError("Debe seleccionar alguna seccion.")
                Exit Function
            End If

            ValidaRelaciones = True

        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Function

    Protected Sub gdvClasif_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvClasif.RowDataBound
        Try
            If Val(Request("idLeyenda")) > 0 Then
                If e.Row.RowType = DataControlRowType.DataRow Then
                    Dim intClasif = Val(e.Row.Cells(0).Text)
                    Dim objLey As New SNProcotiza.clsLeyendasRep

                    objLey.IDLeyenda = Val(Request("idLeyenda"))
                    objLey.IDClasificacionProducto = intClasif
                    Dim dtsRes As DataSet = objLey.ManejaLeyenda(5)

                    If Trim(objLey.ErrorLeyenda) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                If dtsRes.Tables(0).Rows(0).Item("RELACION") = 1 Then
                                    Dim objChk As New CheckBox
                                    objChk = e.Row.Cells(1).Controls(1)
                                    objChk.Checked = True
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gdvPerJur_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvPerJur.RowDataBound
        Try
            If Val(Request("idLeyenda")) > 0 Then
                If e.Row.RowType = DataControlRowType.DataRow Then
                    Dim intPerJur = Val(e.Row.Cells(0).Text)
                    Dim objLey As New SNProcotiza.clsLeyendasRep

                    objLey.IDLeyenda = Val(Request("idLeyenda"))
                    objLey.IDPersonalidadJuridica = intPerJur
                    Dim dtsRes As DataSet = objLey.ManejaLeyenda(6)

                    If Trim(objLey.ErrorLeyenda) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                If dtsRes.Tables(0).Rows(0).Item("RELACION") = 1 Then
                                    Dim objChk As New CheckBox
                                    objChk = e.Row.Cells(1).Controls(1)
                                    objChk.Checked = True
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaLeyendasRep.aspx")
    End Sub
End Class
