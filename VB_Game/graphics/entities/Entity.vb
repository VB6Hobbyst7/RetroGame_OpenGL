Imports OpenTK

''' <summary>
''' Represents an on screen game object
''' </summary>
Public Class Entity : Inherits GameObject

#Region "Member Variables"

    Protected isGrounded As Boolean = False

    Private _velocity As Vector2
    Public Property velocity() As Vector2
        Get
            Return _velocity
        End Get
        Set(ByVal value As Vector2)
            _velocity = value
        End Set
    End Property

#End Region

    Public Sub New(pos As Vector2, texture As Texture)
        MyBase.New(False)
        Me.pos = pos
        Me.texture = texture
        Me.velocity = New Vector2(0, 0)
    End Sub

    Public Sub New(pos As Vector2, textureAtlas As TextureAtlas)
        MyBase.New(False)
        Me.pos = pos
        Me.textureAtlas = textureAtlas
        Me.texture = textureAtlas.getTextures()(0)
        Me.velocity = New Vector2(0, 0)
    End Sub

    Public Overrides Sub tick(delta As Double)
        pos = New Vector2(pos.X + velocity.X * delta, pos.Y + velocity.Y * delta)
    End Sub

    'Overriding base method called by Physics handler
    Public Overrides Sub onCollide(objB As GameObject)
        'Distances needed to stop colliding (smallest gives which side is most likely colliding, not full proof solution)
        'By determining the smallest you can determine collision side to prevent movement in that direction and correct position
        'So no collision occurs
        Dim deltaL = Math.Abs(pos.X + getWidth() - objB.pos.X)
        Dim deltaR = Math.Abs(pos.X - (objB.pos.X + objB.getWidth()))
        Dim deltaD = Math.Abs(pos.Y + getHeight() - objB.pos.Y)
        Dim deltaU = Math.Abs(pos.Y - (objB.pos.Y + objB.getHeight()))

        Dim smallest = Math.Min(deltaL, deltaR)
        smallest = Math.Min(smallest, deltaD)
        smallest = Math.Min(smallest, deltaU)

        If deltaL = smallest Then
            pos = New Vector2(objB.pos.X - getWidth(), pos.Y)
            velocity = New Vector2(0, velocity.Y)
        End If

        If deltaR = smallest Then
            pos = New Vector2(objB.pos.X + objB.getHeight(), pos.Y)
            velocity = New Vector2(0, velocity.Y)
        End If

        If deltaD = smallest Then
            velocity = New Vector2(velocity.X, 0)
            pos = New Vector2(pos.X, objB.pos.Y - getHeight())
            isGrounded = True
        End If

        If deltaU = smallest Then
            velocity = New Vector2(velocity.X, 0)
            pos = New Vector2(pos.X, objB.pos.Y + objB.getWidth())
        End If
        postCollisionHandlingEvent(objB)
    End Sub

    ''' <summary>
    ''' Override method to add custom object collision event handling
    ''' </summary>
    Protected Overridable Sub postCollisionHandlingEvent(objB As GameObject)

    End Sub

End Class