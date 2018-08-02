Imports System.Drawing
Imports System.Windows.Forms
''' <summary>
''' Represents a text string drawn on screen
''' Does software based rendering to a bitmap which is then rendered using OpenGL
''' </summary>
Public Class TextLabel : Inherits GameObject

    Private refreshingTexture As Boolean = False
    Private Const DEFAULT_FONTSIZE = 32
    Private Shared DEFAULT_COLOR = Brushes.Black
    Private Const DEFAULT_FONT_FAMILY = "Impact"

#Region "Members"

    Private _originSize As OpenTK.Vector2
    Public Property OriginSize() As OpenTK.Vector2
        Get
            Return _originSize
        End Get
        Set(ByVal value As OpenTK.Vector2)
            _originSize = value
        End Set
    End Property

    Private _designFontSize As Integer = DEFAULT_FONTSIZE
    Public Property designFontSize() As Integer
        Get
            Return _designFontSize
        End Get
        Set(ByVal value As Integer)
            _designFontSize = value
        End Set
    End Property

    Private _font As Font
    Public Property font() As Font
        Get
            Return _font
        End Get
        Set(ByVal value As Font)
            _font = value
            genTexture()
        End Set
    End Property

    Private _text As String
    Public Property Text() As String
        Get
            Return _text
        End Get
        Set(ByVal value As String)
            _text = value
            genTexture()
        End Set
    End Property

    Private _fontSize As Integer = DEFAULT_FONTSIZE
    Public Property fontSize() As Integer
        Get
            Return _fontSize
        End Get
        Set(ByVal value As Integer)
            _fontSize = value
            If Not font Is Nothing Then
                font = New Font(font.Name, value, font.Style)
            End If
            genTexture()
        End Set
    End Property

    Private _brush As Brush = DEFAULT_COLOR
    Public Property brush() As Brush
        Get
            Return _brush
        End Get
        Set(ByVal value As Brush)
            _brush = value
            genTexture()
        End Set
    End Property

#End Region

#Region "Constructors"

    ''' <summary>
    ''' Creates new textlabel that is the maximum size that can fit in the given bounding box
    ''' </summary>
    ''' <param name="text"></param>
    ''' <param name="boundingSize"></param>
    ''' <param name="brush"></param>
    Public Sub New(text As String, boundingSize As Size, brush As Brush)
        MyBase.New(True)
        _text = text
        font = findSuitableSize(boundingSize)
        Me.designFontSize = font.Size
        Me.fontSize = font.Size
        Me.brush = brush
        genTexture()
    End Sub

    Public Sub New(text As String, fontSize As Integer)
        Me.New(text, New Font(DEFAULT_FONT_FAMILY, fontSize, FontStyle.Regular), Brushes.Black)
    End Sub

    Public Sub New(text As String, fontSize As Integer, brush As Brush)
        Me.New(text, New Font(DEFAULT_FONT_FAMILY, fontSize, FontStyle.Regular), brush)
    End Sub

    Public Sub New(text As String, font As Font, brush As Brush)
        MyBase.New(True)
        _text = text
        Me.font = font
        Me.designFontSize = font.Size
        Me.fontSize = font.Size
        Me.brush = brush
        genTexture()
        Me.OriginSize = New OpenTK.Vector2(Me.getWidth(), Me.getHeight())
    End Sub

#End Region

    ''' <summary>
    ''' Generates new text texture for the text and loads it into application for rendering
    ''' </summary>
    Private Sub genTexture()
        'Me.scale = New OpenTK.Vector2(1 / DEFAULT_UPSCALE, 1 / DEFAULT_UPSCALE)
        If refreshingTexture Then
            OpenTK.Graphics.OpenGL.GL.DeleteTexture(CType(texture, ImageTexture).id)
        End If
        refreshingTexture = True
        'Dim texture As New ImageTexture()
        Dim f As Font = font
        If f Is Nothing Then
            f = New Font("Impact", fontSize, FontStyle.Regular)
        End If

        Dim TestSize As Size = TextRenderer.MeasureText(_text, f)
        Dim bitmap As New Bitmap(TestSize.Width, TestSize.Height)
        Dim g As Graphics = Graphics.FromImage(bitmap)
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        'g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
        g.DrawString(_text, f, brush, 0, 0)
        Me.texture = ContentPipe.loadTexture(bitmap)
    End Sub

    ''' <summary>
    ''' Applies padding in case text gets cut off
    ''' </summary>
    Public Sub applyTextPadding(padding As Integer)
        'Me.scale = New OpenTK.Vector2(1 / DEFAULT_UPSCALE, 1 / DEFAULT_UPSCALE)
        If refreshingTexture Then
            OpenTK.Graphics.OpenGL.GL.DeleteTexture(CType(texture, ImageTexture).id)
        End If
        refreshingTexture = True
        'Dim texture As New ImageTexture()
        Dim f As Font = font
        If f Is Nothing Then
            f = New Font("Impact", fontSize, FontStyle.Regular)
        End If

        Dim TestSize As Size = TextRenderer.MeasureText(_text, f)
        Dim bitmap As New Bitmap(TestSize.Width + padding, TestSize.Height)
        Dim g As Graphics = Graphics.FromImage(bitmap)
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        'g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
        g.DrawString(_text, f, brush, 0, 0)
        Me.texture = ContentPipe.loadTexture(bitmap)
    End Sub

    Private Function findSuitableSize(boundingSize As Size) As Font
        Dim lastWorkingFont As New Font("Impact", 1, FontStyle.Regular)
        Dim curSize = 1
        Dim testFont As Font
        While True
            testFont = New Font("Impact", curSize, FontStyle.Regular)
            Dim size = TextRenderer.MeasureText(_text, testFont)
            If size.Width < boundingSize.Width And size.Height < boundingSize.Height Then
                lastWorkingFont = testFont
            Else
                Exit While
            End If
            curSize += 1
        End While
        Return lastWorkingFont
    End Function

    Public Overrides Sub render(delta As Double)
        SpriteBatch.drawText(Me)
    End Sub

End Class
