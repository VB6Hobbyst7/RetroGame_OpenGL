Public Class TutorialObjectiveOverlay

    Private label As TextLabel
    Private mainFont As New Drawing.Font("Impact", 18 * Constants.DESIGN_SCALE_FACTOR, Drawing.FontStyle.Regular)
    Private msgBackgroundColor = Drawing.Color.FromArgb(80, 0, 0, 0)
    Private msgBackground As ShapeTexture
    Private msgBackgroundPos As OpenTK.Vector2
    Private msgPadding = 5 * Constants.DESIGN_SCALE_FACTOR

    Public Sub New()
        label = New TextLabel("Collect crate and defeat enemy to complete tutorial", mainFont, Drawing.Brushes.White)
        label.applyTextPadding(5 * Constants.DESIGN_SCALE_FACTOR)
        label.pos = New OpenTK.Vector2(-label.getWidth() / 2, -Constants.DESIGN_HEIGHT / 2 + label.getHeight() * 2)

        msgBackground = New ShapeTexture(label.getWidth() + msgPadding * 2, label.getHeight() + msgPadding * 2,
                                             msgBackgroundColor, ShapeTexture.ShapeType.Rectangle)
        msgBackgroundPos = New OpenTK.Vector2(label.pos.X - msgPadding, label.pos.Y - msgPadding)
    End Sub

    Public Sub render(delta)
        SpriteBatch.drawTexture(msgBackground, msgBackgroundPos)
        label.render(delta)
    End Sub

    Public Sub tick(delta)

    End Sub

End Class
