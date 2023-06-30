Friend Class MapTypeDescriptor
    ReadOnly Property Size As (Integer, Integer)
    ReadOnly Property DefaultTerrainType As String
    ReadOnly Property SpawnCharacters As IReadOnlyDictionary(Of String, Integer)
    Sub New(
           size As (Integer, Integer),
           Optional defaultTerrainType As String = TerrainTypes.Empty,
           Optional spawnCharacter As IReadOnlyDictionary(Of String, Integer) = Nothing)
        Me.Size = size
        Me.DefaultTerrainType = defaultTerrainType
        Me.SpawnCharacters = If(spawnCharacter, New Dictionary(Of String, Integer))
    End Sub
End Class
