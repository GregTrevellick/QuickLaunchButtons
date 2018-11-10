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
        //private static string actualPathToExe;
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

            //GeneralOptionsHelper.PersistHiddenOptionsQuizHelperEventHandlerEventHandler += PersistVSToolOptions;
        }

        private static void InvokeApplication(object sender, EventArgs e)
        {
            try
            {
                //////var actualPathToExe = VSPackage.GeneralOptions.ActualPathToExe;
                var actualPathToExe = GeneralOptions.Instance.ActualPathToExe;// GetActualPathToExeFromSettings();
                GeneralOptionsHelper2.InvokeApplication(actualPathToExe, Vsix.Name, CommonConstants.FiddlerOptionsName);
                //ChaseRating();
            }
            catch (Exception ex)
            {
                new FilePrompterHelper(Vsix.Name, null).InformUnexpectedError(ex);
            }
        }

        //public static void GetActualPathToExeFromSettings()
        //{
        //    Task.Run(async () =>
        //    {
        //        GeneralOptions generalOptions = await GeneralOptions.GetLiveInstanceAsync();
        //        actualPathToExe = generalOptions.ActualPathToExe;
        //    });
        //}

        //public static void PersistVSToolOptions(string fileName)
        //{
        //    Task.Run(async () =>
        //    {
        //        GeneralOptions = await GeneralOptions.GetLiveInstanceAsync();

        //        if (string.IsNullOrEmpty(GeneralOptions.ActualPathToExe))
        //        {
        //            GeneralOptions.ActualPathToExe = GeneralOptionsHelper.GetActualPathToExe(
        //                secondaryFilePathSegment: "Fiddler",
        //                executableFileToBrowseFor: CommonConstants.FiddlerExeName + CommonConstants.DefaultExecutableFileSuffix,
        //                multipleSecondaryFilePathSegments: true);

        //            await GeneralOptions.SaveAsync();
        //        }
        //    });
        //}

        //public static void PersistVSToolOptions(string fileName)
        //{
        //    GeneralOptions generalOptions = ThreadHelper.JoinableTaskFactory.Run(GeneralOptions.GetLiveInstanceAsync);
        //    generalOptions.ActualPathToExe = fileName;
        //    generalOptions.Save();
        //}

        //internal void ChaseRating( )
        //{
        //    var packageRatingChaser = new PackageRatingChaser();
        //    packageRatingChaser.Hunt(_hiddenChaserOptions);
        //}

    }
}