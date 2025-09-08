using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.Models;
using System.Text;

namespace RealEstate.WebAPI.Configuration
{
	public static class ConfigJWT
	{
		public static IServiceCollection RegisterJWTToken(this IServiceCollection services, WebApplicationBuilder builder)
		{
			var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
			if (jwtSettings != null && !string.IsNullOrEmpty(jwtSettings.SecretKey))
			{
				builder.Services.AddSingleton(jwtSettings);

				var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

				builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

				builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options =>
					{
						options.TokenValidationParameters = new TokenValidationParameters
						{
							ValidateIssuer = true,
							ValidateAudience = true,
							ValidateLifetime = true,
							ValidateIssuerSigningKey = true,
							ValidIssuer = jwtSettings.Issuer,
							ValidAudience = jwtSettings.Audience,
							IssuerSigningKey = new SymmetricSecurityKey(key),
                            ClockSkew = TimeSpan.Zero
                        };
					});
			}	
			return services;
		}
	}
}
