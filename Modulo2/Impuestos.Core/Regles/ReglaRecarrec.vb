Namespace Impostos.Core.Regles
    ''' <summary>
    ''' Aplica recàrrec d'equivalència del 5% sobre l'import base (abans d'IVA).
    ''' </summary>
    Public Class ReglaRecarrec
        Implements IReglaImpost

        Private ReadOnly _percentatgeRecarrec As Decimal
        Private ReadOnly _importBase As Decimal

        ''' <summary>
        ''' Constructor de la regla de recàrrec.
        ''' </summary>
        ''' <param name="importBase">Import base original (sense IVA) per calcular el recàrrec.</param>
        ''' <param name="percentatgeRecarrec">Percentatge de recàrrec a aplicar (per defecte 5%).</param>
        Public Sub New(importBase As Decimal, Optional percentatgeRecarrec As Decimal = 0.05D)
            _importBase = importBase
            _percentatgeRecarrec = percentatgeRecarrec
        End Sub

        Public Function Aplicar(import As Decimal) As Decimal Implements IReglaImpost.Aplicar
            ' Aplica el recàrrec sobre l'import base, no sobre l'import amb IVA
            Return import + (_importBase * _percentatgeRecarrec)
        End Function

        Public ReadOnly Property Ordre As Integer Implements IReglaImpost.Ordre
            Get
                Return 20 ' Segona regla: després d'IVA
            End Get
        End Property

        Public ReadOnly Property Nom As String Implements IReglaImpost.Nom
            Get
                Return $"Recàrrec Equivalència ({_percentatgeRecarrec * 100}%)"
            End Get
        End Property
    End Class
End Namespace
