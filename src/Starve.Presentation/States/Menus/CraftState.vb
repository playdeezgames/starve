﻿Friend Class CraftState
    Inherits BasePickerState(Of IGameContext, Integer)

    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext(Of IGameContext))
        MyBase.New(parent, setState, context, "Craft What?", context.ControlsText("Craft!", "Cancel"), GameState.ActionMenu)
    End Sub

    Protected Overrides Sub OnActivateMenuItem(value As (String, Integer))
    End Sub

    Protected Overrides Function InitializeMenuItems() As List(Of (String, Integer))
        Return Game.AvailableRecipes.ToList
    End Function
End Class
