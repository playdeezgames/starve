Imports System.Diagnostics.CodeAnalysis
Imports System.Runtime.CompilerServices
Imports Starve.Persistence

Public Module CharacterExtensions
    <Extension>
    Public Function Glyph(character As ICharacter) As Char
        Return character.CharacterType.ToCharacterTypeDescriptor.Glyph
    End Function
    <Extension>
    Public Function Hue(character As ICharacter) As Integer
        Return character.CharacterType.ToCharacterTypeDescriptor.Hue
    End Function
    <Extension>
    Public Sub Move(character As ICharacter, deltaX As Integer, deltaY As Integer)
        Dim currentCell = character.Cell
        Dim nextCell = currentCell.Map.GetCell(currentCell.Column + deltaX, currentCell.Row + deltaY)
        If nextCell Is Nothing OrElse Not nextCell.IsTenable OrElse nextCell.HasCharacter Then
            Return
        End If
        character.ApplyHunger()
        character.SetMovesMade(character.MovesMade + 1)
        character.Cell = nextCell
        currentCell.Character = Nothing
        nextCell.Character = character
    End Sub
    <Extension>
    Public Function Health(character As ICharacter) As Integer
        Return character.Statistic(StatisticTypes.Health)
    End Function
    <Extension>
    Private Sub SetHealth(character As ICharacter, health As Integer)
        character.Statistic(StatisticTypes.Health) = Math.Clamp(health, 0, character.MaximumHealth)
    End Sub
    <Extension>
    Public Function MaximumHealth(character As ICharacter) As Integer
        Return character.Statistic(StatisticTypes.MaximumHealth)
    End Function
    <Extension>
    Public Function Satiety(character As ICharacter) As Integer
        Return character.Statistic(StatisticTypes.Satiety)
    End Function
    <Extension>
    Private Sub SetSatiety(character As ICharacter, satiety As Integer)
        character.Statistic(StatisticTypes.Satiety) = Math.Clamp(satiety, 0, character.MaximumSatiety)
    End Sub
    <Extension>
    Public Function MaximumSatiety(character As ICharacter) As Integer
        Return character.Statistic(StatisticTypes.MaximumSatiety)
    End Function
    <Extension>
    Private Function HungerRate(character As ICharacter) As Integer
        Return character.Statistic(StatisticTypes.HungerRate)
    End Function
    <Extension>
    Private Sub ApplyHunger(character As ICharacter)
        Dim hungerRate = character.HungerRate
        Dim satiety = character.Satiety
        character.SetSatiety(character.Satiety - hungerRate)
        hungerRate = Math.Max(0, hungerRate - satiety)
        character.SetHealth(character.Health - hungerRate)
    End Sub
    <Extension>
    Public Function IsDead(character As ICharacter) As Boolean
        Return character.Health = 0
    End Function
    <Extension>
    Public Function MovesMade(character As ICharacter) As Integer
        Return character.Statistic(StatisticTypes.MovesMade)
    End Function
    <Extension>
    Private Sub SetMovesMade(character As ICharacter, movesMade As Integer)
        character.Statistic(StatisticTypes.MovesMade) = movesMade
    End Sub
End Module
