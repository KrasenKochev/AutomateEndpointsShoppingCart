using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject1.Models;

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

    public static void AssertStoreItemExists(List<StoreItemDto>? items, StoreItemDto expected)
    {
        Assert.IsNotNull(items, "Deserialized item list is null.");
        var match = items.Find(item =>
            item.Id == expected.Id &&
            item.Name == expected.Name &&
            item.Price == expected.Price);
        Assert.IsNotNull(match, "Expected item was not found in the list.");
    }
}
