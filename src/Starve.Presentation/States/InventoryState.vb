Friend Class InventoryState
    Inherits BasePickerState
    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext)
        MyBase.New(parent, setState, context, "Inventory", context.ControlsText("Select", "Cancel"), GameState.ActionMenu)
    End Sub

    Protected Overrides Sub OnActivateMenuItem(value As (String, String))
        Throw New NotImplementedException()
    End Sub

    Protected Overrides Function InitializeMenuItems() As List(Of (String, String))
        Throw New NotImplementedException()
    End Function
End Class
