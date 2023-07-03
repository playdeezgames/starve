Imports Starve.Persistence

Public Interface IGameContext
    Property World As IWorld
    Sub Embark()
    Sub Attack()
    Sub Run()
    Sub TakeItems(item2 As Integer)
    Property TargetCell As ICell
    Property ItemName As String
    ReadOnly Property IsInCombat As Boolean
    ReadOnly Property IsInteracting As Boolean
    ReadOnly Property GroundItems As IEnumerable(Of IItem)
    ReadOnly Property GroundItemsByName(name As String) As IEnumerable(Of IItem)
    ReadOnly Property GroundItemCountByName(name As String) As Integer
    ReadOnly Property HasGroundItems As Boolean
End Interface
