Public Interface IWorld
    Sub Save(filename As String)
    Function CreateMap(mapType As String, size As (Integer, Integer), terrainType As String) As IMap
    Function CreateCharacter(characterType As String, cell As ICell) As ICharacter
    Property Avatar As ICharacter
    ReadOnly Property Characters As IEnumerable(Of ICharacter)
End Interface
