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
        [TestMethod]
        public async Task RemoveItemFromCart_SingleQuantityItem_RemovesItemCompletely()
        {
            var itemID = StoreItems.FirstItem.Id;
            var quantity = 1;

            await CartHelper.AddItemToCartAsync(_client, itemID, quantity);

            var response = await _client.PostAsync(Urls.PostRemoveItemFromCartUrl(itemID.ToString()), null);
            ApiResponseHelper.AssertStatusCodeOk(response);

            var cartResponse = await _client.GetAsync(Urls.GET_CART_ITEMS);
            var cartItems = ApiResponseHelper.DeserializeCartItems(await cartResponse.Content.ReadAsStringAsync());

            Assert.IsFalse(cartItems.Any(ci => ci.Id == itemID));
        }
        [TestMethod]
        public async Task RemoveItemFromCart_MultipleQuantityItem_DecrementsQuantity()
        {
            var itemID = StoreItems.FirstItem.Id;
            var quantity = 3;
            var actualQuantity = quantity - 1;

            await CartHelper.AddItemToCartAsync(_client, itemID, quantity);

            var response = await _client.PostAsync(Urls.PostRemoveItemFromCartUrl(itemID.ToString()), null);
            ApiResponseHelper.AssertStatusCodeOk(response);

            var cartItems = ApiResponseHelper.DeserializeCartItems(await (await _client.GetAsync(Urls.GET_CART_ITEMS)).Content.ReadAsStringAsync());
            var actualItem = cartItems.FirstOrDefault(ci => ci.Id == itemID);

            Assert.IsNotNull(actualItem);
            Assert.AreEqual(actualQuantity, actualItem.Quantity);
        }

        [TestMethod]
        public async Task RemoveItemFromCart_ItemNotInCart_ReturnsBadRequest()
        {
            var itemID = StoreItems.FirstItem.Id;
            var response = await _client.PostAsync(Urls.PostRemoveItemFromCartUrl(itemID.ToString()), null);

            ApiResponseHelper.AssertStatusCodeBadRequest(response);
            await ApiResponseHelper.AssertErrorMessageAsync(response, Messages.ITEM_NOT_IN_CART);
        }

        [TestMethod]
        public async Task RemoveItemFromCart_ItemNotInStore_ReturnsBadRequest()
        {
            var itemId = StoreItems.NonExistingItem.Id;
            await CartHelper.AddItemToCartAsync(_client, itemId, 1);

            var response = await _client.PostAsync(Urls.PostRemoveItemFromCartUrl(itemId.ToString()), null);

            ApiResponseHelper.AssertStatusCodeBadRequest(response);
            await ApiResponseHelper.AssertErrorMessageAsync(response, Messages.ITEM_INFO_NOT_FOUND);
        }









    }
}
