Imports System.IO
Imports System.Drawing
Imports OpenTK.Graphics.OpenGL
Imports System.Drawing.Imaging


Public Class ContentPipe

    Private Shared IMG_DIR As String = "res/img/"

    ''' <summary>
    ''' Loads texture into memory under OpenGL Texture
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Public Shared Function loadTexture(path As String) As Texture2d

        'If Not System.IO.File.Exists(path) Then
        'Throw New FileNotFoundException(IMG_DIR + path + " => not found")
        'End If

        Dim id As Integer = GL.GenTexture
        GL.BindTexture(TextureTarget.Texture2D, id)

        Dim img As New Bitmap(IMG_DIR + path)

        Dim data As BitmapData = img.LockBits(
            New Rectangle(0, 0, img.Width, img.Height),
            ImageLockMode.ReadOnly,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb)

        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                      OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0)

        img.UnlockBits(data)

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, TextureWrapMode.ClampToEdge)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, TextureWrapMode.ClampToEdge)

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureMinFilter.Linear)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear)

        Return New Texture2d(id, img.Width, img.Height)
    End Function

End Class
