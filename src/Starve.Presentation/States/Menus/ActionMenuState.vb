﻿Friend Class ActionMenuState
    Inherits BasePickerState(Of IGameContext, String)

    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext(Of IGameContext))
        MyBase.New(
            parent,
            setState,
            context,
            "Actions Menu",
            context.ControlsText("Select", "Cancel"),
            BoilerplateState.Neutral)
    End Sub

    Protected Overrides Sub OnActivateMenuItem(value As (String, String))
        Select Case value.Item2
            Case InventoryText
                SetState(GameState.Inventory)
            Case StatusText
                SetState(GameState.Status)
            Case PickUpText
                SetState(GameState.Ground)
        End Select
    End Sub

    Protected Overrides Function InitializeMenuItems() As List(Of (String, String))
        Dim result As New List(Of (String, String))
        If Context.Game.HasGroundItems Then
            result.Add((PickUpText, PickUpText))
        End If
        If Context.Game.HasItems Then
            result.Add((InventoryText, InventoryText))
        End If
        result.Add((StatusText, StatusText))
        Return result
    End Function
End Class
