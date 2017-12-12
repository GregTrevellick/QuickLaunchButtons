using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace QuickLaunch.Common
{
    public class GeneralOptionsHelper
    {
        ////////////////////////public static void InvokeApplication(string secondaryFilePathSegment, string executableFileToBrowseFor)
        ////////////////////////{
        ////////////////////////    var actualPathToExe = GetActualPathToExe(secondaryFilePathSegment, executableFileToBrowseFor);
        ////////////////////////    InvokeCommand(actualPathToExe, useShellExecute: true, processWithinProcess: true);
        ////////////////////////}

        public static void InvokeApplication(string actualPathToExe)
        {
            InvokeCommand(actualPathToExe, useShellExecute: true, processWithinProcess: true);
        }

        private static void InvokeCommand(string executableFullPath, bool useShellExecute, bool processWithinProcess)
        {
            string fileName;
            string workingDirectory = string.Empty;

            if (useShellExecute)
            {
                fileName = Path.GetFileName(executableFullPath);
                workingDirectory = Path.GetDirectoryName(executableFullPath);
            }
            else
            {
                fileName = executableFullPath;
            }

            InvokeProcess(string.Empty, fileName, useShellExecute, workingDirectory, processWithinProcess);
        }

        public static string GetActualPathToExe(string secondaryFilePathSegment, string executableFileToBrowseFor)
        {
            var searchPaths = GetSearchPathsForThirdPartyExe(secondaryFilePathSegment, executableFileToBrowseFor);

            foreach (var searchPath in searchPaths)
            {
                if (File.Exists(searchPath))
                {
                    return searchPath;
                }
            }

            ////////////////////////////////////////return @"C:\Users\gtrev\AppData\Local\Programs\Fiddler\Fiddler.exe";
            return null;
        }

        private static IEnumerable<string> GetSearchPathsForThirdPartyExe(string secondaryFilePathSegment, string executableFileToBrowseFor)
        {
            var searchPaths = new List<string>();

            var paths = GetSpecialFoldersPlusThirdPartyExePath(executableFileToBrowseFor, secondaryFilePathSegment).ToList();
            searchPaths.AddRange(paths);

            searchPaths = DoubleUpForDDrive(searchPaths).ToList();

            return searchPaths;
        }

        private static void InvokeProcess(string arguments, string fileName, bool useShellExecute, string workingDirectory, bool processWithinProcess)
        {
            var start = new ProcessStartInfo()
            {
                Arguments = arguments,
                CreateNoWindow = true,
                FileName = fileName,
                UseShellExecute = useShellExecute,
                WindowStyle = ProcessWindowStyle.Hidden,
                WorkingDirectory = workingDirectory
            };

            try
            {
                if (processWithinProcess)
                {
                    var startNoArgs = new ProcessStartInfo()
                    {
                        CreateNoWindow = true,
                        FileName = fileName,
                        UseShellExecute = useShellExecute,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        WorkingDirectory = workingDirectory
                    };

                    using (var proc = Process.Start(startNoArgs))
                    {
                        Thread.Sleep(3000);//TODO use async ?
                        using (Process.Start(start)) { }
                    }
                }
                else
                {
                    using (Process.Start(start)) { }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private static IList<string> GetSpecialFoldersPlusThirdPartyExePath(string executableFileToBrowseFor, string secondaryFilePathSegment)
        {
            var paths = new List<string>();

            if (!string.IsNullOrEmpty(executableFileToBrowseFor))
            {
                //set up array of the four special folders
                var initialFolders = new List<InitialFolderType>
                {
                    InitialFolderType.ProgramFilesX86,
                    InitialFolderType.LocalApplicationData,
                    InitialFolderType.Windows
                };
                var initialFolderPaths = new List<string>();
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

            var result = searchPaths.Union(dPaths);
            return result;
        }
    }
}