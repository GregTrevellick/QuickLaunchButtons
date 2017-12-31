using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using QuickLaunch.Common;
using QuickLaunch.Fiddler.Options;
using VsixRatingChaser.Interfaces;

namespace QuickLaunch.Fiddler.Commands
{
    internal class QuickButtonCommand
    {
        private readonly Package package;
        private static IRatingDetailsDto _hiddenChaserOptions;
        private IServiceProvider ServiceProvider => this.package;
        public const int CommandId = PackageIds.QuickButtonCommandId;
        public static readonly Guid CommandSet = new Guid(PackageGuids.guidQuickButtonCommandPackageCmdSetString);
        public static QuickButtonCommand Instance { get; private set; }

        public static void Initialize(Package package, IRatingDetailsDto hiddenChaserOptions)
        {
            _hiddenChaserOptions = hiddenChaserOptions;
            Instance = new QuickButtonCommand(package);
            GeneralOptionsHelper.PersistHiddenOptionsQuizHelperEventHandlerEventHandler += PersistVSToolOptions;
        }

        private QuickButtonCommand(Package package)
        {
            if (package == null)
            {
                new FilePrompterHelper(Vsix.Name, null).InformUnexpectedError(null);
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
            try
            {
                GeneralOptionsHelper.InvokeApplication(VSPackage.Options.ActualPathToExe, Vsix.Name, CommonConstants.FiddlerOptionsName);
                ChaseRating();
            }
            catch (Exception ex)
            {
                new FilePrompterHelper(Vsix.Name, null).InformUnexpectedError(ex);
            }
        }

        public static void PersistVSToolOptions(string fileName)
        {
            GeneralOptions.PersistVSToolOptions(fileName);
        }

        internal void ChaseRating( )
        {
            var packageRatingChaser = new PackageRatingChaser();
            packageRatingChaser.Hunt(_hiddenChaserOptions);
        }
    }
}