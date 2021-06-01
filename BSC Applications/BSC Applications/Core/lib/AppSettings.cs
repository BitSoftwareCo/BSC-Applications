/*
 * DESCRIPTION:
 * This file is used when BSC Applications needs to view or change Roaming Settings.
 */
using System;
using System.Security.Principal;
using Windows.Storage;

namespace BSC_Applications.Core.lib
{
    class AppSettings
    {
        private static ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

        // User
        public string DisplayName
        {
            get { return roamingSettings.Values["displayName"] as string; }
            set { roamingSettings.Values["displayName"] = value; }
        }
        public bool TemporaryContent
        {
            get { return Boolean.Parse(roamingSettings.Values["temporaryContent"].ToString()); }
            set { roamingSettings.Values["temporaryContent"] = value; }
        }

        // Personalization
        public bool Sound
        {
            get { return Boolean.Parse(roamingSettings.Values["sound"].ToString()); }
            set { roamingSettings.Values["sound"] = value; }
        }
        public int Theme
        {
            get { return int.Parse(roamingSettings.Values["theme"].ToString()); }
            set { roamingSettings.Values["theme"] = value; }
        }
        public bool NavbarLocation
        {
            get { return Boolean.Parse(roamingSettings.Values["navbarLocation"].ToString()); }
            set { roamingSettings.Values["navbarLocation"] = false; }
        }
        public bool BackgroundAcrylic
        {
            get { return Boolean.Parse(roamingSettings.Values["backgroundAcrylic"].ToString()); }
            set { roamingSettings.Values["backgroundAcrylic"] = false; }
        }

        // Other
        public bool CheckForUpdates
        {
            get { return Boolean.Parse(roamingSettings.Values["checkForUpdates"].ToString()); }
            set { roamingSettings.Values["checkForUpdates"] = value; }
        }

        public static AppSettings All
        {
            get
            {
                AppSettings appSettings = new AppSettings
                {
                    DisplayName = roamingSettings.Values["displayName"] as string,
                    TemporaryContent = Boolean.Parse(roamingSettings.Values["temporaryContent"].ToString()),
                    Sound = Boolean.Parse(roamingSettings.Values["sound"].ToString()),
                    Theme = int.Parse(roamingSettings.Values["theme"].ToString()),
                    NavbarLocation = Boolean.Parse(roamingSettings.Values["navbarLocation"].ToString()),
                    BackgroundAcrylic = Boolean.Parse(roamingSettings.Values["backgroundAcrylic"].ToString()),
                    CheckForUpdates = Boolean.Parse(roamingSettings.Values["checkForUpdates"].ToString())
                };
                return appSettings;
        }
        }

        public AppSettings()
        {
            if (!roamingSettings.Values.ContainsKey("new"))
            {
                roamingSettings.Values["displayName"] = WindowsIdentity.GetCurrent().Name.Split("\\")[1];
                roamingSettings.Values["temporaryContent"] = true;

                roamingSettings.Values["sound"] = false;
                roamingSettings.Values["theme"] = 2;                 // 0     - light      | 1     - dark    | 2 - default |
                roamingSettings.Values["navbarLocation"] = false;    // false - left       | true  - top     | ----------- |
                roamingSettings.Values["backgroundAcrylic"] = false; // false - No Acrylic | true  - Acrylic | ----------- |

                roamingSettings.Values["checkForUpdates"] = true;

                roamingSettings.Values["new"] = false;
            }
        }
    }
}
