Namespace Impostos.Core.Regles
    ''' <summary>
    ''' Aplica descompte per pront pagament del 2% sobre el total acumulat.
    ''' </summary>
    Public Class ReglaDescompteProntPagament
        Implements IReglaImpost

        Private ReadOnly _percentatgeDescompte As Decimal

        Public Sub New(Optional percentatgeDescompte As Decimal = 0.02D)
            _percentatgeDescompte = percentatgeDescompte
        End Sub

        Public Function Aplicar(import As Decimal) As Decimal Implements IReglaImpost.Aplicar
            ' Aplica el descompte sobre el total acumulat fins ara
            Return import - (import * _percentatgeDescompte)
        End Function

        Public ReadOnly Property Ordre As Integer Implements IReglaImpost.Ordre
            Get
                Return 30 ' Tercera regla: després de recàrrec
            End Get
        End Property

        Public ReadOnly Property Nom As String Implements IReglaImpost.Nom
            Get
                Return $"Descompte Pront Pagament ({_percentatgeDescompte * 100}%)"
            End Get
        End Property
    End Class
End Namespace
