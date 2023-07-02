Public Interface ICell
    Property Character As ICharacter
    ReadOnly Property HasCharacter As Boolean
    ReadOnly Property Id As Integer
    ReadOnly Property Map As IMap
    ReadOnly Property Column As Integer
    ReadOnly Property Row As Integer
    Property TerrainType As String
    Sub AddItem(item As IItem)
    ReadOnly Property TopItem As IItem
    ReadOnly Property HasItems As Boolean
End Interface
