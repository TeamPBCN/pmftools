using System;
using System.IO;
using VGMToolbox.format;

namespace psmfdump
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("PSP Movie Format (PMF) dumper\nCreated by LITTOMA from Team PB.");
                Console.WriteLine("Usage: psmfdump <PSMF File> [-a|--audio Audio path] [-v|--video Video path]");
                Console.WriteLine("\n\nNOTE: This tool can only works with pmf video containing 1 video and 1 audio stream. Otherwise the output file will crush.");
                return;
            }

            string vp = string.Empty;
            string ap = string.Empty;
            for (int i = 1; i < args.Length; i++)
            {
                var arg = args[i];
                if (arg == "-v" || arg == "--video")
                {
                    vp = args[++i];
                    continue;
                }
                else if (arg == "-a" || arg == "--audio")
                {
                    ap = args[++i];
                    continue;
                }
            }

            DumpPsmf(args[0], vp, ap);
        }

        static void DumpPsmf(string path, string videoPath, string audioPath)
        {
            if (!File.Exists(path))
            {
                return;
            }

            MpegStream.DemuxOptionsStruct demuxOptions = new MpegStream.DemuxOptionsStruct
            {
                ExtractAudio = true,
                ExtractVideo = true,

                AddHeader = true,
                SplitAudioStreams = false,
                AddPlaybackHacks = false,

                videoPath = videoPath,
                audioPath = audioPath
            };

            SonyPmfStream pmfStream = new SonyPmfStream(path);
            pmfStream.DemultiplexStreams(demuxOptions);
        }
    }
}
