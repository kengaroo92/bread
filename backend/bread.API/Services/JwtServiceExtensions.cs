using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public static class JwtServiceExtensions
{
  // https://stackoverflow.com/questions/72797587/asp-net-core-store-jwt-in-cookie
  // https://weblog.west-wind.com/posts/2022/Mar/29/Combining-Bearer-Token-and-Cookie-Auth-in-ASPNET
  public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
  {
    // Generate secret key for JWT authentication.
    var jwtSettingsSection = configuration.GetSection("JwtSettings");
    var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
    services.Configure<JwtSettings>(jwtSettingsSection);
    // Generate the signing key.
    var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
    // Add services for authentication to create a JWT (JSON Web Token) when a successful login request is received.
    services.AddAuthentication(x =>
    {   // Set default authentication scheme. When [Authorize] is used in the controllers, JWT bearer authentication will be used by default.
      x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // When a user tries to access a resource they are not authorized to access, a JWT challenge will occur, typically returning a 401 Unauthorized HTTP response.
    }).AddJwtBearer(x =>
    {
      x.RequireHttpsMetadata = false; // Set whether HTTPS is required or not.
      x.SaveToken = true; // Set if the token should be stored in the AuthenticationProperties.
      x.TokenValidationParameters = new TokenValidationParameters // Set parameters used to validate the token.
      {
        ValidateIssuerSigningKey = true, // Validate issuer signing key.
        IssuerSigningKey = new SymmetricSecurityKey(key), // Set the signing key used for validation.
        ValidateIssuer = false, // Set whether the issuer should be validated.
        ValidateAudience = false, // Set whether the audiance should be validated.
        ClockSkew = TimeSpan.Zero // Set a grace period on expiration, in this case, no grace period is set and tokens will expire exactly on their expiry date.
      };
    });

    return services;
  }
}
