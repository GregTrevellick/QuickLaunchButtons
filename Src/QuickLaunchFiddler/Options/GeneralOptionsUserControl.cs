using QuickLaunch.Common;
using QuickLaunch.Fiddler.Commands;
using System;
using System.Linq;
using System.Windows.Forms;

namespace QuickLaunch.Fiddler.Options
{
    public partial class GeneralOptionsUserControl : UserControl
    {
        internal GeneralOptions generalOptions;
        private const string exe = CommonConstants.FiddlerExeName + CommonConstants.DefaultExecutableFileSuffix;
        private const string CommonActualPathToExeOptionLabel = CommonConstants.ActualPathToExeOptionLabelPrefix + exe;

        public GeneralOptionsUserControl()
        {
            InitializeComponent();
        }
        
        public void Initialize()
        {
            generalOptions.Load();

            labelActualPathToExe.Text = CommonActualPathToExeOptionLabel;
            labelActualPathToExeDescription.Text = CommonConstants.ActualPathToExeOptionDetailedDescription;

            var actualPathToExe = generalOptions.ActualPathToExe;

            if (string.IsNullOrEmpty(actualPathToExe))
            {
                textActualPathToExe.Text = FileFinderHelper.GetKnownActualPathToExe("Fiddler", exe, true);
            }
            else
            {
                textActualPathToExe.Text = actualPathToExe;
            }
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
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                Multiselect = false,
            };
        }

        private void SaveSettings(string fileName)
        {
            textActualPathToExe.Text = fileName;
            generalOptions.ActualPathToExe = fileName;
            generalOptions.Save();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettings(textActualPathToExe.Text);
        }
    }
}
