Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
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
    Private Shared previewImg As ImageTexture

    ''' <summary>
    ''' Map highscore
    ''' </summary>
    Private _highScore As Integer = 0 '0 by default in case property is not part of file
    Public Property Highscore() As Integer
        Get
            Return _highScore
        End Get
        Set(ByVal value As Integer)
            _highScore = value
            saveHighScore()
        End Set
    End Property

    Public Function getName() As String
        Return name
    End Function

    Public Function getChest() As Chest
        Return chest
    End Function

    Public Function getPreviewImg() As ImageTexture
        Return previewImg
    End Function

    Public Sub New(tileMapHandler As TileMapHandler, mapFileName As String)
        Me.tileMapHandler = tileMapHandler
        Me.mapFileName = mapFileName
        loadPreviewImage()
    End Sub

    ''' <summary>
    ''' Check if preview image exists otherwise create
    ''' </summary>
    Private Sub loadPreviewImage()
        Dim fileNameExcExt = mapFileName.Split(".")(0)
        Debug.WriteLine(fileNameExcExt)
        If Not System.IO.File.Exists(Constants.MAP_RES_DIR + fileNameExcExt + ".png") Then
            loadMap()
            generateSnapshot(fileNameExcExt + ".png")
        Else
            previewImg = ContentPipe.loadTexture(Constants.MAP_RES_DIR + fileNameExcExt + ".png")
        End If
    End Sub

    ''' <summary>
    ''' Creates 'new' chest by moving crate to a random position
    ''' as given in chestSpawnPositions
    ''' </summary>
    Public Sub spawnRandomChest()
        Dim distance = 0
        Dim newPoint As Vector2 = chestSpawnPositions(random.Next(chestSpawnPositions.Count))
        While PhysicUtils.calcDistance(chest.pos, newPoint) < Constants.MIN_CHEST_GAP
            newPoint = chestSpawnPositions(random.Next(chestSpawnPositions.Count))
        End While
        chest.setPos(newPoint)
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
                    Case "highscore"
                        reader.Read()
                        Me._highScore = reader.Value
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

                    Case "tiles"
                        'New Row
                        y += 1
                        x = 0
                End Select
            End If
        End While
        f.Close()
        reader.Close()

        'Loads background
        Dim fileNameExcExt = mapFileName.Split(".")(0)
        If System.IO.File.Exists(Constants.TILE_RES_DIR + fileNameExcExt + "_background.png") Then
            background = ContentPipe.loadTexture(Constants.TILE_RES_DIR + fileNameExcExt + "_background.png")
        End If

        'Load available spaces for chests
        For x = 0 To Constants.MAP_WIDTH - 1
            For y = 0 To Constants.MAP_HEIGHT - 2 '-2 as tile below check must be solid so can't go to bottom edge
                If Not tiles(x, y) Is Nothing Then
                    If tiles(x, y).texture Is Nothing Then
                        'If it has a texture its a solid tile
                        'and if its not solid tile below is solid its valid
                        If Not tiles(x, y + 1).texture Is Nothing Then
                            chestSpawnPositions.Add(New Vector2(startX +
                                (x * Constants.TILE_SIZE), startY + (y * Constants.TILE_SIZE)))
                        End If
                    End If
                End If
            Next
        Next


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
            SpriteBatch.drawTexture(background, Constants.TOP_LEFT_COORD, New Vector2(Constants.DESIGN_SCALE_FACTOR))
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

    ''' <summary>
    ''' Creates a map snapshot and exports it to an img: used for map previews
    ''' </summary>
    Public Sub generateSnapshot(outputFile)
        Dim outImg = New Drawing.Bitmap(64 * Constants.MAP_WIDTH, 64 * Constants.MAP_HEIGHT)
        Dim graphics = Drawing.Graphics.FromImage(outImg)

        If Not background Is Nothing Then
            graphics.DrawImage(New Drawing.Bitmap(Constants.TILE_RES_DIR + mapFileName.Split(".")(0) + "_background.png"),
                               New Drawing.Rectangle(0, 0, outImg.Width, outImg.Height))
        End If

        For x = 0 To Constants.MAP_WIDTH - 1
            For y = 0 To Constants.MAP_HEIGHT - 1
                If Not tiles(x, y).getName() Is Nothing Then
                    graphics.DrawImage(New Drawing.Bitmap(Constants.TILE_RES_DIR + tiles(x, y).getName()),
                                       x * 64, y * 64)
                End If
            Next
        Next
        outImg.Save(Constants.MAP_RES_DIR + outputFile)
        previewImg = ContentPipe.loadTexture(outImg)
    End Sub

    ''' <summary>
    ''' Takes current highscore and saves it to the file
    ''' </summary>
    Private Sub saveHighScore()
        Dim mapsStr As String = ""
        Try
            mapsStr = System.IO.File.ReadAllText(Constants.MAP_RES_DIR + mapFileName)
        Catch ex As Exception
            Debug.WriteLine(ex)
            'Failed to read
            Return
        End Try
        Dim mapsObj = JObject.Parse(mapsStr)
        If mapsObj("highscore") Is Nothing Then
            mapsObj.Add("highscore", Highscore)
        Else
            mapsObj("highscore") = Highscore
        End If
        Dim writer As New JsonTextWriter(New System.IO.StreamWriter(Constants.MAP_RES_DIR + mapFileName))
        mapsObj.WriteTo(writer)
        writer.Close()
    End Sub

End Class
