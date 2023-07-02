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

    Public Property TargetCell As ICell Implements ICharacter.TargetCell
        Get
            Return New Cell(WorldData, CharacterData.MapId, CharacterData.TargetCellIndex)
        End Get
        Set(value As ICell)
            If value Is Nothing Then
                value = Cell
            End If
            CharacterData.MapId = value.Map.Id
            CharacterData.TargetCellIndex = value.Id
        End Set
    End Property

    Public ReadOnly Property World As IWorld Implements ICharacter.World
        Get
            Return New World(WorldData)
        End Get
    End Property

    Public ReadOnly Property Items As IEnumerable(Of IItem) Implements ICharacter.Items
        Get
            Return CharacterData.ItemIds.Select(Function(x) New Item(WorldData, x))
        End Get
    End Property

    Private ReadOnly Property IsAvatar As Boolean
        Get
            Return If(WorldData.AvatarCharacterId, -1) = Id
        End Get
    End Property

    Public Sub Recycle() Implements ICharacter.Recycle
        TargetCell = Nothing
        If Not IsAvatar Then
            Cell.Character = Nothing
            CharacterData.Recycled = True
        End If
    End Sub

    Public Sub RemoveItem(item As IItem) Implements ICharacter.RemoveItem
        CharacterData.ItemIds.Remove(item.Id)
    End Sub

    Public Sub AddItem(item As IItem) Implements ICharacter.AddItem
        CharacterData.ItemIds.Add(item.Id)
    End Sub
End Class
