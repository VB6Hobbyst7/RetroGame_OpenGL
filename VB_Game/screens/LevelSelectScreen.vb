Imports OpenTK

Public Class LevelSelectScreen : Inherits Screen

    Private Shared instance As LevelSelectScreen
    Private backgroundImg As GameObject
    Private backgroundFilter As ShapeTexture
    Private btnFont As New Drawing.Font("Impact", 26 * Constants.DESIGN_SCALE_FACTOR, Drawing.FontStyle.Regular)
    Private titleLabel As TextLabel
    Private btnStyle = New ButtonStyle(Drawing.Brushes.White, Drawing.Color.FromArgb(255, 64, 64, 64))

    Private mapPreviewView As Control
    Private mapPreviewSize As New Vector2(290 * Constants.DESIGN_SCALE_FACTOR, 215 * Constants.DESIGN_SCALE_FACTOR)
    Private mapNameLabel As TextLabel
    Private highScoreLabel As TextLabel

    Private currentMapChosenIndex As Integer = 0
    Private currentDisplayedMap As Map

    Private startGameBtn As Button
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
        titleLabel = New TextLabel("Select Level", New Drawing.Font("IMPACT",
                30 * Constants.DESIGN_SCALE_FACTOR, Drawing.FontStyle.Bold), Drawing.Brushes.White)

        titleLabel.pos = New Vector2(-titleLabel.getWidth() / 2,
                                     -Constants.DESIGN_HEIGHT / 2 + titleLabel.getHeight() / 15)

        mapPreviewView = New Control()
        mapPreviewView.pos = New Vector2(-mapPreviewSize.X / 2, titleLabel.pos.Y + titleLabel.getHeight() * 2)
        mapNameLabel = New TextLabel("Map Name", New Drawing.Font("IMPACT",
                22 * Constants.DESIGN_SCALE_FACTOR, Drawing.FontStyle.Regular), Drawing.Brushes.White)
        mapNameLabel.pos = New Vector2(-mapNameLabel.getWidth() / 2,
                                     mapPreviewView.pos.Y - mapNameLabel.getHeight() * 1.05)

        highScoreLabel = New TextLabel("Highscore: ", New Drawing.Font("IMPACT",
                24 * Constants.DESIGN_SCALE_FACTOR, Drawing.FontStyle.Regular), Drawing.Brushes.White)
        highScoreLabel.pos = New Vector2(-highScoreLabel.getWidth() / 2,
                                     mapPreviewView.pos.Y + mapPreviewSize.Y + highScoreLabel.getHeight() / 2)

        Dim btnSize As New Drawing.Size(250 * Constants.DESIGN_SCALE_FACTOR, 45 * Constants.DESIGN_SCALE_FACTOR)

        backBtn = New Button("BACK", New OpenTK.Vector2(-btnSize.Width / 2,
                Constants.DESIGN_HEIGHT / 2 - btnSize.Height * 1.5), mainFont, btnSize, btnStyle)
        backBtn.setOnClickListener(AddressOf onBackClicked)

        startGameBtn = New Button("START GAME", New OpenTK.Vector2(-btnSize.Width / 2,
                backBtn.pos.Y - btnSize.Height * 1.5), mainFont, btnSize, btnStyle)
        startGameBtn.setOnClickListener(AddressOf onStartClicked)

        displayMap()
        'mapPreviewView.texture
    End Sub

    'Show map on the left
    Public Sub navLeft()
        currentMapChosenIndex -= 1
        displayMap()
        If currentMapChosenIndex = 0 Then
            'Disable left nav button
        End If
    End Sub

    'Show map on the right
    Public Sub navRight()
        currentMapChosenIndex += 1
        displayMap()
        If currentMapChosenIndex = TileMapHandler.getInstance().maps.Count - 1 Then
            'Disable right nav button
        End If
    End Sub

    Public Sub displayMap()
        Me.currentDisplayedMap = TileMapHandler.getInstance().maps(currentMapChosenIndex)
        mapPreviewView.texture = currentDisplayedMap.getPreviewImg()
        mapPreviewView.scale = New Vector2(mapPreviewSize.X / currentDisplayedMap.getPreviewImg().width,
                                           mapPreviewSize.Y / currentDisplayedMap.getPreviewImg().height)
        mapNameLabel.Text = currentDisplayedMap.getName()
        highScoreLabel.Text = "Highscore: " + CStr(currentDisplayedMap.Highscore)
    End Sub

    Public Overrides Sub render(delta As Double)
        backgroundImg.render(delta)
        SpriteBatch.drawTexture(backgroundFilter, Constants.TOP_LEFT_COORD)
        titleLabel.render(delta)
        mapPreviewView.render(delta)
        mapNameLabel.render(delta)
        highScoreLabel.render(delta)
        startGameBtn.render(delta)
        backBtn.render(delta)
    End Sub

    Public Overrides Sub update(delta As Double)
    End Sub

    Private Sub onBackClicked()
        Game.getInstance().currentScreen = StartScreen.getInstance()
    End Sub

    Private Sub onStartClicked()
        TileMapHandler.getInstance().loadMap(currentMapChosenIndex)
        GameScreen.getInstance().configureNormal()
        Game.getInstance().currentScreen = GameScreen.getInstance()
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

    Public Overrides Sub onShow()
        highScoreLabel.Text = "Highscore: " + CStr(currentDisplayedMap.Highscore)
    End Sub
End Class
