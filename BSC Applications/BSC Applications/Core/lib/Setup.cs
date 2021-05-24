using System.Collections.Generic;
using System.Security.Principal;
using Windows.Storage;

namespace BSC_Applications.Core.lib
{
    class Setup
    {
        private static ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

        public Setup()
        {
            if (!roamingSettings.Values.ContainsKey("new"))
            {
                roamingSettings.Values["displayName"] = WindowsIdentity.GetCurrent().Name.Split("\\")[1];
                roamingSettings.Values["checkForUpdates"] = true;
                roamingSettings.Values["temporaryContent"] = true;

                roamingSettings.Values["sound"] = false;
                roamingSettings.Values["theme"] = 2;

                roamingSettings.Values["new"] = false;
            }
        }
    }
}
