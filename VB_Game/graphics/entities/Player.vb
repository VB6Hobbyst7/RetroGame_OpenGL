Imports OpenTK
Imports OpenTK.Input
Imports VB_Game

Public Class Player : Inherits Entity : Implements KeyListener

    Private delta As Double = 0
    Private collisionBitmask As Integer = Constants.Physics_COLLISION.PLAYER
    Private isGrounded As Boolean = False

    ''' <summary>
    ''' Used for collision detection
    ''' </summary>
    Private boundingRectTest As BoundingRect

    Public Sub New(pos As Vector2, texture As Texture)
        MyBase.New(pos, texture)
        boundingRectTest = New BoundingRect(New Vector2(getWidth(), getHeight()), pos)
        InputHandler.keyListeners.Add(Me)
    End Sub

    Public Overrides Sub tick(delta As Double)
        Me.delta = delta
        pos = New Vector2(pos.X + velocity.X * delta, pos.Y + velocity.Y * delta)
    End Sub

    'Overriding base method called by Physics handler
    Public Overrides Sub onCollide(objB As GameObject)
        'Distances needed to stop colliding (smallest gives which side is most likely colliding, not full proof solution)
        'but works for my needs if physics gets more complicated this may need to change
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
    End Sub

    Public Overrides Sub render(delta As Double)
        SpriteBatch.drawObject(Me)
    End Sub

    Public Sub KeyUp(e As KeyboardKeyEventArgs) Implements KeyListener.KeyUp
        handleInput()
    End Sub

    Public Sub KeyDown(e As KeyboardKeyEventArgs) Implements KeyListener.KeyDown
        handleInput()
    End Sub

    Private Sub handleInput()
        If InputHandler.isKeyDown(Key.W) And isGrounded Then
            Me.velocity = New Vector2(Me.velocity.X, -500)
            isGrounded = False
        End If

        'If InputHandler.isKeyDown(Key.W) And Not InputHandler.isKeyDown(Key.S) Then
        '    Me.velocity = New Vector2(Me.velocity.X, -100)
        'ElseIf InputHandler.isKeyDown(Key.S) And Not InputHandler.isKeyDown(Key.W) Then
        '    Me.velocity = New Vector2(Me.velocity.X, +100)
        'Else
        '    Me.velocity = New Vector2(Me.velocity.X, 0)
        'End If

        'Handle horizontal input movement
        If InputHandler.isKeyDown(Key.A) And Not InputHandler.isKeyDown(Key.D) Then
            Me.velocity = New Vector2(-300, Me.velocity.Y)
        ElseIf InputHandler.isKeyDown(Key.D) And Not InputHandler.isKeyDown(Key.A) Then
            Me.velocity = New Vector2(300, Me.velocity.Y)
        Else
            Me.velocity = New Vector2(0, Me.velocity.Y)
        End If
    End Sub
End Class
