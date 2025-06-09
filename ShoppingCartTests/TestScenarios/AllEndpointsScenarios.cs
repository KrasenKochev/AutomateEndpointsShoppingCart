using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject1.Constants;
using TestProject1.Helpers;
using TestProject1.TestData;

namespace TestProject1.Scenarios
{
    [TestClass]
    [TestCategory("Scenario")]
    public class AllEndpointsScenarios : BaseTest
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
        public async Task StoreToOrderCompletion_FullFlow_WorksCorrectly()
        {

            var storeResponse = await _client.GetAsync(Urls.GET_STORE_ITEMS);
            ApiResponseHelper.AssertStatusCodeOk(storeResponse);

            var storeContent = await storeResponse.Content.ReadAsStringAsync();
            var storeItems = ApiResponseHelper.DeserializeStoreItems(storeContent);
            ApiResponseHelper.AssertStoreItemsLoaded(storeItems);

            var firstItem = StoreItems.FirstItem;
            var secondItem = StoreItems.SecondItem;
            var firstQuantity = 1;
            var secondQuantity = 3;


            var addFirstResponse = await CartHelper.AddItemToCartAsync(_client, firstItem.Id, firstQuantity);
            ApiResponseHelper.AssertStatusCodeOk(addFirstResponse);


            var addSecondResponse = await CartHelper.AddItemToCartAsync(_client, secondItem.Id, secondQuantity);
            ApiResponseHelper.AssertStatusCodeOk(addSecondResponse);


            var cartResponse1 = await _client.GetAsync(Urls.GET_CART_ITEMS);
            ApiResponseHelper.AssertStatusCodeOk(cartResponse1);

            var cartContent1 = await cartResponse1.Content.ReadAsStringAsync();
            var cartItems1 = ApiResponseHelper.DeserializeCartItems(cartContent1);

            ApiResponseHelper.AssertCartItemExists(cartItems1, firstItem, firstQuantity);
            ApiResponseHelper.AssertCartItemExists(cartItems1, secondItem, secondQuantity);


            var removeFirstResponse = await CartHelper.RemoveItemFromCartAsync(_client, firstItem.Id);
            ApiResponseHelper.AssertStatusCodeOk(removeFirstResponse);

            string expectedMessage = $"One quantity of item '{firstItem.Name}' removed from the cart. Item '{firstItem.Name}' completely removed.";
            await ApiResponseHelper.AssertContentContainsMessage(removeFirstResponse, expectedMessage);


            var cartResponse2 = await _client.GetAsync(Urls.GET_CART_ITEMS);
            ApiResponseHelper.AssertStatusCodeOk(cartResponse2);

            var cartContent2 = await cartResponse2.Content.ReadAsStringAsync();
            var cartItems2 = ApiResponseHelper.DeserializeCartItems(cartContent2);

            ApiResponseHelper.AssertCartItemDoesNotExist(cartItems2, firstItem.Id);
            ApiResponseHelper.AssertCartItemExists(cartItems2, secondItem, secondQuantity);

            var completeOrderResponse = await CartHelper.CompleteOrderAsync(_client);
            ApiResponseHelper.AssertStatusCodeOk(completeOrderResponse);
            ApiResponseHelper.AssertEmptyContent(completeOrderResponse);
        }

    }
}