Imports OpenTK
Imports OpenTK.Input
Imports VB_Game

Public Class Switch : Inherits Control : Implements MouseListener

    Private highlightSelection As ShapeTexture
    Private highlightIndex As Integer 'Which segment highlight should cover
    Private switchSegments As ShapeTexture()
    Private values As String()
    Private selectedIndex As Integer
    Private selectedValue As String
    Private hoveredOver As Boolean = False
    Private size As Vector2
    Private switchPartWidth As Double 'Width of each swith segment
    Private style As SwitchStyle
    Private textLabels As TextLabel()
    Private valueChangeListener As OnValueChanged
    Private lastSelectedIndex As Integer = 0

    Public Function getSelectedValue() As String
        Return selectedValue
    End Function

    ''' <summary>
    ''' Creates new switch for number of values specified
    ''' </summary>
    ''' <param name="pos"></param>
    ''' <param name="size"></param>
    ''' <param name="values">Switch values</param>
    Public Sub New(pos As Vector2, size As Vector2, values As String(), style As SwitchStyle)
        Me.pos = pos
        Me.size = size
        Me.values = values
        Me.style = style
        Me.customRender = True
        switchPartWidth = CDbl(size.X / values.Count)
        highlightSelection = New ShapeTexture(switchPartWidth, size.Y, style.highlightColor, ShapeTexture.ShapeType.Rectangle)
        ReDim switchSegments(values.Count)
        ReDim textLabels(values.Count)
        For i = 0 To values.Count - 1
            switchSegments(i) = New ShapeTexture(switchPartWidth, size.Y, style.unselectedColor, ShapeTexture.ShapeType.Rectangle)
            textLabels(i) = New TextLabel(values(i), New Drawing.Size(size.X - style.textMargin, size.Y - style.textMargin), Drawing.Brushes.White)
            textLabels(i).pos = New Vector2(pos.X + (i * switchPartWidth) +
                ((switchPartWidth - textLabels(i).getWidth()) / 2), pos.Y + ((size.Y - textLabels(i).getHeight()) / 2))
        Next
        'Set 1st as default selected
        setSelectedIndex(0)
        InputHandler.mouseListeners.Add(Me)
    End Sub

    Public Sub New(pos As Vector2, size As Vector2, values As String())
        Me.New(pos, size, values, New SwitchStyle(Drawing.Color.FromArgb(255, 64, 64, 64),
                                                  Drawing.Color.FromArgb(255, 112, 112, 112),
                                                  Drawing.Color.FromArgb(32, 255, 255, 255),
                                                  8 * Constants.DESIGN_SCALE_FACTOR))
    End Sub

    Delegate Sub OnValueChanged(value As Object)

    Public Sub setOnValueChangeListener(listener As OnValueChanged)
        Me.valueChangeListener = listener
    End Sub

    Public Sub setSelectedIndex(index As Integer)
        For i = 0 To values.Count - 1
            If i = index Then
                switchSegments(i).color = style.selectedColor
            Else
                switchSegments(i).color = style.unselectedColor
            End If
        Next
        Me.selectedIndex = index
        Me.selectedValue = values(selectedIndex)
        If Not valueChangeListener Is Nothing And index <> lastSelectedIndex Then
            lastSelectedIndex = index
            valueChangeListener.Invoke(selectedValue)
        End If
    End Sub

    ''' <summary>
    ''' Gives the index of the segment based on the x value relative to the start of the switch (left edge = 0)
    ''' </summary>
    ''' <param name="x"></param>
    ''' <returns></returns>
    Public Function getSegmentByXRel(x As Integer) As Integer
        Return Math.Floor(Math.Abs(x) / switchPartWidth)
    End Function

    Public Overrides Sub render(delta As Double)
        MyBase.render(delta)
        For i = 0 To values.Count - 1
            SpriteBatch.drawTexture(switchSegments(i), New Vector2(pos.X + (switchPartWidth * i), pos.Y))
        Next
        'If hovered over draw corresponding highlight
        If hoveredOver Then
            SpriteBatch.drawTexture(highlightSelection, New Vector2(pos.X + (highlightIndex * switchPartWidth), pos.Y))
        End If

        'Draw text after so highlight is below text
        For i = 0 To values.Count - 1
            textLabels(i).render(delta)
        Next
    End Sub

    Public Sub MouseButtonDown(e As MouseEventArgs) Implements MouseListener.MouseButtonDown
        If hoveredOver And Visible Then
            'Calculate which switch segment was clicked
            setSelectedIndex(getSegmentByXRel(pos.X - SpriteBatch.normaliseScreenX(e.X)))
        End If
    End Sub

    Public Sub MouseMove(e As MouseMoveEventArgs) Implements MouseListener.MouseMove
        If Visible Then
            hoveredOver = PhysicUtils.pointWithin(SpriteBatch.normaliseScreenCoords(e.X, e.Y),
                                               New BoundingRect(Me.size, Me.pos))
            If hoveredOver Then
                highlightIndex = getSegmentByXRel(pos.X - SpriteBatch.normaliseScreenX(e.X))
            End If
        End If
    End Sub

    Public Sub MouseButtonUp(e As MouseEventArgs) Implements MouseListener.MouseButtonUp
    End Sub

    Public Sub MouseScroll(e As MouseWheelEventArgs) Implements MouseListener.MouseScroll
    End Sub

    Public Overrides Function getWidth() As Integer
        Return size.X
    End Function

    Public Overrides Function getHeight() As Integer
        Return size.Y
    End Function

End Class
