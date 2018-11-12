namespace QuickLaunch.Fiddler.Options
{
    public class GeneralOptions : BaseOptionModel<GeneralOptions>
    {
		public string ActualPathToExe { get; set; } ////// = GeneralOptionsHelper.GetDefaultActualPathToExe(true);
	}
}
