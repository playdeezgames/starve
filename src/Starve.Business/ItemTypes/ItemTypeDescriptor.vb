Imports System.ComponentModel
Imports Starve.Persistence

Friend Class ItemTypeDescriptor
    Inherits VisibleEntityDescriptor
    Friend ReadOnly Property VerbTypes As IReadOnlyDictionary(Of String, Action(Of ICharacter, IItem))
    Friend ReadOnly Property CanEquip As Boolean
        Get
            Return True
        End Get
    End Property
    Public Sub New(name As String, glyph As Char, hue As Integer, Optional verbTypes As IReadOnlyDictionary(Of String, Action(Of ICharacter, IItem)) = Nothing)
        MyBase.New(name, glyph, hue)
        Me.VerbTypes = If(verbTypes, New Dictionary(Of String, Action(Of ICharacter, IItem)))
    End Sub
    Friend Function HasVerb(verbType As String) As Boolean
        Return VerbTypes.ContainsKey(verbType)
    End Function
    Friend ReadOnly Property AllVerbTypes As IEnumerable(Of String)
        Get
            Return VerbTypes.Keys
        End Get
    End Property
End Class
