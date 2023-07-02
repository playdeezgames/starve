Friend Class InventoryState
    Inherits BasePickerState(Of String)
    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext)
        MyBase.New(parent, setState, context, "Inventory", context.ControlsText("Select", "Cancel"), GameState.ActionMenu)
    End Sub

    Protected Overrides Sub OnActivateMenuItem(value As (String, String))
        InventoryDetailState.ItemName = value.Item2
        SetState(GameState.InventoryDetail)
    End Sub

    Protected Overrides Function InitializeMenuItems() As List(Of (String, String))
        Return Context.World.Avatar.Items.GroupBy(Function(x) x.Name).Select(Function(x) ($"{x.Key}(x{x.Count})", x.Key)).ToList
    End Function
    Public Overrides Sub OnStart()
        If Not Context.World.Avatar.HasItems Then
            SetState(GameState.ActionMenu)
            Return
        End If
        MyBase.OnStart()
    End Sub

End Class
