using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA2;

namespace AutoUsc
{
    public class UscRobot
    {
        private Application app;
        private UIA2Automation automation;
        private Window mainWindow;
        private string executable;

        public UscRobot(string executable)
        {
            this.executable = executable;
        }

        public void Launch()
        {
            app = Application.Launch(executable);
            automation = new UIA2Automation();

            mainWindow = app.GetMainWindow(automation, TimeSpan.FromSeconds(10));
            Thread.Sleep(1000);
            var popup = mainWindow.FindFirstDescendant(cf => cf.ByName("UMD(TM) Stream Composer / Viewer"))?.AsWindow();
            if (popup != null)
            {
                popup.FindFirstDescendant(cf => cf.ByClassName("Button"))?.AsButton().Click();
            }
        }

        public void Close()
        {
            app.Close();
        }

        public UscProject CreateNewProject(string clipName, string clipDesc = null, string projName = null, string projDesc = null, string output = null)
        {
            UscProject proj = new UscProject(clipName, clipDesc, projName, projDesc, output);

            OpenCreateNewDialog();
            var newProjDialog = mainWindow.FindFirstDescendant(cf => cf.ByName("Create new clip"))?.AsWindow();
            var edits = newProjDialog.FindAllDescendants(cf => cf.ByClassName("Edit"));
            edits[0].AsTextBox().Enter(clipName);
            edits[1].AsTextBox().Enter(clipDesc);
            edits[2].AsTextBox().Enter(projName);
            edits[3].AsTextBox().Enter(projDesc);


            using (Keyboard.Pressing(VirtualKeyShort.ALT))
            {
                Keyboard.Press(VirtualKeyShort.KEY_N);
            }
            Thread.Sleep(TimeSpan.FromSeconds(1));


            var allBtn = newProjDialog.FindAllDescendants(cf => cf.ByClassName("Button"));
            var cbPsp = allBtn.FirstOrDefault(e => e.Name == "PSP Movie Format (for game)")?.AsCheckBox();
            if (cbPsp != null)
            {
                cbPsp.IsChecked = true;
            }

            var btnFin = allBtn.FirstOrDefault(e => e.Properties.AutomationId.IsSupported && e.AutomationId == "12325")?.AsButton();
            if (btnFin != null)
            {
                btnFin.Click();
            }

            Thread.Sleep(1000);
            return proj;
        }

        public void AddAudioStream(string path)
        {
            OpenAudioSourceDialog();

            var dialog = mainWindow.FindFirstDescendant(cf => cf.ByName("Audio Source Setting - Stream 1"))?.AsWindow();
            var allButtons = dialog.FindAllDescendants(cf => cf.ByClassName("Button"));
            var btnOpen = allButtons.FirstOrDefault(e => e.Properties.AutomationId.IsSupported && e.AutomationId == "1120");
            var btnOk = allButtons.FirstOrDefault(e => e.Properties.AutomationId.IsSupported && e.AutomationId == "1");

            btnOpen.Click();
            Thread.Sleep(1000);

            Keyboard.Type(path);
            Keyboard.Press(VirtualKeyShort.ENTER);
            btnOk.Click();
            Thread.Sleep(1000);
        }

        public void AddVideoStream(string path)
        {
            OpenVideoSourceDialog();

            var dialog = mainWindow.FindFirstDescendant(cf => cf.ByName("Video Source Setting - Stream 1"))?.AsWindow();
            var allButtons = dialog.FindAllDescendants(cf => cf.ByClassName("Button"));
            var btnOpen = allButtons.FirstOrDefault(e => e.Properties.AutomationId.IsSupported && e.AutomationId == "1132");
            var btnOk = allButtons.FirstOrDefault(e => e.Properties.AutomationId.IsSupported && e.AutomationId == "1");

            btnOpen.Click();
            Thread.Sleep(1000);

            Keyboard.Type(path);
            Keyboard.Press(VirtualKeyShort.ENTER);
            btnOk.Click();
            Thread.Sleep(1000);
        }

        public void Compose(UscProject project)
        {
            var btnStart = mainWindow.FindFirstDescendant(cf => cf.ByClassName("Button").And(cf.ByName("Start...")))?.AsButton();
            btnStart?.Click();

            Thread.Sleep(1000);
            var actionCombo = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("1355"))?.AsComboBox();
            actionCombo.Select(2);

            var btnStart2 = mainWindow.FindFirstDescendant(cf => cf.ByClassName("Button").And(cf.ByName("Start")))?.AsButton();
            btnStart2.Click();

            while (!btnStart2.IsEnabled)
            {
                Thread.Sleep(1000);
            }

            if (!string.IsNullOrEmpty(project.Output))
            {
                var projectOutput = project.ProjectOutput;
                if (File.Exists(projectOutput))
                {
                    File.Copy(projectOutput, project.Output);
                }
            }
        }

        private void OpenCreateNewDialog()
        {
            using (Keyboard.Pressing(VirtualKeyShort.CONTROL))
            {
                Keyboard.Press(VirtualKeyShort.KEY_N);
            }
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        private void OpenAudioSourceDialog()
        {
            var toolBars = mainWindow.FindAllDescendants(cf => cf.ByClassName("ToolbarWindow32"));
            var toolBar = toolBars.FirstOrDefault(e => e.Properties.AutomationId.IsSupported && e.AutomationId == "59425");
            var allToolBarItems = toolBar.FindAllDescendants();
            var btnAudioSource = allToolBarItems.FirstOrDefault(e => e.Properties.AutomationId.IsSupported && e.AutomationId == "Item 32829")?.AsButton();

            btnAudioSource?.Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        private void OpenVideoSourceDialog()
        {
            var toolBars = mainWindow.FindAllDescendants(cf => cf.ByClassName("ToolbarWindow32"));
            var toolBar = toolBars.FirstOrDefault(e => e.Properties.AutomationId.IsSupported && e.AutomationId == "59425");
            var allToolBarItems = toolBar.FindAllDescendants();
            var btnVideoSource = allToolBarItems.FirstOrDefault(e => e.Properties.AutomationId.IsSupported && e.AutomationId == "Item 32786")?.AsButton();

            btnVideoSource?.Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }
}
