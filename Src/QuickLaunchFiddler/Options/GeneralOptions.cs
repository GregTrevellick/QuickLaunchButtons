using System.ComponentModel;
using Microsoft.VisualStudio.Shell;
using QuickLaunch.Common;

namespace QuickLaunch.Fiddler.Options
{
    public class GeneralOptions : DialogPage
    {
        private const string CommonActualPathToExeOptionLabel = CommonConstants.ActualPathToExeOptionLabelPrefix + CommonConstants.AppFiddler;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonActualPathToExeOptionLabel)]
        [Description(CommonConstants.ActualPathToExeOptionDetailedDescription)]
        public string ActualPathToExe { get; set; }

        public override void LoadSettingsFromStorage()
        {
            base.LoadSettingsFromStorage();

            if (string.IsNullOrEmpty(ActualPathToExe))
            {
                ActualPathToExe = GeneralOptionsHelper.GetActualPathToExe("Fiddler2", CommonConstants.AppFiddler + CommonConstants.DefaultExecutableFileSuffix);
            }

            previousActualPathToExe = ActualPathToExe;
        }

        private string previousActualPathToExe { get; set; }

        protected override void OnApply(PageApplyEventArgs e)
        {
            var actualPathToExeChanged = false;

            if (ActualPathToExe != previousActualPathToExe)
            {
                actualPathToExeChanged = true;
                previousActualPathToExe = ActualPathToExe; 
            }

            if (actualPathToExeChanged)
            {
                if (!ArtefactsHelper.DoesActualPathToExeExist(ActualPathToExe))
                {
                    e.ApplyBehavior = ApplyKind.Cancel;

                    var caption = new ConstantsForAppCommon().Caption;

                    var filePrompterHelper = new FilePrompterHelper(caption, CommonConstants.AppFiddler);

                    var persistOptionsDto = filePrompterHelper.PromptForActualExeFile(ActualPathToExe);

                    if (persistOptionsDto.Persist)
                    {
                        PersistVSToolOptions(persistOptionsDto.ValueToPersist);
                    }
                }
            }

            base.OnApply(e);
        }

        public void PersistVSToolOptions(string fileName)
        {
            VSPackage.Options.ActualPathToExe = fileName;
            VSPackage.Options.SaveSettingsToStorage();
        }
    }
}