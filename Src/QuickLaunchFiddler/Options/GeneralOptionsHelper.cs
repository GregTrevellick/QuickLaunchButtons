using System;

namespace QuickLaunch.Fiddler.Options
{
    public static class GeneralOptionsHelper
	{
		public static string GetDefaultActualPathToExe(bool persist = false)
		{
			var local = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			local = local.Replace("Roaming", @"Local\Programs");
			var defaultActualPathToExe = $@"{local}\Fiddler\Fiddler.exe";

			//if (persist)
			//{
			//	PersistVSToolOptions(defaultActualPathToExe);
			//}

			return defaultActualPathToExe;
		}
    }
}