﻿Imports Starve.Persistence

Friend Class RecipeDescriptor
    Friend ReadOnly Property Inputs As IReadOnlyDictionary(Of String, Integer)
    Friend ReadOnly Property Outputs As IReadOnlyDictionary(Of String, Integer)
    Sub New(inputs As IReadOnlyDictionary(Of String, Integer), outputs As IReadOnlyDictionary(Of String, Integer))
        Me.Inputs = inputs
        Me.Outputs = outputs
    End Sub

    Friend Function CanCraft(character As ICharacter) As Boolean
        Dim itemStacks = character.Items.GroupBy(Function(x) x.ItemType).ToDictionary(Function(x) x.Key, Function(x) x.Count)
        For Each input In Inputs
            If Not itemStacks.ContainsKey(input.Key) OrElse itemStacks(input.Key) < input.Value Then
                Return False
            End If
        Next
        Return True
    End Function

    Friend ReadOnly Property Name As String
        Get
            Dim inputText = String.Join("+", Inputs.Select(Function(x) $"{If(x.Value > 1, $"{x.Value} ", "")}{x.Key.ToItemTypeDescriptor.Name}").ToArray)
            Dim outputText = String.Join("+", Outputs.Select(Function(x) $"{If(x.Value > 1, $"{x.Value} ", "")}{x.Key.ToItemTypeDescriptor.Name}").ToArray)
            Return $"{inputText}->{outputText}"
        End Get
    End Property
End Class
