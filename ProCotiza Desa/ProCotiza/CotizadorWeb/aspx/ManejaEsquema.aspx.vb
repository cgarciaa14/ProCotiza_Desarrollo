'BBVA RQTARESQ-06 CGARCIA 19/04/2017 SE CREA EL OBJETO DE MANEJA ESQUEMA PARA LA RELACION DEL CATÁLOGO DE ESQUEMAS 

Imports System.Data
Partial Class aspx_ManejaEsquema
    Inherits System.Web.UI.Page
    Dim strErr As String = ""
    Dim intCach As Integer

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()
        If Not IsPostBack Then
            CargaCombos(1)            
            If Val(Request("ID_ESQUEMA")) <> 0 Then
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

        Dim dts As New DataSet
        Dim objProducto As New SNProcotiza.clsEsquema
        objProducto.IDEsquema = Val(Request("ID_ESQUEMA"))
        dts = objProducto.MAnejaEsquema(1)        
        Try
                    If dts.Tables(0).Rows.Count > 0 Then
                        lblIDProducto.Text = dts.Tables(0).Rows(0).Item("ID_ESQUEMAS")
                        txtEsquema.Text = dts.Tables(0).Rows(0).Item("C_ESQUEMAS")
                        txtDes.Text = dts.Tables(0).Rows(0).Item("CDESCRIPCION")
                        cmbEstatus.SelectedValue = dts.Tables(0).Rows(0).Item("C_ESTATUS")                        
                        chkDefault.Checked = IIf(dts.Tables(0).Rows(0).Item("REG_DEFAULT") = 1, True, False)
                    Else
                        MensajeError("No se localizó información para la plaza.")
                        Exit Sub
                    End If                
        Catch ex As Exception

        End Try
    End Sub
    Private Function CargaCombos(ByVal intOpc As Integer) As Boolean
        Dim objCombo As New clsProcGenerales
        Dim objEsquema As New SNProcotiza.clsEsquema
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
                Case 2
                    'combo de estatus
                    dtsRes = objEsquema.MAnejaEsquema(1)                    
                    Dim rows As DataRow() = (From t In dtsRes.Tables(0).AsEnumerable().Cast(Of DataRow)()
                                          Where t.Field(Of Integer)("C_ESTATUS") = 3).ToArray()
                    For Each row As DataRow In rows
                        dtsRes.Tables(0).Rows.Remove(row)
                    Next                   
                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dtsRes, "C_ESQUEMAS", "ID_ESQUEMAS", 1, strErr, False, True, "REG_DEFAULT")
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
                Dim strEsquema As Integer = Val(Request("ID_ESQUEMA"))
                Dim objEsquema As New SNProcotiza.clsEsquema
                Dim intOpc As Integer = 2

                objEsquema.CargaSession(Val(Session("cveAcceso")))

                If lblIDProducto.Text <> "" Then
                    objEsquema.IDEsquema = strEsquema
                    intOpc = 3
                End If

                'Guardamos
                objEsquema.Nombre = Trim(txtEsquema.Text)
                objEsquema.Descripcion = Trim(txtDes.Text)
                objEsquema.IDEstatus = Val(cmbEstatus.SelectedValue)
                objEsquema.RegistroDefault = IIf(chkDefault.Checked, 1, 0)
                objEsquema.UsuReg = objEsquema.UserNameAcceso
                'objProducto.IDProductoUG = Val(ddlProductoUG.SelectedValue)


                objEsquema.MAnejaEsquema(intOpc)
                If objEsquema.ErrorEsquemas = "" Then
                    CierraPantalla("./Esquemas.aspx")
                Else
                    MensajeError(objEsquema.ErrorEsquemas)
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

        If Trim(txtEsquema.Text) = "" Then Exit Function
        If Trim(txtDes.Text) = "" Then Exit Function
        'If ddlProductoUG.SelectedValue = 0 Then Exit Function
        If cmbEstatus.SelectedValue = 0 Then Exit Function

        ValidaCampos = True
    End Function
    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./Esquemas.aspx")
    End Sub
End Class
