/*
 * DESCRIPTION:
 * This file is used to reads and returns https://github.com/BitSoftwareCo/BSC-Applications/blob/main/package.json.
 * 
 */
using Newtonsoft.Json;
using System;
using System.Net;

namespace BSC_Applications.src.lib
{
    class Package
    {
        private static wPackage package;

        public static int Set()
        {
            try
            {
                WebClient webClient = new WebClient();
                string webPackage = webClient.DownloadString("https://raw.githubusercontent.com/BitSoftwareCo/BSC-Applications/main/package.json");
                package = JsonConvert.DeserializeObject<wPackage>(webPackage);

                return 0;
            }
            catch (Exception e)
            {
                new Events($"Error! {e.Message}", 2);
                return 1;
            }
        }

        public static string type 
        {
            get
            {
                return package.currentType;
            }
        }
        public static int version 
        {
            get
            {
                string[] sPackage = package.currentVersion.Split(".");
                return int.Parse($"{sPackage[0]}{sPackage[1]}{sPackage[2]}");
            }
        }
        public static string sVersion
        {
            get
            {
                return package.currentVersion;
            }
        }
        public static string publisher
        {
            get
            {
                return package.currentPublisher;
            }
        }
        public static string changelog
        {
            get
            {
                return package.currentChangeLog;
            }
        }
    }

    class wPackage
    {
        public string currentType { get; set; }
        public string currentVersion { get; set; }
        public string currentPublisher { get; set; }
        public string currentChangeLog { get; set; }
    }
}
