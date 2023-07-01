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

    Public ReadOnly Property Column As Integer Implements ICell.Column
        Get
            Return CellIndex Mod Map.Columns
        End Get
    End Property

    Public ReadOnly Property Row As Integer Implements ICell.Row
        Get
            Return CellIndex \ Map.Rows
        End Get
    End Property

    Public Property TerrainType As String Implements ICell.TerrainType
        Get
            Return CellData.TerrainType
        End Get
        Set(value As String)
            CellData.TerrainType = value
        End Set
    End Property

    Public ReadOnly Property HasCharacter As Boolean Implements ICell.HasCharacter
        Get
            Return CellData.CharacterId.HasValue
        End Get
    End Property
End Class
