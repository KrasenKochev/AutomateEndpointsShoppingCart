using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject1.Constants;
using TestProject1.Helpers;
using TestProject1.Models;
using TestProject1.TestData;

namespace TestProject1.Tests
{
    [TestClass]
    [TestCategory("Basic")]
    public class RemoveItemCartEndpointTests : BaseTest
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
        public async Task RemoveItemFromCart_SingleQuantityItem_RemovesItemCompletely()
        {
            var itemID = StoreItems.FirstItem.Id;
            var quantity = 1;
            var itemName = StoreItems.FirstItem.Name;
            var expectedMessage = $"One quantity of item '{itemName}' removed from the cart. Item '{itemName}' completely removed.";

            await CartHelper.AddItemToCartAsync(_client, itemID, quantity);

            var response = await _client.PostAsync(Urls.PostRemoveItemFromCartUrl(itemID.ToString()), null);
            ApiResponseHelper.AssertStatusCodeOk(response);
            await ApiResponseHelper.AssertContentContainsMessage(response, expectedMessage);

            var cartResponse = await _client.GetAsync(Urls.GET_CART_ITEMS);
            ApiResponseHelper.AssertContentEquals(cartResponse, "Shopping cart is empty.");
        }

        [TestMethod]
        public async Task RemoveItemFromCart_MultipleQuantityItem_DecrementsQuantity()
        {
            var item = StoreItems.FirstItem;
            var initialQuantity = 2;
            var expectedQuantity = initialQuantity - 1;

            var expectedMessage =
                $"One quantity of item '{item.Name}' removed from the cart. Remaining quantity: {expectedQuantity}, Price per item: ${item.Price:0.00}, Total price for this item: ${(item.Price * expectedQuantity):0.00}";

            await CartHelper.AddItemToCartAsync(_client, item.Id, initialQuantity);

            var response = await _client.PostAsync(Urls.PostRemoveItemFromCartUrl(item.Id.ToString()), null);
            ApiResponseHelper.AssertStatusCodeOk(response);
            await ApiResponseHelper.AssertContentContainsMessage(response, expectedMessage);

            var cartResponse = await _client.GetAsync(Urls.GET_CART_ITEMS);
            var cartContent = await cartResponse.Content.ReadAsStringAsync();
            var cartItems = ApiResponseHelper.DeserializeCartItems(cartContent);

            ApiResponseHelper.AssertCartItemQuantity(cartItems, item.Id, expectedQuantity);
        }



        [TestMethod]
        public async Task RemoveItemFromCart_ItemNotInCart_ReturnsBadRequest()
        {
            var itemID = StoreItems.FirstItem.Id;
            var response = await _client.PostAsync(Urls.PostRemoveItemFromCartUrl(itemID.ToString()), null);

            ApiResponseHelper.AssertStatusCodeBadRequest(response);
            await ApiResponseHelper.AssertContentContainsMessage(response, Messages.ITEM_NOT_IN_CART);
        }

        [TestMethod]
        public async Task RemoveItemFromCart_ItemNotInStore_ReturnsBadRequest()
        {
            var itemId = StoreItems.NonExistingItem.Id;
            await CartHelper.AddItemToCartAsync(_client, itemId, 1);

            var response = await _client.PostAsync(Urls.PostRemoveItemFromCartUrl(itemId.ToString()), null);

            ApiResponseHelper.AssertStatusCodeBadRequest(response);
            await ApiResponseHelper.AssertContentContainsMessage(response, Messages.ITEM_INFO_NOT_FOUND);
        }









    }
}
