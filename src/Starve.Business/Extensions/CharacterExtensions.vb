﻿Imports System.Diagnostics.CodeAnalysis
Imports System.Runtime.CompilerServices
Imports SPLORR.Game
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
        If nextCell Is Nothing OrElse Not nextCell.IsTenable Then
            Return
        End If
        If nextCell.HasCharacter Then
            character.TargetCell = nextCell
            Return
        End If
        character.ApplyHunger()
        character.SetMovesMade(character.MovesMade + 1)
        character.Cell = nextCell
        currentCell.Character = Nothing
        nextCell.Character = character
        character.TargetCell = Nothing
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
    <Extension>
    Public Function IsInCombat(character As ICharacter) As Boolean
        Return character.TargetCell.Id <> character.Cell.Id AndAlso character.TargetCell.HasCharacter
    End Function
    <Extension>
    Public Sub Run(character As ICharacter)
        character.TargetCell = Nothing
    End Sub
    <Extension>
    Public Function Name(character As ICharacter) As String
        Return character.CharacterType.ToCharacterTypeDescriptor.Name
    End Function
    <Extension>
    Private Function Enemy(character As ICharacter) As ICharacter
        If character.IsInCombat Then
            Return character.TargetCell.Character
        End If
        Return Nothing
    End Function
    <Extension>
    Private Function MinimumAttack(character As ICharacter) As Integer
        Return character.Statistic(StatisticTypes.MinimumAttack)
    End Function
    <Extension>
    Private Function MaximumAttack(character As ICharacter) As Integer
        Return character.Statistic(StatisticTypes.MaximumAttack)
    End Function
    <Extension>
    Private Function MinimumDefend(character As ICharacter) As Integer
        Return character.Statistic(StatisticTypes.MinimumDefend)
    End Function
    <Extension>
    Private Function MaximumDefend(character As ICharacter) As Integer
        Return character.Statistic(StatisticTypes.MaximumDefend)
    End Function
    <Extension>
    Private Function RollAttack(character As ICharacter) As Integer
        Return RNG.FromRange(character.MinimumAttack, character.MaximumAttack)
    End Function
    <Extension>
    Private Function RollDefend(character As ICharacter) As Integer
        Return RNG.FromRange(character.MinimumDefend, character.MaximumDefend)
    End Function
    <Extension>
    Public Sub Attack(character As ICharacter, doCounterAttacks As Boolean)
        Dim enemy = character.Enemy
        Dim message = character.World.CreateMessage()
        Dim attackRoll = character.RollAttack()
        message.AddLine(Business.Hue.LightGray, $"{character.Name} rolls an attack of {attackRoll}")
        Dim defendRoll = enemy.RollDefend()
        message.AddLine(Business.Hue.LightGray, $"{enemy.Name} rolls a defend of {defendRoll}")
        Dim damage = Math.Max(0, attackRoll - defendRoll)
        If damage > 0 Then
            enemy.SetHealth(enemy.Health - damage)
            message.AddLine(Business.Hue.LightGray, $"{enemy.Name} takes {damage} damage.")
            If enemy.IsDead Then
                message.AddLine(Business.Hue.LightGray, $"{character.Name} kills {enemy.Name}")
                enemy.DropItems()
                enemy.Recycle()
                character.TargetCell = Nothing
                doCounterAttacks = False
            Else
                message.AddLine(Business.Hue.LightGray, $"{enemy.Name} has {enemy.Health} health remaining")
            End If
        Else
            message.AddLine(Business.Hue.LightGray, $"{character.Name} misses.")
        End If
        If doCounterAttacks Then
            enemy.TargetCell = character.Cell
            enemy.Attack(False)
        End If
    End Sub
    <Extension>
    Private Sub DropItems(character As ICharacter)
        For Each item In character.Items
            character.RemoveItem(item)
            character.Cell.AddItem(item)
        Next
    End Sub
End Module
