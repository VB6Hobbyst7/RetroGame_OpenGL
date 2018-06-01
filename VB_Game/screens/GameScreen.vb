''' <summary>
''' Main screen where the game is run
''' </summary>
Imports OpenTK
Imports System.Drawing

Public Class GameScreen : Inherits Screen

    Private Shared instance As GameScreen
    Private tileMapHandler As TileMapHandler

    Private Sub New()
        tileMapHandler = TileMapHandler.getInstance()
    End Sub

    Public Shared Function getInstance() As GameScreen
        If instance Is Nothing Then
            instance = New GameScreen()
        End If
        Return instance
    End Function

    Public Overrides Sub render(delta As Double)
        SpriteBatch.drawRect(New Vector2(32, 32), New Vector2(0, 0), Color.Black)
    End Sub

    Public Overrides Sub update(delta As Double)

    End Sub

    Public Overrides Sub dispose()

    End Sub

    Public Overrides Sub onResize()

    End Sub
End Class
