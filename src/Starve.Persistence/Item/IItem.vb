Public Interface IItem
    ReadOnly Property Id As Integer
    ReadOnly Property ItemType As String
    Sub Recycle()
    Property Statistic(statisticType As String) As Integer
End Interface
