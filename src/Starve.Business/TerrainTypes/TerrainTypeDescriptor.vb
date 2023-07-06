Imports Starve.Persistence

Public Class TerrainTypeDescriptor
    Inherits VisibleEntityDescriptor
    Friend ReadOnly Property Tenable As Boolean
    Public ReadOnly Property CanInteract As Boolean
        Get
            Return VerbTypes.Any
        End Get
    End Property
    Friend ReadOnly Property VerbTypes As IReadOnlyDictionary(Of String, Action(Of ICharacter, ICell))
    Public ReadOnly Property AllVerbs As IEnumerable(Of String)
        Get
            Return VerbTypes.Keys
        End Get
    End Property
    Friend ReadOnly Property CellInitializer As Action(Of ICell)
    Sub New(
           name As String,
           glyph As Char,
           hue As Integer,
           Optional tenable As Boolean = True,
           Optional verbTypes As IReadOnlyDictionary(Of String, Action(Of ICharacter, ICell)) = Nothing,
           Optional cellInitializer As Action(Of ICell) = Nothing)
        MyBase.New(name, glyph, hue)
        Me.Tenable = tenable
        Me.VerbTypes = If(verbTypes, New Dictionary(Of String, Action(Of ICharacter, ICell)))
        Me.CellInitializer = If(cellInitializer, AddressOf DoNothing)
    End Sub

    Private Sub DoNothing(cell As ICell)
        'as ordered!
    End Sub
End Class
