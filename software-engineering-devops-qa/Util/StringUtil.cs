using System.Text;

namespace software_engineering_devops_qa.Util;

public static class StringUtil
{
	public static string ToBase64(this string str)
	{
		return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
	}
}