Imports SPLORR.Game
Imports Starve.Persistence

Friend Module MapInitializer
    Friend Sub Initialize(world As IWorld)
        For Each mapType In MapTypes.All
            Dim descriptor = mapType.ToMapTypeDescriptor
            InitializeMap(world, mapType, descriptor)
        Next
    End Sub
    Private Sub InitializeMap(world As IWorld, mapType As String, descriptor As MapTypeDescriptor)
        Dim map = world.CreateMap(mapType, descriptor.Size, descriptor.DefaultTerrainType)
        PopulateCharacters(map, descriptor.SpawnCharacters)
    End Sub
    Private Sub PopulateCharacters(map As IMap, spawnCharacters As IReadOnlyDictionary(Of String, Integer))
        For Each entry In spawnCharacters
            Dim characterType = entry.Key
            Dim count = entry.Value
            While count > 0
                PopulateCharacter(map, characterType)
                count -= 1
            End While
        Next
    End Sub

    Private Sub PopulateCharacter(map As IMap, characterType As String)
        Dim candidate = RNG.FromEnumerable(map.Cells.Where(Function(x) x.Character Is Nothing))
        candidate.Character = map.World.CreateCharacter(characterType, candidate)
    End Sub
End Module
