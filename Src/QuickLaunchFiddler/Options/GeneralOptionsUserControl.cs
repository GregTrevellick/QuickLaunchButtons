using QuickLaunch.Common;
using System;
using System.Linq;
using System.Windows.Forms;

namespace QuickLaunch.Fiddler.Options
{
    public partial class GeneralOptionsUserControl : UserControl
    {
        private const string CommonActualPathToExeOptionLabel = CommonConstants.ActualPathToExeOptionLabelPrefix + CommonConstants.FiddlerExeName + CommonConstants.DefaultExecutableFileSuffix;

        internal GeneralOptions generalOptions;

        public GeneralOptionsUserControl()
        {
            InitializeComponent();
        }
        
        public void Initialize()
        {
            labelActualPathToExe.Text = CommonActualPathToExeOptionLabel;
            generalOptions.Load();
            textActualPathToExe.Text = generalOptions.ActualPathToExe;
        }
        
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Executable file (*.exe)|*.exe|All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Multiselect = false,
            };

            var dialogResult = openFileDialog.ShowDialog();
            
            if (dialogResult == DialogResult.OK)
            {
                var fileName  = openFileDialog.FileNames.Single();

                textActualPathToExe.Text = fileName;

                generalOptions.ActualPathToExe = fileName;
                generalOptions.Save();
            }
        }
    }
}
