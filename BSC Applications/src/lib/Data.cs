/*
 * DESCRIPTION:
 * This file lets us easily get basic info about the app.
 * e.g Name, Version, Type.
 */

namespace BSC_Applications.src.lib
{
    class Data
    {
        private static Windows.ApplicationModel.Package package = Windows.ApplicationModel.Package.Current;
        private static Windows.ApplicationModel.PackageId packageId = package.Id;

        // App Info
        private static string name = package.DisplayName;
        private static Windows.ApplicationModel.PackageVersion version = packageId.Version;

        public static string Name
        {
            get { return name; }
        }
        public static string Version
        {
            get { return $"{version.Major}.{version.Minor}.{version.Build}"; }
        }
        public static int iVersion
        {
            get { return int.Parse($"{version.Major}{version.Minor}{version.Build}"); }
        }
        public static string Type
        {
            get
            {
                string[] verValues = Version.Split(".");
                if (verValues[1] == "0" && verValues[2] == "0")
                    return "Major";
                else if (verValues[2] == "0")
                    return "Beta";
                else
                    return "Build";
            }
        }
    }
}
