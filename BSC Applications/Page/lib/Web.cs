using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BSC_Applications.Page.lib
{
    class Web
    {
        public static string[] package;

        public static void Set()
        {
            using (WebClient wc = new WebClient())
            {
                var webPackage = wc.DownloadString("https://raw.githubusercontent.com/BitSoftwareCo/BSC-Applications/main/package");
                package = webPackage.Split(" - ");
            }
        }
    }
}
