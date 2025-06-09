using TestProject1.Models;
using TestProject1.ChekItemsCartEndpoint;

namespace TestProject1.TestData;

public static class ItemsProperties
{
    public const string StoreItemId = "1";

    public const string StoreItemQuantity = "2";

    public const string StoreItemIdNonExisting = "1331313";

    public const string StoreItemQuantityNegativeAmount = "-1331313";

    public const string StoreItemIdInvalidSymbol = "errror&";

    public const string StoreItemQuantityLarge = "9999999999";

}