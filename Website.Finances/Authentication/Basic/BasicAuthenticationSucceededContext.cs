using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Website.Finances.Authentication.Basic
{
    public class BasicAuthenticationSucceededContext : ResultContext<BasicAuthenticationOptions>
    {
        public string UserId { get; }

        public BasicAuthenticationSucceededContext(string userid, HttpContext context, AuthenticationScheme scheme, BasicAuthenticationOptions options) 
            : base(context, scheme, options)
        {
            UserId = userid;
        }
    }
}
