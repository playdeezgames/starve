Imports AOS.UI
Imports Starve.Persistence

Friend Class CombatState
    Inherits BasePickerState(Of String)

    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext)
        MyBase.New(parent, setState, context, "<placeholder>", context.ControlsText("Select", "RUN!!"), GameState.Run)
    End Sub

    Protected Overrides Sub OnActivateMenuItem(value As (String, String))
        Select Case value.Item2
            Case RunText
                SetState(GameState.Run)
            Case AttackText
                SetState(GameState.Attack)
            Case Else
                Throw New NotImplementedException
        End Select
    End Sub

    Protected Overrides Function InitializeMenuItems() As List(Of (String, String))
        Dim avatar = Context.World.Avatar
        Dim target = TargetCell.Character
        HeaderText = $"{avatar.Name}({avatar.Health}/{avatar.MaximumHealth}) v. {target.Name}({target.Health}/{target.MaximumHealth})"
        Return New List(Of (String, String)) From
            {
                (AttackText, AttackText),
                (RunText, RunText)
            }
    End Function

    Friend Shared Property TargetCell As ICell
End Class
