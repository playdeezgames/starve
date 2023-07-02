Friend Class StatusState
    Inherits BaseGameState

    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext)
        MyBase.New(parent, setState, context)
    End Sub

    Public Overrides Sub HandleCommand(cmd As String)
        Select Case cmd
            Case Command.B, Command.A
                SetState(GameState.ActionMenu)
        End Select
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink)
        displayBuffer.Fill((0, 0), Context.ViewSize, Hue.Black)
        Context.ShowStatusBar(displayBuffer, Context.Font(UIFont), "Space/(A) - Continue", Hue.Black, Hue.LightGray)
    End Sub
End Class
