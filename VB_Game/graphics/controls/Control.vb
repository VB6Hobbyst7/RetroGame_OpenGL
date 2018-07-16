Imports OpenTK

Public Class Control

    Protected customRender As Boolean = False
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

    Private _visible As Boolean = False
    Public Property Visible() As Boolean
        Get
            Return Game.getInstance().gameTime - lastRenderTime < RENDER_HIDE_TIME_THRESHOLD
        End Get
        Set(ByVal value As Boolean)
            _visible = value
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

    Private stopwatch As New Stopwatch()
    Private lastRenderTime As Double = 0
    Private RENDER_HIDE_TIME_THRESHOLD = 0.2 'Time needed since last render to disable controls

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
        If Not customRender Then
            SpriteBatch.drawControl(Me)
        End If
        lastRenderTime = Game.getInstance().gameTime
    End Sub

End Class
