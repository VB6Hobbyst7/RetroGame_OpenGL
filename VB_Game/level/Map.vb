Imports Newtonsoft.Json
Imports System.IO
Imports OpenTK

''' <summary>
''' Represents a tile map consisting of a grid of tiles
''' </summary>
Public Class Map

    Public tiles(Constants.MAP_WIDTH, Constants.MAP_HEIGHT) As Tile
    Private mapFileName As String
    Private name As String
    Private tileMapHandler
    Private startX As Integer = -Constants.INIT_SCREEN_WIDTH / 2
    Private startY As Integer = -Constants.INIT_SCREEN_HEIGHT / 2

    Public Function getName() As String
        Return name
    End Function

    Public Sub New(tileMapHandler As TileMapHandler, mapFileName As String)
        Me.tileMapHandler = tileMapHandler
        Me.mapFileName = mapFileName
        loadMap()
    End Sub

    ''' <summary>
    ''' Loads Json Formatted map into memory
    ''' </summary>
    Public Sub loadMap()
        Debug.WriteLine("loading map")
        Dim f As StreamReader = File.OpenText(Constants.MAP_RES_DIR + mapFileName)
        Dim reader As New JsonTextReader(f)
        Dim currentTile As Tile
        Dim x As Integer = 0
        Dim y As Integer = -1
        While (reader.Read())
            If Not reader.Value Is Nothing Then
                Select Case CStr(reader.Value)
                    Case "mapName"
                        reader.Read()
                        Me.name = reader.Value
                    Case "n"
                        reader.Read()
                        If reader.Value = "" Then
                            currentTile = New Tile()
                            currentTile.pos = New Vector2(startX + (x * Constants.TILE_SIZE), startY + (y * Constants.TILE_SIZE))
                        Else
                            currentTile = tileMapHandler.getTileCopyByName(reader.Value)
                            currentTile.pos = New Vector2(startX + (x * Constants.TILE_SIZE), startY + (y * Constants.TILE_SIZE))
                        End If
                    Case "c"
                        tiles(x, y) = currentTile
                        reader.Read()
                        Dim catCollision = If(CBool(reader.Value),
                            Constants.Physics_CATEGORY.LEVEL, Constants.Physics_CATEGORY.NO_COLLISION) 'Set appropriate bitmask

                        PhysicsHandler.addPhysicsBody(New RigidBody(currentTile,
                            catCollision, Constants.Physics_COLLISION.LEVEL))
                        x += 1
                        'TODO: Implement collision setup
                    Case "tiles"
                        'New Row
                        y += 1
                        x = 0
                End Select
            End If
        End While
        f.Close()
        reader.Close()
    End Sub

    ''' <summary>
    ''' Renders the map to screen
    ''' </summary>
    Public Sub render(delta As Double)
        For x = 0 To Constants.MAP_WIDTH - 1
            For y = 0 To Constants.MAP_HEIGHT - 1
                If Not tiles(x, y) Is Nothing Then
                    tiles(x, y).render(delta)
                End If
            Next
        Next
    End Sub

End Class
