Imports System.Drawing

Public Class ButtonStyle

    Public textColor As Drawing.Brush
    Public backColor As Drawing.Color
    Public borderColor As Drawing.Color
    Public borderSize As Integer = 0

    Public Sub New(textColor As Brush, backColor As Color)
        Me.textColor = textColor
        Me.backColor = backColor
    End Sub

    Public Sub New(textColor As Brush, backColor As Color, borderColor As Color, borderSize As Integer)
        Me.New(textColor, backColor)
        Me.borderColor = borderColor
        Me.borderSize = borderSize
    End Sub
End Class
