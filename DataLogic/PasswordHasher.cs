using Konscious.Security.Cryptography;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ElevDB.DataLogic
{
    public class PasswordHasher
    {
        // De hashing værdier der skal anvendes indlæses fra appsettings.json
        private static int parallelismValue = Int32.Parse(Startup.StaticConfig["Nextcloud:Values:PasswordHashing:Parallelism"]);
        private static int memorySizeValue = Int32.Parse(Startup.StaticConfig["Nextcloud:Values:PasswordHashing:MemorySize"]);
        private static int iterationsValue = Int32.Parse(Startup.StaticConfig["Nextcloud:Values:PasswordHashing:Iterations"]);

        private byte[] CreateSalt() // Der genereres en salt til password hashingen. Denne vil blive gemt sammen med det hashede password.
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }

        private byte[] HashPassword(string password, byte[] salt, int parallelism, int memorySize, int iterations)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)) // Argon2 bruges til at hashe passwordet, ved metode kald kraves password og salt samt følgende argon2 specifikke parametre:
            {                                                           // Parallelism = Antal CPU tråde; MemorySize = RAM til hashing; Iterations = Antal gange hashprocessen køres igennem;
                Salt = salt,
                DegreeOfParallelism = parallelism,
                Iterations = iterations,
                MemorySize = memorySize
            };

            return argon2.GetBytes(16);
        }
        public string HashPasswordForSQL(string password) // Metoden der kan kaldes eksternt, kræver password ved kald. Den foretager de andre metoder i klassen.
        {
            var salt = CreateSalt();
            var hash = HashPassword(password, salt, parallelismValue, memorySizeValue, iterationsValue);

            var saltString = Convert.ToBase64String(salt);
            var hashString = Convert.ToBase64String(hash);
            var saltStringTrimmed = saltString[0..^2]; // Salt og hash string trimmes så de sidste to karakter fjernes, der er tale om to == som ikke skal bruges til passwordhashing.
            var hashStringTrimmed = hashString[0..^2];
            // SQL password strengen skrives så den kan bruges i Nextcloud SQL databasen. Der skrives hvilken argon version samt hashing metoden. Derudover gemmes samtlige værdier fra hashings processen.
            var passwordForSql = $"3|$argon2id$v=19$m={memorySizeValue},t={iterationsValue},p={parallelismValue}${saltStringTrimmed}${hashStringTrimmed}"; 
            return passwordForSql;
        }
    }
}
