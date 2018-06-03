''' <summary>
''' Represents a body on which physics simulations are run on
''' </summary>
Public Class RigidBody

    Public ReadOnly Property pos() As OpenTK.Vector2
        Get
            Return parent.pos
        End Get
    End Property

    Public ReadOnly Property width() As Integer
        Get
            Return parent.getWidth()
        End Get
    End Property

    Public ReadOnly Property height() As Integer
        Get
            Return parent.getHeight()
        End Get
    End Property

    Private _parent As GameObject
    Public Property parent() As GameObject
        Get
            Return _parent
        End Get
        Set(ByVal value As GameObject)
            _parent = value
        End Set
    End Property

    ''' <summary>
    ''' Bit mask for what can collide with this object
    ''' </summary>
    Public categoryBitMask As Integer
    ''' <summary>
    ''' Bit mask for what this object can collide with
    ''' </summary>
    Public collisionBitMask As Integer


    Public Sub New(parent As GameObject, categoryBitMask As Integer, collisionBitMask As Integer)
        Me.parent = parent
        Me.categoryBitMask = categoryBitMask
        Me.collisionBitMask = collisionBitMask
    End Sub

End Class
