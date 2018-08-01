Public Class ImageTexture : Inherits Texture

    Private _id As Integer
    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Public Sub New(id As Integer, width As Integer, height As Integer)
        Me.id = id
        Me.width = width
        Me.height = height
    End Sub
End Class
