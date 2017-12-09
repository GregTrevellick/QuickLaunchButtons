using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using Microsoft.VisualStudio.Shell;

namespace QuickLaunchFiddler.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class QuickButtonCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("7aaba3a9-97d0-41d2-b4c4-b543912979a0");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        ///// <summary>
        ///// Initializes a new instance of the <see cref="QuickButtonCommand"/> class.
        ///// Adds our command handlers for menu (commands must exist in the command table file)
        ///// </summary>
        ///// <param name="package">Owner package, not null.</param>
        //private QuickButtonCommand(Package package)
        //{
        //    if (package == null)
        //    {
        //        throw new ArgumentNullException("package");
        //    }

        //    this.package = package;

        //    OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
        //    if (commandService != null)
        //    {
        //        var menuCommandID = new CommandID(CommandSet, CommandId);
        //        var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
        //        commandService.AddCommand(menuItem);
        //    }
        //}

        private QuickButtonCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException(nameof(package));
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                CommandID menuCommandID = new CommandID(CommandSet, CommandId);
                // Change to StartNotepad handler.  
                MenuCommand menuItem = new MenuCommand(this.StartNotepad, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        private void StartNotepad(object sender, EventArgs e)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "notepad.exe";
            proc.Start();
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static QuickButtonCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new QuickButtonCommand(package);
        }

        ///// <summary>
        ///// This function is the callback used to execute the command when the menu item is clicked.
        ///// See the constructor to see how the menu item is associated with this function using
        ///// OleMenuCommandService service and MenuCommand class.
        ///// </summary>
        ///// <param name="sender">Event sender.</param>
        ///// <param name="e">Event args.</param>
        //private void MenuItemCallback(object sender, EventArgs e)
        //{
        //    string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
        //    string title = "QuickButtonCommand";

        //    // Show a message box to prove we were here
        //    VsShellUtilities.ShowMessageBox(
        //        this.ServiceProvider,
        //        message,
        //        title,
        //        OLEMSGICON.OLEMSGICON_INFO,
        //        OLEMSGBUTTON.OLEMSGBUTTON_OK,
        //        OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        //}
    }
}
