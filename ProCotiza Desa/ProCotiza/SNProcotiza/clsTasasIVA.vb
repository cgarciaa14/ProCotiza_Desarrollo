Imports SDManejaBD

Public Class clsTasasIVA
    Inherits clsSession

    Private strErrTasasIVA As String = ""

    Private intTasaIVA As Integer = 0
    Private intEstatus As Integer = 0
    Private sngRegDef As Single = 0

    Private dblTasa As Double = 0
    Private strNombre As String = ""
    Private strUsuReg As String = ""

    Sub New()
    End Sub
    Sub New(ByVal intCveTasaIVA As Integer)
        CargaTasaIVA(intCveTasaIVA)
    End Sub

    Public ReadOnly Property ErrorTasasIVA() As String
        Get
            Return strErrTasasIVA
        End Get
    End Property

    Public WriteOnly Property UsuarioRegistro() As String
        Set(ByVal value As String)
            strUsuReg = value
        End Set
    End Property

    Public Property IDTasaIVA() As Integer
        Get
            Return intTasaIVA
        End Get
        Set(ByVal value As Integer)
            intTasaIVA = value
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

    Public Property ValorTasa() As Double
        Get
            Return dblTasa
        End Get
        Set(ByVal value As Double)
            dblTasa = value
        End Set
    End Property

    Public Property RegistroDefault() As Single
        Get
            Return sngRegDef
        End Get
        Set(ByVal value As Single)
            sngRegDef = value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return strNombre
        End Get
        Set(ByVal value As String)
            strNombre = value
        End Set
    End Property

    Public Sub CargaTasaIVA(Optional ByVal intTIva As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intTasaIVA = intTIva
            dtsRes = ManejaTasaIVA(1)
            intTasaIVA = 0
            If Trim$(strErrTasasIVA) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intTasaIVA = dtsRes.Tables(0).Rows(0).Item("ID_TASA_IVA")
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    sngRegDef = dtsRes.Tables(0).Rows(0).Item("REG_DEFAULT")
                    strNombre = dtsRes.Tables(0).Rows(0).Item("NOMBRE")
                    dblTasa = dtsRes.Tables(0).Rows(0).Item("TASA")
                Else
                    strErrTasasIVA = "No se encontró información para poder cargar la tasa de IVA"
                End If
            End If
        Catch ex As Exception
            strErrTasasIVA = ex.Message
        End Try
    End Sub

    Public Function ManejaTasaIVA(ByVal intOper As Integer) As DataSet
        strErrTasasIVA = ""
        ManejaTasaIVA = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta TasaIVA
                    If intTasaIVA > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTasaIVA", intTasaIVA.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If Trim(strNombre) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                Case 2 ' inserta TasaIVA
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Doble, "tasa", dblTasa.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 3 ' actualiza TasaIVA
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTasaIVA", intTasaIVA.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "nom", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Doble, "tasa", dblTasa.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 4 ' borra TasaIVA
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTasaIVA", intTasaIVA.ToString)
            End Select

            ManejaTasaIVA = objSD.EjecutaStoredProcedure("spManejaTasasIVA", strErrTasasIVA, strParamStored)
            If strErrTasasIVA = "" Then
                If intOper = 2 Then
                    intTasaIVA = ManejaTasaIVA.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("TASAS_IVA", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrTasasIVA = ex.Message
        End Try
    End Function
End Class
