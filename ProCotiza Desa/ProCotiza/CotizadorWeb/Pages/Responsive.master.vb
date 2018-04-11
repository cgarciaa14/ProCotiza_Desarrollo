' BUG-PC-129 GVARGAS 06/12/2017 Creacion Master Responsive

Imports System.Data
Imports SNProcotiza
Imports System.Web.HttpContext

Partial Class Pages_Responsive
    Inherits System.Web.UI.MasterPage
    Public MenuResponsivo As String

    Public menuStat As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Me.LessCSS.InnerText = "@import url(../CSSDinamics/honda.less); "
        If (Session("agencia") IsNot Nothing) Then
            Dim alianza As String = Session("agencia").ToString()
            If (alianza = "A") Then
                Me.LessCSS.InnerText = "@import url(../CSSDinamics/acura.less); "
            ElseIf (alianza = "H") Then
                Me.LessCSS.InnerText = "@import url(../CSSDinamics/honda.less); "
            ElseIf (alianza = "J") Then
                Me.LessCSS.InnerText = "@import url(../CSSDinamics/jaguar.less); "
            ElseIf (alianza = "S") Then
                Me.LessCSS.InnerText = "@import url(../CSSDinamics/suzuki.less); "
            End If
        End If

        Dim objAcceso As New SNProcotiza.clsSession(Val(Session("cveAcceso")))
        Dim objUsu As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), objAcceso.UsuarioAccesoID)

        MenuResponsivo = Menu(8, objUsu.IDPerfil)
    End Sub

    Private Function Menu(ByVal intOpcion As Integer, ByVal intPerfil As Integer) As String
        Dim valor As String = String.Empty
        Dim clsObj As New clsObjetosSistema
        Dim dtsRes As New DataSet

        clsObj.Perfil = intPerfil
        dtsRes = clsObj.ManejaObjetoSis(intOpcion)

        If clsObj.ErrorObjetos = "" Then
            If dtsRes.Tables(0).Rows.Count > 0 Then
                valor = dtsRes.Tables(0).Rows(0).Item(0).ToString()
            End If
        End If

        Return valor
    End Function
End Class

