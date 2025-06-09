using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject1.Constants;
using TestProject1.Helpers;
using TestProject1.Models;
using TestProject1.TestData;

namespace TestProject1.Tests
{
    [TestClass]
    [TestCategory("Basic")]
    public class AddItemToCartEndpointTests : BaseTest
    {

        [TestMethod]
        public async Task AddItemToCart_WithValidItemAndQuantity_ReturnsOk()
        {

            var StoreItemId = StoreItems.FirstItem.Id;
            var expectedQuantity = "1";

            var url = Urls.PostAddItemToCartUrl(StoreItemId.ToString(), expectedQuantity);
            var response = await _client.PostAsync(url, null);

            ApiResponseHelper.AssertStatusCodeOk(response);
        }

        [TestMethod]
        public async Task AddItemToCart_WithNonExistingId_ReturnsBadRequest()
        {
            var StoreItemId = StoreItems.NonExistingItem.Id;
            var Quantity = "1";

            var url = Urls.PostAddItemToCartUrl(StoreItemId.ToString(), Quantity);
            var response = await _client.PostAsync(url, null);

            ApiResponseHelper.AssertStatusCodeBadRequest(response);
            await ApiResponseHelper.AssertErrorMessageAsync(response, Messages.ITEM_DOES_NOT_EXIST);
        }


        [TestMethod]
        public async Task AddItemToCart_WithNegativeAmount_ReturnsOk()
        {
            var StoreItemId = StoreItems.FirstItem.Id;
            var negativeQuantity = "-1";

            var url = Urls.PostAddItemToCartUrl(StoreItemId.ToString(), negativeQuantity);
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

            var StoreItemIdString = "errror&";
            var Quantity = "1";

            var url = Urls.PostAddItemToCartUrl(StoreItemIdString, Quantity);
            var response = await _client.PostAsync(url, null);

            ApiResponseHelper.AssertStatusCodeNotFound(response);
        }

        [TestMethod]
        public async Task AddItemToCart_WithValidItemAndLargeQuantity_ReturnsNonFound()
        {
            var StoreItemId = StoreItems.SecondItem.Id;
            var Quantity = "9999999999";

            var url = Urls.PostAddItemToCartUrl(StoreItemId.ToString(), Quantity);
            var response = await _client.PostAsync(url, null);

            ApiResponseHelper.AssertStatusCodeNotFound(response);
        }



    }
}
