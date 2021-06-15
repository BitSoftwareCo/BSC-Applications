/*
 * DESCRIPTION:
 * This file is used when BSC Applications needs to view or change Roaming Settings.
 */
using System;
using System.Security.Principal;
using Windows.Storage;

namespace BSC_Applications.src.lib
{
    class AppSettings
    {
        private static ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

        // User
        public string DisplayName
        {
            get { return roamingSettings.Values["displayName"] as string; }
            set { 
                roamingSettings.Values["displayName"] = value;
                new Events($"Changed displayName to: {value}", 0);
            }
        }
        public bool TemporaryContent
        {
            get { return Boolean.Parse(roamingSettings.Values["temporaryContent"].ToString()); }
            set { 
                roamingSettings.Values["temporaryContent"] = value;
                new Events($"Changed temporaryContent to: {value}", 0);
            }
        }

        // Personalization
        public bool Sound
        {
            get { return Boolean.Parse(roamingSettings.Values["sound"].ToString()); }
            set { 
                roamingSettings.Values["sound"] = value;
                new Events($"Changed sound to: {value}", 0);
            }
        }
        public int Theme
        {
            get { return int.Parse(roamingSettings.Values["theme"].ToString()); }
            set { 
                roamingSettings.Values["theme"] = value;
                new Events($"Changed theme to: {value}", 0);
            }
        }
        public bool NavbarLocation
        {
            get { return Boolean.Parse(roamingSettings.Values["navbarLocation"].ToString()); }
            set {
                roamingSettings.Values["navbarLocation"] = false;
                new Events($"Changed navbarLocation to: {value}", 0);
            }
        }
        public bool BackgroundAcrylic
        {
            get { return Boolean.Parse(roamingSettings.Values["backgroundAcrylic"].ToString()); }
            set { 
                roamingSettings.Values["backgroundAcrylic"] = false;
                new Events($"Changed backgroundAcrylic to: {value}", 0);
            }
        }
        public string BackgroundColor
        {
            get { return roamingSettings.Values["backgroundColor"] as string; }
            set
            {
                roamingSettings.Values["backgroundColor"] = value;
                new Events($"Changed backgroundColor to: {value}", 0);
            }
        }
        public string ForegroundColor
        {
            get { return roamingSettings.Values["foregroundColor"] as string; }
            set
            {
                roamingSettings.Values["foregroundColor"] = value;
                new Events($"Changed foregroundColor to: {value}", 0);
            }
        }

        // Other
        public bool CheckForUpdates
        {
            get { return Boolean.Parse(roamingSettings.Values["checkForUpdates"].ToString()); }
            set { 
                roamingSettings.Values["checkForUpdates"] = value;
                new Events($"Changed checkForUpdates to: {value}", 0);
            }
        }
        public bool New
        {
            get { return Boolean.Parse(roamingSettings.Values["new"].ToString()); }
            set
            {
                roamingSettings.Values["new"] = value;
                new Events($"Changed new to: {value}", 0);
            }
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
                new Events("Loaded App Settings", 0);

                roamingSettings.Values["displayName"] = WindowsIdentity.GetCurrent().Name.Split("\\")[1];
                roamingSettings.Values["temporaryContent"] = true;

                roamingSettings.Values["sound"] = false;
                roamingSettings.Values["theme"] = 2;                   // 0     - light         | 1     - dark    | 2 - default |
                roamingSettings.Values["navbarLocation"] = false;      // false - left          | true  - top     | ----------- |
                roamingSettings.Values["backgroundAcrylic"] = false;   // false - No Acrylic    | true  - Acrylic | ----------- |
                roamingSettings.Values["backgroundColor"] = "default"; // default - Theme Color | --------------- | ----------- | 
                roamingSettings.Values["foregroundColor"] = "default"; // default - Theme Color | --------------- | ----------- |

                roamingSettings.Values["checkForUpdates"] = true;

                roamingSettings.Values["new"] = true;
            }
        }
    }
}
