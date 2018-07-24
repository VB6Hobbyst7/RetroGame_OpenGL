Public Class Dialog : Inherits Control

    Private okBtn As Button
    Private cancelBtn As Button
    Private dialogDefaultSize As New OpenTK.Vector2(300 * Constants.DESIGN_SCALE_FACTOR, 200 * Constants.DESIGN_SCALE_FACTOR)
    Private dialogBackColour = Drawing.Color.FromArgb(255, 60, 60, 60)
    'Darkens everything behind dialog
    Private fullscreenTint As New ShapeTexture(Constants.DESIGN_WIDTH, Constants.DESIGN_WIDTH,
                                               Drawing.Color.FromArgb(128, 0, 0, 0), ShapeTexture.ShapeType.Rectangle)
    Private btnStyle As New ButtonStyle(Drawing.Brushes.White, Drawing.Color.FromArgb(255, 20, 20, 20))
    Private btnSize = New Drawing.Size(80 * Constants.DESIGN_SCALE_FACTOR, 40 * Constants.DESIGN_SCALE_FACTOR)

    Public Sub New()
        Me.customRender = True
        Me.texture = New ShapeTexture(dialogDefaultSize.X, dialogDefaultSize.Y, dialogBackColour, ShapeTexture.ShapeType.Rectangle)
        Me.pos = New OpenTK.Vector2(-dialogDefaultSize.X / 2, -dialogDefaultSize.Y / 2)
        okBtn = New Button("OK", New OpenTK.Vector2(0), btnSize, btnStyle)
    End Sub

    Public Overrides Sub render(delta As Double)
        MyBase.render(delta)
        SpriteBatch.drawTexture(fullscreenTint, Constants.TOP_LEFT_COORD)
        SpriteBatch.drawControl(Me)
        okBtn.render(delta)
    End Sub

End Class
