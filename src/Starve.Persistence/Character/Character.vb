Friend Class Character
    Inherits CharacterDataClient
    Implements ICharacter

    Public Sub New(worldData As Data.WorldData, characterId As Integer)
        MyBase.New(worldData, characterId)
    End Sub

    Public ReadOnly Property Id As Integer Implements ICharacter.Id
        Get
            Return CharacterId
        End Get
    End Property

    Public ReadOnly Property CharacterType As String Implements ICharacter.CharacterType
        Get
            Return CharacterData.CharacterType
        End Get
    End Property

    Public Property Cell As ICell Implements ICharacter.Cell
        Get
            Return New Cell(WorldData, CharacterData.MapId, CharacterData.CellIndex)
        End Get
        Set(value As ICell)
            CharacterData.MapId = value.Map.Id
            CharacterData.CellIndex = value.Id
        End Set
    End Property

    Public ReadOnly Property Map As IMap Implements ICharacter.Map
        Get
            Return Cell.Map
        End Get
    End Property

    Public Property Statistic(statisticType As String) As Integer Implements ICharacter.Statistic
        Get
            Return CharacterData.Statistics(statisticType)
        End Get
        Set(value As Integer)
            CharacterData.Statistics(statisticType) = value
        End Set
    End Property
End Class
