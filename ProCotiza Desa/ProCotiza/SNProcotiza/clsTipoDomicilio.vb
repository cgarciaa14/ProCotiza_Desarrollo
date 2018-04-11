'--BBV-P-412:AUBALDO:06/07/2016 RQ10 – Agregar funcionalidad en la pantalla Alta de Agencias (Brecha 63,64, Ampliación de Brecha)

Imports SDManejaBD

Public Class clsTipoDomicilio
    Inherits clsSession

    Private intTDomicilio As Integer = 0
    Private strErrTDomicilio As String = ""
    Private strTDomicilio As String = ""
    Private intEstatus As Integer = 0
    Private intTDomicilioFiltro As Integer = 0
    Private strUsuReg As String = ""

    Public Property IDTDomicilioFiltro() As Integer
        Get
            Return intTDomicilioFiltro
        End Get
        Set(ByVal value As Integer)
            intTDomicilioFiltro = value
        End Set
    End Property

    Public Property IDTDomicilio() As Integer
        Get
            Return intTDomicilio
        End Get
        Set(ByVal value As Integer)
            intTDomicilio = value
        End Set
    End Property

    Public Property TDomicilio() As String
        Get
            Return strTDomicilio
        End Get
        Set(ByVal value As String)
            strTDomicilio = value
        End Set
    End Property
    Public Property Estatus As Integer
        Get
            Return intEstatus
        End Get
        Set(ByVal value As Integer)
            intEstatus = value
        End Set
    End Property

    Public ReadOnly Property ErrorTDomicilio() As String
        Get
            Return strErrTDomicilio
        End Get
    End Property
    Public WriteOnly Property UsuarioRegistro() As String
        Set(ByVal value As String)
            strUsuReg = value
        End Set
    End Property


    'Buscar
    Public Property IDEstatus() As Integer
        Get
            Return intEstatus
        End Get
        Set(ByVal value As Integer)
            intEstatus = value
        End Set
    End Property
    Public Function ManejaTDomicilio(ByVal intOper As Integer) As DataSet
        strErrTDomicilio = ""
        ManejaTDomicilio = New DataSet
        Dim strParamStored As String = ""
        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta Motivo de Baja
                    If intTDomicilio > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTDomicilio", intTDomicilio.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    If strTDomicilio <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "TDomicilio", strTDomicilio.ToString)

                Case 2 ' inserta Motivo de Baja
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTDomicilio", intTDomicilio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "TDomicilio", strTDomicilio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 3 ' actualiza Motivo de Baja
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTDomicilio", intTDomicilio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "TDomicilio", strTDomicilio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 4 ' borra Motivo de Baja
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTDomicilio", intTDomicilio.ToString)


                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)


            End Select

            ManejaTDomicilio = objSD.EjecutaStoredProcedure("spManejaTDomicilio", strErrTDomicilio, strParamStored)
            If strErrTDomicilio = "" Then
                If intOper = 2 Then
                    intTDomicilio = ManejaTDomicilio.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("Tipo Domicilio", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrTDomicilio = ex.Message
        End Try
    End Function
    Public Sub CargaGrupo(Optional ByVal intAse As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intTDomicilio = intAse
            dtsRes = ManejaTDomicilio(1)
            intTDomicilio = 0
            If Trim$(strErrTDomicilio) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intTDomicilio = dtsRes.Tables(0).Rows(0).Item("CVE_PCO_TIPO_Domicilio")
                    strTDomicilio = dtsRes.Tables(0).Rows(0).Item("TIPO_Domicilio").ToString
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                Else
                    strErrTDomicilio = "No se encontró información para poder cargar Tipo Domicilio"
                End If
            End If
        Catch ex As Exception
            strErrTDomicilio = ex.Message
        End Try
    End Sub

End Class


