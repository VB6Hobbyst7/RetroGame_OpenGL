Imports VB_Game
''' <summary>
''' Projectile goes straight (no gravity affect)
''' </summary>
Public Class SimpleProjectile : Inherits Entity

    Private Const MAX_VELOCITY = 30 * Constants.PIXELS_IN_METER 'in m/s
    Public Const WIDTH = 16
    Public Const HEIGHT = 16

    ''' <summary>
    ''' Creates new projectile
    ''' </summary>
    ''' <param name="startPos"></param>
    ''' <param name="dx">direction of profile (1 --> right, -1 --> left)</param>
    Public Sub New(startPos As OpenTK.Vector2, dx As Integer)
        MyBase.New(startPos, New ShapeTexture(WIDTH, HEIGHT, Drawing.Color.BlueViolet, ShapeTexture.ShapeType.Rectangle))
        Me.affectedByGravity = False
        Me.velocity = New OpenTK.Vector2(dx * MAX_VELOCITY, 0)
    End Sub

    Public Overrides Sub onCollide(objB As GameObject)
        MyBase.onCollide(objB)
        If objB.GetType.IsAssignableFrom(GetType(Tile)) Then
            Debug.WriteLine("Destroy projectile")
            GameScreen.getInstance().removeGameObjectNextFrame(Me)
        ElseIf objB.GetType.IsAssignableFrom(GetType(Enemy)) Then
            'Implement damage player logic
            GameScreen.getInstance().removeGameObjectNextFrame(Me)
        End If
    End Sub
End Class
