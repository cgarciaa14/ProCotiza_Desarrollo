'JRHM 15/11/16  BUG-PC-05 Inserccion de validacion para valor tasa iva
Imports System.Data

Partial Class aspx_manejaTasasIVA
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
            If Val(Request("idTasaIVA")) > 0 Then
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
            Dim intTasa As Integer = Val(Request("idTasaIVA"))
            Dim objTasa As New SNProcotiza.clsTasasIVA(intTasa)

            If objTasa.IDTasaIVA > 0 Then
                lblId.Text = objTasa.IDTasaIVA
                txtNom.Text = objTasa.Nombre
                txtTasa.Text = objTasa.ValorTasa
                cmbEstatus.SelectedValue = objTasa.IDEstatus
                chkDefault.Checked = IIf(objTasa.RegistroDefault = 1, True, False)
            Else
                MensajeError("No se localizó información para la tasa del IVA.")
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
                Dim intTasa As Integer = Val(Request("idTasaIVA"))
                Dim objTasa As New SNProcotiza.clsTasasIVA
                Dim intOpc As Integer = 2

                objTasa.CargaSession(Val(Session("cveAcceso")))
                If intTasa > 0 Then
                    objTasa.CargaTasaIVA(intTasa)
                    intOpc = 3
                End If

                'guardamos la info del tipo de operación
                objTasa.Nombre = Trim(txtNom.Text)
                objTasa.ValorTasa = Val(txtTasa.Text)
                objTasa.IDEstatus = Val(cmbEstatus.SelectedValue)
                objTasa.RegistroDefault = IIf(chkDefault.Checked, 1, 0)
                objTasa.UsuarioRegistro = objTasa.UserNameAcceso

                objTasa.ManejaTasaIVA(intOpc)
                If objTasa.ErrorTasasIVA = "" Then
                    CierraPantalla("./consultaTasasIVA.aspx")
                Else
                    MensajeError(objTasa.ErrorTasasIVA)
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
        If String.IsNullOrEmpty(txtTasa.Text) Then Exit Function 'JRHM 10/11/16  Se agrego esta condicion para salir de la funcion debido a que no se debe permitir que el campo tasa este vacio
        ValidaCampos = True
    End Function

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaTasasIVA.aspx")
    End Sub
End Class
