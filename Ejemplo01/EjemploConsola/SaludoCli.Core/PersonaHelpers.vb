Namespace SaludoCli.Core
    Public Class PersonaHelpers
        Public Shared Function NormalizarNombre2(nombre As String) As String
            ' Normaliza a Título Capital, elimina espacios extra
            If String.IsNullOrWhiteSpace(nombre) Then Return ""
            Dim palabras = nombre.Trim().ToLower().Split({" "}, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To palabras.Length - 1
                If palabras(i).Length > 0 Then
                    palabras(i) = Char.ToUpper(palabras(i)(0)) & palabras(i).Substring(1)
                End If
            Next
            Return String.Join(" ", palabras)
        End Function

        Public Shared Function EsNombreValido(nombre As String) As Boolean
            ' Solo letras y espacios, no vacío
            If String.IsNullOrWhiteSpace(nombre) Then Return False
            For Each c In nombre
                If Not Char.IsLetter(c) AndAlso c <> " "c Then Return False
            Next
            Return True
        End Function

        Public Shared Function TryParseFecha(fechaTexto As String, ByRef fecha As Date) As Boolean
            ' Formato dd/MM/yyyy, no futura, >= 01/01/1900
            fecha = Date.MinValue
            Dim valido = Date.TryParseExact(fechaTexto, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, fecha)
            If Not valido Then Return False
            If fecha > Date.Now OrElse fecha < #01/01/1900# Then Return False
            Return True
        End Function

        Public Shared Function EsEmailValido(email As String) As Boolean
            ' Patrón básico
            If String.IsNullOrWhiteSpace(email) Then Return False
            Dim re = New Text.RegularExpressions.Regex("^[^@\s]+@[^@\s]+\.[^@\s]+$")
            Return re.IsMatch(email)
        End Function

        Public Shared Function FormatearResumen(nombre As String, apellidos As String, fecha As Date, email As String) As String
            Return $"Gracias, {nombre} {apellidos}. Naciste el {fecha:dd/MM/yyyy} y tu correo es {email}."
        End Function
    End Class
End Namespace
