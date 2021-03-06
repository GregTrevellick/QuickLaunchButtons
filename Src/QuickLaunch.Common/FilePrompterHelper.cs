﻿using System;
using System.Windows.Forms;

namespace QuickLaunch.Common
{
    public class FilePrompterHelper
    {
        private string caption { get; set; }
        private string executableFileToBrowseFor { get; set; }

        public FilePrompterHelper(string caption, string executableFileToBrowseFor)
        {
            this.caption = caption;
            this.executableFileToBrowseFor = executableFileToBrowseFor;
        }

        public PersistOptionsDto PromptForActualExeFile(string originalPathToFile)
        {
            var saveSettingsDto = new PersistOptionsDto();

            var box = MessageBox.Show(
               CommonConstants.PromptForActualExeFile(originalPathToFile),
               caption,
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);

            switch (box)
            {
                case DialogResult.Yes:
                    var resultAndNamePicked = BrowseFileHelper.BrowseToFileLocation(executableFileToBrowseFor);
                    if (resultAndNamePicked.DialogResult == DialogResult.OK)
                    {
                        SetSaveSettingsDto(saveSettingsDto, resultAndNamePicked.FileNameChosen);
                    }
                    break;
                case DialogResult.No:
                    SetSaveSettingsDto(saveSettingsDto, originalPathToFile);
                    break;
            }

            return saveSettingsDto;
        }

        public void InformMissingActualExeFile(string missingFileName, string optionsName)
        {
            MessageBox.Show(
                CommonConstants.InformMissingActualExeFile(missingFileName, optionsName),
                caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Question);
        }

        public void InformUnexpectedError(Exception ex)
        {
            MessageBox.Show(
                CommonConstants.UnexpectedError +
                   Environment.NewLine +
                   Environment.NewLine +
                   ex?.InnerException?.Message,
                caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Question);
        }

        private void SetSaveSettingsDto(PersistOptionsDto saveSettingsDto, string fileName)
        {
            saveSettingsDto.ValueToPersist = fileName;
            saveSettingsDto.Persist = true;
        }
    }
}
