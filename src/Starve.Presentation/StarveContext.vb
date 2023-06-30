Imports System.IO
Imports AOS.UI
Imports Starve.Business
Imports Starve.Persistence

Public Class StarveContext
    Inherits UIContext

    Public Sub New(fontFilenames As IReadOnlyDictionary(Of String, String), viewSize As (Integer, Integer))
        MyBase.New(fontFilenames, viewSize)
    End Sub

    Public Overrides ReadOnly Property AvailableWindowSizes As IEnumerable(Of (Integer, Integer))
        Get
            Return {
                (ViewWidth * 5, ViewHeight * 5),
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
        With font
            .WriteText(displayBuffer, (0, 0), "Starve!!", Hue.White)
        End With
    End Sub

    Public Overrides Sub ShowAboutContent(displayBuffer As IPixelSink, font As Font)
        With font
            .WriteText(displayBuffer, (0, 0), "About Starve!!", Hue.White)
        End With
    End Sub

    Public Overrides Sub AbandonGame()
        World = Nothing
    End Sub

    Public Overrides Sub LoadGame(slot As Integer)
        World = Persistence.World.Load(SlotFilename(slot))
    End Sub

    Public Overrides Sub SaveGame(slot As Integer)
        World.Save(SlotFilename(slot))
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

    Friend Shared Property World As IWorld
End Class
