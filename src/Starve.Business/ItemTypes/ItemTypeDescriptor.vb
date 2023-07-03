Imports System.ComponentModel
Imports Starve.Persistence

Friend Class ItemTypeDescriptor
    Inherits VisibleEntityDescriptor
    Friend ReadOnly Property Verbs As IReadOnlyDictionary(Of String, Action(Of ICharacter, IItem))
    Public Sub New(name As String, glyph As Char, hue As Integer, Optional verbs As IReadOnlyDictionary(Of String, Action(Of ICharacter, IItem)) = Nothing)
        MyBase.New(name, glyph, hue)
        Me.Verbs = If(verbs, New Dictionary(Of String, Action(Of ICharacter, IItem)))
    End Sub
    Friend Function HasVerb(verbType As String) As Boolean
        Return Verbs.ContainsKey(verbType)
    End Function
    Friend ReadOnly Property AllVerbs As IEnumerable(Of String)
        Get
            Return Verbs.Keys
        End Get
    End Property
End Class
