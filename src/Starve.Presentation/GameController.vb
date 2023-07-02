Imports AOS.UI

Public Class GameController
    Inherits BaseGameController

    Public Sub New(settings As ISettings, context As IUIContext)
        MyBase.New(settings, context)
        SetState(BoilerplateState.Embark, New EmbarkState(Me, AddressOf SetCurrentState, context))
        SetState(BoilerplateState.Neutral, New NeutralState(Me, AddressOf SetCurrentState, context))
        SetState(GameState.Navigation, New NavigationState(Me, AddressOf SetCurrentState, context))
        SetState(GameState.Dead, New DeadState(Me, AddressOf SetCurrentState, context))
        SetState(GameState.Combat, New CombatState(Me, AddressOf SetCurrentState, context))
        SetState(GameState.Run, New RunState(Me, AddressOf SetCurrentState, context))
        SetState(GameState.Attack, New AttackState(Me, AddressOf SetCurrentState, context))
        SetState(GameState.Message, New MessageState(Me, AddressOf SetCurrentState, context))
        SetState(GameState.ActionMenu, New ActionMenuState(Me, AddressOf SetCurrentState, context))
        SetCurrentState(BoilerplateState.Splash, True)
    End Sub
End Class
