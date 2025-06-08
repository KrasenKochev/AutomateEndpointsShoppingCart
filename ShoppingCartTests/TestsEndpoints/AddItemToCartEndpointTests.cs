using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject1.Constants;
using TestProject1.Helpers;
using TestProject1.Models.StoreItemDto;
using TestProject1.TestData;

namespace TestProject1.Tests
{
    [TestClass]
    public class AddItemToCartEndpointTests : BaseTest
    {

        [TestMethod]
        public async Task AddItemToCart_WithValidItemAndQuantity_ReturnsOk()
        {
            var url = Urls.GetAddItemToCartUrl(ItemsProperties.StoreItemId, ItemsProperties.StoreItemQuantity);
            var response = await _client.PostAsync(url, null);

            ApiResponseHelper.AssertStatusCodeOk(response);
        }

        [TestMethod]
        public async Task AddItemToCart_WithNonExistingId_ReturnsBadRequest()
        {
            var url = Urls.GetAddItemToCartUrl(ItemsProperties.StoreItemIdNonExisting, ItemsProperties.StoreItemQuantity);
            var response = await _client.PostAsync(url, null);

            ApiResponseHelper.AssertStatusCodeBadRequest(response);
        }


        [TestMethod]
        public async Task AddItemToCart_WithNegativeAmount_ReturnsOk()
        {
            var url = Urls.GetAddItemToCartUrl(ItemsProperties.StoreItemId, ItemsProperties.StoreItemQuantityNegativeAmount);
            var response = await _client.PostAsync(url, null);

            ApiResponseHelper.AssertStatusCodeOk(response);

        }
        /*
        AddItemToCart_WithNegativeAmount_ReturnsOk :

        Take note that when adding item with negative amount, it should return an error, or at least as, at a design point of view, seems better to return an error code.
        For the sake of the test, the assertion looks for a 200 code.
        */

        [TestMethod]
        public async Task AddItemToCart_WithStringId_ReturnsNonFound()
        {
            var url = Urls.GetAddItemToCartUrl(ItemsProperties.StoreItemIdInvalidSymbol, ItemsProperties.StoreItemQuantityNegativeAmount);
            var response = await _client.PostAsync(url, null);

            ApiResponseHelper.AssertStatusCodeNotFound(response);
        }

        [TestMethod]
        public async Task AddItemToCart_WithValidItemAndLargeQuantity_ReturnsNonFound()
        {
            var url = Urls.GetAddItemToCartUrl(ItemsProperties.StoreItemQuantityLarge, ItemsProperties.StoreItemQuantityNegativeAmount);
            var response = await _client.PostAsync(url, null);

            ApiResponseHelper.AssertStatusCodeNotFound(response);
        }



    }
}
