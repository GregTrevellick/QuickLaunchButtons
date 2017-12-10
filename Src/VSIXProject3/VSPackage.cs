using Microsoft.VisualStudio.Shell;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace VSIXProject3
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]       
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(VSPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class VSPackage : Package
    {
        public const string PackageGuidString = "462b1151-d39e-4099-b947-7702b6b60e9b";

        public VSPackage()
        {
        }

        protected override void Initialize()
        {
            ToolbarTestCommand.Initialize(this);
            base.Initialize();
        }

    }
}
