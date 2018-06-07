Imports VB_Game

Public Class Gun : Inherits Item

    ''' <summary>
    ''' Fire rate of gun in seconds
    ''' </summary>
    Private Const FIRE_RATE = 2
    Private timeSinceLastFire As Double

    Public Sub New(holder As Player)
        MyBase.New(holder)
    End Sub

    Public Overrides Sub useItem()
        If timeSinceLastFire > CDbl(1 / FIRE_RATE) Then
            Debug.WriteLine("fire")
            timeSinceLastFire = 0
            createProjectile()
        End If
    End Sub

    Private Sub createProjectile()
        Dim startX = holder.pos.X
        Dim startY = holder.pos.Y + holder.getHeight() / 2 - SimpleProjectile.HEIGHT / 2
        If holder.getXOrient() = 1 Then
            startX += holder.getWidth()
        Else
            startX -= SimpleProjectile.WIDTH / 2
        End If

        Dim projectile As New SimpleProjectile(New OpenTK.Vector2(startX, startY), holder.getXOrient())
        GameScreen.getInstance().addPhysicsBasedObject(projectile,
            Constants.Physics_CATEGORY.PROJECTILE, Constants.Physics_COLLISION.PROJECTILE)
    End Sub

    Public Overrides Sub update(delta As Double)
        timeSinceLastFire += delta
    End Sub
End Class
