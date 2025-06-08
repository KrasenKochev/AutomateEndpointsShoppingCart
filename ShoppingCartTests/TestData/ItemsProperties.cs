namespace TestProject1.TestData;

using TestProject1.Models.StoreItemDto;
using TestProject1.Models.CartItemDto;
using TestProject1.ChekItemsCartEndpoint;

public static class ItemsProperties
{
    public const string StoreItemId = "1";

    public const string StoreItemQuantity = "2";

    public const string StoreItemIdNonExisting = "1331313";

    public const string StoreItemQuantityNegativeAmount = "-1331313";

    public const string StoreItemIdInvalidSymbol = "errror&";

    public const string StoreItemQuantityLarge = "9999999999";

}