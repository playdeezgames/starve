Imports Starve.Persistence

Public Module WorldInitializer
    Public Sub Initialize(world As IWorld)
        MapInitializer.Initialize(world)
        AvatarInitializer.Initialize(world)
    End Sub
End Module
