Imports Starve.Persistence

Public Interface IGameContext
    Property World As IWorld
    Sub Embark()
    Property TargetCell As ICell
    Property ItemName As String
End Interface
