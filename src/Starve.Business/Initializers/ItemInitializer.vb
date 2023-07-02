Imports Starve.Persistence

Friend Module ItemInitializer
    Friend Function CreateItem(world As IWorld, itemType As String) As IItem
        Dim item = world.CreateItem(itemType)
        Return item
    End Function
End Module
