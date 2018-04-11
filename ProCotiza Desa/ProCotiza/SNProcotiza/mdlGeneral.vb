'BUG-PC-22 02-11-2016 MAUT Se agrega segundo nombre

Imports SDManejaBD

Module mdlGeneral
    Public Sub ArmaParametros(ByRef strParam As String, _
                              ByVal intTipo As TipoDato, _
                              ByVal strNom As String, _
                              ByVal strValor As String)

        strValor = Replace(strValor, ",", "\c\")
        strValor = Replace(strValor, "|", "\p\")

        If Trim$(strParam) = "" Then
            strParam = strNom & "," & intTipo & "," & strValor
        Else
            strParam += "|" & strNom & "," & intTipo & "," & strValor
        End If
    End Sub


    Public Function RevisaCaracteres(ByVal strCadena As String) As String
        Dim intCont As Integer
        Dim strPaso As String
        Dim strLetras As String = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789&¥ "

        RevisaCaracteres = ""
        strCadena = Trim$(strCadena)
        For intCont = 1 To Len(strCadena)
            strPaso = Mid$(strCadena, intCont, 1)
            If InStr(strLetras, strPaso) = 0 Then
                RevisaCaracteres += strPaso
            End If
        Next
        Return RevisaCaracteres
    End Function
    Public Function ObtenError(ByVal intErr As Integer, Optional ByVal strProc As String = "") As String
        Select Case intErr

            Case 118 : ObtenError = "No es posible obtener el RFC debido a que existen los siguientes caracteres inválidos:"
            Case 119 : ObtenError = "Error al momento de generar el RFC"
            Case Else
                ObtenError = "Error Indefinido"
        End Select
    End Function
    Public Function LimpiaCadenaRFC(ByVal strCadena As String, ByVal intAcc As Integer) As String
        Dim intCont As Integer
        Dim strPaso() As String
        Dim strRes As String

        strRes = Trim$(strCadena)
        Select Case intAcc
            Case 1 'limplia prefijos y elimina Jose y Maria

                'se eliminan los prefijos
                strPaso = Split(strCadena, " ")
                If UBound(strPaso, 1) > 0 Then
                    For intCont = 0 To UBound(strPaso, 1)
                        Select Case Trim$(strPaso(intCont))
                            Case "DE", "DEL", "LA", "LAS", "LOS", "Y", "MI", "MC", "MAC", "VON", "VAN"
                            Case Else
                                strRes += " " & strPaso(intCont)
                        End Select
                    Next
                End If

                'se eliminan los nombres de Maria y Jose
                LimpiaCadenaRFC = Trim$(strRes)
                strPaso = Split(Trim$(strRes), " ")
                If UBound(strPaso, 1) > 0 Then
                    Select Case Trim$(strPaso(0))
                        Case "MARÍA", "MARIA", "MA", "JOSE", "JOSÉ"
                            LimpiaCadenaRFC = strPaso(1)
                        Case Else
                            LimpiaCadenaRFC = strPaso(0)
                    End Select
                End If
            Case 2 'Revisa que no sea una palabra que se escuche mal
                LimpiaCadenaRFC = strCadena
                Select Case strCadena
                    Case "BUEI", "BUEY", "CACA", "CACO", "CAGA", "CAGO", "CAKA", "CAKO", "COGE", "COJA", "COJE", "COJI", "COJO", "CULO", "FETO", "GUEY", "JOTO", "KACA", "KACO", "KAGA", "KOGE", "KOJO", "KAKA", "KULO", "LOCA", "LOCO", "LOKA", "LOKO", "MAME", "MAMO", "MEAR", "MEAS", "MEON", "MION", "MOCO", "MULA", "PITO", "PEDA", "PEDO", "PENE", "PUTA", "PUTO", "QULO", "RATA", "RUIN"
                        LimpiaCadenaRFC = Left$(strCadena, 3) & "X"
                End Select
            Case Else
                Return ""
        End Select
        Return LimpiaCadenaRFC
    End Function
    Public Function ObtenHomonimia(ByVal strAPat As String, _
                                ByVal strAMat As String, _
                                ByVal strNom As String, _
                                ByVal strRFC As String, _
                                Optional ByVal strNomSeg As String = "") As String

        'BUG-PC-22 MAUT Se agrega segundo nombre

        Dim strNombre As String
        Dim strTemp As String
        Dim intCont As Integer
        Dim dblVal As Double
        Dim dblVal2 As Double
        Dim dblSum As Double

        ObtenHomonimia = ""
        strTemp = "0"
        'BUG-PC-22 MAUT Se agrega segundo nombre
        strNombre = Trim$(strAPat & " " & strAMat & " " & strNom & " " & strNomSeg)
        For intCont = 1 To Len(strNombre)
            dblVal = Asc(Mid$(strNombre, intCont, 1))
            Select Case dblVal
                Case 38
                    dblVal = 10
                Case 48 To 57
                    dblVal -= 48
                Case 65 To 73
                    dblVal -= 54
                Case 74 To 82
                    dblVal -= 53
                Case 83 To 90
                    dblVal -= 51
                Case Else ' se incluye el 32
                    dblVal = 0
            End Select
            strTemp = strTemp & IIf(dblVal < 10, "0" & dblVal, dblVal)
        Next

        For intCont = 2 To Len(strTemp)
            dblVal = Val(Mid$(strTemp, intCont - 1, 2))
            dblVal2 = Mid$(strTemp, intCont, 1)
            dblSum += (dblVal * dblVal2)
        Next

        dblSum = dblSum Mod 1000
        dblVal = dblSum / 34
        dblVal2 = dblSum Mod 34

        dblSum = 0
        While dblSum < 2
            If dblSum = 0 Then intCont = Fix(dblVal) Else intCont = Fix(dblVal2)
            Select Case intCont
                Case 0 To 8
                    strTemp = Trim$(Str(intCont + 1))
                Case 9 To 22
                    strTemp = Chr(intCont + 56)
                Case Else
                    strTemp = Chr(intCont + 57)
            End Select
            dblSum += 1
            ObtenHomonimia += strTemp
        End While

        'se obtiene el dígito verificador
        ObtenHomonimia = strRFC & ObtenHomonimia
        dblSum = 0
        For intCont = 1 To 12
            dblVal = Asc(Mid$(ObtenHomonimia, intCont, 1))
            dblVal2 = 14 - intCont
            Select Case dblVal
                Case 32
                    dblVal = 37
                Case 38
                    dblVal = 24
                Case 48 To 57
                    dblVal -= 48
                Case 65 To 78
                    dblVal -= 55
                Case 79 To 90
                    dblVal -= 54
                Case Else
                    dblVal = 0
            End Select
            dblSum += (dblVal * dblVal2)
        Next intCont

        dblSum = dblSum Mod 11
        Select Case dblSum
            Case 0
                ObtenHomonimia += "0"
            Case 1
                ObtenHomonimia += "A"
            Case Else
                ObtenHomonimia += Chr(48 + (11 - dblSum))
        End Select
        Return ObtenHomonimia
    End Function
End Module
