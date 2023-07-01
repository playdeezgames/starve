Imports System.Runtime.CompilerServices

Friend Module CharacterTypes
    Friend Const Dude = "Dude"
    Friend Const Snek = "Snek"
    Private ReadOnly descriptors As IReadOnlyDictionary(Of String, CharacterTypeDescriptor) =
        New Dictionary(Of String, CharacterTypeDescriptor) From
        {
            {
                Dude,
                New CharacterTypeDescriptor(
                    "$"c,
                    Hue.Brown,
                    statistics:=New Dictionary(Of String, Integer) From
                    {
                        {StatisticTypes.Health, 100},
                        {StatisticTypes.MaximumHealth, 100},
                        {StatisticTypes.Satiety, 100},
                        {StatisticTypes.MaximumSatiety, 100},
                        {StatisticTypes.HungerRate, 1},
                        {StatisticTypes.MovesMade, 0}
                    })
            },
            {
                Snek,
                New CharacterTypeDescriptor(
                    "2"c,
                    Hue.LightGreen,
                    statistics:=New Dictionary(Of String, Integer) From
                    {
                        {StatisticTypes.Health, 10},
                        {StatisticTypes.MaximumHealth, 10},
                        {StatisticTypes.Satiety, 0},
                        {StatisticTypes.MaximumSatiety, 0},
                        {StatisticTypes.HungerRate, 0},
                        {StatisticTypes.MovesMade, 0}
                    })
            }
        }
    <Extension>
    Friend Function ToCharacterTypeDescriptor(characterType As String) As CharacterTypeDescriptor
        Return descriptors(characterType)
    End Function
    Friend ReadOnly Property All As IEnumerable(Of String)
        Get
            Return descriptors.Keys
        End Get
    End Property
End Module
