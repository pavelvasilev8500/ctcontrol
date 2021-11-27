Dim objWshShell, lc
set objWshShell = WScript.CreateObject("WScript.Shell")
lc = objWshShell.Run("app.exe", 0, false)
set objWshShell = nothing