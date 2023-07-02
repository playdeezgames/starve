Imports Starve.Persistence

Friend Class CharacterTypeDescriptor
    Inherits VisibleEntityDescriptor
    ReadOnly Property Statistics As IReadOnlyDictionary(Of String, Integer)
    ReadOnly Property Name As String
    ReadOnly Property Initializer As Action(Of ICharacter)
    Public Sub New(
                  glyph As Char,
                  hue As Integer,
                  name As String,
                  Optional statistics As IReadOnlyDictionary(Of String, Integer) = Nothing,
                  Optional initializer As Action(Of ICharacter) = Nothing)
        MyBase.New(glyph, hue)
        Me.Statistics = If(statistics, New Dictionary(Of String, Integer))
        Me.Name = name
        Me.Initializer = If(initializer, Sub(x)

                                         End Sub)
    End Sub
End Class
