Friend Class DropState
    Inherits BasePickerState(Of IGameContext, Integer)

    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext(Of IGameContext))
        MyBase.New(parent, setState, context, "<placeholder>", context.ControlsText("Select", "Cancel"), GameState.InventoryDetail)
    End Sub

    Protected Overrides Sub OnActivateMenuItem(value As (String, Integer))
        Dim avatar = Context.Game.World.Avatar
        Dim items = avatar.Items.Where(Function(x) x.Name = Context.Game.ItemName).Take(value.Item2)
        For Each item In items
            avatar.DropItem(item)
        Next
        SetState(GameState.InventoryDetail)
    End Sub

    Protected Overrides Function InitializeMenuItems() As List(Of (String, Integer))
        HeaderText = $"Drop How Many {Context.Game.ItemName}?"
        Return Enumerable.Range(1, Context.Game.World.Avatar.Items.Count(Function(x) x.Name = Context.Game.ItemName)).Select(Function(x) ($"{x}", x)).ToList
    End Function

    Public Overrides Sub OnStart()
        Dim avatar = Context.Game.World.Avatar
        Dim items = avatar.Items.Where(Function(x) x.Name = Context.Game.ItemName)
        If items.Count <= 1 Then
            For Each item In items
                avatar.DropItem(item)
            Next
            SetState(GameState.Inventory)
            Return
        End If
        MyBase.OnStart()
    End Sub
End Class
