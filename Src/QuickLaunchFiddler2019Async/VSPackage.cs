using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using QuickLaunch.Common;
using QuickLaunch.Fiddler.Commands;
using QuickLaunch.Fiddler.Options;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace QuickLaunch.Fiddler
{
    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExists_string, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string, PackageAutoLoadFlags.BackgroundLoad)]
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(productName: "#110", productDetails: "#112", productId: Vsix.Version, IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.guidQuickButtonCommandPackageString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideOptionPage(typeof(DialogPageProvider.General), CommonConstants.FiddlerOptionsName, CommonConstants.General, 0, 0, true)]
    public sealed class VSPackage : AsyncPackage
    {
        public static GeneralOptions GeneralOptions { get; private set; }

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            GeneralOptions = await GeneralOptions.GetLiveInstanceAsync();
            if (string.IsNullOrEmpty(GeneralOptions.ActualPathToExe))
            {
                GeneralOptions.ActualPathToExe = GeneralOptionsHelper.GetActualPathToExe(secondaryFilePathSegment: "Fiddler", executableFileToBrowseFor: CommonConstants.FiddlerExeName + CommonConstants.DefaultExecutableFileSuffix, multipleSecondaryFilePathSegments: true);
                await GeneralOptions.SaveAsync();
            }

            await QuickButtonCommand.InitializeAsync(this);
        }
    }
}