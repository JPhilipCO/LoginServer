using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Logins.Utils
{
    public static class Cryptic
    {
        
        public static string GetHash(string text)
        {
            using (var sha256 = SHA256.Create())
            {

                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));

                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes);
            }
        }

        //todo: Use SALT to make the hash stronger. To create the hash, use GetHash("string-to-encrypt"+Salt).
        //store the SALT along with the hashed password.

        private static string getSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var randm = RandomNumberGenerator.Create())
            {
                randm.GetBytes(bytes);
                return BitConverter.ToString(bytes);
            }
        }
    }
}
