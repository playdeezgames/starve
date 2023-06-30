Friend Class Map
    Inherits MapDataClient
    Implements IMap
    Public Sub New(worldData As Data.WorldData, mapId As Integer)
        MyBase.New(worldData, mapId)
    End Sub
End Class
