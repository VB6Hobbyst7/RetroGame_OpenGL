Imports OpenTK
Imports OpenTK.Input

Public Class InputHandler

    Public Shared keyListeners As New List(Of KeyListener)
    Public Shared mouseListeners As New List(Of MouseListener)
    Private Shared WithEvents game As Game

    Public Shared Sub init(g As Game)
        game = g
    End Sub

    Private Shared Sub MouseScroll(sender As Object, e As MouseWheelEventArgs) Handles game.MouseWheel
        For i = 0 To mouseListeners.Count - 1
            mouseListeners(i).MouseScroll(e)
        Next
    End Sub

    Private Shared Sub MouseButtonDown(sender As Object, e As MouseButtonEventArgs) Handles game.MouseDown
        For i = 0 To mouseListeners.Count - 1
            mouseListeners(i).MouseButtonDown(e)
        Next
    End Sub

End Class
