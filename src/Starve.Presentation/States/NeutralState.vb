﻿Imports AOS.UI
Imports Starve.Business

Friend Class NeutralState
    Inherits BaseGameState

    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext)
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
        Dim avatar = Context.World.Avatar
        If avatar.IsInCombat Then
            SetState(GameState.Combat)
            Return
        End If
        If avatar.IsDead Then
            SetState(GameState.Dead)
            Return
        End If
        SetState(GameState.Navigation)
    End Sub
End Class
