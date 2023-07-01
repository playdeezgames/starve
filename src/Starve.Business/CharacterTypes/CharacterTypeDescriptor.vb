Friend Class CharacterTypeDescriptor
    Inherits VisibleEntityDescriptor
    ReadOnly Property Statistics As IReadOnlyDictionary(Of String, Integer)
    Public Sub New(glyph As Char, hue As Integer, Optional statistics As IReadOnlyDictionary(Of String, Integer) = Nothing)
        MyBase.New(glyph, hue)
        Me.Statistics = If(statistics, New Dictionary(Of String, Integer))
    End Sub
End Class
