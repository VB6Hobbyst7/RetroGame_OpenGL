Imports OpenTK
Imports OpenTK.Input
Imports VB_Game

Public Class Player : Inherits Entity : Implements KeyListener

    Public Sub New(pos As Vector2, textureAtlas As TextureAtlas)
        MyBase.New(pos, textureAtlas)
        InputHandler.keyListeners.Add(Me)
    End Sub

    Public Overrides Sub tick(delta As Double)
        pos = New Vector2(pos.X + velocity.X * delta, pos.Y + velocity.Y * delta)
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
            Me.texture = textureAtlas.getTextures()(1)
        ElseIf InputHandler.isKeyDown(Key.D) And Not InputHandler.isKeyDown(Key.A) Then
            Me.velocity = New Vector2(300, Me.velocity.Y)
            Me.texture = textureAtlas.getTextures()(0)
        Else
            Me.velocity = New Vector2(0, Me.velocity.Y)
        End If
    End Sub
End Class
