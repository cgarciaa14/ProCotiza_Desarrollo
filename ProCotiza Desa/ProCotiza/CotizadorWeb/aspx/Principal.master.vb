' BBV-P-412  RQ-E  gvargas  12/10/2016 Se agrego opciones para menu nuevo y antiguo.
' BBV-P-412: GVARGAS: 07/11/2016 RQ F: CSS dinamico
' BUG-PC-55 GVARGAS 21/04/17 Cambios LogIn TC

Imports System.Data
Imports SNProcotiza
Imports System.Web.HttpContext


Partial Class aspx_Principal
    Inherits System.Web.UI.MasterPage
    Public CargaMenu As String
    Public CargaMenuAntiguo As String
    Public menuStat As String


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Me.LessCSS.InnerText = "@import url(../CSSDinamics/jaguar.less); "
        'If (Session("agencia") IsNot Nothing) Then
        '    Dim alianza As String = Session("agencia").ToString()
        '    If (alianza = "A") Then
        '        Me.LessCSS.InnerText = "@import url(../CSSDinamics/acura.less); "
        '    ElseIf (alianza = "H") Then
        '        Me.LessCSS.InnerText = "@import url(../CSSDinamics/honda.less); "
        '    ElseIf (alianza = "J") Then
        '        Me.LessCSS.InnerText = "@import url(../CSSDinamics/jaguar.less); "
        '    ElseIf (alianza = "S") Then
        '        Me.LessCSS.InnerText = "@import url(../CSSDinamics/suzuki.less); "
        '    End If
        'End If

        Dim objAcceso As New SNProcotiza.clsSession(Val(Session("cveAcceso")))
        Dim objUsu As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), objAcceso.UsuarioAccesoID)

        CargaMenu = Menu(6, objUsu.IDPerfil)
        CargaMenuAntiguo = Menu(7, objUsu.IDPerfil)
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
