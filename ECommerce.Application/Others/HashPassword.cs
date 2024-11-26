using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Others
{
    public class HashPassword
    {
        public static string HashPasswordThrice(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var firstHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var secondHash = sha256.ComputeHash(firstHash);
                var thirdHash = sha256.ComputeHash(secondHash);
                return Convert.ToBase64String(secondHash);
            }
        }
    }
}
