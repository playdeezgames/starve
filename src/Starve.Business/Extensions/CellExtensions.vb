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
    <Extension>
    Function IsTenable(cell As ICell) As Boolean
        Return cell.TerrainType.ToTerrainTypeDescriptor.Tenable
    End Function
    Private ReadOnly neighborDeltas As IReadOnlyList(Of (Integer, Integer)) =
        New List(Of (Integer, Integer)) From
        {
            (0, -1),
            (1, 0),
            (0, 1),
            (-1, 0)
        }
    <Extension>
    Function Neighbors(cell As ICell) As IEnumerable(Of ICell)
        Return neighborDeltas.Select(Function(delta) cell.Map.GetCell(cell.Column + delta.Item1, cell.Row + delta.Item2)).Where(Function(x) x IsNot Nothing)
    End Function
End Module
