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
                }),
            New RecipeDescriptor(
                New Dictionary(Of String, Integer) From
                {
                    {ItemTypes.SharpRock, 1},
                    {ItemTypes.SnekCorpse, 1}
                },
                New Dictionary(Of String, Integer) From
                {
                    {ItemTypes.SharpRock, 1},
                    {ItemTypes.Meat, 1},
                    {ItemTypes.Skin, 1}
                }),
            New RecipeDescriptor(
                New Dictionary(Of String, Integer) From
                {
                    {ItemTypes.Fiber, 2}
                },
                New Dictionary(Of String, Integer) From
                {
                    {ItemTypes.Twine, 1}
                })
        }
End Module
