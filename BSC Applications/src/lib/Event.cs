/*
 * DESCRIPTION:
 * This file is used to debug BSC Applications.
 */
using System.Diagnostics;

namespace BSC_Applications.src.lib
{
    class Event
    {
        public static string events = "\"\",\"BSC Applications Events:\"\n\"\",\"---\"\n";

        public static int none = 0;
        public static int load = 1;
        public static int info = 2;
        public static int warn = 3;
        public static int error = 4;

        public Event(string message, int type)
        {
            string sType = "";
            string sMessage;

            switch (type)
            {
                case 0: sType = ""; break;
                case 1: sType = "load"; break;
                case 2: sType = "info"; break;
                case 3: sType = "warn"; break;
                case 4: sType = "error"; break;
            }

            sMessage = $"\"{sType}\",\"{message}\"\n";

            Debug.WriteLine(sMessage);
            events += sMessage;
        }
    }
}
