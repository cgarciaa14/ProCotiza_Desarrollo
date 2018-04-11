'BUG-PC-11: AVH: 23/11/2016 SE CREA CLASE PARA VALIDAR EL CORREO

Imports System.Text.RegularExpressions

Public Class clsValidaCampo

    Public Function ValidaEmail(ByVal textMail As String)
        Dim intValidaCorreo As Integer

        If textMail.Trim.Length > 0 Then
            intValidaCorreo = validarCorreoElectronico(textMail)

            If intValidaCorreo > 0 Then
                Select Case intValidaCorreo
                    Case 1
                        Throw New Exception("La dirección de correo electrónico es incorrecta.")
                    Case 2
                        Throw New Exception("Las direcciones de correo electrónico deben estar separadas con ; y un espacio.")
                    Case 3
                        Throw New Exception("Error al validar la dirección de correo electrónico.")
                End Select
                Exit Function
            End If
            'Else
            '    Throw New Exception("No se ha capturado el E-mail del contacto.")
        End If
    End Function

    Public Function validarCorreoElectronico(ByVal textMail As String) As Integer
        Dim arr As String()
        Dim i As Integer

        Try
            arr = textMail.Trim.Split(";")
            For i = 0 To arr.Length - 1
                If Not arr(i).Contains("@") Then Return 1

                Dim res() As String
                res = arr(i).Split("@")
                If Regex.IsMatch(arr(i), "([0-9a-z][\W]*[0-9a-z])(@{1,2})([0-9a-z][-\w]*[0-9a-z]*\.)+([0-9a-z][-\w]*[a-z0-9])$") Then
                    'ElseIf Regex.IsMatch(arr(i), "([0-9a-z][\W]*[0-9a-z])*[\w]*(@{1,2})([0-9a-z][-\w]*[0-9a-z]*\.)+([0-9a-z][-\w]*[a-z0-9])$") Then 
                Else
                    Return 1
                End If
            Next

            Return 0
        Catch ex As Exception
            Return 3
        End Try
    End Function

End Class
