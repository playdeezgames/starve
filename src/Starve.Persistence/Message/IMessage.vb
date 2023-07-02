Public Interface IMessage
    ReadOnly Property LineCount As Integer
    ReadOnly Property Lines As IEnumerable(Of IMessageLine)
    Sub AddLine(hue As Integer, text As String)
End Interface
