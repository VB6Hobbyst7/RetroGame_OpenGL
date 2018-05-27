Imports System.Threading
Imports OpenTK
Imports OpenTK.Audio.OpenAL

Public Class Source

#Region "Member Variables"
    Private id As Integer

    Private _gain As Decimal
    Public Property gain() As Decimal
        Get
            Return _gain
        End Get
        Set(ByVal value As Decimal)
            _gain = value
            AL.Source(id, ALSourcef.Gain, _gain)
        End Set
    End Property

    Private _pitch As Decimal
    Public Property pitch() As Decimal
        Get
            Return _pitch
        End Get
        Set(ByVal value As Decimal)
            _pitch = value
            AL.Source(id, ALSourcef.Pitch, _pitch)
        End Set
    End Property

    Private _pos As Vector3
    Public Property pos() As Vector3
        Get
            Return _pos
        End Get
        Set(ByVal value As Vector3)
            _pos = value
            AL.Source(id, ALSource3f.Position, pos.X, pos.Y, pos.Z)
        End Set
    End Property

    Private _shouldLoop As Boolean
    Public Property shouldLoop() As Boolean
        Get
            Return _shouldLoop
        End Get
        Set(ByVal value As Boolean)
            _shouldLoop = value
            AL.Source(id, ALSourceb.Looping, shouldLoop)
        End Set
    End Property
#End Region

    Public Sub New()
        id = AL.GenSource()
        gain = 1
        Dim w As Single
        AL.GetSource(id, ALSourcef.Gain, w)
        Debug.WriteLine(w)
        pitch = 1
        pos = New Vector3(0, 0, 0)
        shouldLoop = False
        Debug.WriteLine(String.Format("Source: {0}", id))
    End Sub

    Public Sub play(buffer As Integer)
        Debug.WriteLine("play")
        AL.Source(id, ALSourcei.Buffer, buffer)
        AL.SourcePlay(id)
    End Sub

End Class
