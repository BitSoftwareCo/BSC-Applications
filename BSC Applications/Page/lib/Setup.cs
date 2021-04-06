using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace BSC_Applications.Page.lib
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
                roamingSettings.Values["displayName"] = name[1];

                roamingSettings.Values["new"] = false;
            }
        }
    }
}
