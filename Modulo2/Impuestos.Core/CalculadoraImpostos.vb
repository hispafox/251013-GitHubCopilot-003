Namespace Impostos.Core
    ''' <summary>
    ''' Calculadora d'impostos refactoritzada que utilitza injecci� de depend�ncies
    ''' per aplicar regles d'impostos de manera extensible i mantenible (OCP).
    ''' </summary>
    Public Class CalculadoraImpostos
        Private ReadOnly _contexteImpost As ContexteImpost

        ''' <summary>
        ''' Constructor amb injecci� de depend�ncies del context d'impostos.
        ''' </summary>
        ''' <param name="contexteImpost">Context que cont� les regles d'impostos configurades.</param>
        Public Sub New(contexteImpost As ContexteImpost)
            _contexteImpost = If(contexteImpost, New ContexteImpost())
        End Sub

        ''' <summary>
        ''' Constructor per defecte que crea un context buit.
        ''' </summary>
        Public Sub New()
            _contexteImpost = New ContexteImpost()
        End Sub

        ''' <summary>
        ''' Calcula el total aplicant les regles configurades sobre l'import base.
        ''' M�tode original mantingut per compatibilitat amb signatura existent.
        ''' </summary>
        ''' <param name="preuUnitat">Preu per unitat.</param>
        ''' <param name="quantitat">Quantitat d'unitats.</param>
        ''' <param name="categoria">Categoria del producte (determina el tipus d'IVA: "reduit" o normal).</param>
        ''' <param name="recarrecEquivalencia">Indica si s'aplica rec�rrec d'equival�ncia.</param>
        ''' <param name="prontPagament">Indica si s'aplica descompte per pront pagament.</param>
        ''' <returns>Import total amb impostos, rec�rrecs i descomptes aplicats.</returns>
        Public Function CalcularTotal(preuUnitat As Decimal, quantitat As Integer, categoria As String,
                                     recarrecEquivalencia As Boolean, prontPagament As Boolean) As Decimal
            ' Calcula l'import base (subtotal)
            Dim importBase As Decimal = preuUnitat * quantitat

            ' Configura el context amb les regles aplicables segons els par�metres
            Dim contexte As New ContexteImpost()

            ' Regla 1: IVA (normal o redu�t segons categoria)
            If categoria IsNot Nothing AndAlso (categoria.ToLower().Trim() = "reduit" OrElse categoria.ToLower().Trim() = "reducido") Then
                contexte.AfegirRegla(New Regles.ReglaIvaReduit(0.1D))
            Else
                contexte.AfegirRegla(New Regles.ReglaIvaNormal(0.21D))
            End If

            ' Regla 2: Rec�rrec d'equival�ncia (opcional)
            If recarrecEquivalencia Then
                contexte.AfegirRegla(New Regles.ReglaRecarrec(importBase, 0.05D))
            End If

            ' Regla 3: Descompte per pront pagament (opcional)
            If prontPagament Then
                contexte.AfegirRegla(New Regles.ReglaDescompteProntPagament(0.02D))
            End If

            ' Aplica totes les regles i retorna el total arrodonit
            Return contexte.AplicarRegles(importBase)
        End Function

        ''' <summary>
        ''' Calcula el total utilitzant el context injectat (permet configuraci� pr�via de regles).
        ''' </summary>
        ''' <param name="importBase">Import base sobre el qual aplicar les regles.</param>
        ''' <returns>Import total amb totes les regles del context aplicades.</returns>
        Public Function CalcularAmbContext(importBase As Decimal) As Decimal
            Return _contexteImpost.AplicarRegles(importBase)
        End Function
    End Class
End Namespace
