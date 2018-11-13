using QuickLaunch.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuickLaunch.Common
{
    public static class FileFinderHelper
    {
        public static string GetKnownActualPathToExe(string secondaryFilePathSegment, string executableFileToBrowseFor, bool multipleSecondaryFilePathSegments = true)
        {
            var searchPaths = GetSearchPathsForThirdPartyExe(secondaryFilePathSegment, executableFileToBrowseFor, multipleSecondaryFilePathSegments);

            foreach (var searchPath in searchPaths)
            {
                if (File.Exists(searchPath))
                {
                    return searchPath;
                }
            }

            return null;
        }

        private static IEnumerable<string> GetSearchPathsForThirdPartyExe(string secondaryFilePathSegment, string executableFileToBrowseFor, bool multipleSecondaryFilePathSegments)
        {
            var searchPaths = new List<string>();

            var defaultPath = GetDefaultActualPathToExe(executableFileToBrowseFor, secondaryFilePathSegment);
            searchPaths.Add(defaultPath);

            var paths = GetSpecialFoldersPlusThirdPartyExePath(executableFileToBrowseFor, secondaryFilePathSegment).ToList();
            searchPaths.AddRange(paths);

            searchPaths = DoubleUpForDDrive(searchPaths).ToList();

            if (multipleSecondaryFilePathSegments)
            {
                searchPaths = DoubleUpForMultipleSecondaryFilePathSegments(searchPaths, secondaryFilePathSegment).ToList();
            }

            return searchPaths;
        }

        private static IList<string> GetSpecialFoldersPlusThirdPartyExePath(string executableFileToBrowseFor, string secondaryFilePathSegment)
        {
            #region
            //var allSpecialFolders = new List<String>();
            //for (int i = 0; i <= 59; i++)
            //{
            //    if (i != 1 && i != 3 && i != 4 && i != 10 && i != 12 && i != 15 && i != 18 && i != 29 && i != 30 && i != 31 && i != 32 && i != 49 && i != 50 && i != 51 && i != 52)
            //    {
            //        var specialFolder = (Environment.SpecialFolder)i;
            //        var initialFolderPath = Environment.GetFolderPath(specialFolder);
            //        allSpecialFolders.Add(initialFolderPath);
            //    }
            //}
            //allSpecialFolders.Sort();
            #endregion

            var paths = new List<string>();

            if (!string.IsNullOrEmpty(executableFileToBrowseFor))
            {
                var initialFolderPaths = new List<string>();

                #region gregtLO add this section to OIA.sln
                var pathVariable = Environment.GetEnvironmentVariable("path");
                //if (!string.IsNullOrEmpty(pathVariable))
                //{
                //    var pathVariables = pathVariable.Split(';');
                //    foreach (var path in pathVariables)
                //    {
                //        //if (path.EndsWith(secondaryFilePathSegment))
                //        //{
                //        //    var trimmedPath = path.Substring(0, path.Length - secondaryFilePathSegment.Length);
                //        //    initialFolderPaths.Add(trimmedPath);
                //        //}
                //    }
                //}
                #endregion

                //set up array of the four special folders
                var initialFolders = new List<InitialFolderType>
                    {
                        InitialFolderType.ProgramFilesX86,
                        InitialFolderType.LocalApplicationData,
                        InitialFolderType.Windows
                    };
                foreach (var initialFolder in initialFolders)
                {
                    var specialFolder = (Environment.SpecialFolder)initialFolder;
                    var initialFolderPath = Environment.GetFolderPath(specialFolder);
                    initialFolderPaths.Add(initialFolderPath);
                    //if x86 add in the non-x86 too
                    if (initialFolder == InitialFolderType.ProgramFilesX86)
                    {
                        var x86 = " (x86)";
                        var initialFolderPathshWithoutx86 = initialFolderPath.Replace(x86, string.Empty);
                        initialFolderPaths.Add(initialFolderPathshWithoutx86);
                    }
                }

                foreach (var folderPath in initialFolderPaths)
                {
                    string path;
                    if (string.IsNullOrEmpty(secondaryFilePathSegment))
                    {
                        path = Path.Combine(folderPath, executableFileToBrowseFor);
                    }
                    else
                    {
                        path = Path.Combine(folderPath, secondaryFilePathSegment, executableFileToBrowseFor);
                    }
                    paths.Add(path);
                }
            }

            return paths;
        }

        private static IEnumerable<string> DoubleUpForDDrive(IEnumerable<string> searchPaths)
        {
            var dPaths = new List<string>();

            foreach (var path in searchPaths)
            {
                var dPath = path.Replace("C:", "D:");
                dPaths.Add(dPath);
            }

            return searchPaths.Union(dPaths);
        }

        private static IEnumerable<string> DoubleUpForMultipleSecondaryFilePathSegments(IEnumerable<string> searchPaths, string secondaryFilePathSegment)//gregtLO unit test reqd
        {
            var nbrPaths = new List<string>();

            foreach (var path in searchPaths)
            {
                for (var i = 9; i > 0; i--)
                {
                    var nbrPath = path.Replace($"\\{secondaryFilePathSegment}\\", $"\\{secondaryFilePathSegment}{i}\\");
                    nbrPaths.Add(nbrPath);
                }
            }

            return searchPaths.Union(nbrPaths);
        }

        private static string GetDefaultActualPathToExe(string executableFileToBrowseFor, string secondaryFilePathSegment)
        {
            var local = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            local = local.Replace("Roaming", @"Local\Programs");
            return $@"{local}\{secondaryFilePathSegment}\{executableFileToBrowseFor}";
        }
    }
}