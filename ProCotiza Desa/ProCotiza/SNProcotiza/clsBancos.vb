Imports SDManejaBD

Public Class clsBancos
    Inherits clsSession

#Region "Variables"

    ''' <summary>
    ''' Clave de Bancos
    ''' </summary>
    ''' <remarks></remarks>
    Private intValor As Integer = 0
    Private strTexto As String = String.Empty
    Private intregdef As Integer = 0

#End Region

#Region "Propiedades"

    ''' <summary>
    ''' Clave de Bancos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _intValor() As Integer
        Get
            Return intValor
        End Get
        Set(ByVal value As Integer)
            intValor = value
        End Set
    End Property

    ''' <summary>
    ''' Descripcion Banco
    ''' </summary>
    ''' <remarks></remarks>
    Public Property _strTexto() As String
        Get
            Return strTexto
        End Get
        Set(ByVal value As String)
            strTexto = value
        End Set
    End Property
    ''' <summary>
    ''' Default Banco
    ''' </summary>
    ''' <remarks></remarks>
    Public Property _intregdef() As Integer
        Get
            Return intregdef
        End Get
        Set(ByVal value As Integer)
            intregdef = value
        End Set
    End Property
#End Region

#Region "Metodos"

    Sub New()
    End Sub

    Sub New(ByVal intBanco As Integer)
        CargaBanco(intBanco)
    End Sub

    Public Function ManejaBanco(ByVal intOper As Integer) As DataSet

        ManejaBanco = New DataSet
        Dim strParamStored As String = ""
        Dim strError As String = String.Empty
        Try
            Dim objSD As New clsConexion

            ArmaParametros(strParamStored, TipoDato.Entero, "Opcion", intOper.ToString)
            Select Case intOper
                Case 1 ' consulta banco
                    If intValor > 0 Then
                        ArmaParametros(strParamStored, TipoDato.Entero, "Valor", intValor.ToString)
                    End If

                Case 2 ' inserta banco
                    ArmaParametros(strParamStored, TipoDato.Entero, "Valor", intValor.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Texto", strTexto.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "reg_default", intregdef.ToString)


                Case 3 ' actualiza banco
                    ArmaParametros(strParamStored, TipoDato.Entero, "Valor", intValor.ToString)
                    ArmaParametros(strParamStored, TipoDato.Cadena, "Texto", strTexto.ToString)
                    ArmaParametros(strParamStored, TipoDato.Entero, "reg_default", intregdef.ToString)

                Case 4 ' borra Banco
                    ArmaParametros(strParamStored, TipoDato.Entero, "Valor", intValor.ToString)
            End Select

            ManejaBanco = objSD.EjecutaStoredProcedure("spManejaBanco", strError, strParamStored)
            If strError = "" Then
                If intOper = 2 Then
                    intValor = ManejaBanco.Tables(0).Rows(0).Item(0)
                End If
            End If

        Catch ex As Exception
            strError = ex.Message
            Throw New Exception(strError)
        End Try
    End Function

    Public Sub CargaBanco(Optional ByVal intBanco As Integer = 0)
        Dim dtsRes As New DataSet
        Dim strError As String = String.Empty
        Try

            intValor = intBanco
            dtsRes = ManejaBanco(1)

            If Trim$(strError) = "" Then
                If dtsRes.Tables(0).Rows.Count > 0 Then


                    intValor = dtsRes.Tables(0).Rows(0).Item("Valor")
                    strTexto = dtsRes.Tables(0).Rows(0).Item("Texto")

                Else
                    strError = "No se encontró información para poder cargar la entidad"
                End If
            End If
        Catch ex As Exception
            strError = ex.Message
            Throw New Exception(strError)
        End Try
    End Sub

#End Region
End Class
