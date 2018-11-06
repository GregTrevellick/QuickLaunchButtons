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
        //private readonly AsyncPackage package;
        //private static IRatingDetailsDto _hiddenChaserOptions;
        public const int CommandId = PackageIds.QuickButtonCommandId;
        public static readonly Guid CommandSet = new Guid(PackageGuids.guidQuickButtonCommandPackageCmdSetString);

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

            GeneralOptionsHelper.PersistHiddenOptionsQuizHelperEventHandlerEventHandler += PersistVSToolOptions;
        }

        private static void InvokeApplication(object sender, EventArgs e)
        {
            try
            {
                GeneralOptionsHelper.InvokeApplication(VSPackage.GeneralOptions.ActualPathToExe, Vsix.Name, CommonConstants.FiddlerOptionsName);
                //ChaseRating();
            }
            catch (Exception ex)
            {
                new FilePrompterHelper(Vsix.Name, null).InformUnexpectedError(ex);
            }
        }

        public static void PersistVSToolOptions(string fileName)
        {
            GeneralOptions generalOptions = ThreadHelper.JoinableTaskFactory.Run(GeneralOptions.GetLiveInstanceAsync);
            generalOptions.ActualPathToExe = fileName;
            generalOptions.Save();
        }

        //internal void ChaseRating( )
        //{
        //    var packageRatingChaser = new PackageRatingChaser();
        //    packageRatingChaser.Hunt(_hiddenChaserOptions);
        //}
    }
}