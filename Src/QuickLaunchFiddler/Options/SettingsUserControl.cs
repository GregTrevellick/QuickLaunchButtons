using System;
using System.Windows.Forms;

namespace QuickLaunch.Fiddler.Options
{
    public partial class SettingsUserControl : UserControl
    {
        public SettingsUserControl()
        {
            InitializeComponent();
        }

        internal GeneralOptions generalOptions;

        public void Initialize()
        {
            generalOptions.Load();
            textBox1.Text = generalOptions.ActualPathToExe;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            //gregt change this to when save it clicked
            generalOptions.ActualPathToExe = textBox1.Text;
            generalOptions.Save();
        }
    }
}
