using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;

namespace QuickLaunch.Fiddler.Commands
{
    internal sealed partial class QuickButtonCommand
    {
        public const int CommandId = 0x0100;//gregt PackageIds.QuickButtonCommandId
        private readonly Package package;
        private IServiceProvider ServiceProvider => this.package;

        public static readonly Guid CommandSet = new Guid("7aaba3a9-97d0-41d2-b4c4-b543912979a0");//gregt PackageGuids.guidQuickButtonCommandPackageCmdSetString;
        public static QuickButtonCommand Instance { get; private set; }

        public static void Initialize(Package package)
        {
            Instance = new QuickButtonCommand(package);
        }

        private QuickButtonCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException(nameof(package));
            }

            this.package = package;

            var commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            if (commandService != null)
            {
                var menuCommandId = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(InvokeApplication, menuCommandId);
                commandService.AddCommand(menuItem);
            }
        }

        private void InvokeApplication(object sender, EventArgs e)
        {
            QuickButtonCommandHelper.InvokeApplication("Fiddler2", "Fiddler.exe");
        }    
    }
}