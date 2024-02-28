using System;
using System.Text;
using NUnit.Framework;
using software_engineering_devops_qa.Dal;
using software_engineering_devops_qa.Models;

[TestFixture]
public class UserDalTest
{
	private const string dbConnection = "Data Source=.user-dal-test.db";
	
	private User TestUser { get; set; }

	[SetUp]
	public void SetUp()
	{
		UserDal.Init(dbConnection);

		var user = new User { Username = Guid.NewGuid().ToString(), PasswordHash = Encoding.UTF8.GetBytes("TEST") };
		user.UserId = new UserDal().Add(dbConnection, user);

		TestUser = user;
	}

	[Test]
	public void GetByUsername_Exists()
	{
		Assert.IsNotNull(new UserDal().GetByUsername(dbConnection, TestUser.Username), "User with given name does not exist");
	}
	
	[Test]
	public void GetByUsername_NotExists()
	{
		Assert.IsNull(new UserDal().GetByUsername(dbConnection, ""), "User with given name exists");
	}
}