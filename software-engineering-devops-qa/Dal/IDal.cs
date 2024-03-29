using software_engineering_devops_qa.Models;

namespace software_engineering_devops_qa.Dal;

public interface IDal<T>
{
	static void Init(string dbConnection) => throw new NotImplementedException();

	List<T> Get(string dbConnection, int userId, Role role);

	int Add(string dbConnection, T model);

	bool Update(string dbConnection, T model);

	bool Delete(string dbConnection, int id);
}