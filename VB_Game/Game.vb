Imports System.Drawing
Imports OpenTK
Imports OpenTK.Graphics
Imports OpenTK.Graphics.OpenGL
Imports OpenTK.Input

Public Class Game : Inherits GameWindow : Implements KeyListener

    Private Shared instance As Game
    Private camera As Camera
    Private audioMaster As AudioMaster

    Private _currentScreen As Screen
    Public Property currentScreen() As Screen
        Get
            Return _currentScreen
        End Get
        Set(ByVal value As Screen)
            _currentScreen = value
        End Set
    End Property

    Public Function getCamera() As Camera
        Return camera
    End Function

    Private Sub New()
        MyBase.New(Constants.INIT_SCREEN_WIDTH, Constants.INIT_SCREEN_HEIGHT, New GraphicsMode(32, 0, 0, 4))
        currentScreen = GameScreen.getInstance()
        'Initialise OpenGL Settings
        GL.Enable(EnableCap.Texture2D)
        GL.Enable(EnableCap.Blend)
        GL.BlendFunc(CType(BlendingFactorSrc.SrcAlpha, BlendingFactor), CType(BlendingFactorSrc.OneMinusSrcAlpha, BlendingFactor))
        audioMaster = AudioMaster.getInstance()
        InputHandler.init(Me)
        InputHandler.keyListeners.Add(Me)
        camera = New Camera(New Vector2(0.5, 0.5), 0, 1)
        _currentScreen = GameScreen.getInstance()
    End Sub

    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        _currentScreen.onResize()
    End Sub

    Protected Overrides Sub OnUpdateFrame(ByVal e As FrameEventArgs)
        camera.update()
        _currentScreen.update(e.Time)
        DebugHandler.update(e.Time)
    End Sub

    Protected Overrides Sub OnRenderFrame(ByVal e As FrameEventArgs)
        GL.Clear(ClearBufferMask.ColorBufferBit)
        GL.ClearColor(Color.Black)
        SpriteBatch.begin(Me.ClientSize.Width, Me.ClientSize.Height)
        camera.applyTransform()
        _currentScreen.render(e.Time)
        DebugHandler.render(e.Time)
        Me.SwapBuffers()
    End Sub

    Public Overloads Sub Dispose()
        MyBase.Dispose()
        _currentScreen.dispose()
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
