using System;
using System.Diagnostics;
using System.Net;

namespace BSC_Applications.Core.lib
{
    class Web
    {
        public static string[] package;

        public static int Set()
        {
            try
            {
                WebClient webClient = new WebClient();
                string webPackage = webClient.DownloadString("https://raw.githubusercontent.com/BitSoftwareCo/BSC-Applications/main/package.bof");
                package = (string[])FileType.ReadBOF(webPackage);
                
                return 0;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception in Web.cs: {e}");
                return 1;
            }
        }
    }
}
