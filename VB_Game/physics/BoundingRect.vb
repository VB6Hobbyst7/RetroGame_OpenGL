Imports OpenTK
''' <summary>
''' Rectangle bounding objects
''' </summary>
Public Class BoundingRect

    Private _size As OpenTK.Vector2
    Public Property size() As OpenTK.Vector2
        Get
            Return _size
        End Get
        Set(ByVal value As OpenTK.Vector2)
            _size = value
        End Set
    End Property

    Private _pos As OpenTK.Vector2
    Public Property pos() As OpenTK.Vector2
        Get
            Return _pos
        End Get
        Set(ByVal value As OpenTK.Vector2)
            _pos = value
        End Set
    End Property

    Public Sub New(size As Vector2, pos As Vector2)
        Me.size = size
        Me.pos = pos
    End Sub
End Class
