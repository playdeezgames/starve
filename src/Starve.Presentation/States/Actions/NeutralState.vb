Imports AOS.UI
Imports Starve.Business

Friend Class NeutralState
    Inherits BaseGameState(Of IGameContext)

    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext(Of IGameContext))
        MyBase.New(parent, setState, context)
    End Sub

    Public Overrides Sub HandleCommand(cmd As String)
        Throw New NotImplementedException
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink)
        Throw New NotImplementedException
    End Sub
    Public Overrides Sub OnStart()
        MyBase.OnStart()
        If Context.Game.HasMessages Then
            MessageState.ReturnState = BoilerplateState.Neutral
            SetState(Message)
            Return
        End If
        Dim avatar = Context.Game.Avatar
        If Not avatar.IsDead Then
            If Context.Game.IsInCombat Then
                SetState(GameState.Combat)
                Return
            ElseIf Context.Game.IsInteracting Then
                SetState(GameState.Interact)
                Return
            End If
        End If
        If avatar.IsDead Then
            SetState(GameState.Dead)
            Return
        End If
        SetState(GameState.Navigation)
    End Sub
End Class
