Public Class Chest : Inherits GameObject

    Private Const IMG_NAME = "chest.png"
    Private Const CHEST_SIZE = 1 'Size as a ratio to tile size

    Public Sub New()
        'Configure chest for collisions
        MyBase.New(False)
        Me.texture = ContentPipe.loadTexture(Constants.TILE_RES_DIR + IMG_NAME)
        Me.scale = New OpenTK.Vector2(Constants.TILE_SIZE / (texture.width * CHEST_SIZE),
                                      Constants.TILE_SIZE / (texture.height * CHEST_SIZE))
        Me.pos = OpenTK.Vector2.Zero
        PhysicsHandler.addPhysicsBody(New RigidBody(Me,
                           Constants.Physics_CATEGORY.CHEST, Constants.Physics_COLLISION.LEVEL))
    End Sub

    ''' <summary>
    ''' Sets position of chest adjusting to center on bottom of tile
    ''' </summary>
    ''' <param name="pos"></param>
    Public Sub setPos(pos As OpenTK.Vector2)
        Me.pos = New OpenTK.Vector2(pos.X + (Constants.TILE_SIZE - Me.getWidth()) / 2,
                                    pos.Y + (Constants.TILE_SIZE - Me.getHeight()) / 2)
        Debug.WriteLine(Me.pos)
    End Sub

End Class
