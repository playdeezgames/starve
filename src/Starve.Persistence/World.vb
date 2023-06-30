Imports System.IO.Enumeration

Public Class World
    Implements IWorld

    Public Sub Save(filename As String) Implements IWorld.Save
        Throw New NotImplementedException()
    End Sub
    Public Shared Function Load(filename As String) As IWorld
        Throw New NotImplementedException()
    End Function
End Class
