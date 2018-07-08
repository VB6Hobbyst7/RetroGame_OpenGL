Public Class Constants

    Public Const FPS_DEBUG = False
    Public Const MAX_FRAME_DELTA_TIME = 0.1 'Prevents issues with window grabbing


    Private Shared _DESIGN_SCALE_FACTOR As Single = 1
    Public Shared Property DESIGN_SCALE_FACTOR() As Single
        Get
            Return _DESIGN_SCALE_FACTOR
        End Get
        Set(ByVal value As Single)
            _DESIGN_SCALE_FACTOR = value
        End Set
    End Property

    'Public Shared DESIGN_SCALE_FACTOR = 1

    Public Shared DESIGN_WIDTH As Integer = 720 * DESIGN_SCALE_FACTOR
    Public Shared DESIGN_HEIGHT As Integer = 540 * DESIGN_SCALE_FACTOR
    Public Const ASPECT_RATIO As Decimal = 4.0F / 3.0F

    'Drawing Constants
    Public Shared GRAPHICS_TRIANGLE_RADIUS_RATIO = 1
    Public Shared MIN_TRIANGLES_CIRCLE = 20
    Public Shared TOP_LEFT_COORD = New OpenTK.Vector2(-DESIGN_WIDTH / 2, -DESIGN_HEIGHT / 2)

    'Physics Constants
    Public Shared PIXELS_IN_METER As Integer = 30 * DESIGN_SCALE_FACTOR
    Public Shared ACC_GRAVITY As New OpenTK.Vector2(0, 45)

    'Map Constants
    Public Shared TILE_SIZE As Integer = 30 * DESIGN_SCALE_FACTOR
    Public Shared MAP_WIDTH As Integer = DESIGN_WIDTH / TILE_SIZE
    Public Shared MAP_HEIGHT As Integer = DESIGN_HEIGHT / TILE_SIZE

    'Resource Constants
    Public Const MAP_RES_DIR As String = "./res/maps/"
    Public Const IMG_RES_DIR As String = "./res/img/"
    Public Const TILE_RES_DIR = IMG_RES_DIR + "tiles/"

    ''' <summary>
    ''' All possible collidable obj bit mask signatures
    ''' </summary>
    Public Shared COLLISION_CATEGORIES() As Integer = {Physics_CATEGORY.LEVEL,
        Physics_CATEGORY.ENEMY, Physics_CATEGORY.PROJECTILE, Physics_CATEGORY.NO_COLLISION, Physics_CATEGORY.CHEST}

    ''' <summary>
    ''' Different types of physics bodies mapping to category bitmasks
    ''' </summary>
    Enum Physics_CATEGORY
        NO_COLLISION = 0
        LEVEL = 6 '110
        ENEMY = 3 '011
        PROJECTILE = 4 '100
        CHEST = 16 '10000
    End Enum

    ''' <summary>
    ''' Different types of physics bodies mapping to collision bitmasks
    ''' </summary>
    Enum Physics_COLLISION
        NO_COLLISION = 0
        PLAYER = 18 '10010
        LEVEL = 0 'level doesn't collide (things collide with level)
        ENEMY = 12 '1100
        PROJECTILE = 3 '011
    End Enum

End Class
