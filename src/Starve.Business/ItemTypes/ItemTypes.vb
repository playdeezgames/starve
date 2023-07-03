﻿Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports Starve.Persistence

Friend Module ItemTypes
    Friend Const SnekCorpse = "SnekCorpse"
    Private ReadOnly descriptors As IReadOnlyDictionary(Of String, ItemTypeDescriptor) =
        New Dictionary(Of String, ItemTypeDescriptor) From
        {
            {
                SnekCorpse,
                New ItemTypeDescriptor(
                    "Snek Corpse",
                    "2"c,
                    Hue.DarkGray,
                    verbs:=New Dictionary(Of String, Action(Of ICharacter, IItem)) From
                    {
                        {VerbTypes.Drop, AddressOf StandardDrop}
                    })
            }
        }

    Private Sub StandardDrop(character As ICharacter, item As IItem)
        character.RemoveItem(item)
        character.Cell.AddItem(item)
    End Sub

    <Extension>
    Friend Function ToItemTypeDescriptor(itemType As String) As ItemTypeDescriptor
        Return descriptors(itemType)
    End Function
    Friend ReadOnly Property All As IEnumerable(Of String)
        Get
            Return descriptors.Keys
        End Get
    End Property
End Module
