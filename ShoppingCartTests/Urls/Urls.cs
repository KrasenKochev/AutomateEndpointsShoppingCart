namespace TestProject1.Constants;

public static class Urls
{
    public static string BASE_URL => "http://localhost:4080";
    public static string GET_STORE_ITEMS => "getstoreitems";
    public static string INVALID_URL => "errrrrorrr ";

    public static string ADD_ITEM_TO_CART_PREFIX => "additemtocart";

    public static string GetAddItemToCartUrl(int id, int amount) =>
    $"{ADD_ITEM_TO_CART_PREFIX}/{id}/{amount}";

    public static string GetAddItemToCartUrlString(string id, int amount) =>
   $"{ADD_ITEM_TO_CART_PREFIX}/{id}/{amount}";

    public static string GetAddItemToCartUrlLong(long id, int amount) =>
   $"{ADD_ITEM_TO_CART_PREFIX}/{id}/{amount}";


}
