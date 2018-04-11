Imports SDManejaBD

Public Class clsPromocionesCte
    Inherits clsSession

    Private strErrPromocion As String = String.Empty

    Private intID_PROMOCION As Integer = 0
    Private strDESCRIPCION As String = String.Empty
    Private intESTATUS As Integer = 0
    Private strUSU_REG As String = String.Empty
    Private intID_EXTERNO As Integer = 0
    Private intIDCLIENTE As Integer = 0
    Private intIDPERIODICIDAD As Integer = 0
    Private strPERIODICIDAD As String = String.Empty


    Public ReadOnly Property ErrorPromociono() As String
        Get
            Return strErrPromocion
        End Get
    End Property

    Sub New()
    End Sub

    Sub New(ByVal intIdPromo As Integer)
        CargaPromocion(intIdPromo)
    End Sub

    Public Property UsuarioRegistro() As String
        Get
            Return strUSU_REG
        End Get
        Set(ByVal value As String)
            strUSU_REG = value
        End Set
    End Property


    Public Property IDPROMOCION() As Integer
        Get
            Return intID_PROMOCION
        End Get
        Set(ByVal value As Integer)
            intID_PROMOCION = value
        End Set

    End Property

    Public Property DESCRIPCION() As String
        Get
            Return strDESCRIPCION
        End Get
        Set(ByVal value As String)
            strDESCRIPCION = value
        End Set
    End Property

    Public Property IDESTATUS() As Integer
        Get
            Return intESTATUS
        End Get
        Set(ByVal value As Integer)
            intESTATUS = value
        End Set
    End Property

    Public Property IDEXTERNO() As Integer
        Get
            Return intID_EXTERNO
        End Get
        Set(ByVal value As Integer)
            intID_EXTERNO = value
        End Set

    End Property

    Public Property IDCLIENTE() As Integer
        Get
            Return intIDCLIENTE
        End Get
        Set(ByVal value As Integer)
            intIDCLIENTE = value
        End Set
    End Property

    Public Property IDPERIODICIDAD() As Integer
        Get
            Return intIDPERIODICIDAD
        End Get
        Set(value As Integer)
            intIDPERIODICIDAD = value
        End Set
    End Property

    Public Property PERIODICIDAD() As String
        Get
            Return strPERIODICIDAD
        End Get
        Set(value As String)
            strPERIODICIDAD = value
        End Set
    End Property

    Public Sub CargaPromocion(Optional ByVal intPromo As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intID_PROMOCION = intPromo
            dtsRes = ManejaPromocion(1)
            intID_PROMOCION = 0
            If Trim$(strErrPromocion) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intID_PROMOCION = dtsRes.Tables(0).Rows(0).Item("ID_PROMOCION")
                    strDESCRIPCION = dtsRes.Tables(0).Rows(0).Item("DESCRIPCION")
                    intESTATUS = dtsRes.Tables(0).Rows(0).Item("ID_ESTATUS")
                    intID_EXTERNO = dtsRes.Tables(0).Rows(0).Item("ID_EXTERNO")
                    intIDCLIENTE = dtsRes.Tables(0).Rows(0).Item("ID_CLIENTE")
                    intIDPERIODICIDAD = dtsRes.Tables(0).Rows(0).Item("ID_PERIODICIDAD")
                    strPERIODICIDAD = dtsRes.Tables(0).Rows(0).Item("PERIODICIDAD")
                Else
                    strErrPromocion = "No se encontró información para poder cargar la promoción"
                End If
            End If
        Catch ex As Exception
            strErrPromocion = ex.Message
        End Try
    End Sub

    Public Function ManejaPromocion(ByVal intPromo As Integer) As DataSet
        strErrPromocion = ""
        ManejaPromocion = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intPromo.ToString)
            Select Case intPromo
                Case 1 ' consulta promocion
                    If intID_PROMOCION > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "ID_PROMOCION", intID_PROMOCION.ToString)
                    If Trim(strDESCRIPCION) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "DESCRIPCION", strDESCRIPCION)
                    If intESTATUS > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "ESTATUS", intESTATUS.ToString)
                    If Trim(strUSU_REG) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "USU_REG", strUSU_REG)
                    If intID_EXTERNO > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "ID_EXTERNO", intID_EXTERNO.ToString)
                    If intIDCLIENTE > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "IDCLIENTE", intIDCLIENTE.ToString)
                Case 2 ' inserta producto
                    ArmaParametros(strParamStored, TipoDato.Cadena, "DESCRIPCION", strDESCRIPCION)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ESTATUS", intESTATUS.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "USU_REG", strUSU_REG)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_EXTERNO", intID_EXTERNO.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "IDCLIENTE", intIDCLIENTE.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "IDPERIODICIDAD", intIDPERIODICIDAD.ToString)
                Case 3 ' actualiza producto
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_PROMOCION", intID_PROMOCION.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "DESCRIPCION", strDESCRIPCION)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ESTATUS", intESTATUS.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "USU_REG", strUSU_REG)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_EXTERNO", intID_EXTERNO.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "IDCLIENTE", intIDCLIENTE.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "IDPERIODICIDAD", intIDPERIODICIDAD.ToString)
                Case 4 ' borra producto
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_PROMOCION", intID_PROMOCION.ToString)
            End Select

            ManejaPromocion = objSD.EjecutaStoredProcedure("spManejaCpromociones", strErrPromocion, strParamStored)
            If strErrPromocion = "" Then
                If intPromo = 2 Then
                    If ManejaPromocion.Tables(0).Rows(0).Item(0).ToString = "ERROR" Then
                        strErrPromocion = "El IDPromocion ya existe."
                    Else
                        intID_PROMOCION = ManejaPromocion.Tables(0).Rows(0).Item(0)
                    End If
                End If

                If intPromo = 2 Or intPromo = 3 Then
                    GuardaLog("CPROMOCIONES", strParamStored, IIf(intPromo = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrPromocion = ex.Message
        End Try
    End Function
End Class
