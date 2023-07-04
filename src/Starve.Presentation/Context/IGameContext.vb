Imports Starve.Persistence

Public Interface IGameContext
    Property World As IWorld
    Sub Embark()
    Sub Attack()
    Sub Run()
    Sub TakeItems(itemCount As Integer)
    Sub DropItems(itemCount As Integer)
    Function ItemCountByName(itemName As String) As Integer
    Function DoItemVerb(verbType As String) As Boolean
    Function VerbTypesByItemName(itemName As String) As IEnumerable(Of String)
    Property TargetCell As ICell
    Property ItemName As String
    ReadOnly Property IsInCombat As Boolean
    ReadOnly Property IsInteracting As Boolean
    ReadOnly Property GroundItems As IEnumerable(Of IItem)
    ReadOnly Property GroundItemsByName(name As String) As IEnumerable(Of IItem)
    ReadOnly Property GroundItemCountByName(name As String) As Integer
    ReadOnly Property HasGroundItems As Boolean
    ReadOnly Property Items As IEnumerable(Of IItem)
    ReadOnly Property HasItems As Boolean
End Interface
