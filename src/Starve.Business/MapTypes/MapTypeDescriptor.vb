Friend Class MapTypeDescriptor
    ReadOnly Property Size As (Integer, Integer)
    ReadOnly Property DefaultTerrainType As String
    Sub New(size As (Integer, Integer), Optional defaultTerrainType As String = TerrainTypes.Empty)
        Me.Size = size
        Me.DefaultTerrainType = defaultTerrainType
    End Sub
End Class
