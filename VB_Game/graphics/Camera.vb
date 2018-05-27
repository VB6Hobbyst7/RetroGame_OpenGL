Imports OpenTK
Imports OpenTK.Graphics.OpenGL
Imports OpenTK.Input
Imports VB_Game

''' <summary>
''' Represents the current viewport
''' </summary>
Public Class Camera : Implements MouseListener

    Public position As Vector2

    Public rotation As Decimal

    Public zoomLevel As Decimal

    Public Sub New(position As Vector2, rotation As Decimal, zoomLevel As Decimal)
        Me.position = position
        Me.rotation = rotation
        Me.zoomLevel = zoomLevel
        InputHandler.mouseListeners.Add(Me)
    End Sub

#Region "Input Handling"

    Public Sub MouseScroll(e As MouseWheelEventArgs) Implements MouseListener.MouseScroll
        zoomLevel += e.Delta / 200
    End Sub

    Public Sub MouseButtonDown(e As MouseEventArgs) Implements MouseListener.MouseButtonDown
        Dim pos = New Vector2(e.X, e.Y)
        pos -= New Vector2(Game.getInstance().Width, Game.getInstance().Height) / 2
        pos = toWorldCoords(pos)
        position = pos
    End Sub

#End Region

#Region "Helper Functions"

    ''' <summary>
    ''' Called every update frame
    ''' </summary>
    Public Sub update()
        'Track player movement
    End Sub

    Public Sub applyTransform()
        Dim transform As Matrix4 = Matrix4.Identity
        transform = Matrix4.Mult(transform, Matrix4.CreateTranslation(-position.X, -position.Y, 0))
        transform = Matrix4.Mult(transform, Matrix4.CreateRotationZ(-rotation))
        transform = Matrix4.Mult(transform, Matrix4.CreateScale(zoomLevel, zoomLevel, 1))
        GL.MultMatrix(transform)
    End Sub

    ''' <summary>
    ''' Takes a screen cordinate and converts it into world coordinates based on transform matrix
    ''' </summary>
    ''' <param name="screenCoord"></param>
    ''' <returns></returns>
    Public Function toWorldCoords(screenCoord As Vector2)
        screenCoord /= zoomLevel
        Dim dx = New Vector2(Math.Cos(rotation), Math.Sin(rotation))
        Dim dy = New Vector2(Math.Cos(rotation + MathHelper.PiOver2), Math.Sin(rotation + MathHelper.PiOver2))
        Return (position + dx * screenCoord.X + dy * screenCoord.Y)
    End Function
#End Region
End Class
