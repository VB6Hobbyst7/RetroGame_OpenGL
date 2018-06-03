Public Class DebugHandler

    Private Shared frame As Integer = 0
    Private Shared fpsLabel As New TextLabel(String.Format("FPS: {0}", 0), New OpenTK.Vector2(-1280 / 2, -960 / 2))

    Public Shared Sub update(time As Double)
        If Constants.FPS_DEBUG Then
            frame += 1

            If frame = 60 Then
                'Debug.WriteLine(String.Format("FPS: {0}", Math.Round(totalFPS / frame)))
                fpsLabel.Text = String.Format("FPS: {0}", CInt(1 / time))
                frame = 0
            End If
        End If
    End Sub

    Public Shared Sub render(delta As Double)
        fpsLabel.render(delta)
    End Sub

End Class
