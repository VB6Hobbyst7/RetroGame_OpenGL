Imports OpenTK

Public Class LevelSelectScreen : Inherits Screen

    Private Shared instance As LevelSelectScreen
    Private backgroundImg As GameObject
    Private backgroundFilter As ShapeTexture
    Private btnFont As New Drawing.Font("Impact", 26 * Constants.DESIGN_SCALE_FACTOR, Drawing.FontStyle.Regular)
    Private titleLabel As TextLabel
    Private btnStyle = New ButtonStyle(Drawing.Brushes.White, Drawing.Color.FromArgb(255, 64, 64, 64))
    Private mapPreviewView As Control

    Private backBtn As Button
    Private mainFont As New Drawing.Font("Impact", 24 * Constants.DESIGN_SCALE_FACTOR, Drawing.FontStyle.Regular)

    Public Sub New()
        'Background image
        backgroundImg = New GameObject(False)
        backgroundImg.texture = ContentPipe.loadTexture(Constants.TILE_RES_DIR + "startscreen_background.png")
        backgroundImg.pos = Constants.TOP_LEFT_COORD
        backgroundImg.scale = New Vector2(Constants.DESIGN_WIDTH / backgroundImg.texture.width)
        backgroundFilter = New ShapeTexture(Constants.DESIGN_WIDTH, Constants.DESIGN_HEIGHT,
                                            Drawing.Color.FromArgb(68, 0, 0, 0), ShapeTexture.ShapeType.Rectangle)

        'Title
        titleLabel = New TextLabel("Map Name", New Drawing.Font("IMPACT", 28 * Constants.DESIGN_SCALE_FACTOR, Drawing.FontStyle.Bold), Drawing.Brushes.White)
        titleLabel.pos = New Vector2(-titleLabel.getWidth() / 2, -Constants.DESIGN_HEIGHT / 2 + titleLabel.getHeight())

        mapPreviewView = New Control()
        'mapPreviewView.texture
    End Sub

    Public Overrides Sub render(delta As Double)
        backgroundImg.render(delta)
        SpriteBatch.drawTexture(backgroundFilter, Constants.TOP_LEFT_COORD)
        titleLabel.render(delta)
    End Sub

    Public Overrides Sub update(delta As Double)
    End Sub

    Public Overrides Sub dispose()
    End Sub

    Public Overrides Sub onResize()
    End Sub

    Public Shared Function getInstance() As LevelSelectScreen
        If instance Is Nothing Then
            instance = New LevelSelectScreen()
        End If
        Return instance
    End Function

End Class
