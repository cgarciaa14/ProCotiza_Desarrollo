'BUG-PC-13: AMR: 23/11/2016 Se quita valor por default en los combos.
'BUG-PC-34 MAUT 16/01/2017 Si el IDexterno está vacío se manda 0
'BUG-PC-39 25/01/17 Se cambio conversion de id externo a entero
Imports System.Data

Partial Class aspx_manejaBrokers
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
            If Val(Request("idBroker")) > 0 Then
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
            Dim intBroker As Integer = Val(Request("idBroker"))
            Dim objBroker As New SNProcotiza.clsBrokerSeguros(intBroker)

            If objBroker.IDBroker > 0 Then
                lblId.Text = objBroker.IDBroker
                txtNom.Text = objBroker.RazonSocial
                txtcorto.Text = objBroker.NomCorto
                'txtprima.Text = objBroker.ConstantePrima
                cmbTipoCalcSeg.SelectedValue = objBroker.TipoCalc
                txtexterno.Text = IIf(objBroker.IDExterno = 0, "", objBroker.IDExterno)
                cmbEstatus.SelectedValue = objBroker.IDEstatus
                txtlink.Text = objBroker.Link
                chkDefault.Checked = IIf(objBroker.RegistroDefault = 1, True, False)
            Else
                MensajeError("No se localizó información para el Broker.")
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
                    'Combo Tipo Calculo Seguro
                    dtsRes = objCombo.ObtenInfoParametros(86, strErr, False, 1)
                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", cmbTipoCalcSeg, strErr)
                        If Trim$(strErr) <> "" Then
                            MensajeError(strErr)
                            Exit Function
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If

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
        If ValidaCampos() Then
            Dim intBroker As Integer = Val(Request("idBroker"))
            Dim objBroker As New SNProcotiza.clsBrokerSeguros
            Dim intOpc As Integer = 2

            objBroker.CargaSession(Val(Session("cveAcceso")))
            If intBroker > 0 Then
                objBroker.CargaBroker(intBroker)
                intOpc = 3
            End If

            'guardamos la info del tipo de operación
            objBroker.RazonSocial = Trim(txtNom.Text)
            objBroker.NomCorto = Trim(txtcorto.Text)
            'objBroker.ConstantePrima = Convert.ToDouble(Trim(txtprima.Text))
            objBroker.TipoCalc = cmbTipoCalcSeg.SelectedValue
            objBroker.RegistroDefault = IIf(chkDefault.Checked, 1, 0)
            If txtexterno.Text.Trim.Length > 0 Then
                objBroker.IDExterno = CLng(txtexterno.Text.ToString)
            Else
                'BUG-PC-34 MAUT 16/01/2017 Si el IDexterno está vacío se manda 0
                objBroker.IDExterno = 0
            End If
            objBroker.IDEstatus = Val(cmbEstatus.SelectedValue)
            objBroker.Link = txtlink.Text.Trim
            objBroker.UsuarioRegistro = objBroker.UserNameAcceso

            objBroker.ManejaBroker(intOpc)
            If objBroker.ErrorBroker = "" Then
                CierraPantalla("./consultaBrokers.aspx")
            Else
                MensajeError(objBroker.ErrorBroker)
            End If
        Else
            MensajeError("Todos los Campos marcados con * son obligatorios")
        End If
    End Sub

    Private Function ValidaCampos() As Boolean
        ValidaCampos = False
        If Trim(txtNom.Text) = "" Then Exit Function
        If Trim(txtcorto.Text) = "" Then Exit Function
        If cmbEstatus.SelectedValue = "" Then Exit Function
        If cmbTipoCalcSeg.SelectedValue = "" Then Exit Function
        ValidaCampos = True
    End Function

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaBrokers.aspx")
    End Sub

End Class
