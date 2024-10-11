using System.Security.Cryptography;
using System.Text;

namespace P0.APP.Util {
    public static class Security{
        public static string Hash(string value)
        {
            var byteArray = SHA256.HashData(Encoding.UTF8.GetBytes(value));
            return Convert.ToHexString(byteArray);
        }
    }
}