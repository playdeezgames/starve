Public Interface IMap
    ReadOnly Property Columns As Integer
    ReadOnly Property Rows As Integer
    ReadOnly Property Cells As IEnumerable(Of ICell)
    ReadOnly Property World As IWorld
    ReadOnly Property Id As Integer
End Interface
