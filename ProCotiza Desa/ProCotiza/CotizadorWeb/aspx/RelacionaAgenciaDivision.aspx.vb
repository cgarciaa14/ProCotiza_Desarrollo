'BBV-P-412:AVH:06/07/2016 RQ19: SE CREA RELACION DE DIVISIONES
'BBV-P-412:BUG-PC-19 24/11/2016 SE CAMBIO LA FORMA COMO SE RELACIONAN LAS AGENCIAS CON LAS DIVISIONES.
'BUG-PC-24 MAUT 08/12/2016 Se cambia dataset de la sesion, no abrir divisiones inactivas
'BUG-PC-34 MAUT 12/01/2017 Se agregan cambios faltantes del BUG-PC-24
'BUG-PC-39 25/01/17 Se agrego metodos para boton limpiar
'BUG-PC-44 07/02/17 JRHM SE Se agrega funcion buscar para corregir detalles al asignar agencia
'BUG-PC-44 07/02/17 JRHM SE Se agrega funcion buscar para corregir detalles al asignar agencia
'BUG-PC-74:MPUESTO:06/06/2017:ATENCION DE LOS SIGUIENTES PUNTOS:
'                               + SEGMENTACION DE CLASES PARA INVOCAR WEB SERVICE DE INSERCION Y ACTUALIZACION DE AGENCIAS
'                               + ACTUALIZACION DE RELACION AGENCIA <--> DIVISION POR SERVICIO WEB 
'RQ-PI7-PC9: CGARCIA: 23/11/2017: SE AGREGO OPCION 12 PARA TRAER EL id_EXTERNO PARA EL WS DE AGENCIAS

Imports System.Data
Imports SNProcotiza
Imports WCF
Imports WCF.clsAgenciasWS

