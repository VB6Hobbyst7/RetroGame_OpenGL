''' <summary>
''' Handles miscellaneous physics related calculations
''' </summary>
Public Class PhysicUtils

    Private Shared PADDING As Integer = 1 'Padding for collisions to improve slide motion

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
        Return (collisionBitmask And categoryBitmask) <> 0
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
            objA.pos.Y + PADDING < objB.pos.Y + objB.getHeight() And
            objA.pos.Y + objA.getHeight() + PADDING > objB.pos.Y)
    End Function

    Public Shared Function doesCollide(objA As BoundingRect, objB As BoundingRect) As Boolean
        Return (objA.pos.X < objB.pos.X + objB.size.X And
            objA.pos.X + objA.size.X > objB.pos.X And
            objA.pos.Y < objB.pos.Y + objB.size.Y And
            objA.pos.Y + objA.size.Y > objB.pos.Y)
    End Function

    Public Shared Function pointWithin(px As Integer, py As Integer, bounding As BoundingRect) As Boolean
        Return px > bounding.pos.X And px < bounding.pos.X + bounding.size.X And
            py > bounding.pos.Y And py < bounding.pos.Y + bounding.size.Y
    End Function

    Public Shared Function pointWithin(pos As OpenTK.Vector2, bounding As BoundingRect) As Boolean
        Return pointWithin(pos.X, pos.Y, bounding)
    End Function

    ''' <summary>
    ''' Calculates the absolute distance between 2  2-dimensional vectors
    ''' </summary>
    ''' <param name="Vector A"></param>
    ''' <param name="Vector B"></param>
    ''' <returns>Distance</returns>
    Public Shared Function calcDistance(a As OpenTK.Vector2, b As OpenTK.Vector2) As Double
        Return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2))
    End Function

End Class
