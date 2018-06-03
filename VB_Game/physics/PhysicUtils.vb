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
        Return collisionBitmask And categoryBitmask <> 0
    End Function

    ''' <summary>
    ''' Performs an AABB collision detection test
    ''' </summary>
    ''' <param name="bodyA"></param>
    ''' <param name="bodyB"></param>
    ''' <returns>Collide</returns>
    Public Shared Function doesCollide(bodyA As RigidBody, bodyB As RigidBody) As Boolean
        Return doesCollide(bodyA.parent, bodyB.parent)
    End Function

    ''' <summary>
    ''' Performs an AABB collision detection test
    ''' </summary>
    ''' <param name="objA"></param>
    ''' <param name="objB"></param>
    ''' <returns>Collide</returns>
    Public Shared Function doesCollide(objA As GameObject, objB As GameObject) As Boolean
        Return (objA.pos.X < objB.pos.X + objB.getWidth() And
            objA.pos.X + objA.getWidth() > objB.pos.X And
            objA.pos.Y + 1 < objB.pos.Y + objB.getHeight() And
            objA.pos.Y + objA.getHeight() + 1 > objB.pos.Y)
    End Function

    Public Shared Function doesCollide(objA As BoundingRect, objB As BoundingRect) As Boolean
        Return (objA.pos.X < objB.pos.X + objB.size.X And
            objA.pos.X + objA.size.X > objB.pos.X And
            objA.pos.Y < objB.pos.Y + objB.size.Y And
            objA.pos.Y + objA.size.Y > objB.pos.Y)
    End Function

End Class