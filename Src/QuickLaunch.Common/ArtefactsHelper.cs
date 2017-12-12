using System.Collections.Generic;
using System.IO;

namespace QuickLaunch.Common
{
    public static class ArtefactsHelper 
    {
        public static bool DoesActualPathToExeExist(string fullExecutableFileName)
        {
            return DoArtefactsExist(new List<string> { fullExecutableFileName });
        }

        public static bool DoArtefactsExist(IEnumerable<string> fullArtefactNames)
        {
            var result = true;

            foreach (var fullArtefactName in fullArtefactNames)
            {
                if (string.IsNullOrEmpty(fullArtefactName))
                {
                    result = false;
                }
                else
                {
                    if (!File.Exists(fullArtefactName))
                    {
                        result = false;
                    }
                }
            }

            return result;
        }
    }
}
