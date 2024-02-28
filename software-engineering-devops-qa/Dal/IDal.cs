namespace software_engineering_devops_qa.Dal;

public interface IDal<T>
{
	static void Init() => throw new NotImplementedException();

	List<T> Get();
}