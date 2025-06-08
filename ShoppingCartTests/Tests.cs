namespace TestProject1;

[TestClass]
public class Tests
{
	private readonly HttpClient _client = new()
	{
		BaseAddress = new Uri("http://localhost:4080")
	};
	
	//Add your tests here
}