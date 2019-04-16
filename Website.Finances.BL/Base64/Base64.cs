using System;
using System.Text;

namespace Website.Finances.BL.Base64
{
    public class Base64
    {
        public static string FromBase64(string base64String) 
            => FromBase64(base64String, Encoding.UTF8);

        public static string FromBase64(string base64String, Encoding encoding) 
            => encoding.GetString(Convert.FromBase64String(base64String));
    }
}
