''' <summary>
''' A single tile in the tile map grid
''' </summary>
Public Class Tile : Inherits GameObject : Implements ICloneable

    Private name As String

    Public Function getName() As String
        Return name
    End Function

    Public Sub New(name As String)
        Debug.WriteLine("constructor call")
        Me.name = name
        Me.texture = ContentPipe.loadTexture(Constants.TILE_RES_DIR + name)
    End Sub

    'Creates an empty tile with no texture
    Public Sub New()

    End Sub

    Public Overrides Sub render()
        MyBase.render()
    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        Return Me.MemberwiseClone()
    End Function
End Class
