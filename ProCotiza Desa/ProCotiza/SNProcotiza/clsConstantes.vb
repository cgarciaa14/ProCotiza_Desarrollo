Imports SDManejaBD
'RQ-SEGIP : RHERNANDEZ: 19/07/17: SE CREA CLASE PARA GUARDAR LAS CONSTANTES DE LOS PRODUCTOS PARA SEGUROS POR FACTOR
'BUG-PC-105: RHERNANDEZ: 07/09/17: SE AGREGA VALOR CUOTA PARA LOS CALCULO DE SEGUROS POR FACTOR
Public Class clsConstantes
    Private strErrorConstantes As String = ""

    Private intOpcion As Integer = 0
    Private intID_Producto As Integer = 0
    Private intID_Aseguradora As Integer = 0
    Private Constante As Double = 0
    Private Cuota As Double = 0

    Sub New()
    End Sub

    Public ReadOnly Property ErrorConstante() As String
        Get
            Return strErrorConstantes
        End Get
    End Property

    Public Property opcion() As Integer
        Get
            Return intOpcion
        End Get
        Set(ByVal value As Integer)
            intOpcion = value
        End Set
    End Property
    Public Property ID_PRODUCTO() As Integer
        Get
            Return intID_Producto
        End Get
        Set(ByVal value As Integer)
            intID_Producto = value
        End Set
    End Property
    Public Property ID_ASEGURADORA() As Integer
        Get
            Return intID_Aseguradora
        End Get
        Set(ByVal value As Integer)
            intID_Aseguradora = value
        End Set
    End Property
    Public Property Constantes() As Double
        Get
            Return Constante
        End Get
        Set(ByVal value As Double)
            Constante = value
        End Set
    End Property
    Public Property ValCuota() As Double
        Get
            Return Cuota
        End Get
        Set(value As Double)
            Cuota = value
        End Set
    End Property


    Public Function ManejaConstantes() As DataSet
        strErrorConstantes = ""
        ManejaConstantes = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion



            Select Case opcion
                Case 1
                    ArmaParametros(strParamStored, TipoDato.Entero, "opcion", opcion.ToString)
                Case 2
                    ArmaParametros(strParamStored, TipoDato.Entero, "opcion", opcion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_PRODUCTO", ID_PRODUCTO.ToString)
                Case 3
                    ArmaParametros(strParamStored, TipoDato.Entero, "opcion", opcion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_PRODUCTO", ID_PRODUCTO.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_ASEGURADORA", ID_ASEGURADORA.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "CONSTANTE_VALUE", Constantes.ToString)
                    ArmaParametros(strParamStored, TipoDato.Doble, "CUOTA", Cuota.ToString)
                Case 4
                    ArmaParametros(strParamStored, TipoDato.Entero, "opcion", opcion.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_PRODUCTO", ID_PRODUCTO.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_ASEGURADORA", ID_ASEGURADORA.ToString)
            End Select
            ManejaConstantes = objSD.EjecutaStoredProcedure("spManejaConstantes", strErrorConstantes, strParamStored)
            If strErrorConstantes = "" Then
                'If opcion = 2 Then
                '    intProducto = ManejaProducto.Tables(0).Rows(0).Item(0)
                'End If

                'If intOper = 2 Or intOper = 3 Then
                '    GuardaLog("PRODUCTOS", strParamStored, IIf(intOper = 2, 117, 118))
                'End If
            End If

        Catch ex As Exception
            strErrorConstantes = ex.Message
        End Try
    End Function
End Class
