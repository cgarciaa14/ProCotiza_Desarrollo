'--BBV-P-412:AUBALDO:06/07/2016 RQ10 – Agregar funcionalidad en la pantalla Alta de Agencias (Brecha 63,64, Ampliación de Brecha)

Imports SDManejaBD

Public Class clsTipoTelefono
    Inherits clsSession

    Private intTTelefono As Integer = 0
    Private strErrTTelefono As String = ""
    Private strTTelefono As String = ""
    Private intEstatus As Integer = 0
    Private intTTelefonoFiltro As Integer = 0
    Private strUsuReg As String = ""

    Public Property IDTTelefonoFiltro() As Integer
        Get
            Return intTTelefonoFiltro
        End Get
        Set(ByVal value As Integer)
            intTTelefonoFiltro = value
        End Set
    End Property

    Public Property IDTTelefono() As Integer
        Get
            Return intTTelefono
        End Get
        Set(ByVal value As Integer)
            intTTelefono = value
        End Set
    End Property

    Public Property TTelefono() As String
        Get
            Return strTTelefono
        End Get
        Set(ByVal value As String)
            strTTelefono = value
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

    Public ReadOnly Property ErrorTTelefono() As String
        Get
            Return strErrTTelefono
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
    Public Function ManejaTTelefono(ByVal intOper As Integer) As DataSet
        strErrTTelefono = ""
        ManejaTTelefono = New DataSet
        Dim strParamStored As String = ""
        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta Tipo Telefono
                    If intTTelefono > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTTelefono", intTTelefono.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    If strTTelefono <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "TTelefono", strTTelefono.ToString)

                Case 2 ' inserta Tipo Telefono
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTTelefono", intTTelefono.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "TTelefono", strTTelefono.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 3 ' actualiza Tipo Telefono
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTTelefono", intTTelefono.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "TTelefono", strTTelefono.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 4 ' borra Tipo Telefono
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTTelefono", intTTelefono.ToString)


                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)


            End Select

            ManejaTTelefono = objSD.EjecutaStoredProcedure("spManejaTTelefono", strErrTTelefono, strParamStored)
            If strErrTTelefono = "" Then
                If intOper = 2 Then
                    intTTelefono = ManejaTTelefono.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("Tipo Telefono", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrTTelefono = ex.Message
        End Try
    End Function
    Public Sub CargaGrupo(Optional ByVal intAse As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intTTelefono = intAse
            dtsRes = ManejaTTelefono(1)
            intTTelefono = 0
            If Trim$(strErrTTelefono) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intTTelefono = dtsRes.Tables(0).Rows(0).Item("CVE_PCO_TIPO_TELEFONO")
                    strTTelefono = dtsRes.Tables(0).Rows(0).Item("TIPO_TELEFONO").ToString
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                Else
                    strErrTTelefono = "No se encontró información para poder cargar Tipo Telefono"
                End If
            End If
        Catch ex As Exception
            strErrTTelefono = ex.Message
        End Try
    End Sub

End Class


