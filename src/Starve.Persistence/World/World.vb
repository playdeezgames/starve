Imports System.IO
Imports System.Text.Json
Imports Starve.Data

Public Class World
    Inherits WorldDataClient
    Implements IWorld
    Public Sub New(worldData As Data.WorldData)
        MyBase.New(worldData)
    End Sub

    Public Property Avatar As ICharacter Implements IWorld.Avatar
        Get
            If WorldData.AvatarCharacterId.HasValue Then
                Return New Character(WorldData, WorldData.AvatarCharacterId.Value)
            End If
            Return Nothing
        End Get
        Set(value As ICharacter)
            WorldData.AvatarCharacterId = value?.Id
        End Set
    End Property

    Public ReadOnly Property Characters As IEnumerable(Of ICharacter) Implements IWorld.Characters
        Get
            Return Enumerable.Range(0, WorldData.Characters.Count).Select(Function(x) New Character(WorldData, x))
        End Get
    End Property

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

    Public Function CreateMap(mapType As String, size As (Integer, Integer), terrainType As String) As IMap Implements IWorld.CreateMap
        Dim mapId = WorldData.Maps.Count
        Dim mapData =
            New MapData With
            {
                .MapType = mapType,
                .Columns = size.Item1,
                .Rows = size.Item2
            }
        While mapData.Cells.Count < size.Item1 * size.Item2
            Dim cellData = New CellData With
                {
                    .TerrainType = terrainType
                }
            mapData.Cells.Add(cellData)
        End While
        WorldData.Maps.Add(mapData)
        Return New Map(WorldData, mapId)
    End Function

    Public Function CreateCharacter(characterType As String, cell As ICell) As ICharacter Implements IWorld.CreateCharacter
        Dim characterData As New CharacterData With
            {
                .Recycled = False,
                .CharacterType = characterType,
                .MapId = cell.Map.Id,
                .CellIndex = cell.Id,
                .TargetCellIndex = cell.Id
            }
        Dim index = WorldData.Characters.FindIndex(Function(x) x.Recycled)
        If index = -1 Then
            index = WorldData.Characters.Count
            WorldData.Characters.Add(characterData)
        Else
            WorldData.Characters(index) = characterData
        End If
        Return New Character(WorldData, index)
    End Function
End Class
