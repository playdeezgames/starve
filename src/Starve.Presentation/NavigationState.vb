Imports AOS.UI
Imports Starve.Business
Imports Starve.Persistence

Friend Class NavigationState
    Inherits BaseGameState

    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext)
        MyBase.New(parent, setState, context)
    End Sub
    Public Overrides Sub HandleCommand(cmd As String)
        SetState(BoilerplateState.GameMenu)
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink)
        displayBuffer.Fill((0, 0), Context.ViewSize, Hue.DarkGray)
        Dim avatar = StarveContext.World.Avatar
        Dim map = avatar.Map
        Dim font = Context.Font(StarveFont)
        Dim offsetX = Context.ViewSize.Item1 \ 2 - CellWidth \ 2 - avatar.Cell.Column * CellWidth
        Dim offsetY = Context.ViewSize.Item2 \ 2 - CellHeight \ 2 - avatar.Cell.Row * CellHeight
        With font
            For column = 0 To map.Columns - 1
                Dim x = offsetX + column * CellWidth
                For row = 0 To map.Rows - 1
                    Dim y = offsetY + row * CellHeight
                    Dim cell = map.GetCell(column, row)
                    RenderTerrain(displayBuffer, font, (x, y), cell)
                    RenderCharacter(displayBuffer, font, (x, y), cell.Character)
                Next
            Next
        End With
    End Sub

    Private Sub RenderCharacter(displayBuffer As IPixelSink, font As Font, position As (x As Integer, y As Integer), character As Persistence.ICharacter)
        If character Is Nothing Then
            Return
        End If
        Dim glyph = character.GetGlyph()
        Dim hue = character.GetHue()
        displayBuffer.Fill(position, (CellWidth, CellHeight), Business.Hue.Black)
        font.WriteText(displayBuffer, position, $"{glyph}", hue)
    End Sub

    Private Sub RenderTerrain(displayBuffer As IPixelSink, font As Font, position As (x As Integer, y As Integer), cell As Persistence.ICell)
        Dim glyph = cell.GetGlyph()
        Dim hue = cell.GetHue()
        displayBuffer.Fill(position, (CellWidth, CellHeight), Business.Hue.Black)
        font.WriteText(displayBuffer, position, $"{glyph}", hue)
    End Sub
End Class
