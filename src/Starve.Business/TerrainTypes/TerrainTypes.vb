Imports System.Runtime.CompilerServices
Imports Starve.Persistence

Public Module TerrainTypes
    Friend Const Empty = "Empty"
    Friend Const Tree = "Tree"
    Private ReadOnly descriptors As IReadOnlyDictionary(Of String, TerrainTypeDescriptor) =
        New Dictionary(Of String, TerrainTypeDescriptor) From
        {
            {Empty, New TerrainTypeDescriptor("Empty", "\"c, Hue.Green)},
            {
                Tree,
                New TerrainTypeDescriptor(
                    "Tree",
                    "k"c,
                    Hue.Green,
                    tenable:=False, verbTypes:=New Dictionary(Of String, Action(Of ICharacter, ICell)) From
                    {
                        {VerbTypes.TakeStick, AddressOf DoTakeStick}
                    })
            }
        }

    Private Sub DoTakeStick(character As ICharacter, cell As ICell)
        Dim item = ItemInitializer.CreateItem(character.World, ItemTypes.Stick)
        character.AddItem(item)
        character.World.CreateMessage().
            AddLine(LightGray, "You take a sturdy stick,").
            AddLine(LightGray, "suitable for snake clubbing!")
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
