using System.IO;

namespace QuickLaunch.Common
{
    public static class ArtefactsHelper 
    {
        public static bool DoesActualPathToExeExist(string fullExecutableFileName)
        {
            var result = true;

            if (string.IsNullOrEmpty(fullExecutableFileName))
            {
                result = false;
            }
            else
            {
                if (!File.Exists(fullExecutableFileName))
                {
                    result = false;
                }
            }

            return result;
        }
    }
}
