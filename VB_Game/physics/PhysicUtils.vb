''' <summary>
''' Handles miscellaneous physics related calculations
''' </summary>
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

    ''' <summary>
    ''' Checks whether the bitmask signatures can collide
    ''' </summary>
    ''' <param name="collisionBitmask"></param>
    ''' <param name="categoryBitmask"></param>
    ''' <returns></returns>
    Public Shared Function canCollide(collisionBitmask As Integer, categoryBitmask As Integer) As Boolean
        Return collisionBitmask & categoryBitmask <> 0
    End Function

    ''' <summary>
    ''' Performs an AABB collision detection test
    ''' </summary>
    ''' <param name="bodyA"></param>
    ''' <param name="bodyB"></param>
    ''' <returns></returns>
    Public Shared Function doesCollide(bodyA As RigidBody, bodyB As RigidBody)
        Return (bodyA.pos.X < bodyB.pos.X + bodyB.width And
            bodyA.pos.X + bodyA.width > bodyB.pos.X And
            bodyA.pos.Y < bodyB.pos.Y + bodyB.height And
            bodyA.pos.Y + bodyA.height > bodyB.pos.Y)
    End Function

End Class
