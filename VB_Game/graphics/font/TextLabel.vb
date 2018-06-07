Imports System.Drawing
Imports System.Windows.Forms
''' <summary>
''' Represents a text string drawn on screen
''' </summary>
Public Class TextLabel : Inherits GameObject

    Private refreshingTexture As Boolean = False

    Private _size As Integer
    Public Property size() As Integer
        Get
            Return _size
        End Get
        Set(ByVal value As Integer)
            _size = value
            genTexture()
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

    Public Sub New(text As String, pos As OpenTK.Vector2)
        MyBase.New(True)
        Me.pos = pos
        _text = text
        genTexture()
    End Sub

    Private Sub genTexture()
        If refreshingTexture Then
            OpenTK.Graphics.OpenGL.GL.DeleteTexture(CType(texture, ImageTexture).id)
        End If
        refreshingTexture = True
        'Dim texture As New ImageTexture()
        Dim f As Font = New Font("Arial", 32, FontStyle.Regular)
        Dim TestSize As Size = TextRenderer.MeasureText(_text, f)
        Dim bitmap As New Bitmap(TestSize.Width, TestSize.Height)
        Dim g As Graphics = Graphics.FromImage(bitmap)
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        g.DrawString(_text, f, Brushes.Black, 0, 0)
        Me.texture = ContentPipe.loadTexture(bitmap)
    End Sub

End Class
