''' <summary>
''' A single tile in the tile map grid
''' </summary>
Public Class Tile : Inherits GameObject : Implements ICloneable

    Private name As String

    Public Function getName() As String
        Return name
    End Function

    Public Overrides Function getWidth() As Integer
        Return Constants.TILE_SIZE
    End Function

    Public Overrides Function getHeight() As Integer
        Return Constants.TILE_SIZE
    End Function

    Public Sub New(name As String)
        Debug.WriteLine("constructor call")
        Me.name = name
        Me.texture = ContentPipe.loadTexture(Constants.TILE_RES_DIR + name)
        Me.scale = New OpenTK.Vector2(Constants.TILE_SIZE / texture.width, Constants.TILE_SIZE / texture.height)
    End Sub

    'Creates an empty tile with no texture
    Public Sub New()

    End Sub

    Public Overrides Sub render(delta As Double)
        'If tile has texture then render
        If Not texture Is Nothing Then
            MyBase.render(delta)
        End If
    End Sub

    ''' <summary>
    ''' Creates a clone of this object with same properties (avoiding reloading of textures)
    ''' </summary>
    ''' <returns></returns>
    Public Function Clone() As Object Implements ICloneable.Clone
        Return Me.MemberwiseClone()
    End Function
End Class
