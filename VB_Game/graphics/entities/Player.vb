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

        velocity += (PhysicUtils.metersToPixels(Constants.ACC_GRAVITY) * delta)
        pos = New Vector2(pos.X + velocity.X * delta, pos.Y + velocity.Y * delta)
        handleCollision()
    End Sub

    Private Sub handleCollision()
        Dim top = False
        Dim bottom = False
        Dim right = False
        Dim left = False
        For Each categoryBitmask In Constants.COLLISION_CATEGORIES
            If PhysicUtils.canCollide(collisionBitmask, categoryBitmask) Then
                'Bitmasks can collide so check all corresponding bodies for collisions
                For Each body In PhysicsHandler.getBodiesByCategory(categoryBitmask)
                    If PhysicUtils.doesCollide(Me, body.parent) Then

                        Dim deltaL = Math.Abs(Me.pos.X + getWidth() - body.pos.X)
                        Dim deltaR = Math.Abs(Me.pos.X - (body.pos.X + body.width))
                        Dim deltaD = Math.Abs(Me.pos.Y + getHeight() - body.pos.Y)
                        Dim deltaU = Math.Abs(Me.pos.Y - (body.pos.Y + body.height))

                        Dim smallest = Math.Min(deltaL, deltaR)
                        smallest = Math.Min(smallest, deltaD)
                        smallest = Math.Min(smallest, deltaU)

                        If deltaL = smallest Then
                            pos = New Vector2(body.pos.X - getWidth(), pos.Y)
                            velocity = New Vector2(0, velocity.Y)
                        End If

                        If deltaR = smallest Then
                            pos = New Vector2(body.pos.X + body.height, pos.Y)
                            velocity = New Vector2(0, velocity.Y)
                        End If

                        If deltaD = smallest Then
                            velocity = New Vector2(velocity.X, 0)
                            pos = New Vector2(pos.X, body.pos.Y - getHeight())
                            isGrounded = True
                        End If

                        If deltaU = smallest Then
                            velocity = New Vector2(velocity.X, 0)
                            pos = New Vector2(pos.X, body.pos.Y + body.height)
                        End If

                    End If

                Next
            End If
        Next
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
