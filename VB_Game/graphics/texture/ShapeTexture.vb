Imports System.Drawing
Imports VB_Game

Public Class ShapeTexture : Inherits Texture

#Region "Member Variables"
    Enum ShapeType
        Rectangle
        Ellipse
    End Enum

    Private _color As Color
    Public Property color() As Color
        Get
            Return _color
        End Get
        Set(ByVal value As Color)
            _color = value
        End Set
    End Property

    Private _shape As ShapeType
    Public Property shape() As ShapeType
        Get
            Return _shape
        End Get
        Set(ByVal value As ShapeType)
            _shape = value
        End Set
    End Property

    Public Sub New(width As Integer, height As Integer, color As Color, shape As ShapeType)
        Me.width = width
        Me.height = height
        Me.color = color
        Me.shape = shape
    End Sub

#End Region



End Class
