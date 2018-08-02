Imports OpenTK.Input

Public Class Dialog : Inherits Control : Implements MouseListener

    Delegate Sub DialogOk()
    Delegate Sub DialogCancel()
    Private DialogOkListener As DialogOk
    Private DialogCancelListener As DialogCancel
    Private okBtn As Button
    Private cancelBtn As Button
    Private textLabels As New List(Of TextLabel)
    Private dialogDefaultSize As New OpenTK.Vector2(330 * Constants.DESIGN_SCALE_FACTOR, 200 * Constants.DESIGN_SCALE_FACTOR)
    Private dialogBackColour = Drawing.Color.FromArgb(255, 60, 60, 60)

    'Darkens everything behind dialog
    Private fullscreenTint As New ShapeTexture(Constants.DESIGN_WIDTH, Constants.DESIGN_WIDTH,
                                               Drawing.Color.FromArgb(128, 0, 0, 0), ShapeTexture.ShapeType.Rectangle)
    Private btnStyle As New ButtonStyle(Drawing.Brushes.White, Drawing.Color.FromArgb(255, 20, 20, 20))
    Private btnSize = New Drawing.Size(120 * Constants.DESIGN_SCALE_FACTOR, 40 * Constants.DESIGN_SCALE_FACTOR)
    Private mainFont As Drawing.Font

    Public Sub New(msgTextLines As String(), Optional ByVal fontSize As Integer = 18, Optional ByVal textPadding As Integer = 0)
        Me.mainFont = New Drawing.Font("Impact", fontSize * Constants.DESIGN_SCALE_FACTOR, Drawing.FontStyle.Regular)
        Me.customRender = True
        Me.texture = New ShapeTexture(dialogDefaultSize.X, dialogDefaultSize.Y, dialogBackColour, ShapeTexture.ShapeType.Rectangle)
        Me.pos = New OpenTK.Vector2(-dialogDefaultSize.X / 2, -dialogDefaultSize.Y / 2)
        Dim buttonSpacers = (dialogDefaultSize.X - 2 * btnSize.Width) / 3 'Spacing between buttons
        okBtn = New Button("YES", New OpenTK.Vector2(Me.pos.X + buttonSpacers, Me.pos.Y + dialogDefaultSize.Y - btnSize.Height * 1.5),
                mainFont, btnSize, btnStyle)
        cancelBtn = New Button("CANCEL", New OpenTK.Vector2(okBtn.pos.X + btnSize.Width + buttonSpacers, okBtn.pos.Y),
                               mainFont, btnSize, btnStyle)
        cancelBtn.setOnClickListener(AddressOf onCancelClicked)
        okBtn.setOnClickListener(AddressOf onOkClicked)
        configureText(msgTextLines)
        If textPadding <> 0 Then
            'Apply padding
            For i = 1 To textLabels.Count - 1
                textLabels(i).applyTextPadding(textPadding)
            Next
        End If
    End Sub

    Public Sub show()
        InputHandler.registerOverrideListener(Me)
    End Sub

    ''' <summary>
    ''' Configures the dialog text
    ''' </summary>
    Public Sub configureText(lines As String())
        Dim textStartX = Me.pos.X + 10 * Constants.DESIGN_SCALE_FACTOR
        Dim textStartY = Me.pos.Y + 30 * Constants.DESIGN_SCALE_FACTOR
        textLabels.Add(New TextLabel(lines(0), mainFont, Drawing.Brushes.White))
        textLabels(0).pos = new OpenTK.Vector2(textStartX, textStartY)
        For i = 1 To lines.Count - 1
            textLabels.Add(New TextLabel(lines(i), mainFont, Drawing.Brushes.White))
            textLabels(i).pos = New OpenTK.Vector2(textStartX,
                    textLabels(i - 1).pos.Y + textLabels(i - 1).getHeight())
        Next
    End Sub

    'Registers the result listeners for the ok btn on the dialog
    Public Sub registerResultListeners(onOkClicked As DialogOk, onCancelClicked As DialogCancel)
        Me.DialogOkListener = onOkClicked
        Me.DialogCancelListener = onCancelClicked
    End Sub

    ''' <summary>
    ''' Runs when cancel btn is clicked dismissing dialog
    ''' </summary>
    Private Sub onCancelClicked()
        InputHandler.unregisterOverrideListener()
        DialogCancelListener.Invoke()
    End Sub

    ''' <summary>
    ''' Runs when ok btn is clicked dismissing dialog
    ''' </summary>
    Private Sub onOkClicked()
        InputHandler.unregisterOverrideListener()
        DialogOkListener.Invoke()
    End Sub

    Public Overrides Sub render(delta As Double)
        MyBase.render(delta)
        SpriteBatch.drawTexture(fullscreenTint, Constants.TOP_LEFT_COORD)
        SpriteBatch.drawControl(Me)
        okBtn.render(delta)
        cancelBtn.render(delta)
        For i = 0 To textLabels.Count - 1
            textLabels(i).render(delta)
        Next
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
