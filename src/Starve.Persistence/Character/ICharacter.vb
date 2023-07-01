Public Interface ICharacter
    ReadOnly Property Id As Integer
    ReadOnly Property CharacterType As String
    Property Cell As ICell
    Property TargetCell As ICell
    ReadOnly Property Map As IMap
    Property Statistic(statisticType As String) As Integer
End Interface
