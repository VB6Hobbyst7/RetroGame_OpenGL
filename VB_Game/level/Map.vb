''' <summary>
''' Represents a tile map consisting of a grid of tiles
''' </summary>
Public Class Map

    Public tiles(Constants.MAP_WIDTH, Constants.MAP_HEIGHT) As Tile

    Public Sub New()

    End Sub

    Public Sub loadMap()

    End Sub

    Public Sub render()
        For x = 0 To Constants.MAP_WIDTH - 1
            For y = 0 To Constants.MAP_HEIGHT - 1
                If Not tiles(x, y) Is Nothing Then
                    tiles(x, y).render()
                End If
            Next
        Next
    End Sub

End Class
