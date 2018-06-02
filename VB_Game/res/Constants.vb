Public Class Constants

    Public Const FPS_DEBUG = False

    Public Const INIT_SCREEN_WIDTH As Integer = 1280
    Public Const INIT_SCREEN_HEIGHT As Integer = 960
    Public Const ASPECT_RATIO As Decimal = 4.0F / 3.0F

    'Physics Constants
    Public Const PIXELS_IN_METER As Integer = 32
    Public Shared ACC_GRAVITY As New OpenTK.Vector2(0, 35)

    'Map Constants
    Public Const TILE_SIZE As Integer = 32
    Public Const MAP_WIDTH As Integer = INIT_SCREEN_WIDTH / TILE_SIZE
    Public Const MAP_HEIGHT As Integer = INIT_SCREEN_HEIGHT / TILE_SIZE

    'Resource Constants
    Public Const MAP_RES_DIR As String = "./res/maps/"
    Public Const IMG_RES_DIR As String = "./res/img/"
    Public Const TILE_RES_DIR = IMG_RES_DIR + "tiles/"

End Class
