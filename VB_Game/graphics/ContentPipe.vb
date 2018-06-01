Imports System.IO
Imports System.Drawing
Imports OpenTK.Graphics.OpenGL
Imports System.Drawing.Imaging
Imports OpenTK.Audio.OpenAL

Public Class ContentPipe

    Private Shared IMG_DIR As String = "res/img/"


    ''' <summary>
    ''' Loads texture into memory under OpenGL Texture
    ''' </summary>
    ''' <param name="img">Bitmap of texture</param>
    ''' <returns></returns>
    Public Shared Function loadTexture(img As Bitmap) As ImageTexture

        Dim id As Integer = GL.GenTexture
        GL.BindTexture(TextureTarget.Texture2D, id)

        Dim data As BitmapData = img.LockBits(
            New Rectangle(0, 0, img.Width, img.Height),
            ImageLockMode.ReadOnly,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb)

        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                      OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0)

        img.UnlockBits(data)

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, TextureWrapMode.ClampToEdge)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, TextureWrapMode.ClampToEdge)

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureMinFilter.Linear)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear)

        Return New ImageTexture(id, img.Width, img.Height)
    End Function

    ''' <summary>
    ''' Loads texture into memory under OpenGL Texture
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Public Shared Function loadTexture(path As String) As ImageTexture
        Return loadTexture(New Bitmap(IMG_DIR + path))
    End Function

    ''' <summary>
    ''' Loads a wav file and places it in buffer
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Public Shared Function loadWave(path As String) As Integer
        Return loadWave(New MemoryStream(File.ReadAllBytes(path)))
    End Function

    ''' <summary>
    ''' Loads a wav file and places it in buffer
    ''' </summary>
    ''' <param name="stream"></param>
    ''' <returns></returns>
    Public Shared Function loadWave(stream As Stream) As Integer
        Dim reader As New BinaryReader(stream)
        Dim signature As String = reader.ReadChars(4)
        If signature <> "RIFF" Then
            Throw New NotSupportedException("Stream is not wave file")
        End If

        Dim chunk_size = New String(reader.ReadChars(4))
        Dim format = New String(reader.ReadChars(4))

        If format <> "WAVE" Then
            Throw New NotSupportedException("Stream is not wave file")
        End If

        Dim format_chunk_size = reader.ReadInt32()
        Dim audio_format = reader.ReadInt16()
        Dim num_channels = 2
        Dim sample_rate = reader.ReadInt32()
        Dim byte_rate = reader.ReadInt32()
        Dim block_align = reader.ReadInt16()
        Dim bits_per_sample = reader.ReadInt16()

        Dim data_signature = New String(reader.ReadChars(4))

        Dim data_chunk_size = reader.ReadInt32()
        Dim read_data = reader.ReadBytes(reader.BaseStream.Length)
        Dim buffer As Integer = AL.GenBuffer()
        AL.BufferData(buffer, GetSoundFormat(num_channels, bits_per_sample), read_data,
                      read_data.Length, sample_rate)
        Debug.WriteLine(String.Format("Buffer: {0}", buffer))
        Return buffer
    End Function

    ''' <summary>
    ''' Determines wave file sound format from number of channels and bits
    ''' </summary>
    ''' <param name="channels"></param>
    ''' <param name="bits"></param>
    ''' <returns></returns>
    Private Shared Function GetSoundFormat(ByVal channels As Integer, ByVal bits As Integer) As ALFormat
        Select Case channels
            Case 1
                Return If(bits = 8, ALFormat.Mono8, ALFormat.Mono16)
            Case 2
                Return If(bits = 8, ALFormat.Stereo8, ALFormat.Stereo16)
            Case Else
                Throw New NotSupportedException("The specified sound format is not supported.")
        End Select
    End Function

End Class
