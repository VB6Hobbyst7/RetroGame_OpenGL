Imports Newtonsoft.Json
Imports System.IO

''' <summary>
''' Represents a tile map consisting of a grid of tiles
''' </summary>
Public Class Map

    Public tiles(Constants.MAP_WIDTH, Constants.MAP_HEIGHT) As Tile
    Private mapFileName As String
    Private name As String

    Public Function getName() As String
        Return name
    End Function

    Public Sub New()
        Me.mapFileName = "map1.ldat"
        loadMap()
    End Sub

    ''' <summary>
    ''' Loads Json Formatted map into memory
    ''' </summary>
    Public Sub loadMap()
        Dim reader As New JsonTextReader(New StreamReader(New FileStream(Constants.MAP_RES_DIR + mapFileName, FileMode.Open)))
        Dim currentTile As Tile
        Dim x As Integer = 0
        Dim y As Integer = 0
        While (reader.Read())
            If Not reader.Value Is Nothing Then
                Select Case CStr(reader.Value)
                    Case "mapName"
                        reader.Read()
                        Me.name = reader.Value
                    Case "name"
                        reader.Read()
                        If reader.Value = "" Then
                            currentTile = New Tile()
                        Else
                            currentTile = TileMapHandler.getInstance().getTileCopyByName(reader.Value)
                        End If

                    Case "collidable"
                        tiles(x, y) = currentTile
                        x += 1
                        'TODO: Implement collision setup
                    Case "tiles"
                        'New Row
                        y += 1
                        x = 0
                End Select
                'Debug.WriteLine(reader.Value)
            End If
        End While
        Debug.WriteLine(Me.name)
    End Sub

    ''' <summary>
    ''' Renders the map to screen
    ''' </summary>
    Public Sub render()
        For x = 0 To Constants.MAP_WIDTH - 1
            For y = 0 To Constants.MAP_HEIGHT - 1
                If Not tiles(x, y) Is Nothing Then
                    tiles(x, y).render()
                End If
            Next
        Next
    End Sub

End Class
