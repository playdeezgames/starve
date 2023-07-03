Imports System.Runtime.CompilerServices

Public Module VerbTypes
    Friend Const Eat = "Eat"
    Private ReadOnly descriptors As IReadOnlyDictionary(Of String, VerbDescriptor) =
        New Dictionary(Of String, VerbDescriptor) From
        {
            {
                Eat,
                New VerbDescriptor(
                    "Eat")
            }
        }
    <Extension>
    Public Function ToVerbTypeDescriptor(verbType As String) As VerbDescriptor
        Return descriptors(verbType)
    End Function
    Friend ReadOnly Property All As IEnumerable(Of String)
        Get
            Return descriptors.Keys
        End Get
    End Property
End Module
