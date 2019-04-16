using System.Threading.Tasks;

namespace Website.Finances.Domain.Interfaces
{
    public interface ICredentialVerifier
    {
        Task<bool> AuthenticateAsync(string username, string password);
    }
}
