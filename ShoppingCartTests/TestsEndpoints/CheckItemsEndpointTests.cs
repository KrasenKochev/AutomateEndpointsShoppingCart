using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject1.Helpers;
using TestProject1.Models;
using TestProject1.Constants;

namespace TestProject1.ChekItemsCartEndpoint;

[TestClass]
public class CheckItemsEndpointTests : BaseTest
{
	[TestMethod]
	public async Task GetStoreItems_ReturnsStatusCodeOk()
	{
		var response = await _client.GetAsync(Urls.GET_STORE_ITEMS);
		ApiResponseHelper.AssertStatusCodeOk(response);
	}

	[TestMethod]
	public async Task GetStoreItems_ReturnsListOfItems()
	{
		var response = await _client.GetAsync("/getstoreitems");
		var content = await response.Content.ReadAsStringAsync();

		ApiResponseHelper.AssertStatusCodeOk(response);
		ApiResponseHelper.AssertIsJsonList(content);
	}

	[TestMethod]
	public async Task GetStoreItems_ReturnsExpectedNumberOfItems()
	{
		var response = await _client.GetAsync("/getstoreitems");
		var content = await response.Content.ReadAsStringAsync();
		var items = ApiResponseHelper.DeserializeStoreItems(content);

		ApiResponseHelper.AssertStatusCodeOk(response);
		Assert.AreEqual(10, items?.Count);
	}

	[TestMethod]
	public async Task GetStoreItems_ContainsSpecificItem()
	{
		var response = await _client.GetAsync("/getstoreitems");
		var content = await response.Content.ReadAsStringAsync();
		var items = ApiResponseHelper.DeserializeStoreItems(content);

		var expected = new StoreItemDto { Id = 1, Name = "First Item", Price = 1.0m };

		ApiResponseHelper.AssertStatusCodeOk(response);
		ApiResponseHelper.AssertStoreItemExists(items, expected);
	}
}
