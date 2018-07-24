''' <summary>
''' Overlay showing tutorial instructions
''' </summary>
Public Class TutorialScreenOverlay

    Private enemyLabel As TextLabel
    Private mainFont As New Drawing.Font("Impact", 18 * Constants.DESIGN_SCALE_FACTOR, Drawing.FontStyle.Regular)
    Private msgBackgroundColor = Drawing.Color.FromArgb(80, 0, 0, 0)
    Private msgBackgrounds(2) As ShapeTexture
    Private msgBackgroundsPos(2) As OpenTK.Vector2
    Private msgPadding = 10

    Public Sub New()
        enemyLabel = New TextLabel("This is an enemy, \r\n avoid touching it", mainFont, Drawing.Brushes.White)
        enemyLabel.pos = New OpenTK.Vector2(0, 0)
        msgBackgrounds(0) = New ShapeTexture(enemyLabel.getWidth() + msgPadding * 2, enemyLabel.getHeight() + msgPadding * 2,
                                             msgBackgroundColor, ShapeTexture.ShapeType.Rectangle)
        msgBackgroundsPos(0) = New OpenTK.Vector2(enemyLabel.pos.X - msgPadding, enemyLabel.pos.Y - msgPadding)
    End Sub

    Public Sub render(delta)
        For i = 0 To 0
            SpriteBatch.drawTexture(msgBackgrounds(i), msgBackgroundsPos(i))
        Next
        enemyLabel.render(delta)
    End Sub

End Class
