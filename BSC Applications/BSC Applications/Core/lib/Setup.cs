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
                roamingSettings.Values["sound"] = true;
                roamingSettings.Values["displayName"] = WindowsIdentity.GetCurrent().Name.Split("\\")[1];
                roamingSettings.Values["checkForUpdates"] = true;
                roamingSettings.Values["theme"] = 2;

                roamingSettings.Values["new"] = true;
            }
        }

        public static List<object> Settings
        {
            get
            {
                List<object> values = new List<object>();

                values.Add(roamingSettings.Values["sound"]);
                values.Add(roamingSettings.Values["displayName"]);
                values.Add(roamingSettings.Values["checkForUpdates"]);
                values.Add(roamingSettings.Values["theme"]);

                return values;
            }
        }
    }
}
