Imports System.Runtime.CompilerServices

Friend Module CharacterTypes
    Friend Const Dude = "Dude"
    Private ReadOnly descriptors As IReadOnlyDictionary(Of String, CharacterTypeDescriptor) =
        New Dictionary(Of String, CharacterTypeDescriptor) From
        {
            {
                Dude,
                New CharacterTypeDescriptor(
                    "$"c,
                    Hue.Brown, statistics:=New Dictionary(Of String, Integer) From
                    {
                        {StatisticTypes.Health, 100},
                        {StatisticTypes.MaximumHealth, 100},
                        {StatisticTypes.Satiety, 100},
                        {StatisticTypes.MaximumSatiety, 100},
                        {StatisticTypes.HungerRate, 1}
                    })}
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
