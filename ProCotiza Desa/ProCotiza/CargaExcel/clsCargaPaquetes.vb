'BBVA-P-412: 08/08/2016: AMR: RQ09 – Carga Masiva de Plan de Financiamiento
'BBVA-P-412 RQ06: AMR: 20/09/2016 Administración de Planes de Financiamiento: Promociones
'BBVA-P-412 RQ WSA. WS GetLoan Products (RQ06, RQ09): AMR: 25/10/2016 WS GetLoan Products
'BUG-PC-29 AMR 02/01/2017: Al seleccionar cualquier version fuera de la que muestra por default manda una pantalla de error.
'BBV-P-412:RQ LOGIN GVARGAS: 02/01/2017 Actualizacion REST Service iv_ticket & userID
'BBVA-P-412 18/04/2017: CGARCIA: RQTARESQ-06  SE AGREGA LA OPCION DE ESQUEMA A LA CONSULTA, INSERCION, Y VALIDACION
'BUG-PC-53 AMR 21/04/2017 Correcciones Multicotiza.
'BUG-PC-64: RHERNANDEZ: 18/05/17: Se cambio packageId de payload para nueve digitos con ceros a la izquierda

Imports System.IO
Imports Excel
Imports System.Data
Imports System.Data.SqlClient
Imports SDManejaBD
Imports SNProcotiza
Imports System.Text
Imports System.Configuration
Imports System.Threading.Thread
Imports WCF
Imports Newtonsoft.Json





