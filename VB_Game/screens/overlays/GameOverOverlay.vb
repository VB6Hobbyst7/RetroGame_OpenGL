Public Class GameOverOverlay

    Private overlayBackground As ShapeTexture
    Private Shared HEIGHT = Constants.DESIGN_HEIGHT / 2.5
    Private pos As OpenTK.Vector2
    Private titleLabel As TextLabel
    Private scoreText As TextLabel
    Private instructionText As TextLabel
    Private Const paddingY As Integer = 16

    Public Sub New()
        pos = New OpenTK.Vector2(-Constants.DESIGN_WIDTH / 2, -HEIGHT / 2)
        overlayBackground = New ShapeTexture(Constants.DESIGN_WIDTH, HEIGHT,
            Drawing.Color.FromArgb(130, 0, 0, 0), ShapeTexture.ShapeType.Rectangle)

        titleLabel = New TextLabel("Game Over", New Drawing.Font("Impact", 40, Drawing.FontStyle.Regular),
                                   Drawing.Brushes.White)
        titleLabel.pos = New OpenTK.Vector2(-titleLabel.getWidth() / 2, pos.Y + paddingY)

        scoreText = New TextLabel("Score: {0}", New Drawing.Font("Impact", 24, Drawing.FontStyle.Regular),
                                   Drawing.Brushes.White)
        scoreText.pos = New OpenTK.Vector2(-scoreText.getWidth() / 2, titleLabel.pos.Y + titleLabel.getHeight() + paddingY)

        instructionText = New TextLabel("Press ENTER to restart", New Drawing.Font("Impact", 20, Drawing.FontStyle.Regular),
                                   Drawing.Brushes.White)
        instructionText.pos = New OpenTK.Vector2(-instructionText.getWidth() / 2, scoreText.pos.Y + scoreText.getHeight() + paddingY * 1.5)
    End Sub

    Public Sub render(delta)
        SpriteBatch.drawTexture(overlayBackground, pos)
        titleLabel.render(delta)
        scoreText.render(delta)
        instructionText.render(delta)
    End Sub

End Class
