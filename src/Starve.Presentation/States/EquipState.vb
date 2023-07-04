Imports Starve.Persistence

Friend Class EquipState
    Inherits BasePickerState(Of IGameContext, IItem)

    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext(Of IGameContext))
        MyBase.New(parent, setState, context, "<placeholder>", context.ControlsText("Equip", "Cancel"), GameState.InventoryDetail)
    End Sub

    Protected Overrides Sub OnActivateMenuItem(value As (String, IItem))
        Context.Game.Equip(value.Item2)
        SetState(GameState.InventoryDetail)
    End Sub

    Protected Overrides Function InitializeMenuItems() As List(Of (String, IItem))
        HeaderText = $"Equip Which {Context.Game.ItemName}?"
        Return Context.Game.ItemsByName(Context.Game.ItemName).Select(Function(item) ($"{item.Name}({item.Durability}/{item.MaximumDurability})", item)).ToList
    End Function
End Class
