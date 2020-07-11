using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Marrwie.Helpers
{
    public class FileHelper
    {
        public static string CheckLogFile(string path)
        {
            var fullPath = Path.Combine(path, "Log.txt");

            if (!System.IO.File.Exists(fullPath))
                System.IO.File.Create(fullPath);

            return fullPath;
        }
    }
}