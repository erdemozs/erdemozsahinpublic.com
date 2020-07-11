using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marrwie.Helpers
{
    public static class StringExtension
    {
        public static string CropText(this string s, int count)
        {
            if(s.Length > count)
            {
                return s.Substring(count)+"..";
            }
            return s;
        }
    }
}