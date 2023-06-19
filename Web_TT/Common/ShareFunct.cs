using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Web_TT.Models;

namespace Web_TT.Common
{
    public class ShareFunct
    {
        // HashMD5
        public static string CreateMD5(string input)
        {

            // byte array representation of that string
            byte[] encodedPassword = new UTF8Encoding().GetBytes(input);

            // need MD5 to calculate the hash
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            // string representation (similar to UNIX format)
            string encoded = BitConverter.ToString(hash)
               // without dashes
               .Replace("-", string.Empty)
               // make lowercase
               .ToLower();

            // encoded contains the hash you want

            return encoded;
        }

        // Check Login()
        public static bool isLogin(DatabaseDataContext db, string adminId)
        {
            var obj = db.tbl_TaiKhoans.FirstOrDefault(o =>
                o.TaiKhoan == adminId && (o.Is_Delete == null || o.Is_Delete == 0));

            if (obj != null && obj.Is_Login == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}