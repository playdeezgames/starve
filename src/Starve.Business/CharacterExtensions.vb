Imports System.Runtime.CompilerServices
Imports Starve.Persistence

Public Module CharacterExtensions
    <Extension>
    Public Function GetGlyph(character As ICharacter) As Char
        Return character.CharacterType.ToCharacterTypeDescriptor.Glyph
    End Function
    <Extension>
    Public Function GetHue(character As ICharacter) As Integer
        Return character.CharacterType.ToCharacterTypeDescriptor.Hue
    End Function
    <Extension>
    Public Sub Move(character As ICharacter, deltaX As Integer, deltaY As Integer)
        Dim currentCell = character.Cell
        Dim nextCell = currentCell.Map.GetCell(currentCell.Column + deltaX, currentCell.Row + deltaY)
        If nextCell Is Nothing Then
            Return
        End If
        character.Cell = nextCell
        currentCell.Character = Nothing
        nextCell.Character = character
    End Sub
End Module
