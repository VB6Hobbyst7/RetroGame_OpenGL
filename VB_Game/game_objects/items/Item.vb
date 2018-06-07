Public Class Item

    Protected holder As Player

    Public Sub New(holder As Player)
        Me.holder = holder
    End Sub

    Public Overridable Sub useItem()
    End Sub

    Public Overridable Sub update(delta As Double)
    End Sub

End Class
