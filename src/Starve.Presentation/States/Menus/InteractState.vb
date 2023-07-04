Friend Class InteractState
    Inherits BasePickerState(Of IGameContext, String)

    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext(Of IGameContext))
        MyBase.New(parent, setState, context, "<placeholder>", context.ControlsText("Select", "Cancel"), BoilerplateState.Neutral)
    End Sub

    Public Overrides Sub HandleCommand(cmd As String)
        If cmd = Command.B Then
            Context.Game.TargetCell = Nothing
        End If
        MyBase.HandleCommand(cmd)
    End Sub

    Protected Overrides Sub OnActivateMenuItem(value As (String, String))
        If Context.Game.DoTargetCellVerb(value.Item2) Then
            MessageState.ReturnState = BoilerplateState.Neutral
            SetState(GameState.Message)
        Else
            SetState(BoilerplateState.Neutral)
        End If
    End Sub

    Protected Overrides Function InitializeMenuItems() As List(Of (String, String))
        Return Context.Game.TargetCellVerbs.Select(Function(x) (x.ToVerbTypeDescriptor.Name, x)).ToList
    End Function
End Class
