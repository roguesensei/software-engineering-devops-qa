namespace software_engineering_devops_qa;

public static class Config
{
	public static string LmsDbConnection { get; set; } = "";

	public static Dictionary<string, string> HttpResponseHeaders { get; set; } = [];
}