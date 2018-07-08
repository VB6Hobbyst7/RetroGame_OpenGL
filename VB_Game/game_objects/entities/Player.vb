Imports OpenTK
Imports OpenTK.Input
Imports VB_Game

Public Class Player : Inherits Entity : Implements KeyListener

    Private currentItem As Item
    Private Const GROUNDED_VEL_THRESHOLD = 0.3
    Private Shared JUMP_FORCE As Integer = 20 * Constants.PIXELS_IN_METER 'Jump force given in initial velocity set
    Private Shared HORIZONTAL_SPEED As Integer = 10 * Constants.PIXELS_IN_METER
    Private spawnPos As Vector2

    ''' <summary>
    ''' Number of chests collected
    ''' </summary>
    Private _score As Integer
    Public Property Score() As Integer
        Get
            Return _score
        End Get
        Set(ByVal value As Integer)
            _score = value
        End Set
    End Property

    ''' <summary>
    ''' Increments current score by 1
    ''' </summary>
    Public Sub incrementScore()
        _score += 1
    End Sub

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
        If objB.GetType.IsAssignableFrom(GetType(Chest)) Then
            GameScreen.getInstance().getCurrentMap().spawnRandomChest()
            incrementScore()
            GameScreen.getInstance().updateScoreLabel()
        Else
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
            If objB.GetType.IsAssignableFrom(GetType(Enemy)) Then
                'Game over - reset
                GameScreen.getInstance().gameOver()
            End If
        End If

    End Sub

    Public Overrides Sub render(delta As Double)
        SpriteBatch.drawObject(Me)
    End Sub

    Public Sub moveToSpawn()
        Me.pos = Me.spawnPos
    End Sub

    Public Sub KeyUp(e As KeyboardKeyEventArgs) Implements KeyListener.KeyUp
        handleInput()
    End Sub

    Public Sub KeyDown(e As KeyboardKeyEventArgs) Implements KeyListener.KeyDown
        handleInput()
    End Sub

    Private Sub handleInput()
        If GameScreen.getInstance().CurrentState = GameScreen.State.PLAY Then
            'Handle horizontal input movement
            If InputHandler.isKeyDown(Key.A) And Not InputHandler.isKeyDown(Key.D) Then
                Me.velocity = New Vector2(-HORIZONTAL_SPEED, Me.velocity.Y)
                Me.texture = textureAtlas.getTextures()(1)
                xOrientation = -1
            ElseIf InputHandler.isKeyDown(Key.D) And Not InputHandler.isKeyDown(Key.A) Then
                Me.velocity = New Vector2(HORIZONTAL_SPEED, Me.velocity.Y)
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
        End If
    End Sub
End Class
