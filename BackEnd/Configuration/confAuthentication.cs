using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;

namespace BackEnd.Configuration
{
    public static class AuthenticationExtensions
    {
        public static AuthenticationBuilder AddConfAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {

            var valor = configuration["jwt:key"];


            return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)!
      .AddJwtBearer(options =>
      {
          options.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuer = false,
              ValidateAudience = false,
              ValidateLifetime = true,
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = new SymmetricSecurityKey(
          Encoding.UTF8.GetBytes(valor)),
              ClockSkew = TimeSpan.Zero
          };

          options.Events = new JwtBearerEvents()
          {
              OnChallenge = context =>
              {
                  context.HandleResponse();
                  context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                  context.Response.ContentType = "application/json";
                  string Error = "";
                  // Ensure we always have an error and error description.
                  if (string.IsNullOrEmpty(context.Error))
                      context.Error = "invalid_token";
                  if (string.IsNullOrEmpty(context.ErrorDescription))
                      context.ErrorDescription = "This request requires a valid JWT access token to be provided";

                  // Add some extra context for expired tokens.
                  if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
                  {
                      var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
                      context.Response.Headers.Add("x-token-expired", authenticationException?.Expires.ToString("o"));
                      context.ErrorDescription = $"The token expired on {authenticationException?.Expires.ToString("o")}";
                  }


                  //return context.Response.WriteAsync(JsonSerializer.Serialize(
                  //    Result.Unauthorized<Unit>(context.ErrorDescription).ToDto()
                  //));

                  return context.Response.WriteAsync(JsonSerializer.Serialize(new
                  {
                      error = context.Error,
                      error_description = context.ErrorDescription
                  }));
              }
          };



      }

      )!;
        }
    }
}
