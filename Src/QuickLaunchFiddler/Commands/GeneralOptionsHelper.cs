using QuickLaunch.Common;
using QuickLaunch.Fiddler.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuickLaunch.Fiddler
{
    public class GeneralOptionsHelper
    {
        public static void InvokeApplication(string actualPathToExe, string extensionName, string optionsName)
        {
            var invokeCommand = false;

            var fileNotKnown = string.IsNullOrEmpty(actualPathToExe) || !File.Exists(actualPathToExe);

            if (fileNotKnown)
            {
                var persistOptionsDto = new FilePrompterHelper(extensionName, actualPathToExe).PromptForActualExeFile(actualPathToExe);

                if (persistOptionsDto.Persist)
                {
                     PersistVSToolOptions(persistOptionsDto.ValueToPersist);
                }

                actualPathToExe = persistOptionsDto.ValueToPersist;

                var fileKnown = !string.IsNullOrEmpty(actualPathToExe) && File.Exists(actualPathToExe);

                if (fileKnown)
                {
                    invokeCommand = true;
                }
            }
            else
            {
                invokeCommand = true;
            }

            if (invokeCommand)
            {
                InvokeCommand(actualPathToExe, useShellExecute: true, processWithinProcess: true);
            }
            else
            {
                new FilePrompterHelper(extensionName, actualPathToExe).InformMissingActualExeFile(actualPathToExe, optionsName);
            }
        }

        public static void PersistVSToolOptions(string fileName)
        {
            Task.Run(async () =>
            {
                var generalOptions = await GeneralOptions.GetLiveInstanceAsync();
                generalOptions.ActualPathToExe = fileName;
                await generalOptions.SaveAsync();
            });
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

        //gregt this needs to be called in order to supply the default executable location
        public static string GetActualPathToExe(string secondaryFilePathSegment, string executableFileToBrowseFor, bool multipleSecondaryFilePathSegments = false)
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

            var paths = GetSpecialFoldersPlusThirdPartyExePath(executableFileToBrowseFor, secondaryFilePathSegment).ToList();
            searchPaths.AddRange(paths);

            searchPaths = DoubleUpForDDrive(searchPaths).ToList();

            if (multipleSecondaryFilePathSegments)
            {
                searchPaths = DoubleUpForMultipleSecondaryFilePathSegments(searchPaths, secondaryFilePathSegment).ToList();
            }

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
                if (!string.IsNullOrEmpty(pathVariable))
                {
                    var pathVariables = pathVariable.Split(';');
                    foreach (var path in pathVariables)
                    {
                        if (path.EndsWith(secondaryFilePathSegment))
                        {
                            var trimmedPath = path.Substring(0, path.Length - secondaryFilePathSegment.Length);
                            initialFolderPaths.Add(trimmedPath);
                        }
                    }
                }
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
    }
}