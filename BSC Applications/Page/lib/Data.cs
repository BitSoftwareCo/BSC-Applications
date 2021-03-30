using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace BSC_Applications.Page.lib
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
