Imports System.IO
Imports OpenTK.Audio
Imports OpenTK.Audio.OpenAL
Imports OpenTK.Input
Imports VB_Game

''' <summary>
''' Manages all game audio and the audio context
''' </summary>
Public Class AudioMaster : Implements IDisposable

    Private buffers As List(Of Integer)
    Private context As AudioContext
    Private source As Source
    Private Shared instance As AudioMaster

    Public Sub New()
        context = New AudioContext()
        context.MakeCurrent()
        setListenerData()

        'Dim buffer = ContentPipe.loadWave(".\res\test.wav")
        'Dim buffer2 = ContentPipe.loadWave(".\res\test2.wav")
        'source = New Source()
        'source.shouldLoop = True
        'Dim source2 As New Source()
        'source2.shouldLoop = True

        'source.play(buffer)
        'source2.play(buffer2)
    End Sub

    Public Shared Function getInstance() As AudioMaster
        If instance Is Nothing Then
            instance = New AudioMaster()
        End If
        Return instance
    End Function

    Public Sub setListenerData()
        AL.Listener(ALListener3f.Position, 0, 0, 0)
        AL.Listener(ALListener3f.Velocity, 0, 0, 0)
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        context.Dispose()
    End Sub
End Class
