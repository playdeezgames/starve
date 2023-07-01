Friend Class TerrainTypeDescriptor
    Inherits VisibleEntityDescriptor
    Friend ReadOnly Property Tenable As Boolean
    Sub New(glyph As Char, hue As Integer, Optional tenable As Boolean = True)
        MyBase.New(glyph, hue)
        Me.Tenable = tenable
    End Sub
End Class
