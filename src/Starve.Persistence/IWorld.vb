Public Interface IWorld
    Sub Save(filename As String)
    Function CreateMap(mapType As String, size As (Integer, Integer), terrainType As String) As IMap
End Interface
