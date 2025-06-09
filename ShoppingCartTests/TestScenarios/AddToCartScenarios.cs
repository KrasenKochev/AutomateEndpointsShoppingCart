using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using TestProject1.Helpers;
using TestProject1.TestData;
using TestProject1.Constants;

namespace TestProject1.Scenarios
{
    [TestClass]
    public class AddToCartScenarios : BaseTest
    {
        [TestMethod]
        public async Task AddSingleItemToCart_Scenario_WorksCorrectly()
        {

            var response = await _client.GetAsync(Urls.GET_STORE_ITEMS);
            ApiResponseHelper.AssertStatusCodeOk(response);


            var content = await response.Content.ReadAsStringAsync();
            var storeItems = ApiResponseHelper.DeserializeStoreItems(content);


            ApiResponseHelper.AssertStoreItemsNotEmpty(storeItems);


            var itemToAdd = storeItems.First();
            var quantity = 1;
            await CartHelper.AddItemToCartAsync(_client, itemToAdd.Id, quantity);

            var cartItems = await CartHelper.GetCartItemsAsync(_client);
            var cartItem = cartItems.FirstOrDefault(ci => ci.Id == itemToAdd.Id);


            ApiResponseHelper.AssertCartItemQuantity(cartItem, 1);


            await CartHelper.CompleteOrderAsync(_client);
        }
    }
}