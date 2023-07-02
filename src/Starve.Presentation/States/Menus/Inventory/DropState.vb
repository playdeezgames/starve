Imports Starve.Persistence

Friend Class DropState
    Inherits BasePickerState(Of String)

    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext)
        MyBase.New(parent, setState, context, "<placeholder>", context.ControlsText("Select", "Cancel"), GameState.InventoryDetail)
    End Sub

    Protected Overrides Sub OnActivateMenuItem(value As (String, String))
        Dim avatar = Context.World.Avatar
        Dim items = avatar.Items.Where(Function(x) x.Name = InventoryDetailState.ItemName).Take(CInt(value.Item2))
        For Each item In items
            avatar.DropItem(item)
        Next
        SetState(GameState.InventoryDetail)
    End Sub

    Protected Overrides Function InitializeMenuItems() As List(Of (String, String))
        HeaderText = $"Drop How Many {InventoryDetailState.ItemName}?"
        Return Enumerable.Range(1, Context.World.Avatar.Items.Count(Function(x) x.Name = InventoryDetailState.ItemName)).Select(Function(x) ($"{x}", $"{x}")).ToList
    End Function

    Public Overrides Sub OnStart()
        Dim avatar = Context.World.Avatar
        Dim items = avatar.Items.Where(Function(x) x.Name = InventoryDetailState.ItemName)
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
