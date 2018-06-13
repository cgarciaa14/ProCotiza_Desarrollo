'BUG-PC-192: DCORNEJO  07/05/2018: Se crea la clase para obtener datos de base de datos para CotSegOrdas.
Imports SDManejaBD
Public Class clsDatosOrdas
#Region "Properties"
    Private strError As String = String.Empty
    Public Property ErrorSeguroOrdas() As String
        Get
            Return strError
        End Get
        Set(ByVal value As String)
            strError = value
        End Set
    End Property

    Private alianzaId As Integer
    Private accessoryDescription As String
    Private agencyNumber As String
    Private idAdditionalPaymentWay As Integer
    Private idAdditionalTerm As Integer
    Private idAdditionalPack As Integer
    Private generateSeveralTerms As Integer
    Private idPaymentType As Integer
    Private age As Integer
    Private gender As Integer
    Private idProduct As Integer
    Private insurerId As String
    Private state As Integer
    Private zipCode As String
    Public Property IdAlianza() As Integer
        Get
            Return alianzaId
        End Get
        Set(ByVal value As Integer)
            alianzaId = value
        End Set
    End Property
    Public Property NumberAgency() As String
        Get
            Return agencyNumber
        End Get
        Set(ByVal value As String)
            agencyNumber = value
        End Set
    End Property
    Public Property Descriptionaccessory() As String
        Get
            Return accessoryDescription
        End Get
        Set(ByVal value As String)
            accessoryDescription = value
        End Set
    End Property
    Public Property AdditionalPaymentWayid() As Integer
        Get
            Return idAdditionalPaymentWay
        End Get
        Set(ByVal value As Integer)
            idAdditionalPaymentWay = value
        End Set
    End Property

    Public Property AdditionalTermid() As Integer
        Get
            Return idAdditionalTerm
        End Get
        Set(ByVal value As Integer)
            idAdditionalTerm = value
        End Set
    End Property
    Public Property AdditionalPackid() As Integer
        Get
            Return idAdditionalPack
        End Get
        Set(ByVal value As Integer)
            idAdditionalPack = value
        End Set
    End Property
    Public Property SeveralTermsgenerate() As Integer
        Get
            Return generateSeveralTerms
        End Get
        Set(ByVal value As Integer)
            generateSeveralTerms = value
        End Set
    End Property
    Public Property PaymentTypeid() As Integer
        Get
            Return idPaymentType
        End Get
        Set(ByVal value As Integer)
            idPaymentType = value
        End Set
    End Property
    Public Property Edad() As Integer
        Get
            Return age
        End Get
        Set(ByVal value As Integer)
            age = value
        End Set
    End Property
    Public Property Genero() As Integer
        Get
            Return gender
        End Get
        Set(ByVal value As Integer)
            gender = value
        End Set
    End Property
    Public Property Productoid() As Integer
        Get
            Return idProduct
        End Get
        Set(ByVal value As Integer)
            idProduct = value
        End Set
    End Property
    Public Property Idinsurer() As String
        Get
            Return insurerId
        End Get
        Set(ByVal value As String)
            insurerId = value
        End Set
    End Property
    Public Property Estado() As Integer
        Get
            Return state
        End Get
        Set(ByVal value As Integer)
            state = value
        End Set
    End Property
    Public Property CodigoPostal() As String
        Get
            Return zipCode
        End Get
        Set(ByVal value As String)
            zipCode = value
        End Set
    End Property
#End Region

    Public Function ObtenDatosOrdas(ByVal opcion As Integer) As DataSet
        Dim objSD As New clsConexion
        Dim strParamStored As String = String.Empty
        Dim strErrBD As String = String.Empty

        ObtenDatosOrdas = New DataSet
        Try

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", opcion.ToString)

            Select Case opcion
                Case 1
                    ArmaParametros(strParamStored, TipoDato.Entero, "alianzaId", alianzaId)
                Case 2
                    ArmaParametros(strParamStored, TipoDato.Entero, "alianzaId", alianzaId)

            End Select

            ObtenDatosOrdas = objSD.EjecutaStoredProcedure("spDatosSeguros", strErrBD, strParamStored)

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
