Imports Starve.Persistence

Friend Class GameContext
    Implements IGameContext

    Public Property World As IWorld Implements IGameContext.World

    Public Sub Embark() Implements IGameContext.Embark
        World = New World(New Data.WorldData)
        WorldInitializer.Initialize(World)
    End Sub
End Class
