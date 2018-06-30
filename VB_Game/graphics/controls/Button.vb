Imports OpenTK
Imports OpenTK.Input
Imports VB_Game

Public Class Button : Inherits Control : Implements MouseListener

    Private clickListener As OnClick
    Private label As TextLabel
    Private highlightOverlay As ShapeTexture
    Private hoveredOver As Boolean = False
    Private size As Vector2

    Public Sub New(text As String, pos As Vector2, size As Drawing.Size)
        Me.size = New Vector2(size.Width, size.Height)
        label = New TextLabel("Button Test", size)
        label.pos = pos
        highlightOverlay = New ShapeTexture(size.Width, size.Height,
            Drawing.Color.FromArgb(50, 255, 255, 255), ShapeTexture.ShapeType.Rectangle)

        Me.texture = New ShapeTexture(size.Width, size.Height,
            Drawing.Color.CadetBlue, ShapeTexture.ShapeType.Rectangle)
        label.pos = New Vector2(pos.X + (size.Width - label.texture.width) / 2,
                                pos.Y + (size.Height - label.texture.height) / 2)
        InputHandler.mouseListeners.Add(Me)
    End Sub

    Public Overrides Sub render(delta As Double)
        MyBase.render(delta)
        If hoveredOver Then
            SpriteBatch.drawTexture(highlightOverlay, pos)
        End If
        label.render(delta)
    End Sub

    Delegate Sub OnClick()

    Public Sub setOnClickListener(listener As OnClick)
        Me.clickListener = listener
    End Sub

    Public Sub MouseButtonUp(e As MouseEventArgs) Implements MouseListener.MouseButtonUp
    End Sub

    Public Sub MouseScroll(e As MouseWheelEventArgs) Implements MouseListener.MouseScroll
    End Sub

    Public Sub MouseButtonDown(e As MouseEventArgs) Implements MouseListener.MouseButtonDown
        If hoveredOver Then
            clickListener.Invoke()
        End If
    End Sub

    Public Sub MouseMove(e As MouseMoveEventArgs) Implements MouseListener.MouseMove

        hoveredOver = PhysicUtils.pointWithin(SpriteBatch.normaliseScreenCoords(e.X, e.Y),
                                   New BoundingRect(Me.size, Me.pos))
    End Sub
End Class
