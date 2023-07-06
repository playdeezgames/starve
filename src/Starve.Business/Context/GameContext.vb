Imports Starve.Persistence

Public Class GameContext
    Implements IGameContext

    Private Property World As IWorld

    Public Property TargetCell As ICell Implements IGameContext.TargetCell

    Public Property ItemName As String Implements IGameContext.ItemName

    Public ReadOnly Property IsInCombat As Boolean Implements IGameContext.IsInCombat
        Get
            Return TargetCell?.Character IsNot Nothing
        End Get
    End Property

    Public ReadOnly Property IsInteracting As Boolean Implements IGameContext.IsInteracting
        Get
            Return If(TargetCell?.TerrainType.ToTerrainTypeDescriptor.CanInteract, False)
        End Get
    End Property

    Public ReadOnly Property GroundItems As IEnumerable(Of IItem) Implements IGameContext.GroundItems
        Get
            Return World.Avatar.Cell.Items
        End Get
    End Property

    Public ReadOnly Property HasGroundItems As Boolean Implements IGameContext.HasGroundItems
        Get
            Return World.Avatar.Cell.HasItems
        End Get
    End Property

    Public ReadOnly Property GroundItemsByName(name As String) As IEnumerable(Of IItem) Implements IGameContext.GroundItemsByName
        Get
            Return GroundItems.Where(Function(x) x.Name = name)
        End Get
    End Property

    Public ReadOnly Property ItemsByName(name As String) As IEnumerable(Of IItem) Implements IGameContext.ItemsByName
        Get
            Return Items.Where(Function(x) x.Name = name)
        End Get
    End Property

    Public ReadOnly Property GroundItemCountByName(name As String) As Integer Implements IGameContext.GroundItemCountByName
        Get
            Return GroundItems.Count(Function(x) x.Name = name)
        End Get
    End Property

    Public ReadOnly Property Items As IEnumerable(Of IItem) Implements IGameContext.Items
        Get
            Return World.Avatar.Items
        End Get
    End Property

    Public ReadOnly Property HasItems As Boolean Implements IGameContext.HasItems
        Get
            Return World.Avatar.HasItems
        End Get
    End Property

    Public ReadOnly Property Avatar As ICharacter Implements IGameContext.Avatar
        Get
            Return World.Avatar
        End Get
    End Property

    Public ReadOnly Property TargetCharacter As ICharacter Implements IGameContext.TargetCharacter
        Get
            Return TargetCell?.Character
        End Get
    End Property

    Public ReadOnly Property TargetCellVerbs As IEnumerable(Of String) Implements IGameContext.TargetCellVerbs
        Get
            Return If(TargetCell?.TerrainType?.ToTerrainTypeDescriptor?.AllVerbs, Array.Empty(Of String))
        End Get
    End Property

    Public ReadOnly Property HasMessages As Boolean Implements IGameContext.HasMessages
        Get
            Return World.HasMessages
        End Get
    End Property

    Public ReadOnly Property CurrentMessage As IMessage Implements IGameContext.CurrentMessage
        Get
            Return World.CurrentMessage
        End Get
    End Property

    Public ReadOnly Property TargetTerrainName As String Implements IGameContext.TargetTerrainName
        Get
            Return TargetCell.TerrainType.ToTerrainTypeDescriptor.Name
        End Get
    End Property

    Public ReadOnly Property CanEquipItem As Boolean Implements IGameContext.CanEquipItem
        Get
            Return If(ItemsByName(ItemName).FirstOrDefault?.CanEquip, False)
        End Get
    End Property

    Public ReadOnly Property HasEquipment As Boolean Implements IGameContext.HasEquipment
        Get
            Return Avatar.HasEquipment
        End Get
    End Property

    Public ReadOnly Property EquippedSlots As IEnumerable(Of String) Implements IGameContext.EquippedSlots
        Get
            Return Avatar.Equipment.Keys
        End Get
    End Property

    Public ReadOnly Property EquipSlotName(equipSlotType As String) As String Implements IGameContext.EquipSlotName
        Get
            Return equipSlotType.ToEquipSlotTypeDescriptor.Name
        End Get
    End Property

    Public ReadOnly Property EquippedItem(equipSlotType As String) As IItem Implements IGameContext.EquippedItem
        Get
            Return Avatar.Equipment(equipSlotType)
        End Get
    End Property

    Public Property EquipSlotType As String Implements IGameContext.EquipSlotType

    Public ReadOnly Property CanCraft As Boolean Implements IGameContext.CanCraft
        Get
            Return Avatar.CanCraft
        End Get
    End Property

    Public ReadOnly Property AvailableRecipes As IEnumerable(Of (String, Integer)) Implements IGameContext.AvailableRecipes
        Get
            Return Enumerable.Range(0, RecipeTypes.Descriptors.Count).
                Select(Function(x) (x, RecipeTypes.Descriptors(x))).
                Where(Function(x) x.Item2.CanCraft(Avatar)).Select(Function(x) (x.Item2.Name, x.Item1))
        End Get
    End Property

    Public ReadOnly Property AvailableVerbs As IEnumerable(Of (String, String)) Implements IGameContext.AvailableVerbs
        Get
            Return Avatar.Cell.TerrainType.ToTerrainTypeDescriptor.AllVerbs.Select(Function(x) (x.ToVerbTypeDescriptor.Name, x))
        End Get
    End Property

    Public Sub Embark() Implements IGameContext.Embark
        World = New World(New Data.WorldData)
        WorldInitializer.Initialize(World)
    End Sub

    Public Sub Attack() Implements IGameContext.Attack
        World.Avatar.Attack(TargetCell.Character, True)
        If Not TargetCell.HasCharacter Then
            TargetCell = Nothing
        End If
    End Sub

    Public Sub Run() Implements IGameContext.Run
        TargetCell = Nothing
        World.Avatar.Run
    End Sub

    Public Sub TakeItems(itemCount As Integer) Implements IGameContext.TakeItems
        Dim avatar = World.Avatar
        Dim cell = avatar.Cell
        For Each item In cell.Items.Where(Function(x) x.Name = ItemName).Take(itemCount)
            avatar.PickUpItem(item)
        Next
    End Sub

    Public Sub DropItems(itemCount As Integer) Implements IGameContext.DropItems
        Dim avatar = World.Avatar
        Dim items = avatar.Items.Where(Function(x) x.Name = ItemName).Take(itemCount)
        For Each item In items
            avatar.DropItem(item)
        Next
    End Sub

    Public Sub DismissMessage() Implements IGameContext.DismissMessage
        World.DismissMessage()
    End Sub

    Public Sub Abandon() Implements IGameContext.Abandon
        World = Nothing
    End Sub

    Public Sub Load(filename As String) Implements IGameContext.Load
        World = Persistence.World.Load(filename)
    End Sub

    Public Sub Save(filename As String) Implements IGameContext.Save
        World.Save(filename)
    End Sub

    Public Function ItemCountByName(itemName As String) As Integer Implements IGameContext.ItemCountByName
        Return World.Avatar.Items.Count(Function(x) x.Name = itemName)
    End Function

    Public Function DoItemVerb(verbType As String) As Boolean Implements IGameContext.DoItemVerb
        World.Avatar.Items.First(Function(x) x.Name = ItemName).DoVerb(verbType, World.Avatar)
        Return World.HasMessages
    End Function

    Public Function VerbTypesByItemName(itemName As String) As IEnumerable(Of String) Implements IGameContext.VerbTypesByItemName
        Return World.Avatar.Items.First(Function(x) x.Name = itemName).VerbTypes
    End Function

    Public Function DoTargetCellVerb(verbType As String) As Boolean Implements IGameContext.DoTargetCellVerb
        Dim result = TargetCell.DoVerb(verbType, Avatar)
        TargetCell = Nothing
        Return result
    End Function

    Public Sub Equip(item As IItem) Implements IGameContext.Equip
        Avatar.Equip(item.ItemType.ToItemTypeDescriptor.EquipSlotType, item)
        World.CreateMessage().AddLine(LightGray, $"{Avatar.Name} equips {item.Name}.")
    End Sub

    Public Sub Unequip() Implements IGameContext.Unequip
        Avatar.Unequip(EquipSlotType)
    End Sub

    Public Sub Craft(recipeIndex As Integer) Implements IGameContext.Craft
        RecipeTypes.Descriptors(recipeIndex).Craft(Avatar)
    End Sub

    Public Sub DoVerb(verbType As String) Implements IGameContext.DoVerb
        Avatar.Cell.TerrainType.ToTerrainTypeDescriptor.VerbTypes(verbType).Invoke(Avatar, Avatar.Cell)
    End Sub
End Class
