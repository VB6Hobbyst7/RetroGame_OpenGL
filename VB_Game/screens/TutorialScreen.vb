Public Class TutorialScreen : Inherits Screen

    Private Shared instance As TutorialScreen

    Public Overrides Sub render(delta As Double)
    End Sub

    Public Overrides Sub update(delta As Double)
    End Sub

    Public Overrides Sub dispose()
    End Sub

    Public Overrides Sub onResize()
    End Sub

    Public Shared Function getInstance() As TutorialScreen
        If instance Is Nothing Then
            instance = New TutorialScreen()
        End If
        Return instance
    End Function

End Class
