using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using QuickLaunch.Common;
using QuickLaunch.Fiddler.Commands;
using QuickLaunch.Fiddler.Options;

namespace QuickLaunch.Fiddler
{
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    [ProvideAutoLoad(UIContextGuids80.NoSolution)]
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration(productName: "#110", productDetails: "#112", productId: Vsix.Version, IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.guidQuickButtonCommandPackageString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideOptionPage(typeof(GeneralOptions), CommonConstants.SharedTopLevelOptionsName, CommonConstants.CategorySubLevelFiddler, 0, 0, true)]
    public sealed class VSPackage : Package
    {
        public static GeneralOptions Options { get; private set; }

        ///// <summary>
        /////Inside this method you can place any initialization code that does not require any Visual Studio service 
        /////because at this point the package object is created but not sited yet inside Visual Studio environment. 
        /////The place to do all the other initialization is the Initialize method.
        ///// </summary>
        //public VSPackage()
        //{
        //}

        protected override void Initialize()
        {
            Options = (GeneralOptions)GetDialogPage(typeof(GeneralOptions));

            QuickButtonCommand.Initialize(this);
            base.Initialize();
        }
    }
}
