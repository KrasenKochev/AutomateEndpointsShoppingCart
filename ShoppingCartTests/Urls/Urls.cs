namespace TestProject1.Constants;

public static class Urls
{
    public static string BASE_URL => "http://localhost:4080";
    public static string GET_STORE_ITEMS => "getstoreitems";
    public static string INVALID_URL => "errrrrorrr ";

    public static string ADD_ITEM_TO_CART_PREFIX => "additemtocart";

    public static string PostAddItemToCartUrl(string id, string amount) =>
    $"{ADD_ITEM_TO_CART_PREFIX}/{id}/{amount}";

    public static string COMPLETE_ORDER_PREFIX => "completeorder";

    public static string GET_CART_ITEMS => "getcartitems";


}
