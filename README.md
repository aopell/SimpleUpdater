# SimpleUpdater
Very small program which updates individual files by providing it a download link and a save location. Made for updating exe files for some programs that I have written.

## How To Use
Open the file with two arguments, `DownloadLink` and `SaveLocation`

### Example:

`SimpleUpdater.exe http://example.com/MyProgramBinary.exe C:\Users\User\Downloads\MyProgramBinary.exe`

C#:
```C#
System.Net.WebClient.DownloadFile("https://github.com/aopell/SimpleUpdater/releases/download/v1.1/SimpleUpdater.exe", System.IO.Path.GetTempPath() + "\\SimpleUpdater.exe");
System.Diagnostics.Process.Start(System.IO.Path.GetTempPath() + "\\SimpleUpdater.exe", "http://example.com/MyProgramBinary.exe <Your Application's Path Here>");
Application.Exit();
```

SimpleUpdater will automatically download your new version, replace your old version, and launch your newly updated program
