Imports System.Drawing
Imports System.Windows.Forms
''' <summary>
''' Represents a text string drawn on screen
''' </summary>
Public Class TextLabel : Inherits GameObject

    Private refreshingTexture As Boolean = False
    Private Const DEFAULT_FONTSIZE = 32
    Private Shared DEFAULT_COLOR = Brushes.Black

#Region "Members"

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
        MyBase.New(True)
        _text = text
        Me.designFontSize = fontSize
        Me.fontSize = fontSize
        genTexture()
    End Sub

    Public Sub New(text As String, fontSize As Integer, brush As Brush)
        MyBase.New(True)
        _text = text
        Me.designFontSize = fontSize
        Me.fontSize = fontSize
        Me.brush = brush
        genTexture()
    End Sub

    Public Sub New(text As String, font As Font, brush As Brush)
        MyBase.New(True)
        _text = text
        Me.font = font
        Me.designFontSize = font.Size
        Me.fontSize = font.Size
        Me.brush = brush
        genTexture()
    End Sub

#End Region

    ''' <summary>
    ''' Generates new font texture
    ''' </summary>
    Private Sub genTexture()
        If refreshingTexture Then
            OpenTK.Graphics.OpenGL.GL.DeleteTexture(CType(texture, ImageTexture).id)
        End If
        refreshingTexture = True
        'Dim texture As New ImageTexture()
        Dim f As Font = font
        If f Is Nothing Then
            f = New Font("Arial", fontSize, FontStyle.Regular)
        End If

        Dim TestSize As Size = TextRenderer.MeasureText(_text, f)
        Dim bitmap As New Bitmap(TestSize.Width, TestSize.Height)
        Dim g As Graphics = Graphics.FromImage(bitmap)
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        g.DrawString(_text, f, brush, 0, 0)
        Me.texture = ContentPipe.loadTexture(bitmap)
    End Sub

    Private Function findSuitableSize(boundingSize As Size) As Font
        Dim lastWorkingFont As New Font("Arial", 1, FontStyle.Regular)
        Dim curSize = 1
        Dim testFont As Font
        While True
            testFont = New Font("Arial", curSize, FontStyle.Regular)
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
