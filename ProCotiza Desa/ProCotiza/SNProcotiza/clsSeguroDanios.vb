'BUG-PC-148: JMENDIETA  29/01/2018:Se crea la clase que consulta informacion en base de datos para seguro de daños
'BUG-PC-171: CGARCIA: 02/04/2018: SE AGREGAN DOS VARIABLES DE CONSULTA
'BUG-PC-190: JMENDIETA: 10/05/2018: En opcion 1 se agrega parametros de broker y agencia
Imports SDManejaBD

Public Class clsSeguroDanios

#Region "Properties"

    Private strError As String = String.Empty
    Private strCoverage As String = String.Empty
    Private brokerId As Integer
    Private aseguradoraId As Integer
    Private strBrokerAseguradora As String
    Private paqueteId As Integer
    Private _TipoSeg As Integer
    Private _TipoProd As Integer
    Private _IdAgencia As Integer 'BUG-PC-190

    Public Property ErrorSeguroDanios() As String
        Get
            Return strError
        End Get
        Set(ByVal value As String)
            strError = value
        End Set
    End Property

    Public Property TipoSeg As Integer
        Get
            Return _TipoSeg
        End Get
        Set(value As Integer)
            _TipoSeg = value
        End Set
    End Property

    Public Property TipoProd As Integer
        Get
            Return _TipoProd
        End Get
        Set(value As Integer)
            _TipoProd = value
        End Set
    End Property

    Public Property Coverage() As String
        Get
            Return strCoverage
        End Get
        Set(ByVal value As String)
            strCoverage = value
        End Set
    End Property

    Public Property IdBroker() As Integer
        Get
            Return brokerId
        End Get
        Set(ByVal value As Integer)
            brokerId = value
        End Set
    End Property

    Public Property IdAseguradora() As Integer
        Get
            Return aseguradoraId
        End Get
        Set(ByVal value As Integer)
            aseguradoraId = value
        End Set
    End Property

    Public Property IdPaquete() As Integer
        Get
            Return paqueteId
        End Get
        Set(ByVal value As Integer)
            paqueteId = value
        End Set
    End Property

    'BUG-PC-190
    Public Property IdAgencia() As Integer
        Get
            Return _IdAgencia
        End Get
        Set(ByVal value As Integer)
            _IdAgencia = value
        End Set
    End Property
#End Region

    Public Function ObtenDatosDanios(ByVal opcion As Integer) As DataSet
        Dim objSD As New clsConexion
        Dim strParamStored As String = String.Empty
        Dim strErrBD As String = String.Empty

        ObtenDatosDanios = New DataSet
        Try

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", opcion.ToString)

            'Opcion 1 no necesita parametros adicionales
            Select Case opcion
                Case 1
                    ArmaParametros(strParamStored, TipoDato.Entero, "TipoProd", _TipoProd)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idBroker", brokerId.ToString) 'BUG-PC-190
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAgencia", _IdAgencia)
                Case 2
                    ArmaParametros(strParamStored, TipoDato.Entero, "idCobertura", strCoverage)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idBroker", brokerId.ToString)
                Case 3
                    ArmaParametros(strParamStored, TipoDato.Entero, "idBroker", brokerId.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAseguradora", aseguradoraId.ToString)
                Case 4
                    ArmaParametros(strParamStored, TipoDato.Cadena, "idPaquete", paqueteId)
                Case 5
                    ArmaParametros(strParamStored, TipoDato.Entero, "TipoSeg", _TipoSeg)
            End Select

            ObtenDatosDanios = objSD.EjecutaStoredProcedure("SpConsultaDatosDanios", strErrBD, strParamStored)
            If Not (String.IsNullOrEmpty(objSD.ErrorConexion)) Then
                strError = objSD.ErrorConexion
            End If
            If Not (String.IsNullOrEmpty(strErrBD)) Then
                strError = strErrBD
            End If
        Catch ex As Exception
            strError = ex.Message
        End Try
    End Function

End Class