Public Class clsCargaPaquetes

    Private strErrorCargaPaquetes As String = String.Empty
    Public objpaq As New clsPaquetes
    Public paqid As Integer = 0
    Public strtbname As String = String.Empty
    Public intTotErrors As Integer = 0
    Public intTotSuccess As Integer = 0
    Public intcveAcceso As Integer = 0
    Public intcveUsu As Integer = 0
    Public objPlazos As New clsPlazo

    Public ReadOnly Property ErrorCargaPaquetes As String
        Get
            Return strErrorCargaPaquetes
        End Get
    End Property

    Public Property CveAcceso As Integer
        Get
            Return intcveAcceso
        End Get
        Set(value As Integer)
            intcveAcceso = value
        End Set
    End Property

    Public Property CveUsu As Integer
        Get
            Return intcveUsu
        End Get
        Set(value As Integer)
            intcveUsu = value
        End Set
    End Property

    Public Function Read(Optional ByVal excelFileName As String = "") As DataSet

        If excelFileName = "" Then
            excelFileName = System.Configuration.ConfigurationManager.AppSettings.Item("Repositorio") & _
                System.Configuration.ConfigurationManager.AppSettings.Item("FileName")
        End If

        Dim objUsuFirma As New SNProcotiza.clsUsuariosSistema(CveAcceso, CveUsu)

        strErrorCargaPaquetes = String.Empty
        Read = New DataSet
        Dim dtstab As dsCargaPaquetes = New dsCargaPaquetes

        Try
            Dim stream As FileStream = File.Open(excelFileName, FileMode.Open, FileAccess.Read)

            Dim excelReader As IExcelDataReader
            Dim strExtension As String = System.IO.Path.GetExtension(excelFileName)
            '1. Reading from a binary Excel file ('97-2003 format; *.xls)
            If UCase(Trim$(strExtension)) = ".XLS" Then
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream)
            End If
            '...
            '2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
            If UCase(Trim$(strExtension)) = ".XLSX" Then
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
            End If
            '...
            '3. DataSet - The result of each spreadsheet will be created in the result.Tables
            excelReader.IsFirstRowAsColumnNames = True
            Read = excelReader.AsDataSet()
            '...
            '4. Free resources (IExcelDataReader is IDisposable)
            excelReader.Close()
            stream.Close()

            ''Validamos que el archivo cargado este correcto(total de pestañas y tipos de datos)
            If ValidaDts(Read, dtstab) Then
            Else
                intTotErrors += 1
                Exit Function
            End If


            Dim idpaq As Integer = 0

            For j As Integer = 0 To dtstab.Tables(0).Rows.Count - 1
                idpaq = dtstab.Tables(0).Rows(j).Item("ID_PAQUETE")

                ''Validamos que todos los registros de la pestaña PAQUETES existan en las demás.
                If ValidaRegistros(idpaq, dtstab) Then

                    ''Validamos la integridad de los datos.
                    If ValidaDatos(idpaq, dtstab) Then
                        InsertaPaquete(idpaq, dtstab)
                    Else
                        intTotErrors += 1
                    End If
                Else
                    intTotErrors += 1
                End If
            Next


        Catch ex As Exception
            intTotErrors += 1
            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error. " & ex.Message
            log(strErrorCargaPaquetes)
            Exit Function
        End Try
    End Function

    Public Function ValidaDts(ByVal ds As DataSet, ByRef dts As dsCargaPaquetes) As Boolean

        ValidaDts = False

        Dim tablas(0 To 7) As String
        Dim dtTabla As DataTable
        Dim renglones() As DataRow
        Dim renglon As DataRow

        tablas(0) = "PAQUETES"
        tablas(1) = "PERSONALIDAD"
        tablas(2) = "SEGUROSPROD"
        tablas(3) = "SEGUROSVIDA"
        tablas(4) = "CONDICIONES"
        tablas(5) = "AGENCIAS"
        tablas(6) = "PRODUCTOS"
        tablas(7) = "ASEGURADORAS"

        Try
            For i As Integer = 0 To 7
                strtbname = ds.Tables(i).TableName

                If strtbname <> tablas(i) Then
                    strErrorCargaPaquetes = "Falta información de " & tablas(i) & "."
                    log(strErrorCargaPaquetes)
                    Exit Function
                End If

                dtTabla = ds.Tables(i)
                renglones = dtTabla.Select("ISNULL(ID_PAQUETE,0) > 0")
                For Each renglon In renglones
                    dts.Tables(ds.Tables(i).TableName).ImportRow(renglon)
                Next
            Next
            ValidaDts = True

        Catch ex As Exception
            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error en: " & strtbname & ". " & ex.Message
            log(strErrorCargaPaquetes)
        End Try
    End Function

    Public Function ValidaRegistros(intPaqId As Integer, ds As DataSet) As Boolean
        ValidaRegistros = False
        Dim TotErr As Integer
        Dim renglones() As DataRow
        Dim strsql As String = "ID_PAQUETE = " & intPaqId
        Dim dt As DataTable

        Try
            For i As Integer = 1 To 7
                dt = ds.Tables(i)
                TotErr = 0
                For Each r As DataRow In dt.Rows
                    renglones = dt.Select(strsql)
                    If renglones.Length = 0 Then
                        strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error. " & "No existe información del ID_PAQUETE " & intPaqId & " en " & dt.TableName & "."
                        TotErr += 1
                    End If
                Next
                If TotErr > 0 Then log(strErrorCargaPaquetes)
            Next

            If TotErr = 0 Then
                ValidaRegistros = True
            End If

        Catch ex As Exception
            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error. " & ex.Message
        End Try
    End Function

    Public Function ValidaDatos(ByVal intPaqId As Integer, ByVal ds As DataSet) As Boolean
        Dim dtsfields As New DataSet
        ''Dim row As DataRow
        Dim strsql As String = "ID_PAQUETE = " & intPaqId


        ValidaDatos = False

        For Each dt As DataTable In ds.Tables
            For Each row As DataRow In dt.Select(strsql)

                Select Case dt.TableName
                    Case "PAQUETES"
                        ''Validamos el nombre del plan
                        objpaq.Nombre = row("NOMBRE")
                        dtsfields = objpaq.ManejaPaquete(1)
                        If objpaq.ErrorPaquete = "" Then
                            If dtsfields.Tables(0).Rows.Count > 0 Then
                                If row("NOMBRE") = dtsfields.Tables(0).Rows(0).Item("NOMBRE") Then
                                    strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El paquete " & row("NOMBRE") & " ya existe."
                                    log(strErrorCargaPaquetes)
                                    Exit Function
                                End If
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objpaq.ErrorPaquete
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Validamos el Tipo de Operación
                        Dim objTipoOper As New clsTiposOperacion(row("ID_TIPO_OPERACION"))
                        If objTipoOper.ErrorTipoOperacion = "" Then
                            If objTipoOper.IDTipoOperacion = 0 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El Tipo de Operación no Existe."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            Else
                                If objTipoOper.IDEstatus <> 2 Then
                                    strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El Tipo de Operación no se encuentra activo."
                                    log(strErrorCargaPaquetes)
                                    Exit Function
                                End If
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objTipoOper.ErrorTipoOperacion
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Validamos el tipo de Moneda
                        Dim objMoneda As New clsMonedas(row("ID_MONEDA"))
                        If objMoneda.ErrorMoneda = "" Then
                            If objMoneda.IDMoneda = 0 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El tipo de Moneda no Existe."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            Else
                                If objMoneda.IDEstatus <> 2 Then
                                    strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE" & row("ID_PAQUETE") & ". El tipo de Moneda no se encuentra activo."
                                    log(strErrorCargaPaquetes)
                                    Exit Function
                                End If
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objMoneda.ErrorMoneda
                            Exit Function
                        End If

                        ''Validamos el tipo de Clasificación.
                        Dim objclasif As New clsParametros(row("ID_CLASIFICACION_PROD"))
                        If objclasif.ErrorParametros = "" Then
                            If objclasif.IDPadre <> 62 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El tipo de Clasificación del Producto es incorrecto."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objclasif.ErrorParametros
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Validamos el tipo de Calendario
                        Dim objcalendario As New clsParametros(row("ID_CALENDARIO"))
                        If objcalendario.ErrorParametros = "" Then
                            If objcalendario.IDPadre <> 26 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El tipo de Calendario es incorrecto."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE" & row("ID_PAQUETE") & ". " & objcalendario.ErrorParametros
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Validamos el tipo de Periodicidad
                        Dim objperiodicidad As New clsParametros(row("ID_PERIODICIDAD"))
                        If objperiodicidad.ErrorParametros = "" Then
                            If objperiodicidad.IDPadre <> 82 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE" & row("ID_PAQUETE") & ". El tipo de Periodicidad es incorrecto."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objperiodicidad.ErrorParametros
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Validamos el tipo de Calcúlo
                        Dim objtipocal As New clsParametros(row("ID_TIPO_CALCULO"))
                        If objtipocal.ErrorParametros = "" Then
                            If objtipocal.IDPadre <> 22 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El tipo de Calculo es incorrecto."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objtipocal.ErrorParametros
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Primer Pago Irregular
                        If row("PRIMER_PAGO_IRREG") > 1 Or row("PRIMER_PAGO_IRREG") < 0 Then
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El valor ingresado de PRIMER_PAGO_IRREG es incorrecto."
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Permite Monto Subsidio
                        If row("PERMITE_MONTO_SUBSIDIO") > 1 Or row("PERMITE_MONTO_SUBSIDIO") < 0 Then
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El valor ingresado de PERMITE_MONTO_SUBSIDIO es incorrecto."
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Permite Porcentaje Subsidio
                        If row("PERMITE_PTJ_SUBSIDIO") > 1 Or row("PERMITE_PTJ_SUBSIDIO") < 0 Then
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El valor ingresado de PERMITE_PTJ_SUBSIDIO es incorrecto."
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Validamos el tipo de Calcúlo Seguro
                        Dim objtipocalseg As New clsParametros(row("ID_TIPO_CALCULO_SEGURO"))
                        If objtipocalseg.ErrorParametros = "" Then
                            If objtipocalseg.IDPadre <> 86 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El tipo de Calculo del Seguro es incorrecto."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objtipocalseg.ErrorParametros
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Permite Pagos Especiales
                        If row("PERMITE_PAGOS_ESPECIALES") > 1 Or row("PERMITE_PAGOS_ESPECIALES") < 0 Then
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & " .El valor ingresado de PERMITE_PAGOS_ESPECIALES es incorrecto."
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Permite Gracia Capital
                        If row("PERMITE_GRACIA_CAPITAL") > 1 Or row("PERMITE_GRACIA_CAPITAL") < 0 Then
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El valor ingresado de PERMITE_GRACIA_CAPITAL es incorrecto."
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Permite Gracia Interes
                        If row("PERMITE_GRACIA_INTERES") > 1 Or row("PERMITE_GRACIA_INTERES") < 0 Then
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El valor ingresado de PERMITE_GRACIA_CAPITAL es incorrecto."
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Validamos el tipo Seguro de Vida(APLICACIÓN DE SEGURO DE VIDA)
                        Dim objtipocalmanual As New clsParametros(row("ID_TIPO_SEGURO_VIDA"))
                        If objtipocalmanual.ErrorParametros = "" Then
                            If objtipocalmanual.IDPadre <> 49 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El tipo Aplicación de Seguro de Vida es incorrecto."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objtipocalmanual.ErrorParametros
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Validamos el tipo de Vencimiento
                        Dim objtipovencimiento As New clsParametros(row("ID_TIPO_VENCIMIENTO"))
                        If objtipovencimiento.ErrorParametros = "" Then
                            If objtipovencimiento.IDPadre <> 100 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El tipo Vencimiento es incorrecto."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objtipovencimiento.ErrorParametros
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Calcular IVA Seguro de Vida
                        If row("CALCULAR_IVA_SEGURO_VIDA") > 1 Or row("CALCULAR_IVA_SEGURO_VIDA") < 0 Then
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El valor ingresado de CALCULAR_IVA_SEGURO_VIDA es incorrecto."
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Inicio Vigencia
                        Dim fecha As DateTime

                        If Not DateTime.TryParse(row("INI_VIG"), fecha) Then
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El valor ingresado para el campo INI_VIG es invalido."
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Fin Vigencia
                        If Not DateTime.TryParse(row("FIN_VIG"), fecha) Then
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El valor ingresado para el campo FIN_VIG es invalido."
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        If row("FIN_VIG") < row("INI_VIG") Then
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_APQUETE " & row("ID_PAQUETE") & ". La fecha ingresada en el campo FIN_VIG no puede ser menor a la ingresada en el campo INI_VIG."
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Validamos la Promoción(Seguros Gratis/Regalado)
                        Dim objPromo As New clsPromocionesCte(row("ID_PROMOCION"))
                        If objPromo.ErrorPromociono = "" Then
                            If objPromo.IDESTATUS <> 2 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". La promoción ingresada no se encuentra activa."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objPromo.ErrorPromociono
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Número de Periodos
                        Dim objplazos As New clsPlazo
                        objplazos.Valor = (row("NOPERIODOS"))
                        objplazos.Id_Periodicidad = row("ID_PERIODICIDAD")
                        dtsfields = objplazos.ManejaPlazos(1)
                        If objplazos.StrErrPlazo = "" Then

                            If dtsfields.Tables(0).Rows.Count > 0 Then
                                If dtsfields.Tables(0).Rows(0).Item("ESTATUS") <> 2 Then
                                    strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El periodo ingresado no se encuentra activo."
                                    log(strErrorCargaPaquetes)
                                    Exit Function
                                End If

                                If dtsfields.Tables(0).Rows(0).Item("ID_PERIODICIDAD") <> row("ID_PERIODICIDAD") Then
                                    strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El periodo ingresado no corresponde con el tipo de Periodicidad."
                                    log(strErrorCargaPaquetes)
                                    Exit Function
                                End If
                            Else
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". No se encontro información de los plazos."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objplazos.StrErrPlazo
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        If row("ID_PROMOCION") = 1 And row("NOPERIODOS") > 0 Then
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". Si el plan no considera promociones el valor ingresado en el campo NOPERIODOS es incorrecto."
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Permite Seguro Regalado
                        If row("ID_PROMOCION") = 1 And row("PERMITE_SEGURO_REGALADO") <> 0 Then
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". Si el plan no considera promociones valor ingresado de PERMITE_SEGURO_REGALADO es incorrecto."
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        If row("PERMITE_SEGURO_REGALADO") > 1 Or row("PERMITE_SEGURO_REGALADO") < 0 Then
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El valor ingresado de PERMITE_SEGURO_REGALADO es incorrecto."
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        ''Validamos los pagos especiales
                        If Val(row("PTJ_PAGOS_ESPECIALES")) < 2 Or Val(row("PTJ_PAGOS_ESPECIALES")) > 2.5 Then
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El valor ingresado de PTJ_PAGOS_ESPECIALES es incorrecto."
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        If row("ID_TIPO_CALCULO") = 167 And Val(row("PTJ_PAGOS_ESPECIALES")) <> 2.5 Then
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El valor ingresado de PTJ_PAGOS_ESPECIALES es incorrecto."
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        If row("ID_TIPO_CALCULO") <> 167 And Val(row("PTJ_PAGOS_ESPECIALES")) <> 2 Then
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El valor ingresado de PTJ_PAGOS_ESPECIALES es incorrecto."
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        'Validamos el tipo de ProductoUG
                        Dim objprodUG As New clsProductosUG(row("ID_PRODUCTO_UG"))
                        If objprodUG.ErrorProductoUG = "" Then
                            If objprodUG.IDEstatus <> 2 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El tipo de ProductoUG ingresado no se encuentra activo."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                            If objprodUG.IDProductoUG <> 96 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El tipo de ProductoUG ingresado no es valido."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objprodUG.ErrorProductoUG
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        'Validamos el tipo de SubProductoUG
                        Dim objSubprodUG As New clsSubProductosUG

                        objSubprodUG.Nombre = row("ID_SUBPRODUCTO_UG")
                        dtsfields = objSubprodUG.ManejaSubProductoUG(1)

                        If objSubprodUG.ErrorSubProductoUG = "" Then
                            If dtsfields.Tables(0).Rows.Count > 0 Then
                                If dtsfields.Tables(0).Rows(0).Item("ESTATUS") <> 2 Then
                                    strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El tipo de SubProductoUG ingresado no se encuentra activo."
                                    log(strErrorCargaPaquetes)
                                    Exit Function
                                End If
                            Else
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & "No existe información de Subproducto"
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objSubprodUG.ErrorSubProductoUG
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If
                        'Valida Esquemas
                        Dim objEsquema As New clsEsquema

                        objEsquema.Nombre = row("ID_ESQUEMAS")
                        dtsfields = objEsquema.MAnejaEsquema(1)

                        If objEsquema.ErrorEsquemas = "" Then
                            If dtsfields.Tables(0).Rows.Count > 0 Then
                                If dtsfields.Tables(0).Rows(0).Item("C_ESTATUS") <> 2 Then
                                    strErrorCargaPaquetes = DateAndTime.Now.ToString & ". ID_PAQUETE " & row("ID_PAQUETE") & ". El tipo de esquema no se encuentra activo."
                                    log(strErrorCargaPaquetes)
                                    Exit Function
                                End If
                            Else
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". ID_PAQUETE " & row("ID_PAQUETE") & "." & "No existe información de este subproducto"
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Erro ID_PAQUETE " & row("ID_PAQUETE") & "." & objEsquema.ErrorEsquemas
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If
                        'If dtsfields.Tables(0).Rows.Count > 0 Then

                        'End If
                        ''dtsfields.Tables(0).Rows(0).Item("ID_SUBPRODUCTO_UG")

                        ''Dim objSubprodUG As New clsSubProductosUG(row("ID_SUBPRODUCTO_UG"))
                        'If objSubprodUG.ErrorSubProductoUG = "" Then
                        '    If objSubprodUG.IDEstatus <> 2 Then
                        '        strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El tipo de SubProductoUG ingresado no se encuentra activo."
                        '        log(strErrorCargaPaquetes)
                        '        Exit Function
                        '    End If
                        'Else
                        '    strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objSubprodUG.ErrorSubProductoUG
                        '    log(strErrorCargaPaquetes)
                        '    Exit Function
                        'End If

                        'Validamos el Tipo de Producto
                        Dim objtprod As New clsTipoProductos(row("ID_TIPO_PRODUCTO"))
                        If objtprod.ErrorTipoProducto = "" Then
                            If objtprod.IDEstatus <> 2 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El dato ingresado en ID_TIPO_PRODUCTO no se encuentra activo."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objtprod.ErrorTipoProducto
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                        'Validamos el Canal
                        If row("ID_CANAL") < 1 And row("ID_CANAL") > 2 Then
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El dato ingresado en 'ID_CANAL' no es correcto."
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                    Case "PERSONALIDAD"
                        Dim objpj As New clsParametros(row("ID_PER_JURIDICA"))
                        If objpj.ErrorParametros = "" Then
                            If objpj.IDPadre <> 13 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El tipo de Personalidad Jurídica es incorrecto."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objpj.ErrorParametros
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                    Case "SEGUROSPROD"
                        Dim objpj As New clsParametros(row("ID_TIPO_SEGURO"))
                        If objpj.ErrorParametros = "" Then
                            If objpj.IDPadre <> 29 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El tipo de Seguro es incorrecto."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                            If objpj.Valor <> 2 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El tipo de Seguro se encuentra inactivo."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objpj.ErrorParametros
                            Exit Function
                        End If

                    Case "SEGUROSVIDA"
                        Dim objpj As New clsParametros(row("ID_TIPO_SEGURO"))
                        If objpj.ErrorParametros = "" Then
                            If objpj.IDPadre <> 169 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El tipo de Seguro de Vida es incorrecto."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                            If IIf(objpj.Valor = "", 0, objpj.Valor) <> 2 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El tipo de Seguro de Vida " & objpj.Descripcion & " se encuentra inactivo."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objpj.ErrorParametros
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                    Case "CONDICIONES"
                        Dim objplazos As New clsPlazo(row("ID_PLAZO"))
                        If objplazos.StrErrPlazo = "" Then
                            If objplazos.Status <> 2 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El plazo seleccionado se encuentra inactivo."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                            If objplazos.Id_Periodicidad <> row("ID_PERIODICIDAD") Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". No coincide el tipo de periodicidad."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objplazos.StrErrPlazo
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                    Case "AGENCIAS"
                        Dim objAgencias As New clsAgencias(row("ID_AGENCIA"))
                        If objAgencias.ErrorAgencia = "" Then
                            If objAgencias.IDAgencia = 0 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". No se pudo recuperar información de la Agencia."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            Else
                                If objAgencias.IDEstatus <> 2 Then
                                    strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". La Agencia se encuentra inactiva."
                                    log(strErrorCargaPaquetes)
                                    Exit Function
                                End If
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE" & row("ID_PAQUETE") & ". " & objAgencias.ErrorAgencia
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                    Case "PRODUCTOS"
                        Dim objprod As New clsProductos
                        objprod.IDTipoProducto = row("ID_TIPO_PRODUCTO")
                        objprod.IDClasificacion = row("ID_CLASIFICACION")
                        objprod.IDMarca = row("ID_MARCA")
                        objprod.IDSubmarca = row("ID_SUBMARCA")
                        objprod.IDVersion = row("ID_VERSION")
                        objprod.IDAnio = row("ID_ANIO")
                        dtsfields = objprod.ManejaProducto(1)

                        If objprod.ErrorProducto = "" Then
                            If dtsfields.Tables(0).Rows.Count > 0 Then
                                If dtsfields.Tables(0).Rows(0).Item("ESTATUS") <> 2 Then
                                    strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". El Producto se encuentra inactivo."
                                    log(strErrorCargaPaquetes)
                                    Exit Function
                                End If
                            Else
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". No se encontro información del Producto."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objprod.ErrorProducto
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If

                    Case "ASEGURADORAS"
                        Dim objaseg As New clsAseguradoras(row("ID_ASEGURADORA"))
                        If objaseg.ErrorAseguradora = "" Then
                            If objaseg.IDAseguradora = 0 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". No se encontro información de la Aseguradora."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                            If objaseg.IDEstatus <> 2 Then
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". La Aseguradora se encuentra inactiva."
                                log(strErrorCargaPaquetes)
                                Exit Function
                            End If
                        Else
                            strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & ". " & objaseg.ErrorAseguradora
                            log(strErrorCargaPaquetes)
                            Exit Function
                        End If
                End Select
            Next
        Next

        ValidaDatos = True

    End Function

    Public Function InsertaPaquete(ByVal intPaqId As Integer, ByVal ds As DataSet) As Boolean
        InsertaPaquete = False
        Dim TotErr As Integer
        Dim TotSucc As Integer = 0
        Dim dts As New dsCargaPaquetes
        Dim dtTabla As DataTable
        Dim renglon As DataRow
        Dim renglones() As DataRow
        Dim strsql As String = "ID_PAQUETE = " & intPaqId
        Dim dtsresult As New DataSet

        Dim conexion As New clsConexion(True)
        conexion.AbreConexion()

        Try

            objpaq.CargaSession(intcveAcceso)
            Dim usrreg As String = objpaq.UserNameAcceso

            For i As Integer = 0 To 7
                dtTabla = ds.Tables(i)
                renglones = dtTabla.Select(strsql)

                For Each renglon In renglones
                    dts.Tables(ds.Tables(i).TableName).ImportRow(renglon)
                Next
            Next


            For Each t As DataTable In dts.Tables
                For Each r As DataRow In t.Rows

                    Select Case t.TableName
                        Case "PAQUETES"
                            Dim objpaq As New clsPaquetes
                            objpaq.IDTipoOperacion = r("ID_TIPO_OPERACION")
                            objpaq.IDMoneda = r("ID_MONEDA")
                            objpaq.IDClasificacionProd = r("ID_CLASIFICACION_PROD")
                            objpaq.Nombre = Trim(r("NOMBRE").ToString)
                            objpaq.PtjMaximoAccesorios = r("PTJ_MAX_ACCESORIOS")
                            objpaq.IDCalendario = r("ID_CALENDARIO")
                            objpaq.IDPeriodicidad = r("ID_PERIODICIDAD")
                            objpaq.IDTipoCalculo = r("ID_TIPO_CALCULO")
                            objpaq.PrimerPagoIrregular = r("PRIMER_PAGO_IRREG")
                            objpaq.PermiteSeguroRegalado = r("PERMITE_SEGURO_REGALADO")
                            objpaq.PermiteMontoSubsidio = r("PERMITE_MONTO_SUBSIDIO")
                            objpaq.PermitePtjSubsidio = r("PERMITE_PTJ_SUBSIDIO")
                            objpaq.IDTipoCalculoSeguro = r("ID_TIPO_CALCULO_SEGURO")
                            objpaq.PermitePagosEspeciales = r("PERMITE_PAGOS_ESPECIALES")
                            objpaq.PermiteGraciaCapital = r("PERMITE_GRACIA_CAPITAL")
                            objpaq.PermiteGraciaInteres = r("PERMITE_GRACIA_INTERES")
                            objpaq.IDTipoSegVida = r("ID_TIPO_SEGURO_VIDA")
                            objpaq.IDTipoVencimiento = r("ID_TIPO_VENCIMIENTO")
                            objpaq.FactorSeguroVida = r("FACTOR_SEGURO_VIDA")
                            objpaq.CalculaIVASeguroVida = r("CALCULAR_IVA_SEGURO_VIDA")
                            objpaq.InicioVigencia = CDate(r("INI_VIG")).ToString("yyyy-MM-dd")
                            objpaq.FinVigencia = CDate(r("FIN_VIG")).ToString("yyyy-MM-dd")
                            objpaq.IncentivoVentas = r("INCENTIVO_VENTAS")
                            objpaq.noperiodos = r("NOPERIODOS")
                            objpaq.IDPROMOCION = r("ID_PROMOCION")
                            objpaq.PorPagEspeciales = r("PTJ_PAGOS_ESPECIALES")
                            objpaq.PorSubsidioArmadora = r("PTJ_SUBSIDIO_ARMADORA")
                            objpaq.PorSubsidioAgencia = r("PTJ_SUBSIDIO_AGENCIA")
                            objpaq.ComisionVtaSeg = r("PTJ_COMISION_VTASEG")
                            objpaq.PorMinComisionVendedor = r("PTJ_MIN_COMISION_VENDEDOR")
                            objpaq.PorMaxComisionVendedor = r("PTJ_MAX_COMISION_VENDEDOR")
                            objpaq.PorMinComisionAgencia = r("PTJ_MIN_COMISION_AGENCIA")
                            objpaq.PorMaxComisionAgencia = r("PTJ_MAX_COMISION_AGENCIA")
                            objpaq.IDViaCalcSegVida = r("VIA_CALC_SEG_VIDA")
                            objpaq.CEROCOMISION = r("CERO_COMISION")
                            objpaq.IDProdUG = r("ID_PRODUCTO_UG")

                            Dim objSubprodUG As New clsSubProductosUG
                            Dim dtsfields As New DataSet

                            objSubprodUG.Nombre = r("ID_SUBPRODUCTO_UG")
                            dtsfields = objSubprodUG.ManejaSubProductoUG(1)
                            objpaq.IDSubProdUG = dtsfields.Tables(0).Rows(0).Item("ID_SUBPRODUCTO_UG")

                            Dim objEsquema As New clsEsquema
                            objEsquema.Nombre = r("ID_ESQUEMAS")
                            dtsfields = objEsquema.MAnejaEsquema(1)
                            objpaq.IDEsquema = dtsfields.Tables(0).Rows(0).Item("ID_ESQUEMAS")


                            objpaq.ImporteMinG = r("IMPORTEMING")
                            objpaq.ImporteMaxG = r("IMPORTEMAXG")
                            objpaq.UsuarioRegistro = usrreg
                            objpaq.IDEstatus = 2

                            objpaq.ManejaPaquete(2, conexion)

                            If objpaq.ErrorPaquete = "" Then
                                paqid = objpaq.IDPaquete
                                intTotSuccess += 1

                                'tipos producto
                                Dim objpaqtp As New clsPaquetes
                                objpaqtp.IDPaquete = paqid
                                objpaqtp.IDTipoProducto = dts.Tables(0).Rows(0).Item("ID_TIPO_PRODUCTO")
                                objpaqtp.IDEstatusOtro = 2
                                objpaqtp.UsuarioRegistro = usrreg
                                objpaqtp.ManejaPaquete(26, conexion)
                                If objpaqtp.ErrorPaquete = "" Then
                                    intTotSuccess += 1
                                Else
                                    TotErr += 1
                                    strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error. " & objpaqtp.ErrorPaquete
                                    Throw New Exception(strErrorCargaPaquetes)
                                End If

                                'clasificaciones
                                Dim objpaqclas As New clsPaquetes
                                objpaqclas.IDPaquete = paqid
                                objpaqclas.IDClasificacionProd = dts.Tables(0).Rows(0).Item("ID_CLASIFICACION_PROD")
                                objpaqclas.IDEstatusOtro = 2
                                objpaqclas.UsuarioRegistro = usrreg
                                objpaqclas.ManejaPaquete(20, conexion)
                                If objpaqclas.ErrorPaquete = "" Then
                                    intTotSuccess += 1
                                Else
                                    TotErr += 1
                                    strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error. " & objpaqclas.ErrorPaquete
                                    Throw New Exception(strErrorCargaPaquetes)
                                End If

                                'tipos operación
                                Dim objpaqto As New clsPaquetes
                                objpaqto.IDPaquete = paqid
                                objpaqto.IDTipoOperacion = dts.Tables(0).Rows(0).Item("ID_TIPO_OPERACION")
                                objpaqto.IDEstatusOtro = 2
                                objpaqto.UsuarioRegistro = usrreg
                                objpaqto.ManejaPaquete(29, conexion)
                                If objpaqto.ErrorPaquete = "" Then
                                    intTotSuccess += 1
                                Else
                                    TotErr += 1
                                    strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error. " & objpaqto.ErrorPaquete
                                    Throw New Exception(strErrorCargaPaquetes)
                                End If

                                'canales
                                Dim objpaqcanales As New clsPaquetes
                                objpaqcanales.IDPaquete = paqid
                                objpaqcanales.IDCanal = r("ID_CANAL")
                                objpaqcanales.IDEstatusOtro = 2
                                objpaqcanales.UsuarioRegistro = usrreg
                                objpaqcanales.ManejaPaquete(42, conexion)
                                If objpaqcanales.ErrorPaquete = "" Then
                                    intTotSuccess += 1
                                Else
                                    TotErr += 1
                                    strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error. " & objpaqcanales.ErrorPaquete
                                    Throw New Exception(strErrorCargaPaquetes)
                                End If

                            Else
                                TotErr += 1
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error. " & objpaq.ErrorPaquete
                                Throw New Exception(strErrorCargaPaquetes)
                            End If

                        Case "PERSONALIDAD"
                            Dim objpaqpj As New clsPaquetes
                            objpaqpj.IDPaquete = paqid
                            objpaqpj.IDPersonalidadJuridica = r("ID_PER_JURIDICA")
                            objpaqpj.IDEstatusOtro = 2
                            objpaqpj.UsuarioRegistro = usrreg
                            objpaqpj.ManejaPaquete(17, conexion)

                            If objpaqpj.ErrorPaquete = "" Then
                                intTotSuccess += 1
                            Else
                                TotErr += 1
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error. " & objpaqpj.ErrorPaquete
                                Throw New Exception(strErrorCargaPaquetes)
                            End If

                        Case "SEGUROSPROD"
                            Dim objpaqseg As New clsPaquetes
                            objpaqseg.IDPaquete = paqid
                            objpaqseg.IDTipoSeg = r("ID_TIPO_SEGURO")
                            objpaqseg.IDEstatusOtro = 2
                            objpaqseg.UsuarioRegistro = usrreg
                            objpaqseg.ManejaPaquete(13, conexion)
                            If objpaqseg.ErrorPaquete = "" Then
                                intTotSuccess += 1
                            Else
                                TotErr += 1
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error. " & objpaqseg.ErrorPaquete
                                Throw New Exception(strErrorCargaPaquetes)
                            End If

                        Case "SEGUROSVIDA"
                            Dim objpaqsegvida As New clsPaquetes
                            objpaqsegvida.IDPaquete = paqid
                            objpaqsegvida.IDTipoSegVida = r("ID_TIPO_SEGURO")
                            objpaqsegvida.IDEstatusOtro = 2
                            objpaqsegvida.UsuarioRegistro = usrreg
                            objpaqsegvida.ManejaPaquete(45, conexion)
                            If objpaqsegvida.ErrorPaquete = "" Then
                                intTotSuccess += 1
                            Else
                                TotErr += 1
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error. " & objpaqsegvida.ErrorPaquete
                                Throw New Exception(strErrorCargaPaquetes)
                            End If

                        Case "CONDICIONES"
                            Dim objpaqplazos As New clsPaquetes
                            objpaqplazos.IDPaquete = paqid
                            objpaqplazos.IDPlazo = r("ID_PLAZO")
                            objpaqplazos.PtjEngancheMinimo = r("PTJ_ENGANCHE_MIN")
                            objpaqplazos.TasaNominal = r("TASA_NOMINAL")
                            objpaqplazos.TasaNominalSeguro = r("TASA_NOMINAL_SEGURO")
                            objpaqplazos.PuntosSeguroCliente = 0
                            objpaqplazos.PtjServiciosFinancieros = r("PTJ_SERV_FINAN")
                            objpaqplazos.PtjOpcionCompra = 0
                            objpaqplazos.TasaPCP = 0
                            objpaqplazos.PtjBlindDiscount = r("PTJ_BLIND_DISCOUNT")
                            objpaqplazos.IDEstatusPlazo = 2
                            objpaqplazos.UsuarioRegistro = usrreg
                            objpaqplazos.ManejaPaquete(6, conexion)
                            If objpaqplazos.ErrorPaquete = "" Then
                                intTotSuccess += 1
                            Else
                                TotErr += 1
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error. " & objpaqplazos.ErrorPaquete
                                Throw New Exception(strErrorCargaPaquetes)
                            End If

                        Case "AGENCIAS"
                            Dim objpaqage As New clsPaquetes
                            objpaqage.IDPaquete = paqid
                            objpaqage.IDAgencia = r("ID_AGENCIA")
                            objpaqage.UsuarioRegistro = usrreg
                            objpaqage.ManejaPaquete(36, conexion)
                            If objpaqage.ErrorPaquete = "" Then
                                intTotSuccess += 1
                            Else
                                TotErr += 1
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error. " & objpaqage.ErrorPaquete
                                Throw New Exception(strErrorCargaPaquetes)
                            End If

                        Case "PRODUCTOS"
                            Dim objpaqprod As New clsPaquetes
                            Dim objproducto As New clsProductos
                            objproducto.IDTipoProducto = r("ID_TIPO_PRODUCTO")
                            objproducto.IDClasificacion = r("ID_CLASIFICACION")
                            objproducto.IDMarca = r("ID_MARCA")
                            objproducto.IDSubmarca = r("ID_SUBMARCA")
                            objproducto.IDVersion = r("ID_VERSION")
                            objproducto.IDAnio = r("ID_ANIO")
                            dtsresult = objproducto.ManejaProducto(1)

                            objpaqprod.IDPaquete = paqid
                            objpaqprod.IDProducto = dtsresult.Tables(0).Rows(0).Item("ID_PRODUCTO")
                            objpaqprod.UsuarioRegistro = usrreg
                            objpaqprod.ManejaPaquete(10, conexion)
                            If objpaqprod.ErrorPaquete = "" Then
                                intTotSuccess += 1
                            Else
                                TotErr += 1
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error. " & objpaqprod.ErrorPaquete
                                Throw New Exception(strErrorCargaPaquetes)
                            End If

                        Case "ASEGURADORAS"
                            Dim objpaqaseg As New clsPaquetes
                            objpaqaseg.IDPaquete = paqid
                            objpaqaseg.IDAseguradora = r("ID_ASEGURADORA")
                            objpaqaseg.UsuarioRegistro = usrreg
                            objpaqaseg.ManejaPaquete(48, conexion)
                            If objpaqaseg.ErrorPaquete = "" Then
                                intTotSuccess += 1
                            Else
                                TotErr += 1
                                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error. " & objpaqaseg.ErrorPaquete
                                Throw New Exception(strErrorCargaPaquetes)
                            End If
                    End Select
                Next
            Next

            'Consulta al WebService.
            If ConsulWS(intPaqId, ds) Then
                If intTotSuccess > 0 Then
                    conexion.TerminaTransaccion()
                    strErrorCargaPaquetes = DateTime.Now.ToString & ". OK. El paquete " & objpaq.Nombre & " se inserto correctamente."
                    log(strErrorCargaPaquetes)
                    InsertaPaquete = True
                End If
            Else
                conexion.TerminaTransaccion(True)
                Exit Function
            End If

        Catch ex As Exception
            conexion.TerminaTransaccion(True)
            strErrorCargaPaquetes = DateTime.Now.ToString & ". Error. " & ex.Message
            log(strErrorCargaPaquetes)
            intTotSuccess = 0
            Exit Function
        End Try

    End Function

    Public Sub log(text As String)

        Dim wr As StreamWriter

        Try
            Dim path As String = System.Configuration.ConfigurationManager.AppSettings.Item("CargaPaquete")
            Dim name As String = "logCargaPaq_" & Today & ".txt"
            name = Replace(name, "/", "")
            Dim txtfile As String = path & name

            If Not System.IO.Directory.Exists(path) Then
                System.IO.Directory.CreateDirectory(path)
            End If

            If Not File.Exists(txtfile) Then
                wr = New StreamWriter(txtfile, True)
                wr.WriteLine(text)
            Else
                wr = New StreamWriter(txtfile, True)
                wr.WriteLine(text)
            End If

            wr.Close()
            wr = Nothing

        Catch ex As Exception
            strErrorCargaPaquetes = ex.Message
        End Try
    End Sub

    Public Function ConsulWS(ByVal intPaqID As Integer, ByVal ds As DataSet) As Boolean
        Try
            ConsulWS = False

            Dim plazos(0) As Integer
            Dim tasas(0) As Decimal
            Dim maxindex As Integer = 0
            Dim minindex As Integer = 0
            Dim maxtasa As Double = 0

            Dim strsql As String = "ID_PAQUETE = " & intPaqID
            Dim loanBASE As loanBASE = New loanBASE()

            Dim objpaqws As SNProcotiza.clsPaquetes = New SNProcotiza.clsPaquetes()
            Dim dts As New DataSet
            dts = objpaqws.consultaWS(1)
            If objpaqws.ErrorPaquete <> "" Then
                strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & intPaqID & "." & " Error al cargar información de paquetes."
            End If

            Dim tbplazo As DataTable = ds.Tables("CONDICIONES")
            Dim tbpaq As DataTable = ds.Tables("PAQUETES")

            For Each row As DataRow In tbplazo.Select(strsql)
                objPlazos.CargaPlazo(row("ID_PLAZO"))
                plazos(UBound(plazos)) = objPlazos.Valor
                ReDim Preserve plazos(UBound(plazos) + 1)

                tasas(UBound(tasas)) = row("TASA_NOMINAL")
                ReDim Preserve tasas(UBound(tasas) + 1)
            Next

            ReDim Preserve plazos(UBound(plazos) - 1)
            ReDim Preserve tasas(UBound(tasas) - 1)

            For i = 1 To UBound(plazos)
                If plazos(i) > plazos(maxindex) Then
                    maxindex = i
                End If
                If plazos(i) < plazos(minindex) Then
                    minindex = i
                End If
            Next

            For i = 1 To UBound(tasas) 'suponemos indice desde 0 a n 
                If tasas(i) > tasas(maxtasa) Then
                    maxtasa = i
                End If
            Next

            'BUG-PC-29
            For Each row As DataRow In tbpaq.Select(strsql)
                loanBASE.loan.loanProduct.extendedData.planType.id = "00002"
                loanBASE.loan.loanProduct.extendedData.operationType.id = "TR"
                loanBASE.loan.loanProduct.extendedData.money.currency = "0001"
                loanBASE.loan.loanProduct.extendedData.paymentType.id = ObtenValor(dts, row("ID_TIPO_VENCIMIENTO"), 5, 0)
                loanBASE.loan.loanProduct.extendedData.maximumAmount.amount = formatsendws(row("IMPORTEMAXG"), 15, 1)
                loanBASE.loan.loanProduct.extendedData.minimumCapital.amount = formatsendws(row("IMPORTEMING"), 15, 1)

                loanBASE.loan.loanProduct.extendedData.maximumTerm = formatsendws(plazos(maxindex), 4, 0)
                loanBASE.loan.loanProduct.extendedData.minimumTerm = formatsendws(plazos(minindex), 4, 0)

                loanBASE.loan.loanProduct.extendedData.gracePeriodUnit = "000"
                loanBASE.loan.dueDate = CDate(row("INI_VIG")).ToString("yyyy-MM-dd")
                loanBASE.loan.period.timeUnit.name = IIf(row("ID_PERIODICIDAD") = 83, "MEN", IIf(row("ID_PERIODICIDAD") = 95, "QUI", "SEM"))
                loanBASE.productCode = row("ID_PRODUCTO_UG")
                loanBASE.subProductCode = row("ID_SUBPRODUCTO_UG")
                loanBASE.Esquema = row("ID_ESQUEMA")
                loanBASE.startLoanDate = CDate(row("FIN_VIG")).ToString("yyyy-MM-dd") & " 00:00:00:000000"

                Dim l1 As listDtoRate = New listDtoRate()
                l1.type.name = "real"
                l1.percentage = formatsendws(tasas(maxtasa), 7, 1, 1)
                loanBASE.iLoanDetail.loanCar.listDtoRate.Add(l1)

                Dim l2 As listDtoRate = New listDtoRate()
                l2.type.name = "mora"
                l2.percentage = "0000000"
                loanBASE.iLoanDetail.loanCar.listDtoRate.Add(l2)

                loanBASE.iLoanDetail.loanCar.packageId = New String("0"c, 9 - Len(paqid.ToString)) & paqid.ToString 'paqid
                loanBASE.iLoanDetail.loanCar.packageDescription = row("NOMBRE")

                Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                Dim jsonBODY As String = serializer.Serialize(loanBASE)

                Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
                Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
                Dim restGT As RESTful = New RESTful()
                restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("url") & System.Configuration.ConfigurationManager.AppSettings.Item("metodo")


                Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)

                Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()
                Dim alert = srrSerialer.Deserialize(Of msjerr)(jsonResult)

                If restGT.IsError Then
                    strErrorCargaPaquetes = DateAndTime.Now.ToString & ". Error ID_PAQUETE " & row("ID_PAQUETE") & "." & IIf(restGT.StatusHTTP = 500, " Error al consultar Servicio Web: ", " Mensaje del Servicio Web: ") & restGT.MensajeError & ". Estatus:" & restGT.StatusHTTP

                    log(strErrorCargaPaquetes)
                    Exit Function
                Else
                    ConsulWS = True
                End If
            Next
        Catch ex As Exception
            strErrorCargaPaquetes = DateAndTime.Now.ToString & ex.Message
            log(strErrorCargaPaquetes)
            Return False
        End Try
    End Function

    Private Function formatsendws(valor As String, lng As Integer, isDec As Integer, Optional ispercent As Integer = 0) As String
        Dim strresult As String = String.Empty
        Dim Pos As Integer = 0

        Select Case ispercent
            Case 0
                If isDec = 1 Then
                    Pos = InStr(valor, ".")
                    If Pos > 0 Then
                        strresult = ((valor).Substring(0, Pos) & (valor).Substring(Pos, 2)).Replace(".", "")
                    Else
                        strresult = valor & "00"
                    End If
                    strresult = New String("0"c, lng - Len(strresult)) & strresult
                Else
                    strresult = New String("0"c, lng - Len(valor)) & valor
                End If
            Case 1
                Pos = InStr(valor, ".")
                If Pos > 0 Then
                    Dim strdec As String = valor.Substring(Pos, valor.Length - Pos)
                    If strdec.Length < 4 Then
                        strdec = strdec & New String("0"c, 4 - Len(strdec))
                    End If

                    Dim strent As String = valor.Substring(0, Pos - 1)
                    strresult = New String("0"c, lng - Len(strent & strdec)) & (strent & strdec)
                Else
                    strresult = valor & "0000"
                    strresult = New String("0"c, lng - Len(strresult)) & strresult
                End If

        End Select

        Return strresult
    End Function

    Private Function ObtenValor(dts As DataSet, valor As Integer, lng As Integer, isDec As Integer) As String
        Dim strdato As String = String.Empty

        Dim renglones() As DataRow

        renglones = dts.Tables(0).Select("IDINTERNO = " & valor.ToString)

        For Each dato As DataRow In renglones
            strdato = dato("IDEXTERNO").ToString
        Next

        strdato = formatsendws(strdato, lng, isDec)

        Return strdato
    End Function
