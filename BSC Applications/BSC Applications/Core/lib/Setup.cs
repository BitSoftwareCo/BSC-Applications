using System.Security.Principal;
using Windows.Storage;

namespace BSC_Applications.Core.lib
{
    class Setup
    {
        ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

        public Setup()
        {
            if (!roamingSettings.Values.ContainsKey("new"))
            {
                string[] name = WindowsIdentity.GetCurrent().Name.Split("\\");

                roamingSettings.Values["sound"] = true;
                roamingSettings.Values["safemode"] = false;
                roamingSettings.Values["displayName"] = name[1];
                roamingSettings.Values["checkForUpdates"] = true;
                roamingSettings.Values["theme"] = 2;

                roamingSettings.Values["new"] = true;
            }
        }
    }
}
