using System.Text;
using System.Threading.Tasks;
using Website.Finances.Domain.Interfaces;

namespace Website.Finances.BL.Authentication
{
    public class CredentialVerifier : ICredentialVerifier
    {
        private readonly IUserRepository _userRepository;
        private readonly IHasher _hasher;

        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public CredentialVerifier(IUserRepository userRepository, IHasher hasher)
        {
            _userRepository = userRepository;
            _hasher = hasher;
        }

        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetByNameAsync(username);

            return user != null 
                   && await _hasher.VerifyAsync(user.PasswordHash, Encoding.GetBytes(password), user.PasswordSalt);
        }
    }
}
