using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;

namespace CommonModelsLib
{
    public static class Encryption
    {
        private static readonly RandomNumberGenerator Rng =
            System.Security.Cryptography.RandomNumberGenerator.Create();
        public async static Task<string> Encrypt(string pass) => Argon2.Hash(pass, timeCost: timeCost, parallelism: parallelism,
            memoryCost: memoryCost, type: type, hashLength: hashLength, secret: secretKey);

        public async static Task<string> Hash(string pass)
        {
            byte[] salt = new byte[16];
            Rng.GetBytes(salt);
            var cfg = new Argon2Config()
            {
                Type = Argon2Type.DataIndependentAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = timeCost,
                MemoryCost = memoryCost,
                Lanes = 5,
                Threads = Environment.ProcessorCount, // higher than "Lanes" doesn't help (or hurt)
                Password = Encoding.UTF8.GetBytes(pass),
                Salt = salt, // >= 8 bytes if not null
                Secret = Encoding.UTF8.GetBytes(secretKey), // from somewhere
                HashLength = hashLength // >= 4
            };
            var argon2A = new Argon2(cfg);
            using SecureArray<byte> hashA = argon2A.Hash();
            var hashString = cfg.EncodeString(hashA.Buffer);

            return hashString;
        }

        public async static Task<bool> Verify(string pass, string Hashed)
        {
            byte[] salt = new byte[16];
            Rng.GetBytes(salt);
            var cfg = new Argon2Config()
            {
                Type = Argon2Type.DataIndependentAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = timeCost,
                MemoryCost = memoryCost,
                Lanes = 5,
                Threads = Environment.ProcessorCount, // higher than "Lanes" doesn't help (or hurt)
                Password = Encoding.UTF8.GetBytes(pass),
                Salt = salt, // >= 8 bytes if not null
                Secret = Encoding.UTF8.GetBytes(secretKey), // from somewhere
                HashLength = hashLength // >= 4
            };
            return Argon2.Verify(Hashed, cfg);
        }


        public static bool cekPw(string pw, string Hashed) => Argon2.Verify(Hashed, pw, secret: secretKey);
        public static int timeCost { set; get; } = 3;
        public static int memoryCost { set; get; } = 6000;
        public static int parallelism { set; get; } = 1;
        public static Argon2Type type { set; get; } = Argon2Type.HybridAddressing;
        public static int hashLength { set; get; } = 32;
        public static string secretKey { set; get; } = "test123456789";
    }
}
