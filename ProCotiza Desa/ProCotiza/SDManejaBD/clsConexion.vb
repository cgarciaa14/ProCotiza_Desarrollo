'BBVA-P-412: 08/08/2016: AMR: RQ09 – Carga Masiva de Plan de Financiamiento

Imports System.Data.Common
Imports System.Data
Imports System.Configuration

Public Class clsConexion
    Private objFactory As DbProviderFactory = Nothing
    Private objConex As DbConnection = Nothing
    Private objTransac As DbTransaction = Nothing

    Private strProveedorConexion As String = ""
    Private strCadenaConexion As String = ""
    Private strErrConexion As String = ""
    Private strParametros As String = ""

    Private blnManejaTransac As Boolean = False
    Private blnTransaccionAbierta As Boolean = False

    Public Sub New(Optional ByVal blnManejaTransaccion As Boolean = False)
        blnManejaTransac = blnManejaTransaccion
    End Sub

    Public ReadOnly Property ErrorConexion() As String
        Get
            Return strErrConexion
        End Get
    End Property

    Public ReadOnly Property Parametros() As String
        Get
            Return strParametros
        End Get
    End Property

    Public ReadOnly Property ManejaTransaccion() As Boolean
        Get
            Return blnManejaTransac
        End Get
    End Property

    Public Property CadenaConexionBD() As String
        Get
            Return strCadenaConexion
        End Get

        Set(ByVal value As String)
            strCadenaConexion = value
        End Set
    End Property

    Public Property ProveedorBD() As String
        Get
            Return strProveedorConexion
        End Get

        Set(ByVal value As String)
            strProveedorConexion = value
        End Set
    End Property

    Public Function AbreConexion() As Boolean
        AbreConexion = False
        strErrConexion = ""

        Try
            If Not blnTransaccionAbierta Then
                If Trim(strProveedorConexion) = "" Then
                    strProveedorConexion = System.Configuration.ConfigurationManager.AppSettings.Item("Proveedor")
                End If

                If Trim(strCadenaConexion) = "" Then
                    strCadenaConexion = System.Configuration.ConfigurationManager.AppSettings.Item("ConexionBD")
                End If

                objFactory = DbProviderFactories.GetFactory(strProveedorConexion)

                objConex = objFactory.CreateConnection
                objConex.ConnectionString = strCadenaConexion
                objConex.Open()

                If blnManejaTransac Then
                    objTransac = objConex.BeginTransaction
                    blnTransaccionAbierta = True
                End If
            End If

            AbreConexion = True
        Catch ex As Exception

            If blnManejaTransac Then
                objTransac.Rollback()
            End If

            objConex = Nothing
            strErrConexion = ex.Message
            AbreConexion = False
        End Try
    End Function

    Public Function EjecutaStoredProcedure(ByVal strStored As String, _
                                           ByRef strErr As String, _
                                           Optional ByVal strParam As String = "") As DataSet

        EjecutaStoredProcedure = New DataSet
        strErr = AbreConexion()

        If Trim(strErr) = "True" Then
            Try
                Dim objCommand As DbCommand
                Dim objAdap As Data.Common.DbDataAdapter

                objCommand = objFactory.CreateCommand
                objCommand.CommandText = strStored
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.Connection = objConex

                If Not objTransac Is Nothing Then
                    objCommand.Transaction = objTransac
                End If

                objAdap = objFactory.CreateDataAdapter
                objAdap.SelectCommand = objCommand
                If Trim$(strParam) <> "" Then
                    strErr = AgregaParametros(strParam, objCommand)
                End If

                If Trim$(strErr) = "" Then
                    objAdap.Fill(EjecutaStoredProcedure, "RESULTADO")
                End If

                If objTransac Is Nothing Then
                    objConex.Close()
                End If

            Catch ex As Exception
                objConex.Close()
                strErr = ex.Message
            End Try
        End If

    End Function

    Public Function TerminaTransaccion(Optional ByVal blnRollback As Boolean = False) As Boolean
        TerminaTransaccion = False
        Try
            If blnManejaTransac Then
                If blnTransaccionAbierta Then
                    If Not IsNothing(objTransac) Then
                        If blnRollback Then
                            objTransac.Rollback()
                        Else
                            objTransac.Commit()
                        End If

                        objConex.Close()
                    End If

                    blnTransaccionAbierta = False
                End If
            End If

            TerminaTransaccion = True
        Catch ex As Exception
            strErrConexion = ex.Message
        End Try
    End Function


    Public Function EjecutaQueryConsulta(ByVal strQuery As String) As DataSet

        EjecutaQueryConsulta = New DataSet
        strErrConexion = ""

        If AbreConexion() Then
            Try
                Dim objCommand As DbCommand
                Dim objAdap As Data.Common.DbDataAdapter

                objCommand = objFactory.CreateCommand
                objCommand.CommandText = strQuery
                objCommand.Connection = objConex

                objAdap = objFactory.CreateDataAdapter
                objAdap.SelectCommand = objCommand

                objAdap.Fill(EjecutaQueryConsulta, "RESULTADO")
                objCommand.Parameters.Clear()

                If Not blnTransaccionAbierta Then
                    objConex.Close()
                End If

            Catch ex As Exception
                objConex.Close()
                strErrConexion = ex.Message
            End Try
        End If
    End Function

    Public Function EjecutaQuery(ByVal strQuery As String) As Integer

        EjecutaQuery = -1
        strErrConexion = ""

        If AbreConexion() Then
            Try
                Dim objCommand As DbCommand

                objCommand = objFactory.CreateCommand
                objCommand.CommandType = CommandType.Text
                objCommand.CommandText = strQuery
                objCommand.Connection = objConex
                objCommand.CommandTimeout = 0

                EjecutaQuery = objCommand.ExecuteNonQuery()

                If Not blnTransaccionAbierta Then
                    objConex.Close()
                End If

            Catch ex As Exception
                objConex.Close()
                strErrConexion = ex.Message
            End Try
        End If
    End Function

    Private Function AgregaParametros(ByVal strParam As String, _
                                  ByRef objCmd As DbCommand) As String

        AgregaParametros = ""
        Try
            Dim strPaso() As String = Split(strParam, "|")
            Dim intDat As Integer = 0

            For intDat = 0 To UBound(strPaso, 1)
                Dim strDet() As String = Split(strPaso(intDat), ",")
                Dim objParam As DbParameter

                'creamos el parámetro y le asignamos sus valores
                objParam = objFactory.CreateParameter
                objParam.ParameterName = "@" & strDet(0) 'nombre

                strDet(2) = Replace(strDet(2), "\c\", ",")
                strDet(2) = Replace(strDet(2), "\p\", "|")

                Select Case Val(strDet(1)) ' tipo
                    Case 1 'cadena
                        objParam.DbType = DbType.String
                        objParam.Value = Trim$(strDet(2))
                    Case 2 'entero
                        objParam.DbType = DbType.Int32
                        objParam.Value = Val(strDet(2))
                    Case 3 'double
                        objParam.DbType = DbType.Double
                        'objParam.DbType = DbType.VarNumeric
                        objParam.Value = Val(strDet(2))
                    Case 4 'fecha
                        objParam.DbType = DbType.Date
                        objParam.Value = strDet(2)
                    Case 5 'fecha y hora
                        objParam.DbType = DbType.DateTime
                        objParam.Value = strDet(2)
                    Case 6 'Boolean
                        objParam.DbType = DbType.Boolean
                        objParam.Value = strDet(2)
                End Select

                'If Val(strDet(3)) = 1 Then
                ' 'dirección
                ' objParam.Direction = ParameterDirection.InputOutput
                ' End If

                objCmd.Parameters.Add(objParam)
            Next
        Catch ex As Exception
            AgregaParametros = ex.Message
        End Try
    End Function
End Class
