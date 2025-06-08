namespace TestProject1.TestData;

using TestProject1.Models.StoreItemDto;
using TestProject1.Models.CartItemDto;
public static class StoreItems
{
    public static StoreItemDto FirstItem => new()
    {
        Id = 1,
        Name = "First Item",
        Price = 1.0m
    };

    public static StoreItemDto NonExistingItem => new()
    {
        Id = 13,
        Name = "Non-Existing-Item",
        Price = 13.13m
    };



}