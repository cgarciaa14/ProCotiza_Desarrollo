'RQ-PC5: JMENDIETA  27/01/2018: Se crea la clase para obtener datos de base de datos para Marsh Multicotizador.
'BUG-PC-177: JMENDIETA: 16/04/2017 Se agrega opcion 8 y propiedad de codigo postal.
Imports SDManejaBD
Public Class clsDatosMarshMulti

#Region "Properties"
    Private strError As String = String.Empty

    Public Property ErrorSeguroDanios() As String
        Get
            Return strError
        End Get
        Set(ByVal value As String)
            strError = value
        End Set
    End Property

    Private brokerId As Integer
    Private useType As Integer
    Private carType As Integer
    Private policyType As Integer
    Private idClasif As Integer
    Private idMarca As Integer
    Private idSubmarca As Integer
    Private idVersion As Integer
    Private idAnio As Integer
    Private asegId As Integer
    Private idPaquete As Integer
    Private idParametro As Integer
    Private _CodigoPostal As String



    Public Property IdBroker() As Integer
        Get
            Return brokerId
        End Get
        Set(ByVal value As Integer)
            brokerId = value
        End Set
    End Property

    Public Property UseTypeId() As Integer
        Get
            Return useType
        End Get
        Set(ByVal value As Integer)
            useType = value
        End Set
    End Property

    Public Property CarTypeId() As Integer
        Get
            Return carType
        End Get
        Set(ByVal value As Integer)
            carType = value
        End Set
    End Property

    Public Property PolicyTypeId() As Integer
        Get
            Return policyType
        End Get
        Set(ByVal value As Integer)
            policyType = value
        End Set
    End Property

    Public Property ClasificacionId() As Integer
        Get
            Return idClasif
        End Get
        Set(ByVal value As Integer)
            idClasif = value
        End Set
    End Property

    Public Property MarcaId() As Integer
        Get
            Return idMarca
        End Get
        Set(ByVal value As Integer)
            idMarca = value
        End Set
    End Property

    Public Property SubmarcaId() As Integer
        Get
            Return idSubmarca
        End Get
        Set(ByVal value As Integer)
            idSubmarca = value
        End Set
    End Property

    Public Property VersionId() As Integer
        Get
            Return idVersion
        End Get
        Set(ByVal value As Integer)
            idVersion = value
        End Set
    End Property

    Public Property AnioId() As Integer
        Get
            Return idAnio
        End Get
        Set(ByVal value As Integer)
            idAnio = value
        End Set
    End Property

    Public Property AseguradoraId() As Integer
        Get
            Return asegId
        End Get
        Set(ByVal value As Integer)
            asegId = value
        End Set
    End Property

    Public Property PaqueteId() As Integer
        Get
            Return idPaquete
        End Get
        Set(ByVal value As Integer)
            idPaquete = value
        End Set
    End Property

    Public Property ParametroId() As Integer
        Get
            Return idParametro
        End Get
        Set(ByVal value As Integer)
            idParametro = value
        End Set
    End Property

    Public Property CodigoPostal() As String
        Get
            Return _CodigoPostal
        End Get
        Set(ByVal value As String)
            _CodigoPostal = value
        End Set
    End Property

#End Region




    Public Function ObtenDatosMarsh(ByVal opcion As Integer) As DataSet
        Dim objSD As New clsConexion
        Dim strParamStored As String = String.Empty
        Dim strErrBD As String = String.Empty

        ObtenDatosMarsh = New DataSet
        Try

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", opcion.ToString)

            Select Case opcion
                Case 1
                    ArmaParametros(strParamStored, TipoDato.Entero, "brokerId", brokerId)
                    ArmaParametros(strParamStored, TipoDato.Entero, "useType", useType)
                Case 2
                    ArmaParametros(strParamStored, TipoDato.Entero, "brokerId", brokerId)
                    ArmaParametros(strParamStored, TipoDato.Entero, "carType", carType)
                Case 3
                    ArmaParametros(strParamStored, TipoDato.Entero, "brokerId", brokerId)
                    ArmaParametros(strParamStored, TipoDato.Entero, "policyType", policyType)
                Case 4
                    ArmaParametros(strParamStored, TipoDato.Entero, "idClasif", idClasif)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idMarca", idMarca)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idSubmarca", idSubmarca)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idVersion", idVersion)
                    ArmaParametros(strParamStored, TipoDato.Entero, "idAnio", idAnio)
                Case 5
                    ArmaParametros(strParamStored, TipoDato.Entero, "brokerId", brokerId)
                    ArmaParametros(strParamStored, TipoDato.Entero, "asegId", asegId)
                Case 6
                    ArmaParametros(strParamStored, TipoDato.Entero, "idPaquete", idPaquete)
                Case 7
                    ArmaParametros(strParamStored, TipoDato.Entero, "idParametro", idParametro)
                Case 8
                    ArmaParametros(strParamStored, TipoDato.Cadena, "codigoPostal", _CodigoPostal) 'BUG-PC-177

            End Select

            ObtenDatosMarsh = objSD.EjecutaStoredProcedure("SpConsultaDatosMarshV02", strErrBD, strParamStored)

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
