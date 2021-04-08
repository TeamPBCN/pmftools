using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using CommandLine;

namespace AutoUsc
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(opts =>
                   {
                       UscProject proj = null;
                       var robot = new UscRobot(opts.Executable);
                       try
                       {
                           robot.Launch();
                           proj = robot.CreateNewProject(opts.ClipName, opts.ClipDescription, opts.ProjectName, opts.ProjectDescription, opts.Output);
                           robot.AddVideoStream(Path.GetFullPath(opts.Video));
                           robot.AddAudioStream(Path.GetFullPath(opts.Audio));
                           robot.Compose(proj);
                       }
                       finally
                       {
                           robot.Close();
                           proj?.Delete();

                       }
                   })
                   .WithNotParsed(errors =>
                   {

                   });
        }
    }

    public class Options
    {
        public const string DefaultExecutablePath = ".\\Umd Stream Composer\\bin\\UmdStreamComposer.exe";

        [Option('x', "executable", HelpText = "Path to UmdStreamComposer.exe", Default = DefaultExecutablePath, Required = false)]
        public string Executable { get; set; }

        [Option("cn", HelpText = "Clip name", Required = true)]
        public string ClipName { get; set; }

        [Option("cd", HelpText = "Clip description", Default = "", Required = false)]
        public string ClipDescription { get; set; }

        [Option("pn", HelpText = "Project name", Required = true)]
        public string ProjectName { get; set; }

        [Option("pd", HelpText = "Project description", Default = "", Required = false)]
        public string ProjectDescription { get; set; }

        [Option('a', "audio", HelpText = "Input audio", Required = true)]
        public string Audio { get; set; }

        [Option('v', "video", HelpText = "Input video", Required = true)]
        public string Video { get; set; }

        [Option('o', "output", HelpText = "Output file", Required = false)]
        public string Output { get; set; }
    }
}
