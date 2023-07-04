Imports SPLORR.Game
Imports Starve.Persistence
Imports System.Runtime.CompilerServices

Public Module CharacterCombatExtensions
    <Extension>
    Public Sub Run(character As ICharacter)
        'TODO: counterattack!
    End Sub
    <Extension>
    Public Function MinimumAttack(character As ICharacter) As Integer

        Return character.Statistic(StatisticTypes.MinimumAttack) + character.Equipment.Values.Sum(Function(x) x.MinimumAttack)
    End Function
    <Extension>
    Public Function MaximumAttack(character As ICharacter) As Integer
        Return character.Statistic(StatisticTypes.MaximumAttack) + character.Equipment.Values.Sum(Function(x) x.MaximumAttack)
    End Function
    <Extension>
    Public Function MinimumDefend(character As ICharacter) As Integer
        Return character.Statistic(StatisticTypes.MinimumDefend)
    End Function
    <Extension>
    Public Function MaximumDefend(character As ICharacter) As Integer
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
    Public Sub Attack(character As ICharacter, enemy As ICharacter, Optional doCounterAttacks As Boolean = False, Optional counterIndex As Integer = 0, Optional counterMaximum As Integer = 0, Optional title As String = "")
        Dim message = character.World.CreateMessage()
        If counterIndex > 0 Then
            message.AddLine(LightGray, $"{title} {counterIndex}/{counterMaximum}")
        End If
        Dim attackRoll = character.RollAttack()
        message.AddLine(Business.Hue.LightGray, $"{character.Name} rolls an attack of {attackRoll}")
        Dim defendRoll = enemy.RollDefend()
        message.AddLine(Business.Hue.LightGray, $"{enemy.Name} rolls a defend of {defendRoll}")
        Dim damage = Math.Max(0, attackRoll - defendRoll)
        If damage > 0 Then
            enemy.SetHealth(enemy.Health - damage)
            message.AddLine(Business.Hue.LightGray, $"{enemy.Name} takes {damage} damage.")
            If enemy.IsDead Then
                message.Sfx = If(doCounterAttacks, Sfx.EnemyDeath, Sfx.PlayerDeath)
                message.AddLine(Business.Hue.LightGray, $"{character.Name} kills {enemy.Name}")
                enemy.DropItems()
                enemy.Recycle()
            Else
                message.Sfx = If(doCounterAttacks, Sfx.EnemyHit, Sfx.PlayerHit)
                message.AddLine(Business.Hue.LightGray, $"{enemy.Name} has {enemy.Health} health remaining")
            End If
        Else
            message.Sfx = Sfx.Miss
            message.AddLine(Business.Hue.LightGray, $"{character.Name} misses.")
        End If
        If doCounterAttacks Then
            character.DoCounterAttacks("Counter Attack")
        End If
    End Sub
    <Extension>
    Friend Sub DoCounterAttacks(character As ICharacter, title As String)
        Dim neighbors = character.Cell.Neighbors.Where(Function(x) If(x?.HasCharacter, False))
        Dim total = neighbors.Count
        Dim index = 1
        For Each neighbor In neighbors
            neighbor.Character.Attack(character, False, index, total, title)
            index += 1
            If character.IsDead Then
                Exit For
            End If
        Next
    End Sub

End Module
