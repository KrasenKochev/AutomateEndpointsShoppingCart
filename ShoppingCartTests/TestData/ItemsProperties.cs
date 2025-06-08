namespace TestProject1.TestData;

using TestProject1.Models.StoreItemDto;
using TestProject1.Models.CartItemDto;
using TestProject1.ChekItemsCartEndpoint;

public static class ItemsProperties
{
    public const int StoreItemId = 1;

    public const int StoreItemQuantity = 2;

    public const int StoreItemIdNonExisting = 1331313;

    public const int StoreItemQuantityNegativeAmount = -1331313;

    public const string StoreItemIdInvalidSymbol = "errror&";

    public const long StoreItemQuantityLarge = 9999999999;

}