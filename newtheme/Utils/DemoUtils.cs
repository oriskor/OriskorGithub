﻿using System.Web.Script.Serialization;

namespace EDI.Utils
{
    public static class DemoUtils
    {
        public static string Encode(object input)
        {
            return new JavaScriptSerializer().Serialize(input);
        }
    }
}