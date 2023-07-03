Friend Class InventoryDetailState
    Inherits BasePickerState(Of IGameContext, String)

    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext(Of IGameContext))
        MyBase.New(parent, setState, context, "<placeholder>", context.ControlsText("Select", "Cancel"), GameState.Inventory)
    End Sub

    Protected Overrides Sub OnActivateMenuItem(value As (String, String))
        Select Case value.Item2
            Case DropText
                SetState(GameState.Drop)
        End Select
    End Sub

    Protected Overrides Function InitializeMenuItems() As List(Of (String, String))
        Dim items = Context.Game.World.Avatar.Items.Where(Function(x) x.Name = ItemName)
        Dim item = items.First
        Dim itemCount = items.Count
        HeaderText = $"{ItemName}(x{itemCount})"
        Dim result As New List(Of (String, String)) From {
            (DropText, DropText)
        }
        Return result
    End Function

    Public Overrides Sub OnStart()
        Dim avatar = Context.Game.World.Avatar
        Dim items = avatar.Items.Where(Function(x) x.Name = ItemName)
        If Not items.Any Then
            SetState(GameState.Inventory)
            Return
        End If
        MyBase.OnStart()
    End Sub

    Friend Shared Property ItemName As String
End Class
