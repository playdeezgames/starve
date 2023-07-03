Friend Class TakeState
    Inherits BasePickerState(Of IGameContext, Integer)
    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext(Of IGameContext))
        MyBase.New(parent, setState, context, "<placeholder>", context.ControlsText("Select", "Cancel"), GameState.Ground)
    End Sub

    Protected Overrides Sub OnActivateMenuItem(value As (String, Integer))
        Dim avatar = Context.Game.World.Avatar
        Dim cell = avatar.Cell
        For Each item In cell.Items.Where(Function(x) x.Name = Context.Game.ItemName).Take(value.Item2)
            avatar.PickUpItem(item)
        Next
        SetState(GameState.Ground)
    End Sub

    Protected Overrides Function InitializeMenuItems() As List(Of (String, Integer))
        HeaderText = $"Take How Many {Context.Game.ItemName}?"
        Return Enumerable.Range(1, Context.Game.World.Avatar.Cell.Items.Count(Function(x) x.Name = Context.Game.ItemName)).Select(Function(x) ($"{x}", x)).ToList
    End Function

    Public Overrides Sub OnStart()
        Dim avatar = Context.Game.World.Avatar
        Dim cell = avatar.Cell
        Dim items = cell.Items.Where(Function(x) x.Name = Context.Game.ItemName)
        If items.Count <= 1 Then
            For Each item In items
                avatar.PickUpItem(item)
            Next
            SetState(GameState.Ground)
            Return
        End If
        MyBase.OnStart()
    End Sub
End Class
