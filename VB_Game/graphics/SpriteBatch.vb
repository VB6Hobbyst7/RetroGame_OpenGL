Imports OpenTK
Imports OpenTK.Graphics.OpenGL
Imports System.Drawing

Public Class SpriteBatch

    Private Shared currentScale As New Vector2(1, 1)
    Private Shared lastScaleX As Double = 0.0
    Private Shared margin_x As Single = 0.0
    Private Shared margin_y As Single = 0.0

#Region "RectDrawing"

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
        GL.Color4(color)
        For i = 0 To 3
            GL.TexCoord2(verts(i))
            verts(i).X *= texture.width 'Adjusts coordinates to match up with width of texture
            verts(i).Y *= texture.height 'Adjusts coordinates to match up with height of texture
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
        GL.Color4(color)
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
        drawTexture(texture, pos, New Vector2(1, 1))
    End Sub

    Public Shared Sub drawControl(control As Control)
        drawTexture(control.texture, control.pos, control.scale)
    End Sub

    Public Shared Sub drawTexture(texture As Texture, pos As Vector2, scale As Vector2)
        If texture.GetType.IsAssignableFrom(GetType(ImageTexture)) Then
            drawImage(CType(texture, ImageTexture), pos, scale)
        ElseIf texture.GetType.IsAssignableFrom(GetType(ShapeTexture)) Then
            Dim shapeTexture As ShapeTexture = CType(texture, ShapeTexture)

            Select Case shapeTexture.shape
                Case ShapeTexture.ShapeType.Ellipse

                Case ShapeTexture.ShapeType.Rectangle
                    drawRect(New Vector2(shapeTexture.width * scale.X, shapeTexture.height * scale.Y), pos, shapeTexture.color)
            End Select

        End If
    End Sub

    ''' <summary>
    ''' Draws a game object
    ''' </summary>
    ''' <param name="obj"></param>
    Public Shared Sub drawObject(obj As GameObject)
        drawTexture(obj.texture, obj.pos, obj.scale)
    End Sub

    ''' <summary>
    ''' Draws game object applying a scale
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <param name="scale"></param>
    Public Shared Sub drawObject(obj As GameObject, scale As Vector2)
        drawTexture(obj.texture, obj.pos, scale)
    End Sub

#End Region

#Region "Circle Drawing"
    Public Shared Sub drawCircle(radius As Double, pos As Vector2)
        GL.Disable(EnableCap.Texture2D)
        GL.Begin(PrimitiveType.TriangleFan)
        GL.Color4(Color.HotPink)
        Dim triangleAmount = Math.Max(Math.Round(
            radius / Constants.GRAPHICS_TRIANGLE_RADIUS_RATIO), Constants.MIN_TRIANGLES_CIRCLE)
        For i = 0 To triangleAmount - 1
            Dim vertex = New Vector2(pos.X + (radius * Math.Cos(i * ((2 * Math.PI) / triangleAmount))),
                pos.Y + (radius * Math.Sin(i * ((2 * Math.PI) / triangleAmount))))
            vertex *= currentScale
            GL.Vertex2(vertex)
        Next
        GL.End()
    End Sub
#End Region

#Region "Text Drawing"
    Public Shared Sub drawText(label As TextLabel)
        Dim verts = New Vector2() {
            New Vector2(0, 0),
            New Vector2(1, 0),
            New Vector2(1, 1),
            New Vector2(0, 1)
        }
        GL.Enable(EnableCap.Texture2D)

        If lastScaleX <> currentScale.X Then
            label.fontSize = (label.designFontSize * currentScale.X)
            label.scale = New Vector2(label.OriginSize.X / label.texture.width,
                                      label.OriginSize.Y / label.texture.height)
        End If


        Dim texture = CType(label.texture, ImageTexture)
        GL.BindTexture(TextureTarget.Texture2D, texture.id)
        GL.Begin(PrimitiveType.Quads)
        GL.Color4(Color.White)
        For i = 0 To 3
            GL.TexCoord2(verts(i))
            verts(i).X *= texture.width 'Adjusts coordinates to match up with width of texture
            verts(i).Y *= texture.height 'Adjusts coordinates to match up with height of texture
            verts(i) *= label.scale 'Adjusts size for increased quality
            verts(i) += label.pos 'adjusts pos
            verts(i) *= currentScale
            GL.Vertex2(verts(i))
        Next
        GL.End()
        lastScaleX = currentScale.X
    End Sub
#End Region

#Region "Projection Handling"

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

        margin_x = (screenWidth - width) / 2
        margin_y = (screenHeight - height) / 2
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

        currentScale.X = screenWidth / Constants.DESIGN_WIDTH
        currentScale.Y = screenHeight / Constants.DESIGN_HEIGHT

        'Draws Viewport on Screen
        'drawRect(New Vector2(screenWidth, screenHeight), New Vector2(-screenWidth / 2, -screenHeight / 2), Color.White)
    End Sub

    ''' <summary>
    ''' Returns a normalised representation of screen coords to game coords
    ''' </summary>
    ''' <param name="x"></param>
    ''' <param name="y"></param>
    ''' <returns>Normalised Coords</returns>
    Public Shared Function normaliseScreenCoords(x As Integer, y As Integer) As Vector2
        Return New Vector2(normaliseScreenX(x), normaliseScreenY(y))
    End Function

    ''' <summary>
    ''' Returns a normalised representation of screen coord x to game coord x
    ''' </summary>
    ''' <param name="x"></param>
    ''' <returns>Normalised Coord</returns>
    Public Shared Function normaliseScreenX(x As Integer) As Integer
        Return (x - margin_x) * ((Constants.DESIGN_WIDTH) / (Game.getInstance().Width - margin_x * 2
                    )) - Constants.DESIGN_WIDTH / 2
    End Function


    ''' <summary>
    ''' Returns a normalised representation of screen coord y to game coord y
    ''' </summary>
    ''' <param name="x"></param>
    ''' <returns>Normalised Coord</returns>
    Public Shared Function normaliseScreenY(y As Integer) As Integer
        Return (y - margin_y) * ((Constants.DESIGN_HEIGHT) / (Game.getInstance().Height - margin_y * 2
                    )) - Constants.DESIGN_HEIGHT / 2
    End Function

#End Region

End Class
