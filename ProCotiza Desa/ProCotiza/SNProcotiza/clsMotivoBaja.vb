'--BBV-P-412:AUBALDO:06/07/2016 RQ10 – Agregar funcionalidad en la pantalla Alta de Agencias (Brecha 63,64, Ampliación de Brecha)

Imports SDManejaBD
Public Class clsMotivoBaja
    Inherits clsSession

    Private intMBaja As Integer = 0
    Private strErrMBaja As String = ""
    Private strMBaja As String = ""    
    Private intEstatus As Integer = 0
    Private intMBajaFiltro As Integer = 0
    Private strUsuReg As String = ""

    Public Property IDMBajaFiltro() As Integer
        Get
            Return intMBajaFiltro
        End Get
        Set(ByVal value As Integer)
            intMBajaFiltro = value
        End Set
    End Property

    Public Property IDMBaja() As Integer
        Get
            Return intMBaja
        End Get
        Set(ByVal value As Integer)
            intMBaja = value
        End Set
    End Property

    Public Property MBaja() As String
        Get
            Return strMBaja
        End Get
        Set(ByVal value As String)
            strMBaja = value
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
    
    Public ReadOnly Property ErrorMBaja() As String
        Get
            Return strErrMBaja
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
    Public Function ManejaMotivoBaja(ByVal intOper As Integer) As DataSet
        strErrMBaja = ""
        ManejaMotivoBaja = New DataSet
        Dim strParamStored As String = ""
        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta Motivo de Baja
                    If intMBaja > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMBaja", intMBaja.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    If strMBaja <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "MBaja", strMBaja.ToString)

                Case 2 ' inserta Motivo de Baja
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMBaja", intMBaja.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "MBaja", strMBaja.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 3 ' actualiza Motivo de Baja
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMBaja", intMBaja.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "MBaja", strMBaja.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "Estatus", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 4 ' borra Motivo de Baja
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMBaja", intMBaja.ToString)


                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)


            End Select

            ManejaMotivoBaja = objSD.EjecutaStoredProcedure("spManejaMotivoBaja", strErrMBaja, strParamStored)
            If strErrMBaja = "" Then
                If intOper = 2 Then
                    intMBaja = ManejaMotivoBaja.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("Motivo de Baja", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrMBaja = ex.Message
        End Try
    End Function
    Public Sub CargaGrupo(Optional ByVal intAse As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intMBaja = intAse
            dtsRes = ManejaMotivoBaja(1)
            intMBaja = 0
            If Trim$(strErrMBaja) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intMBaja = dtsRes.Tables(0).Rows(0).Item("CVE_PCO_TIPO_BAJA")
                    strMBaja = dtsRes.Tables(0).Rows(0).Item("TIPO_BAJA").ToString
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                Else
                    strErrMBaja = "No se encontró información para poder cargar la Grupo"
                End If
            End If
        Catch ex As Exception
            strErrMBaja = ex.Message
        End Try
    End Sub

End Class
