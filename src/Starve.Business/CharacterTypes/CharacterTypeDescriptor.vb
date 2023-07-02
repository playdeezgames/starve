Imports Starve.Persistence

Friend Class CharacterTypeDescriptor
    Inherits VisibleEntityDescriptor
    ReadOnly Property Statistics As IReadOnlyDictionary(Of String, Integer)
    ReadOnly Property Initializer As Action(Of ICharacter)
    Public Sub New(
                  name As String,
                  glyph As Char,
                  hue As Integer,
                  Optional statistics As IReadOnlyDictionary(Of String, Integer) = Nothing,
                  Optional initializer As Action(Of ICharacter) = Nothing)
        MyBase.New(name, glyph, hue)
        Me.Statistics = If(statistics, New Dictionary(Of String, Integer))
        Me.Initializer = If(initializer, Sub(x)

                                         End Sub)
    End Sub
End Class
