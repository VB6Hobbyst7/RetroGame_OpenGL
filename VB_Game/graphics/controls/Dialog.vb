Public Class Dialog : Inherits Control : Implements MouseListener

    Delgate Sub OkClicked
    Delgate Sub CancelClicked
    Private btns As New List(Of Button)
    Private textLabels As New List(Of TextLabel)
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
        Dim buttonSpacers = (dialogDefaultSize.X - 2 * btnSize.X) / 3 'Spacing between buttons
        okBtn = New Button("OK", New OpenTK.Vector2(buttonSpacers, Me.pos.Y + dialogDefaultSize.Y - btnSize.Height * 1.5),
                btnSize, btnStyle)
        cancelBtn = New Button("CANCEL", New OpenTK.Vector2(okBtn.pos.X + btnSize.Width + buttonSpacers, okBtn.pos.Y), btnSize, btnStyle)
        InputHandler.registerOverrideListener(Me)
    End Sub

    Public Sub configureText(lines As String())
        Dim textStartX = 10 * Constants.DESIGN_SCALE_FACTOR
        Dim textStartY = 20 * Constants.DESIGN_SCALE_FACTOR
        For i = 0 To lines.Count
            textLabels(i) = new TextLabel()
        Next
    End Sub

    'Registers the result listeners for the ok and cancel btns on the dialog
    Public Sub registerResultListeners(onOkClicked As OkClicked, onCancelClicked As onCancelClicked)
        okBtn.setOnClickListener(AddressOf onOkClicked)
        cancelBtn.setOnClickListener(AddressOf onCancelClicked)
    End Sub

    Public Sub dismiss()
        InputHandler.unregisterOverrideListener()
    End Sub

    Public Overrides Sub render(delta As Double)
        MyBase.render(delta)
        SpriteBatch.drawTexture(fullscreenTint, Constants.TOP_LEFT_COORD)
        SpriteBatch.drawControl(Me)
        okBtn.render(delta)
    End Sub

    Public Sub MouseButtonUp(e As MouseEventArgs) Implements MouseListener.MouseButtonUp
        For i = 0 To btns.Count - 1
            btn.MouseButtonUp(e)
        Next
    End Sub

    Public Sub MouseScroll(e As MouseWheelEventArgs) Implements MouseListener.MouseScroll
        For i = 0 To btns.Count - 1
            btn.MouseButtonUp(e)
        Next
    End Sub

    Public Sub MouseButtonDown(e As MouseEventArgs) Implements MouseListener.MouseButtonDown
        For i = 0 To btns.Count - 1
            btn.MouseButtonUp(e)
        Next
    End Sub

    Public Sub MouseMove(e As MouseMoveEventArgs) Implements MouseListener.MouseMove
        For i = 0 To btns.Count - 1
            btn.MouseButtonUp(e)
        Next
    End Sub

End Class
