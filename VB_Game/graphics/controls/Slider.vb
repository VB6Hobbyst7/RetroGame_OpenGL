﻿Imports OpenTK
Imports OpenTK.Input
Imports VB_Game

Public Class Slider : Inherits Control : Implements MouseListener

    Private Const SCROLL_SPEED = 2
    Private Const MAX_VAL = 100
    Private Const MIN_VAL = 0
    Private hoveredOver As Boolean = False
    Private grabbedSlider As Boolean = False
    Private sliderTexture As ShapeTexture
    Private valueChangeListener As OnValueChanged

    Private _value As Integer
    Public Property Value() As Integer
        Get
            Return _value
        End Get
        Set(ByVal value As Integer)
            If value > MAX_VAL Then
                _value = MAX_VAL
            ElseIf value < MIN_VAL Then
                _value = MIN_VAL
            Else
                _value = value
            End If
            sliderTexture.width = (Me.getWidth() * (_value / 100))
            If Not valueChangeListener Is Nothing Then
                valueChangeListener.Invoke(_value)
            End If
        End Set
    End Property

    Delegate Function OnValueChanged(value As Object)

    Public Sub setOnValueChangeListener(listener As OnValueChanged)
        Me.valueChangeListener = listener
    End Sub

    Public Sub New(pos As Vector2, size As Vector2)
        Me.texture = New ShapeTexture(size.X, size.Y,
            Drawing.Color.Gray, ShapeTexture.ShapeType.Rectangle)
        Me.pos = pos

        sliderTexture = New ShapeTexture(0, size.Y, Drawing.Color.Black, ShapeTexture.ShapeType.Rectangle)

        InputHandler.mouseListeners.Add(Me)
    End Sub

    Public Overrides Sub render(delta As Double)
        MyBase.render(delta)
        SpriteBatch.drawTexture(sliderTexture, pos)
    End Sub

    Public Sub MouseScroll(e As MouseWheelEventArgs) Implements MouseListener.MouseScroll
        If hoveredOver Then
            Debug.WriteLine(Value)
            Value += e.DeltaPrecise * SCROLL_SPEED
        End If
    End Sub

    Public Sub MouseButtonDown(e As MouseEventArgs) Implements MouseListener.MouseButtonDown
        If hoveredOver Then
            grabbedSlider = True
            Value = ((SpriteBatch.normaliseScreenX(e.X) - pos.X) / Me.getWidth()) * 100
        End If
    End Sub

    Public Sub MouseButtonUp(e As MouseEventArgs) Implements MouseListener.MouseButtonUp
        grabbedSlider = False
    End Sub

    Public Sub MouseMove(e As MouseMoveEventArgs) Implements MouseListener.MouseMove

        hoveredOver = PhysicUtils.pointWithin(SpriteBatch.normaliseScreenCoords(e.X, e.Y),
                                   New BoundingRect(New Vector2(Me.getWidth(), Me.getWidth()), Me.pos))
        If grabbedSlider Then
            Value = ((SpriteBatch.normaliseScreenX(e.X) - pos.X) / Me.getWidth()) * 100
        End If
    End Sub
End Class