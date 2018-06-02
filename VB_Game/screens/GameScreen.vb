''' <summary>
''' Main screen where the game is run
''' </summary>
Imports OpenTK
Imports System.Drawing
Imports OpenTK.Input
Imports VB_Game

Public Class GameScreen : Inherits Screen : Implements MouseListener

    Private Shared instance As GameScreen
    Private tileMapHandler As TileMapHandler
    Private testEntity As Entity

    Private Sub New()
        InputHandler.mouseListeners.Add(Me)
        tileMapHandler = TileMapHandler.getInstance()
        testEntity = New Entity(New Vector2(0, 0), New ShapeTexture(32, 32, Color.Black, ShapeTexture.ShapeType.Rectangle))
        testEntity.velocity = New Vector2(0, -68.6)
        PhysicsHandler.physicsBodies.Add(testEntity)
    End Sub

    Public Shared Function getInstance() As GameScreen
        If instance Is Nothing Then
            instance = New GameScreen()
        End If
        Return instance
    End Function

    Public Overrides Sub render(delta As Double)
        tileMapHandler.render()
        testEntity.render()
    End Sub

    Public Overrides Sub update(delta As Double)
        PhysicsHandler.update(delta)
        testEntity.tick(delta)
    End Sub

    Public Overrides Sub dispose()

    End Sub

    Public Overrides Sub onResize()

    End Sub

    Public Sub MouseScroll(e As MouseWheelEventArgs) Implements MouseListener.MouseScroll

    End Sub

    Public Sub MouseButtonDown(e As MouseEventArgs) Implements MouseListener.MouseButtonDown
        testEntity.velocity = New Vector2(0, -500)
    End Sub
End Class
