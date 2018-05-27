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
    Public Shared Sub drawImage(texture As ImageTexture, pos As Vector2, scale As Vector2, color As Color, origin As Vector2)
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
    Public Shared Sub drawImage(texture As ImageTexture, pos As Vector2, scale As Vector2)
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
    ''' Draws an image on the screen
    ''' </summary>
    ''' <param name="texture"></param>
    ''' <param name="pos"></param>
    Public Shared Sub drawImage(texture As ImageTexture, pos As Vector2)
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

    Public Shared Sub drawTexture(texture As Texture, pos As Vector2)
        If texture.GetType.IsAssignableFrom(GetType(ImageTexture)) Then
            drawImage(CType(texture, ImageTexture), pos)
        End If
    End Sub

    Public Shared Sub drawObject(obj As GameObject)
        drawTexture(obj.texture, obj.pos)
    End Sub



    Public Shared Sub begin(screenWidth, screenHeight)
        Dim scale_w As Single = CSng(screenWidth) / CSng(Constants.INIT_SCREEN_WIDTH)
        Dim scale_h As Single = CSng(screenHeight) / CSng(Constants.INIT_SCREEN_HEIGHT)
        Dim ar_new As Single = screenWidth / screenHeight
        If ar_new > Constants.ASPECT_RATIO Then
            scale_w = scale_h
        Else
            scale_h = scale_w
        End If

        Dim margin_x As Single = (screenWidth - 1280 * scale_w) / 2
        Dim margin_y As Single = (screenHeight - 720 * scale_h) / 2

        GL.Viewport(margin_x, margin_y, 1280 * scale_w, 720 * scale_h)
        GL.MatrixMode(MatrixMode.Projection)
        GL.LoadIdentity()
        'Sets up coordinate system on drawing canvas
        GL.Ortho((-screenWidth / 2) / Constants.ASPECT_RATIO, (screenWidth / 2) / Constants.ASPECT_RATIO,
                 (screenHeight / 2) / Constants.ASPECT_RATIO, (-screenHeight / 2) / Constants.ASPECT_RATIO, 0, 1)
        GL.MatrixMode(MatrixMode.Modelview)
        GL.LoadIdentity()

        'Draws Viewport on Screen
        drawRect(New Vector2(screenWidth, screenHeight), New Vector2(-screenWidth / 2, -screenHeight / 2), Color.White)
    End Sub

End Class
