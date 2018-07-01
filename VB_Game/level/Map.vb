﻿Imports Newtonsoft.Json
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
    Private startX As Integer = -Constants.DESIGN_WIDTH / 2
    Private startY As Integer = -Constants.DESIGN_HEIGHT / 2
    Private background As Texture
    Private chestSpawnPositions As New List(Of Vector2)
    Private chest As Chest
    Private Shared random As New Random()

    Public Function getName() As String
        Return name
    End Function

    Public Sub New(tileMapHandler As TileMapHandler, mapFileName As String)
        Me.tileMapHandler = tileMapHandler
        Me.mapFileName = mapFileName
    End Sub

    ''' <summary>
    ''' Creates 'new' chest by moving crate to a random position
    ''' as given in chestSpawnPositions
    ''' </summary>
    Public Sub spawnRandomChest()
        chest.setPos(chestSpawnPositions(random.Next(chestSpawnPositions.Count)))
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

                        'Temp if tile is air valid chest spawn location
                        If Not CBool(reader.Value) Then
                            chestSpawnPositions.Add(currentTile.pos)
                        End If

                    Case "tiles"
                        'New Row
                        y += 1
                        x = 0
                End Select
            End If
        End While
        f.Close()
        reader.Close()

        'Temp load static background
        background = ContentPipe.loadTexture(Constants.TILE_RES_DIR + "background_1.png")

        'Create chest object and move to initial pos
        chest = New Chest()
        spawnRandomChest()
    End Sub

    ''' <summary>
    ''' Renders the map to screen
    ''' </summary>
    Public Sub render(delta As Double)
        'Draw background
        If Not background Is Nothing Then
            SpriteBatch.drawTexture(background, New Vector2(-Constants.DESIGN_WIDTH / 2,
                -Constants.DESIGN_HEIGHT / 2))
        End If
        'Draw tiles
        For x = 0 To Constants.MAP_WIDTH - 1
            For y = 0 To Constants.MAP_HEIGHT - 1
                If Not tiles(x, y) Is Nothing Then
                    tiles(x, y).render(delta)
                End If
            Next
        Next
        chest.render(delta)
    End Sub

    Public Overrides Function ToString() As String
        Dim rows As New List(Of String)
        For y = 0 To Constants.MAP_HEIGHT - 1
            Dim row As New List(Of String)
            For x = 0 To Constants.MAP_WIDTH - 1
                row.Add(tiles(x, y).ToString())
            Next
            rows.Add(String.Join(",", row))
        Next
        Return String.Join(vbLf, rows)
    End Function

End Class
