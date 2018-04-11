'BBV-P-412:AVH:28/06/2016 RQ16: SE CREA CATALOGO ALIANZAS
'BBVA-P-412: 25-08-2016: AMR: RQCOT-01 Ajustes a la pantalla de Cotización. 
'BBV-P-412_RQCOT-05:AVH:31/08/2016 SE AGREGA RUTA DE IMAGEN PARA EL REPORTE
'BUG-PC-13: AMR: 24/11/2016 Se quita valor por default en los combos.
'BUG-PC-34 MAUT 12/01/2017 Se hace obligatoria la carga de la imagen
'BBV-P-412:BUG-PC-35 18/01/2017 JRHM Se cambia cambian campos obligatorios a campos normales
'BUG-PC-38 MAUT 23/01/2017 Se quita campo de imagen reporte
'BUG-PC-39 25/01/17 Correccion de errores multiples
'BBV-P-412 RQADM-04 ERV 20/04/2017 Se agrego funcionalidad para VALIDA_PROSPECTOR
Imports System.Data
Imports System.IO

Partial Class aspx_manejaAlianzas
    Inherits System.Web.UI.Page
    Dim strErr As String = ""
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()

        If Not IsPostBack Then
            If Not CargaCombos(1) Then
                MensajeError(strErr)
            End If
            If Val(Request("idAlianza")) >= 0 Then
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
            Dim dts As New DataSet
            Dim objAlianza As New SNProcotiza.clsAlianzas
            objAlianza.IDAlianza = Val(Request("idAlianza"))

            dts = objAlianza.ManejaAlianza(1)

            If dts.Tables(0).Rows.Count > 0 Then

                lblIDAlianza.Text = dts.Tables(0).Rows(0).Item("ID_ALIANZA")
                txtAlianza.Text = dts.Tables(0).Rows(0).Item("ALIANZA")
                txtDes.Text = dts.Tables(0).Rows(0).Item("DESCRIPCION")
                txtURL.Text = dts.Tables(0).Rows(0).Item("URL")
                cmbEstatus.SelectedValue = dts.Tables(0).Rows(0).Item("ESTATUS")
                ChkProspector.Checked = IIf(dts.Tables(0).Rows(0).Item("VALIDA_PROSPECTOR") = 1, True, False)
                chkDefault.Checked = IIf(dts.Tables(0).Rows(0).Item("REG_DEFAULT") = 1, True, False)
                ddlbroker.SelectedValue = dts.Tables(0).Rows(0).Item("ID_BROKER")
                'Dim IMG As String = dts.Tables(0).Rows(0).Item("IMG_REP")

                'Dim strPath As String = System.AppDomain.CurrentDomain.BaseDirectory
                'IMG = Replace(IMG, strPath, "~/")
                'IMG = Replace(IMG, "\", "/")
                'Me.Image1.ImageUrl = IMG
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

                    'Combo Broker
                    Dim objbroker As New SNProcotiza.clsBrokerSeguros
                    objbroker.IDEstatus = 2
                    dtsRes = objbroker.ManejaBroker(1)
                    If objbroker.ErrorBroker = "" Then

                        If dtsRes.Tables.Count > 0 Then
                        If dtsRes.Tables(0).Rows.Count > 0 Then
                                objCombo.LlenaCombos(dtsRes, "RAZON_SOCIAL", "ID_BROKER", ddlbroker, strErr, False, True, "REG_DEFAULT")
                            If strErr <> "" Then
                                Exit Function
                            End If
                        Else
                                strErr = "No se encontro información de Brokers."
                            Exit Function
                        End If
                    Else
                            strErr = "No se encontro información."
                        End If
                    Else
                        strErr = objbroker.ErrorBroker
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
                Dim intAlianza As Integer = Val(Request("idAlianza"))
                Dim objAlianza As New SNProcotiza.clsAlianzas
                Dim intOpc As Integer = 2

                objAlianza.CargaSession(Val(Session("cveAcceso")))
                If intAlianza >= 0 Then
                    objAlianza.CargaAlianza(intAlianza)
                    intOpc = 3
                End If

                'Dim FileName As String
                'FileName = Path.GetFileName(Me.txtNomImagen.Value)
                'Dim strPath As String = System.AppDomain.CurrentDomain.BaseDirectory + "img\"
                'Dim img As String = strPath + FileName

                'If txtNomImagen.Value <> "" Then
                '    If Not System.IO.File.Exists(img) Then
                '        My.Computer.FileSystem.CopyFile(Me.txtNomImagen.Value, img)
                '    End If
                '    objAlianza.Img_Rep = img
                'Else
                '    img = ""
                'End If

                'Guardamos
                objAlianza.Alianza = Trim(txtAlianza.Text)
                objAlianza.Descripcion = Trim(txtDes.Text)
                objAlianza.URL = Trim(txtURL.Text)
                objAlianza.Estatus = Val(cmbEstatus.SelectedValue)
                objAlianza.Valida_Prospector = IIf(ChkProspector.Checked, 1, 0)
                objAlianza.RegistroDefault = IIf(chkDefault.Checked, 1, 0)
                objAlianza.UsuarioRegistro = objAlianza.UserNameAcceso
                objAlianza.IDBroker = ddlbroker.SelectedValue
                'objAlianza.Img_Rep = img

                Dim dts As New DataSet
                dts = objAlianza.ManejaAlianza(intOpc)
                If dts.Tables(0).Rows(0).Item("ID_ALIANZA") = -1 Then
                    MensajeError("El nombre de la Alianza ya existe.")
                    Exit Sub
                End If
                If objAlianza.ErrorAlianza = "" Then
                    CierraPantalla("./consultaAlianzas.aspx")
                Else
                    MensajeError(objAlianza.ErrorAlianza)
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

        If Trim(txtAlianza.Text) = "" Then Exit Function
        'If Trim(txtDes.Text) = "" Then Exit Function
        'If Trim(txtURL.Text) = "" Then Exit Function

        If cmbEstatus.SelectedValue = 0 Then Exit Function

        ''BUG-PC-34 MAUT 12/01/2017 Se hace obligatoria la carga de la imagen
        'Dim FileName As String
        'FileName = Path.GetFileName(Me.txtNomImagen.Value)
        'If Trim(FileName) = "" Then Exit Function

        ValidaCampos = True
    End Function
    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaAlianzas.aspx")
    End Sub
End Class
