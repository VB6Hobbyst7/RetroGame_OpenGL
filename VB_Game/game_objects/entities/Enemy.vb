Imports OpenTK

Public Class Enemy : Inherits Entity

    Private FAST_SPEED As Integer = 6 * Constants.PIXELS_IN_METER
    Private SPEED As Integer = 3 * Constants.PIXELS_IN_METER
    Private started As Boolean = False 'flag indicating whether enemy has hit the ground initially
    Private leftDir As Boolean = False
    Private spawnPos As Vector2

    ''' <summary>
    ''' Creates new enemy
    ''' </summary>
    ''' <param name="pos"></param>
    ''' <param name="texture"></param>
    ''' <param name="leftDir">Should go left first</param>
    Public Sub New(pos As Vector2, texture As Texture, leftDir As Boolean)
        MyBase.New(pos, texture)
        Me.velocity = New Vector2(0, velocity.Y)
        Me.leftDir = leftDir
        spawnPos = pos
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
            If started = False Then
                started = True
                If leftDir Then
                    velocity = New Vector2(-SPEED, 0)
                Else
                    velocity = New Vector2(SPEED, 0)
                End If
            End If
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

        'Check if fallen below map
        If pos.Y > Constants.DESIGN_HEIGHT / 2 Then
            respawn() 'If fallen out of map, respawn
        End If
    End Sub

    ''' <summary>
    ''' Respawns enemy at top
    ''' </summary>
    Public Sub respawn()
        Me.pos = spawnPos
        Me.texture = EnemyFactory.FAST_ENEMY_TEXTURE 'switch graphic to show faster enemy
        Me.SPEED = Me.FAST_SPEED 'Speed up enemy
        leftDir = Not leftDir 'Switch direction
        'Apply speedup
        If leftDir Then
            velocity = New Vector2(-SPEED, 0)
        Else
            velocity = New Vector2(SPEED, 0)
        End If
    End Sub

End Class
