'BBV-P412 RQCOT-01.2: AMR 23/02/2017: Seguros.
'BUG-PC-51 MAPH 17/04/2017 CAMBIOS SOLICITADOS POR MARREDONDO
Imports SDManejaBD

Public Class clsAccesorios
    Inherits clsSession

    Private strErrAcc As String = ""

    Private intAccesorio As Integer = 0
    Private intMarca As Integer = 0
    Private intTipoProd As Integer = 0
    Private intEstatus As Integer = 0

    Private sngRegDef As Single = 0
    Private sngAfectaSeguro As Single = -1

    Private dblPrecio As Double = 0
    Private strNombre As String = ""
    Private strUsuReg As String = ""

    Sub New()
    End Sub
    Sub New(ByVal intCveacc As Integer)
        CargaAccesorio(intCveacc)
    End Sub

    Public ReadOnly Property ErrorAccesorios() As String
        Get
            Return strErrAcc
        End Get
    End Property

    Public WriteOnly Property UsuarioRegistro() As String
        Set(ByVal value As String)
            strUsuReg = value
        End Set
    End Property

    Public Property IDAccesorio() As Integer
        Get
            Return intAccesorio
        End Get
        Set(ByVal value As Integer)
            intAccesorio = value
        End Set
    End Property

    Public Property IDMarca() As Integer
        Get
            Return intMarca
        End Get
        Set(ByVal value As Integer)
            intMarca = value
        End Set
    End Property

    Public Property IDTipoProducto() As Integer
        Get
            Return intTipoProd
        End Get
        Set(ByVal value As Integer)
            intTipoProd = value
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

    Public Property AfectaCalculoSeguro() As Single
        Get
            Return sngAfectaSeguro
        End Get
        Set(ByVal value As Single)
            sngAfectaSeguro = value
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

    Public Property Descripcion() As String
        Get
            Return strNombre
        End Get
        Set(ByVal value As String)
            strNombre = value
        End Set
    End Property

    Public Property Precio() As Double
        Get
            Return dblPrecio
        End Get
        Set(ByVal value As Double)
            dblPrecio = value
        End Set
    End Property

    Public Sub CargaAccesorio(Optional ByVal intAcc As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            intAccesorio = intAcc
            dtsRes = ManejaAccesorio(1)
            intAccesorio = 0
            If Trim$(strErrAcc) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    intAccesorio = dtsRes.Tables(0).Rows(0).Item("ID_ACCESORIO")
                    intMarca = dtsRes.Tables(0).Rows(0).Item("ID_MARCA")
                    intTipoProd = dtsRes.Tables(0).Rows(0).Item("ID_TIPO_PRODUCTO")
                    intEstatus = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                    sngRegDef = dtsRes.Tables(0).Rows(0).Item("REG_DEFAULT")
                    strNombre = dtsRes.Tables(0).Rows(0).Item("DESCRIPCION")
                    dblPrecio = dtsRes.Tables(0).Rows(0).Item("PRECIO")
                    sngAfectaSeguro = dtsRes.Tables(0).Rows(0).Item("AFECTA_CALCULO_SEGURO")
                Else
                    strErrAcc = "No se encontró información para poder cargar la marca"
                End If
            End If
        Catch ex As Exception
            strErrAcc = ex.Message
        End Try
    End Sub

    Public Function ManejaAccesorio(ByVal intOper As Integer) As DataSet
        strErrAcc = ""
        ManejaAccesorio = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta 
                    If intAccesorio > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAccesorio", intAccesorio.ToString)
                    If intMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    If intTipoProd > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idTipoProd", intTipoProd.ToString)
                    If intEstatus > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    If sngAfectaSeguro > -1 Then ArmaParametros(strParamStored, TipoDato.Entero, "afectaCalcSeg", sngAfectaSeguro.ToString)
                    If Trim(strNombre) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "descrip", strNombre)
                Case 2 ' inserta 
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoProd", intTipoProd.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "afectaCalcSeg", sngAfectaSeguro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "descrip", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Doble, "precio", dblPrecio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 3 ' actualiza
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAccesorio", intAccesorio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idTipoProd", intTipoProd.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "afectaCalcSeg", sngAfectaSeguro.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "default", sngRegDef.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "descrip", strNombre)
                    ArmaParametros(strParamStored, TipoDato.Doble, "precio", dblPrecio.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", strUsuReg)
                Case 4 ' borra 
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAccesorio", intAccesorio.ToString)
                Case 6 'Consulta Garantia Extendida
                    If intAccesorio > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idAccesorio", intAccesorio.ToString)
                    If intMarca > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", intMarca.ToString)
                    If intEstatus >= 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "estat", intEstatus.ToString)
            End Select

            ManejaAccesorio = objSD.EjecutaStoredProcedure("spManejaAccesorios", strErrAcc, strParamStored)
            If strErrAcc = "" Then
                If intOper = 2 Then
                    intAccesorio = ManejaAccesorio.Tables(0).Rows(0).Item(0)
                End If

                If intOper = 2 Or intOper = 3 Then
                    GuardaLog("ACCESORIOS", strParamStored, IIf(intOper = 2, 117, 118))
                End If
            End If

        Catch ex As Exception
            strErrAcc = ex.Message
        End Try
    End Function
End Class