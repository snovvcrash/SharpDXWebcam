SharpDXWebcam
=============

This project is a C# port of [Get-DXWebcamVideo.ps1](https://github.com/xorrior/RandomPS-Scripts/blob/master/Get-DXWebcamVideo.ps1) PowerShell script (by [@xorrior](https://twitter.com/xorrior) and [@sixdub](https://twitter.com/sixdub)) which utilizes the DirectX and DShowNET assemblies to record video from the host's webcam.

All credit for the DirectX.Capture and DShowNET libraries goes to the original authors:

- **DirectX.Capture** - [@Brian-Low](https://www.codeproject.com/script/Membership/View.aspx?mid=89875) - [DirectX.Capture Class Library](https://www.codeproject.com/Articles/3566/DirectX-Capture-Class-Library)
- **DShowNET** - [DirectShowNet library](https://directshownet.sourceforge.net/)

> This project is intended for security specialists operating under a contract; all information provided in it is for educational purposes only. The authors cannot be held liable for any damages caused by improper usage of any of the related projects and/or appropriate security tooling. Distribution of malware, disruption of systems, and violation of secrecy of correspondence are prosecuted by law.

## Help

```console
C:\SharpDXWebcam> SharpDXWebcam.exe --help

   ______                 ___  _  ___      __    __
  / __/ /  ___ ________  / _ \| |/_/ | /| / /__ / /  _______ ___ _
 _\ \/ _ \/ _ `/ __/ _ \/ // />  < | |/ |/ / -_) _ \/ __/ _ `/  ' \
/___/_//_/\_,_/_/ / .__/____/_/|_| |__/|__/\__/_.__/\__/\_,_/_/_/_/
                 /_/

  -r, --RecordTime                (Default: 5) Amount of time to record in seconds. It takes 1-2 seconds for the video
                                  to open. Defaults to 5.
  -p, --Path                      File path to save the recorded output. Defaults to the current user's APPDATA
                                  directory. The output format is AVI.
  -v, --VideoInputIndex           (Default: 0) The index of the video input device to use. Default = 0 (first device).
  -a, --AudioInputIndex           (Default: 0) The index of the audio input device to use. Default = 0 (first device).
  -c, --VideoCompressorPattern    The pattern to use to find the name of the preferred video compressor.
  -d, --AudioCompressorPattern    The pattern to use to find the name of the preferred audio compressor.
  -f, --FrameRate                 (Default: 7) The frame rate to use when capturing video. Default = 7.
  --help                          Display this help screen.
```

## Demo

![demo.png](/assets/demo.png)

## Credits

- Brian Low ([@Brian-Low](https://www.codeproject.com/script/Membership/View.aspx?mid=89875))
- Chris Ross ([@xorrior](https://twitter.com/xorrior))
- Justin Warner ([@sixdub](https://twitter.com/sixdub))
