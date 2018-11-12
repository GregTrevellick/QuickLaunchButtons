using System;

namespace QuickLaunch.Fiddler.Commands
{
    public static class FileSystemHelper
	{
		public static string GetDefaultActualPathToExe()
		{
			var local = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			local = local.Replace("Roaming", @"Local\Programs");
            return $@"{local}\Fiddler\Fiddler.exe";
        }
    }
}