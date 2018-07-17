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
        SETTINGS = 3
        SHOW_INSTRUCTIONS = 4
    End Enum

    Private Shared instance As GameScreen
    Private tileMapHandler As TileMapHandler
    Private player As Player
    Private nextFrameRemovalList As New List(Of GameObject)
    Private gameOverOverlay As New GameOverOverlay() 'Screen overlay showing game over message
    Private pauseScreen As New PauseScreenOverlay()
    Private settingsOverlay As New SettingsScreenOverlay()
    Private tutorialOverlay As New TutorialScreenOverlay()
    Private scoreLabel As TextLabel
    Private scoreLabelBackground As ShapeTexture
    Private tutorialEnemy As Enemy
    Public isTutorial As Boolean
    Private _CurrentStae As State = State.PLAY
    Public Property CurrentState() As State
        Get
            Return _CurrentStae
        End Get
        Set(ByVal value As State)
            _CurrentStae = value
            If value = State.SETTINGS Then
                settingsOverlay.load()
            End If
        End Set
    End Property

    Private gameObjects As New List(Of GameObject)

    Private Sub New()
        InputHandler.keyListeners.Add(Me)
        tileMapHandler = TileMapHandler.getInstance()
        player = New Player(New Vector2(0, 0),
                            New TextureAtlas("./res/sprites/player_atlas.png", New Vector2(32, 32)))
        player.scale = New Vector2(Constants.DESIGN_SCALE_FACTOR, Constants.DESIGN_SCALE_FACTOR)
        EnemyFactory.init()
        gameObjects.Add(player)
        PhysicsHandler.addPhysicsBody(New RigidBody(player,
            Constants.Physics_CATEGORY.NO_COLLISION, Constants.Physics_COLLISION.PLAYER))

        scoreLabel = New TextLabel("Score: 0", 32 * Constants.DESIGN_SCALE_FACTOR, Brushes.White)
        scoreLabel.pos = Constants.TOP_LEFT_COORD
        scoreLabelBackground = New ShapeTexture(scoreLabel.getWidth() * 1.3, scoreLabel.getHeight(),
            Drawing.Color.FromArgb(127, 0, 0, 0), ShapeTexture.ShapeType.Rectangle)
        settingsOverlay.setOnBackAction(AddressOf onSettingsBack)
    End Sub

    Public Sub onSettingsBack()
        Debug.WriteLine("Game screen settings back")
        CurrentState = State.PAUSE
    End Sub

    Public Sub updateScoreLabel()
        scoreLabel.Text = String.Format("Score: {0}", player.Score)
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
        player.Score = 0
        updateScoreLabel()
        EnemyFactory.reset()
        getCurrentMap().spawnRandomChest()
        CurrentState = State.PLAY
    End Sub

    Public Sub gameOver()
        CurrentState = State.GAMEOVER
        'Sets new highscore if it is achieved
        If player.Score > getCurrentMap.Highscore Then
            getCurrentMap().Highscore = player.Score
        End If
    End Sub

    Public Shared Function getInstance() As GameScreen
        If instance Is Nothing Then
            instance = New GameScreen()
        End If
        Return instance
    End Function

    Public Function getCurrentMap() As Map
        Return tileMapHandler.currentMap
    End Function

    Public Overrides Sub render(delta As Double)
        tileMapHandler.render(delta)
        For i = 0 To gameObjects.Count - 1
            gameObjects(i).render(delta)
        Next

        SpriteBatch.drawTexture(scoreLabelBackground, Constants.TOP_LEFT_COORD)
        scoreLabel.render(delta)

        If CurrentState = State.GAMEOVER Then
            gameOverOverlay.render(delta)
        ElseIf CurrentState = State.PAUSE Then
            pauseScreen.render(delta)
        ElseIf CurrentState = State.SETTINGS Then
            settingsOverlay.render(delta)
        ElseIf CurrentState = State.SHOW_INSTRUCTIONS Then
            tutorialOverlay.render(delta)
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
            If Not isTutorial Then
                EnemyFactory.tick(delta)
            End If
            PhysicsHandler.update(delta)
        End If

        gameOverOverlay.tick(delta)
        pauseScreen.tick(delta)
        settingsOverlay.tick(delta)
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
            restart()
        ElseIf e.Key = Key.Escape Then
            'Toggle Pause Screen
            If CurrentState = State.PLAY Then
                CurrentState = State.PAUSE
            ElseIf CurrentState <> State.SHOW_INSTRUCTIONS Then
                CurrentState = State.PLAY
            End If
        End If
    End Sub

    ''' <summary>
    ''' Function called when this screen is switched to as the current displayed screen
    ''' </summary>
    Public Overrides Sub onShow()
        settingsOverlay.setOnBackAction(AddressOf onSettingsBack)
    End Sub

    Public Function getScore() As Integer
        Return player.Score
    End Function

    Public Sub configureNormal()
        isTutorial = False
    End Sub

    Public Sub configureTutorial()
        isTutorial = True
        CurrentState = State.SHOW_INSTRUCTIONS
        getCurrentMap().getChest().setPos(New Vector2(7 * Constants.TILE_SIZE, -2 * Constants.TILE_SIZE))
        player.pos = New Vector2(player.pos.X, 2 * Constants.TILE_SIZE - 3)
        tutorialEnemy = New Enemy(New Vector2(0, -6 * Constants.TILE_SIZE - 1), New ShapeTexture(Constants.TILE_SIZE, Constants.TILE_SIZE,
            Drawing.Color.ForestGreen, ShapeTexture.ShapeType.Rectangle), 0)
        addPhysicsBasedObject(tutorialEnemy, Constants.Physics_CATEGORY.ENEMY, Constants.Physics_COLLISION.ENEMY)
    End Sub
End Class
