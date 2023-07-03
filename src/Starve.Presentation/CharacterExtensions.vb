Imports System.Runtime.CompilerServices
Imports Starve.Persistence

Friend Module CharacterExtensions
    <Extension>
    Friend Function IsInCombat(character As ICharacter) As Boolean
        Return character.IsAvatar AndAlso CombatState.TargetCell?.Character IsNot Nothing
    End Function
End Module
