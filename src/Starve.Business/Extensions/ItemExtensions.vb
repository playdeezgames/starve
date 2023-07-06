Imports System.Runtime.CompilerServices
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
    <Extension>
    Friend Function Satiety(item As IItem) As Integer
        Return item.Statistic(StatisticTypes.Satiety)
    End Function
    <Extension>
    Public Function Durability(item As IItem) As Integer
        Return item.Statistic(StatisticTypes.Durability)
    End Function
    <Extension>
    Public Function MaximumDurability(item As IItem) As Integer
        Return item.Statistic(StatisticTypes.MaximumDurability)
    End Function
    <Extension>
    Public Function MinimumAttack(item As IItem) As Integer
        Return item.Statistic(StatisticTypes.MinimumAttack)
    End Function
    <Extension>
    Public Function MaximumAttack(item As IItem) As Integer
        Return item.Statistic(StatisticTypes.MaximumAttack)
    End Function
    <Extension>
    Friend Function IsWeapon(item As IItem) As Boolean
        Return item.Statistic(StatisticTypes.MaximumAttack) > 0
    End Function
    <Extension>
    Friend Sub Deplete(item As IItem, depletion As Integer)
        item.SetDurability(item.Durability - depletion)
    End Sub
    <Extension>
    Friend Function IsDepleted(item As IItem) As Boolean
        Return item.Durability = 0
    End Function
    <Extension>
    Private Sub SetDurability(item As IItem, durability As Integer)
        item.Statistic(StatisticTypes.Durability) = Math.Clamp(durability, 0, item.MaximumDurability)
    End Sub
End Module
