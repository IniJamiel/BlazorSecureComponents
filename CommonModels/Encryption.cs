using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Isopoh.Cryptography.Argon2;

namespace CommonModelsLib
{
    public static class Encryption
    {
        public async static Task<string> Encrypt(string pass) => Argon2.Hash(pass, timeCost: timeCost, parallelism: parallelism,
            memoryCost: memoryCost, type: type, hashLength: hashLength, secret: secretKey);
        

        public static bool cekPw(string pw, string Hashed) => Argon2.Verify(Hashed, pw, secret:secretKey);
        public static int timeCost { set; get; } = 3;
        public static int memoryCost { set; get; } = 600;
        public static int parallelism { set; get; } = 1;
        public static Argon2Type type { set; get; } = Argon2Type.HybridAddressing;
        public static int hashLength { set; get; } = 32;
        public static string secretKey { set; get; } = "test123456789";
    }
}
