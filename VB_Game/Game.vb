Imports System.Drawing
Imports System.IO
Imports OpenTK
Imports OpenTK.Audio
Imports OpenTK.Graphics
Imports OpenTK.Graphics.OpenGL
Imports OpenTK.Input
Imports VB_Game

Public Class Game : Inherits GameWindow : Implements KeyListener

    Private Shared instance As Game
    Private texture As ImageTexture
    Private camera As Camera
    Private audioMaster As AudioMaster
    Private testEntity As Entity
    Private testEntity2 As Entity

    Public Function getCamera() As Camera
        Return camera
    End Function

    Private frame As Integer = 0
    Private totalFPS As Decimal = 0
    Dim xPos = 0
    Dim yPos = 0

    Private Sub New()
        MyBase.New(Constants.INIT_SCREEN_WIDTH, Constants.INIT_SCREEN_HEIGHT, New GraphicsMode(32, 0, 0, 4))

        'Initialise OpenGL Settings
        GL.Enable(EnableCap.Texture2D)
        GL.Enable(EnableCap.Blend)
        GL.BlendFunc(CType(BlendingFactorSrc.SrcAlpha, BlendingFactor), CType(BlendingFactorSrc.OneMinusSrcAlpha, BlendingFactor))
        audioMaster = AudioMaster.getInstance()
        InputHandler.init(Me)
        InputHandler.keyListeners.Add(Me)
        camera = New Camera(New Vector2(0.5, 0.5), 0, 0.2)
        testEntity = New Entity(New Vector2(0, 0), New ShapeTexture(32, 32, Color.Black, ShapeTexture.ShapeType.Rectangle))
        testEntity2 = New Entity(New Vector2(48, 48), New ShapeTexture(32, 32, Color.Black, ShapeTexture.ShapeType.Rectangle))
    End Sub

    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
        texture = ContentPipe.loadTexture("grass_tile_1.png")
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
    End Sub

    Protected Overrides Sub OnUpdateFrame(ByVal e As FrameEventArgs)
        camera.update()
        testEntity.tick(0)
    End Sub

    Protected Overrides Sub OnRenderFrame(ByVal e As FrameEventArgs)
        frame += 1
        totalFPS += Me.RenderFrequency

        If frame = 60 Then
            Debug.WriteLine(String.Format("FPS: {0}", Math.Round(totalFPS / frame)))
            frame = 0
            totalFPS = 0
        End If

        GL.Clear(ClearBufferMask.ColorBufferBit)
        GL.ClearColor(Color.Black)
        'Setup drawing
        SpriteBatch.begin(Me.ClientSize.Width, Me.ClientSize.Height)
        camera.applyTransform()

        SpriteBatch.drawImage(texture, New Vector2(-1024, -1024), New Vector2(1, 1), Color.White, New Vector2(0, 0))

        'Performance testing on a large draw
        For i = 0 To 10
            For n = 0 To 10
                SpriteBatch.drawImage(texture, New Vector2(i * 512, n * 512), New Vector2(1, 1), Color.White, New Vector2(0, 0))
            Next
        Next
        testEntity.render()
        testEntity2.render()
        Me.SwapBuffers()
    End Sub

    Public Overloads Sub Dispose()
        MyBase.Dispose()
        audioMaster.Dispose()
    End Sub

    Public Shared Function getInstance() As Game
        If instance Is Nothing Then
            instance = New Game()
        End If
        Return instance
    End Function

    Private Sub KeyListener_KeyUp(e As KeyboardKeyEventArgs) Implements KeyListener.KeyUp

    End Sub

    Private Sub KeyListener_KeyDown(e As KeyboardKeyEventArgs) Implements KeyListener.KeyDown
        If e.Key = Key.Q Then
            Me.Close()
        End If

        If e.Key = Key.F Then
            If WindowState = OpenTK.WindowState.Fullscreen Then
                WindowState = OpenTK.WindowState.Normal
            Else
                WindowState = OpenTK.WindowState.Fullscreen
            End If
        End If
    End Sub
End Class
