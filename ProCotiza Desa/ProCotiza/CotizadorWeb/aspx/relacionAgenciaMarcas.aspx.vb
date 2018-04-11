Imports System.Data

Partial Class aspx_relacionAgenciaMarcas
    Inherits System.Web.UI.Page
    Dim strError As String = String.Empty

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()

        If Not IsPostBack Then
            BuscaDatos()
        End If

    End Sub

    Public Sub MensajeError(ByVal strMsj As String)
        strMsj = Replace(strMsj, "'", "")
        strMsj = Replace(strMsj, """", "")
        strMsj = Replace(strMsj, "(", "")
        strMsj = Replace(strMsj, ")", "")
        lblMensaje.Text = "<script>alert('" & strMsj & "')</script>"
    End Sub

    Public Sub LimpiaError()
        lblMensaje.Text = ""
        lblScript.Text = ""
    End Sub

    Private Sub BuscaDatos()
        Try
            Dim objAgencia As New SNProcotiza.clsAgencias
            Dim dtsRes As New DataSet

            objAgencia.IDAgencia = Val(Request("AgeID"))
            dtsRes = objAgencia.ManejaAgencia(24)

            Session("dtsConsultaMarcas") = Nothing
            grvConsulta.PageIndex = 0
            grvConsulta.DataSource = Nothing

            If dtsRes.Tables.Count > 0 Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    Session("dtsConsultaMarcas") = dtsRes
                    grvConsulta.DataSource = dtsRes
                Else
                    ''MensajeError("No se encontró información con los parámetros proporcionados.")
                End If
            Else
                ''MensajeError("No se encontró información para los parámetros proporcionados.")
            End If

            grvConsulta.DataBind()
        Catch ex As Exception
            MensajeError(ex.Message)
        End Try
    End Sub

    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsultaMarcas"), DataSet)
        grvConsulta.DataBind()
    End Sub
End Class
