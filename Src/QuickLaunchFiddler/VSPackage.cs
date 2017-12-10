using Microsoft.VisualStudio.Shell;
using QuickLaunchFiddler.Commands;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace QuickLaunchFiddler
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration(productName: "#110", productDetails: "#112", productId: Vsix.Version, IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.guidQuickButtonCommandPackageString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    //[ProvideOptionPage(typeof(GeneralOptions), Vsix.Name, CommonConstants.CategorySubLevel, 0, 0, true)]
    public sealed class VSPackage : Package
    {
        //public VSPackage()
        //{
        //    // Inside this method you can place any initialization code that does not require
        //    // any Visual Studio service because at this point the package object is created but
        //    // not sited yet inside Visual Studio environment. The place to do all the other
        //    // initialization is the Initialize method.
        //}

        protected override void Initialize()
        {
            QuickButtonCommand.Initialize(this);
            base.Initialize();
        }
    }
}
