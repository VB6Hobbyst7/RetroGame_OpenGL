Imports System.IO
''' <summary>
''' Handles map resources
''' </summary>
Public Class TileMapHandler

    Private Shared instance As TileMapHandler
    Public maps As New List(Of Map)
    Public currentMap As Map

    'List of all tiles available
    Public tiles As New List(Of Tile)

    Private Sub New()
        loadAllTileTextures()
        loadAllMaps()
    End Sub

    Public Sub loadMap()
        maps(0).loadMap()
        currentMap = maps(0)
        PhysicsHandler.clearBodies()
        tiles.Clear()
    End Sub

    ''' <summary>
    ''' Loads all tile textures fomr res/img/tiles into memory
    ''' </summary>
    Public Sub loadAllTileTextures()
        Dim dir As New DirectoryInfo(".\res\img\tiles")
        For Each file In dir.GetFiles()
            tiles.Add(New Tile(file.Name))
        Next
    End Sub

    Public Sub loadAllMaps()
        Dim dir As New DirectoryInfo(".\res\maps\")
        For Each file In dir.GetFiles()
            maps.Add(New Map(Me, file.Name))
        Next
    End Sub

    Public Shared Function getInstance() As TileMapHandler
        If instance Is Nothing Then
            instance = New TileMapHandler()
        End If
        Return instance
    End Function

    Public Sub render(delta As Double)
        If Not currentMap Is Nothing Then
            currentMap.render(delta)
        End If
    End Sub

    Public Function getTileCopyByName(name As String)
        For Each tile In tiles
            If tile.getName() = name Then
                Return tile.Clone()
            End If
        Next
        Return Nothing
    End Function

End Class
