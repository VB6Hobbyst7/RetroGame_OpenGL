Imports OpenTK.Input

''' <summary>
''' Interface providing ability to register for mouse events
''' </summary>
Public Interface MouseListener

    Sub MouseScroll(e As MouseWheelEventArgs)
    Sub MouseButtonDown(e As MouseEventArgs)
    Sub MouseButtonUp(e As MouseEventArgs)
    Sub MouseMove(e As MouseMoveEventArgs)

End Interface
