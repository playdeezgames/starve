Imports AOS.UI

Public Class GameController
    Inherits BaseGameController

    Public Sub New(settings As ISettings, context As IUIContext)
        MyBase.New(settings, context)

        SetCurrentState(BoilerplateState.Splash, True)
    End Sub
End Class
