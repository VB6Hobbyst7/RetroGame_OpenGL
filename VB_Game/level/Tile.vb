''' <summary>
''' A single tile in the tile map grid
''' </summary>
Public Class Tile : Inherits GameObject : Implements ICloneable

    Private name As String

    Public Function getName() As String
        Return name
    End Function

    Public Sub New(name As String)
        Me.name = name
    End Sub

    Public Overrides Sub render()
        MyBase.render()
    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        Return Me.MemberwiseClone()
    End Function
End Class
