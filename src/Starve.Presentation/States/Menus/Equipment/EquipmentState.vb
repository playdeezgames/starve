Friend Class EquipmentState
    Inherits BasePickerState(Of IGameContext, String)

    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext(Of IGameContext))
        MyBase.New(parent, setState, context, "Equipment", context.ControlsText("Select", "Cancel"), GameState.ActionMenu)
    End Sub

    Protected Overrides Sub OnActivateMenuItem(value As (String, String))
        Context.Game.EquipSlotType = value.Item2
        SetState(GameState.EquipmentDetail)
    End Sub

    Protected Overrides Function InitializeMenuItems() As List(Of (String, String))
        Return Context.Game.EquippedSlots.Select(Function(x) ($"{Context.Game.EquipSlotName(x)}: {Context.Game.EquippedItem(x).Name}", x)).ToList
    End Function
End Class
