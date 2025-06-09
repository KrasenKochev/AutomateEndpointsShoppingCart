using TestProject1.Models;

namespace TestProject1.Helpers
{
    using TestProject1.Constants;
    public static class CartHelper
    {
        public static async Task AddItemToCartAsync(HttpClient client, int itemId, int quantity)
        {
            var url = Urls.PostAddItemToCartUrl(itemId.ToString(), quantity.ToString());
            await client.PostAsync(url, new StringContent(""));
        }

        public static async Task RemoveItemFromCartAsync(HttpClient client, int itemId)
        {
            var url = Urls.PostRemoveItemFromCartUrl(itemId.ToString());
            var response = await client.PostAsync(url, null);
            ApiResponseHelper.AssertStatusCodeOk(response);
        }
        public static async Task<List<CartItemDto>> GetCartItemsAsync(HttpClient client)
        {
            var response = await client.GetAsync(Urls.GET_CART_ITEMS);
            var content = await response.Content.ReadAsStringAsync();
            return ApiResponseHelper.DeserializeCartItems(content);
        }

        public static async Task<CartItemDto> GetCartItemAsync(HttpClient client, int itemId)
        {
            var items = await GetCartItemsAsync(client);
            return items.FirstOrDefault(ci => ci.Id == itemId);
        }

    }
}
