Namespace Impostos.Core.Regles
    ''' <summary>
    ''' Aplica IVA reduït del 10% sobre l'import base.
    ''' </summary>
    Public Class ReglaIvaReduit
        Implements IReglaImpost

        Private ReadOnly _percentatgeIva As Decimal

        Public Sub New(Optional percentatgeIva As Decimal = 0.1D)
            _percentatgeIva = percentatgeIva
        End Sub

        Public Function Aplicar(import As Decimal) As Decimal Implements IReglaImpost.Aplicar
            ' Aplica el percentatge d'IVA reduït sobre l'import
            Return import + (import * _percentatgeIva)
        End Function

        Public ReadOnly Property Ordre As Integer Implements IReglaImpost.Ordre
            Get
                Return 10 ' Primera regla: IVA (mateix ordre que IVA normal, només una s'aplicarà)
            End Get
        End Property

        Public ReadOnly Property Nom As String Implements IReglaImpost.Nom
            Get
                Return $"IVA Reduït ({_percentatgeIva * 100}%)"
            End Get
        End Property
    End Class
End Namespace
