using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart;

[ApiController]
[Route("/")]
public class ShoppingCartController : Controller
{
	private static readonly Dictionary<int, StoreItem> StoreItems = new()
	{
		{ 1, new StoreItem { Id = 1, Name = "First Item", Price = 1.0m } },
		{ 2, new StoreItem { Id = 2, Name = "Second Item", Price = 2.0m } },
		{ 3, new StoreItem { Id = 3, Name = "Third Item", Price = 3.3m } },
		{ 4, new StoreItem { Id = 4, Name = "Fourth Item", Price = 4.0m } },
		{ 5, new StoreItem { Id = 5, Name = "Fifth Item", Price = 5.0m } },
		{ 6, new StoreItem { Id = 6, Name = "Sixth Item", Price = 6.0m } },
		{ 7, new StoreItem { Id = 7, Name = "Seventh Item", Price = 7.0m } },
		{ 8, new StoreItem { Id = 8, Name = "Eight Item", Price = 8.0m } },
		{ 9, new StoreItem { Id = 9, Name = "Ninth Item", Price = 9.0m } },
		{ 10, new StoreItem { Id = 10, Name = "Tenth Item", Price = 10.0m } }
	};

	[HttpGet]
	[Route("getstoreitems")]
	public IActionResult GetStoreItems()
	{
		return Ok(StoreItems.Values);
	}

	[HttpPost]
	[Route("additemtocart/{id:int}/{amount:int}")]
	public IActionResult AddItemToCart(int id, int amount)
	{
		if (!StoreItems.ContainsKey(id))
			return BadRequest("Item does not exist");

		ShoppingCart.TryAdd(id, 0);

		ShoppingCart[id] += amount;

		return Ok();
	}

	[HttpPost]
	[Route("completeorder")]
	public IActionResult CompleteOrder()
	{
		if (ShoppingCart.Count == 0)
			return BadRequest("Shopping cart is empty");

		ShoppingCart.Clear();

		return Ok();
	}

	//Area for Bonus tasks

	private static readonly Dictionary<int, int> ShoppingCart = new();

	private class StoreItem
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public decimal Price { get; set; }
	}

	[HttpGet]
	[Route("getcartitems")]
	public IActionResult GetCartItems()
	{
		if (ShoppingCart.Count == 0)
			return Ok("Shopping cart is empty.");

		var items = ShoppingCart.Select(entry =>
		{
			var storeItem = StoreItems.GetValueOrDefault(entry.Key);
			var price = storeItem?.Price ?? 0;

			return new
			{
				Id = entry.Key,
				Name = storeItem?.Name ?? "Unknown",
				PricePerProduct = price,
				Quantity = entry.Value,
				TotalPrice = price * entry.Value
			};
		});

		return Ok(items);
	}

	[HttpPost]
	[Route("removeitemfromcart/{id:int}")]
	public IActionResult RemoveItemFromCart(int id)
	{
		if (!ShoppingCart.ContainsKey(id))
			return BadRequest("Item is not in the cart.");

		if (!StoreItems.ContainsKey(id))
			return BadRequest("Item information not found in store.");

		var item = StoreItems[id];

		if (ShoppingCart[id] > 1)
		{
			ShoppingCart[id] -= 1;
			decimal total = item.Price * ShoppingCart[id];

			return Ok($"One quantity of item '{item.Name}' removed from the cart. Remaining quantity: {ShoppingCart[id]}, Price per item: {item.Price:C}, Total price for this item: {total:C}");
		}
		else
		{
			ShoppingCart.Remove(id);
			return Ok($"One quantity of item '{item.Name}' removed from the cart. Item '{item.Name}' completely removed.");
		}
	}
}