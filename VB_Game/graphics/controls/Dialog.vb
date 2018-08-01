Imports OpenTK.Input

Public Class Dialog : Inherits Control : Implements MouseListener

    Private okBtn As Button
    Private cancelBtn As Button
    Private textLabels As New List(Of TextLabel)
    Private dialogDefaultSize As New OpenTK.Vector2(300 * Constants.DESIGN_SCALE_FACTOR, 200 * Constants.DESIGN_SCALE_FACTOR)
    Private dialogBackColour = Drawing.Color.FromArgb(255, 60, 60, 60)

    'Darkens everything behind dialog
    Private fullscreenTint As New ShapeTexture(Constants.DESIGN_WIDTH, Constants.DESIGN_WIDTH,
                                               Drawing.Color.FromArgb(128, 0, 0, 0), ShapeTexture.ShapeType.Rectangle)
    Private btnStyle As New ButtonStyle(Drawing.Brushes.White, Drawing.Color.FromArgb(255, 20, 20, 20))
    Private btnSize = New Drawing.Size(120 * Constants.DESIGN_SCALE_FACTOR, 40 * Constants.DESIGN_SCALE_FACTOR)
    Private mainFont = New Drawing.Font("Impact", 18 * Constants.DESIGN_SCALE_FACTOR, Drawing.FontStyle.Regular)

    Public Sub New()
        Me.customRender = True
        Me.texture = New ShapeTexture(dialogDefaultSize.X, dialogDefaultSize.Y, dialogBackColour, ShapeTexture.ShapeType.Rectangle)
        Me.pos = New OpenTK.Vector2(-dialogDefaultSize.X / 2, -dialogDefaultSize.Y / 2)
        Dim buttonSpacers = (dialogDefaultSize.X - 2 * btnSize.Width) / 3 'Spacing between buttons
        okBtn = New Button("OK", New OpenTK.Vector2(Me.pos.X + buttonSpacers, Me.pos.Y + dialogDefaultSize.Y - btnSize.Height * 1.5),
                mainFont, btnSize, btnStyle)
        cancelBtn = New Button("CANCEL", New OpenTK.Vector2(okBtn.pos.X + btnSize.Width + buttonSpacers, okBtn.pos.Y),
                               mainFont, btnSize, btnStyle)
        cancelBtn.setOnClickListener(AddressOf onCancelClicked)
        InputHandler.registerOverrideListener(Me)
    End Sub

    Public Sub configureText(lines As String())
        Dim textStartX = 10 * Constants.DESIGN_SCALE_FACTOR
        Dim textStartY = 20 * Constants.DESIGN_SCALE_FACTOR
        For i = 0 To lines.Count
            'textLabels(i) = new TextLabel()
        Next
    End Sub

    'Registers the result listeners for the ok btn on the dialog
    Public Sub registerResultListeners(onOkClicked As Button.OnClick)
        okBtn.setOnClickListener(onOkClicked)
    End Sub

    ''' <summary>
    ''' Runs when cancel btn is clicked dismissing dialog
    ''' </summary>
    Private Sub onCancelClicked()
        InputHandler.unregisterOverrideListener()
    End Sub

    Public Overrides Sub render(delta As Double)
        MyBase.render(delta)
        SpriteBatch.drawTexture(fullscreenTint, Constants.TOP_LEFT_COORD)
        SpriteBatch.drawControl(Me)
        okBtn.render(delta)
        cancelBtn.render(delta)
    End Sub

    Public Sub MouseButtonUp(e As MouseEventArgs) Implements MouseListener.MouseButtonUp
        okBtn.MouseButtonUp(e)
        cancelBtn.MouseButtonUp(e)
    End Sub

    Public Sub MouseScroll(e As MouseWheelEventArgs) Implements MouseListener.MouseScroll
        okBtn.MouseScroll(e)
        cancelBtn.MouseScroll(e)
    End Sub

    Public Sub MouseButtonDown(e As MouseEventArgs) Implements MouseListener.MouseButtonDown
        okBtn.MouseButtonDown(e)
        cancelBtn.MouseButtonDown(e)
    End Sub

    Public Sub MouseMove(e As MouseMoveEventArgs) Implements MouseListener.MouseMove
        okBtn.MouseMove(e)
        cancelBtn.MouseMove(e)
    End Sub

End Class
