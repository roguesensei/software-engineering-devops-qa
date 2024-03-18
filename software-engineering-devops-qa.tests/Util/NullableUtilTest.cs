using software_engineering_devops_qa.Util;

namespace software_engineering_devops_qa.Tests.Util;

[TestFixture]
public class NullableUtilTest
{
	[Test]
	public void Nullable_HasValue()
	{
		int? a;
		a = 1;

		Assert.DoesNotThrow(() => a.ExpectValue(), "Value that exists threw an error");
	}

	[Test]
	public void Nullable_NotHasValue()
	{
		int? a = null;

		Assert.Throws<Exception>(() => a.ExpectValue(), "Value that doesn't exist didn't throw an error");
	}

	[Test]
	public void Nullable_EmptyString()
	{
		var str = string.Empty;

		Assert.Throws<Exception>(() => str.ExpectValue(), "Empty string value didn't throw an error");
	}
}