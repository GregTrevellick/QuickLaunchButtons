//using Microsoft.VisualStudio.Shell;
//using QuickLaunch.Common;
//using System.ComponentModel;
//using System.Runtime.InteropServices;
//using System.Windows.Forms;

//namespace QuickLaunch.Fiddler.Options
//{
//    [Guid("00000000-0000-0000-0000-000000000000")]
//    public class GeneralOptions2 : DialogPage
//    {
//        //private const string CommonActualPathToExeOptionLabel = CommonConstants.ActualPathToExeOptionLabelPrefix + CommonConstants.FiddlerExeName + CommonConstants.DefaultExecutableFileSuffix;

//        //[DisplayName(CommonActualPathToExeOptionLabel)]
//        //[Description(CommonConstants.ActualPathToExeOptionDetailedDescription)]
//        public string ActualPathToExe { get; set; } = GeneralOptionsHelper.GetDefaultActualPathToExe();

//        protected override IWin32Window Window
//        {
//            get
//            {
//                var settingsUserControl = new SettingsUserControl
//                {
//                    //generalOptions2 = this
//                };

//                settingsUserControl.Initialize();

//                return settingsUserControl;
//            }
//        }


//    }
//}
