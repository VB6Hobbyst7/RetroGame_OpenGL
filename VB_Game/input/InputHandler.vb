Imports OpenTK
Imports OpenTK.Input

Public Class InputHandler

    Public Shared keyListeners As New List(Of KeyListener)
    Public Shared mouseListeners As New List(Of MouseListener)
    Private Shared WithEvents game As Game
    Private Shared keys As New Dictionary(Of Integer, Boolean)

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

    Private Shared Sub KeyDown(sender As Object, e As KeyboardKeyEventArgs) Handles game.KeyDown
        keys.Item(e.Key) = True
        For i = 0 To keyListeners.Count - 1
            keyListeners(i).KeyDown(e)
        Next
    End Sub

    Private Shared Sub KeyUp(sender As Object, e As KeyboardKeyEventArgs) Handles game.KeyUp
        keys.Item(e.Key) = False
        For i = 0 To keyListeners.Count - 1
            keyListeners(i).KeyUp(e)
        Next
    End Sub

    ''' <summary>
    ''' Returns whether specified key is currently pressed
    ''' </summary>
    ''' <param name="keyCode"></param>
    ''' <returns></returns>
    Public Shared Function isKeyDown(keyCode As Integer) As Boolean
        If Not keys.Keys.Contains(keyCode) Then
            Return False
        End If
        Return keys.Item(keyCode)
    End Function



End Class
