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
    End Sub
End Module
