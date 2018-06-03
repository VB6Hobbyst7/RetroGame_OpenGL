Imports OpenTK
Imports VB_Game

Public Class Player : Inherits Entity : Implements CollisionListener

    Public Sub New(pos As Vector2, texture As Texture)
        MyBase.New(pos, texture)
        PhysicsHandler.collisionListeners.Add(Me)
    End Sub

    Public Sub onCollide(objA As GameObject, objB As GameObject) Implements CollisionListener.onCollide
        Me.velocity = Vector2.Zero
        While (PhysicUtils.doesCollide(objA, objB))
            Me.pos = New Vector2(pos.X, pos.Y - 1)
        End While
    End Sub
End Class
