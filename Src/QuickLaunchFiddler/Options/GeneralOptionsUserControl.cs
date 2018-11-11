using System;
/////////////////////////////////////////////////////////////using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace QuickLaunch.Fiddler.Options
{
    public partial class GeneralOptionsUserControl : UserControl
    {
        public GeneralOptionsUserControl()
        {
            InitializeComponent();
        }

        internal GeneralOptions generalOptions;

        public void Initialize()
        {
            generalOptions.Load();
            textBox1.Text = generalOptions.ActualPathToExe;
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
                textBox1.Text = fileName;
                generalOptions.ActualPathToExe = fileName;
                generalOptions.Save();
            }
        }
    }
}
