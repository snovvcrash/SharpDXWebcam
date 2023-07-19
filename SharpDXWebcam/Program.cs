// Authors: Chris Ross (@xorrior), Justin Warner (@sixdub), @snovvcrash
// Based on: https://github.com/xorrior/RandomPS-Scripts/blob/master/Get-DXWebcamVideo.ps1
// License: BSD 3-Clause

// ALL CREDIT FOR THE 'DirectX.Capture' AND 'DShowNET' PROJECTS GOES TO THE ORIGINAL AUTHORS

// 'DirectX.Capture'
// Author: Brian Low
// CodeProject User: @Brian-Low
// Link: http://www.codeproject.com/Articles/3566/DirectX-Capture-Class-Library
// License: Public Domain

// 'DShowNET'
// Author: Unknown
// Link: http://directshownet.sourceforge.net/
// License: GNU Lesser General Public License

using System;
using System.IO;
using System.Collections.Generic;

using DirectX.Capture;
using CommandLine;
using CommandLine.Text;

namespace SharpDXWebcam
{
    public class DXWebcamVideoCapture
    {
        public class Options
        {
            [Option('r', "RecordTime", Default = 5, HelpText = "Amount of time to record in seconds. It takes 1-2 seconds for the video to open. Defaults to 5.")]
            public int RecordTime { get; set; }

            [Option('p', "Path", HelpText = "File path to save the recorded output. Defaults to the current user's APPDATA directory. The output format is AVI.")]
            public string Path { get; set; }

            [Option('v', "VideoInputIndex", Default = 0, HelpText = "The index of the video input device to use. Default = 0 (first device).")]
            public int VideoInputIndex { get; set; }

            [Option('a', "AudioInputIndex", Default = 0, HelpText = "The index of the audio input device to use. Default = 0 (first device).")]
            public int AudioInputIndex { get; set; }

            [Option('c', "VideoCompressorPattern", HelpText = "The pattern to use to find the name of the preferred video compressor.")]
            public string VideoCompressorPattern { get; set; }

            [Option('d', "AudioCompressorPattern", HelpText = "The pattern to use to find the name of the preferred audio compressor.")]
            public string AudioCompressorPattern { get; set; }

            [Option('f', "FrameRate", Default = 7, HelpText = "The frame rate to use when capturing video. Default = 7.")]
            public int FrameRate { get; set; }
        }

        public static void Main(string[] args)
        {
            var parser = new Parser(with => with.HelpWriter = null);
            var parserResult = parser.ParseArguments<Options>(args);

            try
            {
                parserResult
                .WithParsed(options =>
                    GetDXWebcamVideo(
                        options.RecordTime,
                        options.Path,
                        options.VideoInputIndex,
                        options.AudioInputIndex,
                        options.VideoCompressorPattern,
                        options.AudioCompressorPattern,
                        options.FrameRate))
                .WithNotParsed(errs => DisplayHelp(parserResult, errs));
            }
            catch (Exception e)
            {
                Console.WriteLine($"[-] {e.Message}");
            }
        }

        static void DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errors)
        {
            var helpText = HelpText.AutoBuild(
                result,
                h =>
                {
                    h.AdditionalNewLineAfterOption = false;
                    h.Heading = @"
   ______                 ___  _  ___      __    __
  / __/ /  ___ ________  / _ \| |/_/ | /| / /__ / /  _______ ___ _
 _\ \/ _ \/ _ `/ __/ _ \/ // />  < | |/ |/ / -_) _ \/ __/ _ `/  ' \
/___/_//_/\_,_/_/ / .__/____/_/|_| |__/|__/\__/_.__/\__/\_,_/_/_/_/
                 /_/                                               ";
                    h.AutoVersion = false;
                    h.MaximumDisplayWidth = 120;
                    h.Copyright = "";
                    return HelpText.DefaultParsingErrorsHandler(result, h);
                },
                e => e);

            Console.WriteLine(helpText);
        }

        static Filter GetDXAudioInput(int index)
        {
            Filters filters = new Filters();
            if (filters.AudioInputDevices != null && filters.AudioInputDevices.Count > index)
                return filters.AudioInputDevices[index];

            Console.WriteLine("[!] There are no audio inputs");
            return null;
        }

        static Filter GetDXVideoInput(int index)
        {
            Filters filters = new Filters();
            if (filters.VideoInputDevices != null && filters.VideoInputDevices.Count > index)
                return filters.VideoInputDevices[index];

            Console.WriteLine("[!] There are no video inputs");
            return null;
        }

        static Filter GetDXAudioCompression(string pattern)
        {
            Filters filters = new Filters();
            foreach (Filter compressor in filters.AudioCompressors)
                if (compressor.Name.Contains(pattern))
                    return compressor;

            Console.WriteLine("[!] Audio compression not available");
            return null;
        }

        static Filter GetDXVideoCompression(string pattern)
        {
            Filters filters = new Filters();
            foreach (Filter compressor in filters.VideoCompressors)
                if (compressor.Name.Contains(pattern))
                    return compressor;

            Console.WriteLine("[!] Video compression not available");
            return null;
        }

        static void GetDXWebcamVideo(int recordTime = 5, string path = null, int videoInputIndex = 0, int audioInputIndex = 0, string videoCompressorPattern = null, string audioCompressorPattern = null, int frameRate = 7)
        {
            Filter videoInput = GetDXVideoInput(videoInputIndex);
            if (videoInput == null)
            {
                Console.WriteLine("[!] Failed to set video input device");
                return;
            }
            Console.WriteLine($"[*] Selected video device: {videoInput.Name}");

            Filter audioInput = GetDXAudioInput(audioInputIndex);
            if (audioInput == null)
            {
                Console.WriteLine("[!] Failed to set audio input device");
                return;
            }
            Console.WriteLine($"[*] Selected audio device: {audioInput.Name}");

            string outputFilePath = path ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "out.avi");
            Capture capture = new Capture(videoInput, audioInput)
            {
                Filename = outputFilePath
            };

            if (!string.IsNullOrEmpty(videoCompressorPattern))
            {
                try
                {
                    Filter videoCompression = GetDXVideoCompression(videoCompressorPattern);
                    if (videoCompression != null)
                    {
                        Console.WriteLine($"[*] Selected video compression: {videoCompression.Name}");
                        capture.VideoCompressor = videoCompression;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"[!] Failed to set video compression: {e.Message}");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(audioCompressorPattern))
            {
                try
                {
                    Filter audioCompression = GetDXAudioCompression(audioCompressorPattern);
                    if (audioCompression != null)
                    {
                        Console.WriteLine($"[*] Selected audio compression: {audioCompression.Name}");
                        capture.AudioCompressor = audioCompression;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"[!] Failed to set audio compression: {e.Message}");
                    return;
                }
            }

            capture.FrameRate = frameRate;
            Console.WriteLine($"[*] Frame rate set to {frameRate}");

            try
            {
                capture.Start();
            }
            catch (Exception e)
            {
                capture.Stop();
                Console.WriteLine($"[!] Failed to start video capture: {e.Message}");
                return;
            }

            Console.WriteLine($"[*] Capture started. Sleeping for {recordTime}s ...");
            System.Threading.Thread.Sleep(recordTime * 1000);

            capture.Stop();

            FileInfo recordedFile = new FileInfo(outputFilePath);
            Console.WriteLine($"[+] Capture completed: {recordedFile.FullName}");
        }
    }
}
