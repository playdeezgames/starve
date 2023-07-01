Imports System.Runtime.CompilerServices
Imports AOS.UI
Imports Starve.Persistence

Friend Module ContextExtensions
    <Extension>
    Friend Function World(context As IUIContext) As IWorld
        Return StarveContext.World
    End Function
End Module
