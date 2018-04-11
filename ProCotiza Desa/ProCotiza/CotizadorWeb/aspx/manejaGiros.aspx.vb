'BBV-P-412:BUG-PC-10 JRHM: 18/11/2016 Se comentaron lineas en metodo Llenagrdvtipoopr para que traiga todos los tipos de operacion y no en base al numero de giro
Imports System.Data

Partial Class aspx_manejaGiros
    Inherits System.Web.UI.Page
    Dim strErr As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()
        If Not IsPostBack Then
            CargaCombos(1)
            Llenagrdvtipoopr(1)
            If Val(Request("idGiro")) > 0 Then
                CargaInfo()
            End If
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

    Private Sub CargaInfo()
        Try
            Dim intGiro As Integer = Val(Request("idGiro"))
            Dim objGiro As New SNProcotiza.clsGiros(intGiro)

            If objGiro.IDGiro > 0 Then
                lblId.Text = objGiro.IDGiro
                txtNom.Text = objGiro.Nombre
                txtUID.Text = objGiro.IDExterno
                cmbEstatus.SelectedValue = objGiro.IDEstatus
                chkDefault.Checked = IIf(objGiro.RegistroDefault = 1, True, False)
            Else
                MensajeError("No se localizó información para el giro.")
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
            End Select

            CargaCombos = True
        Catch ex As Exception
            CargaCombos = False
            MensajeError(ex.Message)
        End Try
    End Function

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then
                Dim intGiro As Integer = Val(Request("idGiro"))
                Dim objGiro As New SNProcotiza.clsGiros
                Dim intOpc As Integer = 2

                Dim dtslstbx As New DataSet
                Dim objto As New SNProcotiza.clsTiposOperacion

                dtslstbx = objto.ManejaTipoOperacion(11)

                objGiro.CargaSession(Val(Session("cveAcceso")))
                If intGiro > 0 Then
                    objGiro.CargaGiro(intGiro)
                    intOpc = 3
                End If

                Dim _checkbx As New CheckBox
                Dim contador As Integer = 0
                Dim tipoopor As String = ""

                For i = 0 To grdvtipoopr.Rows.Count - 1
                    If grdvtipoopr.Rows(i).RowType = DataControlRowType.DataRow Then

                        _checkbx = grdvtipoopr.Rows(i).Cells(0).FindControl("checkbx")
                        If _checkbx.Checked = False Then
                            contador = contador + 1
                        ElseIf _checkbx.Checked = True Then
                            tipoopor = tipoopor & dtslstbx.Tables(0).Rows(i)(2).ToString & "."
                        End If
                    End If
                Next

                If contador > 0 Then
                    MensajeError("No se ha seleccionado tipo de operación.")
                Else
                    tipoopor = tipoopor.Substring(0, Len(tipoopor) - 1)

                    'guardamos la info del tipo de operación
                    Dim strNom As String = Trim(txtNom.Text)
                    strNom = Replace(strNom, "<", "")
                    strNom = Replace(strNom, ">", "")
                    strNom = Replace(strNom, "/", "")
                    strNom = Replace(strNom, "!", "")

                    objGiro.Nombre = strNom
                    objGiro.IDExterno = Trim(txtUID.Text)
                    objGiro.IDEstatus = Val(cmbEstatus.SelectedValue)
                    objGiro.RegistroDefault = IIf(chkDefault.Checked, 1, 0)
                    objGiro.TipoOperacion = tipoopor
                    objGiro.UsuarioRegistro = objGiro.UserNameAcceso

                    objGiro.ManejaGiro(intOpc)
                    If objGiro.ErrorGiros = "" Then
                        CierraPantalla("./consultaGiros.aspx")
                    Else
                        MensajeError(objGiro.ErrorGiros)
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
        If Trim(txtNom.Text) = "" Then Exit Function

        ValidaCampos = True
    End Function

    Private Sub Llenagrdvtipoopr(ByVal intOpc As Integer)
        Dim dtslstbx As New DataSet
        Dim objto As New SNProcotiza.clsTiposOperacion
        Dim objgiro As New SNProcotiza.clsGiros

        'If Val(Request("idGiro")) > 0 Then
        '    objgiro.IDGiro = Val(Request("idGiro"))
        '    dtslstbx = objgiro.ManejaGiro(1)
        '    grdvtipoopr.DataSource = dtslstbx
        '    grdvtipoopr.DataBind()
        'Else
            dtslstbx = objto.ManejaTipoOperacion(11)
            If dtslstbx.Tables(0).Rows.Count - 1 > 0 Then
                grdvtipoopr.DataSource = dtslstbx
                grdvtipoopr.DataBind()
            End If
        'End If
        grdvtipoopr.Columns(1).Visible = False
        grdvtipoopr.Columns(3).Visible = False
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaGiros.aspx")
    End Sub
End Class
