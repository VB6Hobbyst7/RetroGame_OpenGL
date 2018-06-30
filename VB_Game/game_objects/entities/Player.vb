Imports OpenTK
Imports OpenTK.Input
Imports VB_Game

Public Class Player : Inherits Entity : Implements KeyListener

    Private currentItem As Item
    Private Const GROUNDED_VEL_THRESHOLD = 0.3
    Private Const JUMP_FORCE As Integer = 620 'Jump force given in initial velocity set
    Private spawnPos As Vector2

    ''' <summary>
    ''' Current orientation on x axis (-1 facing left, 1 facing right)
    ''' </summary>
    Private xOrientation As Integer
    Public Function getXOrient() As Integer
        Return xOrientation
    End Function

    ''' <summary>
    ''' Creates new player (only one player should be created)
    ''' </summary>
    ''' <param name="spawnPos"></param>
    ''' <param name="textureAtlas"></param>
    Public Sub New(spawnPos As Vector2, textureAtlas As TextureAtlas)
        MyBase.New(spawnPos, textureAtlas)
        Me.spawnPos = spawnPos
        xOrientation = 1
        currentItem = New Gun(Me)
        InputHandler.keyListeners.Add(Me)
    End Sub

    Public Overrides Sub tick(delta As Double)

        If InputHandler.isKeyDown(Key.W) And isGrounded And Me.velocity.Y < GROUNDED_VEL_THRESHOLD Then
            Me.velocity = New Vector2(Me.velocity.X, -JUMP_FORCE)
            isGrounded = False
        End If

        pos = New Vector2(pos.X + velocity.X * delta, pos.Y + velocity.Y * delta)
        If Not currentItem Is Nothing Then
            currentItem.update(delta)
        End If
    End Sub

    Public Overrides Sub onCollide(objB As GameObject)
        MyBase.onCollide(objB)
        If objB.GetType.IsAssignableFrom(GetType(Enemy)) Then
            'Game over - reset
            GameScreen.getInstance().restart()
            Me.pos = spawnPos
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
        'If InputHandler.isKeyDown(Key.W) And isGrounded Then
        '    Me.velocity = New Vector2(Me.velocity.X, -500)
        '    isGrounded = False
        'End If

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
            xOrientation = -1
        ElseIf InputHandler.isKeyDown(Key.D) And Not InputHandler.isKeyDown(Key.A) Then
            Me.velocity = New Vector2(300, Me.velocity.Y)
            Me.texture = textureAtlas.getTextures()(0)
            xOrientation = 1
        Else
            Me.velocity = New Vector2(0, Me.velocity.Y)
        End If

        If InputHandler.isKeyDown(Key.Space) Then
            If Not currentItem Is Nothing Then
                currentItem.useItem()
            End If
        End If
    End Sub
End Class
