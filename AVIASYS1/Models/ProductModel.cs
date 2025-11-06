namespace AVIASYS1.Models
{
	public class ProductModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Category { get; set; } = string.Empty;
		public string Status { get; set; } = "В производстве";
	}
}