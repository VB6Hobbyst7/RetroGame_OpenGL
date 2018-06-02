Public Class DebugHandler

    Private Shared frame As Integer = 0
    Private Shared totalFPS As Double = 0

    Public Shared Sub update(time As Double, renderFrequency As Double)
        If Constants.FPS_DEBUG Then
            frame += 1
            totalFPS += renderFrequency

            If frame = 60 Then
                Debug.WriteLine(String.Format("FPS: {0}", Math.Round(totalFPS / frame)))
                frame = 0
                totalFPS = 0
            End If
        End If
    End Sub

End Class
