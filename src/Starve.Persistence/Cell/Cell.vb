Imports Starve.Data

Friend Class Cell
    Inherits CellDataClient
    Implements ICell
    Public Sub New(worldData As Data.WorldData, mapId As Integer, cellIndex As Integer)
        MyBase.New(worldData, mapId, cellIndex)
    End Sub
    Public Property Character As ICharacter Implements ICell.Character
        Get
            If CellData.CharacterId.HasValue Then
                Return New Character(WorldData, CellData.CharacterId.Value)
            End If
            Return Nothing
        End Get
        Set(value As ICharacter)
            If value Is Nothing Then
                CellData.CharacterId = Nothing
                Return
            End If
            CellData.CharacterId = value.Id
        End Set
    End Property

    Public ReadOnly Property Id As Integer Implements ICell.Id
        Get
            Return CellIndex
        End Get
    End Property

    Public ReadOnly Property Map As IMap Implements ICell.Map
        Get
            Return New Map(WorldData, MapId)
        End Get
    End Property
End Class
