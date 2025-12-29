namespace MyShop.Models
{
	public class Cart
	{
		public long Id { get; set; }
		public string? Name { get; set; }
		public string? Slug { get; set; }
		public string? Image { get; set; }
		public int Quantity { get; set; } = 1;
		public decimal? Price { get; set; } = 0;
	}
}
