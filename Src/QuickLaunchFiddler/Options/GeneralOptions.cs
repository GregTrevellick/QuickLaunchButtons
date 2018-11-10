using System.ComponentModel;
using QuickLaunch.Common;

namespace QuickLaunch.Fiddler.Options
{
    public class GeneralOptions : BaseOptionModel<GeneralOptions>
    {
        private const string CommonActualPathToExeOptionLabel = CommonConstants.ActualPathToExeOptionLabelPrefix + CommonConstants.FiddlerExeName + CommonConstants.DefaultExecutableFileSuffix;

        [DisplayName(CommonActualPathToExeOptionLabel)]
        [Description(CommonConstants.ActualPathToExeOptionDetailedDescription)]
		public string ActualPathToExe { get; set; } = GeneralOptionsHelper.GetDefaultActualPathToExe(true);
	}
}
