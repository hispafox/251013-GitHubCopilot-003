Namespace Impostos.Core.Regles
    ''' <summary>
    ''' Aplica IVA redu�t del 10% sobre l'import base.
    ''' </summary>
    Public Class ReglaIvaReduit
        Implements IReglaImpost

        Private ReadOnly _percentatgeIva As Decimal

        Public Sub New(Optional percentatgeIva As Decimal = 0.1D)
            _percentatgeIva = percentatgeIva
        End Sub

        Public Function Aplicar(import As Decimal) As Decimal Implements IReglaImpost.Aplicar
            ' Aplica el percentatge d'IVA redu�t sobre l'import
            Return import + (import * _percentatgeIva)
        End Function

        Public ReadOnly Property Ordre As Integer Implements IReglaImpost.Ordre
            Get
                Return 10 ' Primera regla: IVA (mateix ordre que IVA normal, nom�s una s'aplicar�)
            End Get
        End Property

        Public ReadOnly Property Nom As String Implements IReglaImpost.Nom
            Get
                Return $"IVA Redu�t ({_percentatgeIva * 100}%)"
            End Get
        End Property
    End Class
End Namespace
