Imports System.Diagnostics.CodeAnalysis
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
    Public Function Move(character As ICharacter, deltaX As Integer, deltaY As Integer) As ICell
        Dim currentCell = character.Cell
        Dim nextCell = currentCell.Map.GetCell(currentCell.Column + deltaX, currentCell.Row + deltaY)
        If nextCell Is Nothing OrElse Not nextCell.IsTenable Then
            Return Nothing
        End If
        If nextCell.HasCharacter Then
            Return nextCell
        End If
        character.DoCounterAttacks("Opportunity Attack")
        character.ApplyHunger()
        character.SetMovesMade(character.MovesMade + 1)
        character.Cell = nextCell
        currentCell.Character = Nothing
        nextCell.Character = character
        Return Nothing
    End Function
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
    Public Sub Run(character As ICharacter)
        'TODO: counterattack!
    End Sub
    <Extension>
    Public Function Name(character As ICharacter) As String
        Return character.CharacterType.ToCharacterTypeDescriptor.Name
    End Function
    <Extension>
    Public Function MinimumAttack(character As ICharacter) As Integer
        Return character.Statistic(StatisticTypes.MinimumAttack)
    End Function
    <Extension>
    Public Function MaximumAttack(character As ICharacter) As Integer
        Return character.Statistic(StatisticTypes.MaximumAttack)
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
    Private Sub DoCounterAttacks(character As ICharacter, title As String)
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
    <Extension>
    Public Sub DropItem(character As ICharacter, item As IItem)
        character.RemoveItem(item)
        character.Cell.AddItem(item)
    End Sub
    <Extension>
    Private Sub DropItems(character As ICharacter)
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
