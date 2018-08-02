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
        'Does map preloading for preview images (not very effective if time improve)
        For i = 0 To maps.Count - 1
            loadMap(i)
            PhysicsHandler.clearBodies()
        Next
        currentMap = Nothing
    End Sub

    ''' <summary>
    ''' Loads map based on map index for maps array
    ''' </summary>
    ''' <param name="mapIndex"></param>
    Public Sub loadMap(mapIndex As Integer)
        'Clear anything existing
        PhysicsHandler.clearBodies()
        maps(mapIndex).loadMap()
        currentMap = maps(mapIndex)
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
            If file.Extension.ToLower() = ".ldat" Then
                maps.Add(New Map(Me, file.Name))
            End If
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

    ''' <summary>
    ''' Returns a tile based on name cloned from loaded texture to reduce memory usage
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    Public Function getTileCopyByName(name As String)
        For Each tile In tiles
            If tile.getName() = name Then
                Return tile.Clone()
            End If
        Next
        Return Nothing
    End Function

End Class
