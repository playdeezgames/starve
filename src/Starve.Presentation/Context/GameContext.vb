Imports Starve.Persistence

Friend Class GameContext
    Implements IGameContext

    Public Property World As IWorld Implements IGameContext.World

    Public Property TargetCell As ICell Implements IGameContext.TargetCell

    Public Property ItemName As String Implements IGameContext.ItemName

    Public ReadOnly Property IsInCombat As Boolean Implements IGameContext.IsInCombat
        Get
            Return TargetCell?.Character IsNot Nothing
        End Get
    End Property

    Public Sub Embark() Implements IGameContext.Embark
        World = New World(New Data.WorldData)
        WorldInitializer.Initialize(World)
    End Sub
End Class
