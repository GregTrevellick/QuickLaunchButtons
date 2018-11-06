using System.ComponentModel;
using System.ComponentModel;
//using Microsoft.VisualStudio.Shell;
using QuickLaunch.Common;
using Task = System.Threading.Tasks.Task;

namespace QuickLaunch.Fiddler.Options
{
    public class GeneralOptions : BaseOptionModel<GeneralOptions>
    {
        private const string CommonActualPathToExeOptionLabel = CommonConstants.ActualPathToExeOptionLabelPrefix + CommonConstants.FiddlerExeName + CommonConstants.DefaultExecutableFileSuffix;

        [DisplayName(CommonActualPathToExeOptionLabel)]
        [Description(CommonConstants.ActualPathToExeOptionDetailedDescription)]
        public string ActualPathToExe { get; set; }
    }
}
