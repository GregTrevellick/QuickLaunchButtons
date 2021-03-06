﻿using Microsoft.VisualStudio.Shell;
using QuickLaunch.Common;
using QuickLaunch.Fiddler.Options;
using System.ComponentModel.Design;
using System;
using Task = System.Threading.Tasks.Task;

namespace QuickLaunch.Fiddler.Commands
{
    internal sealed class QuickButtonCommand
    {
        public const int CommandId = PackageIds.QuickButtonCommandId;
        public static readonly Guid CommandSet = new Guid(PackageGuids.guidQuickButtonCommandPackageCmdSetString);
        public static GeneralOptions GeneralOptions { get; private set; }

        private const string exe = CommonConstants.FiddlerExeName + CommonConstants.DefaultExecutableFileSuffix;

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            if (package == null)
            {
                new FilePrompterHelper(Vsix.Name, null).InformUnexpectedError(null);
            }

            var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;

            if (commandService != null)
            {
                var menuCommandId = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(InvokeApplication, menuCommandId);
                commandService.AddCommand(menuItem);
            }
        }

        private static void InvokeApplication(object sender, EventArgs e)
        {
            try
            {
                var actualPathToExe = GeneralOptions.Instance.ActualPathToExe;

				if (string.IsNullOrEmpty(actualPathToExe))
				{
                    actualPathToExe = FileFinderHelper.GetKnownActualPathToExe("Fiddler", exe, true); 
                }

				InvokerHelper.InvokeApplication(actualPathToExe, Vsix.Name, CommonConstants.FiddlerOptionsName);
            }
            catch (Exception ex)
            {
                new FilePrompterHelper(Vsix.Name, null).InformUnexpectedError(ex);
            }
        }
    }
}