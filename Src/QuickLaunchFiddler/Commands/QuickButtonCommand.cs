using Microsoft.VisualStudio.Shell;
//using OpenInAbracadabra.Options.Abracadabra;
//using OpenInApp.Common.Helpers;
//using OpenInApp.Menu;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace QuickLaunchFiddler.Commands
{
    internal sealed class QuickButtonCommand
    {
        public const int CommandId = 0x0100;

        public static readonly Guid CommandSet = new Guid("7aaba3a9-97d0-41d2-b4c4-b543912979a0");//gregt PackageGuids.guidQuickButtonCommandPackageCmdSetString;

        private readonly Package package;

        private QuickButtonCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException(nameof(package));
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                CommandID menuCommandID = new CommandID(CommandSet, CommandId);
                MenuCommand menuItem = new MenuCommand(this.StartNotepad, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        private void StartNotepad(object sender, EventArgs e)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "notepad.exe";
            proc.Start();
        }

        public static QuickButtonCommand Instance
        {
            get;
            private set;
        }

        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        public static void Initialize(Package package)
        {
            Instance = new QuickButtonCommand(package);
        }
    }
}