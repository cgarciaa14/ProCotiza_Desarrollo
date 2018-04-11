'BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.
Imports SDManejaBD

Public Class clsPlazo
    Inherits clsSession

#Region "Variables"
    Public _strErrPlazo As String = ""

    Private _idplazo As Integer = 0
    Private _status As Integer
    Private _valor As String

    Private _nombre As String = ""
    Private _usu_reg As String = ""
    Private _plazoFeMto As Date
    Private _id_periodicidad As Integer = 0
    Private _idPaquete As Integer = 0
#End Region

#Region "Propiedades"

    Sub New()
    End Sub
    Sub New(ByVal intCvePlazo As Integer)
        CargaPlazo(intCvePlazo)
    End Sub

    Public Property Valor() As String
        Get
            Return _valor
        End Get
        Set(ByVal value As String)
            _valor = value
        End Set
    End Property

    Public Property StrErrPlazo() As String
        Get
            Return _strErrPlazo
        End Get
        Set(ByVal value As String)
            _strErrPlazo = value
        End Set
    End Property

    Public Property Id_Plazo() As Integer
        Get
            Return _idplazo
        End Get
        Set(ByVal value As Integer)
            _idplazo = value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Public Property Status() As Integer
        Get
            Return _status
        End Get
        Set(ByVal value As Integer)
            _status = value
        End Set
    End Property

    Public Property PlazoFeMto() As Date
        Get
            Return _plazoFeMto
        End Get
        Set(ByVal value As Date)
            _plazoFeMto = value
        End Set
    End Property

    Public Property Usu_Reg() As String
        Get
            Return _usu_reg
        End Get
        Set(ByVal value As String)
            _usu_reg = value
        End Set
    End Property

    Public Property Id_Periodicidad() As Integer
        Get
            Return _id_periodicidad
        End Get
        Set(value As Integer)
            _id_periodicidad = value
        End Set
    End Property

    Public Property IDPaquete() As Integer
        Get
            Return _idPaquete
        End Get
        Set(value As Integer)
            _idPaquete = value
        End Set
    End Property

#End Region

    Public Function ManejaPlazos(ByVal intOper As Integer) As DataSet

        StrErrPlazo = ""
        ManejaPlazos = New DataSet
        Dim strParamStored As String = ""
        Try
            Dim objSD As New clsConexion
            ArmaParametros(strParamStored, TipoDato.Entero, "opcion", intOper.ToString)

            Select Case intOper
                Case 1 'Selecciona Plazo    
                    If Id_Plazo > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "Id_Plazo", _idplazo.ToString())
                    If _valor > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "Valor", _valor.ToString())
                    If _id_periodicidad > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "ID_PERIODICIDAD", _id_periodicidad.ToString())
                    If _status > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "status", _status.ToString())
                Case 2 'Inserta Plazo
                    If Trim(_nombre) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "NOMBRE", _nombre.ToString())
                    If _valor > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "valor", _valor.ToString())
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_PERIODICIDAD", _id_periodicidad.ToString())
                    If _status > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "status", _status.ToString())
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", _usu_reg)
                Case 3  'Actualiza Plazo    
                    If Trim(_nombre) <> "" Then ArmaParametros(strParamStored, TipoDato.Cadena, "NOMBRE", _nombre.ToString())
                    If _status > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "status", _status.ToString())
                    If Id_Plazo > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "Id_Plazo", _idplazo.ToString())
                    If _valor > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "Valor", _valor.ToString())
                    ArmaParametros(strParamStored, TipoDato.Entero, "ID_PERIODICIDAD", _id_periodicidad.ToString())
                    ArmaParametros(strParamStored, TipoDato.Cadena, "usu_reg", _usu_reg)
                Case 4 'Elimina Plazo
                    If _idplazo > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "Id_Plazo", _idplazo.ToString())
                Case 5 'Consulta cotizador
                    If _idPaquete > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "IDPAQUETE", _idPaquete.ToString())
                Case 6 'Consulta cotizador
                    If _idPaquete > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "IDPAQUETE", _idPaquete.ToString())
                    If Id_Plazo > 0 Then ArmaParametros(strParamStored, TipoDato.Entero, "Id_Plazo", _idplazo.ToString())
            End Select
            ManejaPlazos = objSD.EjecutaStoredProcedure("spManejaPlazos", StrErrPlazo, strParamStored)
        Catch ex As ApplicationException
            StrErrPlazo = ex.Message
        End Try
        Return ManejaPlazos
    End Function

    Public Sub CargaPlazo(Optional ByVal intPlazo As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            _idplazo = intPlazo
            dtsRes = ManejaPlazos(1)
            intPlazo = 0
            If Trim$(StrErrPlazo) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    Id_Plazo = dtsRes.Tables(0).Rows(0).Item("ID_PLAZO")
                    Nombre = dtsRes.Tables(0).Rows(0).Item("NOMBRE")
                    Valor = dtsRes.Tables(0).Rows(0).Item("VALOR")
                    Id_Periodicidad = dtsRes.Tables(0).Rows(0).Item("ID_PERIODICIDAD")
                    Status = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                Else
                    StrErrPlazo = "No se encontró información para poder cargar el Plazo"
                End If
            End If
        Catch ex As Exception
            StrErrPlazo = ex.Message
        End Try
    End Sub

    Public Sub CargaPlazoxValor(Optional ByVal intPlazoValor As Integer = 0)
        Dim dtsRes As New DataSet
        Try

            _valor = intPlazoValor
            dtsRes = ManejaPlazos(5)
            intPlazoValor = 0
            If Trim$(StrErrPlazo) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    Id_Plazo = dtsRes.Tables(0).Rows(0).Item("ID_PLAZO")
                    Nombre = dtsRes.Tables(0).Rows(0).Item("NOMBRE")
                    Valor = dtsRes.Tables(0).Rows(0).Item("VALOR")
                    Id_Periodicidad = dtsRes.Tables(0).Rows(0).Item("ID_PERIODICIDAD")
                    Status = dtsRes.Tables(0).Rows(0).Item("ESTATUS")
                Else
                    StrErrPlazo = "No se encontró información para poder cargar el estado"
                End If
            End If
        Catch ex As Exception
            StrErrPlazo = ex.Message
        End Try
    End Sub

End Class

