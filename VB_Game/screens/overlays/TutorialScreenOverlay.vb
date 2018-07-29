''' <summary>
''' Overlay showing tutorial instructions
''' </summary>
Public Class TutorialScreenOverlay

    Private labels(4) As TextLabel
    Private mainFont As New Drawing.Font("Impact", 18 * Constants.DESIGN_SCALE_FACTOR, Drawing.FontStyle.Regular)
    Private msgBackgroundColor = Drawing.Color.FromArgb(80, 0, 0, 0)
    Private msgBackgrounds(3) As ShapeTexture
    Private msgBackgroundsPos(3) As OpenTK.Vector2
    Private msgPadding = 10
    Private continueBtn As Button

    Public Sub New()
        labels(0) = New TextLabel("This is an enemy", mainFont, Drawing.Brushes.White)
        labels(0).pos = New OpenTK.Vector2(-labels(0).getWidth() / 2, -250 * Constants.DESIGN_SCALE_FACTOR)

        labels(1) = New TextLabel("avoid touching it", mainFont, Drawing.Brushes.White)
        labels(1).pos = New OpenTK.Vector2(labels(0).pos.X, labels(0).pos.Y + labels(0).getHeight())

        msgBackgrounds(0) = New ShapeTexture(Math.Max(labels(0).getWidth(), labels(1).getWidth()) + msgPadding * 2, labels(0).getHeight() + labels(1).getHeight() + msgPadding * 2,
                                             msgBackgroundColor, ShapeTexture.ShapeType.Rectangle)
        msgBackgroundsPos(0) = New OpenTK.Vector2(labels(0).pos.X - msgPadding, labels(0).pos.Y - msgPadding)

        labels(2) = New TextLabel("Move using WASD", mainFont, Drawing.Brushes.White)
        labels(2).pos = New OpenTK.Vector2(-labels(2).getWidth() / 2 + 16 * Constants.DESIGN_SCALE_FACTOR, 20 * Constants.DESIGN_SCALE_FACTOR)

        msgBackgrounds(1) = New ShapeTexture(labels(2).getWidth() + msgPadding * 2, labels(2).getHeight() + msgPadding * 2,
                                             msgBackgroundColor, ShapeTexture.ShapeType.Rectangle)
        msgBackgroundsPos(1) = New OpenTK.Vector2(labels(2).pos.X - msgPadding, labels(2).pos.Y - msgPadding)

        'Chest message
        labels(3) = New TextLabel("This is a chest", mainFont, Drawing.Brushes.White)
        labels(3).pos = New OpenTK.Vector2(135 * Constants.DESIGN_SCALE_FACTOR, -130 * Constants.DESIGN_SCALE_FACTOR)

        labels(4) = New TextLabel("collect them", mainFont, Drawing.Brushes.White)
        labels(4).pos = New OpenTK.Vector2(labels(3).pos.X, labels(3).pos.Y + labels(3).getHeight())

        msgBackgrounds(2) = New ShapeTexture(Math.Max(labels(3).getWidth(), labels(4).getWidth()) + msgPadding * 2, labels(3).getHeight() + labels(4).getHeight() + msgPadding * 2,
                                             msgBackgroundColor, ShapeTexture.ShapeType.Rectangle)
        msgBackgroundsPos(2) = New OpenTK.Vector2(labels(3).pos.X - msgPadding, labels(3).pos.Y - msgPadding)

        continueBtn = New Button("CONTINUE", New OpenTK.Vector2(-150 * Constants.DESIGN_SCALE_FACTOR / 2, 180 * Constants.DESIGN_SCALE_FACTOR), New Drawing.Size(150 * Constants.DESIGN_SCALE_FACTOR,
                    45 * Constants.DESIGN_SCALE_FACTOR), New ButtonStyle(Drawing.Brushes.White, Drawing.Color.FromArgb(255, 64, 64, 64)))
        continueBtn.setOnClickListener(AddressOf onContinuePressed)

    End Sub

    Public Sub onContinuePressed()
        GameScreen.getInstance().CurrentState = GameScreen.State.PLAY
    End Sub

    Public Sub render(delta)
        For i = 0 To 2
            SpriteBatch.drawTexture(msgBackgrounds(i), msgBackgroundsPos(i))
        Next
        For i = 0 To 4
            labels(i).render(delta)
        Next
        continueBtn.render(delta)
    End Sub

End Class
