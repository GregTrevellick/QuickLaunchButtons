﻿using System;

namespace QuickLaunch.Common
{
    public static class CommonConstants
    {
        public const string ActualPathToExeOptionDetailedDescription = "Specify the absolute install path for the application.";
        public const string ActualPathToExeOptionLabelPrefix = "Application path to ";
        public const string DefaultExecutableFileSuffix = ".exe";
        public const string General = "General";
        public static string ContinueAnyway = "Click OK to open anyway, or CANCEL to return to Visual Studio.";

        public const string FiddlerExeName = "Fiddler";
        public const string FiddlerOptionsName = "Fiddler1";
        public const string WiresharkExeName = "Wireshark";
        public const string WiresharkOptionsName = "Wireshark1";

        public static string InformUserMissingFile(string missingFileName)
        {
            return $"The executable file for {missingFileName} does not exist.";
        }

        public static string PromptForActualExeFile(string dodgyPathToFile)
        {
            return InformUserMissingFile(dodgyPathToFile)
                + Environment.NewLine + Environment.NewLine
                + "Do you want to browse the for the file ?"
                + Environment.NewLine + Environment.NewLine
                + "Click YES to locate the file, NO to save anyway.";
        }
        
        public static string UnexpectedError =
            "An unexpected error has occured. Please restart Visual Studio and re-try." + Environment.NewLine + Environment.NewLine +
            "If the error persists please log a bug for this extension via the Visual Studio Marketplace at https://marketplace.visualstudio.com" + Environment.NewLine + Environment.NewLine +
            "Press OK to return to Visual Studio.";
    }
}
