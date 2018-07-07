using System;
using System.Security.Cryptography;
using System.Text;

namespace Eagles.Base.Md5Helper.Implement
{
    public class Md5Helper: IMd5Helper
    {
        public string Md5Encypt(string str)
        {
            var md5 = new MD5CryptoServiceProvider();
            var t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(str)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }
    }
}
