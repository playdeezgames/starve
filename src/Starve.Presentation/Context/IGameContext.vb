Imports Starve.Persistence

Public Interface IGameContext
    Property World As IWorld
    Sub Embark()
    Property TargetCell As ICell
    Property ItemName As String
    ReadOnly Property IsInCombat As Boolean
End Interface
