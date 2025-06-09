using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject1.Constants;
using TestProject1.Helpers;
using TestProject1.Models.StoreItemDto;
using TestProject1.TestData;

namespace TestProject1.Tests
{
    [TestClass]
    public class RemoveItemCartEndpointTests : BaseTest
    {
        [TestMethod]
        public async Task RemoveItemFromCart_SingleQuantityItem_RemovesItemCompletely()
        {
            var item = StoreItems.FirstItem;
            await CartHelper.AddItemToCartAsync(_client, item.Id, 1);

            var response = await _client.PostAsync(Urls.PostRemoveItemFromCartUrl(item.Id.ToString()), null);
            ApiResponseHelper.AssertStatusCodeOk(response);

            var cartResponse = await _client.GetAsync(Urls.GET_CART_ITEMS);
            var cartItems = ApiResponseHelper.DeserializeCartItems(await cartResponse.Content.ReadAsStringAsync());

            Assert.IsFalse(cartItems.Any(ci => ci.Id == item.Id));
        }
        [TestMethod]
        public async Task RemoveItemFromCart_MultipleQuantityItem_DecrementsQuantity()
        {
            var item = StoreItems.FirstItem;
            await CartHelper.AddItemToCartAsync(_client, item.Id, 3);

            var response = await _client.PostAsync(Urls.PostRemoveItemFromCartUrl(item.Id.ToString()), null);
            ApiResponseHelper.AssertStatusCodeOk(response);

            var cartItems = ApiResponseHelper.DeserializeCartItems(await (await _client.GetAsync(Urls.GET_CART_ITEMS)).Content.ReadAsStringAsync());
            var actualItem = cartItems.FirstOrDefault(ci => ci.Id == item.Id);

            Assert.IsNotNull(actualItem);
            Assert.AreEqual(2, actualItem.Quantity);
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
