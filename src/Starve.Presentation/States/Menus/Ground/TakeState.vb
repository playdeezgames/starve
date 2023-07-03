Friend Class TakeState
    Inherits BasePickerState(Of IGameContext, Integer)
    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext(Of IGameContext))
        MyBase.New(parent, setState, context, "<placeholder>", context.ControlsText("Select", "Cancel"), GameState.Ground)
    End Sub

    Protected Overrides Sub OnActivateMenuItem(value As (String, Integer))
        Context.Game.TakeItems(value.Item2)
        SetState(GameState.Ground)
    End Sub

    Protected Overrides Function InitializeMenuItems() As List(Of (String, Integer))
        HeaderText = $"Take How Many {Context.Game.ItemName}?"
        Return Enumerable.Range(1, Context.Game.GroundItemCountByName(Context.Game.ItemName)).Select(Function(x) ($"{x}", x)).ToList
    End Function

    Public Overrides Sub OnStart()
        Dim itemCount = Context.Game.GroundItemCountByName(Context.Game.ItemName)
        If itemCount <= 1 Then
            Context.Game.TakeItems(itemCount)
            SetState(GameState.Ground)
            Return
        End If
        MyBase.OnStart()
    End Sub
End Class
