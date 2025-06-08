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
        public async Task AddItemToCart_WithNegativeAmount_ReturnsBadRequest()
        {
            var url = Urls.GetAddItemToCartUrl(ItemsProperties.StoreItemId, ItemsProperties.StoreItemQuantityNegativeAmount);
            var response = await _client.PostAsync(url, null);

            ApiResponseHelper.AssertStatusCodeBadRequest(response);
        }


        [TestMethod]
        public async Task AddItemToCart_WithStringId_ReturnsNonFound()
        {
            var url = Urls.GetAddItemToCartUrlString(ItemsProperties.StoreItemIdInvalidSymbol, ItemsProperties.StoreItemQuantityNegativeAmount);
            var response = await _client.PostAsync(url, null);

            ApiResponseHelper.AssertStatusCodeNotFound(response);
        }

        [TestMethod]
        public async Task AddItemToCart_WithValidItemAndLargeQuantity_ReturnsNonFound()
        {
            var url = Urls.GetAddItemToCartUrlLong(ItemsProperties.StoreItemQuantityLarge, ItemsProperties.StoreItemQuantityNegativeAmount);
            var response = await _client.PostAsync(url, null);

            ApiResponseHelper.AssertStatusCodeNotFound(response);
        }



    }
}
