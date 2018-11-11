using Microsoft.VisualStudio.Shell;
using System.Windows.Forms;

namespace QuickLaunch.Fiddler.Options
{
    /// <summary>
    /// A base class for a DialogPage to show in Tools -> Options.
    /// </summary>
    internal class BaseOptionPage<T> : DialogPage where T : BaseOptionModel<T>, new()
    {
        private BaseOptionModel<T> _model;

        public BaseOptionPage()
        {
            #pragma warning disable VSTHRD104 // Offer async methods
            _model = ThreadHelper.JoinableTaskFactory.Run(BaseOptionModel<T>.CreateAsync);
            #pragma warning restore VSTHRD104 // Offer async methods
        }

        public override object AutomationObject => _model;

        public override void LoadSettingsFromStorage()
        {
            _model.Load();
        }

        public override void SaveSettingsToStorage()
        {
            _model.Save();
        }

        protected override IWin32Window Window
        {
            get
            {
                var settingsUserControl = new SettingsUserControl
                {
                    generalOptions = new GeneralOptions()
                };

                settingsUserControl.Initialize();

                return settingsUserControl;
            }
        }
    }
}
