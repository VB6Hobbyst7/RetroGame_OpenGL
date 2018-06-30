Public Class EnemyFactory

    Private Const SPAWN_X = 0
    Private Shared SPAWN_Y = -Constants.DESIGN_HEIGHT / 2

    Private Shared ENEMY_TEXTURE

    Private Const DIFFICULTY_MULTIPLIER = 1.1

    Private Const DOUBLE_SPAWN_CHANCE = 100 'chance of two enemies spawning at once
    Private Shared random As New Random()
    Private Shared SPAWN_RATE = 30 'number per minute
    Private Shared timeToNextSpawn = Math.Round(60 / SPAWN_RATE)

    Private Const queueSpawnCooldown = 0.5 'time needed for enemies to move out of way before spawning new
    Private Shared timeSinceLastSpawn = 0

    Private Shared spawnQueue As New Queue(Of Enemy)

    Public Shared Sub init()
        ENEMY_TEXTURE = New ShapeTexture(Constants.TILE_SIZE, Constants.TILE_SIZE,
                                         Drawing.Color.Crimson, ShapeTexture.ShapeType.Rectangle)
    End Sub

    Public Shared Sub spawn()
        Dim spawnCount = 1
        If Random.Next(0, 100) < DOUBLE_SPAWN_CHANCE Then
            spawnCount = 2
        End If
        'TODO: Add big one spawn and apply difficulty multiplier
        For i = 0 To spawnCount - 1
            spawnQueue.Enqueue(New Enemy(New OpenTK.Vector2(SPAWN_X, SPAWN_Y), ENEMY_TEXTURE, True))
        Next
    End Sub

    Public Shared Sub reset()
        spawnQueue.Clear()
        timeToNextSpawn = Math.Round(60 / SPAWN_RATE)
        timeSinceLastSpawn = 0
    End Sub

    Public Shared Sub tick(delta As Double)
        timeToNextSpawn -= delta
        timeSinceLastSpawn += delta
        If timeToNextSpawn <= 0 Then
            spawn()
            timeToNextSpawn = Math.Round((60 / SPAWN_RATE))
        End If

        If spawnQueue.Count > 0 Then
            If timeSinceLastSpawn > queueSpawnCooldown Then
                GameScreen.getInstance().addPhysicsBasedObject(spawnQueue.Dequeue(),
                    Constants.Physics_CATEGORY.ENEMY, Constants.Physics_COLLISION.ENEMY)
                timeSinceLastSpawn = 0
            End If
        End If
    End Sub

End Class
