namespace software_engineering_devops_qa.Util;

public static class NullableUtil
{
	public static T ExpectValue<T>(this T? value, string errorMessage)
		where T : struct
	{
		if (!value.HasValue)
		{
			throw new Exception(errorMessage);
		}

		return value.Value;
	}

	public static T ExpectValue<T>(this T? value, string errorMessage)
		where T : class
	{
		if (value is null)
		{
			throw new Exception(errorMessage);
		}

		return value;
	}

	public static string ExpectValue(this string? value, string errorMessage)
	{
		if (string.IsNullOrEmpty(value))
		{
			throw new Exception(errorMessage);
		}

		return value;
	}
}