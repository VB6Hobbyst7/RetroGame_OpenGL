Imports OpenTK

''' <summary>
''' Represents any graphical game object
''' </summary>
Public Class GameObject

    Private _pos As Vector2
    Public Property pos() As Vector2
        Get
            Return _pos
        End Get
        Set(ByVal value As Vector2)
            _pos = value
        End Set
    End Property

    Private _texture As Texture
    Public Property texture() As Texture
        Get
            Return _texture
        End Get
        Set(ByVal value As Texture)
            _texture = value
        End Set
    End Property

    Public Overridable Sub render()
        SpriteBatch.drawObject(Me)
    End Sub

End Class
