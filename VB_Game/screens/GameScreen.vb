Imports OpenTK
Imports System.Drawing
Imports OpenTK.Input
Imports VB_Game

''' <summary>
''' Main screen where the game is run
''' </summary>
Public Class GameScreen : Inherits Screen : Implements KeyListener

    Public Enum State
        PLAY = 0
        PAUSE = 1
        GAMEOVER = 2
    End Enum

    Private Shared instance As GameScreen
    Private tileMapHandler As TileMapHandler
    Private player As Player
    Private testAtlas As TextureAtlas
    Private nextFrameRemovalList As New List(Of GameObject)
    Private gameOverOverlay As New GameOverOverlay 'Screen overlay showing game over message

    Public CurrentState As State = State.PLAY

    Private gameObjects As New List(Of GameObject)

    Private Sub New()
        InputHandler.keyListeners.Add(Me)
        PhysicsHandler.init()
        tileMapHandler = TileMapHandler.getInstance()
        testAtlas = New TextureAtlas("./res/sprites/player_atlas.png", New Vector2(32, 32))
        player = New Player(New Vector2(0, 0), testAtlas)
        player.scale = New Vector2(Constants.DESIGN_SCALE_FACTOR, Constants.DESIGN_SCALE_FACTOR)
        EnemyFactory.init()
        gameObjects.Add(player)
        PhysicsHandler.addPhysicsBody(New RigidBody(player,
            Constants.Physics_CATEGORY.NO_COLLISION, Constants.Physics_COLLISION.PLAYER))
    End Sub

    ''' <summary>
    ''' Restarts game resetting everything to default states
    ''' </summary>
    Public Sub restart()
        Dim i = 0
        While i < gameObjects.Count()
            If gameObjects(i).GetType.IsAssignableFrom(GetType(SimpleProjectile)) Or
                gameObjects(i).GetType.IsAssignableFrom(GetType(Enemy)) Then
                removeGameObject(gameObjects(i))
                i -= 1
            Else
                i += 1
            End If
        End While
        player.moveToSpawn()
        EnemyFactory.reset()
        CurrentState = State.PLAY
    End Sub

    Public Sub gameOver()
        CurrentState = State.GAMEOVER
    End Sub

    Public Shared Function getInstance() As GameScreen
        If instance Is Nothing Then
            instance = New GameScreen()
        End If
        Return instance
    End Function

    Public Overrides Sub render(delta As Double)
        tileMapHandler.render(delta)
        For i = 0 To gameObjects.Count - 1
            gameObjects(i).render(delta)
        Next
        If CurrentState = State.GAMEOVER Then
            gameOverOverlay.render(delta)
        End If

        For i = 0 To nextFrameRemovalList.Count - 1
            removeGameObject(nextFrameRemovalList(i))
        Next
    End Sub

    Public Overrides Sub update(delta As Double)
        If CurrentState = State.PLAY Then
            For i = 0 To gameObjects.Count - 1
                gameObjects(i).tick(delta)
            Next
            EnemyFactory.tick(delta)
            PhysicsHandler.update(delta)
        End If
    End Sub

    ''' <summary>
    ''' Adds objects to be removed next frame to improve visuals
    ''' </summary>
    ''' <param name="obj"></param>
    Public Sub removeGameObjectNextFrame(obj As GameObject)
        nextFrameRemovalList.Add(obj)
    End Sub

    Public Sub removeGameObject(obj As GameObject)
        gameObjects.Remove(obj)
        PhysicsHandler.scheduleDispose(obj)
    End Sub

    Public Sub addPhysicsBasedObject(obj As GameObject, categoryBitmask As Integer, collisionBitmask As Integer)
        gameObjects.Add(obj)
        PhysicsHandler.addPhysicsBody(New RigidBody(obj, categoryBitmask, collisionBitmask))
    End Sub

    Public Overrides Sub dispose()

    End Sub

    Public Overrides Sub onResize()

    End Sub

    Public Sub KeyUp(e As KeyboardKeyEventArgs) Implements KeyListener.KeyUp
    End Sub

    Public Sub KeyDown(e As KeyboardKeyEventArgs) Implements KeyListener.KeyDown
        If CurrentState = State.GAMEOVER And e.Key = Key.Enter Then
            'Restart game
            restart()
        End If
    End Sub
End Class
