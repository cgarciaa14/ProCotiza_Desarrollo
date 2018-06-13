'RQ-PC8: CGARCIA: 09/05/2018: SE CREA PANTALLA

Imports System.Data
Imports SNProcotiza

Partial Class aspx_manejaCoberturas
    Inherits System.Web.UI.Page

    Dim strErr As String = String.Empty
    Dim objCobertura As New clsCoberturas
    Dim intCoberttura As Integer = 0
    Dim _str As String = String.Empty



    Protected Sub aspx_manejaCoberturas_Load(sender As Object, e As EventArgs) Handles Me.Load
        If CInt(Request("idCobertura")) <> 0 Then
            intCoberttura = CInt(Request("idCobertura"))
        End If

        If Not IsPostBack Then

            If CInt(Request("idCobertura")) <> 0 Then
                cargaCombos(1)
                intCoberttura = CInt(Request("idCobertura"))
                _fnConsultaInfo(1)
            Else
                cargaCombos(1)
            End If

        End If

    End Sub



    Private Function cargaCombos(ByVal opc As Integer) As Boolean
        Dim dsRes As New DataSet
        Dim objParam As New clsParametros
        Dim objCombo As New clsProcGenerales
        Dim objAlianza As New clsAlianzas
        cargaCombos = False

        Try
            Select Case opc
                Case 1
                    'combo de clasificacion
                    dsRes = objParam.ManejaParametro(11)
                    If objParam.ErrorParametros.Trim = String.Empty Then
                        If (Not IsNothing(dsRes) AndAlso dsRes.Tables.Count > 0 AndAlso dsRes.Tables(0).Rows.Count > 0) Then
                            objCombo.LlenaCombos(dsRes, "TEXTO", "ID_PARAMETRO", ddlClasificacion2, strErr, True)
                        Else
                            _str = ReplaceMSJ(strErr)
                            ScriptManager.RegisterStartupScript(updtInfo, updtInfo.GetType(), "Script3", "ShowMsj('" + _str + "');", True)
                        End If
                    Else
                        _str = ReplaceMSJ(strErr)
                        ScriptManager.RegisterStartupScript(updtInfo, updtInfo.GetType(), "Script3", "ShowMsj('" + _str + "');", True)
                    End If

                    'como de alianzas
                    objAlianza.IDEstatus = 2
                    objAlianza.Alianza = String.Empty
                    dsRes = objAlianza.ManejaAlianza(1)

                    If objAlianza.ErrorAlianza.Trim = String.Empty Then
                        If (Not IsNothing(dsRes) AndAlso dsRes.Tables.Count > 0 AndAlso dsRes.Tables(0).Rows.Count > 0) Then
                            objCombo.LlenaCombos(dsRes, "ALIANZA", "ID_ALIANZA", ddlAlianza2, strErr, True)
                        Else
                            _str = ReplaceMSJ(strErr)
                            ScriptManager.RegisterStartupScript(updtInfo, updtInfo.GetType(), "Script3", "ShowMsj('" + _str + "');", True)

                        End If
                    Else
                        _str = ReplaceMSJ(strErr)
                        ScriptManager.RegisterStartupScript(updtInfo, updtInfo.GetType(), "Script3", "ShowMsj('" + _str + "');", True)
                    End If

                    'combo estatus
                    dsRes = objCombo.ObtenInfoParametros(1, strErr)
                    If Trim$(strErr) = "" Then
                        objCombo.LlenaCombos(dsRes, "TEXTO", "ID_PARAMETRO", ddlEstatus, strErr, True)
                        If Trim$(strErr) <> "" Then
                            _str = ReplaceMSJ(strErr)
                            ScriptManager.RegisterStartupScript(updtInfo, updtInfo.GetType(), "Script3", "ShowMsj('" + _str + "');", True)
                        End If
                    Else
                        _str = ReplaceMSJ(strErr)
                        ScriptManager.RegisterStartupScript(updtInfo, updtInfo.GetType(), "Script3", "ShowMsj('" + _str + "');", True)
                    End If

            End Select
            cargaCombos = True
        Catch ex As Exception
            cargaCombos = False
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", "alert('" + ReplaceMSJ(ex.Message) + "');", True)
        End Try

    End Function


    Private Function ReplaceMSJ(ByVal strMsj As String) As String
        strMsj = Replace(strMsj, "'", "")
        strMsj = Replace(strMsj, """", "")
        strMsj = Replace(strMsj, "(", "")
        strMsj = Replace(strMsj, ")", "")

        Return strMsj

    End Function

    Sub _fnConsultaInfo(ByVal _tipo As Integer)
        Dim dsRes As New DataSet

        Select Case _tipo
            Case 1

                objCobertura._intOpcion = _tipo
                objCobertura._intCobertura = intCoberttura

                dsRes = objCobertura.ManejaCoberturas()
                If (dsRes.Tables.Count > 0 AndAlso dsRes.Tables(0).Rows.Count > 0) Then
                    txtName.Text = CStr(dsRes.Tables(0).Rows(0).Item("NAME").ToString)
                    ddlClasificacion2.SelectedValue = CInt(dsRes.Tables(0).Rows(0).Item("CLASIFICACION"))
                    ddlAlianza2.SelectedValue = CInt(dsRes.Tables(0).Rows(0).Item("ALIANZA"))
                    ddlEstatus.SelectedValue = CInt(dsRes.Tables(0).Rows(0).Item("ESTATUS"))
                    txtIdExterno.Text = CStr(dsRes.Tables(0).Rows(0).Item("EXTERNO").ToString)
                Else
                    _str = "Error al consultar los datos."
                    ScriptManager.RegisterStartupScript(updtInfo, updtInfo.GetType(), "Script3", "ShowMsj('" + _str + "');", True)
                End If

        End Select

    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs)
        'Response.Redirect("./consultaCoberturas.aspx", False)
        Dim strLocation As String = ("../aspx/consultaCoberturas.aspx")
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        Dim dsRes As New DataSet()

        If CInt(Request("idCobertura")) <> 0 Then
            objCobertura._intOpcion = 4
            objCobertura._intCobertura = CInt(Request("idCobertura"))
        Else
            objCobertura._intOpcion = 3
        End If

        objCobertura._strCobertura = txtName.Text.Trim
        objCobertura._intClasificacion = CInt(ddlClasificacion2.SelectedValue)
        objCobertura._intAlianza = CInt(ddlAlianza2.SelectedValue)
        objCobertura._intEstatus = CInt(ddlEstatus.SelectedValue)
        objCobertura._intIdExterno = txtIdExterno.Text.Trim

        dsRes = objCobertura.ManejaCoberturas()

        If (dsRes.Tables.Count > 0 AndAlso dsRes.Tables(0).Rows.Count > 0 AndAlso CInt(dsRes.Tables(0).Rows(0).Item("RESULTADO") = 1)) Then

            'Dim strLocation As String = ("../aspx/consultaCoberturas.aspx")
            'ScriptManager.RegisterStartupScript(updtInfo, updtInfo.GetType(), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            _str = "Cobertura guardada"

            ''btnRegresar_Click(sender, e)
            ScriptManager.RegisterStartupScript(updtInfo, updtInfo.GetType(), "Script3", "ShowMsj('" + _str + "');", True)
        Else
            _str = dsRes.Tables(1).Rows(0).Item("MensajeError").ToString()
            'ScriptManager.RegisterStartupScript(updtInfo, updtInfo.GetType(), "Script3", "ShowMsj('" + _str + "');", True)
        End If



    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function SaveCobertura(ByVal varIDCob As Integer, ByVal varNombre As String, ByVal varClasificacion As Integer, ByVal varIdExterno As String, ByVal varAlianza As Integer, ByVal varEstatus As Integer) As String
        Dim responseSave As responseSave = New responseSave()
        Dim objCobertura As New clsCoberturas

        responseSave.code = ""
        responseSave.message = ""
        responseSave.url = ""

        Dim dsRes As New DataSet()

        If varIDCob <> 0 Then
            objCobertura._intOpcion = 4
            objCobertura._intCobertura = varIDCob
        Else
            objCobertura._intOpcion = 3
        End If

        objCobertura._strCobertura = varNombre.Trim
        objCobertura._intClasificacion = varClasificacion
        objCobertura._intAlianza = varAlianza
        objCobertura._intEstatus = varEstatus
        objCobertura._intIdExterno = varIdExterno.Trim

        dsRes = objCobertura.ManejaCoberturas()

        If (dsRes.Tables.Count > 0 AndAlso dsRes.Tables(0).Rows.Count > 0 AndAlso CInt(dsRes.Tables(0).Rows(0).Item("RESULTADO") = 1)) Then
            responseSave.code = "OK"
            responseSave.message = "El registro se guardó con éxito."
            responseSave.url = "../aspx/consultaCoberturas.aspx"
        Else
            responseSave.code = "FAIL"
            responseSave.message = dsRes.Tables(1).Rows(0).Item("MensajeError").ToString()
        End If

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim json_Respuesta As String = serializer.Serialize(responseSave)
        Return json_Respuesta
    End Function

    Public Class responseSave
        Public code As String
        Public message As String
        Public url As String
    End Class

End Class
