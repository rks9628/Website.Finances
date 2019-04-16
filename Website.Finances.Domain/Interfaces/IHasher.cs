using System.Threading.Tasks;

namespace Website.Finances.Domain.Interfaces
{
    public interface IHasher
    {
        Task<byte[]> HashAsync(byte[] password, byte[] salt);
        Task<bool> VerifyAsync(byte[] hashed, byte[] original, byte[] salt);
    }
}
