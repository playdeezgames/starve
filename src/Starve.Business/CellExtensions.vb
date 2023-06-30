Imports System.Runtime.CompilerServices
Imports Starve.Persistence

Public Module CellExtensions
    <Extension>
    Function GetGlyph(cell As ICell) As Char
        Return cell.TerrainType.ToTerrainTypeDescriptor.Glyph
    End Function
    <Extension>
    Function GetHue(cell As ICell) As Integer
        Return cell.TerrainType.ToTerrainTypeDescriptor.Hue
    End Function
End Module
