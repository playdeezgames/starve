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
        SetState(GameState.Navigation)
    End Sub
End Class