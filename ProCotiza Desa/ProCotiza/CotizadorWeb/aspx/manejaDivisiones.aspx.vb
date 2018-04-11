'BBV-P-412:AVH:06/07/2016 RQ19: SE CREA CATALOGO DE DIVISIONES
'BUG-PC-11: AVH: 23/11/2016 SE QUITA VALOR DEFAULT EN COMBOS
'BUG-PC-39 25/01/17 Correccion de errores multiples
Imports System.Data

Partial Class aspx_manejaDivisiones
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
            If Val(Request("idDivision")) >= 0 Then
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
            Dim objDivision As New SNProcotiza.clsDivisiones
            objDivision.IDDivision = Val(Request("idDivision"))

            dts = objDivision.ManejaDivision(1)

            If dts.Tables(0).Rows.Count > 0 Then

                lblIDDivision.Text = dts.Tables(0).Rows(0).Item("ID_DIVISION")
                txtDivision.Text = dts.Tables(0).Rows(0).Item("DIVISION")
                txtDes.Text = dts.Tables(0).Rows(0).Item("DESCRIPCION")
                txtURL.Text = dts.Tables(0).Rows(0).Item("URL")
                cmbEstatus.SelectedValue = dts.Tables(0).Rows(0).Item("ESTATUS")
                chkDefault.Checked = IIf(dts.Tables(0).Rows(0).Item("REG_DEFAULT") = 1, True, False)
            Else
                MensajeError("No se localizó información para la plaza.")
                Exit Sub
            End If
        Catch ex As Exception
            ' MensajeError(ex.Message)
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
                Dim intDivision As Integer = Val(Request("idDivision"))
                Dim objDivision As New SNProcotiza.clsDivisiones
                Dim intOpc As Integer = 2

                objDivision.CargaSession(Val(Session("cveAcceso")))
                If intDivision >= 0 Then
                    objDivision.CargaDivision(intDivision)
                    intOpc = 3
                End If

                'Guardamos
                objDivision.Division = Trim(txtDivision.Text)
                objDivision.Descripcion = Trim(txtDes.Text)
                objDivision.URL = Trim(txtURL.Text)
                objDivision.Estatus = Val(cmbEstatus.SelectedValue)
                objDivision.RegistroDefault = IIf(chkDefault.Checked, 1, 0)
                objDivision.UsuarioRegistro = objDivision.UserNameAcceso



                Dim dts As New DataSet()
                dts = objDivision.ManejaDivision(intOpc)
                If dts.Tables(0).Rows(0).Item("ID_DIVISION") = -1 Then
                    MensajeError("El nombre de la division ya existe.")
                    Exit Sub
                End If
                If objDivision.ErrorDivision = "" Then
                    CierraPantalla("./consultaDivisiones.aspx")
                Else
                    MensajeError(objDivision.ErrorDivision)
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

        If Trim(txtDivision.Text) = "" Then Exit Function
        If Trim(txtDes.Text) = "" Then Exit Function
        If Trim(txtURL.Text) = "" Then Exit Function
        If Trim(txtDes.Text) = "" Then Exit Function
        If cmbEstatus.SelectedValue = 0 Then Exit Function

        ValidaCampos = True
    End Function
    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaDivisiones.aspx")
    End Sub
End Class
