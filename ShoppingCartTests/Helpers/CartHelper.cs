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


    }
}
