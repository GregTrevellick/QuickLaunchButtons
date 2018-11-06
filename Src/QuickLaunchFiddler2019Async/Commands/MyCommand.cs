//using System;
//using System.ComponentModel.Design;
//using Microsoft.VisualStudio.Shell;
//using Microsoft.VisualStudio.Shell.Interop;
//using Task = System.Threading.Tasks.Task;

//namespace QuickLaunch.Fiddler
//{
//    internal sealed class MyCommand
//    {
//        public static async Task InitializeAsync(AsyncPackage package)
//        {
//            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
           
//            var commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;

//            // must match the button GUID and ID specified in the .vsct file
//            var cmdId = new CommandID(Guid.Parse("2b40859b-27f8-4dc6-85b1-f253386aa5f6"), 0x0100); 
//            var cmd = new MenuCommand((s, e) => Execute(package), cmdId);
//            commandService.AddCommand(cmd);
//        }

//        private static void Execute(AsyncPackage package)
//        {
//            ThreadHelper.ThrowIfNotOnUIThread();

//            VsShellUtilities.ShowMessageBox(
//                package,
//                $"Inside {typeof(MyCommand).FullName}.Execute() 2",
//                nameof(MyCommand),
//                OLEMSGICON.OLEMSGICON_INFO,
//                OLEMSGBUTTON.OLEMSGBUTTON_OK,
//                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
//        }

//        public void GetEm()
//        {
//            // Call the Instance singleton from the UI thread is easy
//            //bool showMessage = OtherOptions.Instance.ShowMessage;

//            Task.Run(async () =>
//            {
//                // Make the call to GetLiveInstanceAsync from a background thread to avoid blocking the UI thread
//                GeneralOptions options = await GeneralOptions.GetLiveInstanceAsync();
//                string message = options.Message;
//                // Do something with message
//            });
//        }

//    }
//}
