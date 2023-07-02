Imports System.Runtime.CompilerServices
Imports Starve.Persistence

Public Module ItemExtensions
    <Extension>
    Public Function Glyph(item As IItem) As Char
        Return item.ItemType.ToItemTypeDescriptor.Glyph
    End Function
    <Extension>
    Public Function Hue(item As IItem) As Integer
        Return item.ItemType.ToItemTypeDescriptor.Hue
    End Function
    <Extension>
    Public Function Name(item As IItem) As String
        Return item.ItemType.ToItemTypeDescriptor.Name
    End Function
End Module
