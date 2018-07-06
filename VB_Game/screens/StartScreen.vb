Imports OpenTK

Public Class StartScreen : Inherits Screen

    Private Shared instance As StartScreen
    Private startGameBtn As Button
    Private backgroundImg As GameObject
    Private backgroundFilter As ShapeTexture
    Private btnFont As New Drawing.Font("Impact", 32 * Constants.DESIGN_SCALE_FACTOR, Drawing.FontStyle.Regular)
    Private titleLabel As TextLabel
    Private TOP_LEFT As New Vector2(-Constants.DESIGN_WIDTH / 2, -Constants.DESIGN_HEIGHT / 2)
    Private btnStyle = New ButtonStyle(Drawing.Brushes.White, Drawing.Color.FromArgb(255, 64, 64, 64))

    Private Sub startGame()
        Debug.WriteLine("start game")
    End Sub

    Public Sub New()
        'Background image
        backgroundImg = New GameObject(False)
        backgroundImg.texture = ContentPipe.loadTexture(Constants.TILE_RES_DIR + "startscreen_background.png")
        backgroundImg.pos = TOP_LEFT
        backgroundImg.scale = New Vector2(Constants.DESIGN_WIDTH / backgroundImg.texture.width)
        backgroundFilter = New ShapeTexture(Constants.DESIGN_WIDTH, Constants.DESIGN_HEIGHT,
                                            Drawing.Color.FromArgb(68, 0, 0, 0), ShapeTexture.ShapeType.Rectangle)

        'Title
        titleLabel = New TextLabel("GAME NAME", New Drawing.Font("Arial", 56, Drawing.FontStyle.Bold), Drawing.Brushes.Black)
        titleLabel.pos = New Vector2(-titleLabel.getWidth() / 2, -titleLabel.getHeight() * 3)
        'Buttons
        Dim btnSize = New Drawing.Size(300 * Constants.DESIGN_SCALE_FACTOR, 80 * Constants.DESIGN_SCALE_FACTOR)
        startGameBtn = New Button("PLAY", New Vector2(-btnSize.Width / 2, titleLabel.pos.Y + titleLabel.getHeight() * 1.5),
                                  btnFont, btnSize, btnStyle)
        startGameBtn.setOnClickListener(AddressOf startGame)
    End Sub

    Public Overrides Sub render(delta As Double)
        backgroundImg.render(delta)
        SpriteBatch.drawTexture(backgroundFilter, TOP_LEFT)
        startGameBtn.render(delta)
        titleLabel.render(delta)
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
