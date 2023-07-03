Imports Starve.Persistence
Imports System.Runtime.CompilerServices

Public Module CharacterItemExtensions
    <Extension>
    Public Sub DropItem(character As ICharacter, item As IItem)
        character.RemoveItem(item)
        character.Cell.AddItem(item)
    End Sub
    <Extension>
    Friend Sub DropItems(character As ICharacter)
        For Each item In character.Items
            character.DropItem(item)
        Next
    End Sub
    <Extension>
    Public Sub PickUpItem(character As ICharacter, item As IItem)
        character.AddItem(item)
        character.Cell.RemoveItem(item)
    End Sub

End Module
