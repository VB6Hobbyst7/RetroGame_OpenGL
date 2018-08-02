Imports System.IO
Imports System.Reflection
Imports NAudio.Wave

Public Class AudioPlayer

    Private reader As AudioFileReader
    Private player As IWavePlayer
    Private volume As Single

    ''' <summary>
    ''' Creates new audio player instance for a sound file
    ''' </summary>
    ''' <param name="fileName"></param>
    Public Sub New(fileName As String, Optional volume As Single = 1.0F)
        load(fileName)
    End Sub

    Public Sub play()
        reader.CurrentTime = TimeSpan.Zero
        player.Play()
    End Sub

    Private Sub load(fileName As String)
        reader = New AudioFileReader(Constants.AUDIO_RES_DIR + fileName)
        player = New WaveOutEvent()
        player.Init(reader)
    End Sub

    Public Sub setVolume(volume As Single)
        Me.player.Volume = volume
    End Sub

End Class

Public Class SoundEffects
    Public Shared bounce As New AudioPlayer("bounce.wav")
    Public Shared chest As New AudioPlayer("chest.wav")
    Public Shared hit_enemy As New AudioPlayer("hit_enemy.wav")
    Public Shared menu_navigate As New AudioPlayer("menu_navigate.wav")
    Public Shared pause_in As New AudioPlayer("pause_in.wav")
    Public Shared pause_out As New AudioPlayer("pause_out.wav")
    Public Shared weapon_fire As New AudioPlayer("weapon_fire.wav")
    Public Shared gameover As New AudioPlayer("gameover.wav")

    Public Shared Sub setVolume(volume As Single)
        Dim properties() As FieldInfo = GetType(SoundEffects).GetFields()
        For i = 0 To properties.Count - 1
            Dim s = properties(i).GetValue(Nothing)
            Dim audioObj As AudioPlayer = CType(properties(i).GetValue(Nothing), AudioPlayer)
            audioObj.setVolume(volume)
        Next
    End Sub
End Class
