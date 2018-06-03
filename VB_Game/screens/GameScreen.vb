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

    Private Sub New()
        InputHandler.mouseListeners.Add(Me)
        PhysicsHandler.init()
        tileMapHandler = TileMapHandler.getInstance()
        player = New Player(New Vector2(0, 0), New ShapeTexture(32, 32, Color.Black, ShapeTexture.ShapeType.Rectangle))
        PhysicsHandler.addPhysicsBody(New RigidBody(player,
            Constants.Physics_CATEGORY.PLAYER, Constants.Physics_COLLISION.NO_COLLISION)) 'No collision as player handles own collision
    End Sub

    Public Shared Function getInstance() As GameScreen
        If instance Is Nothing Then
            instance = New GameScreen()
        End If
        Return instance
    End Function

    Public Overrides Sub render(delta As Double)
        tileMapHandler.render(delta)
        player.render(delta)
    End Sub

    Public Overrides Sub update(delta As Double)
        PhysicsHandler.update(delta)
        player.tick(delta)
    End Sub

    Public Overrides Sub dispose()

    End Sub

    Public Overrides Sub onResize()

    End Sub

    Public Sub MouseScroll(e As MouseWheelEventArgs) Implements MouseListener.MouseScroll

    End Sub

    Public Sub MouseButtonDown(e As MouseEventArgs) Implements MouseListener.MouseButtonDown
        player.velocity = New Vector2(0, -500)
    End Sub
End Class
