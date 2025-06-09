using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using TestProject1.Helpers;
using TestProject1.TestData;
using TestProject1.Constants;

namespace TestProject1.Scenarios
{
    [TestClass]
    [TestCategory("Scenario")]
    public class StoreItemsScenarios : BaseTest
    {
        [TestMethod]
        public async Task GetStoreItems_HappyPath_ReturnsAllItems()
        {
            var response = await _client.GetAsync(Urls.GET_STORE_ITEMS);
            ApiResponseHelper.AssertStatusCodeOk(response);

            var content = await response.Content.ReadAsStringAsync();
            var items = ApiResponseHelper.DeserializeStoreItems(content);

            ApiResponseHelper.AssertStoreItemsNotEmpty(items);
        }

        [TestMethod]
        public async Task GetStoreItems_InvalidSuffix_ReturnsNotFound()
        {
            var addedSuffix = "errrror";
            var response = await _client.GetAsync(Urls.GET_STORE_ITEMS + addedSuffix);

            ApiResponseHelper.AssertStatusCodeNotFound(response);
        }

        [TestMethod]
        public async Task GetStoreItems_EmptyUrl_ReturnsNotFound()
        {
            var response = await _client.GetAsync("");
            ApiResponseHelper.AssertStatusCodeNotFound(response);
        }
    }
}
