Friend Class RecipeDescriptor
    Friend ReadOnly Property Inputs As IReadOnlyDictionary(Of String, Integer)
    Friend ReadOnly Property Outputs As IReadOnlyDictionary(Of String, Integer)
    Sub New(inputs As IReadOnlyDictionary(Of String, Integer), outputs As IReadOnlyDictionary(Of String, Integer))
        Me.Inputs = inputs
        Me.Outputs = outputs
    End Sub
End Class
