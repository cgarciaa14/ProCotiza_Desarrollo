'BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.
'BBVA-P-412: 25-08-2016: AMR: RQCOT-01 Ajustes a la pantalla de Cotización.
'BUG-PC-06: AVH: 14/11/2016 Se agrega opcional en LLenaCombos--%>
'BUG-PC-09:AMR:16/11/2016 CUTARMA01:No pertime espacios en blanco al ingresar el nombre de la marca
'BUG-PC-19:RH:25/11/2016 Correcciones combo giro
'BBV-P-412:BUG-PC-35 18/01/2017 JRHM Se modifica LlenaCombos, agregandole un nuevo parametro para que aparesca opcion vacia en combobox
'BUG-PC-39 JRHM 25/01/17 Correccion de errores multiples
'BUG-PC-48 JRHM 28/02/17 SE AGREGO FUNCIONALIDAD PARA CONTROL DE ENGANCHE CI
'RQ-INB217: RHERNANDEZ: 15/08/17: SE AGREGO LA FUNCIONALIDAD DE VALORES DEFAULT PARA LAS MARCAS EN EL COTIZADOR
'RQ-PI7-PC1:RHERNANDEZ: 12/10/17: Se modifica pantalla cotizador para administrar la nueva cascada de submarca>clasificacion>anio>version
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Net.Mail
Imports SNProcotiza
Imports System.IO


