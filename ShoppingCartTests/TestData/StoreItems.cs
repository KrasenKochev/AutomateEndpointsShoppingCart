namespace TestProject1.TestData;

using TestProject1.Models;

public static class StoreItems
{
    public static StoreItemDto FirstItem => new()
    {
        Id = 1,
        Name = "First Item",
        Price = 1.0m
    };

    public static StoreItemDto SecondItem => new()
    {
        Id = 2,
        Name = "Second Item",
        Price = 2.0m
    };

    public static StoreItemDto NonExistingItem => new()
    {
        Id = 13,
        Name = "Non-Existing-Item",
        Price = 13.13m
    };

    public static List<StoreItemDto> AllItems => new()
    {
        FirstItem,
        SecondItem,
        NonExistingItem

    };
}
