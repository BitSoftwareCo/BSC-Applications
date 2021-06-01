/*
 * DESCRIPTION:
 * This file wites and reads all of our custiom file types(.BOF).
 */
using System.Collections.Generic;

namespace BSC_Applications.Core.lib
{
    class FileType
    {
        public static object WriteBOF(object[] data)
        {
            string final = "{ ";
            for (int i = 0; i < data.Length; i++)
            {
                final += $"{data[i]}, ";
            }
            final += "}";
            return final;
        }

        public static object[] ReadBOF(object data)
        {
            string[] format = data.ToString().Replace("{ ", "")
                                              .Split(", ");
            List<string> final = new List<string>();
            for (int i = 0; i < format.Length; i++)
            {
                if (!format[i].Contains("}"))
                {
                    final.Add(format[i]);
                }
            }

            return final.ToArray();
        }
    }
}
