using Microsoft.VisualStudio.Shell;
using QuickLaunch.Common;
using QuickLaunch.Fiddler.Options;
using System.ComponentModel.Design;
using System;
using Task = System.Threading.Tasks.Task;

namespace QuickLaunch.Fiddler.Commands
{
    internal sealed class QuickButtonCommand
    {
        //private static IRatingDetailsDto _hiddenChaserOptions;
        public const int CommandId = PackageIds.QuickButtonCommandId;
        public static readonly Guid CommandSet = new Guid(PackageGuids.guidQuickButtonCommandPackageCmdSetString);
        public static GeneralOptions GeneralOptions { get; private set; }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            if (package == null)
            {
                new FilePrompterHelper(Vsix.Name, null).InformUnexpectedError(null);
            }

            var commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;

            if (commandService != null)
            {
                var menuCommandId = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(InvokeApplication, menuCommandId);
                commandService.AddCommand(menuItem);
            }
        }

        private static void InvokeApplication(object sender, EventArgs e)
        {
            try
            {
                var actualPathToExe = GeneralOptions.Instance.ActualPathToExe;
                if (string.IsNullOrEmpty(actualPathToExe))
                {
                    var local = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    local = local.Replace("Roaming", @"Local\Programs");
                    actualPathToExe = $@"{local}\Fiddler\Fiddler.exe";
                }
                GeneralOptionsHelper.InvokeApplication(actualPathToExe, Vsix.Name, CommonConstants.FiddlerOptionsName);
                //ChaseRating();
            }
            catch (Exception ex)
            {
                new FilePrompterHelper(Vsix.Name, null).InformUnexpectedError(ex);
            }
        }

        //internal void ChaseRating( )
        //{
        //    var packageRatingChaser = new PackageRatingChaser();
        //    packageRatingChaser.Hunt(_hiddenChaserOptions);
        //}
    }
}