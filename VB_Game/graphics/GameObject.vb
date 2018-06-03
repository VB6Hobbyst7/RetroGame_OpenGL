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

    Public Overridable Function getWidth() As Integer
        Return texture.width
    End Function

    Public Overridable Function getHeight() As Integer
        Return texture.width
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

    ''' <summary>
    ''' Refers to whether this obj remains stationery (e.g. not effected by gravity)
    ''' </summary>
    Private _isStatic As Boolean
    Public Property isStatic() As Boolean
        Get
            Return _isStatic
        End Get
        Set(ByVal value As Boolean)
            _isStatic = value
        End Set
    End Property

    Public Overridable Sub render()
        SpriteBatch.drawObject(Me)
    End Sub

End Class
