Imports Starve.Persistence

Friend Class InventoryDetailState
    Inherits BasePickerState(Of IGameContext, String)

    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext(Of IGameContext))
        MyBase.New(parent, setState, context, "<placeholder>", context.ControlsText("Select", "Cancel"), GameState.Inventory)
    End Sub

    Protected Overrides Sub OnActivateMenuItem(value As (String, String))
        Select Case value.Item2
            Case DropText
                SetState(GameState.Drop)
            Case Else
                Context.Game.World.Avatar.Items.First(Function(x) x.Name = Context.Game.ItemName).DoVerb(value.Item2, Context.Game.World.Avatar)
                If Context.Game.World.HasMessages Then
                    MessageState.ReturnState = GameState.InventoryDetail
                    SetState(GameState.Message)
                Else
                    OnStart()
                End If
        End Select
    End Sub

    Protected Overrides Function InitializeMenuItems() As List(Of (String, String))
        Dim items = Context.Game.World.Avatar.Items.Where(Function(x) x.Name = Context.Game.ItemName)
        Dim item = items.First
        Dim itemCount = items.Count
        HeaderText = $"{Context.Game.ItemName}(x{itemCount})"
        Dim result As New List(Of (String, String)) From {
            (DropText, DropText)
        }
        For Each verbType In item.VerbTypes
            result.Add((verbType.ToVerbTypeDescriptor.Name, verbType))
        Next
        Return result
    End Function

    Public Overrides Sub OnStart()
        Dim avatar = Context.Game.World.Avatar
        Dim items = avatar.Items.Where(Function(x) x.Name = Context.Game.ItemName)
        If Not items.Any Then
            SetState(GameState.Inventory)
            Return
        End If
        MyBase.OnStart()
    End Sub
End Class
