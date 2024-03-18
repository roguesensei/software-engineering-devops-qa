using System.Security.Cryptography;
using System.Text;
using software_engineering_devops_qa.Dal;
using software_engineering_devops_qa.Models;

[TestFixture]
public class UserDalTest
{
	private const string dbConnection = "Data Source=.user-dal-test.db";
	private User? TestUser { get; set; }

	[SetUp]
	public void SetUp()
	{
		UserDal.Init(dbConnection, "Test1234!");

		var user = new User { Username = Guid.NewGuid().ToString(), PasswordHash = Encoding.UTF8.GetBytes("TEST") };
		user.UserId = new UserDal().Add(dbConnection, user);

		TestUser = user;
	}

	[Test]
	public void Admin_Exists()
	{
		Assert.That(new UserDal().GetByUsername(dbConnection, "admin"), Is.Not.Null, "Admin does not exist");
	}

	[Test]
	public void GetByUsername_Exists()
	{
		Assert.That(new UserDal().GetByUsername(dbConnection, TestUser!.Username), Is.Not.Null, "User with given name does not exist");
	}

	[Test]
	public void GetByUsername_NotExists()
	{
		Assert.That(new UserDal().GetByUsername(dbConnection, ""), Is.Null, "User with given name exists");
	}

	[Test]
	public void AddUpdateUser_Test()
	{
		var dal = new UserDal();
		var user = new User()
		{
			Username = "Alice",
			PasswordHash = GetPasswordHash()
		};

		user = new()
		{
			UserId = dal.Add(dbConnection, user),
			Username = "Alice",
			Role = Role.Admin,
		};
		dal.Update(dbConnection, user);

		var compare = dal.GetById(dbConnection, user.UserId);

		Assert.That(user.Role, Is.EqualTo(compare?.Role), "User was not updated, the role comparison failed");
	}

	[Test]
	public void DeleteUser_Test()
	{
		var dal = new UserDal();
		var user = new User()
		{
			Username = "Dave",
			PasswordHash = GetPasswordHash()
		};
		var id = dal.Add(dbConnection, user);

		Assert.That(dal.Delete(dbConnection, id), "Could not delete the user");
	}

	private byte[] GetPasswordHash() => SHA256.HashData(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
}