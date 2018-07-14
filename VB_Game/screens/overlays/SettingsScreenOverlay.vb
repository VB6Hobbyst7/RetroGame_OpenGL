Public Class SettingsScreenOverlay

    Private overlayBackground As ShapeTexture
    Private pos As OpenTK.Vector2
    Private titleLabel As TextLabel
    Private Const paddingY As Integer = 32

    Private backBtn As Button
    Private btnStyle As New ButtonStyle(Drawing.Brushes.White, Drawing.Color.FromArgb(255, 64, 64, 64),
                                                Drawing.Color.Black, 5)

    Public Sub New()
        pos = New OpenTK.Vector2(-Constants.DESIGN_WIDTH / 2, -Constants.DESIGN_HEIGHT / 2)
        overlayBackground = New ShapeTexture(Constants.DESIGN_WIDTH, Constants.DESIGN_HEIGHT,
            Drawing.Color.FromArgb(200, 0, 0, 0), ShapeTexture.ShapeType.Rectangle)

        titleLabel = New TextLabel("Settings", New Drawing.Font("Impact", 40, Drawing.FontStyle.Regular),
                                  Drawing.Brushes.White)
        titleLabel.pos = New OpenTK.Vector2(-titleLabel.getWidth() / 2, pos.Y + paddingY)

        Dim btnSize As New Drawing.Size(200, 50)

        backBtn = New Button("Back", New OpenTK.Vector2(-btnSize.Width / 2,
                titleLabel.pos.Y + titleLabel.getHeight() + paddingY * 2), btnSize, btnStyle)
        backBtn.setOnClickListener(AddressOf onBackClicked)

    End Sub

    Private Sub onBackClicked()
        GameScreen.getInstance().CurrentState = GameScreen.State.PAUSE
    End Sub

    Public Sub render(delta)
        SpriteBatch.drawTexture(overlayBackground, pos)
        titleLabel.render(delta)
        backBtn.render(delta)
    End Sub

    Public Sub tick(delta)
        backBtn.tick(delta)
    End Sub

End Class
