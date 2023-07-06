Imports Starve.Persistence

Public Interface IGameContext
    Sub Embark()
    Sub Attack()
    Sub Run()
    Sub TakeItems(itemCount As Integer)
    Sub DropItems(itemCount As Integer)
    Function ItemCountByName(itemName As String) As Integer
    Function DoItemVerb(verbType As String) As Boolean
    Function VerbTypesByItemName(itemName As String) As IEnumerable(Of String)
    Property ItemName As String
    ReadOnly Property IsInCombat As Boolean
    ReadOnly Property IsInteracting As Boolean
    ReadOnly Property GroundItems As IEnumerable(Of IItem)
    ReadOnly Property GroundItemsByName(name As String) As IEnumerable(Of IItem)
    ReadOnly Property GroundItemCountByName(name As String) As Integer
    ReadOnly Property HasGroundItems As Boolean
    ReadOnly Property Items As IEnumerable(Of IItem)
    ReadOnly Property HasItems As Boolean
    ReadOnly Property Avatar As ICharacter
    ReadOnly Property TargetCharacter As ICharacter
    ReadOnly Property TargetTerrainName As String
    ReadOnly Property TargetCellVerbs As IEnumerable(Of String)
    Sub DismissMessage()
    Sub Abandon()
    Sub Load(filename As String)
    Sub Save(filename As String)
    ReadOnly Property HasMessages As Boolean
    ReadOnly Property CurrentMessage As IMessage
    ReadOnly Property CanEquipItem As Boolean
    ReadOnly Property ItemsByName(name As String) As IEnumerable(Of IItem)
    Function DoTargetCellVerb(verbType As String) As Boolean
    Sub Equip(item As IItem)
    Sub Unequip()
    Sub Craft(recipeIndex As Integer)
    ReadOnly Property HasEquipment As Boolean
    ReadOnly Property EquippedSlots As IEnumerable(Of String)
    ReadOnly Property EquipSlotName(equipSlotType As String) As String
    ReadOnly Property EquippedItem(equipSlotType As String) As IItem
    Property EquipSlotType As String
    ReadOnly Property CanCraft As Boolean
    ReadOnly Property AvailableRecipes As IEnumerable(Of (String, Integer))
    ReadOnly Property AvailableVerbs As IEnumerable(Of (String, String))
    Sub DoVerb(verbType As String)
    Sub ClearTargetCell()
    Sub Move(deltaX As Integer, deltaY As Integer)
    ReadOnly Property IsDead As Boolean
End Interface
