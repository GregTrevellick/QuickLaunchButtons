namespace QuickLaunch.Common
{
    public class ConstantsForAppCommon
    {
        private string _vsixName;
        private string _vsixVersion;

        public ConstantsForAppCommon()
        {
        }

        public ConstantsForAppCommon(string vsixName, string vsixVersion)//gregt leverage this
        {
            _vsixName = vsixName;
            _vsixVersion = vsixVersion;
        }

        public string Caption 
        { 
            get 
                { 
                    return _vsixName + " " + _vsixVersion;
                }
        }
    }
}
