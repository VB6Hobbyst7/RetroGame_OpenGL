Imports OpenTK
Imports OpenTK.Graphics.OpenGL
Imports System.Drawing

Public Class SpriteBatch

    ''' <summary>
    ''' Draws an image on the screen
    ''' </summary>
    ''' <param name="texture"></param>
    ''' <param name="pos"></param>
    ''' <param name="scale"></param>
    ''' <param name="color"></param>
    ''' <param name="origin"></param>
    Public Shared Sub drawImage(texture As Texture2d, pos As Vector2, scale As Vector2, color As Color, origin As Vector2)
        Dim verts = New Vector2() {
            New Vector2(0, 0),
            New Vector2(1, 0),
            New Vector2(1, 1),
            New Vector2(0, 1)
        }
        GL.Enable(EnableCap.Texture2D)
        GL.BindTexture(TextureTarget.Texture2D, texture.id)
        GL.Begin(PrimitiveType.Quads)
        GL.Color3(color)
        For i = 0 To 3
            GL.TexCoord2(verts(i))
            verts(i).X *= texture.width 'Adjusts coordinates to match up with width of texture
            verts(i).Y *= texture.height 'Adjusts coordinates to match up with width of texture
            verts(i) -= origin
            verts(i) *= scale
            verts(i) += pos 'adjusts pos
            GL.Vertex2(verts(i))
        Next
        GL.End()
    End Sub

    ''' <summary>
    ''' Draws an image on the screen
    ''' </summary>
    ''' <param name="texture"></param>
    ''' <param name="pos"></param>
    ''' <param name="scale"></param>
    Public Shared Sub drawImage(texture As Texture2d, pos As Vector2, scale As Vector2)
        Dim verts = New Vector2() {
            New Vector2(0, 0),
            New Vector2(1, 0),
            New Vector2(1, 1),
            New Vector2(0, 1)
        }
        GL.Enable(EnableCap.Texture2D)
        GL.BindTexture(TextureTarget.Texture2D, texture.id)
        GL.Begin(PrimitiveType.Quads)
        GL.Color3(Color.White)
        For i = 0 To 3
            GL.TexCoord2(verts(i))
            verts(i).X *= texture.width 'Adjusts coordinates to match up with width of texture
            verts(i).Y *= texture.height 'Adjusts coordinates to match up with width of texture
            verts(i) *= scale
            verts(i) += pos 'adjusts pos
            GL.Vertex2(verts(i))
        Next
        GL.End()
    End Sub

    ''' <summary>
    ''' Draws a rectangle on screen
    ''' </summary>
    ''' <param name="size"></param>
    ''' <param name="pos"></param>
    ''' <param name="color"></param>
    Public Shared Sub drawRect(size As Vector2, pos As Vector2, color As Color)
        Dim verts = New Vector2() {
            New Vector2(0, 0),
            New Vector2(1, 0),
            New Vector2(1, 1),
            New Vector2(0, 1)
        }
        GL.Disable(EnableCap.Texture2D)
        GL.Begin(PrimitiveType.Quads)
        GL.Color3(color)
        For i = 0 To 3
            GL.TexCoord2(verts(i))
            verts(i).X *= size.X
            verts(i).Y *= size.Y
            verts(i) += pos 'adjusts pos
            GL.Vertex2(verts(i))
        Next
        GL.End()
    End Sub

    Public Shared Sub begin(screenWidth, screenHeight)
        GL.MatrixMode(MatrixMode.Projection)
        GL.LoadIdentity()
        'Sets up coordinate system on drawing canvas
        GL.Ortho(-screenWidth / 2, screenWidth / 2, screenHeight / 2, -screenHeight / 2, 0, 1)
    End Sub



End Class
