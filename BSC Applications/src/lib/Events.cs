/*
 * DESCRIPTION:
 * This file is used to debug BSC Applications.
 */
using System.Diagnostics;

namespace BSC_Applications.src.lib
{
    class Events
    {
        public static string events = "\"Info\",\"BSC Applications Events:\"\n\"NaN\",\"---\"\n";

        // types: 0 - info, 1 - warning, 2 - error
        public Events(string message, int type)
        {
            string _message;
            if (type == 0) _message = $"\"Info\",\"{message}\"\n";
            else if(type == 1) _message = $"\"Warn\",\"{message}\"\n";
            else _message = $"\"Erro\",\"{message}]\"\n";

            Debug.WriteLine(_message);
            events += _message;
        }
    }
}
