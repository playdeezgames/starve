Friend Class CharacterTypeDescriptor
    Inherits VisibleEntityDescriptor
    ReadOnly Property Statistics As IReadOnlyDictionary(Of String, Integer)
    ReadOnly Property Name As String
    Public Sub New(glyph As Char, hue As Integer, name As String, Optional statistics As IReadOnlyDictionary(Of String, Integer) = Nothing)
        MyBase.New(glyph, hue)
        Me.Statistics = If(statistics, New Dictionary(Of String, Integer))
        Me.Name = name
    End Sub
End Class
