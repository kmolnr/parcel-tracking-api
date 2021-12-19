using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ParcelTracking.EFCore;

namespace ParcelTracking.API.Services.Authentication
{
	public class AuthenticationService : IAuthenticationService
	{
		private IdentitySettings IdentitySettings { get; }

		private readonly ParcelTrackingDbContext context;

		public AuthenticationService(ParcelTrackingDbContext context, Microsoft.Extensions.Options.IOptions<IdentitySettings> identitySettings)
		{
			this.IdentitySettings = identitySettings.Value;

			this.context = context;
		}

		public User Authenticate(string userName, string password)
		{
			// SHA-2 password encryption recommended.

			var user = this.context.User.SingleOrDefault(u => u.Name == userName && u.Password == password && u.Role == "user");

			if (user == null)
            {
				return null;
            }

			var tokenHandler = new JwtSecurityTokenHandler();

			var secretKey = Encoding.ASCII.GetBytes(this.IdentitySettings.SecretKey);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, user.Name),
					new Claim(ClaimTypes.Role, user.Role),
				}),
				Expires = DateTime.UtcNow.AddDays(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature),
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);

			var userToken = tokenHandler.WriteToken(token);

			return new User
            {
				Name = user.Name,
				Token = userToken,
            };
		}
	}
}
