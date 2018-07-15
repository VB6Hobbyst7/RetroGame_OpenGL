Module Module1
    Sub Main()
        Constants.loadSettings()
        Dim game = VB_Game.Game.getInstance()
        game.Run()
    End Sub
End Module
