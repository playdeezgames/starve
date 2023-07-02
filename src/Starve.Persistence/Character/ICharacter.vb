﻿Imports System.ComponentModel

Public Interface ICharacter
    ReadOnly Property Id As Integer
    ReadOnly Property CharacterType As String
    Property Cell As ICell
    Property TargetCell As ICell
    ReadOnly Property Map As IMap
    Property Statistic(statisticType As String) As Integer
    ReadOnly Property World As IWorld
    Sub Recycle()
    ReadOnly Property Items As IEnumerable(Of IItem)
    Sub RemoveItem(item As IItem)
    Sub AddItem(item As IItem)
End Interface
