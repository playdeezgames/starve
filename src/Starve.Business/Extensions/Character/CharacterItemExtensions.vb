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
    <Extension>
    Friend Function CanCraft(character As ICharacter) As Boolean
        Return RecipeTypes.Descriptors.Any(Function(x) x.CanCraft(character))
    End Function
    <Extension>
    Friend Sub DoForaging(character As ICharacter, itemType As String)
        character.ApplyHunger()
        character.Cell.Statistic(ForageAttempts) += 1
        If ItemTypes.All.Contains(itemType) Then
            character.Cell.Statistic(ForagingWeight(itemType)) -= 1
            character.AddItem(ItemInitializer.CreateItem(character.World, itemType))
        End If
    End Sub
End Module
