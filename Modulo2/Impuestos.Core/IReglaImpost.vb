Namespace Impostos.Core
    ''' <summary>
    ''' Interf�cie que defineix una regla d'impost aplicable sobre un import base.
    ''' Permet estendre el sistema amb noves regles sense modificar el codi existent (OCP).
    ''' </summary>
    Public Interface IReglaImpost
        ''' <summary>
        ''' Aplica la regla d'impost sobre l'import indicat.
        ''' </summary>
        ''' <param name="import">Import base sobre el qual aplicar la regla.</param>
        ''' <returns>Import resultant despr�s d'aplicar la regla.</returns>
        Function Aplicar(import As Decimal) As Decimal

        ''' <summary>
        ''' Retorna l'ordre d'aplicaci� de la regla (menys �s primer).
        ''' </summary>
        ReadOnly Property Ordre As Integer

        ''' <summary>
        ''' Nom descriptiu de la regla per a prop�sits de logging o debugging.
        ''' </summary>
        ReadOnly Property Nom As String
    End Interface
End Namespace