Public Class clsProcGenerales
    Private strErrProc As String = ""
    Private dtsCorridaSeguros As DataSet = New DataSet

    Public ReadOnly Property ErrorProceso() As String
        Get
            Return strErrProc
        End Get
    End Property

    Public Function ObtenInfoParametros(ByVal intPadre As Integer, _
                                        ByRef strErr As String, _
                                        Optional ByVal blnOrdenValor As Boolean = False, _
                                        Optional ByVal strValor As String = "") As DataSet
        ObtenInfoParametros = New DataSet
        strErr = ""
        Try
            Dim objSN As New clsParametros
            objSN.IDPadre = intPadre
            objSN.OrdenarXValor = blnOrdenValor
            objSN.Valor = strValor
            ObtenInfoParametros = objSN.ManejaParametro(1)
            strErr = objSN.ErrorParametros
        Catch ex As Exception
            strErr = ex.Message
        End Try
    End Function

    Public Function ObtenInfoParametrosCot(ByVal Opcion As Integer, _
                                    ByRef strErr As String) As DataSet
        ObtenInfoParametrosCot = New DataSet
        strErr = ""
        Try
            Dim objSN As New clsParametros
            ObtenInfoParametrosCot = objSN.ManejaParametro(Opcion)
            strErr = objSN.ErrorParametros
        Catch ex As Exception
            strErr = ex.Message
        End Try
    End Function

    Public Sub LlenaCombos(ByVal dtsSource As DataSet, _
                           ByVal strCol As String, _
                           ByVal strVal As String, _
                           ByRef objCmb As Object, _
                           ByRef strErr As String, _
                           Optional ByVal blnAgregaBlanco As Boolean = False, _
                           Optional ByVal blnSeleccionaDefault As Boolean = False, _
                           Optional ByVal strDefault As String = "", _
                           Optional ByVal intConsec As Integer = 0)
        Try
            Dim intR As Integer = 0
            Dim intVal As Integer = 0
            Dim objRow As DataRow

            'BUG-PC-09
            objCmb.Items.Clear()



            If blnAgregaBlanco Then
                objCmb.Items.Insert(0, New ListItem("< SELECCIONAR >", intConsec))
                objCmb.AppendDataBoundItems = True
            End If

            If blnSeleccionaDefault Then
                If Trim(strDefault) <> "" Then
                    For Each objRow In dtsSource.Tables(0).Rows
                        If objRow.Item(strDefault) = 1 Then
                            intVal = objRow.Item(strVal)
                            Exit For
                        End If
                    Next
                End If
            End If


            objCmb.DataSource = dtsSource.Tables(0)
            objCmb.DataTextField = strCol
            objCmb.DataValueField = strVal
            objCmb.DataMember = "Hola"
            objCmb.DataBind()

            'If blnAgregaBlanco Then
            '    objCmb.SelectedValue = ""
            'End If

            If blnSeleccionaDefault And intVal > 0 Then
                objCmb.SelectedValue = intVal
            End If

        Catch ex As Exception
            strErr = ex.Message
        End Try
    End Sub

    Public Sub MensajeError(ByVal objWF As Object, _
                            ByVal strErr As String)
        Dim objLbl As Object
        strErr = Replace(strErr, "'", "")
        strErr = strErr.Replace(Chr(10), "\n")
        strErr = strErr.Replace(Chr(13), "")
        objLbl = objWF.FindControl("lblMensaje")

        If IsNothing(objLbl) Then
            objWF.Page.RegisterStartupScript("Mensaje", "<script>alert('" & strErr & "')</script>")
        Else
            objLbl.Text = "<script>alert('" & strErr & "')</script>"
        End If
    End Sub

    Public Function EnviaMail(ByVal strAsunto As String, _
                              ByVal strMailPara As String, _
                              ByVal strCuerpo As String, _
                              Optional ByVal strMailDe As String = "", _
                              Optional ByVal blnEsHTML As Boolean = False) As String
        Try
            EnviaMail = ""

            Dim objMail As New System.Net.Mail.MailMessage()
            Dim objSMTP As New System.Net.Mail.SmtpClient
            Dim strMailUsu As String = ""
            Dim strMailPwd As String = ""
            Dim blnPideCredenciales As Boolean = False

            Dim strMailServer As String = System.Configuration.ConfigurationManager.AppSettings.Item("mailServer")
            Dim strPort As String = System.Configuration.ConfigurationManager.AppSettings.Item("mailPort")
            Dim strPaso() As String = Split(strMailPara, ";")
            Dim intCont As Integer = 0

            If Trim$(strMailDe) = "" Then
                strMailDe = System.Configuration.ConfigurationManager.AppSettings.Item("mailCtaDefault")
            End If

            If Val(System.Configuration.ConfigurationManager.AppSettings.Item("mailCredenciales")) = 1 Then
                blnPideCredenciales = True
            End If

            'construimos el mensaje
            objMail.From = New System.Net.Mail.MailAddress(strMailDe)

            For intCont = 0 To UBound(strPaso, 1)
                objMail.To.Add(strPaso(intCont))
            Next

            objMail.Subject = strAsunto
            objMail.Body = strCuerpo
            objMail.IsBodyHtml = blnEsHTML
            objMail.Priority = System.Net.Mail.MailPriority.Normal

            'enviamos el mail
            objSMTP.Host = strMailServer
            If Val(strPort) > 0 Then
                objSMTP.Port = Val(strPort)
            End If

            If blnPideCredenciales Then
                strMailUsu = System.Configuration.ConfigurationManager.AppSettings.Item("mailUser")
                strMailPwd = System.Configuration.ConfigurationManager.AppSettings.Item("mailPwd")
                objSMTP.Credentials = New System.Net.NetworkCredential(strMailUsu, strMailPwd)
            End If

            objSMTP.Send(objMail)

        Catch ex As Exception
            EnviaMail = ex.Message
        End Try
    End Function

    Public Function AgregaColumna(ByVal intTipo As Integer, ByVal strNom As String) As DataColumn
        AgregaColumna = New DataColumn
        AgregaColumna.ColumnName = strNom
        Select Case intTipo
            Case 1 'entero
                AgregaColumna.DataType = GetType(Integer)
            Case 2 'cadena
                AgregaColumna.DataType = GetType(String)
            Case 3 'fecha
                AgregaColumna.DataType = GetType(Date)
            Case 4 'double
                AgregaColumna.DataType = GetType(Double)
        End Select
    End Function

    Public Sub LlenaObjs(ByRef objeto As Object, ByRef valor As Array)
        Dim dts As New DataSet
        Dim strErr As String = ""
        Dim origenobjeto As String = Path.GetFileName(objeto.Page.AppRelativeVirtualPath.ToString())
        objeto.Items.Clear()

        Select Case objeto.id
            Case "ddltoperacion"
                Dim objTO As New clsTiposOperacion
                objTO.IDPaquete = valor(0)
                dts = objTO.ManejaTipoOperacion(13)

                If objTO.ErrorTipoOperacion = "" Then
                    If dts.Tables.Count > 0 Then
                        If dts.Tables(0).Rows.Count > 0 Then
                            LlenaCombos(dts, "NOMBRE", "ID_TIPO_OPERACION", objeto, strErr)
                        Else
                            objeto.Items.Insert(0, New ListItem("", "0"))
                            objeto.AppendDataBoundItems = True
                        End If
                    End If
                End If

            Case "ddlgiro"
                Dim objGiros As New clsGiros
                objGiros.IDEstatus = valor(0)
                objGiros.TipoOperacion = valor(1)
                dts = objGiros.ManejaGiro(1)

                If objGiros.ErrorGiros = "" Then
                    If dts.Tables.Count > 0 Then
                        If dts.Tables(0).Rows.Count > 0 Then
                            LlenaCombos(dts, "NOMBRE", "ID_GIRO", objeto, strErr)
                        Else
                            objeto.Items.Insert(0, New ListItem("", "0"))
                            objeto.AppendDataBoundItems = True
                        End If
                    End If
                End If

            Case "ddlmarca"
                Dim objMarca As New clsMarcas
                objMarca.IDEstatus = valor(0)
                objMarca.IDAgencia = valor(1)
                dts = objMarca.ManejaMarca(10)

                If strErr = objMarca.ErrorMarcas Then
                    If dts.Tables.Count > 0 Then
                        If dts.Tables(0).Rows.Count > 0 Then
                            LlenaCombos(dts, "NOMBRE", "ID_MARCA", objeto, strErr, False, True, "REG_DEFAULT")
                        Else
                            objeto.Items.Insert(0, New ListItem("", "0"))
                            objeto.AppendDataBoundItems = True
                        End If
                    End If
                End If

            Case "ddlmodelo"
                Dim objProd As New clsProductos
                objProd.IDMarca = valor(0)
                objProd.IDAgencia = valor(1)
                dts = objProd.ManejaProducto(5)

                If objProd.ErrorProducto = "" Then
                    If dts.Tables.Count > 0 Then
                        If dts.Tables(0).Rows.Count() > 0 Then
                            LlenaCombos(dts, "NOMBRE", "ID_SUBMARCA", objeto, strErr)
                        Else
                            objeto.Items.Insert(0, New ListItem("", "0"))
                            objeto.AppendDataBoundItems = True
                        End If
                    End If
                End If

            Case "ddlversion" ''RQ18
                Dim objVer As New clsProductos
                If origenobjeto <> "Cotizador.aspx" Then
                    objVer.IDMarca = valor(0)
                    objVer.IDAgencia = valor(1)
                    objVer.IDSubmarca = valor(2)
                    dts = objVer.ManejaProducto(6)
                Else
                    objVer.IDMarca = valor(0)
                    objVer.IDAgencia = valor(1)
                    objVer.IDSubmarca = valor(2)
                    objVer.IDClasificacion = valor(3)
                    objVer.IDAnio = valor(4)
                    dts = objVer.ManejaProducto(13)
                End If

                If objVer.ErrorProducto = "" Then
                    If dts.Tables.Count > 0 Then
                        If dts.Tables(0).Rows.Count > 0 Then
                            LlenaCombos(dts, "NOMBRE", "ID_VERSION", objeto, strErr)
                        Else
                            objeto.Items.Insert(0, New ListItem("", "0"))
                            objeto.AppendDataBoundItems = True
                        End If
                    End If
                End If
            Case "ddlanio" ''RQ18

                Dim objanio As New clsProductos
                If origenobjeto <> "Cotizador.aspx" Then
                    objanio.IDMarca = valor(0)
                    objanio.IDAgencia = valor(1)
                    objanio.IDSubmarca = valor(2)
                    objanio.IDVersion = valor(3)
                    dts = objanio.ManejaProducto(7)
                Else
                    objanio.IDMarca = valor(0)
                    objanio.IDAgencia = valor(1)
                    objanio.IDSubmarca = valor(2)
                    objanio.IDClasificacion = valor(3)
                    dts = objanio.ManejaProducto(12)
                End If


                If objanio.ErrorProducto = "" Then
                    If dts.Tables.Count > 0 Then
                        If dts.Tables(0).Rows.Count > 0 Then
                            LlenaCombos(dts, "NOMBRE", "ID_ANIO", objeto, strErr)
                        Else
                            objeto.Items.Insert(0, New ListItem("", "0"))
                            objeto.AppendDataBoundItems = True
                        End If
                    End If
                End If

            Case "ddlplan"
                Dim objPaq As New clsPaquetes

                objPaq.IDMoneda = valor(0)
                objPaq.IDProducto = valor(1)
                objPaq.IDAgencia = valor(2)
                objPaq.IDClasificacionProd = valor(3)
                dts = objPaq.ManejaPaquete(38)

                If objPaq.ErrorPaquete = "" Then
                    If dts.Tables.Count > 0 Then
                        If dts.Tables(0).Rows.Count > 0 Then
                            LlenaCombos(dts, "NOMBRE", "ID_PAQUETE", objeto, strErr)
                        Else
                            objeto.Items.Insert(0, New ListItem("", "0"))
                            objeto.AppendDataBoundItems = True
                        End If
                    End If
                End If

            Case "ddlplazos"
                Dim objplazos As New clsPlazo

                objplazos.IDPaquete = valor(0)
                dts = objplazos.ManejaPlazos(5)

                If objplazos.StrErrPlazo = "" Then
                    If dts.Tables.Count > 0 Then
                        If dts.Tables(0).Rows.Count > 0 Then
                            LlenaCombos(dts, "DESCRIPCION", "ID_PLAZO", objeto, strErr)
                        Else
                            objeto.Items.Insert(0, New ListItem("", "0"))
                            objeto.AppendDataBoundItems = True
                        End If
                    End If
                End If

            Case "ddlagencia"
                Dim objAge As New clsAgencias
                objAge.IDEstatus = valor(0)
                objAge.IDEstado = valor(1)
                dts = objAge.ManejaAgencia(1)

                If objAge.ErrorAgencia = "" Then
                    If dts.Tables.Count > 0 Then
                        If dts.Tables(0).Rows.Count > 0 Then
                            LlenaCombos(dts, "NOMBRE", "ID_AGENCIA", objeto, strErr)
                        Else
                            objeto.Items.Insert(0, New ListItem("", "0"))
                            objeto.AppendDataBoundItems = True
                        End If
                    End If
                End If

            Case "ddlejecutivo"
                Dim objUser As New clsUsuariosSistema
                objUser.IDAgencia = valor(0)
                objUser.IDEstado = valor(1)
                dts = objUser.ManejaUsuario(16)

                If objUser.ErrorUsuario = "" Then
                    If dts.Tables.Count > 0 Then
                        If dts.Tables(0).Rows.Count > 0 Then
                            LlenaCombos(dts, "NOMBRE", "ID_USUARIO", objeto, strErr)
                        Else
                            objeto.Items.Insert(0, New ListItem("", "0"))
                            objeto.AppendDataBoundItems = True
                        End If
                    End If
                End If

            Case "ddlvendedor"
                Dim objUser As New clsUsuariosSistema
                objUser.IDAgencia = valor(0)
                objUser.IDEstado = valor(1)
                dts = objUser.ManejaUsuario(15)

                If objUser.ErrorUsuario = "" Then
                    If dts.Tables.Count > 0 Then
                        If dts.Tables(0).Rows.Count > 0 Then
                            LlenaCombos(dts, "NOMBRE", "ID_USUARIO", objeto, strErr)
                        Else
                            objeto.Items.Insert(0, New ListItem("", "0"))
                            objeto.AppendDataBoundItems = True
                        End If
                    End If
                End If

            Case "ddlpj"
                Dim objParam As New clsParametros
                objParam.IDPaquete = valor(0)
                dts = objParam.ManejaParametro(5)

                If objParam.ErrorParametros = "" Then
                    If dts.Tables.Count > 0 Then
                        If dts.Tables(0).Rows.Count > 0 Then
                            LlenaCombos(dts, "TEXTO", "ID_PARAMETRO", objeto, strErr)
                        Else
                            objeto.Items.Insert(0, New ListItem("", "0"))
                            objeto.AppendDataBoundItems = True
                        End If
                    End If
                End If

            Case "ddliva"
                Dim objIva As New clsTasasIVA
                objIva.IDEstatus = valor(0)
                dts = objIva.ManejaTasaIVA(1)

                If objIva.ErrorTasasIVA = "" Then
                    If dts.Tables.Count > 0 Then
                        If dts.Tables(0).Rows.Count > 0 Then
                            LlenaCombos(dts, "NOMBRE", "ID_TASA_IVA", objeto, strErr)
                        Else
                            objeto.Items.Insert(0, New ListItem("", "0"))
                            objeto.AppendDataBoundItems = True
                        End If
                    End If
                End If

            Case "ddlclasif"
                Dim objClasif As New clsProductos
                If origenobjeto <> "Cotizador.aspx" Then
                    objClasif.IDAgencia = valor(0)
                    objClasif.IDMarca = valor(1)
                    objClasif.IDSubmarca = valor(2)
                    objClasif.IDVersion = valor(3)
                    objClasif.IDAnio = valor(4)
                    dts = objClasif.ManejaProducto(9)
                Else
                    objClasif.IDAgencia = valor(0)
                    objClasif.IDMarca = valor(1)
                    objClasif.IDSubmarca = valor(2)
                    dts = objClasif.ManejaProducto(11)
                End If

                If objClasif.ErrorProducto = "" Then
                    If dts.Tables.Count > 0 Then
                        If dts.Tables(0).Rows.Count > 0 Then
                            LlenaCombos(dts, "NOMBRE", "ID_PARAMETRO", objeto, strErr)
                        Else
                            objeto.Items.Insert(0, New ListItem("", "0"))
                            objeto.AppendDataBoundItems = True
                        End If
                    End If
                End If

            Case "ddlmonedafact"
                Dim objMon As New clsMonedas
                objMon.IDEstatus = valor(0)
                dts = objMon.ManejaMoneda(1)
                If objMon.ErrorMoneda = "" Then
                    If dts.Tables.Count > 0 Then
                        If dts.Tables(0).Rows.Count > 0 Then
                            LlenaCombos(dts, "NOMBRE", "ID_MONEDA", objeto, strErr)
                        Else
                            objeto.Items.Insert(0, New ListItem("", "0"))
                            objeto.AppendDataBoundItems = True
                        End If
                    End If
                End If

            Case "ddlmonedafin"
                Dim objMon As New clsMonedas
                objMon.IDMoneda = valor(0)
                dts = objMon.ManejaMoneda(5)
                If objMon.ErrorMoneda = "" Then
                    If dts.Tables.Count > 0 Then
                        If dts.Tables(0).Rows.Count > 0 Then
                            LlenaCombos(dts, "NOMBRE", "ID_MONEDA", objeto, strErr)
                        Else
                            objeto.Items.Insert(0, New ListItem("", "0"))
                            objeto.AppendDataBoundItems = True
                        End If
                    End If
                End If

            Case "ddlalianza"
                Dim objalianza As New clsAlianzas
                objalianza.IDUsuario = valor(0)
                objalianza.IDEdo = valor(1)
                dts = objalianza.ManejaAlianza(10)
                If objalianza.ErrorAlianza = "" Then
                    If dts.Tables.Count > 0 Then
                        If dts.Tables(0).Rows.Count > 0 Then
                            LlenaCombos(dts, "ALIANZA", "ID_ALIANZA", objeto, strErr)
                            objeto.Items.Insert(0, New ListItem("< SELECCIONAR >", "-1"))
                        Else
                            objeto.Items.Insert(0, New ListItem("", "0"))
                            objeto.AppendDataBoundItems = True
                        End If
                    End If
                End If

            Case "ddlgrupo"
                Dim objgrupo As New clsGrupos
                objgrupo.IDUsuario = valor(0)
                objgrupo.IDEdo = valor(1)
                dts = objgrupo.ManejaGrupo(10)
                If objgrupo.ErrorGrupo = "" Then
                    If dts.Tables.Count > 0 Then
                        If dts.Tables(0).Rows.Count > 0 Then
                            LlenaCombos(dts, "GRUPO", "ID_GRUPO", objeto, strErr)
                            objeto.Items.Insert(0, New ListItem("< SELECCIONAR >", "-1"))
                        Else
                            objeto.Items.Insert(0, New ListItem("", "0"))
                            objeto.AppendDataBoundItems = True
                        End If
                    End If
                End If

            Case "ddldivision"
                Dim objdiv As New clsDivisiones
                objdiv.IDUsuario = valor(0)
                objdiv.IDEdo = valor(1)
                dts = objdiv.ManejaDivision(10)
                If objdiv.ErrorDivision = "" Then
                    If dts.Tables.Count > 0 Then
                        If dts.Tables(0).Rows.Count > 0 Then
                            LlenaCombos(dts, "DIVISION", "ID_DIVISION", objeto, strErr)
                            objeto.Items.Insert(0, New ListItem("< SELECCIONAR >", "-1"))
                        Else
                            objeto.Items.Insert(0, New ListItem("", "0"))
                            objeto.AppendDataBoundItems = True
                        End If
                    End If
                End If
            Case "ddlengancheCI"
                Dim objver As New clsVersiones
                objver.IDVersion = valor(0)
                objver.Plazo = valor(1)
                dts = objver.ManejaVersion(8)
                If objver.ErrorVersion = "" Then
                    If dts.Tables.Count > 0 Then
                        If dts.Tables(0).Rows.Count > 0 Then
                            LlenaCombos(dts, "PORC_ENGANCHE", "ID", objeto, strErr)
                            If strErr <> "" Then
                                Exit Sub
                            End If
                        Else
                            objeto.Items.Insert(0, New ListItem("", "0"))
                            objeto.AppendDataBoundItems = True
                        End If
                    End If
                End If
        End Select
    End Sub
End Class
