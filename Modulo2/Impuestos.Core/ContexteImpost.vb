Imports System.Linq

Namespace Impostos.Core
    ''' <summary>
    ''' Context que orquestra l'aplicació de regles d'impostos en l'ordre especificat.
    ''' Permet afegir, eliminar i ordenar regles dinàmicament (punt d'extensió OCP).
    ''' </summary>
    Public Class ContexteImpost
        Private ReadOnly _regles As List(Of IReglaImpost)

        Public Sub New()
            _regles = New List(Of IReglaImpost)()
        End Sub

        ''' <summary>
        ''' Afegeix una regla al context. Les regles s'aplicaran ordenades per la propietat Ordre.
        ''' </summary>
        Public Sub AfegirRegla(regla As IReglaImpost)
            If regla Is Nothing Then
                Throw New ArgumentNullException(NameOf(regla))
            End If
            _regles.Add(regla)
        End Sub

        ''' <summary>
        ''' Esborra totes les regles del context.
        ''' </summary>
        Public Sub NetejarRegles()
            _regles.Clear()
        End Sub

        ''' <summary>
        ''' Aplica totes les regles registrades sobre l'import base, en l'ordre definit per Ordre.
        ''' Aplica redondeig bancari a 2 decimals al final.
        ''' </summary>
        ''' <param name="importBase">Import inicial sense impostos.</param>
        ''' <returns>Import final amb totes les regles aplicades i arrodonit.</returns>
        Public Function AplicarRegles(importBase As Decimal) As Decimal
            Dim totalAcumulat As Decimal = importBase

            ' Ordena les regles per l'ordre especificat i aplica-les seqüencialment
            Dim reglesOrdenades = _regles.OrderBy(Function(r) r.Ordre).ToList()

            For Each regla In reglesOrdenades
                totalAcumulat = regla.Aplicar(totalAcumulat)
            Next

            ' Redondeig bancari (MidpointRounding.ToEven) a 2 decimals
            Return Math.Round(totalAcumulat, 2, MidpointRounding.ToEven)
        End Function

        ''' <summary>
        ''' Retorna la llista de regles actuals (només lectura).
        ''' </summary>
        Public ReadOnly Property ReglesActuals As IReadOnlyList(Of IReglaImpost)
            Get
                Return _regles.AsReadOnly()
            End Get
        End Property
    End Class
End Namespace
