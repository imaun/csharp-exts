using System.Security.Cryptography;

namespace Roonia.Extensions
{
    public static class HashingExts
    {
        
        public static string HashString(this string input, string salt = null) {
            
            using (var md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(i.ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

    }
}