using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject1.Constants;
using TestProject1.Helpers;
using TestProject1.Models;
using TestProject1.TestData;

namespace TestProject1.ChekItemsCartEndpoint;

[TestClass]
[TestCategory("Basic")]
public class CheckItemsEndpointTests : BaseTest
{
	[TestInitialize]
	public async Task TestInitialize()
	{
		try
		{
			await CartHelper.CompleteOrderAsync(_client);
		}
		catch { }
	}

	[TestMethod]
	public async Task GetStoreItems_ReturnsStatusCodeOk()
	{
		var response = await _client.GetAsync(Urls.GET_STORE_ITEMS);
		ApiResponseHelper.AssertStatusCodeOk(response);
	}

	[TestMethod]
	public async Task GetStoreItems_InvalidRoute_ReturnsNotFound()
	{
		var response = await _client.GetAsync(Urls.INVALID_URL);
		ApiResponseHelper.AssertStatusCodeNotFound(response);
	}

	[TestMethod]
	public async Task GetStoreItems_ReturnsListOfItems()
	{
		var response = await _client.GetAsync(Urls.GET_STORE_ITEMS);
		var content = await response.Content.ReadAsStringAsync();

		ApiResponseHelper.AssertStatusCodeOk(response);
		ApiResponseHelper.AssertIsJsonList(content);
	}

	[TestMethod]
	public async Task GetStoreItems_ReturnsExpectedNumberOfItems()
	{
		var response = await _client.GetAsync(Urls.GET_STORE_ITEMS);
		var content = await response.Content.ReadAsStringAsync();
		var items = ApiResponseHelper.DeserializeStoreItems(content);

		ApiResponseHelper.AssertStatusCodeOk(response);
		Assert.AreEqual(10, items?.Count);
	}

	[TestMethod]
	public async Task GetStoreItems_ContainsSpecificItem()
	{
		var response = await _client.GetAsync(Urls.GET_STORE_ITEMS);
		var content = await response.Content.ReadAsStringAsync();
		var items = ApiResponseHelper.DeserializeStoreItems(content);

		ApiResponseHelper.AssertStatusCodeOk(response);
		ApiResponseHelper.AssertStoreItemExists(items, StoreItems.FirstItem);
	}

	[TestMethod]
	public async Task GetStoreItems_DoesNotContainNonExistingItem()
	{
		var response = await _client.GetAsync(Urls.GET_STORE_ITEMS);
		var content = await response.Content.ReadAsStringAsync();
		var items = ApiResponseHelper.DeserializeStoreItems(content);

		ApiResponseHelper.AssertStatusCodeOk(response);
		ApiResponseHelper.AssertStoreItemIsNonExisting(items, StoreItems.NonExistingItem);
	}

	[TestMethod]
	public async Task GetStoreItems_EachItemHasRequiredFields()
	{
		var response = await _client.GetAsync(Urls.GET_STORE_ITEMS);
		var content = await response.Content.ReadAsStringAsync();
		var items = ApiResponseHelper.DeserializeStoreItems(content);

		ApiResponseHelper.AssertStatusCodeOk(response);
		ApiResponseHelper.AssertEachItemHasRequiredFields(items);
	}

}
