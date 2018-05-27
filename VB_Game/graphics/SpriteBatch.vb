Imports OpenTK
Imports OpenTK.Graphics.OpenGL
Imports System.Drawing

Public Class SpriteBatch

    Private Shared currentScale As New Vector2(1, 1)

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
            verts(i) *= currentScale
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
        drawImage(texture, pos, scale, Color.White, Vector2.Zero)
    End Sub

    ''' <summary>
    ''' Draws an image on the screen
    ''' </summary>
    ''' <param name="texture"></param>
    ''' <param name="pos"></param>
    Public Shared Sub drawImage(texture As ImageTexture, pos As Vector2)
        drawImage(texture, pos, New Vector2(1, 1), Color.White, Vector2.Zero)
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
            verts(i) *= currentScale
            GL.Vertex2(verts(i))
        Next
        GL.End()
    End Sub

    Public Shared Sub drawTexture(texture As Texture, pos As Vector2)
        If texture.GetType.IsAssignableFrom(GetType(ImageTexture)) Then
            drawImage(CType(texture, ImageTexture), pos)
        ElseIf texture.GetType.IsAssignableFrom(GetType(ShapeTexture)) Then
            Dim shapeTexture As ShapeTexture = CType(texture, ShapeTexture)

            Select Case shapeTexture.shape
                Case ShapeTexture.ShapeType.Ellipse

                Case ShapeTexture.ShapeType.Rectangle
                    drawRect(New Vector2(shapeTexture.width, shapeTexture.height), pos, shapeTexture.color)
            End Select

        End If
    End Sub

    Public Shared Sub drawObject(obj As GameObject)
        drawTexture(obj.texture, obj.pos)
    End Sub


    ''' <summary>
    ''' Prepares screen for rendering by setting up viewport and projection matricies
    ''' </summary>
    ''' <param name="screenWidth"></param>
    ''' <param name="screenHeight"></param>
    Public Shared Sub begin(screenWidth, screenHeight)
        Dim width = screenWidth
        Dim height = width / Constants.ASPECT_RATIO
        If height > screenHeight Then
            height = screenHeight
            width = height * Constants.ASPECT_RATIO
        End If

        Dim margin_x As Single = (screenWidth - width) / 2
        Dim margin_y As Single = (screenHeight - height) / 2
        Dim vp_width As Integer = width
        Dim vp_height As Integer = height

        GL.Viewport(margin_x, margin_y, vp_width, vp_height)
        GL.MatrixMode(MatrixMode.Projection)
        GL.LoadIdentity()
        'sets up coordinate system on drawing canvas
        GL.Ortho((-screenWidth / 2), (screenWidth / 2),
                 (screenHeight / 2), (-screenHeight / 2), 0, 1)
        GL.MatrixMode(MatrixMode.Modelview)
        GL.LoadIdentity()

        currentScale.X = screenWidth / Constants.INIT_SCREEN_WIDTH
        currentScale.Y = screenHeight / Constants.INIT_SCREEN_HEIGHT

        'Draws Viewport on Screen
        drawRect(New Vector2(screenWidth, screenHeight), New Vector2(-screenWidth / 2, -screenHeight / 2), Color.White)
    End Sub

End Class
