using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject1.Models.CartItemDto;
using TestProject1.Models.StoreItemDto;

namespace TestProject1.Helpers;

public static class ApiResponseHelper
{
    public static void AssertStatusCodeOk(HttpResponseMessage response)
    {
        Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
    public static void AssertStatusCodeBadRequest(HttpResponseMessage response)
    {
        Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }
    public static void AssertStatusCodeNotFound(HttpResponseMessage response)
    {
        Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }
    public static void AssertIsJsonList(string content)
    {
        Assert.IsTrue(content.StartsWith("[") && content.EndsWith("]"), "Expected JSON array format.");
    }
    public static List<StoreItemDto>? DeserializeStoreItems(string content)
    {
        return JsonSerializer.Deserialize<List<StoreItemDto>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
    public static List<CartItemDto> DeserializeCartItems(string content)
    {
        return JsonSerializer.Deserialize<List<CartItemDto>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new();
    }
    public static void AssertStoreItemExists(List<StoreItemDto>? items, StoreItemDto expected)
    {
        Assert.IsNotNull(items, "Deserialized item list is null.");
        var match = items.Find(item =>
            item.Id == expected.Id &&
            item.Name == expected.Name &&
            item.Price == expected.Price);
        Assert.IsNotNull(match, "Expected item was not found in the list.");
    }
    public static void AssertStoreItemIsNonExisting(List<StoreItemDto>? items, StoreItemDto expected)
    {
        Assert.IsNotNull(items, "Deserialized item list is null.");
        var nonExistingItem = items.Find(item =>
        item.Id == expected.Id &&
        item.Name == expected.Name &&
        item.Price == expected.Price);

        Assert.IsNull(nonExistingItem, $"Unexpected item found in store items: Id={expected.Id}, Name='{expected.Name}', Price={expected.Price:C}.");
    }

    public static void AssertEachItemHasRequiredFields(List<StoreItemDto>? items)
    {
        Assert.IsNotNull(items, "Deserialized item list is null.");

        foreach (var item in items)
        {
            Assert.IsTrue(item.Id > 0, $"Item ID should be greater than 0. Found: {item.Id}");
            Assert.IsFalse(string.IsNullOrWhiteSpace(item.Name), $"Item Name should not be null or empty. Found: '{item.Name}'");
            Assert.IsTrue(item.Price > 0, $"Item Price should be greater than 0. Found: {item.Price}");
        }
    }

}
