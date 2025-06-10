using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject1.Constants;
using TestProject1.Helpers;
using TestProject1.Models;
using TestProject1.TestData;

namespace TestProject1.Tests
{
    [TestClass]


    [TestCategory("Basic")]
    public class CompleteOrderEndpointTests : BaseTest
    {
        [TestMethod]
        public async Task CompleteOrder_ItemsInCart_ReturnsOk()
        {
            var addToCartUrl = Urls.PostAddItemToCartUrl(ItemsProperties.StoreItemId, ItemsProperties.StoreItemQuantity.ToString());
            var AddItemToCartResponse = await _client.PostAsync(addToCartUrl, null);

            ApiResponseHelper.AssertStatusCodeOk(AddItemToCartResponse);

            var response = await _client.PostAsync(Urls.COMPLETE_ORDER_PREFIX, null);

            ApiResponseHelper.AssertStatusCodeOk(response);
            ApiResponseHelper.AssertEmptyContent(response);
        }

        [TestMethod]
        public async Task CompleteOrder_NoItemsInCart_ReturnsBadRequest()
        {
            var response = await _client.PostAsync(Urls.COMPLETE_ORDER_PREFIX, null);

            ApiResponseHelper.AssertStatusCodeBadRequest(response);

            await ApiResponseHelper.AssertContentContainsMessage(response, Messages.SHOPPING_CART_IS_EMPTY_COMPLETE_ORDER);
        }

    }
}
