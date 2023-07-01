Imports AOS.UI
Imports Starve.Business

Friend Class DeadState
    Inherits BaseGameState

    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext)
        MyBase.New(parent, setState, context)
    End Sub

    Public Overrides Sub HandleCommand(cmd As String)
        If cmd = Command.A Then
            Context.AbandonGame()
            SetState(BoilerplateState.MainMenu)
        End If
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink)
        displayBuffer.Fill((0, 0), Context.ViewSize, Hue.Black)
        Dim font = Context.Font(UIFont)
        With font
            Dim text = "Yer Dead!"
            .WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(text) \ 2, ViewHeight \ 2 - font.Height \ 2), text, Hue.Red)
        End With
        Context.ShowStatusBar(displayBuffer, font, "Space/(A) - Continue", Hue.Black, Hue.LightGray)
    End Sub
End Class
