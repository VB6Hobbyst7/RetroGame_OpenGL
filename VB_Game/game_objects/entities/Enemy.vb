Imports OpenTK

Public Class Enemy : Inherits Entity

    Private Const SPEED As Integer = 3 * Constants.PIXELS_IN_METER

    Public Sub New(pos As Vector2, texture As Texture)
        MyBase.New(pos, texture)
        Me.velocity = New Vector2(-SPEED, velocity.Y)
    End Sub

    Public Overrides Sub onCollide(objB As GameObject)
        'Distances needed to stop colliding (smallest gives which side is most likely colliding, not full proof solution)
        'By determining the smallest you can determine collision side to prevent movement in that direction and correct position
        'So no collision occurs
        Dim deltaL = Math.Abs(pos.X + getWidth() - objB.pos.X)
        Dim deltaR = Math.Abs(pos.X - (objB.pos.X + objB.getWidth()))
        Dim deltaD = Math.Abs(pos.Y + getHeight() - objB.pos.Y)
        Dim deltaU = Math.Abs(pos.Y - (objB.pos.Y + objB.getHeight()))

        Dim smallest = Math.Min(deltaL, deltaR)
        smallest = Math.Min(smallest, deltaD)
        smallest = Math.Min(smallest, deltaU)

        If deltaL = smallest Then
            pos = New Vector2(objB.pos.X - getWidth(), pos.Y)
            velocity = New Vector2(-SPEED, velocity.Y)
        End If

        If deltaR = smallest Then
            pos = New Vector2(objB.pos.X + objB.getHeight(), pos.Y)
            velocity = New Vector2(SPEED, velocity.Y)
        End If

        If deltaD = smallest Then
            velocity = New Vector2(velocity.X, 0)
            pos = New Vector2(pos.X, objB.pos.Y - getHeight())
            isGrounded = True
        End If

        If deltaU = smallest Then
            velocity = New Vector2(velocity.X, 0)
            pos = New Vector2(pos.X, objB.pos.Y + objB.getWidth())
        End If

        If objB.GetType.IsAssignableFrom(GetType(SimpleProjectile)) Then
            Me.dispose()
        End If
    End Sub

    Public Overrides Sub tick(delta As Double)
        velocity = New Vector2(velocity.X, velocity.Y)
        pos = New Vector2(pos.X + velocity.X * delta, pos.Y + velocity.Y * delta)
    End Sub

End Class
