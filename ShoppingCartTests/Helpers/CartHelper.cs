using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TestProject1.Constants;
using TestProject1.Models;

namespace TestProject1.Helpers
{

    public static class CartHelper
    {
        public static async Task<HttpResponseMessage> AddItemToCartAsync(HttpClient client, int itemId, int quantity)
        {
            var url = Urls.PostAddItemToCartUrl(itemId.ToString(), quantity.ToString());
            return await client.PostAsync(url, null);
        }
        public static async Task<List<CartItemDto>> GetCartItemsAsync(HttpClient client)
        {
            var response = await client.GetAsync(Urls.GET_CART_ITEMS);
            ApiResponseHelper.AssertStatusCodeOk(response);

            var content = await response.Content.ReadAsStringAsync();
            return ApiResponseHelper.DeserializeCartItems(content);
        }

        public static async Task<HttpResponseMessage> CompleteOrderAsync(HttpClient client)
        {
            var response = await client.PostAsync(Urls.COMPLETE_ORDER_PREFIX, null);
            ApiResponseHelper.AssertStatusCodeOk(response);
            return response;

        }
        public static async Task<HttpResponseMessage> RemoveItemFromCartAsync(HttpClient client, int itemId)
        {
            return await client.PostAsync(Urls.PostRemoveItemFromCartUrl(itemId.ToString()), null);
        }

    }
}