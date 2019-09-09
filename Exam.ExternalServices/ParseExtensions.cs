using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Exam.ExternalServices
{
    public static class ParseExtensions
    {

        public static string Serialize<T>(this T model)
        {
            return JsonConvert.SerializeObject(model);
        }

        public static MemoryStream ConvertToMemoryStream(this string base64)
        {
            var imageBytes = Convert.FromBase64String(base64);
            var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            return ms;
        }

        public static string GetQueryString(this object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }
    }
}