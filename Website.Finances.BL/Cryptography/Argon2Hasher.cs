using Isopoh.Cryptography.Argon2;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Extensions;
using Website.Finances.Domain.Interfaces;

namespace Website.Finances.BL.Cryptography
{
    public class Argon2Hasher : IHasher
    {
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public Task<byte[]> HashAsync(byte[] password, byte[] salt)
        {
            var config = GenerateConfig(password, salt);
            var hasher = new Argon2(config);
            return Task.FromResult(hasher.Hash().Buffer.ToArray());
        }

        public async Task<bool> VerifyAsync(byte[] hashed, byte[] original, byte[] salt)
        {
            return hashed.IsSame(await HashAsync(original, salt));
        }

        private Argon2Config GenerateConfig(byte[] password, byte[] salt)
        {
            return new Argon2Config
            {
                Type = Argon2Type.DataIndependentAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = 2,
                MemoryCost = 32768,
                Lanes = 5,
                Threads = 1,
                Password = password,
                Salt = salt,
                HashLength = 30
            };
        }
    }
}
