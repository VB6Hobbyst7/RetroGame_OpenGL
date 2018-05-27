Imports OpenTK

''' <summary>
''' Represents an on screen game object
''' </summary>
Public Class Entity : Inherits GameObject

#Region "Member Variables"

    Private _velocity As Vector2
    Public Property velocity() As Vector2
        Get
            Return _velocity
        End Get
        Set(ByVal value As Vector2)
            _velocity = value
        End Set
    End Property

#End Region

    Public Sub New(pos As Vector2, texture As Texture)
        Me.pos = pos
        Me.texture = texture
        Me.velocity = New Vector2(0, 0)
    End Sub

    Public Sub tick(delta As Decimal)
        'Update position based on current velocity
        pos = New Vector2(pos.X + velocity.X * delta, pos.Y + velocity.Y * delta)
    End Sub

End Class