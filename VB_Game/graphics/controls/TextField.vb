Imports OpenTK
Imports OpenTK.Input
Imports VB_Game

Public Class TextField : Inherits Control : Implements MouseListener : Implements KeyListener

    Private highlightSelection As ShapeTexture
    Private hoveredOver As Boolean = False
    Private size As Vector2
    Private maxTextWidth As Integer
    Private isFocused As Boolean = False
    Private valueChangeListener As OnValueChanged
    Private textLabel As TextLabel
    Private mainFont = New Drawing.Font("Impact", 18 * Constants.DESIGN_SCALE_FACTOR, Drawing.FontStyle.Regular)
    Private validKeys As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    Private TEXT_PADDING = 5 * Constants.DESIGN_SCALE_FACTOR

    Public Sub New(pos As Vector2, size As Vector2)
        Me.pos = pos
        Me.size = size
        maxTextWidth = size.X - 2 * TEXT_PADDING
        Me.customRender = True
        InputHandler.mouseListeners.Add(Me)
        'Use placeholder string to get height measure
        textLabel = new TextLabel("Q", new Drawing.Size(size.X, size.Y), Drawing.Brushes.White)
        textLabel.pos = new Vector2(Me.pos.X + TEXT_PADDING, Me.pos.Y + (Me.size.Y - textLabel.getHeight()) / 2)
        textLabel.Text = ""
    End Sub

    Public Sub New(pos As Vector2, size As Vector2, initialString As String)
        Me.pos = pos
        Me.size = size
        maxTextWidth = size.X - 2 * TEXT_PADDING
        Me.customRender = True
        InputHandler.mouseListeners.Add(Me)
        'Use placeholder string to get height measure
        textLabel = new TextLabel(initialString, new Drawing.Size(size.X, size.Y), Drawing.Brushes.White)
        textLabel.pos = new Vector2(Me.pos.X + TEXT_PADDING, Me.pos.Y + (Me.size.Y - textLabel.getHeight()) / 2)
    End Sub

    Delegate Sub OnValueChanged(value As Object)

    Public Sub setOnValueChangeListener(listener As OnValueChanged)
        Me.valueChangeListener = listener
    End Sub

    Public Overrides Sub render(delta As Double)
        MyBase.render(delta)

    End Sub

    Private Sub KeyDown(e As KeyboardKeyEventArgs) Implements KeyListener.KeyDown
        If Visible And isFocused Then
            If e.Key = Key.BackSpace Then
                'Delete key on backspace
                textLabel.Text = textLabel.Text.Substring(0, textLabel.Text.Length - 1)
            ElseIf InStr(validKeys, e.Key.ToString()) Then
                'Character is valid
                textLabel.Text = textLabel.Text + e.Key.ToString()
                If textLabel.getWidth() > maxTextWidth Then
                    'If text is too long don't allow entering any more
                    textLabel.Text = textLabel.Text.Substring(0, textLabel.Text.Length - 1)
                End If
            End If
        End If
    End Sub

    Private Sub KeyUp(e As KeyboardKeyEventArgs) Implements KeyListener.KeyUp
    End Sub

    Public Sub MouseButtonDown(e As MouseEventArgs) Implements MouseListener.MouseButtonDown
        If hoveredOver And Visible Then
            isFocused = True
        Else
            isFocused = False
        End If
    End Sub

    Public Sub MouseMove(e As MouseMoveEventArgs) Implements MouseListener.MouseMove
        If Visible Then
            hoveredOver = PhysicUtils.pointWithin(SpriteBatch.normaliseScreenCoords(e.X, e.Y),
                                               New BoundingRect(Me.size, Me.pos))
        End If
    End Sub

    Public Sub MouseButtonUp(e As MouseEventArgs) Implements MouseListener.MouseButtonUp
    End Sub

    Public Sub MouseScroll(e As MouseWheelEventArgs) Implements MouseListener.MouseScroll
    End Sub

End Class
