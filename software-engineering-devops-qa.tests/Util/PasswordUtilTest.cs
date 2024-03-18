using software_engineering_devops_qa.Util;

namespace software_engineering_devops_qa.Tests.Util;

[TestFixture]
public class PasswordUtilTest
{
	[Test]
	public void Password_TooShort()
	{
		Assert.That(!PasswordUtil.FitsPasswordPolicy("Sh0rt!"), "Password that was too short was marked as valid");
	}

	[Test]
	public void Password_MissingLowerCaseChar()
	{
		Assert.That(!PasswordUtil.FitsPasswordPolicy("TH1SH@S0NLYC@P5"), "Password that was missing a lower case character was marked as valid");
	}

	[Test]
	public void Password_MissingUpperCaseChar()
	{
		Assert.That(!PasswordUtil.FitsPasswordPolicy("th1sh@sn0c@p5"), "Password that was missing an upper case character was marked as valid");
	}

	[Test]
	public void Password_MissingDigitChar()
	{
		Assert.That(!PasswordUtil.FitsPasswordPolicy("NoNumbers@reHere"), "Password that was missing a digit was marked as valid");
	}

	[Test]
	public void Password_MissingSpecialChar()
	{
		Assert.That(!PasswordUtil.FitsPasswordPolicy("N0Spec1alCharsH3r3"), "Password that was missing a special character was marked as valid");
	}

	[Test]
	public void Password_Valid()
	{
		Assert.That(PasswordUtil.FitsPasswordPolicy("Th!s1s@ValidP@55w0rd"), "Valid password failed the test");
	}
}