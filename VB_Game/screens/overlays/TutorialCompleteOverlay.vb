Public Class TutorialCompleteOverlay

    Private overlayBackground As ShapeTexture
    Private Shared HEIGHT = Constants.DESIGN_HEIGHT / 2.5
    Private pos As OpenTK.Vector2
    Private titleLabel As TextLabel
    Private instructionText As TextLabel
    Private paddingY As Integer = 16 * Constants.DESIGN_SCALE_FACTOR
    Private lastScore As Integer = -1

    Public Sub New()
        pos = New OpenTK.Vector2(-Constants.DESIGN_WIDTH / 2, -HEIGHT / 2)
        overlayBackground = New ShapeTexture(Constants.DESIGN_WIDTH, HEIGHT,
            Drawing.Color.FromArgb(130, 0, 0, 0), ShapeTexture.ShapeType.Rectangle)

        titleLabel = New TextLabel("Tutorial Complete", New Drawing.Font("Impact", 40 * Constants.DESIGN_SCALE_FACTOR, Drawing.FontStyle.Regular),
                                   Drawing.Brushes.White)
        titleLabel.pos = New OpenTK.Vector2(-titleLabel.getWidth() / 2, pos.Y + paddingY)

        instructionText = New TextLabel("Press ENTER to return to home screen", New Drawing.Font("Impact", 20 * Constants.DESIGN_SCALE_FACTOR, Drawing.FontStyle.Regular),
                                   Drawing.Brushes.White)
        instructionText.pos = New OpenTK.Vector2(-instructionText.getWidth() / 2, titleLabel.pos.Y + titleLabel.getHeight() + paddingY * 1.5)
    End Sub

    Public Sub render(delta)
        SpriteBatch.drawTexture(overlayBackground, pos)
        titleLabel.render(delta)
        instructionText.render(delta)
    End Sub

    Public Sub tick(delta)

    End Sub

End Class
