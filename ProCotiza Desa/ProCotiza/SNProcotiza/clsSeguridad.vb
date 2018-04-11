' BBV-P-412  RQ WSD  gvargas   10/25/2016 Modificado ValidaUsuario() permitiendo ingresar por otro portal externo de Login

Imports SDManejaBD

Public Class clsSeguridad

    Inherits clsSession

    Private strErrSeg As String = ""
    Private patron_busqueda As String = "bqhñ5jCAEyFG9IJKoLM2NRPSTW4X7Y0ZwaVcDÑefgiklmdnrpOustvxz13Q68BHU"
    Private Patron_encripta As String = "7SÑC6o9XDGHIJKLNUPQBRTVAWYZac3deghijklmbn12ñrqFustvMxyz4f580wOEp"

    Public Sub New()
    End Sub

    Public Sub New(ByVal intIDAcceso As Integer)
        CargaSession(intIDAcceso)
    End Sub

    Public ReadOnly Property ErrorSeguridad() As String
        Get
            Return strErrSeg
        End Get
    End Property

    Public Function EncriptarCadena(ByVal strCadena As String) As String

        EncriptarCadena = ""
        Dim strErrEncripta As String = String.Empty
        Dim dts = New DataSet
        Dim strParamStored As String = ""

        Try
            Dim objSD As New clsConexion
            ArmaParametros(strParamStored, TipoDato.Cadena, "sCad", strCadena)
            ArmaParametros(strParamStored, TipoDato.Cadena, "sretorno", "")

            dts = objSD.EjecutaStoredProcedure("SEC_Encripta", strErrEncripta, strParamStored)

            If strErrEncripta = "" Then
                EncriptarCadena = dts.Tables(0).Rows(0).Item(0).ToString
            End If

        Catch ex As Exception
            strErrEncripta = ex.Message
        End Try

    End Function

    Public Function EncriptarCaracter(ByVal strCar As String, _
                                       ByVal intVar As Integer, _
                                       ByVal intInd As Integer) As String
        Dim intIndex As Integer
        EncriptarCaracter = ""

        If patron_busqueda.IndexOf(strCar) <> -1 Then
            intIndex = (patron_busqueda.IndexOf(strCar) + intVar + intInd) Mod patron_busqueda.Length
            EncriptarCaracter = Patron_encripta.Substring(intIndex, 1)
        Else
            EncriptarCaracter = strCar
        End If
    End Function

    Public Function DesEncriptarCadena(ByVal cadena As String) As String
        Dim idx As Integer
        Dim result As String = ""

        For idx = 0 To cadena.Length - 1
            result += DesEncriptarCaracter(cadena.Substring(idx, 1), cadena.Length, idx)
        Next
        Return result
    End Function

    Public Function DesEncriptarCaracter(ByVal caracter As String, _
                                         ByVal variable As Integer, _
                                         ByVal a_indice As Integer) As String
        Dim indice As Integer

        If Patron_encripta.IndexOf(caracter) <> -1 Then
            If (Patron_encripta.IndexOf(caracter) - variable - a_indice) > 0 Then
                indice = (Patron_encripta.IndexOf(caracter) - variable - a_indice) Mod Patron_encripta.Length
            Else
                indice = (patron_busqueda.Length) + ((Patron_encripta.IndexOf(caracter) - variable - a_indice) Mod Patron_encripta.Length)
            End If
            indice = indice Mod Patron_encripta.Length
            Return patron_busqueda.Substring(indice, 1)
        Else
            Return caracter
        End If
    End Function

    Public Function ValidaUsuario(ByVal strUsu As String, _
                                  ByVal strPwd As String) As DataSet

        'Función que valida el acceso al sistema
        Dim objUsu As New clsUsuariosSistema
        Dim strEncripta As String = ""

        strErrSeg = ""
        ValidaUsuario = New DataSet

        Try
            'se obtiene la información del usuario
            objUsu.UserName = Trim$(strUsu)
            ValidaUsuario = objUsu.ManejaUsuario(5)
            strErrSeg = objUsu.ErrorUsuario

            If Trim$(strErrSeg) = "" Then
                If ValidaUsuario.Tables.Count > 0 Then
                    If ValidaUsuario.Tables(0).Rows.Count > 0 Then
                        If ValidaUsuario.Tables(0).Rows(0).Item("ESTATUS") = 3 Then
                            strErrSeg = "Usuario Inactivo"
                        Else
                            Dim ValidarPassword As String = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("ValidarPassword").ToString())
                            If ValidarPassword = 1 Then 'web.config ValidarPassword
                                strEncripta = EncriptarCadena(Trim$(strPwd))
                                If Trim$(strEncripta) <> Trim$(ValidaUsuario.Tables(0).Rows(0).Item("PASSWORD")) Then
                                    strErrSeg = "Contraseña Incorrecta"
                                End If
                            End If
                        End If
                    Else
                        strErrSeg = "El usuario NO existe"
                    End If
                Else
                    strErrSeg = "El usuario NO existe"
                End If
            End If
        Catch e As Exception
            strErrSeg = e.Message
        End Try
    End Function

End Class