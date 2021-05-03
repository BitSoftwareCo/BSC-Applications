using Windows.ApplicationModel;

namespace BSC_Applications.Core.lib
{
    class Data
    {
        private static Package package = Package.Current;
        private static PackageId packageId = package.Id;

        // App Info
        private static string name = package.DisplayName;
        private static PackageVersion version = packageId.Version;

        public static string Name
        {
            get { return name; }
        }
        public static string Version
        {
            get { return $"{version.Major}.{version.Minor}.{version.Build}"; }
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

        // Publisher Info
        private static string publisher = package.PublisherDisplayName;
        private static string website = "https://bitsoftwareco.github.io/";
        private static string docs = "https://bitsoftwareco.github.io/docs/BSC-Applications.html";

        public static string Publisher
        {
            get { return publisher; }
        }
        public static string Website
        {
            get { return website; }
        }
        public static string Docs
        {
            get { return docs; }
        }
    }
}
