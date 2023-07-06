Imports System.Runtime.CompilerServices
Imports SPLORR.Game
Imports Starve.Persistence

Public Module TerrainTypes
    Friend Const Grass = "Grass"
    Friend Const Tree = "Tree"
    Private ReadOnly descriptors As IReadOnlyDictionary(Of String, TerrainTypeDescriptor) =
        New Dictionary(Of String, TerrainTypeDescriptor) From
        {
            {
                Grass,
                New TerrainTypeDescriptor(
                    "Grass",
                    "\"c,
                    Hue.Green,
                    tenable:=True,
                    verbTypes:=New Dictionary(Of String, Action(Of ICharacter, ICell)) From
                    {
                        {VerbTypes.Forage, AddressOf ForageGrass}
                    },
                    cellInitializer:=AddressOf InitializeGrass)
            },
            {
                Tree,
                New TerrainTypeDescriptor(
                    "Tree",
                    "k"c,
                    Hue.Green,
                    tenable:=False,
                    verbTypes:=New Dictionary(Of String, Action(Of ICharacter, ICell)) From
                    {
                        {VerbTypes.TakeStick, AddressOf DoTakeStick}
                    })
            }
        }

    Private Sub InitializeGrass(cell As ICell)
        cell.Statistic(StatisticTypes.Depletion) = 20
        cell.Statistic(StatisticTypes.MossWeight) = 5
        cell.Statistic(StatisticTypes.FiberWeight) = 15
    End Sub

    Private Sub ForageGrass(character As ICharacter, cell As ICell)
        Dim generated = RNG.FromGenerator(New Dictionary(Of String, Integer) From
            {
                {"", cell.Statistic(StatisticTypes.Depletion)},
                {ItemTypes.Fiber, cell.Statistic(StatisticTypes.FiberWeight)},
                {ItemTypes.Moss, cell.Statistic(StatisticTypes.MossWeight)}
            })
        Dim message = character.World.CreateMessage()
        character.ApplyHunger()
        If String.IsNullOrEmpty(generated) Then
            message.Sfx = Sfx.Shucks
            message.AddLine(Red, $"{character.Name} finds nothing!")
            Return
        End If
        Select Case generated
            Case ItemTypes.Fiber
                cell.Statistic(FiberWeight) -= 1
            Case ItemTypes.Moss
                cell.Statistic(MossWeight) -= 1
        End Select
        cell.Statistic(Depletion) += 1
        Dim item = ItemInitializer.CreateItem(character.World, generated)
        character.AddItem(item)
        message.AddLine(LightGray, $"{character.Name} finds {item.Name}")
    End Sub

    Private Sub DoTakeStick(character As ICharacter, cell As ICell)
        Dim item = ItemInitializer.CreateItem(character.World, ItemTypes.Stick)
        character.AddItem(item)
        character.World.CreateMessage().
            AddLine(LightGray, "You take a sturdy stick,").
            AddLine(LightGray, "suitable for snake clubbing!").
            AddLine(LightGray, "(you have to equip it first)")
    End Sub

    <Extension>
    Public Function ToTerrainTypeDescriptor(terrainType As String) As TerrainTypeDescriptor
        Return descriptors(terrainType)
    End Function
    Friend ReadOnly Property All As IEnumerable(Of String)
        Get
            Return descriptors.Keys
        End Get
    End Property
End Module
