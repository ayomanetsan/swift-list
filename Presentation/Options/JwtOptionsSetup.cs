using Infrastructure.Identity;
using Microsoft.Extensions.Options;

namespace Presentation.Options
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {

        public void Configure(JwtOptions options)
        {
            options.Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            options.Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            options.SecretKey = Environment.GetEnvironmentVariable("JWT_SECRET");
        }
    }
}
