﻿Imports System.Runtime.CompilerServices
Imports Starve.Persistence

Public Module ItemExtensions
    <Extension>
    Public Function Glyph(item As IItem) As Char
        Return item.ItemType.ToItemTypeDescriptor.Glyph
    End Function
    <Extension>
    Public Function Hue(item As IItem) As Integer
        Return item.ItemType.ToItemTypeDescriptor.Hue
    End Function
    <Extension>
    Public Function Name(item As IItem) As String
        Return item.ItemType.ToItemTypeDescriptor.Name
    End Function
    <Extension>
    Public Function VerbTypes(item As IItem) As IEnumerable(Of String)
        Return item.ItemType.ToItemTypeDescriptor.AllVerbTypes
    End Function
    <Extension>
    Public Sub DoVerb(item As IItem, verbType As String, character As ICharacter)
        item.ItemType.ToItemTypeDescriptor.VerbTypes(verbType).Invoke(character, item)
    End Sub
    <Extension>
    Public Function CanEquip(item As IItem) As Boolean
        Return item.ItemType.ToItemTypeDescriptor.CanEquip
    End Function
End Module
