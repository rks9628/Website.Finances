using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Website.Finances.Domain.Interfaces;

namespace Website.Finances.Authentication.Basic
{
    internal static class BasicAuthenticationExtensions
    {
        public static AuthenticationBuilder AddBasicAuthentication(this AuthenticationBuilder builder, ICredentialVerifier credentialVerifier)
        {
            builder.Services.AddSingleton(credentialVerifier);
            return builder.AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>(
                BasicAuthenticationDefaults.AuthenticationScheme,
                BasicAuthenticationDefaults.DisplayName,
                o => { });
        }

        public static AuthenticationBuilder AddBasicAuthentication<T>(this AuthenticationBuilder builder)
            where T : class, ICredentialVerifier
        {
            builder.Services.AddSingleton<ICredentialVerifier, T>();
            return builder.AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>(
                BasicAuthenticationDefaults.AuthenticationScheme,
                BasicAuthenticationDefaults.DisplayName,
                o => { });
        }
    }
}
