Public Interface ICharacter
    ReadOnly Property Id As Integer
    ReadOnly Property CharacterType As String
    Property Cell As ICell
    ReadOnly Property Map As IMap
End Interface
