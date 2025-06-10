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
        public async Task AddItemToCart_WithValidItemAndQuantity_ReturnsOk()
        {
            var item = StoreItems.FirstItem;
            var quantity = ItemsProperties.StoreItemQuantity;

            var addResponse = await CartHelper.AddItemToCartAsync(_client, item.Id, quantity);
            ApiResponseHelper.AssertStatusCodeOk(addResponse);

            var response = await _client.GetAsync(Urls.GET_CART_ITEMS);
            ApiResponseHelper.AssertStatusCodeOk(response);

            var content = await response.Content.ReadAsStringAsync();
            var cartItems = ApiResponseHelper.DeserializeCartItems(content);

            ApiResponseHelper.AssertCartItemExists(cartItems, item, quantity);
        }

        [TestMethod]
        public async Task AddItemToCart_WithNonExistingId_ReturnsBadRequest()
        {
            var item = StoreItems.NonExistingItem;
            var quantity = ItemsProperties.StoreItemQuantity;

            var addResponse = await CartHelper.AddItemToCartAsync(_client, item.Id, quantity);
            ApiResponseHelper.AssertStatusCodeBadRequest(addResponse);
            await ApiResponseHelper.AssertContentContainsMessage(addResponse, Messages.ITEM_DOES_NOT_EXIST);


            var cartResponse = await _client.GetAsync(Urls.GET_CART_ITEMS);
            ApiResponseHelper.AssertStatusCodeOk(cartResponse);
            ApiResponseHelper.AssertContentEquals(cartResponse, Messages.SHOPPING_CART_IS_EMPTY_GET_CART_ITEMS);
        }

        [TestMethod]
        public async Task AddItemToCart_WithNegativeAmount_ReturnsOk()
        {
            var item = StoreItems.FirstItem;
            var quantity = -1;

            var addResponse = await CartHelper.AddItemToCartAsync(_client, item.Id, quantity);
            ApiResponseHelper.AssertStatusCodeOk(addResponse);

            var response = await _client.GetAsync(Urls.GET_CART_ITEMS);
            ApiResponseHelper.AssertStatusCodeOk(response);

            var content = await response.Content.ReadAsStringAsync();
            var cartItems = ApiResponseHelper.DeserializeCartItems(content);

            ApiResponseHelper.AssertCartItemExists(cartItems, item, quantity);

        }
        /*
        AddItemToCart_WithNegativeAmount_ReturnsOk :

        Take note that when adding item with negative amount, it should return an error, or at least as, at a design point of view, seems better to return an error code.
        For the sake of the test, the assertion looks for a 200 code.
        */

        [TestMethod]
        public async Task AddItemToCart_WithStringId_ReturnsNonFound()
        {

            var StoreItemIdString = ItemsProperties.StoreItemIdInvalidSymbol;
            var Quantity = "1";

            var url = Urls.PostAddItemToCartUrl(StoreItemIdString, Quantity);
            var response = await _client.PostAsync(url, null);

            ApiResponseHelper.AssertStatusCodeNotFound(response);

            var cartResponse = await _client.GetAsync(Urls.GET_CART_ITEMS);
            ApiResponseHelper.AssertContentEquals(cartResponse, Messages.SHOPPING_CART_IS_EMPTY_GET_CART_ITEMS);
        }

        [TestMethod]
        public async Task AddItemToCart_WithValidItemAndLargeQuantity_ReturnsNonFound()
        {
            var StoreItemId = StoreItems.SecondItem.Id;
            var Quantity = ItemsProperties.StoreItemQuantityLarge;

            var url = Urls.PostAddItemToCartUrl(StoreItemId.ToString(), Quantity);
            var response = await _client.PostAsync(url, null);

            ApiResponseHelper.AssertStatusCodeNotFound(response);

            var cartResponse = await _client.GetAsync(Urls.GET_CART_ITEMS);
            ApiResponseHelper.AssertContentEquals(cartResponse, Messages.SHOPPING_CART_IS_EMPTY_GET_CART_ITEMS);
        }

        /*
        AddItemToCart_WithValidItemAndLargeQuantity_ReturnsNonFound :

        Take note that when adding item with large quantity(int) multiple times, the quantity of the item in the cart goes into negative.
        */


    }
}
