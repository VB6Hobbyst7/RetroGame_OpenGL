Public Class Constants

    Public Const FPS_DEBUG = True

    Public Shared INIT_SCREEN_WIDTH As Integer = 720
    Public Shared INIT_SCREEN_HEIGHT As Integer = 540
    Public Const ASPECT_RATIO As Decimal = 4.0F / 3.0F

    'Physics Constants
    Public Const PIXELS_IN_METER As Integer = 30
    Public Shared ACC_GRAVITY As New OpenTK.Vector2(0, 45)

    'Map Constants
    Public Shared TILE_SIZE As Integer = 30
    Public Shared MAP_WIDTH As Integer = INIT_SCREEN_WIDTH / TILE_SIZE
    Public Shared MAP_HEIGHT As Integer = INIT_SCREEN_HEIGHT / TILE_SIZE

    'Resource Constants
    Public Const MAP_RES_DIR As String = "./res/maps/"
    Public Const IMG_RES_DIR As String = "./res/img/"
    Public Const TILE_RES_DIR = IMG_RES_DIR + "tiles/"

    ''' <summary>
    ''' All possible collidable obj bit mask signatures
    ''' </summary>
    Public Shared COLLISION_CATEGORIES() As Integer = {Physics_CATEGORY.LEVEL,
        Physics_CATEGORY.ENEMY, Physics_CATEGORY.PROJECTILE, Physics_CATEGORY.NO_COLLISION}

    ''' <summary>
    ''' Different types of physics bodies mapping to category bitmasks
    ''' </summary>
    Enum Physics_CATEGORY
        NO_COLLISION = 0
        LEVEL = 6 '110
        ENEMY = 7 '111
        PROJECTILE = 2 '010
    End Enum

    ''' <summary>
    ''' Different types of physics bodies mapping to collision bitmasks
    ''' </summary>
    Enum Physics_COLLISION
        NO_COLLISION = 0
        PLAYER = 4 '100
        LEVEL = 0 'level doesn't collide (things collide with level)
        ENEMY = 10 '1010
        PROJECTILE = 3 '011
    End Enum

End Class
