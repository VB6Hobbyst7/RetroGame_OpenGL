''' <summary>
''' Represents a screen in the game (e.g. gamescreen, pausescreen)
''' </summary>
Public MustInherit Class Screen

    Public MustOverride Sub render(delta As Double)
    Public MustOverride Sub update(delta As Double)
    Public MustOverride Sub dispose()
    Public MustOverride Sub onResize()
    Public MustOverride Sub onShow()

End Class
