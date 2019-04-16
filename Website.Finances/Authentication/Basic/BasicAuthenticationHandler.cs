using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Website.Finances.BL.Base64;
using Website.Finances.Domain.Interfaces;

namespace Website.Finances.Authentication.Basic
{
    internal class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        private const string Prefix = "Basic ";

        private readonly ICredentialVerifier _credentialVerifier;

        public BasicAuthenticationHandler(ICredentialVerifier credentialVerifier, IOptionsMonitor<BasicAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) 
            : base(options, logger, encoder, clock)
        {
            _credentialVerifier = credentialVerifier;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.NoResult();

            var authHeader = Request.Headers["Authorization"];

            if (authHeader.Count != 1)
                return AuthenticateResult.Fail("Multiple authentication values supplied");

            var authHeaderValue = authHeader[0];

            if (!authHeaderValue.StartsWith(Prefix))
                return AuthenticateResult.Fail("Wrong authentication method");

            var credentialsValue = authHeaderValue.Substring(Prefix.Length, authHeaderValue.Length - Prefix.Length);
            var (username, password) = DecodeCredentials(credentialsValue);

            if (username == null || password == null)
                return AuthenticateResult.Fail("Wrong credential format");

            if (!await _credentialVerifier.AuthenticateAsync(username, password))
                return AuthenticateResult.Fail("Failed to validate credentials");

            var successContext = new BasicAuthenticationSucceededContext(username, Context, Scheme, Options)
            {
                Principal = CreateClaimsPrincipal(username)
            };

            if (successContext.Result != null)
                return successContext.Result;

            successContext.Success();
            return successContext.Result;
        }

        private ClaimsPrincipal CreateClaimsPrincipal(string username)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, username, ClaimValueTypes.String, ClaimsIssuer) };
            return new ClaimsPrincipal(new ClaimsIdentity(claims, Scheme.Name));
        }

        private static (string username, string password) DecodeCredentials(string base64Credentials)
        {
            var decodedString = Base64.FromBase64(base64Credentials);
            var splitStrings = decodedString.Split(":", 2);

            return splitStrings.Length < 2 
                ? (null, null) 
                : (splitStrings[0], splitStrings[1]);
        }
    }
}
