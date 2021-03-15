using System;
using System.IO;

namespace AutoUsc
{
    public class UscProject
    {
        public UscProject(string clipName, string clipDesc = null, string projName = null, string projDesc = null, string output = null)
        {
            this.ClipName = clipName;
            this.ClipDesc = clipDesc;
            this.ProjectName = projName;
            this.ProjectDescription = projDesc;
            this.Output = output;
        }

        public string ClipName { get; private set; }
        public string ClipDesc { get; private set; }
        public string ProjectName { get; private set; }
        public string ProjectDescription { get; private set; }
        public string Output { get; private set; }
        public string ProjectOutput => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UmdStreamComposer", "MuxWork", ProjectName, "00001", "00001.MPS");
    }
}