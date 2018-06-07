Imports OpenTK

Public Class Enemy : Inherits Entity

    Public Sub New(pos As Vector2, texture As Texture)
        MyBase.New(pos, texture)
    End Sub

    Public Overrides Sub onCollide(objB As GameObject)
        MyBase.onCollide(objB)
        If objB.GetType.IsAssignableFrom(GetType(Player)) Then
            'Debug.WriteLine("player collided with enemy")
            Me.dispose()
        End If
    End Sub

    Public Overrides Sub tick(delta As Double)
        velocity = New Vector2(-5, velocity.Y)
        pos = New Vector2(pos.X + velocity.X * delta, pos.Y + velocity.Y * delta)
    End Sub

End Class
