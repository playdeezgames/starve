Imports System.IO
Imports Starve.Persistence

Public Class StarveContext
    Inherits UIContext(Of IGameContext)

    Public Sub New(fontFilenames As IReadOnlyDictionary(Of String, String), viewSize As (Integer, Integer))
        MyBase.New(New GameContext, fontFilenames, viewSize)
    End Sub

    Public Overrides ReadOnly Property AvailableWindowSizes As IEnumerable(Of (Integer, Integer))
        Get
            Return {
                (ViewWidth * 5, ViewHeight * 5),
                (ViewWidth * 15 \ 2, ViewHeight * 15 \ 2),
                (ViewWidth * 10, ViewHeight * 10),
                (ViewWidth * 15, ViewHeight * 15),
                (ViewWidth * 20, ViewHeight * 20),
                (ViewWidth * 25, ViewHeight * 25),
                (ViewWidth * 30, ViewHeight * 30),
                (ViewWidth * 35, ViewHeight * 35),
                (ViewWidth * 40, ViewHeight * 40)
                }
        End Get
    End Property

    Public Overrides Sub ShowSplashContent(displayBuffer As IPixelSink, font As Font)
        Dim text = "Starve!!"
        With font
            .WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(text) \ 2, ViewHeight \ 2 - font.Height \ 2), text, Hue.Yellow)
        End With
        ShowStatusBar(displayBuffer, font, "Space/(A) - Continue", Hue.Black, Hue.LightGray)
    End Sub

    Public Overrides Sub ShowAboutContent(displayBuffer As IPixelSink, font As Font)
        With font
            .WriteText(displayBuffer, (0, 0), "About Starve!!", Hue.Orange)
            .WriteText(displayBuffer, (0, font.Height * 2), "Art:", Hue.White)
            .WriteText(displayBuffer, (0, font.Height * 3), "https://opengameart.org/content/micro-roguelike", Hue.White)
            .WriteText(displayBuffer, (0, font.Height * 5), "A Production of TheGrumpyGameDev", Hue.White)
            .WriteText(displayBuffer, (0, font.Height * 7), "For Retrograde Jam 4, July 1-9, 2023", Hue.White)
            .WriteText(displayBuffer, (0, font.Height * 9), "See 'aboot.txt'", Hue.White)
        End With
    End Sub

    Public Overrides Sub AbandonGame()
        Game.World = Nothing
    End Sub

    Public Overrides Sub LoadGame(slot As Integer)
        Game.World = Persistence.World.Load(SlotFilename(slot))
    End Sub

    Public Overrides Sub SaveGame(slot As Integer)
        Game.World.Save(SlotFilename(slot))
    End Sub

    Public Overrides Function DoesSlotExist(slot As Integer) As Boolean
        Return File.Exists(SlotFilename(slot))
    End Function

    Private ReadOnly SlotFilename As IReadOnlyDictionary(Of Integer, String) =
        New Dictionary(Of Integer, String) From
        {
            {1, "slot1.json"},
            {2, "slot2.json"},
            {3, "slot3.json"},
            {4, "slot4.json"},
            {5, "slot5.json"}
        }
End Class
