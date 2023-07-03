﻿Imports Starve.Persistence

Friend Class GameContext
    Implements IGameContext

    Public Property World As IWorld Implements IGameContext.World

    Public Property TargetCell As ICell Implements IGameContext.TargetCell

    Public Property ItemName As String Implements IGameContext.ItemName

    Public ReadOnly Property IsInCombat As Boolean Implements IGameContext.IsInCombat
        Get
            Return TargetCell?.Character IsNot Nothing
        End Get
    End Property

    Public ReadOnly Property IsInteracting As Boolean Implements IGameContext.IsInteracting
        Get
            Return If(TargetCell?.TerrainType.ToTerrainTypeDescriptor.CanInteract, False)
        End Get
    End Property

    Public ReadOnly Property GroundItems As IEnumerable(Of IItem) Implements IGameContext.GroundItems
        Get
            Return World.Avatar.Cell.Items
        End Get
    End Property

    Public ReadOnly Property HasGroundItems As Boolean Implements IGameContext.HasGroundItems
        Get
            Return World.Avatar.Cell.HasItems
        End Get
    End Property

    Public ReadOnly Property GroundItemsByName(name As String) As IEnumerable(Of IItem) Implements IGameContext.GroundItemsByName
        Get
            Return GroundItems.Where(Function(x) x.Name = name)
        End Get
    End Property

    Public ReadOnly Property GroundItemCountByName(name As String) As Integer Implements IGameContext.GroundItemCountByName
        Get
            Return GroundItems.Count(Function(x) x.Name = name)
        End Get
    End Property

    Public Sub Embark() Implements IGameContext.Embark
        World = New World(New Data.WorldData)
        WorldInitializer.Initialize(World)
    End Sub

    Public Sub Attack() Implements IGameContext.Attack
        World.Avatar.Attack(TargetCell.Character, True)
    End Sub

    Public Sub Run() Implements IGameContext.Run
        TargetCell = Nothing
        World.Avatar.Run
    End Sub

    Public Sub TakeItems(itemCount As Integer) Implements IGameContext.TakeItems
        Dim avatar = World.Avatar
        Dim cell = avatar.Cell
        For Each item In cell.Items.Where(Function(x) x.Name = ItemName).Take(itemCount)
            avatar.PickUpItem(item)
        Next
    End Sub
End Class
