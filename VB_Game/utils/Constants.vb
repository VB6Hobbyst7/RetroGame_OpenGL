Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

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
            'Set corresponding values
            DESIGN_WIDTH = 720 * DESIGN_SCALE_FACTOR
            DESIGN_HEIGHT = 540 * DESIGN_SCALE_FACTOR
            PIXELS_IN_METER = 30 * DESIGN_SCALE_FACTOR
            TILE_SIZE = 30 * DESIGN_SCALE_FACTOR
            TOP_LEFT_COORD = New OpenTK.Vector2(-DESIGN_WIDTH / 2, -DESIGN_HEIGHT / 2)
            MAP_WIDTH = DESIGN_WIDTH / TILE_SIZE
            MAP_HEIGHT = DESIGN_HEIGHT / TILE_SIZE
        End Set
    End Property

    'Graphics constants
    Public Shared GRAPHICS_PRESETS As New Dictionary(Of String, Single())
    Public Shared NUM_FSAA_SAMPLES = 4
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

    'Gameplay Constants
    Public Shared MIN_CHEST_GAP = TILE_SIZE * 10 'Minimum spacing between chests spawns

#Region "Physics Constants"

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

#End Region

    ''' <summary>
    ''' Initialises constants - loading settings
    ''' </summary>
    Public Shared Sub init()
        GRAPHICS_PRESETS.Add("HIGH", {2.0, 32.0})
        GRAPHICS_PRESETS.Add("MED", {1.0, 8.0})
        GRAPHICS_PRESETS.Add("LOW", {0.5, 4})
        loadSettings()
    End Sub

    ''' <summary>
    ''' Loads settings from .settings file
    ''' </summary>
    Public Shared Sub loadSettings()
        Dim settingsStr As String = ""
        Try
            settingsStr = System.IO.File.ReadAllText("./.settings")
        Catch ex As Exception
            Debug.WriteLine(ex)
            'Failed reading settings use default settings
            Return
        End Try
        Dim settingsObj = JObject.Parse(settingsStr)
        Dim graphicsSettings = CType(settingsObj.GetValue("graphics"), JObject)

        If Not graphicsSettings.GetValue("preset") Is Nothing Then
            NUM_FSAA_SAMPLES = GRAPHICS_PRESETS(graphicsSettings.GetValue("preset"))(1)
            DESIGN_SCALE_FACTOR = GRAPHICS_PRESETS(graphicsSettings.GetValue("preset"))(0)
        End If
    End Sub

    ''' <summary>
    ''' Saves all settings to .settings file
    ''' </summary>
    Public Shared Sub saveSettings()
        Dim settings As New JObject
        Dim graphicsSettings As New JObject()
        graphicsSettings.Add(New JProperty("fsaa_samples", NUM_FSAA_SAMPLES))
        graphicsSettings.Add(New JProperty("resolution_design_scale", DESIGN_SCALE_FACTOR))

        settings.Add(New JProperty("graphics", graphicsSettings))

        Dim writer As New JsonTextWriter(New System.IO.StreamWriter("./.settings"))
        settings.WriteTo(writer)
        writer.Close()
    End Sub

End Class
