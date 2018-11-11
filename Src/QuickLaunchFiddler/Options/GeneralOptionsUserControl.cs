using QuickLaunch.Common;
using System;
using System.Linq;
using System.Windows.Forms;

namespace QuickLaunch.Fiddler.Options
{
    public partial class GeneralOptionsUserControl : UserControl
    {
        internal GeneralOptions generalOptions;

        private const string CommonActualPathToExeOptionLabel = CommonConstants.ActualPathToExeOptionLabelPrefix + CommonConstants.FiddlerExeName + CommonConstants.DefaultExecutableFileSuffix;

        public GeneralOptionsUserControl()
        {
            InitializeComponent();
        }
        
        public void Initialize()
        {
            labelActualPathToExe.Text = CommonActualPathToExeOptionLabel;
            textActualPathToExe.Text = generalOptions.ActualPathToExe;
            textActualPathToExeDescription.Text = CommonConstants.ActualPathToExeOptionDetailedDescription;

            generalOptions.Load();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var openFileDialog = GetOpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveSettings(openFileDialog.FileNames.Single());
            }
        }

        private OpenFileDialog GetOpenFileDialog()
        {
            return new OpenFileDialog
            {
                Filter = "Executable file (*.exe)|*.exe|All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Multiselect = false,
            };
        }

        private void SaveSettings(string fileName)
        {
            textActualPathToExe.Text = fileName;
            generalOptions.ActualPathToExe = fileName;
            generalOptions.Save();
        }
    }
}
