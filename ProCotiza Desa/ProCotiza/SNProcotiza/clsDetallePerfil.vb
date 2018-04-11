'BBV-P-412:AVH:12/07/2016 RQ01: SE CREA CLASE DE DETALLE PERFIL
'BBV-P-412:AVH:19/07/2016 RQB:  SE CREAN OPCIONES PARA PERFILES
'BUG PC 32 GVARGAS 06/01/2017 Mostrar solo perfiles <> inativos

Imports SDManejaBD
Public Class clsDetallePerfil
    Inherits clsSession

    Private strErrDetallePerfil As String = ""

    Private intIdUsuario As Integer = 0
    Private numCuenta As String = ""
    'Private strNombreJefe As String = ""
    Private strTitularCuenta As String = ""
    Private strUsuReg As String = ""

    Private intPagoComision As Integer = 0
    Private intUsuCveReg As Integer = 0


    'PERFILES
    Private intIdPerfil As Integer = 0
    Private intEstatus As Integer
    Private strPerfil As String = ""




    Public Property IdUsuario() As Integer
        Get
            Return intIdUsuario
        End Get
        Set(ByVal value As Integer)
            intIdUsuario = value
        End Set
    End Property
    Public Property NCuenta() As String
        Get
            Return numCuenta
        End Get
        Set(ByVal value As String)
            numCuenta = value
        End Set
    End Property
    'Public Property NombreJefe() As String
    '    Get
    '        Return strNombreJefe
    '    End Get
    '    Set(ByVal value As String)
    '        strNombreJefe = value
    '    End Set
    'End Property
    Public Property TitularCuenta() As String
        Get
            Return strTitularCuenta
        End Get
        Set(ByVal value As String)
            strTitularCuenta = value
        End Set
    End Property
    Public WriteOnly Property UsuarioRegistro() As String
        Set(ByVal value As String)
            strUsuReg = value
        End Set
    End Property
    
    Public ReadOnly Property ErrorDetalle() As String
        Get
            Return strErrDetallePerfil
        End Get
    End Property
    '================PERFILES
    Public Property IdPerfil() As Integer
        Get
            Return intIdPerfil
        End Get
        Set(ByVal value As Integer)
            intIdPerfil = value
        End Set
    End Property
    Public Property Estatus() As Integer
        Get
            Return intEstatus
        End Get
        Set(ByVal value As Integer)
            intEstatus = value
        End Set
    End Property
    Public Property Perfil() As String
        Get
            Return strPerfil
        End Get
        Set(ByVal value As String)
            strPerfil = value
        End Set
    End Property
    Public WriteOnly Property UsuarioCveRegistro() As Integer
        Set(ByVal value As Integer)
            intUsuCveReg = value
        End Set
    End Property
    Public Property PagoComision() As Integer
        Get
            Return intPagoComision
        End Get
        Set(ByVal value As Integer)
            intPagoComision = value
        End Set
    End Property

    Public Function ManejaPerfil(ByVal intOper As Integer) As DataSet
        strErrDetallePerfil = ""
        ManejaPerfil = New DataSet
        Dim strParamStored As String = ""
        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1
                    If intIdUsuario > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idUsuario", intIdUsuario.ToString)

                Case 2 'Guarda/modifica
                    If intIdUsuario > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idUsuario", intIdUsuario.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "numCuenta", numCuenta.ToString)
                    'ArmaParametros(strParamStored, TipoDato.Cadena, "NombreJefe", strNombreJefe.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "PagoComision", intPagoComision)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "TitularCuenta", strTitularCuenta.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)

                Case 3
                    If intIdPerfil > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPerfil", intIdPerfil.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    If strPerfil <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Perfil", strPerfil.ToString)
                Case 4
                    If intIdPerfil > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPerfil", intIdPerfil.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    If strPerfil <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Perfil", strPerfil.ToString)
                    If intUsuCveReg > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "usu_cve_reg", intUsuCveReg.ToString)
                Case 5
                    If intIdPerfil > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idPerfil", intIdPerfil.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    If strPerfil <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "Perfil", strPerfil.ToString)


            End Select

            ManejaPerfil = objSD.EjecutaStoredProcedure("spManejaPerfiles", strErrDetallePerfil, strParamStored)
            If strErrDetallePerfil = "" Then
                If intOper = 2 Then
                    intIdUsuario = ManejaPerfil.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("DetallePerfil", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrDetallePerfil = ex.Message
        End Try
    End Function
End Class
