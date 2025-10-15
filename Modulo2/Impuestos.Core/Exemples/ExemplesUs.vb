Namespace Impostos.Core.Exemples
    ''' <summary>
    ''' Exemples d'ús de la CalculadoraImpostos refactoritzada.
    ''' Demostra configuració per defecte, personalitzada i amb ordre modificat.
    ''' </summary>
    Public Class ExemplesUs

        ''' <summary>
        ''' Exemple 1: Configuració per defecte (sense injecció explícita).
        ''' Utilitza el mètode CalcularTotal original amb paràmetres booleanos.
        ''' </summary>
        Public Shared Sub Exemple1_ConfiguracioPerDefecte()
            Console.WriteLine("=== EXEMPLE 1: Configuració Per Defecte ===")
            
            Dim calculadora As New CalculadoraImpostos()
            
            ' Càlcul amb IVA normal (21%), recàrrec (5%) i descompte pront pagament (2%)
            Dim total As Decimal = calculadora.CalcularTotal(
                preuUnitat:=100D, 
                quantitat:=2, 
                categoria:="normal", 
                recarrecEquivalencia:=True, 
                prontPagament:=True
            )
            
            ' Resultat esperat:
            ' Base: 200
            ' + IVA 21%: 200 + 42 = 242
            ' + Recàrrec 5% sobre base: 242 + 10 = 252
            ' - Descompte 2% sobre total: 252 - 5.04 = 246.96
            Console.WriteLine($"Total: {total:C2} (esperat: 246.96 €)")
            Console.WriteLine()
        End Sub

        ''' <summary>
        ''' Exemple 2: Configuració personalitzada amb injecció de dependències.
        ''' Permet preconfigurar les regles abans de calcular.
        ''' </summary>
        Public Shared Sub Exemple2_ConfiguracioPersonalitzada()
            Console.WriteLine("=== EXEMPLE 2: Configuració Personalitzada amb DI ===")
            
            ' Crear context i afegir regles manualment
            Dim contexte As New ContexteImpost()
            contexte.AfegirRegla(New Regles.ReglaIvaReduit(0.1D))                ' Ordre 10: IVA reduït 10%
            contexte.AfegirRegla(New Regles.ReglaRecarrec(200D, 0.05D))         ' Ordre 20: Recàrrec 5% sobre 200
            contexte.AfegirRegla(New Regles.ReglaDescompteProntPagament(0.02D)) ' Ordre 30: Descompte 2%
            
            Dim calculadoraCustom As New CalculadoraImpostos(contexte)
            Dim totalCustom As Decimal = calculadoraCustom.CalcularAmbContext(200D)
            
            ' Resultat esperat:
            ' Base: 200
            ' + IVA 10%: 200 + 20 = 220
            ' + Recàrrec 5% sobre 200: 220 + 10 = 230
            ' - Descompte 2%: 230 - 4.6 = 225.4
            Console.WriteLine($"Total personalitzat: {totalCustom:C2} (esperat: 225.40 €)")
            Console.WriteLine()
        End Sub

        ''' <summary>
        ''' Exemple 3: Canviar l'ordre d'aplicació de regles.
        ''' Demostra com crear regles amb ordre personalitzat.
        ''' </summary>
        Public Shared Sub Exemple3_CanviarOrdreRegles()
            Console.WriteLine("=== EXEMPLE 3: Canviar Ordre de Regles ===")
            
            ' Crear regla de descompte amb ordre modificat (abans del recàrrec)
            Dim contexteReordenat As New ContexteImpost()
            contexteReordenat.AfegirRegla(New Regles.ReglaIvaNormal(0.21D))                      ' Ordre 10
            contexteReordenat.AfegirRegla(New ReglaDescompteProntPagamentAntesRecarrec())       ' Ordre 15
            contexteReordenat.AfegirRegla(New Regles.ReglaRecarrec(200D, 0.05D))                ' Ordre 20
            
            Dim calculadoraReordenada As New CalculadoraImpostos(contexteReordenat)
            Dim totalReordenat As Decimal = calculadoraReordenada.CalcularAmbContext(200D)
            
            ' Resultat esperat (ordre diferent):
            ' Base: 200
            ' + IVA 21%: 200 + 42 = 242
            ' - Descompte 2%: 242 - 4.84 = 237.16
            ' + Recàrrec 5% sobre base: 237.16 + 10 = 247.16
            Console.WriteLine($"Total reordenat: {totalReordenat:C2} (esperat: 247.16 €)")
            Console.WriteLine()
        End Sub

        ''' <summary>
        ''' Exemple 4: Mostrar detall de regles aplicades.
        ''' </summary>
        Public Shared Sub Exemple4_DetallReglesAplicades()
            Console.WriteLine("=== EXEMPLE 4: Detall de Regles Aplicades ===")
            
            Dim contexte As New ContexteImpost()
            contexte.AfegirRegla(New Regles.ReglaIvaNormal(0.21D))
            contexte.AfegirRegla(New Regles.ReglaRecarrec(100D, 0.05D))
            contexte.AfegirRegla(New Regles.ReglaDescompteProntPagament(0.02D))
            
            Console.WriteLine("Regles configurades (per ordre d'aplicació):")
            For Each regla In contexte.ReglesActuals.OrderBy(Function(r) r.Ordre)
                Console.WriteLine($"  - Ordre {regla.Ordre}: {regla.Nom}")
            Next
            
            Dim total As Decimal = contexte.AplicarRegles(100D)
            Console.WriteLine($"Total final: {total:C2}")
            Console.WriteLine()
        End Sub

        ''' <summary>
        ''' Punt d'entrada per executar tots els exemples.
        ''' </summary>
        Public Shared Sub ExecutarTotsElsExemples()
            Exemple1_ConfiguracioPerDefecte()
            Exemple2_ConfiguracioPersonalitzada()
            Exemple3_CanviarOrdreRegles()
            Exemple4_DetallReglesAplicades()
            
            Console.WriteLine("Premeu qualsevol tecla per sortir...")
            Console.ReadKey()
        End Sub
    End Class

    ''' <summary>
    ''' Regla personalitzada que aplica el descompte abans del recàrrec.
    ''' Demostra com estendre el sistema amb nou ordre d'aplicació.
    ''' </summary>
    Public Class ReglaDescompteProntPagamentAntesRecarrec
        Implements IReglaImpost

        Private ReadOnly _percentatgeDescompte As Decimal

        Public Sub New(Optional percentatgeDescompte As Decimal = 0.02D)
            _percentatgeDescompte = percentatgeDescompte
        End Sub

        Public Function Aplicar(import As Decimal) As Decimal Implements IReglaImpost.Aplicar
            Return import - (import * _percentatgeDescompte)
        End Function

        Public ReadOnly Property Ordre As Integer Implements IReglaImpost.Ordre
            Get
                Return 15 ' Entre IVA (10) i Recàrrec (20)
            End Get
        End Property

        Public ReadOnly Property Nom As String Implements IReglaImpost.Nom
            Get
                Return "Descompte Pront Pagament Anticipat (ordre modificat)"
            End Get
        End Property
    End Class
End Namespace
