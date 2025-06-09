namespace TestProject1.Models;

public class CartItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal PricePerProduct { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}

