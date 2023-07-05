Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports Starve.Persistence

Friend Module ItemTypes
    Friend Const SnekCorpse = "SnekCorpse"
    Friend Const Stick = "Stick"
    Friend Const Rock = "Rock"
    Friend Const SharpRock = "SharpRock"
    Friend Const Skin = "Skin"
    Friend Const Meat = "Meat"
    Private ReadOnly descriptors As IReadOnlyDictionary(Of String, ItemTypeDescriptor) =
        New Dictionary(Of String, ItemTypeDescriptor) From
        {
            {
                SnekCorpse,
                New ItemTypeDescriptor(
                    "Snek Corpse",
                    "2"c,
                    Hue.Tan,
                    verbTypes:=New Dictionary(Of String, Action(Of ICharacter, IItem)) From
                    {
                        {VerbTypes.Eat, AddressOf EatSnekCorpse}
                    })
            },
            {
                Skin,
                New ItemTypeDescriptor(
                    "Skin",
                    ChrW(&H95),
                    Hue.Tan)
            },
            {
                Meat,
                New ItemTypeDescriptor(
                    "Meat",
                    ChrW(&H9A),
                    Hue.Red)
            },
            {
                Stick,
                New ItemTypeDescriptor(
                    "Stick",
                    "T"c,
                    Hue.Brown,
                    equipSlotType:=EquipSlotTypes.Weapon,
                    statistics:=New Dictionary(Of String, Integer) From
                    {
                        {StatisticTypes.Durability, 50},
                        {StatisticTypes.MaximumDurability, 50},
                        {StatisticTypes.MinimumAttack, 5},
                        {StatisticTypes.MaximumAttack, 5}
                    })
            },
            {
                Rock,
                New ItemTypeDescriptor(
                    "Rock",
                    ChrW(&HAC),
                    Hue.DarkGray,
                    equipSlotType:=EquipSlotTypes.Weapon,
                    statistics:=New Dictionary(Of String, Integer) From
                    {
                        {StatisticTypes.Durability, 100},
                        {StatisticTypes.MaximumDurability, 100},
                        {StatisticTypes.MinimumAttack, 5},
                        {StatisticTypes.MaximumAttack, 5}
                    })
            },
            {
                SharpRock,
                New ItemTypeDescriptor(
                    "Sharp Rock",
                    ChrW(&HAD),
                    Hue.DarkGray,
                    equipSlotType:=EquipSlotTypes.Weapon,
                    statistics:=New Dictionary(Of String, Integer) From
                    {
                        {StatisticTypes.Durability, 75},
                        {StatisticTypes.MaximumDurability, 75},
                        {StatisticTypes.MinimumAttack, 10},
                        {StatisticTypes.MaximumAttack, 10}
                    })
            }
        }

    Private Sub EatSnekCorpse(character As ICharacter, item As IItem)
        character.RemoveItem(item)
        item.Recycle()
        character.SetSatiety(character.Satiety + 20) 'TODO: pull number from statistic?
        Dim message = character.World.CreateMessage
        message.AddLine(LightGray, "You eat the snek corpse!")
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
