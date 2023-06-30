Imports AOS.UI
Imports Starve.Business

Friend Class NavigationState
    Inherits BaseGameState

    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext)
        MyBase.New(parent, setState, context)
    End Sub
    Public Overrides Sub HandleCommand(cmd As String)
        SetState(BoilerplateState.GameMenu)
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink)
        displayBuffer.Fill((0, 0), Context.ViewSize, Hue.Black)
        With Context.Font(UIFont)
            .WriteText(displayBuffer, (0, 0), "Yer playin' the game!", Hue.White)
        End With
    End Sub
End Class
