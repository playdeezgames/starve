Friend Class ActionMenuState
    Inherits BasePickerState

    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext)
        MyBase.New(
            parent,
            setState,
            context,
            "Actions Menu",
            context.ControlsText("Select", "Cancel"),
            BoilerplateState.Neutral)
    End Sub

    Protected Overrides Sub OnActivateMenuItem(value As (String, String))
        Select Case value.Item2
            Case InventoryText
                SetState(GameState.Inventory)
            Case StatusText
                SetState(GameState.Status)
            Case PickUpText
                SetState(GameState.Ground)
        End Select
    End Sub

    Protected Overrides Function InitializeMenuItems() As List(Of (String, String))
        Dim result As New List(Of (String, String))
        Dim avatar = Context.World.Avatar
        If avatar.Cell.HasItems Then
            result.Add((PickUpText, PickUpText))
        End If
        If avatar.HasItems Then
            result.Add((InventoryText, InventoryText))
        End If
        result.Add((StatusText, StatusText))
        Return result
    End Function
End Class
