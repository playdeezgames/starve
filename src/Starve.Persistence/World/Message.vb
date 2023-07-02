Friend Class Message
    Inherits MessageDataClient
    Implements IMessage

    Public Sub New(worldData As Data.WorldData, messageId As Integer)
        MyBase.New(worldData, messageId)
    End Sub

    Public ReadOnly Property LineCount As Integer Implements IMessage.LineCount
        Get
            Return MessageData.Lines.Count
        End Get
    End Property

    Public ReadOnly Property Lines As IEnumerable(Of IMessageLine) Implements IMessage.Lines
        Get
            Return Enumerable.Range(0, LineCount).Select(Function(x) New MessageLine(WorldData, MessageId, x))
        End Get
    End Property

    Public Sub AddLine(hue As Integer, text As String) Implements IMessage.AddLine
        MessageData.Lines.Add(New Data.MessageLineData With
                              {
                                .Text = text,
                                .Hue = hue
                              })
    End Sub
End Class
