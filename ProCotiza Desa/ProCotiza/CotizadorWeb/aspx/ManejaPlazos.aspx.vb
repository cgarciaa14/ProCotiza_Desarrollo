Imports System.Data

Partial Class aspx_ManejaPlazos
    Inherits System.Web.UI.Page
    Dim strErr As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()

        If Not IsPostBack Then
            CargaCombos()
            If Val(Request("plazoId")) > 0 Then
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

    Private Function CargaCombos() As Boolean
        Dim objCombo As New clsProcGenerales
        Dim dtsRes As New DataSet
        Try
            CargaCombos = False

            'Combo Estatus
            dtsRes = objCombo.ObtenInfoParametros(1, strErr)
            If Trim$(strErr) = "" Then
                objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", ddlestatus, strErr, False)
                If Trim$(strErr) <> "" Then
                    MensajeError(strErr)
                    Exit Function
                End If
            Else
                MensajeError(strErr)
                Exit Function
            End If
            'Combo Periodicidad
            dtsRes = objCombo.ObtenInfoParametros(82, strErr, False, "1")
            If Trim$(strErr) = "" Then
                objCombo.LlenaCombos(dtsRes, "TEXTO", "ID_PARAMETRO", ddlperiodicidad, strErr, False)
                LlenaDesc()
                If Trim$(strErr) <> "" Then
                    MensajeError(strErr)
                    Exit Function
                End If
            Else
                MensajeError(strErr)
                Exit Function
            End If

            CargaCombos = True
        Catch ex As Exception
            CargaCombos = False
            MensajeError(ex.Message)
        End Try
    End Function

    Private Sub CargaInfo()
        Try
            Dim intPlazo As Integer = Val(Request("plazoId"))
            Dim objPlazo As New SNProcotiza.clsPlazo(intPlazo)

            If objPlazo.Id_Plazo > 0 Then
                lblidplazo.Text = objPlazo.Id_Plazo
                txtdesc.Text = objPlazo.Nombre
                txtvalor.Text = objPlazo.Valor
                ddlestatus.SelectedValue = objPlazo.Status
                ddlperiodicidad.SelectedValue = objPlazo.Id_Periodicidad
                'chkDefault.Checked = IIf(objPlazo.RegistroDefault = 1, True, False)
            Else
                MensajeError("No se localizó información para la marca.")
            End If
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Public Sub LlenaDesc()
        txtdesc.Text = String.Empty
        If ddlperiodicidad.SelectedValue = 83 Then
            txtdesc.Text = "MESES"
        ElseIf ddlperiodicidad.SelectedValue = 84 Then
            txtdesc.Text = "SEMANAS"
        ElseIf ddlperiodicidad.SelectedValue = 95 Then
            txtdesc.Text = "QUINCENAS"
        End If
    End Sub

    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If ddlestatus.SelectedValue = "" Then Exit Function
        If ddlperiodicidad.SelectedValue = "" Then Exit Function
        If Trim(txtdesc.Text) = "" Then Exit Function
        If Trim(txtvalor.Text) = "" Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then

                Dim intPlazo As Integer = Val(Request("plazoId"))
                Dim objPlazo As New SNProcotiza.clsPlazo
                Dim intOpc As Integer = 2

                objPlazo.CargaSession(Val(Session("cveAcceso")))
                If intPlazo > 0 Then
                    objPlazo.CargaPlazo(intPlazo)
                    intOpc = 3
                End If

                LlenaDesc()
                'guardamos la info del plazo
                objPlazo.Nombre = Trim(txtvalor.Text) & " " & Trim(txtdesc.Text)
                objPlazo.Valor = Convert.ToInt16(Trim(txtvalor.Text))
                objPlazo.Status = ddlestatus.SelectedValue
                objPlazo.Id_Periodicidad = ddlperiodicidad.SelectedValue
                'objMarca.RegistroDefault = IIf(chkDefault.Checked, 1, 0)
                objPlazo.Usu_Reg = objPlazo.UserNameAcceso

                objPlazo.ManejaPlazos(intOpc)
                If objPlazo.StrErrPlazo = "" Then
                    CierraPantalla("./consultaPlazos.aspx")
                Else
                    MensajeError(objPlazo.StrErrPlazo)
                End If

            Else
                MensajeError("Todos los campos marcados con * son obligatorios.")
            End If

        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaPlazos.aspx?idPlazo=0")
    End Sub

    Protected Sub ddlperiodicidad_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlperiodicidad.SelectedIndexChanged
        LlenaDesc()
    End Sub
End Class
