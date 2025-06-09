using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject1.Constants;
using TestProject1.Helpers;
using TestProject1.Models;
using TestProject1.TestData;

namespace TestProject1.Tests
{
    [TestClass]
    [TestCategory("Basic")]
    public class GetCartItemsEndpointTests : BaseTest
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

        public async Task GetCartItems_EmptyCart_ReturnsMessage()
        {
            var response = await _client.GetAsync(Urls.GET_CART_ITEMS);
            ApiResponseHelper.AssertStatusCodeOk(response);
            await ApiResponseHelper.AssertErrorMessageAsync(response, Messages.SHOPPING_CART_IS_EMPTY_GET_CART_ITEMS);
        }

        [TestMethod]

        public async Task GetCartItems_SingleItem_ReturnsCorrectData()
        {
            var expectedItem = StoreItems.FirstItem;
            var quantity = 2;

            await CartHelper.AddItemToCartAsync(_client, expectedItem.Id, quantity);

            var response = await _client.GetAsync(Urls.GET_CART_ITEMS);
            var content = await response.Content.ReadAsStringAsync();
            var cartItems = ApiResponseHelper.DeserializeCartItems(content);

            var actualItem = cartItems.FirstOrDefault(i => i.Id == expectedItem.Id);
            Assert.IsNotNull(actualItem);
            Assert.AreEqual(quantity, actualItem.Quantity);
            Assert.AreEqual(expectedItem.Price * quantity, actualItem.TotalPrice);
        }


        [TestMethod]
        public async Task GetCartItems_MultipleItems_ReturnsAllWithCorrectData()
        {
            var firstItem = StoreItems.FirstItem;
            var secondItem = StoreItems.SecondItem;

            var firstQuantity = 2;
            var secondQuantity = 3;

            await CartHelper.AddItemToCartAsync(_client, firstItem.Id, firstQuantity);
            await CartHelper.AddItemToCartAsync(_client, secondItem.Id, secondQuantity);

            var response = await _client.GetAsync(Urls.GET_CART_ITEMS);
            var content = await response.Content.ReadAsStringAsync();
            var cartItems = ApiResponseHelper.DeserializeCartItems(content);

            var actualFirstItem = cartItems.FirstOrDefault(i => i.Id == firstItem.Id);
            Assert.IsNotNull(actualFirstItem);
            Assert.AreEqual(firstQuantity, actualFirstItem.Quantity);
            Assert.AreEqual(firstItem.Price * firstQuantity, actualFirstItem.TotalPrice);

            var actualSecondItem = cartItems.FirstOrDefault(i => i.Id == secondItem.Id);
            Assert.IsNotNull(actualSecondItem);
            Assert.AreEqual(secondQuantity, actualSecondItem.Quantity);
            Assert.AreEqual(secondItem.Price * secondQuantity, actualSecondItem.TotalPrice);
        }



    }
}

