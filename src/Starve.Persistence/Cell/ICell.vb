Public Interface ICell
    Property Character As ICharacter
    ReadOnly Property HasCharacter As Boolean
    ReadOnly Property Id As Integer
    ReadOnly Property Map As IMap
    ReadOnly Property Column As Integer
    ReadOnly Property Row As Integer
    Property TerrainType As String
End Interface
