Imports OpenTK

Public Class Control

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

    Private _textureAtlas As TextureAtlas
    Public Property textureAtlas() As TextureAtlas
        Get
            Return _textureAtlas
        End Get
        Set(ByVal value As TextureAtlas)
            _textureAtlas = value
        End Set
    End Property

    Public Overridable Function getWidth() As Integer
        Return texture.width * scale.X
    End Function

    Public Overridable Function getHeight() As Integer
        Return texture.height * scale.X
    End Function

    Public Sub setWidth(width As Double)
        scale = New Vector2(texture.width / width, scale.Y)
    End Sub

    Public Sub setHeight(height As Double)
        scale = New Vector2(scale.X, texture.height / height)
    End Sub


    ''' <summary>
    ''' Scale property defaulting to (1, 1)
    ''' </summary>
    Private _scale As New Vector2(1, 1)
    Public Property scale() As Vector2
        Get
            Return _scale
        End Get
        Set(ByVal value As Vector2)
            _scale = value
        End Set
    End Property

    Public Overridable Sub render(delta As Double)
        SpriteBatch.drawControl(Me)
    End Sub

    Public Overridable Sub tick(delta As Double)
    End Sub

End Class
