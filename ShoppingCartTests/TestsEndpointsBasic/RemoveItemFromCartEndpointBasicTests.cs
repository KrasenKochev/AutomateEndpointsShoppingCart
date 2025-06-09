using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject1.Constants;
using TestProject1.Helpers;
using TestProject1.Models;
using TestProject1.TestData;

namespace TestProject1.Tests
{
    [TestClass]
    public class RemoveItemCartEndpointTests : BaseTest
    {
        [TestMethod]
        public async Task RemoveItemFromCart_SingleQuantityItem_RemovesItemCompletely()
        {
            var itemID = StoreItems.FirstItem.Id;
            var quantity = 1;

            await CartHelper.AddItemToCartAsync(_client, itemID, quantity);

            await CartHelper.RemoveItemFromCartAsync(_client, itemID);

            var cartItems = await CartHelper.GetCartItemsAsync(_client);

            Assert.IsFalse(cartItems.Any(ci => ci.Id == itemID));
        }

        [TestMethod]
        public async Task RemoveItemFromCart_MultipleQuantityItem_DecrementsQuantity()
        {
            var itemId = StoreItems.FirstItem.Id;
            var quantity = 3;
            var actualQuantity = quantity - 1;

            await CartHelper.AddItemToCartAsync(_client, itemId, quantity);
            await CartHelper.RemoveItemFromCartAsync(_client, itemId);

            var actualItem = await CartHelper.GetCartItemAsync(_client, itemId);

            Assert.IsNotNull(actualItem);
            Assert.AreEqual(actualQuantity, actualItem.Quantity);
        }


        [TestMethod]
        public async Task RemoveItemFromCart_ItemNotInCart_ReturnsBadRequest()
        {
            var id = StoreItems.FirstItem.Id;
            var response = await _client.PostAsync(Urls.PostRemoveItemFromCartUrl(id.ToString()), null);

            ApiResponseHelper.AssertStatusCodeBadRequest(response);
            await ApiResponseHelper.AssertErrorMessageAsync(response, Messages.ITEM_NOT_IN_CART);
        }

        [TestMethod]
        public async Task RemoveItemFromCart_ItemNotInStore_ReturnsBadRequest()
        {
            var id = StoreItems.NonExistingItem.Id;
            await CartHelper.AddItemToCartAsync(_client, id, 1);

            var response = await _client.PostAsync(Urls.PostRemoveItemFromCartUrl(id.ToString()), null);

            ApiResponseHelper.AssertStatusCodeBadRequest(response);
            await ApiResponseHelper.AssertErrorMessageAsync(response, Messages.ITEM_INFO_NOT_FOUND);
        }









    }
}
