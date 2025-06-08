using System.Text.Json;

namespace TestProject1;

[TestClass]
public class Tests
{
	private readonly HttpClient _client = new()
	{
		BaseAddress = new Uri("http://localhost:4080")
	};
	public class StoreItemDto
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public decimal Price { get; set; }
	}


	//Add your tests here

	[TestMethod]
	public async Task GetStoreItems_ReturnStatusCodeOk()
	{
		var response = await _client.GetAsync("/getstoreitems");
		AssertStatusCodeOk(response);
	}

	[TestMethod]
	public async Task GetStoreItems_ReturnsListOfItems()
	{
		var response = await _client.GetAsync("/getstoreitems");
		var content = await response.Content.ReadAsStringAsync();

		AssertStatusCodeOk(response);
		await AssertResponseIsJsonArray(response);

	}

	[TestMethod]
	public async Task GetStoreItems_ReturnsExpectedNumberOfItems()
	{
		var response = await _client.GetAsync("/getstoreitems");
		AssertStatusCodeOk(response);

		var items = await DeserializeJsonListResponse<StoreItemDto>(response);
		Assert.AreEqual(10, items.Count);

	}
	[TestMethod]
	public async Task GetStoreItems_ReturnsSpecificItem()
	{
		var response = await _client.GetAsync("/getstoreitems");
		response.EnsureSuccessStatusCode(); // Assert that the call succeeded

		var content = await response.Content.ReadAsStringAsync();

		var items = JsonSerializer.Deserialize<List<StoreItemDto>>(content, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		});

		AssertStatusCodeOk(response);
		Assert.IsNotNull(items, "Response deserialized to null");

		var expectedItem = items.FirstOrDefault(item =>
			item.Id == 1 &&
			item.Name == "First Item" &&
			item.Price == 1.0m
		);

		Assert.IsNotNull(expectedItem, "Expected item not found in the list.");
	}
	private void AssertStatusCodeOk(HttpResponseMessage response)
	{
		Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
	}

	private void AssertStatusCodeBadRequest(HttpResponseMessage response)
	{
		Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
	}

	private void AssertStatusCodeNotFound(HttpResponseMessage response)
	{
		Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);
	}

	private async Task AssertResponseIsJsonArray(HttpResponseMessage response)
	{
		var content = await response.Content.ReadAsStringAsync();
		Assert.IsTrue(content.StartsWith("[") && content.EndsWith("]"),
			"Expected content to be a JSON array.");
	}

	private async Task<List<T>> DeserializeJsonListResponse<T>(HttpResponseMessage response)
	{
		var content = await response.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<List<T>>(content, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		}) ?? new List<T>();
	}



}

internal class StoreItemDto
{
}