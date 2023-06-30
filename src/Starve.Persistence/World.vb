Imports System.IO
Imports System.Text.Json
Imports Starve.Data

Public Class World
    Inherits WorldDataClient
    Implements IWorld
    Public Sub New(worldData As Data.WorldData)
        MyBase.New(worldData)
    End Sub

    Public Sub Save(filename As String) Implements IWorld.Save
        File.WriteAllText(filename, JsonSerializer.Serialize(WorldData))
    End Sub
    Public Shared Function Load(filename As String) As IWorld
        Try
            Return New World(JsonSerializer.Deserialize(Of WorldData)(File.ReadAllText(filename)))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function CreateMap(mapType As String, size As (Integer, Integer)) As IMap Implements IWorld.CreateMap
        Dim mapId = WorldData.Maps.Count
        WorldData.Maps.Add(
            New MapData With
            {
                .MapType = mapType,
                .Columns = size.Item1,
                .Rows = size.Item2
            })
        Return New Map(WorldData, mapId)
    End Function
End Class