End Class

Public Class loanBASE
    Public loan As loan = New loan()
    Public productCode As String
    Public subProductCode As String
    Public Esquema As String
    Public startLoanDate As String
    Public iLoanDetail As iLoanDetail = New iLoanDetail()
End Class

Public Class loan
    Public loanProduct As loanProduct = New loanProduct()
    Public dueDate As String
    Public period As period = New period
End Class

Public Class loanProduct
    Public extendedData As extendedData = New extendedData()
End Class

Public Class extendedData
    Public planType As planType = New planType()
    Public operationType As operationType = New operationType()
    Public money As money = New money()
    Public paymentType As paymentType = New paymentType()
    Public maximumAmount As maximumAmount = New maximumAmount()
    Public minimumCapital As minimumCapital = New minimumCapital()
    Public maximumTerm As String
    Public minimumTerm As String
    Public gracePeriodUnit As String
End Class

Public Class planType
    Public id As String
End Class

'BUG-PC-29
Public Class operationType
    Public id As String
End Class

Public Class money
    Public currency As String
End Class

Public Class paymentType
    Public id As String
End Class

Public Class maximumAmount
    Public amount As String
End Class

Public Class minimumCapital
    Public amount As String
End Class

Public Class period
    Public timeUnit As timeUnit = New timeUnit
End Class

Public Class timeUnit
    Public name As String
End Class

Public Class iLoanDetail
    Public loanCar As loanCar = New loanCar()
End Class

Public Class loanCar
    Public listDtoRate As List(Of listDtoRate) = New List(Of listDtoRate)
    Public packageId As String
    Public packageDescription As String
End Class

Public Class listDtoRate
    Public type As Tipe = New Tipe()
    Public percentage As String
End Class

Public Class Tipe
    Public name As String
End Class

Public Class msjerr
    Public message As String
    Public status As String
End Class