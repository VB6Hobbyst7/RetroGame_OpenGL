Imports System.Drawing
Imports OpenTK
''' <summary>
''' Loads in and stores a collection of sub images from one image
''' </summary>
Public Class TextureAtlas

    Private path As String
    Private imgSize As Vector2
    Private textures() As ImageTexture

    Public Function getTextures() As ImageTexture()
        Return textures
    End Function

    Public Sub New(path As String, imgSize As Vector2)
        Me.path = path
        Me.imgSize = imgSize
        loadAtlas()
    End Sub

    Public Sub loadAtlas()
        Dim img As New Bitmap(path)
        Dim imgsAcross = CInt(img.Width / imgSize.X)
        Dim imgsDown = CInt(img.Height / imgSize.Y)
        textures = New ImageTexture((imgsAcross * imgsDown) - 1) {} '-1 because sizes start at 0
        For y = 0 To imgsDown - 1
            For x = 0 To imgsAcross - 1
                textures(y * imgsAcross + x) = ContentPipe.loadTexture(img.Clone(
                    New Rectangle(x * imgSize.X, y * imgSize.Y, imgSize.X, imgSize.Y), Imaging.PixelFormat.Format32bppArgb))
            Next
        Next
        img.Dispose()
    End Sub

End Class
