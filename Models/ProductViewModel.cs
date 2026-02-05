namespace MyShop.Models
{
	public class ProductViewModel
	{
		public Product Product { get; set; } = new Product();
		public double AvgRate { get; set; }
		public int TotalEnroll { get; set; }
	}
}
