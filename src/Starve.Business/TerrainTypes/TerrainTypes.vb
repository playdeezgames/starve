Imports System.Runtime.CompilerServices
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
                    })
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

    Private Sub ForageGrass(character As ICharacter, cell As ICell)
        Dim item = ItemInitializer.CreateItem(character.World, ItemTypes.Fiber)
        character.AddItem(item)
        character.World.CreateMessage().AddLine(LightGray, $"{character.Name} finds {item.Name}")
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
