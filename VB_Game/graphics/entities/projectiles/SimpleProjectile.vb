''' <summary>
''' Projectile goes straight (no gravity affect)
''' </summary>
Public Class SimpleProjectile : Inherits Entity

    Private Const MAX_VELOCITY = 40

    ''' <summary>
    ''' Creates new projectile
    ''' </summary>
    ''' <param name="startPos"></param>
    ''' <param name="dx">direction of profile (1 --> right, -1 --> left)</param>
    Public Sub New(startPos As OpenTK.Vector2, dx As Integer)
        MyBase.New(startPos, New ShapeTexture(16, 16, Drawing.Color.BlueViolet, ShapeTexture.ShapeType.Rectangle))
        Me.affectedByGravity = False
        Me.velocity = New OpenTK.Vector2(dx * MAX_VELOCITY)
    End Sub

End Class
