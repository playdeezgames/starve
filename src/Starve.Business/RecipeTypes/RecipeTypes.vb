Friend Module RecipeTypes
    Friend ReadOnly Descriptors As IReadOnlyList(Of RecipeDescriptor) =
        New List(Of RecipeDescriptor) From
        {
            New RecipeDescriptor(
                New Dictionary(Of String, Integer) From
                {
                    {ItemTypes.Rock, 2}
                },
                New Dictionary(Of String, Integer) From
                {
                    {ItemTypes.Rock, 1},
                    {ItemTypes.SharpRock, 1}
                })
        }
End Module
