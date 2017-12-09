using Microsoft.VisualStudio.Shell;
//using OpenInAbracadabra.Options.Abracadabra;
//using OpenInApp.Common.Helpers;
//using OpenInApp.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using static System.Environment;

namespace QuickLaunchFiddler.Commands
{
    internal sealed class QuickButtonCommand
    {
        public const int CommandId = 0x0100;

        public static readonly Guid CommandSet = new Guid("7aaba3a9-97d0-41d2-b4c4-b543912979a0");//gregt PackageGuids.guidQuickButtonCommandPackageCmdSetString;

        private readonly Package package;

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
                MenuCommand menuItem = new MenuCommand(this.StartNotepad, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        private void StartNotepad(object sender, EventArgs e)
        {
            //Process proc = new Process();
            //proc.StartInfo.FileName = "notepad.exe";
            //proc.Start();

            var actualPathToExe = GetActualPathToExe();
            InvokeCommand(actualPathToExe, useShellExecute: true, processWithinProcess: true);
        }

        public static QuickButtonCommand Instance
        {
            get;
            private set;
        }

        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        public static void Initialize(Package package)
        {
            Instance = new QuickButtonCommand(package);
        }


        public static void InvokeCommand(
            string executableFullPath,
            bool useShellExecute,
            bool processWithinProcess)
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





        public static string GetActualPathToExe()
        {
            var searchPaths = GetSearchPathsForThirdPartyExe();

            foreach (var searchPath in searchPaths)
            {
                if (File.Exists(searchPath))
                {
                    return searchPath;
                }
            }

            return null;
        }



        internal static IEnumerable<string> GetSearchPathsForThirdPartyExe()
        {
            var searchPaths = new List<string>();

            var secondaryFilePathSegment = "Fiddler2";
            var executableFileToBrowseFor = "Fiddler.exe";

            var paths = GetSpecialFoldersPlusThirdPartyExePath(executableFileToBrowseFor, secondaryFilePathSegment).ToList();
            searchPaths.AddRange(paths);

            searchPaths = DoubleUpForDDrive(searchPaths).ToList();

            return searchPaths;
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
                    var specialFolder = (SpecialFolder)initialFolder;
                    var initialFolderPath = GetFolderPath(specialFolder);
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


        public enum InitialFolderType
        {
            None = 0,
            LocalApplicationData = 28,
            ProgramFilesX86 = 42,
            Windows = 36,
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