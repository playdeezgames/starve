Friend Class EquipState
    Inherits BasePickerState(Of IGameContext, IItem)
    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext(Of IGameContext))
        MyBase.New(parent, setState, context, "<placeholder>", context.ControlsText("Equip", "Cancel"), GameState.InventoryDetail)
    End Sub
    Protected Overrides Sub OnActivateMenuItem(value As (String, IItem))
        Game.Equip(value.Item2)
        MessageState.ReturnState = GameState.InventoryDetail
        SetState(Message)
    End Sub
    Protected Overrides Function InitializeMenuItems() As List(Of (String, IItem))
        HeaderText = $"Equip Which {Game.ItemName}?"
        Return Game.ItemsByName(Game.ItemName).Select(Function(item) ($"{item.Name}({item.Durability}/{item.MaximumDurability})", item)).ToList
    End Function
    Public Overrides Sub OnStart()
        MyBase.OnStart()
        Select Case Game.ItemCountByName(Game.ItemName)
            Case 0
                SetState(GameState.InventoryDetail)
            Case 1
                Game.Equip(Game.ItemsByName(Game.ItemName).Single)
                MessageState.ReturnState = GameState.Inventory
                SetState(Message)
        End Select
    End Sub
End Class
