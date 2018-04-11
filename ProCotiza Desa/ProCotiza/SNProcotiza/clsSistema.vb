Imports SDManejaBD

Public Class clsSistema
    Inherits clsSession

    Private strErrSistemas As String = ""
    Private intSistema As Integer = 0
    Private strDescripcion As String = ""
    Private intEstatus As Integer = 0
    Private strUsuReg As String = ""

    Sub New()
    End Sub

    Sub New(ByVal intCveSistema As Integer)
        CargaSistema(intCveSistema)
    End Sub

    Public ReadOnly Property ErrorSistemas() As String
        Get
            Return strErrSistemas
        End Get
    End Property

    Public Property IDSistema() As Integer
        Get
            Return intSistema
        End Get
        Set(ByVal value As Integer)
            intSistema = value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Return strDescripcion
        End Get
        Set(ByVal value As String)
            strDescripcion = value
        End Set
    End Property

    Public Property IDEstatus() As Integer
        Get
            Return intEstatus
        End Get
        Set(ByVal value As Integer)
            intEstatus = value
        End Set
    End Property

    Public WriteOnly Property UsuarioRegistro() As String
        Set(ByVal value As String)
            strUsuReg = value
        End Set
    End Property

    Public Sub CargaSistema(Optional ByVal intSist As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intSistema = intSist
            dtsRes = ManejaSistema(1)
            intSistema = 0
            If Trim$(strErrSistemas) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intSistema = dtsRes.Tables(0).Rows(0).Item("ID_CSISTEMA")
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("STATUS")
                    strDescripcion = dtsRes.Tables(0).Rows(0).Item("DESCRIPCION")
                Else
                    strErrSistemas = "No se encontró información para poder cargar los Sistemas"
                End If
            End If
        Catch ex As Exception
            strErrSistemas = ex.Message
        End Try
    End Sub

    Public Function ManejaSistema(ByVal intOper As Integer) As DataSet
        strErrSistemas = ""
        ManejaSistema = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta Sistema
                    If intSistema > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "ID_CSISTEMA", intSistema.ToString)
                    If Trim(strDescripcion) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "DESCRIPCION", strDescripcion)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "STATUS", intEstatus.ToString)
                Case 2 ' inserta Sistema
                    ArmaParametros(strParamStored, TipoDato.Cadena, "DESCRIPCION", strDescripcion)
                    ArmaParametros(strParamStored, TipoDato.Entero, "STATUS", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "USU_REG", strUsuReg)
                Case 3 ' actualiza Sistema
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_CSISTEMA", intSistema.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "STATUS", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "DESCRIPCION", strDescripcion)
                Case 4 ' borra Sistema
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_CSISTEMA", intSistema.ToString)
            End Select

            ManejaSistema = objSD.EjecutaStoredProcedure("spManejaCSISTEMA", strErrSistemas, strParamStored)
            If strErrSistemas = "" Then
                If intOper = 2 Then
                    intSistema = ManejaSistema.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("CSISTEMA", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrSistemas = ex.Message
        End Try
    End Function

End Class
