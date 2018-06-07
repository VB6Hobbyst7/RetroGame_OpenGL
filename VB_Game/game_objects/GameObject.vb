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
        Return texture.width
    End Function

    Public Overridable Function getHeight() As Integer
        Return texture.width
    End Function

    Public Function getBoundingRect() As BoundingRect
        Return New BoundingRect(New Vector2(getWidth(), getHeight()), pos)
    End Function

    ''' <summary>
    ''' Scale propery defaulting to (1, 1)
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

    Public Sub New(gravity As Boolean)
        Me.affectedByGravity = gravity
    End Sub

    ''' <summary>
    ''' Should obj be affected by gravitational force
    ''' </summary>
    Private _affectedByGravity As Boolean
    Public Property affectedByGravity() As Boolean
        Get
            Return _affectedByGravity
        End Get
        Set(ByVal value As Boolean)
            _affectedByGravity = value
        End Set
    End Property

    Public Overridable Sub render(delta As Double)
        SpriteBatch.drawObject(Me)
    End Sub

    ''' <summary>
    ''' Override this method to subscribe to collision events
    ''' </summary>
    ''' <param name="objB">Other object colliding with</param>
    Public Overridable Sub onCollide(objB As GameObject)

    End Sub

    Public Overridable Sub tick(delta As Double)
    End Sub

    ''' <summary>
    ''' Removes GameObject from all relevant containers
    ''' </summary>
    Public Overridable Sub dispose()
        GameScreen.getInstance().removeGameObject(Me)
        PhysicsHandler.scheduleDispose(Me)
    End Sub

End Class
