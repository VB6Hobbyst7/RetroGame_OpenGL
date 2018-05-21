Imports OpenTK.Input

''' <summary>
''' Interface providing ability to register for mouse events
''' </summary>
Public Interface MouseListener

    Sub MouseScroll(e As MouseWheelEventArgs)
    Sub MouseButtonDown(e As MouseEventArgs)

End Interface
