Imports OpenTK
Imports System.Drawing
Imports OpenTK.Input
Imports VB_Game

''' <summary>
''' Main screen where the game is run
''' </summary>
Public Class GameScreen : Inherits Screen : Implements MouseListener

    Private Shared instance As GameScreen
    Private tileMapHandler As TileMapHandler
    Private player As Player
    Private testEnemy As Entity
    Private testAtlas As TextureAtlas
    Private nextFrameRemovalList As New List(Of GameObject)

    Private gameObjects As New List(Of GameObject)

    Private Sub New()
        InputHandler.mouseListeners.Add(Me)
        PhysicsHandler.init()
        tileMapHandler = TileMapHandler.getInstance()
        testAtlas = New TextureAtlas("./res/sprites/player_atlas.png", New Vector2(32, 32))
        player = New Player(New Vector2(0, 0), testAtlas)
        testEnemy = New Enemy(New Vector2(200, -400), New ShapeTexture(32, 32, Color.Fuchsia, ShapeTexture.ShapeType.Rectangle))
        gameObjects.Add(player)
        gameObjects.Add(testEnemy)
        PhysicsHandler.addPhysicsBody(New RigidBody(player,
            Constants.Physics_CATEGORY.NO_COLLISION, Constants.Physics_COLLISION.PLAYER))
        PhysicsHandler.addPhysicsBody(New RigidBody(testEnemy,
            Constants.Physics_CATEGORY.ENEMY, Constants.Physics_COLLISION.ENEMY))

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

        For i = 0 To nextFrameRemovalList.Count - 1
            removeGameObject(nextFrameRemovalList(i))
        Next
    End Sub

    Public Overrides Sub update(delta As Double)
        For i = 0 To gameObjects.Count - 1
            gameObjects(i).tick(delta)
        Next
        PhysicsHandler.update(delta)
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

    Public Sub MouseScroll(e As MouseWheelEventArgs) Implements MouseListener.MouseScroll

    End Sub

    Public Sub MouseButtonDown(e As MouseEventArgs) Implements MouseListener.MouseButtonDown
    End Sub
End Class
