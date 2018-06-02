Public Class Constants

    Public Const FPS_DEBUG = False

    Public Shared INIT_SCREEN_WIDTH As Integer = 1280
    Public Shared INIT_SCREEN_HEIGHT As Integer = 960
    Public Shared ASPECT_RATIO As Decimal = 4.0F / 3.0F

    'Map Constants
    Public Shared TILE_SIZE As Integer = 64
    Public Shared MAP_WIDTH As Integer = INIT_SCREEN_WIDTH / TILE_SIZE
    Public Shared MAP_HEIGHT As Integer = INIT_SCREEN_HEIGHT / TILE_SIZE

    'Resource Constants
    Public Shared MAP_RES_DIR As String = "./res/maps/"
    Public Shared IMG_RES_DIR As String = "./res/img/"
    Public Shared TILE_RES_DIR = IMG_RES_DIR + "tiles/"

End Class
