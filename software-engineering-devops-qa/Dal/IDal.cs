namespace software_engineering_devops_qa.Dal;

public interface IDal<T>
{
	static void Init(string dbConnection) => throw new NotImplementedException();

	List<T> Get(string dbConnection);

	int Add(string dbConnection, T model);
}