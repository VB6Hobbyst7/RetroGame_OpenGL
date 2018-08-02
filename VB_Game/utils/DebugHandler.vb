Public Class DebugHandler

    Private Shared frame As Integer = 0
    Private Shared fpsLabel As New TextLabel("FPS", 16 * Constants.DESIGN_SCALE_FACTOR)
    Private Shared updateTime = 0
    Private Shared minFPS = Integer.MaxValue
    Private Shared maxFPS = 0
    Private Shared totalFPS = 0
    Private Shared count = 0

    Public Shared Sub init()
        fpsLabel.pos = New OpenTK.Vector2(-Constants.DESIGN_WIDTH / 2 + (50 * Constants.DESIGN_SCALE_FACTOR),
                                          -Constants.DESIGN_HEIGHT / 2)
    End Sub

    Public Shared Sub update(time As Double)
        If Constants.FPS_DEBUG Then
            updateTime += time

            If updateTime >= 1 Then
                Dim fps = CInt(1 / time)
                totalFPS += fps
                count += 1
                minFPS = Math.Min(minFPS, fps)
                maxFPS = Math.Max(maxFPS, fps)
                fpsLabel.Text = String.Format(
"FPS: {0}, Avg {1}
Min {2}, Max {3}", fps, Math.Round(totalFPS / count), minFPS, maxFPS)
                Debug.WriteLine(String.Format(
"FPS: {0}, Avg {1}
Min {2}, Max {3}", fps, Math.Round(totalFPS / count), minFPS, maxFPS))

                updateTime = 0
            End If
        End If
    End Sub

    Public Shared Sub render(delta As Double)
        If Constants.FPS_DEBUG Then
            fpsLabel.render(delta)
        End If
    End Sub

End Class
