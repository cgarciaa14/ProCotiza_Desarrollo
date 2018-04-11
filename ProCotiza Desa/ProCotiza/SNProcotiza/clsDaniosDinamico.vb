'BUG-PC-166: JMENDIETA: 09/03/2018: Se crea la clase que manejara los datos de deducibles de una cotización.

Imports SDManejaBD

Public Class clsDaniosDinamico
#Region "Properties"
    Private strError As String = String.Empty

    Private idCotizacion As String
    Private idCobertura As String
    Private descCobertura As String
    Private idDeducDanios As String
    Private desDeducDanios As String
    Private idDeducDaniosRobo As String
    Private descDeducDaniosRobo As String
    Private idIndemnizacion As String
    Private descIndemnizacion As String

    Public Property CotizacionId() As String
        Get
            Return idCotizacion
        End Get
        Set(ByVal value As String)
            idCotizacion = value
        End Set
    End Property

    Public Property CoberturaId() As String
        Get
            Return idCobertura
        End Get
        Set(ByVal value As String)
            idCobertura = value
        End Set
    End Property

    Public Property CoberturaDesc() As String
        Get
            Return descCobertura
        End Get
        Set(ByVal value As String)
            descCobertura = value
        End Set
    End Property

    Public Property DeducDaniosId() As String
        Get
            Return idDeducDanios
        End Get
        Set(ByVal value As String)
            idDeducDanios = value
        End Set
    End Property

    Public Property DeducDaniosDesc() As String
        Get
            Return desDeducDanios
        End Get
        Set(ByVal value As String)
            desDeducDanios = value
        End Set
    End Property

    Public Property DeducDaniosRoboId As String
        Get
            Return idDeducDaniosRobo
        End Get
        Set(ByVal value As String)
            idDeducDaniosRobo = value
        End Set
    End Property

    Public Property DeducDaniosRoboDesc() As String
        Get
            Return descDeducDaniosRobo
        End Get
        Set(ByVal value As String)
            descDeducDaniosRobo = value
        End Set
    End Property

    Public Property IndemnizacionId() As String
        Get
            Return idIndemnizacion
        End Get
        Set(ByVal value As String)
            idIndemnizacion = value
        End Set
    End Property

    Public Property IndemnizacionDesc() As String
        Get
            Return descIndemnizacion
        End Get
        Set(ByVal value As String)
            descIndemnizacion = value
        End Set
    End Property

#End Region



    Public Function ManeDaniosDinamico(ByVal opcion As Integer) As DataSet
        Dim objSD As New clsConexion
        Dim strParamStored As String = String.Empty
        Dim strErrBD As String = String.Empty

        ManeDaniosDinamico = New DataSet
        Try

            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", opcion.ToString)

            Select Case opcion
                Case 1
                    ArmaParametros(strParamStored, TipoDato.Cadena, "idCotizacion", idCotizacion)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "idCobertura", idCobertura)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "descCobertura", descCobertura)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "idDeducDanios", idDeducDanios)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "desDeducDanios", desDeducDanios)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "idDeducDaniosRobo", idDeducDaniosRobo)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "descDeducDaniosRobo", descDeducDaniosRobo)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "idIndemnizacion", idIndemnizacion)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "descIndemnizacion", descIndemnizacion)
            End Select

            ManeDaniosDinamico = objSD.EjecutaStoredProcedure("spDatosDaniosDinamico", strErrBD, strParamStored)

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
