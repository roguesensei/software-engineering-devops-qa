using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace software_engineering_devops_qa.Util;

public static class JwtUtil
{
	private static readonly byte[] encryptionKey = Encoding.ASCII.GetBytes("MY-VERY-SECURE-SECRET-TOK");

	public static string CreateJwt(DateTime expiresAt, IEnumerable<Claim> claims)
	{
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