/*
 * DESCRIPTION:
 * This file is used when BSC Applications needs to view or change Roaming Settings.
 */
using System.Security.Principal;
using Windows.Storage;

namespace BSC_Applications.src.lib
{
    class AppSettings
    {
        private static ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

        public AppSettings()
        {
            if (!roamingSettings.Values.ContainsKey("new"))
            {
                new Events("Loaded App Settings", 0);

                roamingSettings.Values["displayName"] = WindowsIdentity.GetCurrent().Name.Split("\\")[1];
                roamingSettings.Values["temporaryContent"] = true;

                roamingSettings.Values["sound"] = false;
                roamingSettings.Values["navbarLocation"] = false;      // false - left          | true  - top     | ----------- |
                roamingSettings.Values["backgroundAcrylic"] = false;   // false - No Acrylic    | true  - Acrylic | ----------- |
                roamingSettings.Values["backgroundColor"] = "default"; // default - Theme Color | --------------- | ----------- |

                roamingSettings.Values["new"] = true;
            }
        }

        public static void Reset()
        {
            roamingSettings.Values["displayName"] = WindowsIdentity.GetCurrent().Name.Split("\\")[1];
            roamingSettings.Values["temporaryContent"] = true;

            roamingSettings.Values["sound"] = false;
            roamingSettings.Values["navbarLocation"] = false;      // false - left          | true  - top     | ----------- |
            roamingSettings.Values["backgroundAcrylic"] = false;   // false - No Acrylic    | true  - Acrylic | ----------- |
            roamingSettings.Values["backgroundColor"] = "default"; // default - Theme Color | --------------- | ----------- |

            roamingSettings.Values["new"] = true;
        }
    }
}
