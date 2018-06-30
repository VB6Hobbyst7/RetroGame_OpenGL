Public Class PauseScreenOverlay

    Private overlayBackground As ShapeTexture
    Private pos As OpenTK.Vector2
    Private titleLabel As TextLabel
    Private Const paddingY As Integer = 32

    Private resumeBtn As Button
    Private settingsBtn As Button
    Private quitBtn As Button
    Private btnStyle As New ButtonStyle(Drawing.Brushes.White, Drawing.Color.FromArgb(255, 64, 64, 64),
                                                Drawing.Color.Black, 5)

    Public Sub New()
        pos = New OpenTK.Vector2(-Constants.DESIGN_WIDTH / 2, -Constants.DESIGN_HEIGHT / 2)
        overlayBackground = New ShapeTexture(Constants.DESIGN_WIDTH, Constants.DESIGN_HEIGHT,
            Drawing.Color.FromArgb(200, 0, 0, 0), ShapeTexture.ShapeType.Rectangle)

        titleLabel = New TextLabel("Game Paused", New Drawing.Font("Impact", 40, Drawing.FontStyle.Regular),
                                  Drawing.Brushes.White)
        titleLabel.pos = New OpenTK.Vector2(-titleLabel.getWidth() / 2, pos.Y + paddingY)

        Dim btnSize As New Drawing.Size(200, 50)

        resumeBtn = New Button("Resume", New OpenTK.Vector2(-btnSize.Width / 2,
                titleLabel.pos.Y + titleLabel.getHeight() + paddingY * 2), btnSize, btnStyle)


        settingsBtn = New Button("Settings", New OpenTK.Vector2(-btnSize.Width / 2,
                resumeBtn.pos.Y + btnSize.Height + paddingY * 2), btnSize, btnStyle)

        quitBtn = New Button("Quit", New OpenTK.Vector2(-btnSize.Width / 2,
                settingsBtn.pos.Y + btnSize.Height + paddingY * 2), btnSize, btnStyle)

        resumeBtn.setOnClickListener(AddressOf onResumeClicked)
        settingsBtn.setOnClickListener(AddressOf onSettingsClicked)
        quitBtn.setOnClickListener(AddressOf onQuitClicked)

    End Sub

    Private Sub onSettingsClicked()
        GameScreen.getInstance().CurrentState = GameScreen.State.SETTINGS
    End Sub

    Private Sub onQuitClicked()

    End Sub

    Private Sub onResumeClicked()
        GameScreen.getInstance().CurrentState = GameScreen.State.PLAY
        Debug.WriteLine(GameScreen.getInstance().CurrentState)
    End Sub

    Public Sub render(delta)
        SpriteBatch.drawTexture(overlayBackground, pos)
        titleLabel.render(delta)
        resumeBtn.render(delta)
        settingsBtn.render(delta)
        quitBtn.render(delta)
    End Sub

End Class
