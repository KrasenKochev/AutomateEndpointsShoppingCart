using TestProject1.ChekItemsCartEndpoint;
using TestProject1.Models;

namespace TestProject1.TestData;

public static class ItemsProperties
{
    public const string StoreItemId = "1";

    public const int StoreItemQuantity = 1;

    public const string StoreItemIdNonExisting = "1331313";

    public const string StoreItemQuantityNegativeAmount = "-1";

    public const string StoreItemIdInvalidSymbol = "errror&";

    public const string StoreItemQuantityLarge = "9999999999";

}