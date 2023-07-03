Imports System.Runtime.CompilerServices
Imports Starve.Persistence

Friend Module CharacterExtensions
    <Extension>
    Friend Function IsInCombat(character As ICharacter, game As IGameContext) As Boolean
        Return character.IsAvatar AndAlso game.TargetCell?.Character IsNot Nothing
    End Function
End Module
