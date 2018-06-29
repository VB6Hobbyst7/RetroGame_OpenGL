Imports OpenTK

Public Class StartScreen : Inherits Screen

    Private Shared instance As StartScreen
    Private testButton As Button
    Private testSlider As Slider

    Public Sub New()
        testButton = New Button("Button", Vector2.Zero, New Drawing.Size(300, 80))
        testSlider = New Slider(New Vector2(-200, -100), New Vector2(200, 40))
    End Sub

    Public Overrides Sub render(delta As Double)
        testButton.render(delta)
        testSlider.render(delta)
    End Sub

    Public Overrides Sub update(delta As Double)
    End Sub

    Public Overrides Sub dispose()
    End Sub

    Public Overrides Sub onResize()
    End Sub

    Public Shared Function getInstance() As StartScreen
        If instance Is Nothing Then
            instance = New StartScreen()
        End If
        Return instance
    End Function

End Class
