Imports System.Drawing
Imports OpenTK
Imports OpenTK.Graphics
Imports OpenTK.Graphics.OpenGL

Public Class Game : Inherits GameWindow

    Private Shared instance As Game
    Private texture As Texture2d
    Private camera As Camera

    Public Function getCamera() As Camera
        Return camera
    End Function

    Private frame As Integer = 0
    Private totalFPS As Decimal = 0
    Dim xPos = 0
    Dim yPos = 0

    Public Sub New()
        MyBase.New(800, 600, New GraphicsMode(32, 0, 0, 4))
        'Initialise OpenGL Settings
        GL.Enable(EnableCap.Texture2D)
        GL.Enable(EnableCap.Blend)
        GL.BlendFunc(CType(BlendingFactorSrc.SrcAlpha, BlendingFactor), CType(BlendingFactorSrc.OneMinusSrcAlpha, BlendingFactor))
        InputHandler.init(Me)
        camera = New Camera(New Vector2(0.5, 0.5), 0, 1)
    End Sub

    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
        GL.ClearColor(Color.MidnightBlue)
        texture = ContentPipe.loadTexture("grass_tile_1.png")
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
    End Sub

    Protected Overrides Sub OnUpdateFrame(ByVal e As FrameEventArgs)
        camera.update()
        'xPos += 1
        'yPos += 1
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
        GL.ClearColor(Color.White)

        'Setup drawing
        SpriteBatch.begin(Me.Width, Me.Height)
        camera.applyTransform()

        'Performance testing on a large draw
        For i = 0 To 10
            For n = 0 To 10
                SpriteBatch.drawImage(texture, New Vector2(i * 512, n * 512), New Vector2(1, 1), Color.White, New Vector2(0, 0))
            Next
        Next

        SpriteBatch.drawRect(New Vector2(64, 64), New Vector2(0, 0), Color.Crimson)

        Me.SwapBuffers()
    End Sub

    Public Shared Function getInstance() As Game
        If instance Is Nothing Then
            instance = New Game()
        End If
        Return instance
    End Function
End Class
