Imports OpenTK.Input

''' <summary>
''' Interface providing ability to register for key events
''' </summary>
Public Interface KeyListener

    Sub KeyUp(e As KeyboardKeyEventArgs)
    Sub KeyDown(e As KeyboardKeyEventArgs)

End Interface
