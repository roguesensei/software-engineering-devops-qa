using Microsoft.AspNetCore.Server.Kestrel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace software_engineering_devops_qa.Util;

public static class JwtUtil
{
	public static string CreateJwt(DateTime expiresAt, IEnumerable<Claim> claims)
	{
		var secret = Environment.GetEnvironmentVariable("JWT_SECRET_TOKEN")!;
		var encryptionKey = Encoding.UTF8.GetBytes(secret);

		var tokenHandler = new JwtSecurityTokenHandler();
		var key = new SymmetricSecurityKey(encryptionKey);
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Issuer = "LMS-API",
			Subject = new ClaimsIdentity(claims),
			NotBefore = DateTime.UtcNow,
			Expires = expiresAt,
			SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}
}