using System.Security.Cryptography;
using System.Text;

namespace software_engineering_devops_qa.Util;

public static class PasswordUtil
{
	public static readonly string passwordPolicyError = $"Passwords must be at least {minLength} characters with at least one upper, lower, special and digit character";

	public static bool FitsPasswordPolicy(string password)
	{
		if (password.Length < minLength)
		{
			return false;
		}

		var hasLowerCaseChar = false;
		var hasUpperCaseChar = false;
		var hasSpecialChar = false;
		var hasDigitChar = false;
		foreach (var c in password)
		{
			var charCode = (int)c;
			if (charCode >= 48 && charCode <= 57)
			{
				hasDigitChar = true;
			}
			else if (charCode >= 65 && charCode <= 90)
			{
				hasUpperCaseChar = true;
			}
			else if (charCode >= 97 && charCode <= 122)
			{
				hasLowerCaseChar = true;
			}
			else
			{
				hasSpecialChar = true;
			}
		}

		return hasLowerCaseChar && hasUpperCaseChar && hasSpecialChar && hasDigitChar;
	}

	public static byte[] HashPassword(string password) => SHA256.HashData(Encoding.UTF8.GetBytes(password));

	private const int minLength = 8;
}