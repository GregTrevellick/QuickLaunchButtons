using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using QuickLaunch.Common;
using QuickLaunch.Wireshark.Options;

namespace QuickLaunch.Wireshark.Commands
{
    internal class QuickButtonCommand
    {
        private readonly Package package;
        private IServiceProvider ServiceProvider => this.package;
        public const int CommandId = PackageIds.QuickButtonCommandId;
        public static readonly Guid CommandSet = new Guid(PackageGuids.guidQuickButtonCommandPackageCmdSetString);
        public static QuickButtonCommand Instance { get; private set; }

        public static void Initialize(Package package)
        {
            Instance = new QuickButtonCommand(package);
            GeneralOptionsHelper.PersistHiddenOptionsQuizHelperEventHandlerEventHandler += PersistVSToolOptions;
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
            GeneralOptionsHelper.InvokeApplication(VSPackage.Options.ActualPathToExe, Vsix.Name, CommonConstants.WiresharkOptionsName);
        }

        public static void PersistVSToolOptions(string fileName)
        {
            GeneralOptions.PersistVSToolOptions(fileName);
        }
    }
}