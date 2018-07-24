Imports OpenTK

Public Class StartScreen : Inherits Screen

    Private settingsOverlay As SettingsScreenOverlay
    Public showSettings As Boolean
    Private Shared instance As StartScreen
    Private startGameBtn As Button
    Private settingsBtn As Button
    Private tutorialBtn As Button
    Private backgroundImg As GameObject
    Private backgroundFilter As ShapeTexture
    Private btnFont As New Drawing.Font("Impact", 26 * Constants.DESIGN_SCALE_FACTOR, Drawing.FontStyle.Regular)
    Private titleLabel As TextLabel
    Private btnStyle = New ButtonStyle(Drawing.Brushes.White, Drawing.Color.FromArgb(255, 64, 64, 64))
    Private testDialog As New Dialog()

    Private Sub startGame()
        Debug.WriteLine("on start game start")
        settingsOverlay.load()
        Game.getInstance().currentScreen = LevelSelectScreen.getInstance()
    End Sub

    Private Sub goToSettings()
        settingsOverlay.load()
        showSettings = True
    End Sub

    Private Sub startTutorial()
        settingsOverlay.load()
        TileMapHandler.getInstance().loadMap(0)
        GameScreen.getInstance().configureTutorial()
        Game.getInstance().currentScreen = GameScreen.getInstance()
    End Sub


    Public Sub New()
        'Background image
        backgroundImg = New GameObject(False)
        backgroundImg.texture = ContentPipe.loadTexture(Constants.TILE_RES_DIR + "startscreen_background.png")
        backgroundImg.pos = Constants.TOP_LEFT_COORD
        backgroundImg.scale = New Vector2(Constants.DESIGN_WIDTH / backgroundImg.texture.width)
        backgroundFilter = New ShapeTexture(Constants.DESIGN_WIDTH, Constants.DESIGN_HEIGHT,
                                            Drawing.Color.FromArgb(68, 0, 0, 0), ShapeTexture.ShapeType.Rectangle)

        'Title
        titleLabel = New TextLabel("GAME NAME", New Drawing.Font("IMPACT", 40 * Constants.DESIGN_SCALE_FACTOR, Drawing.FontStyle.Bold), Drawing.Brushes.White)
        titleLabel.pos = New Vector2(-titleLabel.getWidth() / 2, -titleLabel.getHeight() * 3)
        'Buttons
        Dim btnSize = New Drawing.Size(300 * Constants.DESIGN_SCALE_FACTOR, 45 * Constants.DESIGN_SCALE_FACTOR)

        startGameBtn = New Button("PLAY", New Vector2(-btnSize.Width / 2, titleLabel.pos.Y + titleLabel.getHeight() * 1.5),
                                  btnFont, btnSize, btnStyle)
        startGameBtn.setOnClickListener(AddressOf startGame)

        settingsBtn = New Button("SETTINGS", New Vector2(-btnSize.Width / 2, startGameBtn.pos.Y + startGameBtn.getHeight() * 1.8),
                                  btnFont, btnSize, btnStyle)
        settingsBtn.setOnClickListener(AddressOf goToSettings)

        tutorialBtn = New Button("TUTORIAL", New Vector2(-btnSize.Width / 2, settingsBtn.pos.Y + settingsBtn.getHeight() * 1.8),
                                  btnFont, btnSize, btnStyle)
        tutorialBtn.setOnClickListener(AddressOf startTutorial)

        settingsOverlay = New SettingsScreenOverlay(False)
        settingsOverlay.setOnBackAction(AddressOf onSettingsBack)
    End Sub

    Public Sub onSettingsBack()
        showSettings = False
    End Sub

    Public Overrides Sub render(delta As Double)
        backgroundImg.render(delta)
        SpriteBatch.drawTexture(backgroundFilter, Constants.TOP_LEFT_COORD)
        If showSettings Then
            settingsOverlay.render(delta)
        Else
            startGameBtn.render(delta)
            settingsBtn.render(delta)
            tutorialBtn.render(delta)
            titleLabel.render(delta)
        End If
        testDialog.render(delta)
    End Sub

    Public Overrides Sub update(delta As Double)
        settingsOverlay.tick(delta)
    End Sub

    Public Overrides Sub dispose()
    End Sub

    Public Overrides Sub onResize()
    End Sub

    ''' <summary>
    ''' Function called when this screen is switched to as the current displayed screen
    ''' </summary>
    Public Overrides Sub onShow()
        Debug.WriteLine("Showing start screen")
        settingsOverlay.setOnBackAction(AddressOf onSettingsBack)
    End Sub

    Public Shared Function getInstance() As StartScreen
        Debug.WriteLine("get instance")
        If instance Is Nothing Then
            instance = New StartScreen()
        End If
        Return instance
    End Function

End Class
