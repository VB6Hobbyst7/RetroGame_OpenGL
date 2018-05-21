Public Class Texture2d

    Private _id As Integer
    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _width As Integer
    Public Property width() As Integer
        Get
            Return _width
        End Get
        Set(ByVal value As Integer)
            _width = value
        End Set
    End Property

    Private _height As Integer
    Public Property height() As Integer
        Get
            Return _height
        End Get
        Set(ByVal value As Integer)
            _height = value
        End Set
    End Property

    Public Sub New(id As Integer, width As Integer, height As Integer)
        Me.id = id
        Me.width = width
        Me.height = height
    End Sub
End Class
