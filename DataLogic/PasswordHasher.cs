using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ElevDB.DataLogic
{
    public class PasswordHasher
    {
        private static int parallelismValue = Int32.Parse(Startup.StaticConfig["Nextcloud:Values:PasswordHashing:Parallelism"]);
        private static int memorySizeValue = Int32.Parse(Startup.StaticConfig["Nextcloud:Values:PasswordHashing:MemorySize"]);
        private static int iterationsValue = Int32.Parse(Startup.StaticConfig["Nextcloud:Values:PasswordHashing:Iterations"]);


        private byte[] CreateSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }

        private byte[] HashPassword(string password, byte[] salt, int parallelism, int memorySize, int iterations)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = parallelism,
                Iterations = iterations,
                MemorySize = memorySize
            };

            return argon2.GetBytes(16);
        }

        public string HashPasswordForSQL(string password)
        {
            var salt = CreateSalt();
            var hash = HashPassword(password, salt, parallelismValue, memorySizeValue, iterationsValue);

            var saltString = Convert.ToBase64String(salt);
            var hashString = Convert.ToBase64String(hash);
            var saltStringTrimmed = saltString[0..^2];
            var hashStringTrimmed = hashString[0..^2];

            var passwordForSql = $"3|$argon2id$v=19$m={memorySizeValue},t={iterationsValue},p={parallelismValue}${saltStringTrimmed}${hashStringTrimmed}";
            return passwordForSql;
        }
    }
}
