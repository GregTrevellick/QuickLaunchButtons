using QuickLaunch.Common;
using QuickLaunch.Fiddler.Options;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace QuickLaunch.Fiddler.Commands
{
    public static class InvokerHelper
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
    }
}