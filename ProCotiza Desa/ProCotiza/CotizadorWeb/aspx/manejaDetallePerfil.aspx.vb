'BBV-P-412:AVH:12/07/2016 RQ01: SE CREA DETALLE PERFIL
'BBV-P-412:AVH:27/10/2016 RQ WSC: SE AGREGA WS PARA VALIDAR No. CUENTA 
'BBV-P-412:RQ 10 AVH: 01/11/2016 Cambio de las clases de WS
'BUG-PC-01: AVH: 08-11-2016 MODIFICACION DE MENSAJE DE ERROR
'BBV-P-412:RQ LOGIN GVARGAS: 23/12/2016 Actualizacion REST Service iv_ticket & userID
'BUG-PC-42 JRHM 30/01/17 Se agrega validacion de campo vacio en numero de cuenta no consulte WS
'BUG-PC-44 JRHM 07/02/17 Se agregan las validaciones al numero de cuenta igual que en maneja agencia
Imports System.Data
Imports SNProcotiza

Partial Class aspx_manejaDetallePerfil
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LimpiaError()


        Dim Perfil As Integer

        Dim objUser As New clsUsuariosSistema
        Dim dts As DataSet
        Dim dtsDePerfil As DataSet


        objUser.IDUsuario = Val(Request("idUsu"))
        dts = objUser.ManejaUsuario(1)

        Me.lblIdVendedor.Text = Val(Request("idUsu"))
        Me.lblNombre.Text = dts.Tables(0).Rows(0).Item("NOMBRE").ToString

        If Not IsPostBack Then
            Dim objDetalle As New SNProcotiza.clsDetallePerfil
            objDetalle.CargaSession(Val(Session("cveAcceso")))
            objDetalle.IdUsuario = Val(Request("idUsu"))
            dtsDePerfil = objDetalle.ManejaPerfil(1)

            Dim VALOR As String
            For i As Integer = 0 To dtsDePerfil.Tables(0).Rows.Count - 1
                VALOR = dtsDePerfil.Tables(0).Rows(i)("CAMPO")

                If VALOR = "txtNumeroCuenta" Then
                    Me.txtNumeroCuenta.Text = dtsDePerfil.Tables(0).Rows(i)("VALOR")
                End If
                'If VALOR = "txtNombreJefe" Then
                '    Me.txtNombreJefe.Text = dtsDePerfil.Tables(0).Rows(i)("VALOR")
                'End If
                If VALOR = "txtTitular" Then
                    Me.txtTitular.Text = dtsDePerfil.Tables(0).Rows(i)("VALOR")
                End If

                cbPagoComisiones.Checked = dtsDePerfil.Tables(0).Rows(i)("PAGO_COMISION")
            Next
        End If


        If dts.Tables(0).Rows.Count > 0 Then
            Perfil = dts.Tables(0).Rows(0).Item("ID_PERFIL").ToString
            If Perfil = 74 Then
                'Me.pnlNombreJefe.Visible = True
                Me.pnlNumeroCuenta.Visible = True
                Me.pnlTitutal.Visible = True
            Else
                'Me.pnlNombreJefe.Visible = True
                Me.pnlNumeroCuenta.Visible = False
                Me.pnlTitutal.Visible = False
            End If
        End If
        Valida_Campos()

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

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        lblMensaje.Text = String.Empty
        Dim numCuenta As String
        'Dim nomJefe As String
        Dim titularCuenta As String


        If cbPagoComisiones.Checked = True Then
            If txtTitular.Text = "" Or txtNumeroCuenta.Text = "" Then
                MensajeError("Los campos marcados con * son obligatorios")
                Exit Sub
            End If
        End If

        numCuenta = Me.txtNumeroCuenta.Text
        'nomJefe = Me.txtNombreJefe.Text 
        titularCuenta = Me.txtTitular.Text



        Dim objDetalle As New SNProcotiza.clsDetallePerfil
        objDetalle.CargaSession(Val(Session("cveAcceso")))


        objDetalle.IdUsuario = Val(Request("idUsu"))
        objDetalle.NCuenta = txtNumeroCuenta.Text
        'objDetalle.NombreJefe = txtNombreJefe.Text
        objDetalle.TitularCuenta = txtTitular.Text
        objDetalle.UsuarioRegistro = objDetalle.UserNameAcceso
        objDetalle.PagoComision = IIf(cbPagoComisiones.Checked, 1, 0)

        objDetalle.ManejaPerfil(2)

        If objDetalle.ErrorDetalle = "" Then
            CierraPantalla("./consultaDetallePerfil.aspx")
        Else
            MensajeError(objDetalle.ErrorDetalle)
        End If

    End Sub
    Public Sub CierraPantalla(ByVal strPant As String)
        lblMensaje.Text += "<script>alert('El registro se guardó con éxito'); document.location.href='" & strPant & "';</script>"
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaDetallePerfil.aspx")
    End Sub

    Protected Sub cbPagoComisiones_CheckedChanged(sender As Object, e As System.EventArgs) Handles cbPagoComisiones.CheckedChanged
        Valida_Campos()
    End Sub

    Public Sub Valida_Campos()

        If cbPagoComisiones.Checked = True Then
            Me.btnValidar.Visible = True
            Me.txtNumeroCuenta.Enabled = True
            'Me.txtNombreJefe.Enabled = True
            Me.txtTitular.Enabled = False
        Else
            Me.btnValidar.Visible = False
            Me.txtNumeroCuenta.Enabled = False
            'Me.txtNombreJefe.Enabled = False
            Me.txtTitular.Enabled = False
            Me.txtNumeroCuenta.Text = ""
            Me.txtTitular.Text = ""
        End If
    End Sub

    Public Sub btnValidar_Click(sender As Object, e As System.EventArgs) Handles btnValidar.Click
        If (txtNumeroCuenta.Text <> "") Then
        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
        Me.txtTitular.Text = ""
        LimpiaError()

        Dim rest As WCF.RESTful = New WCF.RESTful()
            rest.buscarHeader("ResponseWarningDescription")

        rest.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("Customer") + "?$filter=(accountNumber==" + Me.txtNumeroCuenta.Text + ")"
        Dim respuesta As String = rest.ConnectionGet(userID, iv_ticket1, String.Empty)

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jresul2 As Customer = serializer.Deserialize(Of Customer)(respuesta)



        Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim alert = srrSerialer.Deserialize(Of msjerr)(respuesta)

        If rest.IsError Then
            If rest.MensajeError <> "" Then
                    MensajeError("Error WS - " & rest.MensajeError)
            Else
                    MensajeError("Error WS - " & alert.message & " Estatus: " & alert.status & "." & "Mensaje:" & rest.MensajeError)
            End If
            Exit Sub
            Else
                If rest.valorHeader <> "" And jresul2.Person.id Is Nothing Then
                    MensajeError("Error WS - " & rest.valorHeader)
        Else
            Me.txtTitular.Text = jresul2.Person.name + " " + jresul2.Person.lastName + " " + jresul2.Person.mothersLastName

                End If
        End If
        Else
            MensajeError("Error: Debes agregar un numero de cuenta antes de validar.")
        End If


    End Sub
    Public Class Customer
        Public Person As Person
    End Class
    Public Class Person
        Public id As String
        Public name As String
        Public lastName As String
        Public mothersLastName As String
    End Class
    Public Class msjerr
        Public message As String
        Public status As String
    End Class
End Class

