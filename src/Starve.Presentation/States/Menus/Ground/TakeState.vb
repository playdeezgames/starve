Friend Class TakeState
    Inherits BasePickerState(Of String)
    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext)
        MyBase.New(parent, setState, context, "<placeholder>", context.ControlsText("Select", "Cancel"), GameState.Ground)
    End Sub

    Protected Overrides Sub OnActivateMenuItem(value As (String, String))
        Dim avatar = Context.World.Avatar
        Dim cell = avatar.Cell
        For Each item In cell.Items.Where(Function(x) x.Name = ItemName).Take(CInt(value.Item2))
            avatar.PickUpItem(item)
        Next
        SetState(GameState.Ground)
    End Sub

    Protected Overrides Function InitializeMenuItems() As List(Of (String, String))
        HeaderText = $"Take How Many {ItemName}?"
        Return Enumerable.Range(1, Context.World.Avatar.Cell.Items.Count(Function(x) x.Name = ItemName)).Select(Function(x) ($"{x}", $"{x}")).ToList
    End Function

    Public Overrides Sub OnStart()
        Dim avatar = Context.World.Avatar
        Dim cell = avatar.Cell
        Dim items = cell.Items.Where(Function(x) x.Name = ItemName)
        If items.Count <= 1 Then
            For Each item In items
                avatar.PickUpItem(item)
            Next
            SetState(GameState.Ground)
            Return
        End If
        MyBase.OnStart()
    End Sub

    Friend Shared Property ItemName As String
End Class
