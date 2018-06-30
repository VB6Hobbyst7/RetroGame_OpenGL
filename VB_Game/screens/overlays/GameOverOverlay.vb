Public Class GameOverOverlay

    Private overlayBackground As ShapeTexture
    Private Const HEIGHT = 80
    Private pos As OpenTK.Vector2

    Public Sub New()
        pos = New OpenTK.Vector2(-Constants.DESIGN_WIDTH / 2, 0)
        overlayBackground = New ShapeTexture(Constants.DESIGN_WIDTH, HEIGHT,
            Drawing.Color.FromArgb(50, 255, 255, 255), ShapeTexture.ShapeType.Rectangle)
    End Sub

    Public Sub render(delta)
        SpriteBatch.drawTexture(overlayBackground, pos)
    End Sub

End Class