Partial Class aspx_RelacionaAgenciaDivision
    Inherits System.Web.UI.Page
    Dim strErr As String = String.Empty
    Dim objPaq As New clsPaquetes
    Dim objAgencias As New clsAgencias
    Dim ID_DIVISION As Integer

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(Val(Session("cveAcceso")), Val(Session("cveUsu")))

        If objUsuFirma.ErrorUsuario = "Sesion Terminada" Then
            Response.Redirect("../login.aspx", True)
        End If

        LimpiaError()
        ID_DIVISION = Val(Request("idDivision"))

        Dim dts As New DataSet
        Dim objDivision As New SNProcotiza.clsDivisiones

        objDivision.IDDivision = Val(Request("idDivision"))
        dts = objDivision.ManejaDivision(1)
        'BUG-PC-24 08/12/2016 MAUT No se deben asignar agencias a divisiones inactivas
        If dts.Tables(0).Rows(0).Item("ESTATUS") = 3 Then
            lblMensaje.Text += "<script>alert('No se pueden asignar Agencias a Divisiones inactivas.'); window.close();</script>"
        End If

        lblTitulo.Text = "RELACIONA AGENCIA - DIVISION (" & (dts.Tables(0).Rows(0).Item("DIVISION")).ToString & ")"


        If Not IsPostBack Then
            CargaCombos(1)
            CargaCombos(2)
            'Se cambia el select fuera del cargacombo
            Me.cmbDivision.SelectedValue = Val(Request("idDivision"))
        End If


    End Sub

    Private Sub CargaInfo()
        Try
            Dim dts As New DataSet
            Dim objDivision As New SNProcotiza.clsDivisiones
            objDivision.IDDivision = Val(Request("idDivision"))

            dts = objDivision.ManejaDivision(1)

        Catch ex As Exception
            ' MensajeError(ex.Message)
        End Try
    End Sub

    Public Sub LimpiaError()
        lblMensaje.Text = String.Empty
        lblScript.Text = String.Empty
    End Sub

    Public Sub MensajeError(ByVal strMsj As String)
        strMsj = Replace(strMsj, "'", "")
        strMsj = Replace(strMsj, """", "")
        strMsj = Replace(strMsj, "(", "")
        strMsj = Replace(strMsj, ")", "")
        lblMensaje.Text = "<script>alert('" & strMsj & "')</script>"
    End Sub

    Private Function CargaCombos(ByVal intOpc As Integer) As Boolean

        Dim objDivisiones As New clsDivisiones
        Dim objCombo As New clsProcGenerales
        Dim dtsRes As New DataSet

        Try
            CargaCombos = False

            Select Case intOpc
                Case 1

                    'BUG-PC-24 MAUT 08/12/2016 Se cambia objAgencias por objDivisiones
                    objDivisiones.IDEstatus = 2
                    dtsRes = objDivisiones.ManejaDivision(1)
                    strErr = objDivisiones.ErrorDivision
                    If Trim$(strErr) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                'BUG-PC-24 08/12/2016 MAUT Se agrega opcion Seleccionar
                                objCombo.LlenaCombos(dtsRes, "DIVISION", "ID_DIVISION", cmbDivision, strErr, True, , , -1)
                                cmbDivision.SelectedValue = dtsRes.Tables(0).Rows(0).Item("ID_DIVISION").ToString
                                If Trim$(strErr) <> "" Then
                                    MensajeError(strErr)
                                    Exit Function
                                End If
                            End If
                        End If
                    Else
                        MensajeError(strErr)
                        Exit Function
                    End If

                Case 2
                    'If cmbGrupo.SelectedValue <> 0 Then
                    'objDivisiones.IDAgencia = cmbGrupo.SelectedValue

                    'End If

                    objDivisiones.IDEstatus = 2
                    objDivisiones.IDDivision = Val(Request("idDivision"))
                    'BUG-PC-24 08/12/2016 MAUT Se agrega filtro para la division
                    objDivisiones.IDDivisionFiltro = Val(Request("idDivision"))


                    dtsRes = objDivisiones.ManejaDivision(7)
                    strErr = objDivisiones.ErrorDivision
                    If Trim$(strErr) = "" Then
                        If dtsRes.Tables.Count > 0 Then
                            If dtsRes.Tables(0).Rows.Count > 0 Then
                                'BUG-PC-24 MAUT 08/12/2016 Se cambia dataset de la sesion
                                Session("dtsConsultaPaqAge") = dtsRes
                                grvConsulta.DataSource = dtsRes
                                grvConsulta.DataBind()
                            Else
                                'BUG-PC-24 MAUT Si no se encuentran registros, se limpia
                                grvConsulta.DataSource = Nothing
                                grvConsulta.DataBind()
                            End If
                        End If
                    Else
                        MensajeError(strErr)
                    End If

            End Select

            CargaCombos = True
        Catch ex As Exception
            CargaCombos = False
            MensajeError(ex.Message)
        End Try
    End Function

    Protected Sub grvConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        grvConsulta.PageIndex = e.NewPageIndex
        grvConsulta.DataSource = CType(Session("dtsConsultaPaqAge"), DataSet)
        grvConsulta.DataBind()
    End Sub

    Protected Sub grvConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        If e.CommandName = "ID_DIVISION" Then
            Dim objTO As New clsDivisiones
            Dim intConsul As Integer = 7
            Dim intBorra As Integer = 8
            Dim intInserta As Integer = 9
            Dim dtsRes As New DataSet
            Dim objRow As GridViewRow = grvConsulta.Rows(Val(e.CommandArgument))
            Dim objImg As ImageButton = objRow.Cells(3).Controls(1)
            Dim idAgencia As String = grvConsulta.DataKeys(Convert.ToInt32(e.CommandArgument)).Values(0).ToString()

            'BUG-PC-74:MPUESTO:06/06/2017:INICIO
            Dim _clsAgenciasWS As New clsAgenciasWS()
            Dim _messageError As String = String.Empty
            Dim _result As Boolean = False

            'RQ-PI7-PC9: CGARCIA: 23/11/2017: SE AGREGO OPCION 12 PARA TRAER EL id_EXTERNO PARA EL WS DE AGENCIAS
            Dim dtsDiv As New DataSet
            Dim ID_EXTERNO As String

            objTO.IDDivision = ID_DIVISION
            dtsDiv = objTO.ManejaDivision(12)

            If (Not IsNothing(dtsDiv) AndAlso dtsDiv.Tables.Count > 0 AndAlso dtsDiv.Tables(0).Rows.Count > 0) Then
                ID_EXTERNO = dtsDiv.Tables(0).Rows(0).Item("ID_EXTERNO").ToString
            Else
                ID_EXTERNO = String.Empty
            End If

            _clsAgenciasWS.LoadAgencyFromDB(idAgencia, ID_EXTERNO) 'ID_DIVISION
            'BUG-PC-74:MPUESTO:06/06/2017:FIN

            objTO.IDDivision = ID_DIVISION
            objTO.IDAgencia = Val(idAgencia)
            objTO.UsuarioRegistro = objTO.UserNameAcceso
            dtsRes = objTO.ManejaDivision(intConsul)

            If dtsRes.Tables(0).Rows.Count > 0 Then
                If dtsRes.Tables(0).Rows(0).Item("ID_DIVISION") = 0 Then
                    objTO.ManejaDivision(intInserta)
                    objImg.ImageUrl = "../img/tick.png"
                Else
                    objTO.ManejaDivision(intBorra)
                    objImg.ImageUrl = "../img/cross.png"
                End If
            Else
                objTO.ManejaDivision(intInserta)
                objImg.ImageUrl = "../img/tick.png"
            End If

            'BUG-PC-74:MPUESTO:06/06/2017:INICIO
            _result = New clsAgenciasWS().InvokeModifyAgencyWS(_clsAgenciasWS._Agency, _messageError)
            If Not _result Then
                MensajeError("Error WS - " & _messageError)
            Else
                LimpiaError()
            End If
            'BUG-PC-74:MPUESTO:06/06/2017:FIN

            'Se cambia el select fuera del cargacombo
            Me.cmbDivision.SelectedValue = Val(Request("idDivision"))
        End If
        CargaCombos(2)

    End Sub

    Protected Sub grvConsulta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvConsulta.RowDataBound
        Dim _objImg As New ImageButton

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim datakey As String = Convert.ToString(grvConsulta.DataKeys(e.Row.RowIndex).Values(2).ToString)

            _objImg = e.Row.FindControl("ImageButton1")
            If datakey <> 0 Then
                _objImg.ImageUrl = "../img/tick.png"
            Else : datakey = 0
                _objImg.ImageUrl = "../img/cross.png"
            End If
        End If
    End Sub

    Protected Sub cmbDivision_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbDivision.SelectedIndexChanged
        Dim objDivisiones As New clsDivisiones
        Dim dtsRes As New DataSet

        If cmbDivision.SelectedValue <> -1 Then
            objDivisiones.IDDivisionFiltro = cmbDivision.SelectedValue
        End If

        If txtNom.Text <> "" Then
            objDivisiones.Agencia = txtNom.Text
        End If

        'objAlianzas.IDAgencia = cmbAgencia.SelectedValue
        objDivisiones.IDDivision = Val(Request("idDivision"))

        dtsRes = objDivisiones.ManejaDivision(7)
        strErr = objDivisiones.ErrorDivision

        If Trim$(strErr) = "" Then
            If dtsRes.Tables.Count > 0 Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    'BUG-PC-24 MAUT 08/12/2016 Se cambia dataset de la sesion
                    Session("dtsConsultaPaqAge") = dtsRes

                    grvConsulta.DataSource = dtsRes
                    grvConsulta.DataBind()
                Else
                    grvConsulta.DataSource = Nothing
                    grvConsulta.DataBind()
                End If
            End If


        Else
            MensajeError(strErr)
        End If
    End Sub

    Protected Sub bntBuscaProd_Click(sender As Object, e As System.EventArgs) Handles bntBuscaProd.Click
        Buscar()
    End Sub

    Protected Sub btnTodas_Click(sender As Object, e As System.EventArgs) Handles btnTodas.Click
        Dim objDivisiones As New clsDivisiones
        Dim intInserta As Integer = 9
        'BUG-PC-74:MPUESTO:06/06/2017:INICIO
        Dim _result As Boolean = True
        Dim ErrorMessage As String = String.Empty
        'BUG-PC-74:MPUESTO:06/06/2017:FIN
        If cmbDivision.SelectedValue <> -1 Then
            objDivisiones.IDDivisionFiltro = cmbDivision.SelectedValue
        End If
        If txtNom.Text <> "" Then
            objDivisiones.Agencia = txtNom.Text
        End If
        objDivisiones.IDDivision = Val(Request("idDivision"))
        objDivisiones.ManejaDivision(intInserta)
        'BUG-PC-74:MPUESTO:06/06/2017:INICIO
        _result = UpdateAllAgencies(UpdateAction.All, ErrorMessage)
        If Not _result Then
            MensajeError("Error WS - " & ErrorMessage)
        Else
            LimpiaError()
        End If
        'BUG-PC-74:MPUESTO:06/06/2017:FIN
        CargaCombos(2)
    End Sub

    Protected Sub btnNinguna_Click(sender As Object, e As System.EventArgs) Handles btnNinguna.Click
        Dim objDivisiones As New clsDivisiones
        Dim intBorra As Integer = 8
        'BUG-PC-74:MPUESTO:06/06/2017:INICIO
        Dim _result As Boolean = True
        Dim ErrorMessage As String = String.Empty
        'BUG-PC-74:MPUESTO:06/06/2017:INICIO
        If cmbDivision.SelectedValue <> 0 Then
            objDivisiones.IDDivisionFiltro = cmbDivision.SelectedValue
        End If
        If txtNom.Text <> "" Then
            objDivisiones.Agencia = txtNom.Text
        End If
        objDivisiones.IDDivision = Val(Request("idDivision"))
        objDivisiones.ManejaDivision(intBorra)
        'BUG-PC-74:MPUESTO:06/06/2017:INICIO
        _result = UpdateAllAgencies(UpdateAction.None, ErrorMessage)
        If Not _result Then
            MensajeError("Error WS - " & ErrorMessage)
        Else
            LimpiaError()
        End If
        'BUG-PC-74:MPUESTO:06/06/2017:FIN
        Buscar()
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        Limpiar()
        Buscar()
    End Sub

    Public Sub Limpiar()
        txtNom.Text = ""
        Me.cmbDivision.SelectedValue = -1
    End Sub

    Public Sub Buscar()
        Dim objDivisiones As New clsDivisiones
        Dim dtsRes As New DataSet

        If cmbDivision.SelectedValue <> 0 Then
            objDivisiones.IDDivisionFiltro = cmbDivision.SelectedValue
        End If

        objDivisiones.IDDivision = Val(Request("idDivision"))
        objDivisiones.Agencia = txtNom.Text
        dtsRes = objDivisiones.ManejaDivision(7)
        strErr = objDivisiones.ErrorDivision

        If Trim$(strErr) = "" Then
            If dtsRes.Tables.Count > 0 Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    'BUG-PC-24 MAUT 08/12/2016 Se cambia dataset de la sesion
                    Session("dtsConsultaPaqAge") = dtsRes
                    grvConsulta.DataSource = dtsRes
                    grvConsulta.DataBind()
                Else
                    grvConsulta.DataSource = Nothing
                    grvConsulta.DataBind()
                End If
            End If
        Else
            MensajeError(strErr)
        End If
    End Sub

    Private Function UpdateAllAgencies(ByVal actionToDo As UpdateAction, ByRef _MessageResult As String) As Boolean
        Dim _result = True
        Dim _clsAgenciasWS As New clsAgenciasWS()
        Dim _ErrorMessage As String = String.Empty
        Dim _resultAll As Boolean = True
        Dim dtsRes As New DataSet

        dtsRes = CType(Session("dtsConsultaPaqAge"), DataSet)
        If Not dtsRes Is Nothing Then
            If dtsRes.Tables.Count > 0 Then
                If dtsRes.Tables(0).Rows.Count Then
                    For Each item As DataRow In dtsRes.Tables(0).Rows
                        _clsAgenciasWS.LoadAgencyFromDB(Convert.ToInt32(item("ID_AGENCIA")))
                        _clsAgenciasWS._Agency.divisionId = IIf(actionToDo = UpdateAction.None, 0, Convert.ToInt32(Val(Request("idDivision"))))
                        _result = _clsAgenciasWS.InvokeModifyAgencyWS(_clsAgenciasWS._Agency, _ErrorMessage)
                        If _result = False Then
                            _resultAll = _result
                            _MessageResult = _ErrorMessage
                        End If
                    Next
                End If
            End If
        End If
        Return _resultAll
    End Function

    Public Enum UpdateAction
        None
        All
    End Enum
End Class

