Public Class PhysicUtils

    Public Shared Function pixelsToMeters(pixels As Integer) As Double
        Return pixels / Constants.PIXELS_IN_METER
    End Function

    Public Shared Function metersToPixels(meters As Double) As Integer
        Return CInt(meters * Constants.PIXELS_IN_METER)
    End Function

    Public Shared Function metersToPixels(meters As OpenTK.Vector2) As OpenTK.Vector2
        Return New OpenTK.Vector2(metersToPixels(meters.X), metersToPixels(meters.Y))
    End Function

    Public Shared Function pixelsToMeters(pixels As OpenTK.Vector2) As OpenTK.Vector2
        Return New OpenTK.Vector2(pixelsToMeters(pixels.X), pixelsToMeters(pixels.Y))
    End Function

End Class
