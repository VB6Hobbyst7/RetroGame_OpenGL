Imports System.Drawing

Public Class SwitchStyle

    Public selectedColor As Color
    Public unselectedColor As Color
    Public highlightColor As Color
    Public textMargin As Integer 'Number of pixels to seperate edge of text and switch edges

    Public Sub New(selectedColor As Color, unselectedColor As Color, highlightColor As Color, textMargin As Integer)
        Me.selectedColor = selectedColor
        Me.unselectedColor = unselectedColor
        Me.highlightColor = highlightColor
        Me.textMargin = textMargin
    End Sub

End Class
