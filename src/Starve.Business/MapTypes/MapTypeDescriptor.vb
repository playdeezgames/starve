Imports Starve.Persistence

Friend Class MapTypeDescriptor
    ReadOnly Property Size As (Integer, Integer)
    ReadOnly Property DefaultTerrainType As String
    ReadOnly Property SpawnCharacters As IReadOnlyDictionary(Of String, Integer)
    ReadOnly Property CustomInitializer As Action(Of IMap)
    Sub New(
           size As (Integer, Integer),
           Optional defaultTerrainType As String = TerrainTypes.Empty,
           Optional spawnCharacter As IReadOnlyDictionary(Of String, Integer) = Nothing,
           Optional customInitializer As Action(Of IMap) = Nothing)
        Me.Size = size
        Me.DefaultTerrainType = defaultTerrainType
        Me.SpawnCharacters = If(spawnCharacter, New Dictionary(Of String, Integer))
        Me.CustomInitializer = If(customInitializer, AddressOf DoNothing)
    End Sub
    Private Sub DoNothing(map As IMap)
        'like it sez....
    End Sub
End Class
