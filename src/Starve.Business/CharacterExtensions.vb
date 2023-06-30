Imports System.Runtime.CompilerServices
Imports Starve.Persistence

Public Module CharacterExtensions
    <Extension>
    Public Function GetGlyph(character As ICharacter) As Char
        Return character.CharacterType.ToCharacterTypeDescriptor.Glyph
    End Function
    <Extension>
    Public Function GetHue(character As ICharacter) As Integer
        Return character.CharacterType.ToCharacterTypeDescriptor.Hue
    End Function
End Module
