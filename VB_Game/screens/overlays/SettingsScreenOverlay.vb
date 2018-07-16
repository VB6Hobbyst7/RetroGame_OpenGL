Public Class SettingsScreenOverlay

    Delegate Sub onBackDelegate()
    Private overlayBackground As ShapeTexture
    Private pos As OpenTK.Vector2
    Private titleLabel As TextLabel
    Private Const paddingY As Integer = 32
    Private onBack As onBackDelegate
    Private backBtn As Button
    Private btnStyle As New ButtonStyle(Drawing.Brushes.White, Drawing.Color.FromArgb(255, 64, 64, 64))
    Private mainFont As New Drawing.Font("Impact", 24 * Constants.DESIGN_SCALE_FACTOR, Drawing.FontStyle.Regular)
    Private renderOverlay As Boolean = True
    Private volumeSlider As Slider
    Private volumeLabel As TextLabel
    Private volumeTitleLabel As TextLabel
    Dim controlSize As New OpenTK.Vector2(280 * Constants.DESIGN_SCALE_FACTOR, 44 * Constants.DESIGN_SCALE_FACTOR)

    Public Sub New()
        pos = New OpenTK.Vector2(-Constants.DESIGN_WIDTH / 2, -Constants.DESIGN_HEIGHT / 2)
        overlayBackground = New ShapeTexture(Constants.DESIGN_WIDTH, Constants.DESIGN_HEIGHT,
            Drawing.Color.FromArgb(200, 0, 0, 0), ShapeTexture.ShapeType.Rectangle)

        titleLabel = New TextLabel("Settings", New Drawing.Font("Impact", 32 * Constants.DESIGN_SCALE_FACTOR, Drawing.FontStyle.Regular),
                                  Drawing.Brushes.White)
        titleLabel.pos = New OpenTK.Vector2(-titleLabel.getWidth() / 2, pos.Y + paddingY)

        Dim btnSize As New Drawing.Size(250 * Constants.DESIGN_SCALE_FACTOR, 45 * Constants.DESIGN_SCALE_FACTOR)

        backBtn = New Button("BACK", New OpenTK.Vector2(-btnSize.Width / 2,
                Constants.DESIGN_HEIGHT / 2 - btnSize.Height * 1.5), mainFont, btnSize, btnStyle)
        backBtn.setOnClickListener(AddressOf onBackClicked)

        'Volume settings controls
        volumeSlider = New Slider(New OpenTK.Vector2(-controlSize.X / 5,
                                                     titleLabel.pos.Y + titleLabel.getHeight() * 3), controlSize)
        volumeSlider.setOnValueChangeListener(AddressOf onVolumeChanged)
        volumeLabel = New TextLabel("0", mainFont, Drawing.Brushes.White)
        volumeLabel.pos = New OpenTK.Vector2(volumeSlider.pos.X + volumeSlider.getWidth() + volumeLabel.getWidth() / 4,
                                             volumeSlider.pos.Y)
        volumeTitleLabel = New TextLabel("Volume", mainFont, Drawing.Brushes.White)
        volumeTitleLabel.pos = New OpenTK.Vector2(volumeSlider.pos.X - volumeSlider.getWidth() / 8 - volumeTitleLabel.getWidth(),
                                             volumeSlider.pos.Y)
    End Sub

    Public Sub onVolumeChanged(val As Object)
        volumeLabel.Text = CStr(val)
    End Sub

    Public Sub New(renderOverlay As Boolean)
        Me.New()
        Me.renderOverlay = renderOverlay
    End Sub

    Public Sub setOnBackAction(backAction As onBackDelegate)
        onBack = backAction
    End Sub

    Private Sub onBackClicked()
        If Not onBack Is Nothing Then
            onBack.Invoke()
        End If
    End Sub

    Public Sub render(delta)
        If renderOverlay Then
            SpriteBatch.drawTexture(overlayBackground, pos)
        End If
        titleLabel.render(delta)
        backBtn.render(delta)
        volumeSlider.render(delta)
        volumeLabel.render(delta)
        volumeTitleLabel.render(delta)
    End Sub

    Public Sub tick(delta)
    End Sub

End Class
