Imports OpenTK
Imports OpenTK.Input
Imports VB_Game

Public Class Player : Inherits Entity : Implements CollisionListener, KeyListener

    Private delta As Double = 0
    Private debugText As TextLabel

    Public Sub New(pos As Vector2, texture As Texture)
        MyBase.New(pos, texture)
        PhysicsHandler.collisionListeners.Add(Me)
        InputHandler.keyListeners.Add(Me)
        debugText = New TextLabel("Testing", New Vector2(-1280 / 2, -960 / 2))
    End Sub

    Public Sub onCollide(objA As GameObject, objB As GameObject) Implements CollisionListener.onCollide
        If (objA.pos.Y + objA.getHeight()) + velocity.X * delta > objB.pos.Y Then
            debugText.Text = "Bottom"
            Debug.WriteLine("whha")
        Else
            Debug.WriteLine("whha")
            debugText.Text = ""
        End If

        If objA.pos.X > objB.pos.X Then
            Debug.WriteLine("Right")
        End If

        Me.velocity = New Vector2(Me.velocity.X, 0)
        Me.pos = New Vector2(pos.X, objB.pos.Y - getHeight())
    End Sub

    Public Overrides Sub tick(delta As Double)
        Me.delta = delta
        pos = New Vector2(pos.X + velocity.X * delta, pos.Y + velocity.Y * delta)
    End Sub

    Public Overrides Sub render(delta As Double)
        SpriteBatch.drawObject(Me)
        debugText.render(delta)
    End Sub

    Public Sub KeyUp(e As KeyboardKeyEventArgs) Implements KeyListener.KeyUp
        handleInput()
    End Sub

    Public Sub KeyDown(e As KeyboardKeyEventArgs) Implements KeyListener.KeyDown
        handleInput()
    End Sub

    Private Sub handleInput()
        If InputHandler.isKeyDown(Key.W) Then
            Me.velocity = New Vector2(Me.velocity.X, 500)
        End If

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
