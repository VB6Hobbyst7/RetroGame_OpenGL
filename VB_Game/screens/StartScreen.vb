Imports OpenTK

Public Class StartScreen : Inherits Screen

    Private Shared instance As StartScreen
    Private testButton As Button
    Private backgroundImg As GameObject
    Private btnFont As New Drawing.Font("Impact", 32, Drawing.FontStyle.Regular)

    Private Sub startGame()
        Debug.WriteLine("start game")
    End Sub

    Public Sub New()

        testButton = New Button("PLAY", Vector2.Zero, btnFont, New Drawing.Size(300, 80),
                                New ButtonStyle(Drawing.Brushes.White, Drawing.Color.FromArgb(255, 64, 64, 64)))
        testButton.setOnClickListener(AddressOf startGame)
        backgroundImg = New GameObject(False)
        backgroundImg.texture = ContentPipe.loadTexture(Constants.TILE_RES_DIR + "startscreen_background.png")
        backgroundImg.pos = New Vector2(-Constants.DESIGN_WIDTH / 2, -Constants.DESIGN_HEIGHT / 2)
        backgroundImg.scale = New Vector2(Constants.DESIGN_WIDTH / backgroundImg.texture.width)
    End Sub

    Public Overrides Sub render(delta As Double)
        backgroundImg.render(delta)
        testButton.render(delta)
    End Sub

    Public Overrides Sub update(delta As Double)
    End Sub

    Public Overrides Sub dispose()
    End Sub

    Public Overrides Sub onResize()
    End Sub

    Public Shared Function getInstance() As StartScreen
        If instance Is Nothing Then
            instance = New StartScreen()
        End If
        Return instance
    End Function

End Class
