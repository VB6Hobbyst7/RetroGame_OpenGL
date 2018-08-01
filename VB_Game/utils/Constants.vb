Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

'''
''' Holds Game Constants and loads user settings
'''
Public Class Constants

    Public Const FPS_DEBUG = False
    Public Const MAX_FRAME_DELTA_TIME = 0.1 'Prevents issues with window grabbing
    Public Const RELEASE = False 'If its release disable developer features

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
    Public Shared CURRENT_GRAPHICS_PRESET As String
    Public Shared GRAPHICS_PRESETS As New Dictionary(Of String, Single())
    Public Shared NUM_FSAA_SAMPLES = 4
    Public Shared DESIGN_WIDTH As Integer = 720 * DESIGN_SCALE_FACTOR
    Public Shared DESIGN_HEIGHT As Integer = 540 * DESIGN_SCALE_FACTOR
    Public Shared WINDOW_START_WIDTH = 720
    Public Shared WINDOW_START_HEIGHT = 540
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
    Public Const AUDIO_RES_DIR As String = "./res/audio/"

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
        GRAPHICS_PRESETS.Add("HIGH", {3.0, 32.0})
        GRAPHICS_PRESETS.Add("MED", {2, 8.0})
        GRAPHICS_PRESETS.Add("LOW", {1, 4.0})
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
            Debug.WriteLine("no settings file present: creating default")
            genDefaultSettings()
            saveSettings()
            'Failed reading settings use default settings
            Return
        End Try
        Dim settingsObj = JObject.Parse(settingsStr)
        Dim graphicsSettings = CType(settingsObj.GetValue("graphics"), JObject)

        If Not graphicsSettings.GetValue("preset") Is Nothing Then
            Dim preset = graphicsSettings.GetValue("preset").ToString().ToUpper()
            NUM_FSAA_SAMPLES = GRAPHICS_PRESETS(preset)(1)
            DESIGN_SCALE_FACTOR = CDbl(GRAPHICS_PRESETS(preset)(0))
            CURRENT_GRAPHICS_PRESET = preset
        End If

        Dim volumeSettings = CType(settingsObj.GetValue("volume"), JObject)
        If Not volumeSettings.GetValue("vol_level") Is Nothing Then
            'set volume
            If AudioMaster.getInstance().isEnabled() Then
                AudioMaster.getInstance().setVolume(CInt(volumeSettings.GetValue("vol_level")))
            End If
        End If

        If Not graphicsSettings.GetValue("windowed_mode") Is Nothing Then
            If graphicsSettings.GetValue("windowed_mode").ToString().ToLower() = "windowed" Then
                Game.getInstance().WindowState = OpenTK.WindowState.Normal
            ElseIf graphicsSettings.GetValue("windowed_mode").ToString().ToLower() = "fullscreen" Then
                Game.getInstance().WindowState = OpenTK.WindowState.Fullscreen
            End If
        End If

    End Sub

    ''' <summary>
    ''' Saves all settings to .settings file
    ''' </summary>
    Public Shared Sub saveSettings()
        Dim settings As New JObject
        Dim volumeSettings As New JObject()

        If AudioMaster.getInstance().isEnabled() Then
            volumeSettings.Add(New JProperty("vol_level", AudioMaster.getInstance().getVolume()))
        Else
            volumeSettings.Add(New JProperty("vol_level", 0))
        End If


        Dim graphicsSettings As New JObject()
        graphicsSettings.Add(New JProperty("preset", CURRENT_GRAPHICS_PRESET))

        If Game.getInstance().WindowState = OpenTK.WindowState.Normal Then
            graphicsSettings.Add(New JProperty("windowed_mode", "windowed"))
        ElseIf Game.getInstance().WindowState = OpenTK.WindowState.Fullscreen Then
            graphicsSettings.Add(New JProperty("windowed_mode", "fullscreen"))
        End If

        settings.Add(New JProperty("graphics", graphicsSettings))
        settings.Add(New JProperty("volume", volumeSettings))

        Dim writer As New JsonTextWriter(New System.IO.StreamWriter("./.settings"))
        settings.WriteTo(writer)
        writer.Close()
    End Sub

    Private Shared Sub genDefaultSettings()
        Game.getInstance().WindowState = OpenTK.WindowState.Normal
        If AudioMaster.getInstance().isEnabled() Then
            AudioMaster.getInstance().setVolume(0)
        End If
        CURRENT_GRAPHICS_PRESET = "LOW"
        NUM_FSAA_SAMPLES = GRAPHICS_PRESETS(CURRENT_GRAPHICS_PRESET)(1)
        DESIGN_SCALE_FACTOR = CDbl(GRAPHICS_PRESETS(CURRENT_GRAPHICS_PRESET)(0))
    End Sub

End Class
